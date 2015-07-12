using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class SelectOperatorEventArgs : EventArgs
    {
        public int PatchID { get; private set; }
        public int OperatorID { get; private set; }

        public SelectOperatorEventArgs(int patchID, int operatorID)
        {
            PatchID = patchID;
            OperatorID = operatorID;
        }
    }
}