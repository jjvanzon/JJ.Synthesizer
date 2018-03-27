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
		: EntityPresenterWithoutSaveBase<(Document document, Scale scale, IList<MidiMapping> midiMappings, IList<Patch> patches), CurrentInstrumentBarViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IMidiMappingRepository _midiMappingRepository;
		private readonly IPatchRepository _patchRepository;
		private readonly IScaleRepository _scaleRepository;
		private readonly AutoPatcher _autoPatcher;
		private readonly SystemFacade _systemFacade;

		public CurrentInstrumentBarPresenter(
			AutoPatcher autoPatcher,
			SystemFacade systemFacade,
			IDocumentRepository documentRepository,
			IMidiMappingRepository midiMappingRepository,
			IPatchRepository patchRepository,
			IScaleRepository scaleRepository)
		{
			_autoPatcher = autoPatcher ?? throw new ArgumentNullException(nameof(autoPatcher));
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_midiMappingRepository = midiMappingRepository ?? throw new ArgumentNullException(nameof(midiMappingRepository));
			_patchRepository = patchRepository ?? throw new ArgumentNullException(nameof(patchRepository));
			_scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
			_systemFacade = systemFacade ?? throw new ArgumentNullException(nameof(systemFacade));
		}

		protected override (Document document, Scale scale, IList<MidiMapping> midiMappings, IList<Patch> patches) GetEntity(CurrentInstrumentBarViewModel userInput)
		{
			Document document = _documentRepository.Get(userInput.DocumentID);

			IList<MidiMapping> midiMappings = _systemFacade.GetDefaultMidiMappings();

			Scale scale = midiMappings.SelectMany(x => x.MidiMappingElements)
			                          .Select(x => x.Scale)
			                          .FirstOrDefault(x => x != null);

			IList<Patch> patches = userInput.Patches
			                                .Select(x => x.EntityID)
			                                .Select(x => _patchRepository.Get(x))
			                                .ToList();

			return (document, scale, midiMappings, patches);
		}

		protected override CurrentInstrumentBarViewModel ToViewModel((Document document, Scale scale, IList<MidiMapping> midiMappings, IList<Patch> patches) tuple)
		{
			(Document document, Scale scale, IList<MidiMapping> midiMappings, IList<Patch> patches) = tuple;

			return document.ToCurrentInstrumentBarViewModel(scale, midiMappings, patches);
		}

		public CurrentInstrumentBarViewModel AddMidiMapping(CurrentInstrumentBarViewModel userInput, int midiMappingID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					MidiMapping midiMapping = _midiMappingRepository.Get(midiMappingID);
					entities.midiMappings.Add(midiMapping);
				});
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

		public CurrentInstrumentBarViewModel Load(CurrentInstrumentBarViewModel userInput) => Refresh(userInput);

		public CurrentInstrumentBarViewModel MoveMidiMapping(CurrentInstrumentBarViewModel userInput, int midiMappingID, int newPosition)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					if (newPosition < 0) newPosition = 0;
					if (newPosition > entities.midiMappings.Count - 1) newPosition = entities.midiMappings.Count - 1;

					MidiMapping midiMapping = _midiMappingRepository.Get(midiMappingID);
					entities.midiMappings.Remove(midiMapping);
					entities.midiMappings.Insert(newPosition, midiMapping);
				});
		}

		public CurrentInstrumentBarViewModel MoveMidiMappingBackward(CurrentInstrumentBarViewModel userInput, int midiMappingID)
		{
			int currentPosition = userInput.MidiMappings.IndexOf(x => x.EntityID == midiMappingID);

			return MoveMidiMapping(userInput, midiMappingID, currentPosition - 1);
		}

		public CurrentInstrumentBarViewModel MoveMidiMappingForward(CurrentInstrumentBarViewModel userInput, int midiMappingID)
		{
			int currentPosition = userInput.MidiMappings.IndexOf(x => x.EntityID == midiMappingID);

			return MoveMidiMapping(userInput, midiMappingID, currentPosition + 1);
		}

		public CurrentInstrumentBarViewModel MovePatch(CurrentInstrumentBarViewModel userInput, int patchID, int newPosition)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					if (newPosition < 0) newPosition = 0;
					if (newPosition > entities.patches.Count - 1) newPosition = entities.patches.Count - 1;

					Patch patch = _patchRepository.Get(patchID);
					entities.patches.Remove(patch);
					entities.patches.Insert(newPosition, patch);
				});
		}

		public CurrentInstrumentBarViewModel MovePatchBackward(CurrentInstrumentBarViewModel userInput, int patchID)
		{
			int currentPosition = userInput.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(userInput, patchID, currentPosition - 1);
		}

		public CurrentInstrumentBarViewModel MovePatchForward(CurrentInstrumentBarViewModel userInput, int patchID)
		{
			int currentPosition = userInput.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(userInput, patchID, currentPosition + 1);
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

		public CurrentInstrumentBarViewModel RemoveMidiMapping(CurrentInstrumentBarViewModel userInput, int midiMappingID)
		{
			return ExecuteAction(userInput, entities => entities.midiMappings.RemoveFirst(x => x.ID == midiMappingID));
		}

		public CurrentInstrumentBarViewModel RemovePatch(CurrentInstrumentBarViewModel userInput, int patchID)
		{
			return ExecuteAction(userInput, entities => entities.patches.RemoveFirst(x => x.ID == patchID));
		}

		public CurrentInstrumentBarViewModel SetScale(CurrentInstrumentBarViewModel userInput, int scaleID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					Scale scale = _scaleRepository.Get(scaleID);
					entities.scale = scale;
				});
		}

		[Obsolete("Call Load instead.", true)]
		public override void Show(CurrentInstrumentBarViewModel viewModel) => throw new NotSupportedException("Call Load instead.");
	}
}