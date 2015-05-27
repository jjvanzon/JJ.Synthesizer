using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ChildDocumentPropertiesViewModel
    {
        public bool Visible { get; set; }

        public IDNameAndListIndexViewModel Document { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
