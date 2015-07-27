using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
       
        public int ID { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }

        public ChildDocumentGridViewModel InstrumentGrid { get; set; }
        public ChildDocumentGridViewModel EffectGrid { get; set; }

        public IList<ChildDocumentPropertiesViewModel> ChildDocumentPropertiesList { get; set; }

        /// <summary> Leading for saving the data. </summary>
        public IList<ChildDocumentViewModel> ChildDocumentList { get; set; }

        public SampleGridViewModel SampleGrid { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }

        public PatchGridViewModel PatchGrid { get; set; }
        public IList<PatchDetailsViewModel> PatchDetailsList { get; set; }

        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }
    }
}
