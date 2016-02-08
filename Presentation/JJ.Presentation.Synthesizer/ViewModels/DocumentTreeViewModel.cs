using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;
using System;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public PatchesTreeNodeViewModel PatchesNode { get; set; }
        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel ScalesNode { get; set; }
        public DummyViewModel AudioFileOutputsNode { get; set; }

        public ReferencedDocumentsTreeNodeViewModel ReferencedDocumentsNode { get; set; }
    }
}
