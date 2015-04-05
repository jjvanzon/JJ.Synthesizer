using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Presentation.Synthesizer.ToEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Converters
{
    internal class RecursiveViewModelToEntityConverter
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IInletRepository _inletRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IEntityPositionRepository _entityPositionRepository;

        private readonly Dictionary<int, Operator> _operatorDictionary = new Dictionary<int, Operator>();
        private readonly Dictionary<int, Outlet> _outletDictionary = new Dictionary<int, Outlet>();

        public RecursiveViewModelToEntityConverter(
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
        }

        public Operator Convert(OperatorViewModel operatorViewModel)
        {
            if (operatorViewModel == null) throw new NullException(() => operatorViewModel);

            Operator op = ToEntityRecursive(operatorViewModel);

            return op;
        }

        private Operator ToEntityRecursive(OperatorViewModel viewModel)
        {
            Operator op;
            if (_operatorDictionary.TryGetValue(viewModel.ID, out op))
            {
                return op;
            }

            op = viewModel.ToEntity(_operatorRepository);

            _operatorDictionary.Add(op.ID, op);

            EntityPosition entityPosition = viewModel.ToEntityPosition(_entityPositionRepository);

            foreach (InletViewModel inletViewModel in viewModel.Inlets)
            {
                Inlet inlet = ToEntityRecursive(inletViewModel);
                inlet.LinkTo(op);
            }

            foreach (OutletViewModel outletViewModel in viewModel.Outlets)
            {
                Outlet outlet = ToEntityRecursive(outletViewModel);
                outlet.LinkTo(op);
            }

            return op;
        }

        private Inlet ToEntityRecursive(InletViewModel inletViewModel)
        {
            Inlet inlet = inletViewModel.ToEntity(_inletRepository);

            if (inletViewModel.InputOutlet == null)
            {
                inlet.UnlinkOutlet();
            }
            else
            {
                Outlet inputOutlet = ToEntityRecursive(inletViewModel.InputOutlet);
                inlet.LinkTo(inputOutlet);
            }

            return inlet;
        }

        private Outlet ToEntityRecursive(OutletViewModel outletViewModel)
        {
            Outlet outlet;
            if (_outletDictionary.TryGetValue(outletViewModel.ID, out outlet))
            {
                return outlet;
            }

            outlet = outletViewModel.ToEntity(_outletRepository);

            _outletDictionary.Add(outlet.ID, outlet);

            Operator op = ToEntityRecursive(outletViewModel.Operator);
            outlet.LinkTo(op);

            return outlet;
        }
    }
}
