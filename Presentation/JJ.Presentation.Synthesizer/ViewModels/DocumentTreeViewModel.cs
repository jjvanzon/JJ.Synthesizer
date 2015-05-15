using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel
    {
        public bool Visible { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary> Only filled in for the root node. </summary>
        public ReferencedDocumentsNodeViewModel ReferencedDocuments { get; set; }

        public IList<ChildDocumentTreeViewModel> Instruments { get; set; }
        public IList<ChildDocumentTreeViewModel> Effects { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel AudioFileOutputsNode { get; set; }
        public DummyViewModel PatchesNode { get; set; }
    }
}
