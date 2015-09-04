using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class DocumentCannotDeleteViewModel
    {
        public bool Visible { get; set; }
        public IDAndName Document { get; set; }
        public IList<Message> Messages { get; set; }
    }
}
