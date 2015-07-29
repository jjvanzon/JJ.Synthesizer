using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<PatchListItemViewModel> List { get; set; }
    }
}
