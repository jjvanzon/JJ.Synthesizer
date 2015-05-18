using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class MainViewModel
    {
        // General

        public MenuViewModel Menu { get; set; }
        public IList<Message> Messages { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public NotFoundViewModel NotFound { get; set; }

        // The Open Document

        public int DocumentID { get; set; }
        public string WindowTitle { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public InstrumentListViewModel Instruments { get; set; }

        public EffectListViewModel Effects { get; set; }
        public SampleListViewModel Samples { get; set; }
        public CurveListViewModel Curves { get; set; }
        public PatchListViewModel Patches { get; set; }
        public AudioFileOutputListViewModel AudioFileOutputs { get; set; }

        // The Document List

        public DocumentListViewModel DocumentList { get; set; }
        public DocumentDetailsViewModel DocumentDetails { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeleteViewModel DocumentDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeletedViewModel DocumentDeleted { get; set; }

        // Temporary View Models

        /// <summary> Temporary. It will be a list of AudioFileOutputDetailsViewModels later. </summary>
        public AudioFileOutputDetailsViewModel TemporaryAudioFileOutputDetails { get; set; }

        /// <summary> Temporary. It will be a list of PatchDetailsViewModels later. </summary>
        public PatchDetailsViewModel TemporaryPatchDetails { get; set; }
    }
}
