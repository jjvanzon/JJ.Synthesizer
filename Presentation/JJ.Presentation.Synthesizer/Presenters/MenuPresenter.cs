using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class MenuPresenter
    {
        public MenuViewModel Show()
        {
            var viewModel = new MenuViewModel
            {
                MenuItems = new MenuItemViewModel[]
                {
                    CreateViewMenu()
                }
            };

            return viewModel;
        }

        private MenuItemViewModel CreateViewMenu()
        {
            var viewModel = new MenuItemViewModel
            {
                Text = Titles.View,
                MenuItems = new MenuItemViewModel[]
                {
                    new MenuItemViewModel
                    { 
                        Text = PropertyDisplayNames.Documents
                    }
                }
            };

            return viewModel;
        }
    }
}