using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    public static class ToPatchDetailsViewModelExtensions
    {
        public static PatchDetailsViewModel ToDetailsViewModel(this Patch patch, EntityPositionManager entityPositionManager)
        {
            if (patch == null) throw new NullException(() => patch);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new PatchDetailsViewModel
            {
                Patch = patch.ToViewModelRecursive(),
                ValidationMessages = new List<ValidationMessage>()
            };

            viewModel.OperatorTypeToolboxItems = ViewModelHelper.CreateOperatorTypesViewModel();

            foreach (OperatorViewModel operatorViewModel in viewModel.Patch.Operators)
            {
                SetViewModelPosition(operatorViewModel, entityPositionManager);
            }

            return viewModel;
        }

        private static void SetViewModelPosition(OperatorViewModel operatorViewModel, EntityPositionManager entityPositionManager)
        {
            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }

        private static PatchViewModel ToViewModelRecursive(this Patch patch)
        {
            PatchViewModel viewModel = patch.ToViewModel();

            var dictionary = new Dictionary<Operator, OperatorViewModel>();

            viewModel.Operators = new List<OperatorViewModel>(patch.Operators.Count);

            foreach (Operator op in patch.Operators)
            {
                OperatorViewModel operatorViewModel = op.ToViewModelRecursive(dictionary);
                viewModel.Operators.Add(operatorViewModel);
            }

            return viewModel;
        }

        private static OperatorViewModel ToViewModelRecursive(this Operator op, IDictionary<Operator, OperatorViewModel> dictionary)
        {
            OperatorViewModel viewModel;
            if (dictionary.TryGetValue(op, out viewModel))
            {
                return viewModel;
            }

            viewModel = op.ToViewModel();

            dictionary.Add(op, viewModel);

            if (!String.Equals(op.OperatorTypeName, PropertyNames.PatchInlet))
            {
                viewModel.Inlets = op.Inlets.Select(x => x.ToViewModelRecursive(dictionary)).ToArray();
            }
            else
            {
                viewModel.Inlets = new List<InletViewModel>();
            }

            if (!String.Equals(op.OperatorTypeName, PropertyNames.PatchOutlet))
            {
                viewModel.Outlets = op.Outlets.Select(x => x.ToViewModelRecursive(dictionary)).ToArray();
            }
            else
            {
                viewModel.Outlets = new List<OutletViewModel>();
            }

            return viewModel;
        }

        /// <summary>
        /// Includes its inlets and outlets.
        /// </summary>
        public static OperatorViewModel ToViewModelWithRelatedEntities(this Operator op)
        {
            // Do not reuse this in ToViewModelRecursive, because there you have to do a dictionary.Add there right in the middle of things.
            OperatorViewModel viewModel = op.ToViewModel();

            viewModel.Inlets = op.Inlets.Select(x => x.ToViewModel()).ToArray();
            viewModel.Outlets = op.Outlets.Select(x => x.ToViewModel()).ToArray();

            return viewModel;
        }

        private static InletViewModel ToViewModelRecursive(this Inlet inlet, IDictionary<Operator, OperatorViewModel> dictionary)
        {
            InletViewModel viewModel = inlet.ToViewModel();

            if (inlet.InputOutlet != null)
            {
                viewModel.InputOutlet = inlet.InputOutlet.ToViewModelRecursive(dictionary);
            }

            return viewModel;
        }

        private static OutletViewModel ToViewModelRecursive(this Outlet outlet, IDictionary<Operator, OperatorViewModel> dictionary)
        {
            OutletViewModel viewModel = outlet.ToViewModel();

            // Recursive call
            viewModel.Operator = outlet.Operator.ToViewModelRecursive(dictionary);

            return viewModel;
        }
    }
}
