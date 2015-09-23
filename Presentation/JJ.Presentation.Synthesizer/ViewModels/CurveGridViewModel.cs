using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}
