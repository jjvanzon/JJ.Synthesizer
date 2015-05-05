using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class PageEventArgs : EventArgs
    {
        public int PageNumber { get; private set; }

        public PageEventArgs(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}
