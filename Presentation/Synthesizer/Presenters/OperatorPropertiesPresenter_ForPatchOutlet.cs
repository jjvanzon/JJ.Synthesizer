using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Business.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchOutlet
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForPatchOutlet>
    {
        public OperatorPropertiesPresenter_ForPatchOutlet(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForPatchOutlet ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForPatchOutlet();
        }

        protected override void UpdateEntity(OperatorPropertiesViewModel_ForPatchOutlet viewModel)
        {
            // ViewModel Validator
            IValidator validator = new OperatorPropertiesViewModel_ForPatchOutlet_Validator(viewModel);
            if (!validator.IsValid)
            {
                viewModel.Successful = validator.IsValid;
                viewModel.ValidationMessages.AddRange(validator.ValidationMessages.ToCanonical());
                return;
            }

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;
        }
    }
}