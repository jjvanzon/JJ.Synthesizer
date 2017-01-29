using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchGridPresenter : PresenterBase<PatchGridViewModel>
    {
        private readonly PatchRepositories _repositories;
        private readonly DocumentManager _documentManager;

        public PatchGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
            _repositories = new PatchRepositories(repositories);
        }

        public PatchGridViewModel Show(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Refresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Close(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            PatchGridViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private PatchGridViewModel CreateViewModel(PatchGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

            // Business
            var patchManager = new PatchManager(_repositories);
            IList<Patch> patchesInGroup = patchManager.GetPatchesInGroup_IncludingGroupless(document.Patches, userInput.Group);
            IList<UsedInDto<Patch>> usedInDtos = _documentManager.GetUsedIn(patchesInGroup);

            // ToViewModel
            PatchGridViewModel viewModel = usedInDtos.ToPatchGridViewModel(userInput.DocumentID, userInput.Group);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            return viewModel;
        }
    }
}
