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
        //public int DocumentID { get; set; }

        public MenuViewModel Menu { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public NotFoundViewModel NotFound { get; set; }

        public IList<Message> Messages { get; set; }

        public DocumentListViewModel DocumentList { get; set; }

        public DocumentTreeViewModel DocumentTree { get; set; }

        public DocumentDetailsViewModel DocumentDetails { get; set; }

        public DocumentPropertiesViewModel DocumentProperties { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeleteViewModel DocumentDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeletedViewModel DocumentDeleted { get; set; }

        public AudioFileOutputListViewModel AudioFileOutputList { get; set; }

        public CurveListViewModel CurveList { get; set; }

        public PatchListViewModel PatchList { get; set; }

        public SampleListViewModel SampleList { get; set; }

        /// <summary>
        /// Temporary. It will be a list of AudioFileOutputDetailsViewModels later.
        /// </summary>
        public AudioFileOutputDetailsViewModel AudioFileOutputDetails { get; set; }

        public PatchDetailsViewModel PatchDetails { get; set; }
    }
}
