using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class AddOperatorEventArgs : EventArgs
    {
        public int ListIndex { get; private set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; private set; }
        public int? ChildDocumentListIndex { get; private set; }
        public int OperatorTypeID { get; private set; }

        public AddOperatorEventArgs(
            int listIndex, 
            ChildDocumentTypeEnum? childDocumentTypeEnum, 
            int? childDocumentListIndex,
            int operatorTypeID)
        {
            ListIndex = listIndex;
            ChildDocumentTypeEnum = childDocumentTypeEnum;
            ChildDocumentListIndex = childDocumentListIndex;
            OperatorTypeID = operatorTypeID;
        }
    }
}
