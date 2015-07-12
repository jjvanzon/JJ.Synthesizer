using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class NodeViewModel
    {
        public int ID { get; set; }

        public double Time { get; set; }
        public double Value { get; set; }
        public double Direction { get; set; }
        public IDAndName NodeType { get; set; }
    }
}
