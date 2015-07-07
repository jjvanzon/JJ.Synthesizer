using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class MoveOperatorEventArgs : EventArgs
    {
        public int ListIndex { get; private set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; private set; }
        public int? ChildDocumentListIndex { get; private set; }
        public int OperatorIndexNumber { get; private set; }
        public float CenterX { get; private set; }
        public float CenterY { get; private set; }

        public MoveOperatorEventArgs(
            int listIndex, 
            ChildDocumentTypeEnum? childDocumentTypeEnum, 
            int? childDocumentListIndex,
            int operatorIndexNumber,
            float centerX, 
            float centerY)
        {
            ListIndex = listIndex;
            ChildDocumentTypeEnum = childDocumentTypeEnum;
            ChildDocumentListIndex = childDocumentListIndex;
            OperatorIndexNumber = operatorIndexNumber;
            CenterX = centerX;
            CenterY = centerY;
        }
    }
}
