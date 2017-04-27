using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchDetailsViewModel : ViewModelBase
    {
        public PatchViewModel Entity { get; set; }
        public IList<IDAndName> OperatorToolboxItems { get; set; }
        public OperatorViewModel SelectedOperator { get; set; }
        public bool CanSave { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}
