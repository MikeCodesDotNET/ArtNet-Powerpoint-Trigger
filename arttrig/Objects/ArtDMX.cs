using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arttrig.Objects
{
    public class ArtDMX
    {
       
        public byte Value
        {
            get
            {
                return chanValue;
            }
            set
            {
                chanValue = value;
            }
        }

        public int Universe
        {
            get
            {
                return universe;
            }
            set
            {
                universe = value;
            }
            
        }

        public byte Channel
        {
            get
            {
                return channel;
            }
            set
            {
                channel = value;
            }
        }

        
        private byte chanValue;
        private int universe;
        private byte channel;


    }
}
