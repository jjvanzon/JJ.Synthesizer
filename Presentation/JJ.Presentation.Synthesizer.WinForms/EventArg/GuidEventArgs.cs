using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class GuidEventArgs : EventArgs
    {
        public Guid Guid { get; private set; }

        public GuidEventArgs(Guid guid)
        {
            Guid = guid;
        }
    }
}
