using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
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

        public string WindowTitle { get; set; }
        public MenuViewModel Menu { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public IList<Message> WarningMessages { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public IList<Message> PopupMessages { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public NotFoundViewModel NotFound { get; set; }

        // The Document List

        public DocumentGridViewModel DocumentGrid { get; set; }
        public DocumentDetailsViewModel DocumentDetails { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeleteViewModel DocumentDelete { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeletedViewModel DocumentDeleted { get; set; }

        // The Open Document

        public DocumentViewModel Document { get; set; }

        public bool Successful  { get; set; }
    }
}
