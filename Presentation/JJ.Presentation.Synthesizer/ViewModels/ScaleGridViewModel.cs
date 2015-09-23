using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class ScaleGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}
