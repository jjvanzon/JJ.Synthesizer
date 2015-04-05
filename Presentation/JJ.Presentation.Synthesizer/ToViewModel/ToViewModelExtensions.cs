using JJ.Business.Synthesizer.Managers;
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
        public static PatchEditViewModel ToPatchEditViewModel(this Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var viewModel = new PatchEditViewModel
            {
                Patch = patch.ToViewModelRecursive()
            };

            return viewModel;
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

            viewModel.Inlets = op.Inlets.Select(x => x.ToViewModelRecursive(dictionary)).ToArray();
            viewModel.Outlets = op.Outlets.Select(x => x.ToViewModelRecursive(dictionary)).ToArray();

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

            var viewModel = new OperatorViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
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
                Name = entity.Name
            };

            return viewModel;
        }
    }
}
