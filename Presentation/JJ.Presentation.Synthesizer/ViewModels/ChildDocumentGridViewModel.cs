using JJ.Business.CanonicalModel;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ChildDocumentGridViewModel
    {
        public ChildDocumentGridKeysViewModel Keys { get; set; }
        public bool Visible { get; set; }
        public IList<ChildDocumentListItemViewModel> List { get; set; }
    }
}
