using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class DocumentDetailsViewModel
    {
        public IDName Document { get; set; }
        public IList<Message> Messages { get; set; }
        public bool CanDelete { get; set; }
        public bool IDVisible { get; set; }
    }
}
