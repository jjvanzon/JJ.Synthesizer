using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Canonical;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchInlet
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesPresenter_ForPatchInlet(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public OperatorPropertiesViewModel_ForPatchInlet Show(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForPatchInlet viewModel = entity.ToPropertiesViewModel_ForPatchInlet(_repositories.InletTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForPatchInlet Refresh(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForPatchInlet viewModel = entity.ToPropertiesViewModel_ForPatchInlet(_repositories.InletTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForPatchInlet Close(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForPatchInlet viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForPatchInlet LoseFocus(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForPatchInlet viewModel = Update(userInput);

            return viewModel;
        }

        private OperatorPropertiesViewModel_ForPatchInlet Update(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validator
            IValidator validator = new OperatorPropertiesViewModel_ForPatchInlet_Validator(userInput);
            if (!validator.IsValid)
            {
                userInput.Successful = validator.IsValid;
                userInput.ValidationMessages = validator.ValidationMessages.ToCanonical();
                return userInput;
            }

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // ToViewModel
            OperatorPropertiesViewModel_ForPatchInlet viewModel = entity.ToPropertiesViewModel_ForPatchInlet(_repositories.InletTypeRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        // Helpers

        private void CopyNonPersistedProperties(OperatorPropertiesViewModel_ForPatchInlet sourceViewModel, OperatorPropertiesViewModel_ForPatchInlet destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}