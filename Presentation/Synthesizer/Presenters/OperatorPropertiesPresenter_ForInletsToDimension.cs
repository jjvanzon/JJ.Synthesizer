using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForInletsToDimension
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForInletsToDimension>
    {
        public OperatorPropertiesPresenter_ForInletsToDimension(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForInletsToDimension ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForInletsToDimension(_repositories.PatchRepository);
        }

        protected override void UpdateEntity(OperatorPropertiesViewModel_ForInletsToDimension viewModel)
        {
            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);

            VoidResultDto result1 = patchManager.SetOperatorInletCount(entity, viewModel.InletCount);
            if (!result1.Successful)
            {
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result1.Messages);

                // Successful?
                viewModel.Successful = result1.Successful;

                return;
            }

            VoidResultDto result2 = patchManager.SaveOperator(entity);
            // ReSharper disable once InvertIf
            if (!result2.Successful)
            {
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result2.Messages);

                // Successful?
                viewModel.Successful = result2.Successful;
            }
        }
    }
}