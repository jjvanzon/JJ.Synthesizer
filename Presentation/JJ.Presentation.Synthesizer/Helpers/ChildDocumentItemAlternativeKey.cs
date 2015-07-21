using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal class ChildDocumentItemAlternativeKey
    {
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
        public int EntityListIndex { get; set; }

        // TODO: Remove outcommented code.
        //public int? ChildDocumentID { get; set; }
    }
}
