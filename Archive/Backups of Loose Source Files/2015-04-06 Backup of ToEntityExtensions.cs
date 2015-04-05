using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Converters;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        public static Patch ToEntity(
            this PatchEditViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            Patch patch = viewModel.Patch.ToEntityWithRelatedEntities(patchRepository, operatorRepository, entityPositionRepository);

            return patch;
        }

        private static Patch ToEntity(this PatchViewModel viewModel, IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch entity = patchRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = patchRepository.Create();
            }
            entity.Name = viewModel.PatchName;

            return entity;
        }

        private static Patch ToEntityWithRelatedEntities(
            this PatchViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Patch patch = viewModel.ToEntity(patchRepository);

            //var converter = new RecursiveViewModelToEntityConverter(operatorRepository, inletRepository, outletRepository, entityPositionRepository);


            var dictionary = new Dictionary<int, Operator>();

            foreach (OperatorViewModel operatorViewModel in viewModel.Operators)
            {
                Operator op = operatorViewModel.ToEntityRecursive(operatorRepository, entityPositionRepository, dictionary);
                op.LinkTo(patch);
            }

            return patch;
        }

        private static Operator ToEntityRecursive(
            this OperatorViewModel viewModel,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository,
            IDictionary<int, Operator> dictionary)
        {
            Operator op;
            if (dictionary.TryGetValue(viewModel.ID, out op))
            {
                return op;
            }

            op = viewModel.ToEntity(operatorRepository);

            // TODO: EntityPositions.

            foreach (InletViewModel inletViewModel in viewModel.Inlets)
            {
                Inlet inlet = inletViewModel.ToEntityRecursive(inletRepository);
                inlet.LinkTo(op);
            }

            foreach (OutletViewModel outletViewModel in viewModel.Outlets)
            {
                Outlet outlet = outletViewModel.ToEntityRecursive(outletRepository);
                outlet.LinkTo(op);
            }

            return op;
        }

        private static Inlet ToEntityRecursive(
            this InletViewModel viewModel,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository,
            IDictionary<int, Operator> dictionary)
        {
        }

        private static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository repository)
        {
            Operator entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        private static Inlet ToEntityRecursive(
            this InletViewModel viewModel,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Inlet inlet = viewModel.ToEntity(inletRepository);

            // TODO: Related entities.
            throw new NotImplementedException();
        }

        private static Inlet ToEntity(this InletViewModel viewModel, IInletRepository repository)
        {
            Inlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        private static Inlet ToEntityRecursive(
            this InletViewModel viewModel,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Inlet inlet = viewModel.ToEntity(inletRepository);

            // TODO: Related entities.
            throw new NotImplementedException();
        }

        private static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository repository)
        {
            Outlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }
   }
}