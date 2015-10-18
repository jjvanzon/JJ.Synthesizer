using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class NodeViewModel
    {
        public int ID { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }
        public IDAndName NodeType { get; set; }
    }
}
