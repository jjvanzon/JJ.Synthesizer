using JJ.Framework.Exceptions;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : PropertiesPresenterBase<PatchPropertiesViewModel>
    {
        private readonly PatchRepositories _repositories;

        public PatchPropertiesPresenter(PatchRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        protected override PatchPropertiesViewModel CreateViewModel(PatchPropertiesViewModel userInput)
        {
            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel();

            return viewModel;
        }

        protected override PatchPropertiesViewModel UpdateEntity(PatchPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Patch patch = _repositories.PatchRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(patch, _repositories);
                VoidResult result = patchManager.SavePatch();

                // Non-Persisted
                viewModel.ValidationMessages = result.Messages;
                viewModel.Successful = result.Successful;
            });
        }
    }
}