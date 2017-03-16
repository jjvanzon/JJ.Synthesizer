using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class LibraryGridViewModel : ViewModelBase
    {
        public IList<LibraryListItemViewModel> List { get; set; }
    }
}