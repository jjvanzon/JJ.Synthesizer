using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class CreateOperatorEventArgs : EventArgs
    {
        public int ChildDocumentID { get; }
        public int OperatorTypeID { get; }

        public CreateOperatorEventArgs(int childDocumentID, int operatorTypeID)
        {
            ChildDocumentID = childDocumentID;
            OperatorTypeID = operatorTypeID;
        }
    }
}