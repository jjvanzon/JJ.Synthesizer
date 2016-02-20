using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveDetailsViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public IList<NodeViewModel> Nodes { get; set; }
        public int? SelectedNodeID { get; set; }
        public IList<IDAndName> NodeTypeLookup { get; set; }
    }
}
