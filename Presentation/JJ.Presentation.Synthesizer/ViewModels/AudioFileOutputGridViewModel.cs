using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputGridViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public IList<AudioFileOutputListItemViewModel> List { get; set; }
    }
}
