using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentPropertiesViewModel
    {
        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IDAndName Entity { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
