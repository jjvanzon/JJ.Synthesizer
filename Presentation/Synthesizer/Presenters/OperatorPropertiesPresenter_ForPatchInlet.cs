using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Business.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchInlet
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForPatchInlet>
    {
        public OperatorPropertiesPresenter_ForPatchInlet(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForPatchInlet ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForPatchInlet();
        }

        protected override OperatorPropertiesViewModel_ForPatchInlet UpdateEntity(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            { 
                // ViewModel Validator
                IValidator validator = new OperatorPropertiesViewModel_ForPatchInlet_Validator(userInput);
                if (!validator.IsValid)
                {
                    userInput.ValidationMessages.AddRange(validator.ValidationMessages.ToCanonical());
                    userInput.Successful = false;
                    return;
                }

                // GetEntity
                Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(entity.Patch, _repositories);
                VoidResultDto result = patchManager.SaveOperator(entity);
                
                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}