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
    /// <summary>
    /// Empty view models start out with Visible = false.
    /// </summary>
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
                DocumentTreeMenuItem = new MenuItemViewModel(),
                InstrumentsMenuItem = new MenuItemViewModel(),
                AudioFileOutputsMenuItem = new MenuItemViewModel(),
                CurvesMenuItem = new MenuItemViewModel(),
                PatchesMenuItem = new MenuItemViewModel(),
                SamplesMenuItem = new MenuItemViewModel(),
                AudioFileOutputDetailsMenuItem = new MenuItemViewModel(),
                PatchDetailsMenuItem = new MenuItemViewModel()
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel
            {
                Visible = true
            };

            return viewModel;
        }
    }
}
