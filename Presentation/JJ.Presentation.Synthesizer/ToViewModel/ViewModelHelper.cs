using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
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

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void UpdateViewModel_WithInletsAndOutlets_WithoutEntityPosition(Operator entity, OperatorViewModel operatorViewModel)
        {
            UpdateViewModel_WithoutEntityPosition(entity, operatorViewModel);

            // TODO: Split up into multiple methods.
            var inletViewModelsToKeep = new List<InletViewModel>(entity.Inlets.Count);
            foreach (Inlet inlet in entity.Inlets)
            {
                InletViewModel inletViewModel = operatorViewModel.Inlets.Where(x => x.ID == inlet.ID).FirstOrDefault();
                if (inletViewModel == null)
                {
                    inletViewModel = inlet.ToViewModel();
                    operatorViewModel.Inlets.Add(inletViewModel);
                }

                inletViewModel.Name = inlet.Name;
                inletViewModel.SortOrder = inlet.SortOrder;

                inletViewModelsToKeep.Add(inletViewModel);
            }

            IList<InletViewModel> existingInletViewModels = operatorViewModel.Inlets;
            IList<InletViewModel> inletViewModelsToDelete = existingInletViewModels.Except(inletViewModelsToKeep).ToArray();
            foreach (InletViewModel inletViewModelToDelete in inletViewModelsToDelete)
            {
                inletViewModelToDelete.InputOutlet = null;
                operatorViewModel.Inlets.Remove(inletViewModelToDelete);
            }

            operatorViewModel.Inlets = operatorViewModel.Inlets.OrderBy(x => x.SortOrder).ToList();

            // TODO: Split up into multiple methods.
            var outletViewModelsToKeep = new List<OutletViewModel>(entity.Outlets.Count);
            foreach (Outlet outlet in entity.Outlets)
            {
                OutletViewModel outletViewModel = operatorViewModel.Outlets.Where(x => x.ID == outlet.ID).FirstOrDefault();
                if (outletViewModel == null)
                {
                    outletViewModel = outlet.ToViewModel();
                    operatorViewModel.Outlets.Add(outletViewModel);

                    // The only inverse property in all the view models.
                    outletViewModel.Operator = operatorViewModel;
                }

                outletViewModel.Name = outlet.Name;
                outletViewModel.SortOrder = outlet.SortOrder;

                outletViewModelsToKeep.Add(outletViewModel);
            }

            IList<OutletViewModel> existingOutletViewModels = operatorViewModel.Outlets;
            IList<OutletViewModel> outletViewModelsToDelete = existingOutletViewModels.Except(outletViewModelsToKeep).ToArray();
            foreach (OutletViewModel outletViewModelToDelete in outletViewModelsToDelete)
            {
                // The only inverse property in all the view models.
                outletViewModelToDelete.Operator = null;

                operatorViewModel.Outlets.Remove(outletViewModelToDelete);
            }

            operatorViewModel.Outlets = operatorViewModel.Outlets.OrderBy(x => x.SortOrder).ToList();
        }

        /// <summary>
        /// Is used to be able to update an existing operator view model in-place
        /// without having to re-establish the intricate relations with other operators.
        /// </summary>
        public static void UpdateViewModel_WithoutEntityPosition(Operator entity, OperatorViewModel viewModel)
        {
            if (entity == null) throw new NullException(() => entity);
            if (viewModel == null) throw new NullException(() => viewModel);

            viewModel.Name = entity.Name;
            viewModel.ID = entity.ID;
            viewModel.Caption = GetOperatorCaption(entity);

            if (entity.OperatorType != null)
            {
                viewModel.OperatorTypeID = entity.OperatorType.ID;
            }
            else
            {
                viewModel.OperatorTypeID = 0; // Should never happen.
            }
        }

        private static string GetOperatorCaption(Operator entity)
        {
            if (entity.GetOperatorTypeEnum() == OperatorTypeEnum.Value)
            {
                var wrapper = new Value_OperatorWrapper(entity);
                return wrapper.Value.ToString("0.####");
            }

            if (!String.IsNullOrWhiteSpace(entity.Name))
            {
                return entity.Name;
            }

            string caption = ResourceHelper.GetOperatorTypeDisplayName(entity.GetOperatorTypeEnum());
            return caption;
        }
    }
}