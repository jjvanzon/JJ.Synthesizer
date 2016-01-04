using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveDetailsViewModel
    {
        public int ID { get; set; }
        public IList<NodeViewModel> Nodes { get; set; }
        public int? SelectedNodeID { get; set; }
        public IList<IDAndName> NodeTypeLookup { get; set; }
        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}
