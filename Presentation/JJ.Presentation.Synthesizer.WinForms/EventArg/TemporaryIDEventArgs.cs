using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class TemporaryIDEventArgs : EventArgs
    {
        public Guid TemporaryID { get; private set; }

        public TemporaryIDEventArgs(Guid temporaryID)
        {
            TemporaryID = temporaryID;
        }
    }
}
