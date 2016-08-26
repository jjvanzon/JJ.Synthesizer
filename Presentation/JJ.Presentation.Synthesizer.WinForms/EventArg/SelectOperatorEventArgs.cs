using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class SelectOperatorEventArgs : EventArgs
    {
        public int PatchID { get; }
        public int OperatorID { get; }

        public SelectOperatorEventArgs(int patchID, int operatorID)
        {
            PatchID = patchID;
            OperatorID = operatorID;
        }
    }
}