using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Presentation.Synthesizer.ToEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    /// <summary>
    /// Handles the recursive conversion of viewmodels of operators and their inlets and outlets
    /// to entities. It delegates to the 'singular' forms of those conversions: the extension methods
    /// that do not convert anything other than the entity itself without any related entities.
    /// </summary>
    internal class RecursiveViewModelToEntityConverter
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IOperatorTypeRepository _operatorTypeRepository;
        private readonly IInletRepository _inletRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IEntityPositionRepository _entityPositionRepository;

        private readonly Dictionary<int, Operator> _operatorDictionary = new Dictionary<int, Operator>();
        private readonly Dictionary<int, Outlet> _outletDictionary = new Dictionary<int, Outlet>();

        public RecursiveViewModelToEntityConverter(
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _operatorRepository = operatorRepository;
            _operatorTypeRepository = operatorTypeRepository;
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

            op = viewModel.ToEntity(_operatorRepository, _operatorTypeRepository);

            _operatorDictionary.Add(op.ID, op);

            viewModel.ToEntityPosition(_entityPositionRepository);

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

            // I have to do a lot of hacking here, to make sure I save the Operator first,
            // and then the Outlet, because otherwise NHibernate will crash.

            // First convert operator, because NHibernate cannot handle 
            // saving THE child object first and then the parent object,
            // It would try to execute an insert statement on the child object without its 'ParentID' being filled in.
            Operator op = ToEntityRecursive(outletViewModel.Operator);

            // I have a chicken and egg problem. I convert the operator, in there the operator
            // is first converted without related entities, then added to the dictionary,
            // then it starts converting outlets, which delegate to convert operator,
            // which returns the operator without related entities and then I try to the outlet it here.
            // The if-else structure here is like a chicken-or-egg detection, if you will.

            if (op.Outlets.Count != 0)
            {
                // Operator.ToEntityWithRelatedEntities has already converted toe outlet.
                // Do not call ToEntity here, or you would get a second copy of the outlet, if it is new.
                outlet = op.Outlets.Where(x => x.ID == outletViewModel.ID).Single();
            }
            else
            {
                // Operator.ToEntityWithRelatedEntities has not yet converted the outlet.
                outlet = outletViewModel.ToEntity(_outletRepository);
                outlet.LinkTo(op);
                _outletDictionary.Add(outlet.ID, outlet);
            }

            return outlet;
        }
    }
}
