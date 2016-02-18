using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchDetailsViewModel : ViewModelBase
    {
        public PatchViewModel Entity { get; set; }
        public IList<OperatorTypeViewModel> OperatorToolboxItems { get; set; }
        public OperatorViewModel SelectedOperator { get; set; }
    }
}
