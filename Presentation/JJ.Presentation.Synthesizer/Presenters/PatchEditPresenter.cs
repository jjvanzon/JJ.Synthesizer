using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Converters;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class PatchEditPresenter
    {
        private IOperatorRepository _operatorRepository;
        private IEntityPositionRepository _entityPositionRepository;

        public PatchEditPresenter(IOperatorRepository operatorRepository, IEntityPositionRepository entityPositionRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _operatorRepository = operatorRepository;
            _entityPositionRepository = entityPositionRepository;
        }

        public PatchEditViewModel Create()
        {
            Operator rootOperator = _operatorRepository.Create();

            PatchEditViewModel viewModel = rootOperator.ToPatchEditViewModel(_entityPositionRepository);

            return viewModel;
        }

        public PatchEditViewModel Edit(int rootOperatorID)
        {
            Operator rootOperator = _operatorRepository.Get(rootOperatorID);

            PatchEditViewModel viewModel = rootOperator.ToPatchEditViewModel(_entityPositionRepository);

            return viewModel;
        }

        public PatchEditViewModel Save(PatchEditViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator rootOperator = viewModel.ToEntity(_operatorRepository, _entityPositionRepository);

            throw new NotImplementedException();
        }

        // TODO: StartMoveOperator and EndMoveOperator?
        public OperatorViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float newX, float newY)
        {
            throw new NotImplementedException();
        }

        public PatchEditViewModel ChangeInletInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            throw new NotImplementedException();
        }
    }
}
