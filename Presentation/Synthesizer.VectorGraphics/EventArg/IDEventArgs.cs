using System;

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
