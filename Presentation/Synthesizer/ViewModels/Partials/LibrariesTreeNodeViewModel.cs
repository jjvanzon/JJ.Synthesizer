using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class LibrariesTreeNodeViewModel
    {
        public string Text { get; set; }
        public IList<LibraryViewModel> List { get; set; }
    }
}
