using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class DocumentViewModel
    {
        public int ID { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }

        public ChildDocumentListViewModel InstrumentList { get; set; }
        public IList<ChildDocumentPropertiesViewModel> InstrumentPropertiesList { get; set; }
        public IList<ChildDocumentViewModel> InstrumentDocumentList { get; set; }

        public ChildDocumentListViewModel EffectList { get; set; }
        public IList<ChildDocumentPropertiesViewModel> EffectPropertiesList { get; set; }
        public IList<ChildDocumentViewModel> EffectDocumentList { get; set; }

        public SampleListViewModel SampleList { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public CurveListViewModel CurveList { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }

        public PatchListViewModel PatchList { get; set; }
        public IList<PatchDetailsViewModel> PatchDetailsList { get; set; }

        public AudioFileOutputListViewModel AudioFileOutputList { get; set; }
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }
    }
}
