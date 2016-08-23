using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class MoveOperatorEventArgs : EventArgs
    {
        public int ChildDocumentID { get; }
        public int OperatorID { get; }
        public float X { get; }
        public float Y { get; }

        public MoveOperatorEventArgs(int childDocumentID, int operatorID, float x, float y)
        {
            ChildDocumentID = childDocumentID;
            OperatorID = operatorID;
            X = x;
            Y = y;
        }
    }
}