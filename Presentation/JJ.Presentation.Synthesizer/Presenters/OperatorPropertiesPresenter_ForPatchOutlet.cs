using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Business.Canonical;
using JJ.Framework.Common;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchOutlet
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForPatchOutlet>
    {
        public OperatorPropertiesPresenter_ForPatchOutlet(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForPatchOutlet ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForPatchOutlet();
        }

        protected override OperatorPropertiesViewModel_ForPatchOutlet Update(OperatorPropertiesViewModel_ForPatchOutlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validator
            IValidator validator = new OperatorPropertiesViewModel_ForPatchOutlet_Validator(userInput);
            if (!validator.IsValid)
            {
                userInput.Successful = validator.IsValid;
                userInput.ValidationMessages.AddRange(validator.ValidationMessages.ToCanonical());
                return userInput;
            }

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // ToViewModel
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = entity.ToPropertiesViewModel_ForPatchOutlet();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}