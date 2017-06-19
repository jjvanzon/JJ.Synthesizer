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
        private readonly PatchManager _patchManager;
        private readonly AutoPatcher _autoPatcher;

        public PatchPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchManager = new PatchManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
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
            VoidResult result = _patchManager.SavePatch(patch);

            // Non-Persisted
            viewModel.ValidationMessages = result.Messages.ToCanonical();
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
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(patch);
                    Outlet outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

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
                    IResult result = _patchManager.DeletePatchWithRelatedEntities(patch);

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }

        public PatchPropertiesViewModel HasDimensionChanged(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Cannot use the template method, because CreateViewModel must come after the business.

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.ID);

            // Business
            IResult result = _patchManager.SavePatch(patch);

            // CreateViewModel
            PatchPropertiesViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}