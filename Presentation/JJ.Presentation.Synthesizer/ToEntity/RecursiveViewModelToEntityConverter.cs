using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;

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

            ConvertToInletsRecursive(viewModel.Inlets, op);
            ConvertToOutletsRecursive(viewModel.Outlets, op);

            return op;
        }

        private void ConvertToInletsRecursive(IList<InletViewModel> sourceInletViewModels, Operator destOperator)
        {
            var idsToKeep = new List<int>(sourceInletViewModels.Count + 1);

            foreach (InletViewModel inletViewModel in sourceInletViewModels)
            {
                Inlet inlet = ToEntityRecursive(inletViewModel);
                inlet.LinkTo(destOperator);
                idsToKeep.Add(inlet.ID);
            }

            // Hack back in a PatchInlet's Inlet, that was excluded from the view model.
            if (destOperator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
            {
                // It is quite a hack, because it relies on the dest operator's already having this inlet.
                Inlet inlet = destOperator.Inlets.Where(x => String.Equals(x.Name, PropertyNames.Input)).Single();

                idsToKeep.Add(inlet.ID);
            }

            int[] existingIDs = destOperator.Inlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                Inlet entityToDelete = _inletRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                _inletRepository.Delete(entityToDelete);
            }
        }

        private void ConvertToOutletsRecursive(IList<OutletViewModel> sourceOutletViewModels, Operator destOperator)
        {
            var idsToKeep = new List<int>(sourceOutletViewModels.Count + 1);

            foreach (OutletViewModel outletViewModel in sourceOutletViewModels)
            {
                Outlet outlet = ToEntityRecursive(outletViewModel);
                outlet.LinkTo(destOperator);
                idsToKeep.Add(outlet.ID);
            }

            // Hack back in a PatchOutlet's Outlet, that was excluded from the view model.
            if (destOperator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
            {
                // It is quite a hack, because it relies on the dest operator's already having this outlet.
                Outlet outlet = destOperator.Outlets.Where(x => String.Equals(x.Name, PropertyNames.Result)).Single();

                idsToKeep.Add(outlet.ID);
            }

            int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int outletIDToDelete in idsToDelete)
            {
                Outlet entityToDelete = _outletRepository.Get(outletIDToDelete);
                entityToDelete.UnlinkRelatedEntities();
                _outletRepository.Delete(entityToDelete);
            }
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
            // saving the child object first and then the parent object,
            // It would try to execute an insert statement on the child object without its 'ParentID' being filled in.
            if (outletViewModel.Operator == null)
            {
                throw new NullException(() => outletViewModel.Operator);
            }
            Operator op = ToEntityRecursive(outletViewModel.Operator);

            // I have a chicken and egg problem. I converted the operator, in there the operator
            // is first converted without related entities, then added to the dictionary,
            // then it starts converting outlets, which delegate in turn to convert operator,
            // which returns the operator without related entities and then I try to the outlet it here.
            // The if-else structure here is like a chicken-or-egg detection, if you will.

            outlet = op.Outlets.Where(x => x.ID == outletViewModel.ID).SingleOrDefault();

            // If outlet found: Operator.ToEntityWithRelatedEntities has already converted toe outlet.
            // Do not call ToEntity here, or you would get a second copy of the outlet, if it is new.

            if (outlet == null)
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
