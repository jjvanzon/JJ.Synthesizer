using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToPatchDetailsViewModelExtensions
    {
        public static PatchDetailsViewModel ToDetailsViewModel(this Patch patch, IOperatorTypeRepository operatorTypeRepository, EntityPositionManager entityPositionManager)
        {
            if (patch == null) throw new NullException(() => patch);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new PatchDetailsViewModel
            {
                Entity = patch.ToViewModelRecursive(entityPositionManager),
                ValidationMessages = new List<Message>()
            };

            viewModel.OperatorToolboxItems = ViewModelHelper.CreateOperatorTypesViewModel(operatorTypeRepository);

            foreach (OperatorViewModel operatorViewModel in viewModel.Entity.Operators)
            {
                SetViewModelPosition(operatorViewModel, entityPositionManager);
            }

            return viewModel;
        }

        private static void SetViewModelPosition(OperatorViewModel operatorViewModel, EntityPositionManager entityPositionManager)
        {
            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.EntityPositionID = entityPosition.ID;
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }

        private static PatchViewModel ToViewModelRecursive(this Patch patch, EntityPositionManager entityPositionManager)
        {
            PatchViewModel viewModel = patch.ToViewModel();

            var dictionary = new Dictionary<Operator, OperatorViewModel>();

            viewModel.Operators = patch.Operators.ToViewModelsRecursive(entityPositionManager, dictionary);

            return viewModel;
        }

        private static IList<OperatorViewModel> ToViewModelsRecursive(this IList<Operator> operators, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            var list = new List<OperatorViewModel>(operators.Count);

            foreach (Operator op in operators)
            {
                OperatorViewModel operatorViewModel = op.ToViewModelRecursive(entityPositionManager, dictionary);

                list.Add(operatorViewModel);
            }

            return list;
        }

        private static OperatorViewModel ToViewModelRecursive(this Operator op, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            OperatorViewModel viewModel;
            if (dictionary.TryGetValue(op, out viewModel))
            {
                return viewModel;
            }

            viewModel = op.ToViewModel(entityPositionManager);

            dictionary.Add(op, viewModel);

            // TODO: When PatchInlets do not have inlets and PatchOutlets do not have PatchOutlets (in the future) these if's are probably not necessary anymore.

            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                viewModel.Inlets = op.Inlets.ToViewModelsRecursive(entityPositionManager, dictionary);
            }
            else
            {
                viewModel.Inlets = new List<InletViewModel>();
            }

            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
            {
                viewModel.Outlets = op.Outlets.ToViewModelsRecursive(entityPositionManager, dictionary);
            }
            else
            {
                viewModel.Outlets = new List<OutletViewModel>();
            }

            return viewModel;
        }

        /// <summary>
        /// Includes its inlets and outlets.
        /// Also includes the inverse property OutletViewModel.Operator.
        /// That view model is one the few with an inverse property.
        /// </summary>
        public static OperatorViewModel ToViewModelWithRelatedEntitiesAndInverseProperties(this Operator op, EntityPositionManager entityPositionManager)
        {
            OperatorViewModel operatorViewModel = op.ToViewModel(entityPositionManager);
            operatorViewModel.Inlets = op.Inlets.ToViewModels();
            operatorViewModel.Outlets = op.Outlets.ToViewModels();

            // This is the inverse property in the view model!
            foreach (OutletViewModel outletViewModel in operatorViewModel.Outlets)
            {
                outletViewModel.Operator = operatorViewModel;
            }

            return operatorViewModel;
        }

        private static IList<InletViewModel> ToViewModelsRecursive(this IList<Inlet> entities, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            var viewModels = new List<InletViewModel>(entities.Count);

            // TODO: Introduce SortOrder property and then sort by it.

            for (int i = 0; i < entities.Count; i++)
            {
                Inlet entity = entities[i];

                InletViewModel viewModel = entity.ToViewModelRecursive(entityPositionManager, dictionary);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private static InletViewModel ToViewModelRecursive(this Inlet inlet, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            InletViewModel viewModel = inlet.ToViewModel();

            if (inlet.InputOutlet != null)
            {
                viewModel.InputOutlet = inlet.InputOutlet.ToViewModelRecursive(entityPositionManager, dictionary);
            }

            return viewModel;
        }

        private static IList<OutletViewModel> ToViewModelsRecursive(this IList<Outlet> entities, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            var viewModels = new List<OutletViewModel>(entities.Count);

            // TODO: Introduce SortOrder property and then sort by it.

            for (int i = 0; i < entities.Count; i++)
            {
                Outlet entity = entities[i];
                OutletViewModel viewModel = entity.ToViewModelRecursive(entityPositionManager, dictionary);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private static OutletViewModel ToViewModelRecursive(this Outlet outlet, EntityPositionManager entityPositionManager, Dictionary<Operator, OperatorViewModel> dictionary)
        {
            OutletViewModel viewModel = outlet.ToViewModel();

            entityPositionManager.GetOrCreateOperatorPosition(outlet.Operator);

            // Recursive call
            viewModel.Operator = outlet.Operator.ToViewModelRecursive(entityPositionManager, dictionary);

            return viewModel;
        }
    }
}
