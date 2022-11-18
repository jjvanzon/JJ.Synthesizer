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
                Patch = patch.ToViewModel()
            };

            // Convert operators and build up a dictionary
            IDictionary<Operator, OperatorViewModel> dictionary = new Dictionary<Operator, OperatorViewModel>();
            viewModel.Patch.Operators = new List<OperatorViewModel>(patch.Operators.Count);
            foreach (Operator op in patch.Operators)
            {
                OperatorViewModel operatorViewModel = op.ToViewModel();
                viewModel.Patch.Operators.Add(operatorViewModel);

                dictionary.Add(op, operatorViewModel);
            }

            // Convert references from one operator to another.
            for (int i = 0; i < patch.Operators.Count; i++)
            {
                Operator op = patch.Operators[i];
                OperatorViewModel operatorViewModel = viewModel.Patch.Operators[i];

                for (int j = 0; j < op.Inlets.Count; j++)
                {
                    Inlet inlet = op.Inlets[j];
                    InletViewModel inletViewModel = operatorViewModel.Inlets[j];
                
                    if (inlet.InputOutlet != null)
                    {
                        Operator op2 = inlet.InputOutlet.Operator;
                        OperatorViewModel operatorViewModel2 = dictionary[op2];

                        OutletViewModel outletViewModel = operatorViewModel2.Outlets.Where(x => x.ID == inlet.InputOutlet.ID).Single();

                        inletViewModel.InputOutlet = outletViewModel;
                    }
                }
            }

            return viewModel;
        }

        private static PatchViewModel ToViewModel(this Patch patch)
        {
            var viewModel = new PatchViewModel 
            { 
                PatchName = patch.Name 
            };

            return viewModel;
        }

        private static OperatorViewModel ToViewModel(this Operator op)
        {
            var viewModel = new OperatorViewModel
            {
                ID = op.ID,
                Name = op.Name,
                Inlets = op.Inlets.Select(x => x.ToViewModel()).ToArray(),
                Outlets = op.Outlets.Select(x => x.ToViewModel()).ToArray()
            };

            // TODO: This seems like hacking. Evaluate all the ToViewModel code.
            foreach (var x in viewModel.Outlets)
            {
                x.Operator = viewModel;
            }

            return viewModel;
        }

        private static InletViewModel ToViewModel(this Inlet inlet)
        {
            var viewModel = new InletViewModel
            {
                ID = inlet.ID,
                Name = inlet.Name
            };

            return viewModel;
        }

        private static OutletViewModel ToViewModel(this Outlet outlet)
        {
            var viewModel = new OutletViewModel
            {
                ID = outlet.ID,
                Name = outlet.Name
            };

            return viewModel;
        }

        [Obsolete("", true)]
        public static PatchEditViewModel ToPatchEditViewModel(this Operator rootOperator)
        {
            if (rootOperator == null) throw new NullException(() => rootOperator);

            //return ToPatchEditViewModel(new Operator[] { rootOperator });
            throw new NotImplementedException();
        }
        
        [Obsolete("", true)]
        public static PatchEditViewModel ToPatchEditViewModel(this IList<Operator> rootOperators)
        {
            if (rootOperators == null) throw new NullException(() => rootOperators);

            IDictionary<Operator, OperatorViewModel> alreadyProcessed = new Dictionary<Operator, OperatorViewModel>();

            var viewModel = new PatchEditViewModel
            {
                //RootOperators = rootOperators.Select(x => x.ToViewModelRecursive(alreadyProcessed)).ToArray()
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
