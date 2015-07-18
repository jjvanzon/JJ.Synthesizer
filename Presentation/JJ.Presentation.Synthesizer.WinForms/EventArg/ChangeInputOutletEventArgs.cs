using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class ChangeInputOutletEventArgs : EventArgs
    {
        public int InletID { get; private set; }
        public int InputOutletID { get; private set; }

        public ChangeInputOutletEventArgs(int inletID, int inputOutletID)
        {
            InletID = inletID;
            InputOutletID = inputOutletID;
        }
    }
}