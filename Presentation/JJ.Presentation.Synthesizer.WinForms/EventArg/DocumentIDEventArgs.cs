using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class DocumentIDEventArgs : EventArgs
    {
        public int DocumentID { get; private set; }

        public DocumentIDEventArgs(int documentID)
        {
            DocumentID = documentID;
        }
    }
}