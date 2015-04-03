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
        public static PatchEditViewModel ToPatchEditViewModel(this Operator rootOperator)
        {
            if (rootOperator == null) throw new NullException(() => rootOperator);

            return ToPatchEditViewModel(new Operator[] { rootOperator });
        }

        public static PatchEditViewModel ToPatchEditViewModel(this IList<Operator> rootOperators)
        {
            if (rootOperators == null) throw new NullException(() => rootOperators);

            IDictionary<Operator, OperatorViewModel> alreadyProcessed = new Dictionary<Operator, OperatorViewModel>();

            var viewModel = new PatchEditViewModel
            {
                RootOperators = rootOperators.Select(x => x.ToViewModelRecursive(alreadyProcessed)).ToArray()
            };

            return viewModel;
        }

        private static OperatorViewModel ToViewModelRecursive(this Operator op, IDictionary<Operator, OperatorViewModel> alreadyProcessed)
        {
            OperatorViewModel operatorViewModel;
            if (alreadyProcessed .TryGetValue(op, out operatorViewModel))
            {
                return operatorViewModel;
            }
            var viewModel = new OperatorViewModel();
            alreadyProcessed.Add(op, viewModel);

            viewModel.ID = op.ID;
            viewModel.Name = op.Name;
            viewModel.Inlets = op.Inlets.Select(x => x.ToViewModelRecursive(alreadyProcessed)).ToArray();
            viewModel.Outlets = op.Outlets.Select(x => x.ToViewModelRecursive(alreadyProcessed)).ToArray();

            return viewModel;
        }

        private static InletViewModel ToViewModelRecursive(this Inlet inlet, IDictionary<Operator, OperatorViewModel> alreadyProcessed)
        {
            var viewModel = new InletViewModel
            {
                ID = inlet.ID,
                Name = inlet.Name
            };

            if (inlet.InputOutlet != null)
            {
                viewModel.InputOutlet = inlet.InputOutlet.ToViewModelRecursive(alreadyProcessed);
            }

            return viewModel;
        }

        private static OutletViewModel ToViewModelRecursive(this Outlet outlet, IDictionary<Operator, OperatorViewModel> alreadyProcessed)
        {
            var viewModel = new OutletViewModel
            {
                ID = outlet.ID,
                Name = outlet.Name,
            };

            // Recursive call
            viewModel.Operator = outlet.Operator.ToViewModelRecursive(alreadyProcessed);

            return viewModel;
        }
    }
}
