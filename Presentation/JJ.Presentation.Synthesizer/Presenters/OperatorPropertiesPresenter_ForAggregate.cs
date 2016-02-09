using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForAggregate
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesPresenter_ForAggregate(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public OperatorPropertiesViewModel_ForAggregate Show(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForAggregate viewModel = entity.ToPropertiesViewModel_ForAggregate();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForAggregate Refresh(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForAggregate viewModel = entity.ToPropertiesViewModel_ForAggregate();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForAggregate Close(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForAggregate viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForAggregate LoseFocus(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForAggregate viewModel = Update(userInput);

            return viewModel;
        }

        private OperatorPropertiesViewModel_ForAggregate Update(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // ToViewModel
            OperatorPropertiesViewModel_ForAggregate viewModel = entity.ToPropertiesViewModel_ForAggregate();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        // Helpers

        private void CopyNonPersistedProperties(OperatorPropertiesViewModel_ForAggregate sourceViewModel, OperatorPropertiesViewModel_ForAggregate destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}