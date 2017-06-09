using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchGridPresenter : GridPresenterBase<PatchGridViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public PatchGridPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
        }

        protected override PatchGridViewModel CreateViewModel(PatchGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

            // Business
            var patchManager = new PatchManager(_repositories);
            IList<Patch> patchesInGroup = patchManager.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(document.Patches, userInput.Group, mustIncludeHidden: true);
            IList<UsedInDto<Patch>> usedInDtos = _documentManager.GetUsedIn(patchesInGroup);

            // ToViewModel
            PatchGridViewModel viewModel = usedInDtos.ToGridViewModel(userInput.DocumentID, userInput.Group);

            return viewModel;
        }

        public PatchGridViewModel Play(PatchGridViewModel userInput, int patchID)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(patchID);

                // Business
                var patchManager = new PatchManager(patch, _repositories);
                Result<Outlet> result = patchManager.AutoPatch_TryCombineSounds(patch);
                Outlet outlet = result.Data;

                // Non-Persisted
                viewModel.OutletIDToPlay = outlet?.ID;
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                viewModel.Successful = result.Successful;
            });
        }

        public PatchGridViewModel Delete(PatchGridViewModel userInput, int patchID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Patch patch = _repositories.PatchRepository.Get(patchID);

                    // Businesss
                    var patchManager = new PatchManager(patch, _repositories);
                    IResult result = patchManager.DeletePatchWithRelatedEntities();

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }

    }
}
