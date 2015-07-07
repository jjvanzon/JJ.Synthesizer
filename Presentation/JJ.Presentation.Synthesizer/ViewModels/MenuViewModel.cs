using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class MenuViewModel
    {
        public MenuItemViewModel DocumentsMenuItem { get; set; }
        public MenuItemViewModel DocumentCloseMenuItem { get; set; }
        public MenuItemViewModel DocumentTreeMenuItem { get; set; }
        public MenuItemViewModel DocumentSaveMenuItem { get; set; }
    }
}
