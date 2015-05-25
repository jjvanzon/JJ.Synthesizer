using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class Int32EventArgs : EventArgs
    {
        public int Int32 { get; private set; }

        public Int32EventArgs(int int32)
        {
            Int32 = int32;
        }
    }
}
