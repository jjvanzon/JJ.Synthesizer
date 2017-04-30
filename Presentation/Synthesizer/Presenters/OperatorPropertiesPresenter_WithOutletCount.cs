using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithOutletCount
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithOutletCount>
    {
        public OperatorPropertiesPresenter_WithOutletCount(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithOutletCount ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithOutletCount();
        }

        protected override OperatorPropertiesViewModel_WithOutletCount UpdateEntity(OperatorPropertiesViewModel_WithOutletCount userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(entity.Patch, _repositories);
                {
                    VoidResultDto result1 = patchManager.SetOperatorOutletCount(entity, userInput.OutletCount);
                    if (!result1.Successful)
                    {
                        // Non-Persisted
                        viewModel.ValidationMessages.AddRange(result1.Messages);

                        // Successful?
                        viewModel.Successful = result1.Successful;

                        return;
                    }
                }
                {
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
            });
        }
    }
}