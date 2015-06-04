using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Keys
{
    public sealed class SampleListKeysViewModel
    {
        public int DocumentID { get; set; }
        public ChildDocumentTypeEnum? ChildDocumentTypeEnum { get; set; }
        public int? ChildDocumentListIndex { get; set; }
    }
}
