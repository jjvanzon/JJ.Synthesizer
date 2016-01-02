using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchDetailsViewModel
    {
        public PatchViewModel Entity { get; set; }
        public IList<OperatorTypeViewModel> OperatorToolboxItems { get; set; }
        public OperatorViewModel SelectedOperator { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public bool Visible { get; set; }
        public bool Successful { get; set; }
    }
}
