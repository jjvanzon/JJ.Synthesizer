using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Keys
{
    public sealed class SampleKeysViewModel
    {
        public int ID { get; set; }

        public int DocumentID { get; set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
        public int ListIndex { get; set; }
    }
}
