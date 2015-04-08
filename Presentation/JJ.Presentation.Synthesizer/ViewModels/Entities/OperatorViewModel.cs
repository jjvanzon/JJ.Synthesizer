using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class OperatorViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public bool IsSelected { get; set; }

        public IList<InletViewModel> Inlets { get; set; }
        public IList<OutletViewModel> Outlets { get; set; }
    }
}
