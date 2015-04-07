using JJ.Business.CanonicalModel;
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
        public PatchViewModel Patch { get; set; }

        public List<ValidationMessage> ValidationMessages { get; set; }

        public bool SavedMessageVisible { get; set; }
    }
}
