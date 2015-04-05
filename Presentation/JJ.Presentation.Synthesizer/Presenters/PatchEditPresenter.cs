using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class PatchEditPresenter
    {
        private IPatchRepository _patchRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IEntityPositionRepository _entityPositionRepository;

        private EntityPositionManager _entityPositionManager;

        public PatchEditPresenter(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository, 
            IEntityPositionRepository entityPositionRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _operatorRepository = operatorRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;

            _entityPositionManager = new EntityPositionManager(_entityPositionRepository);
        }

        public PatchEditViewModel Create()
        {
            Patch entity = _patchRepository.Create();

            PatchEditViewModel viewModel = entity.ToPatchEditViewModel();

            return viewModel;
        }

        public PatchEditViewModel Edit(int patchID)
        {
            Patch entity = _patchRepository.Get(patchID);

            PatchEditViewModel viewModel = entity.ToPatchEditViewModel();

            foreach (OperatorViewModel operatorViewModel in viewModel.Patch.Operators)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel;
        }

        public PatchEditViewModel Save(PatchEditViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            // TODO: Make a patch validator.
            //IValidator validator = new PatchValidator

            throw new NotImplementedException();
        }

        public PatchEditViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float centerX, float centerY)
        {
            Operator op = _operatorRepository.Get(operatorID);

            EntityPosition entityPosition = _entityPositionManager.SetOrCreateOperatorPosition(operatorID, centerX, centerY);

            OperatorViewModel operatorViewModel = viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single();
            operatorViewModel.CenterX = centerX;
            operatorViewModel.CenterY = centerY;

            // TODO: Make it work statelessly too.

            return viewModel;
        }

        public PatchEditViewModel ChangeInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // TODO: This would be so much simpler statefully.
            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            Inlet inlet = _inletRepository.Get(inletID);
            Outlet outlet = _outletRepository.Get(inputOutletID);
            inlet.LinkTo(outlet);

            PatchEditViewModel viewModel2 = patch.ToPatchEditViewModel();

            // TODO: I got very confused about having to do this separately,
            // so it should belong in the main ToViewModel procedure
            foreach (OperatorViewModel operatorViewModel in viewModel2.Patch.Operators)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel2;
        }

        // Helpers

        private void SetViewModelPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }
    }
}
