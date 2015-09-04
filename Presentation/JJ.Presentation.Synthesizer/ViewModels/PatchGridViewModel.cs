using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<PatchListItemViewModel> List { get; set; }
    }
}
