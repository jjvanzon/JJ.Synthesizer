using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class NodeViewModel
    {
        public int ID { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }
        public IDAndName NodeType { get; set; }
        public string Caption { get; set; }
    }
}
