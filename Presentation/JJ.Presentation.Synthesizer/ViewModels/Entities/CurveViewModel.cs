using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class CurveViewModel
    {
        public int ID { get; set; }
        public int ListIndex { get; set; }
        public string Name { get; set; }
        public IList<NodeViewModel> Nodes { get; set; }
    }
}
