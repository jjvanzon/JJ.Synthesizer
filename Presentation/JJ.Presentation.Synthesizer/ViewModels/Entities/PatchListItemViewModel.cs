using JJ.Presentation.Synthesizer.ViewModels.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class PatchListItemViewModel
    {
        public PatchKeysViewModel Keys { get; set; }
        public string Name { get; set; }
    }
}
