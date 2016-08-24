using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class GroupAndChildDocumentIDEventArgs : EventArgs
    {
        public string Group { get; }
        public int ChildDocumentID { get; }

        public GroupAndChildDocumentIDEventArgs(string group, int childDocumentID)
        {
            Group = group;
            ChildDocumentID = childDocumentID;
        }
    }
}
