using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<CurveListItemViewModel> List { get; set; }
    }
}
