using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class MenuItemViewModel
    {
        public string Text { get; set; }

        // From a multi-lingual point of view, this is pretty much impossible.
        ///// <summary>
        ///// The character that you might type to automatically
        ///// activate the menu item.
        ///// </summary>
        //public char AltCharacter { get; set; }

        public IList<MenuItemViewModel> MenuItems { get; set; }
    }
}
