using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentDetailsViewModel
    {
        public bool Visible { get; set; }
        public IDAndName Document { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public bool CanDelete { get; set; }
        public bool IDVisible { get; set; }
    }
}
