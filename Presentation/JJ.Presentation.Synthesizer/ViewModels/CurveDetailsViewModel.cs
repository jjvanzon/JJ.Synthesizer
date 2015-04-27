using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurveDetailsViewModel
    {
        public CurveViewModel Curve { get; set; }
        public IList<IDName> NodeTypes { get; set; }
    }
}
