using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class ChangeInputOutletEventArgs : EventArgs
    {
        public int ChildDocumentID { get; }
        public int InletID { get; }
        public int InputOutletID { get; }

        public ChangeInputOutletEventArgs(int childDocumentID, int inletID, int inputOutletID)
        {
            ChildDocumentID = childDocumentID;
            InletID = inletID;
            InputOutletID = inputOutletID;
        }
    }
}