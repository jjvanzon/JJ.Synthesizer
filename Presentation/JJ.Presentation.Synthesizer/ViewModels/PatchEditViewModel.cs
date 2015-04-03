using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchEditViewModel
    {
        public IList<OperatorViewModel> RootOperators { get; set; }
   }
}
