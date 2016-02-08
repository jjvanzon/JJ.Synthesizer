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
    internal class OperatorPropertiesPresenter_ForUnbundle
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesPresenter_ForUnbundle(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public OperatorPropertiesViewModel_ForUnbundle Show(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForUnbundle viewModel = entity.ToPropertiesViewModel_ForUnbundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForUnbundle Refresh(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForUnbundle viewModel = entity.ToPropertiesViewModel_ForUnbundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForUnbundle Close(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForUnbundle viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForUnbundle LoseFocus(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForUnbundle viewModel = Update(userInput);

            return viewModel;
        }

        private OperatorPropertiesViewModel_ForUnbundle Update(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result1 = patchManager.SetOperatorOutletCount(entity, userInput.OutletCount);
            if (!result1.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForUnbundle viewModel = entity.ToPropertiesViewModel_ForUnbundle();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result1.Messages);
                viewModel.Successful = false;

                return viewModel;
            }

            // Business
            VoidResult result2 = patchManager.SaveOperator(entity);
            if (!result2.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForUnbundle viewModel = entity.ToPropertiesViewModel_ForUnbundle();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);
                viewModel.Successful = false;

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_ForUnbundle viewModel2 = entity.ToPropertiesViewModel_ForUnbundle();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }

        // Helpers

        private void CopyNonPersistedProperties(OperatorPropertiesViewModel_ForUnbundle sourceViewModel, OperatorPropertiesViewModel_ForUnbundle destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}