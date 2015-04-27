using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class AudioFileOutputListViewModel
    {
        public IList<AudioFileOutputListItemViewModel> List { get; set; }
        public PagerViewModel Pager { get; set; }
    }
}
