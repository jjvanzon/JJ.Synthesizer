using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static NotFoundViewModel CreateNotFoundViewModel(string entityTypeName)
        {
            var viewModel = new NotFoundViewModel
            {
                Visible = true,
                Message = CommonMessageFormatter.ObjectNotFound(entityTypeName)
            };

            return viewModel;
        }

        public static MenuViewModel CreateMenuViewModel()
        {
            var viewModel = new MenuViewModel
            {
                ViewMenu = CreateViewMenu()
            };

            return viewModel;
        }

        private static ViewMenuViewModel CreateViewMenu()
        {
            var viewModel = new ViewMenuViewModel
            {
                DocumentsMenuItem = new MenuItemViewModel(),
                DocumentTreeMenuItem = new MenuItemViewModel()
            };

            return viewModel;
        }

        public static DocumentDeleteConfirmedViewModel CreateDocumentDeleteConfirmedViewModel()
        {
            var viewModel = new DocumentDeleteConfirmedViewModel
            {
                Visible = true
            };

            return viewModel;
        }
    }
}
