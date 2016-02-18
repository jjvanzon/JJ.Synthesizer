using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveGridViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}
