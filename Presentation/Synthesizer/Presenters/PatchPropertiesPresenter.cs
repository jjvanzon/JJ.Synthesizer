using JJ.Framework.Exceptions;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : PresenterBase<PatchPropertiesViewModel>
    {
        private readonly PatchRepositories _repositories;

        public PatchPropertiesPresenter(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

        }

        public PatchPropertiesViewModel Show(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchPropertiesViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchPropertiesViewModel Refresh(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchPropertiesViewModel viewModel = CreateViewModel(userInput);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchPropertiesViewModel Close(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchPropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public PatchPropertiesViewModel LoseFocus(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchPropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private PatchPropertiesViewModel Update(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch =_repositories.PatchRepository.Get(userInput.ID);

            // Business
            var patchManager = new PatchManager(patch, _repositories);
            VoidResult result = patchManager.SavePatch();

            // ToViewModel
            PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages = result.Messages;
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        private PatchPropertiesViewModel CreateViewModel(PatchPropertiesViewModel userInput)
        {
            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            return viewModel;
        }
    }
}