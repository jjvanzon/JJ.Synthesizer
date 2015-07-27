using JJ.Business.Synthesizer.Enums;
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
        // TODO: Remove outcomented code.
        //[Obsolete("Possibly obsolete. Many methods that take ChildDocumentItemAlternativeKey might not use this property anymore.")]
        //public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
        public int EntityListIndex { get; set; }
    }
}
