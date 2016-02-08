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
    internal class OperatorPropertiesPresenter_ForSample
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesPresenter_ForSample(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public OperatorPropertiesViewModel_ForSample Show(OperatorPropertiesViewModel_ForSample userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForSample viewModel = entity.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForSample Refresh(OperatorPropertiesViewModel_ForSample userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            OperatorPropertiesViewModel_ForSample viewModel = entity.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForSample Close(OperatorPropertiesViewModel_ForSample userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForSample viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public OperatorPropertiesViewModel_ForSample LoseFocus(OperatorPropertiesViewModel_ForSample userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            OperatorPropertiesViewModel_ForSample viewModel = Update(userInput);

            return viewModel;
        }

        private OperatorPropertiesViewModel_ForSample Update(OperatorPropertiesViewModel_ForSample userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);
            if (!result.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForSample viewModel = entity.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result.Messages);
                viewModel.Successful = false;

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_ForSample viewModel2 = entity.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }

        // Helpers

        private void CopyNonPersistedProperties(OperatorPropertiesViewModel_ForSample sourceViewModel, OperatorPropertiesViewModel_ForSample destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }
    }
}