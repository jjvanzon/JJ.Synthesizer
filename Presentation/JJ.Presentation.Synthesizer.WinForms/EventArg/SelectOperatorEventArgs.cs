using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class SelectOperatorEventArgs : EventArgs
    {
        public int ChildDocumentID { get; }
        public int OperatorID { get; }

        public SelectOperatorEventArgs(int childDocumentID, int operatorID)
        {
            ChildDocumentID = childDocumentID;
            OperatorID = operatorID;
        }
    }
}