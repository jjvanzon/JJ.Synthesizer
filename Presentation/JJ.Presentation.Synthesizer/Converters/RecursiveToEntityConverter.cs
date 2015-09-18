using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.LinkTo;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Presentation.Synthesizer.Converters
{
    /// <summary>
    /// Handles the recursive conversion of viewmodels of operators and their inlets and outlets
    /// to entities. It delegates to the 'singular' forms of those conversions: the extension methods
    /// that do not convert anything other than the entity itself without any related entities.
    /// </summary>
    internal class RecursiveToEntityConverter
    {
        private readonly PatchRepositories _repositories;
        private readonly Dictionary<int, Operator> _operatorDictionary = new Dictionary<int, Operator>();
        private readonly Dictionary<int, Outlet> _outletDictionary = new Dictionary<int, Outlet>();
        private readonly Patch _patch;

        public RecursiveToEntityConverter(PatchRepositories patchRepositories)
        {
            if (patchRepositories == null) throw new NullException(() => patchRepositories);

            _repositories = patchRepositories;
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

            op = viewModel.ToEntity(_repositories.OperatorRepository, _repositories.OperatorTypeRepository);

            _operatorDictionary.Add(op.ID, op);

            viewModel.ToEntityPosition(_repositories.EntityPositionRepository);

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

            Inlet patchInletInlet = ToEntityHelper.HACK_CreatePatchInletInletIfNeeded(
                destOperator, _repositories.InletRepository, _repositories.IDRepository);
            if (patchInletInlet != null)
            {
                idsToKeep.Add(patchInletInlet.ID);
            }

            var patchManager = new PatchManager(_repositories);

            int[] existingIDs = destOperator.Inlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                patchManager.DeleteInlet(idToDelete);
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

            Outlet patchOutletOutlet = ToEntityHelper.HACK_CreatePatchOutletOutletIfNeeded(
                destOperator, _repositories.OutletRepository, _repositories.IDRepository);
            if (patchOutletOutlet != null)
            {
                idsToKeep.Add(patchOutletOutlet.ID);
            }

            var patchManager = new PatchManager(_repositories);

            int[] existingIDs = destOperator.Outlets.Select(x => x.ID).ToArray();
            int[] idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                patchManager.DeleteOutlet(idToDelete);
            }
        }

        private Inlet ToEntityRecursive(InletViewModel inletViewModel)
        {
            Inlet inlet = inletViewModel.ToEntity(_repositories.InletRepository);

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
            // then it starts converting outlets, and this method,
            // which delegate in turn to convert operator,
            // which returns the operator without related entities and then I try to convert the outlet it here.
            outlet = outletViewModel.ToEntity(_repositories.OutletRepository);
            outlet.LinkTo(op);
            // The 'if' here is like a chicken-or-egg detection, if you will.
            if (!_outletDictionary.ContainsKey(outletViewModel.ID))
            {
                _outletDictionary.Add(outlet.ID, outlet);
            }

            return outlet;
        }
    }
}
