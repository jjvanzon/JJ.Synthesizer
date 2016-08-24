using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class DocumentAndChildEntityEventArgs : EventArgs
    {
        public int DocumentID { get; }
        public int ChildEntityID { get; }

        public DocumentAndChildEntityEventArgs(int documentID, int childEntityID)
        {
            DocumentID = documentID;
            ChildEntityID = childEntityID;
        }

    }
}
