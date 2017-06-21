using JJ.Business.Canonical;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
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
            return op.ToPropertiesViewModel_ForInletsToDimension();
        }

        protected override void UpdateEntity(OperatorPropertiesViewModel_ForInletsToDimension viewModel)
        {
            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

            // Business
            VoidResult result1 = _patchManager.SetOperatorInletCount(entity, viewModel.InletCount);
            if (!result1.Successful)
            {
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result1.Messages.ToCanonical());

                // Successful?
                viewModel.Successful = result1.Successful;

                return;
            }

            VoidResult result2 = _patchManager.SaveOperator(entity);
            // ReSharper disable once InvertIf
            if (!result2.Successful)
            {
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result2.Messages.ToCanonical());

                // Successful?
                viewModel.Successful = result2.Successful;
            }
        }
    }
}