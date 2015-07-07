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
        public int ListIndex { get; private set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; private set; }
        public int? ChildDocumentListIndex { get; private set; }
        public string Value { get; private set; }

        public SetValueEventArgs(
            int listIndex, 
            ChildDocumentTypeEnum? childDocumentTypeEnum, 
            int? childDocumentListIndex,
            string value)
        {
            ListIndex = listIndex;
            ChildDocumentTypeEnum = childDocumentTypeEnum;
            ChildDocumentListIndex = childDocumentListIndex;
            Value = value;
        }
    }
}
