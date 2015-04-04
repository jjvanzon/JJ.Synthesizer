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
        private IOperatorRepository _operatorRepository; // TODO: See if this repository is even used anymore.
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
                SetPosition(operatorViewModel);
            }

            return viewModel;
        }

        private void SetPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }

        public PatchEditViewModel Save(PatchEditViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _entityPositionRepository);

            throw new NotImplementedException();
        }

        public PatchEditViewModel ChangeInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // TODO: Program the real ToEntity method.
            //Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _entityPositionRepository);
            Patch patch = _patchRepository.Get(viewModel.Patch.ID);

            Inlet inlet = _inletRepository.Get(inletID);
            Outlet outlet = _outletRepository.Get(inputOutletID);
            inlet.LinkTo(outlet);

            PatchEditViewModel viewModel2 = patch.ToPatchEditViewModel();

            return viewModel;
        }

        // TODO: StartMoveOperator and EndMoveOperator?
        //public OperatorViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float newX, float newY)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
