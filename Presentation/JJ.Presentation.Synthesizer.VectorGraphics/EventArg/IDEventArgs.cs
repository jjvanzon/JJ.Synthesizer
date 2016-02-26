using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
{
    public class IDEventArgs : EventArgs
    {
        public int ID { get; private set; }

        public IDEventArgs(int id)
        {
            ID = id;
        }
    }
}
