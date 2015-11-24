using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System.Collections.Generic;
using System;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentTreeViewModel
    {
        public bool Visible { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        public PatchesTreeNodeViewModel PatchesNode { get; set; }

        public DummyViewModel CurvesNode { get; set; }
        public DummyViewModel SamplesNode { get; set; }
        public DummyViewModel ScalesNode { get; set; }
        public DummyViewModel AudioFileOutputsNode { get; set; }

        /// <summary> Only filled in for the root node. </summary>
        public ReferencedDocumentsTreeNodeViewModel ReferencedDocumentsNode { get; set; }

        [Obsolete]
        public IList<PatchTreeNodeViewModel> InstrumentNode { get; set; }

        [Obsolete]
        public IList<PatchTreeNodeViewModel> EffectNode { get; set; }
    }
}
