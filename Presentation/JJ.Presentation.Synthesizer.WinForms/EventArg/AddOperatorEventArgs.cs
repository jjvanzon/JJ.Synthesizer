using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class AddOperatorEventArgs : EventArgs
    {
        public int PatchID { get; private set; }
        public int OperatorTypeID { get; private set; }

        public AddOperatorEventArgs(int patchID, int operatorTypeID)
        {
            PatchID = patchID;
            OperatorTypeID = operatorTypeID;
        }
    }
}