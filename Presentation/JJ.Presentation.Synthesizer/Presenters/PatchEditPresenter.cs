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
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class PatchEditPresenter
    {
        private IPatchRepository _patchRepository;
        // TODO: See if this repository is even used anymore.
        private IOperatorRepository _operatorRepository;
        private IEntityPositionRepository _entityPositionRepository;

        private EntityPositionManager _entityPositionManager;

        public PatchEditPresenter(
            IPatchRepository patchRepository, 
            IOperatorRepository operatorRepository, 
            IEntityPositionRepository entityPositionRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
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

        [Obsolete("", true)]
        private void SetPositionsRecursive(OperatorViewModel operatorViewModel)
        {
            SetPosition(operatorViewModel);

            for (int i = 0; i < operatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inlet = operatorViewModel.Inlets[i];

                if (inlet.InputOutlet != null)
                {
                    SetPositionsRecursive(inlet.InputOutlet.Operator);
                }
            }
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

        // TODO: StartMoveOperator and EndMoveOperator?
        //public OperatorViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float newX, float newY)
        //{
        //    throw new NotImplementedException();
        //}

        //public PatchEditViewModel ChangeInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
