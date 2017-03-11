using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class MainViewModel
    {
        // General

        public string TitleBar { get; set; }
        public MenuViewModel Menu { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public IList<Message> WarningMessages { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public IList<Message> PopupMessages { get; set; }
        
        // The Document List

        public DocumentGridViewModel DocumentGrid { get; set; }
        /// <summary> Only for creating a new document. </summary>
        public DocumentDetailsViewModel DocumentDetails { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentCannotDeleteViewModel DocumentCannotDelete { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeleteViewModel DocumentDelete { get; set; }
        /// <summary> It is suggested you show this as a modal window. </summary>
        public DocumentDeletedViewModel DocumentDeleted { get; set; }

        // The Open Document

        public DocumentViewModel Document { get; set; }
    }
}
