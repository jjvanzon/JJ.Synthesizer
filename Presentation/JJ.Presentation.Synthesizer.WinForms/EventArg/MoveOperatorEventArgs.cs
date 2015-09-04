using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class MoveOperatorEventArgs : EventArgs
    {
        public int OperatorID { get; private set; }
        public float CenterX { get; private set; }
        public float CenterY { get; private set; }

        public MoveOperatorEventArgs(int operatorID, float centerX, float centerY)
        {
            OperatorID = operatorID;
            CenterX = centerX;
            CenterY = centerY;
        }
    }
}