using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class ViewMenuViewModel
    {
        public MenuItemViewModel DocumentsMenuItem { get; set; }
        public MenuItemViewModel DocumentTreeMenuItem { get; set; }
        public MenuItemViewModel InstrumentsMenuItem { get; set; }

        public MenuItemViewModel AudioFileOutputsMenuItem { get; set; }
        public MenuItemViewModel CurvesMenuItem { get; set; }
        public MenuItemViewModel PatchesMenuItem { get; set; }
        public MenuItemViewModel SamplesMenuItem { get; set; }
        public MenuItemViewModel AudioFileOutputPropertiesMenuItem { get; set; }
        public MenuItemViewModel PatchDetailsMenuItem { get; set; }

    }
}
