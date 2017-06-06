using JJ.Business.Canonical;
using JJ.Framework.Exceptions;
using Canonicals = JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : PropertiesPresenterBase<PatchPropertiesViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly PatchRepositories _patchRepositories;

        public PatchPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchRepositories = new PatchRepositories(_repositories);
        }

        protected override PatchPropertiesViewModel CreateViewModel(PatchPropertiesViewModel userInput)
        {
            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel();

            return viewModel;
        }

        protected override void UpdateEntity(PatchPropertiesViewModel viewModel)
        {
            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(viewModel.ID);

            // Business
            var patchManager = new PatchManager(patch, _patchRepositories);
            Canonicals.VoidResultDto result = patchManager.SavePatch();

            // Non-Persisted
            viewModel.ValidationMessages = result.Messages;
            viewModel.Successful = result.Successful;
        }

        public PatchPropertiesViewModel Play(PatchPropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Patch patch = _repositories.PatchRepository.Get(userInput.ID);

                    // Business
                    var patchManager = new PatchManager(patch, _patchRepositories);
                    Result<Outlet> result = patchManager.AutoPatch_TryCombineSounds(patch);
                    Outlet outlet = result.Data;

                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet?.ID;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.Successful = result.Successful;
                });
        }

        public PatchPropertiesViewModel Delete(PatchPropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Patch patch = _repositories.PatchRepository.Get(userInput.ID);

                    // Businesss
                    var patchManager = new PatchManager(patch, _patchRepositories);
                    IResult result = patchManager.DeletePatchWithRelatedEntities();

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }
    }
}