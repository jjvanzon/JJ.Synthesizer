using JJ.Business.CanonicalModel;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class InstrumentListViewModel
    {
        public int ParentDocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<IDNameAndTemporaryID> List { get; set; }
    }
}
