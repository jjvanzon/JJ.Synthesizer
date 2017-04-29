using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class DocumentGridViewModel : ViewModelBase
    {
        public IList<IDAndName> List { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}
