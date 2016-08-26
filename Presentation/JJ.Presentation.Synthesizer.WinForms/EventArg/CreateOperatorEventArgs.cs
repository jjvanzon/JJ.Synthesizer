using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class CreateOperatorEventArgs : EventArgs
    {
        public int PatchID { get; }
        public int OperatorTypeID { get; }

        public CreateOperatorEventArgs(int patchID, int operatorTypeID)
        {
            PatchID = patchID;
            OperatorTypeID = operatorTypeID;
        }
    }
}