using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class MoveEntityEventArgs : EventArgs
    {
        public int EntityID { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public MoveEntityEventArgs(int entityID, float x, float y)
        {
            EntityID = entityID;
            X = x;
            Y = y;
        }
    }
}