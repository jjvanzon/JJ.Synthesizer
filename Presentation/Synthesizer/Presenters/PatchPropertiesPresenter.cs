using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : EntityPresenterWithSaveBase<Patch, PatchPropertiesViewModel>
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

        protected override Patch GetEntity(PatchPropertiesViewModel userInput)
        {
            return _repositories.PatchRepository.Get(userInput.ID);
        }

        protected override PatchPropertiesViewModel ToViewModel(Patch entity)
        {
            return entity.ToPropertiesViewModel();
        }

        protected override IResult Save(Patch entity)
        {
            return _patchManager.SavePatch(entity);
        }

        public PatchPropertiesViewModel Play(PatchPropertiesViewModel userInput)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entity =>
                {
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(entity);
                    outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }
                    return null;
                },
                viewModel => viewModel.OutletIDToPlay = outlet?.ID);
        }

        public PatchPropertiesViewModel Delete(PatchPropertiesViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity => _patchManager.DeletePatchWithRelatedEntities(entity));
        }

        public PatchPropertiesViewModel ChangeHasDimension(PatchPropertiesViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity => _patchManager.SavePatch(entity));
        }
    }
}