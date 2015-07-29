using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Helpers;
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
        public int ID { get; set; }
        public string Name { get; set; }
        public IDAndName ChildDocumentType { get; set; }
        public IList<IDAndName> ChildDocumentTypeLookup { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public bool Successful { get; set; }
    }
}
