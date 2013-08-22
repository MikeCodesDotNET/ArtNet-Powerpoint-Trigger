#Region "Imports"
Imports System.Text
Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Threading
#End Region


Public Class ArtNet


#Region "Declares"

#Region "Network"
    ' network 
    Private UDP_Server As UdpClient
    Private UDP_Client As New UdpClient
    ' recieve thread 
    Private RecieveThread As Thread
    ' raised if a network error occurs
    Private Event NetworkError(ByVal ErrorStr As String)
    ' raised apon UDP reception
    Private Event DataArrival(ByVal data() As Byte)

#End Region

#Region "Art-Net"
    ' Art-Net constants
    Private Const OemUser As Short = &HFFS  ' Reserved for Unknown user/manufact
    Private Const ProtocolVersion As Short = 14
    Private Const DefaultPort As Short = 6454 ' network port for Art-Net

    ' packet types
    Private Const OpPoll As Short = &H2000S  'Poll */
    Private Const OpPollReply As Short = &H2100S  'ArtPollReply */
    Private Const OpPollFpReply As Short = &H2200S  'Reply from Four-Play */
    Private Const OpOutput As Short = &H5000S  'Output */
    Private Const OpAddress As Short = &H6000S  'Program Node Settings */
    Private Const OpInput As Short = &H7000S  'Setup DMX input enables */

    Private Const StyleNode As Short = 0  ' Responder is a Node (DMX <-> Ethernet Device)
    Private Const StyleServer As Short = 1  ' Lighting console or similar

    Private Const MaxNumPorts As Short = 1  '4
    Private Const MaxExNumPorts As Short = 32
    Private Const ShortNameLength As Short = 18
    Private Const LongNameLength As Short = 64
    Private Const PortNameLength As Short = 32
    Private Const MaxDataLength As Short = 512 - 1  ' 0..511


    ' These are used in creation of packets, they are block copied into 
    ' place to gain even a tiny bit more speed...

    ' Ascii "Art-Net" & 0
    Private ArtNetHead() As Byte = {65, 114, 116, 45, 78, 101, 116, 0}

    ' Addr... IP address (4 byte) and port (2 byte) of our node
    Private ArtAddr() As Byte = {127, 0, 0, 1, LoByte(DefaultPort), HiByte(DefaultPort)}

    Private ArtShortName As String = "Art-Net Node"
    Private ArtLongName As String = "ArtNet PowerPoint Trigger"
    Private ArtNodeReport As String = "Seems to be working..."

    ' true if artnet has been detected
    Private ArtDetected As Boolean

#End Region

#Region "DMX"
    ' this event is raised when a art-net dmx packet is recieved
    Public Event DMX_Recieved(ByVal Universe As Integer, ByVal DMX() As Byte)
    'Public Event DMX_Updated(ByVal Universe As Integer)

#End Region

#Region "Misc"
    ' simply for storing some info about nodes we find
    Public Structure NodeInfoStruct
        Public Name As String
        Public ShortName As String
        Public NodeReport As String
        Public NumberOfPorts As String
        Public IPAddress As String
    End Structure
#End Region

#End Region


#Region "Public Interface"

    ' note: this broadcasts to everyone :)
    ' Public BroadcastAddress as String = "2.255.255.255"
    ' this is the address Art-Net packets will be sent to
    Public BroadcastAddress As String = "255.255.255.255"

    ' has art-net been recieved in this session
    Public ReadOnly Property Detected() As Boolean
        Get
            Return ArtDetected
        End Get
    End Property
    ' set/retrieve the ShortName of our Node
    Public Property ShortName() As String
        Get
            Return ArtShortName
        End Get
        Set(ByVal Value As String)
            ArtShortName = Left(Value, ShortNameLength)
        End Set
    End Property
    ' set/retrieve the LongName of our Node
    Public Property LongName() As String
        Get
            Return ArtLongName
        End Get
        Set(ByVal Value As String)
            ArtLongName = Left(Value, LongNameLength)
        End Set
    End Property



    ' send a DMX packet to 'Universe', dmx is in a byte array 'Data' and the DataLength also is required
    Public Sub SendDMX(ByVal Universe As Int16, ByVal Data() As Byte, ByVal DataLength As Int16)
        ' check the param supplied
        ' msgbox's are used because they should never invaild, you must get it write!
        If DataLength > 512 Then
            MsgBox("Art-Net> Universe '" & Universe & "' larger than 512 bytes")
            Exit Sub
        ElseIf DataLength < 0 Then
            MsgBox("Art-Net> Universe '" & Universe & "' Invalid")
            Exit Sub
        End If

        ' this constant defines the length in bytes of the header info of the 
        ' OpOutput packet
        Const HeaderLength = 17

        ' prepare a buffer of the correct length
        Dim buf(HeaderLength + DataLength) As Byte

        ' copy the header 'Art-Net ' into the buffer
        Buffer.BlockCopy(ArtNetHead, 0, buf, 0, ArtNetHead.Length)

        ' op code 
        buf(8) = LoByte(OpOutput)  ' dmx output
        buf(9) = HiByte(OpOutput)

        ' version (two bytes)
        buf(10) = 0
        buf(11) = ProtocolVersion
        'sequence
        buf(12) = 0
        'physical 
        buf(13) = 0
        ' universe (two bytes)
        buf(14) = LoByte(Universe)
        buf(15) = HiByte(Universe)
        ' data length (two bytes, note: manual byte-swap!)
        buf(16) = HiByte(DataLength) ' data length 1 - 512
        buf(17) = LoByte(DataLength) ' data length

        Try
            ' now copy in the dmx data
            Buffer.BlockCopy(Data, 0, buf, 18, DataLength)
            Exit Try
        Catch ex As Exception
            Debug.WriteLine("blockcopy failure! " & ex.Message)
            Exit Sub
        End Try

        ' broadcast the newly formed Art-Net packet
        UDP_Send(BroadcastAddress, DefaultPort, buf)
    End Sub


#End Region


#Region "Management"

    ' this recieves and processes data from the network
    Private Sub ArtRecieve(ByVal Data() As Byte)
        Dim x As Short


        Dim OpCode As Int16

        Try


            ' check for art-net header, exit if not an 'Art-Net ' packet
            For x = 0 To ArtNetHead.Length - 1
                If Data(x) <> ArtNetHead(x) Then Exit Sub
            Next

            ArtDetected = True

            ' determine the OpCode of the packet
            OpCode = MakeInt16(Data(8), Data(9))

            ' do something with the packet
            Select Case OpCode

                Case OpOutput ' we recieved an Art-Net DMX packet

                    Dim VerH As Byte = Data(10)
                    Dim Ver As Byte = Data(11)

                    ' chech version
                    If Ver <> ProtocolVersion Then
                        Debug.WriteLine("Possible Version Conflict!")
                    End If

                    Dim Sequence As Byte = Data(12) ' not used in this app
                    Dim Physical As Byte = Data(13) ' reserved

                    'where the dmx is destined
                    Dim Universe As Int16 = MakeInt16(Data(15), Data(14))
                    ' the length of the dmx data
                    Dim Length As Int16 = MakeInt16(Data(17), Data(16))

                    Dim buf(512) As Byte ' temp buffer

                    ' get the dmx data
                    For x = 0 To Length
                        buf(x) = Data(x + 17)
                    Next

                    ' we have recieved a dmx packet
                    RaiseEvent DMX_Recieved(Universe, buf)


                Case OpPoll ' Poll for nodes, 
                    Send_ArtPollReply() ' send a reply...
                    Debug.WriteLine("Recieved Poll, Sent Reply")

                Case OpPollReply
                    Debug.WriteLine("Recieved Art Poll Reply")


                Case Else
                    Debug.WriteLine("Unknown/NYI OpCode: " & OpCode)
            End Select

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    ' send a 'ArtPoll', any node should respond with a ArtPollReply
    Private Sub Send_ArtPoll()

        Const HeaderLength = 13

        ' prepare a buffer of the correct length
        Dim buf(HeaderLength) As Byte

        ' copy the header 'Art-Net ' into the buffer
        Buffer.BlockCopy(ArtNetHead, 0, buf, 0, ArtNetHead.Length)

        ' op code 
        buf(8) = LoByte(OpPoll)  ' OpPoll Artnet Poll Request
        buf(9) = HiByte(OpPoll)

        ' version (two bytes)
        buf(10) = 0
        buf(11) = ProtocolVersion

        '  TalkToMe As Byte  ' bit 0 = not used

        '   Prev def was bit 0 = 0 if reply is broadcast
        '        bit 0 = 1 if reply is to server IP
        ' All replies are noe broadcast as this feature caused too many
        ' tech support calls
        ' bit 1 = 0 then Node only replies when polled
        ' bit 1 = 1 then Node sends reply when it needs to
        buf(12) = 0

        ' pad As Byte
        buf(13) = 0

        ' broadcast the newly formed Art-Net packet
        UDP_Send(BroadcastAddress, DefaultPort, buf)

    End Sub

    ' send a 'ArtPollReply' packet,in response to an ArtPoll
    Private Sub Send_ArtPollReply()
        Const HeaderLength = 239 ' total length of this packet

        ' prepare a buffer of the correct length
        Dim buf(HeaderLength) As Byte

        ' copy the header 'Art-Net ' into the buffer
        Buffer.BlockCopy(ArtNetHead, 0, buf, 0, ArtNetHead.Length)

        ' op code 
        buf(8) = LoByte(OpPollReply)  ' OpPoll Artnet Poll Request
        buf(9) = HiByte(OpPollReply)

        ' Address of node (us) (4 bytes (IP) + 2 bytes (port))
        ' copy our pre-prepared address into the buffer
        Buffer.BlockCopy(ArtAddr, 0, buf, 10, ArtAddr.Length)

        ' The node's current FIRMWARE VERS lo
        buf(16) = 1   'VersionInfoH As Byte   
        buf(17) = 1   'VersionInfo As Byte  

        'SubSwitchH As Byte     ' 0 - not used yet
        ''subswitch As Byte      ' from switch on front panel (0-15)
        buf(18) = 0
        buf(19) = 0 ' SUB-NET  (0-15)

        'Oem As Integer
        buf(20) = HiByte(OemUser)
        buf(21) = LoByte(OemUser)

        'UbeaVersion As Byte   ' Firmware version of UBEA
        buf(22) = 0

        'Status  As Byte
        buf(23) = 0

        'EstaMan  As Integer        ' Reserved for ESTA manufacturer id lo, zero for now
        buf(24) = 0
        buf(25) = 0

        Dim x As Byte

        ' build the names, and the node report...
        For x = 26 To 26 + ArtShortName.Length - 1
            buf(x) = Asc(ArtShortName.ToCharArray(x - 26, 1))
        Next
        For x = 44 To 44 + ArtLongName.Length - 1
            buf(x) = Asc(ArtLongName.ToCharArray(x - 44, 1))
        Next
        For x = 108 To 108 + ArtNodeReport.Length - 1
            buf(x) = Asc(ArtNodeReport.ToCharArray(x - 108, 1))
        Next

        ' number of ports supported
        buf(172) = 0  ' hi
        buf(173) = 4  ' low

        'PortTypes(1 To MaxNumPorts) As Byte
        buf(174) = 0 ' port 1 type 
        buf(175) = 0 ' port 2 type
        buf(176) = 0 ' port 3 type
        buf(177) = 0 ' port 4 type

        'GoodInput(1 To MaxNumPorts) As Byte
        buf(178) = 0
        buf(179) = 0
        buf(180) = 0
        buf(181) = 0

        'GoodOutput(1 To MaxNumPorts)
        buf(182) = 0
        buf(183) = 0
        buf(184) = 0
        buf(185) = 0

        'Swin(1 To MaxNumPorts)  As Byte
        buf(186) = 0
        buf(187) = 0
        buf(188) = 0
        buf(189) = 0

        'Swout(1 To MaxNumPorts)   As Byte
        buf(190) = 0
        buf(191) = 0
        buf(192) = 0
        buf(193) = 0

        'SwVideo    As Byte
        buf(194) = 0

        'SwMacro  As Byte
        buf(195) = 0

        'SwRemote   As Byte
        buf(196) = 0

        'Spare1    As Byte             ' Spare, currently zero
        'Spare2    As Byte             ' Spare, currently zero
        'Spare3    As Byte              ' Spare, currently zero
        For x = 197 To 199
            buf(x) = 0
        Next

        'Style     As Byte               ' Set to Style code to describe type of equipment
        buf(200) = StyleNode ' StyleServer

        'Mac(1 To 6) As Byte               ' Mac Address, zero if info not available
        buf(201) = 0
        buf(202) = 0
        buf(203) = 0
        buf(204) = 0
        buf(205) = 0
        buf(206) = 0

        'Filler(1 To 32)   As Byte              ' Filler bytes, currently zero.
        For x = 207 To 207 + 32
            buf(x) = x
        Next

        buf(239) = 255
        ' That's it!

        ' broadcast the newly formed Art-Net packet
        UDP_Send(BroadcastAddress, DefaultPort, buf)
    End Sub

#End Region

#Region "Network"

    ' start a thread to listen for UDP packets
    Private Function UDP_Listen(ByVal Port As Integer) As Boolean
        Try

            UDP_Server = New UdpClient(Port) ' listen on port for UDP

            RecieveThread = New Thread(AddressOf UDP_Recieve) ' create a recieve thread
            RecieveThread.Start() ' start a recieve thread
        Catch err As Exception
            RaiseEvent NetworkError(err.ToString)
        End Try

    End Function

    ' send a byte array
    Private Sub UDP_Send(ByVal Host As String, ByVal Port As Integer, ByVal Data As Byte())
        Try
            UDP_Client.Connect(Host, Port)
            UDP_Client.Send(Data, Data.Length)
        Catch err As Exception
            RaiseEvent NetworkError(err.ToString)
        End Try
    End Sub

    Private Sub UDP_Recieve()
        Dim LocalIP As String

        ' get the local ip
        Dim LocalHostName As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
        LocalIP = CType(LocalHostName.AddressList.GetValue(0), IPAddress).ToString

        Do
            Try

                ' store the remote ip 
                Dim RemoteIP_EP As New IPEndPoint(IPAddress.Broadcast, 0)

                ' wait for data from the socket
                Dim data() As Byte = UDP_Server.Receive(RemoteIP_EP)

                ' only process packets not from us, or else you get feedback :)
                If LocalIP.ToString <> RemoteIP_EP.Address.ToString Then
                    RaiseEvent DataArrival(data)
                End If

                Thread.Sleep(0) ' chill for a bit
            Catch err As Exception
                RaiseEvent NetworkError(err.ToString)
            End Try
        Loop
    End Sub

    Private Sub CloseSocket()
        Try
            UDP_Server.Close()
            RecieveThread.Abort()
        Catch err As Exception
            RaiseEvent NetworkError(err.ToString)
        End Try
    End Sub
#End Region

#Region "Byte Functions"

    ' break an int16 into it's lo and hi bytes
    Private Function HiByte(ByVal wParam As Integer)
        HiByte = wParam \ &H100 And &HFF&
    End Function
    Private Function LoByte(ByVal wParam As Integer)
        LoByte = wParam And &HFF&
    End Function
    ' perform a byte-swap
    Private Function End16(ByVal iNum As Short) As Short
        Dim iRes As Short
        iRes = CShort(iNum And &HFFS) * 2 ^ 8
        iRes = iRes Or CShort(iNum And &HFF00S) / 256
        Return (iRes - IIf(iRes > 32767, 65536, 0))
    End Function
    ' make an integer from two bytes 
    Private Function MakeInt16(ByVal lsb As Byte, ByVal msb As Byte) As Int16
        Dim newnum As Int16
        newnum = msb
        newnum = newnum << 8
        newnum = newnum + lsb
        Return newnum
    End Function

#End Region

#Region "New() & Finalize()"

    Public Sub New(Optional ByVal ListenForIncommingData As Boolean = True)
        ' add a handler to the DataArrival event to the ArtRecieve Sub
        AddHandler DataArrival, AddressOf ArtRecieve
        ' start listening for Art-Net packets
        If ListenForIncommingData = True Then UDP_Listen(6454)
    End Sub

    Protected Overrides Sub Finalize()
        CloseSocket() ' close the socket!
        MyBase.Finalize()
    End Sub

#End Region

End Class
