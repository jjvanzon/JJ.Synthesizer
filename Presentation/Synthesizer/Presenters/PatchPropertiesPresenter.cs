using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class PatchPropertiesPresenter : EntityPresenterWithSaveBase<Patch, PatchPropertiesViewModel>
	{
		private readonly RepositoryWrapper _repositories;
		private readonly PatchFacade _patchFacade;
		private readonly AutoPatcher _autoPatcher;

		public PatchPropertiesPresenter(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
			_patchFacade = new PatchFacade(repositories);
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

		protected override IResult Save(Patch entity, PatchPropertiesViewModel userInput)
		{
			return _patchFacade.SavePatch(entity);
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
				entity => _patchFacade.DeletePatchWithRelatedEntities(entity));
		}

		public PatchPropertiesViewModel ChangeHasDimension(PatchPropertiesViewModel userInput)
		{
			return ExecuteAction(
				userInput,
				entity => _patchFacade.SavePatch(entity));
		}
	}
}