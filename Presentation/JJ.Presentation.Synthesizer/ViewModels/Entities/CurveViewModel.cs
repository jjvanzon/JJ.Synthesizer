using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class CurveViewModel
    {
        public CurveKeysViewModel Keys { get; set; }
        public string Name { get; set; }
        public IList<NodeViewModel> Nodes { get; set; }
    }
}
