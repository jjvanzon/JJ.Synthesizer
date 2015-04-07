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
using JJ.Presentation.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;

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

        private Patch _entity;
        private PatchEditViewModel _viewModel;

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
            _entity = _patchRepository.Create();

            _viewModel = CreateViewModel(_entity);

            return _viewModel;
        }

        public PatchEditViewModel Edit(int patchID)
        {
            _entity = _patchRepository.Get(patchID);

            _viewModel = CreateViewModel(_entity);

            return _viewModel;
        }

        public PatchEditViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float centerX, float centerY)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_entity == null)
            {
                _entity = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            Operator op = _operatorRepository.Get(operatorID); // This is just to check that the entity exists. TODO: But that's weird. You should be doing that in the entity position manager if anywhere.
            EntityPosition entityPosition = _entityPositionManager.SetOrCreateOperatorPosition(operatorID, centerX, centerY);

            if (_viewModel == null)
            {
                _viewModel = CreateViewModel(_entity);
            }
            else
            {
                OperatorViewModel operatorViewModel = _viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single();
                operatorViewModel.CenterX = centerX;
                operatorViewModel.CenterY = centerY;

                _viewModel.SavedMessageVisible = false;
            }

            return _viewModel;
        }

        public PatchEditViewModel ChangeInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_entity == null)
            {
                _entity = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            Inlet inlet = _inletRepository.Get(inletID);
            Outlet outlet = _outletRepository.Get(inputOutletID);
            inlet.LinkTo(outlet);

            // TODO: In a stateful situation you might just adjust a small part of the view model.
            _viewModel = CreateViewModel(_entity);

            return _viewModel;
        }

        public PatchEditViewModel Save(PatchEditViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_entity == null)
            {
                _entity = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            if (_viewModel == null)
            {
                _viewModel = CreateViewModel(_entity);
            }

            IValidator validator = new PatchValidator(_entity);
            if (!validator.IsValid)
            {
                _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                _viewModel.SavedMessageVisible = false;
                return _viewModel;
            }
            else
            {
                _patchRepository.Commit();
                _viewModel.SavedMessageVisible = true;
                return _viewModel;
            }
        }

        // Helpers

        private PatchEditViewModel CreateViewModel(Patch entity)
        {
            PatchEditViewModel viewModel = entity.ToPatchEditViewModel();

            // TODO: I got very confused about having to do this separately,
            // so it should belong in the main ToViewModel procedure
            foreach (OperatorViewModel operatorViewModel in viewModel.Patch.Operators)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel;
        }

        private void SetViewModelPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }
    }
}
