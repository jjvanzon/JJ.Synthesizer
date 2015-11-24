using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class PatchGroupTreeNodeViewModel
    {
        public string Name { get; set; }
        public IList<PatchTreeNodeViewModel> Patches { get; set; }
    }
}
