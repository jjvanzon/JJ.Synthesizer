using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithInletCount
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithInletCount>
    {
        public OperatorPropertiesPresenter_WithInletCount(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithInletCount ToViewModel(Operator op)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = op.ToPropertiesViewModel_WithInletCount();
            return viewModel;
        }

        protected override OperatorPropertiesViewModel_WithInletCount UpdateEntity(OperatorPropertiesViewModel_WithInletCount userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(entity.Patch, _repositories);
                {
                    VoidResultDto result1 = patchManager.SetOperatorInletCount(entity, userInput.InletCount);
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