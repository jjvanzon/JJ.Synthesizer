using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        public static Patch ToEntity(
            this PatchDetailsViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            Patch patch = viewModel.Patch.ToEntityWithRelatedEntities(patchRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository);

            return patch;
        }

        private static Patch ToEntityWithRelatedEntities(
            this PatchViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Patch patch = viewModel.ToEntity(patchRepository);

            RecursiveViewModelToEntityConverter converter = new RecursiveViewModelToEntityConverter(operatorRepository, inletRepository, outletRepository, entityPositionRepository);

            var convertedOperators = new HashSet<Operator>();

            foreach (OperatorViewModel operatorViewModel in viewModel.Operators)
            {
                Operator op = converter.Convert(operatorViewModel);
                op.LinkTo(patch);

                convertedOperators.Add(op);
            }

            IList<Operator> operatorsToDelete = patch.Operators.Except(convertedOperators).ToArray();
            foreach (Operator op in operatorsToDelete)
            {
                op.UnlinkRelatedEntities();
                op.DeleteRelatedEntities(inletRepository, outletRepository, entityPositionRepository);
                operatorRepository.Delete(op);
            }

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

        public static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Operator entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            entity.OperatorTypeName = viewModel.OperatorTypeName;
            return entity;
        }

        public static Inlet ToEntity(this InletViewModel viewModel, IInletRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Inlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Outlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        public static EntityPosition ToEntityPosition(this OperatorViewModel viewModel, IEntityPositionRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            var manager = new EntityPositionManager(repository);
            EntityPosition entityPosition = manager.SetOrCreateOperatorPosition(viewModel.ID, viewModel.CenterX, viewModel.CenterY);
            return entityPosition;
        }
    }
}