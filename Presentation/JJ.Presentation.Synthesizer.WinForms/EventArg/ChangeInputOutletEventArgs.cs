using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class ChangeInputOutletEventArgs : EventArgs
    {
        public int ListIndex { get; private set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; private set; }
        public int? ChildDocumentListIndex { get; private set; }
        public int Inlet_OperatorIndexNumber{ get; private set; }
        public int Inlet_ListIndex{ get; private set; }
        public int InputOutlet_OperatorIndexNumber{ get; private set; }
        public int InputOutlet_ListIndex { get; private set; }

        public ChangeInputOutletEventArgs(
            int listIndex,
            ChildDocumentTypeEnum? childDocumentTypeEnum,
            int? childDocumentListIndex,
            int inlet_OperatorIndexNumber,
            int inlet_ListIndex,
            int inputOutlet_OperatorIndexNumber,
            int inputOutlet_ListIndex)
        {
            ListIndex = listIndex;
            ChildDocumentTypeEnum = childDocumentTypeEnum;
            ChildDocumentListIndex = childDocumentListIndex;
            Inlet_OperatorIndexNumber = inlet_OperatorIndexNumber;
            Inlet_ListIndex = inlet_ListIndex;
            InputOutlet_OperatorIndexNumber = inputOutlet_OperatorIndexNumber;
            InputOutlet_ListIndex = inputOutlet_ListIndex;
        }                                                                    
    }
}
