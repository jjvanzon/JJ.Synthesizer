using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class NodeViewModel
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public IDAndName NodeType { get; set; }
        public string Caption { get; set; }
    }
}
