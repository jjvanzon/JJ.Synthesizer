using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class IDEventArgs : EventArgs
    {
        public int ID { get; private set; }

        public IDEventArgs(int id)
        {
            ID = id;
        }
    }
}
