using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
