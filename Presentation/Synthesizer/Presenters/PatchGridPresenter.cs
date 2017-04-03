using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchGridPresenter : GridPresenterBase<PatchGridViewModel>
    {
        private readonly PatchRepositories _repositories;
        private readonly DocumentManager _documentManager;

        public PatchGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentManager = new DocumentManager(repositories);
            _repositories = new PatchRepositories(repositories);
        }

        protected override PatchGridViewModel CreateViewModel(PatchGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

            // Business
            var patchManager = new PatchManager(_repositories);
            IList<Patch> patchesInGroup = patchManager.GetPatchesInGroup_IncludingGroupless(document.Patches, userInput.Group);
            IList<UsedInDto<Patch>> usedInDtos = _documentManager.GetUsedIn(patchesInGroup);

            // ToViewModel
            PatchGridViewModel viewModel = usedInDtos.ToPatchGridViewModel(userInput.DocumentID, userInput.Group);

            return viewModel;
        }
    }
}
