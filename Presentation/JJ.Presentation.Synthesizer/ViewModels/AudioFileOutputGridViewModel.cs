using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputGridViewModel
    {
        public int DocumentID { get; set; }

        public bool Visible { get; set; }
        public bool Successful { get; set; }

        public IList<AudioFileOutputListItemViewModel> List { get; set; }
    }
}
