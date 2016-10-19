using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;
using System;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel : ViewModelBase
    {
        public int ID { get; set; }

        public PatchesTreeNodeViewModel PatchesNode { get; set; }
        public TreeLeafViewModel CurvesNode { get; set; }
        public TreeLeafViewModel SamplesNode { get; set; }
        public TreeLeafViewModel ScalesNode { get; set; }
        public TreeLeafViewModel AudioOutputNode { get; set; }
        public TreeLeafViewModel AudioFileOutputListNode { get; set; }

        public ReferencedDocumentsTreeNodeViewModel ReferencedDocumentNode { get; set; }
    }
}
