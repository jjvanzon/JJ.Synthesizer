using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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
        public static NotFoundViewModel CreateNotFoundViewModel(string entityTypeDisplayName)
        {
            var viewModel = new NotFoundViewModel
            {
                Message = CommonMessageFormatter.ObjectNotFound(entityTypeDisplayName)
            };

            return viewModel;
        }

        public static NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            return CreateNotFoundViewModel<Document>();
        }

        public static NotFoundViewModel CreateNotFoundViewModel<TEntity>()
        {
            string entityTypeName = typeof(TEntity).Name;
            string entityTypeDisplayName = ResourceHelper.GetPropertyDisplayName(entityTypeName);

            NotFoundViewModel viewModel = CreateNotFoundViewModel(entityTypeDisplayName);
            return viewModel;
        }

        public static MenuViewModel CreateMenuViewModel(bool documentIsOpen)
        {
            var viewModel = new MenuViewModel
            {
                DocumentsMenuItem = new MenuItemViewModel { Visible = true },
                DocumentTreeMenuItem = new MenuItemViewModel { Visible = documentIsOpen },
                DocumentCloseMenuItem = new MenuItemViewModel { Visible = documentIsOpen },
                DocumentSaveMenuItem = new MenuItemViewModel { Visible = documentIsOpen }
            };

            return viewModel;
        }

        public static DocumentDeletedViewModel CreateDocumentDeletedViewModel()
        {
            var viewModel = new DocumentDeletedViewModel();

            return viewModel;
        }
    }
}
