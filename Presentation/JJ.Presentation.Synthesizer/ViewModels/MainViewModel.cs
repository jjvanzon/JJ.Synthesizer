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
        public MenuViewModel Menu { get; set; }

        public DocumentListViewModel DocumentList { get; set; }

        public DocumentTreeViewModel DocumentTree { get; set; }

        public DocumentDetailsViewModel DocumentDetails { get; set; }

        public DocumentPropertiesViewModel DocumentProperties { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentConfirmDeleteViewModel DocumentConfirmDelete { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeleteConfirmedViewModel DocumentDeleteConfirmed { get; set; }

        /// <summary> It is suggested you show this as a modal window. </summary>
        public NotFoundViewModel NotFound { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
