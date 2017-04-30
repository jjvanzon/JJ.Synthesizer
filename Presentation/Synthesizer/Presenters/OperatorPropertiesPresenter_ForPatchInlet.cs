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

        protected override OperatorPropertiesViewModel_ForPatchInlet ToViewModel(Operator op) => op.ToPropertiesViewModel_ForPatchInlet();

        protected override void UpdateEntity(OperatorPropertiesViewModel_ForPatchInlet viewModel)
        {
            // ViewModel Validator
            IValidator validator = new OperatorPropertiesViewModel_ForPatchInlet_Validator(viewModel);
            if (!validator.IsValid)
            {
                viewModel.ValidationMessages.AddRange(validator.ValidationMessages.ToCanonical());
                viewModel.Successful = false;
                return;
            }

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResultDto result = patchManager.SaveOperator(entity);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;
        }
    }
}