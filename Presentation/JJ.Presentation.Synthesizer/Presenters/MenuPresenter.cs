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
                ViewMenu = CreateViewMenu()
            };

            return viewModel;
        }

        private ViewMenuViewModel CreateViewMenu()
        {
            var viewModel = new ViewMenuViewModel
            {
                DocumentMenuItem = CreateDocumentMenuItem()
            };

            return viewModel;
        }

        private MenuItemViewModel CreateDocumentMenuItem()
        {
            var viewModel = new MenuItemViewModel();
            return viewModel;
        }
    }
}