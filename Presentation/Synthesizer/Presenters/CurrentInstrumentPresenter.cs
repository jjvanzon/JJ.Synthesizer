using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class CurrentInstrumentPresenter
		: EntityPresenterWithoutSaveBase<(Document document, IList<Patch> patches), CurrentInstrumentViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IPatchRepository _patchRepository;
		private readonly AutoPatcher _autoPatcher;

		public CurrentInstrumentPresenter(AutoPatcher autoPatcher, IDocumentRepository documentRepository, IPatchRepository patchRepository)
		{
			_autoPatcher = autoPatcher ?? throw new ArgumentNullException(nameof(autoPatcher));
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_patchRepository = patchRepository ?? throw new ArgumentNullException(nameof(patchRepository));
		}

		protected override (Document document, IList<Patch> patches) GetEntity(CurrentInstrumentViewModel userInput)
		{
			Document document = _documentRepository.Get(userInput.DocumentID);

			IList<Patch> patches = userInput.List
			                                .Select(x => x.PatchID)
			                                .Select(x => _patchRepository.Get(x))
			                                .ToList();

			return (document, patches);
		}

		protected override CurrentInstrumentViewModel ToViewModel((Document document, IList<Patch> patches) x)
		{
			return x.document.ToCurrentInstrumentViewModel(x.patches);
		}

		public CurrentInstrumentViewModel Add(CurrentInstrumentViewModel userInput, int patchID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					Patch patch = _patchRepository.Get(patchID);
					entities.patches.Add(patch);
				});
		}

		public CurrentInstrumentViewModel Move(CurrentInstrumentViewModel viewModel, int patchID, int newPosition)
		{
			return ExecuteAction(
				viewModel,
				entities =>
				{
					if (newPosition < 0) newPosition = 0;
					if (newPosition > entities.patches.Count - 1) newPosition = entities.patches.Count - 1;

					Patch patch = _patchRepository.Get(patchID);
					entities.patches.Remove(patch);
					entities.patches.Insert(newPosition, patch);
				});
		}

		public CurrentInstrumentViewModel MoveBackward(CurrentInstrumentViewModel viewModel, int patchID)
		{
			int currentPosition = viewModel.List.IndexOf(x => x.PatchID == patchID);

			return Move(viewModel, patchID, currentPosition - 1);
		}

		public CurrentInstrumentViewModel MoveForward(CurrentInstrumentViewModel viewModel, int patchID)
		{
			int currentPosition = viewModel.List.IndexOf(x => x.PatchID == patchID);

			return Move(viewModel, patchID, currentPosition + 1);
		}

		public CurrentInstrumentViewModel Play(CurrentInstrumentViewModel userInput)
		{
			Outlet outlet = null;

			return ExecuteAction(
				userInput,
				entities =>
				{
					Patch autoPatch = _autoPatcher.AutoPatch(entities.patches);
					_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
					Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);
					outlet = result.Data;
					return result;
				},
				viewModel => viewModel.OutletIDToPlay = outlet?.ID);
		}

		public CurrentInstrumentViewModel PlayItem(CurrentInstrumentViewModel userInput, int patchID)
		{
			Outlet outlet = null;

			return ExecuteAction(
				userInput,
				entities =>
				{
					Patch patch = entities.patches.Where(x => x.ID == patchID).SingleOrDefaultWithClearException(new { ID = patchID });
					Patch autoPatch = _autoPatcher.AutoPatch(patch); // Use AutoPatch to hack in creating a new patch, not changing the old one.
					_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
					Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);
					outlet = result.Data;
					return result;
				},
				viewModel => viewModel.OutletIDToPlay = outlet?.ID);
		}

		public CurrentInstrumentViewModel Remove(CurrentInstrumentViewModel viewModel, int patchID)
		{
			return ExecuteAction(viewModel, entities => entities.patches.RemoveFirst(x => x.ID == patchID));
		}

		[Obsolete("Use Load instead.", true)]
		public override void Show(CurrentInstrumentViewModel viewModel) => throw new NotSupportedException("Call Load instead.");
	}
}