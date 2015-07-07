using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class SelectOperatorEventArgs : EventArgs
    {
        public int ListIndex { get; private set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; private set; }
        public int? ChildDocumentListIndex { get; private set; }
        public int OperatorIndexNumber { get; private set; }

        public SelectOperatorEventArgs(
            int listIndex, 
            ChildDocumentTypeEnum? childDocumentTypeEnum, 
            int? childDocumentListIndex,
            int operatorIndexNumber)
        {
            ListIndex = listIndex;
            ChildDocumentTypeEnum = childDocumentTypeEnum;
            ChildDocumentListIndex = childDocumentListIndex;
            OperatorIndexNumber = operatorIndexNumber;
        }
    }
}
