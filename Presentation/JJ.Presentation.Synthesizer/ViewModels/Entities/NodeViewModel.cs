using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class NodeViewModel
    {
        public int ID { get; set; }
        public int ListIndex { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }
        public double Direction { get; set; }
        public IDAndName NodeType { get; set; }
    }
}
