using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class SetValueEventArgs : EventArgs
    {
        public int PatchID { get; private set; }
        public string Value { get; private set; }

        public SetValueEventArgs(int patchID, string value)
        {
            PatchID = patchID;
            Value = value;
        }
    }
}