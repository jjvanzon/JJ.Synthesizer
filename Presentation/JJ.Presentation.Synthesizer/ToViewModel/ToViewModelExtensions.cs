using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToViewModelExtensions
    {
        public static PatchEditViewModel ToEditViewModel(this Patch patch, EntityPositionManager entityPositionManager)
        {
            if (patch == null) throw new NullException(() => patch);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new PatchEditViewModel
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

        // Singular Form

        public static PatchViewModel ToViewModel(this Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new PatchViewModel
            {
                ID = entity.ID,
                PatchName = entity.Name
            };

            return viewModel;
        }

        public static OperatorViewModel ToViewModel(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            string name;
            if (String.Equals(entity.OperatorTypeName, PropertyNames.ValueOperator))
            {
                name = entity.AsValueOperator.Value.ToString("0.####");
            }
            else
            {
                name = entity.Name;
            }

            var viewModel = new OperatorViewModel
            {
                ID = entity.ID,
                TemporaryID = Guid.NewGuid(),
                Name = name,
                OperatorTypeName = entity.OperatorTypeName
            };

            return viewModel;
        }

        public static InletViewModel ToViewModel(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new InletViewModel
            {
                ID = entity.ID,
                Name = entity.Name
            };

            return viewModel;
        }

        public static OutletViewModel ToViewModel(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var viewModel = new OutletViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                TemporaryID = Guid.NewGuid()
            };

            return viewModel;
        }
    }
}
