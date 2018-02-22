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
	internal class CurrentInstrumentBarPresenter
		: EntityPresenterWithoutSaveBase<(Document document, IList<Patch> patches), CurrentInstrumentBarViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IPatchRepository _patchRepository;
		private readonly AutoPatcher _autoPatcher;
		private readonly SystemFacade _systemFacade;

		public CurrentInstrumentBarPresenter(
			AutoPatcher autoPatcher,
			SystemFacade systemFacade,
			IDocumentRepository documentRepository,
			IPatchRepository patchRepository)
		{
			_autoPatcher = autoPatcher ?? throw new ArgumentNullException(nameof(autoPatcher));
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_patchRepository = patchRepository ?? throw new ArgumentNullException(nameof(patchRepository));
			_systemFacade = systemFacade ?? throw new ArgumentNullException(nameof(systemFacade));
		}

		protected override (Document document, IList<Patch> patches) GetEntity(CurrentInstrumentBarViewModel userInput)
		{
			Document document = _documentRepository.Get(userInput.DocumentID);

			IList<Patch> patches = userInput.Patches
			                                .Select(x => x.EntityID)
			                                .Select(x => _patchRepository.Get(x))
			                                .ToList();
			return (document, patches);
		}

		protected override CurrentInstrumentBarViewModel ToViewModel((Document document, IList<Patch> patches) tuple)
		{
			(Document document, IList<Patch> patches) = tuple;

			// It is kind of a hack to do this much in the ToViewModel, because these entities should already have been retrieved.
			IList<MidiMapping> midiMappings = _systemFacade.GetDefaultMidiMappings();

			Scale scale = midiMappings.SelectMany(x => x.MidiMappingElements)
			                          .Select(x => x.Scale)
			                          .FirstOrDefault(x => x != null);

			return document.ToCurrentInstrumentBarViewModel(scale, midiMappings, patches);
		}

		public CurrentInstrumentBarViewModel AddPatch(CurrentInstrumentBarViewModel userInput, int patchID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					Patch patch = _patchRepository.Get(patchID);
					entities.patches.Add(patch);
				});
		}

		public CurrentInstrumentBarViewModel MovePatch(CurrentInstrumentBarViewModel viewModel, int patchID, int newPosition)
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

		public CurrentInstrumentBarViewModel MovePatchBackward(CurrentInstrumentBarViewModel viewModel, int patchID)
		{
			int currentPosition = viewModel.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(viewModel, patchID, currentPosition - 1);
		}

		public CurrentInstrumentBarViewModel MovePatchForward(CurrentInstrumentBarViewModel viewModel, int patchID)
		{
			int currentPosition = viewModel.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(viewModel, patchID, currentPosition + 1);
		}

		public CurrentInstrumentBarViewModel Play(CurrentInstrumentBarViewModel userInput)
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

		public CurrentInstrumentBarViewModel PlayPatch(CurrentInstrumentBarViewModel userInput, int patchID)
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

		public CurrentInstrumentBarViewModel RemovePatch(CurrentInstrumentBarViewModel viewModel, int patchID)
		{
			return ExecuteAction(viewModel, entities => entities.patches.RemoveFirst(x => x.ID == patchID));
		}

		public CurrentInstrumentBarViewModel Load(CurrentInstrumentBarViewModel userInput) => Refresh(userInput);

		[Obsolete("Use Load instead.", true)]
		public override void Show(CurrentInstrumentBarViewModel viewModel) => throw new NotSupportedException("Call Load instead.");
	}
}