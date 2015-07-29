using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputGridViewModel
    {
        public int DocumentID { get; set; }

        public bool Visible { get; set; }
        public IList<AudioFileOutputListItemViewModel> List { get; set; }
    }
}
