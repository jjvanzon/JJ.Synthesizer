using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForBundle
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesPresenter_ForBundle(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public OperatorPropertiesViewModel_ForBundle Show(OperatorPropertiesViewModel_ForBundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForBundle viewModel = entity.ToPropertiesViewModel_ForBundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForBundle Refresh(OperatorPropertiesViewModel_ForBundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForBundle viewModel = entity.ToPropertiesViewModel_ForBundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForBundle Close(OperatorPropertiesViewModel_ForBundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForBundle viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForBundle LoseFocus(OperatorPropertiesViewModel_ForBundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForBundle viewModel = Update(userInput);

            return viewModel;
        }

        private OperatorPropertiesViewModel_ForBundle Update(OperatorPropertiesViewModel_ForBundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);

            VoidResult result1 = patchManager.SetOperatorInletCount(entity, userInput.InletCount);
            if (!result1.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForBundle viewModel = entity.ToPropertiesViewModel_ForBundle();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result1.Messages);
                viewModel.Successful = false;

                return viewModel;
            }

            VoidResult result2 = patchManager.SaveOperator(entity);
            if (!result2.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForBundle viewModel = entity.ToPropertiesViewModel_ForBundle();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_ForBundle viewModel2 = entity.ToPropertiesViewModel_ForBundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }

        // Helpers

        private void CopyNonPersistedProperties(OperatorPropertiesViewModel_ForBundle sourceViewModel, OperatorPropertiesViewModel_ForBundle destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}