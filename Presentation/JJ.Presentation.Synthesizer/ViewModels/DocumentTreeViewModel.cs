using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel
    {
        public bool Visible { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary> Only filled in for the root node. </summary>
        public ReferencedDocumentsTreeNodeViewModel ReferencedDocuments { get; set; }

        public IList<ChildDocumentTreeNodeViewModel> Instruments { get; set; }
        public IList<ChildDocumentTreeNodeViewModel> Effects { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel AudioFileOutputsNode { get; set; }
        public DummyViewModel ScalesNode { get; set; }
    }
}
