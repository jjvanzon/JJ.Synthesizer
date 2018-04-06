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
	internal class InstrumentBarPresenter
		: EntityPresenterWithoutSaveBase<(Document document, Scale scale, IList<MidiMappingGroup> midiMappings, IList<Patch> patches), InstrumentBarViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IMidiMappingGroupRepository _midiMappingGroupRepository;
		private readonly IPatchRepository _patchRepository;
		private readonly IScaleRepository _scaleRepository;
		private readonly AutoPatcher _autoPatcher;
		private readonly SystemFacade _systemFacade;

		public InstrumentBarPresenter(
			AutoPatcher autoPatcher,
			SystemFacade systemFacade,
			IDocumentRepository documentRepository,
			IMidiMappingGroupRepository midiMappingRepository,
			IPatchRepository patchRepository,
			IScaleRepository scaleRepository)
		{
			_autoPatcher = autoPatcher ?? throw new ArgumentNullException(nameof(autoPatcher));
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_midiMappingGroupRepository = midiMappingRepository ?? throw new ArgumentNullException(nameof(midiMappingRepository));
			_patchRepository = patchRepository ?? throw new ArgumentNullException(nameof(patchRepository));
			_scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
			_systemFacade = systemFacade ?? throw new ArgumentNullException(nameof(systemFacade));
		}

		protected override (Document document, Scale scale, IList<MidiMappingGroup> midiMappings, IList<Patch> patches) GetEntity(InstrumentBarViewModel userInput)
		{
			Document document = _documentRepository.Get(userInput.DocumentID);

			Scale scale = _scaleRepository.TryGet(userInput.Scale.ID);

			IList<MidiMappingGroup> midiMappings = userInput.MidiMappingGroups
			                                           .Select(x => _midiMappingGroupRepository.Get(x.EntityID))
			                                           .ToList();
			IList<Patch> patches = userInput.Patches
			                                .Select(x => _patchRepository.Get(x.EntityID))
			                                .ToList();
			if (midiMappings.Count == 0)
			{
				midiMappings = _systemFacade.GetDefaultMidiMappingGroups();
			}

			if (scale == null)
			{
				scale = _systemFacade.GetDefaultScale();
			}

			return (document, scale, midiMappings, patches);
		}

		protected override InstrumentBarViewModel ToViewModel((Document document, Scale scale, IList<MidiMappingGroup> midiMappings, IList<Patch> patches) tuple)
		{
			(Document document, Scale scale, IList<MidiMappingGroup> midiMappings, IList<Patch> patches) = tuple;

			return document.ToInstrumentBarViewModel(scale, midiMappings, patches);
		}

		public InstrumentBarViewModel AddMidiMappingGroup(InstrumentBarViewModel userInput, int midiMappingGroupID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					MidiMappingGroup midiMapping = _midiMappingGroupRepository.Get(midiMappingGroupID);
					entities.midiMappings.Add(midiMapping);
				});
		}

		public InstrumentBarViewModel AddPatch(InstrumentBarViewModel userInput, int patchID)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					Patch patch = _patchRepository.Get(patchID);
					entities.patches.Add(patch);
				});
		}

		public InstrumentBarViewModel MoveMidiMappingGroup(InstrumentBarViewModel userInput, int midiMappingGroupID, int newPosition)
		{
			return ExecuteAction(
				userInput,
				entities =>
				{
					if (newPosition < 0) newPosition = 0;
					if (newPosition > entities.midiMappings.Count - 1) newPosition = entities.midiMappings.Count - 1;

					MidiMappingGroup midiMapping = _midiMappingGroupRepository.Get(midiMappingGroupID);
					entities.midiMappings.Remove(midiMapping);
					entities.midiMappings.Insert(newPosition, midiMapping);
				});
		}

		public InstrumentBarViewModel MoveMidiMappingGroupBackward(InstrumentBarViewModel userInput, int midiMappingGroupID)
		{
			int currentPosition = userInput.MidiMappingGroups.IndexOf(x => x.EntityID == midiMappingGroupID);

			return MoveMidiMappingGroup(userInput, midiMappingGroupID, currentPosition - 1);
		}

		public InstrumentBarViewModel MoveMidiMappingGroupForward(InstrumentBarViewModel userInput, int midiMappingGroupID)
		{
			int currentPosition = userInput.MidiMappingGroups.IndexOf(x => x.EntityID == midiMappingGroupID);

			return MoveMidiMappingGroup(userInput, midiMappingGroupID, currentPosition + 1);
		}

		public InstrumentBarViewModel MovePatch(InstrumentBarViewModel userInput, int patchID, int newPosition)
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

		public InstrumentBarViewModel MovePatchBackward(InstrumentBarViewModel userInput, int patchID)
		{
			int currentPosition = userInput.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(userInput, patchID, currentPosition - 1);
		}

		public InstrumentBarViewModel MovePatchForward(InstrumentBarViewModel userInput, int patchID)
		{
			int currentPosition = userInput.Patches.IndexOf(x => x.EntityID == patchID);

			return MovePatch(userInput, patchID, currentPosition + 1);
		}

		public InstrumentBarViewModel OpenDocument(InstrumentBarViewModel userInput) => Refresh(userInput);

		public InstrumentBarViewModel Play(InstrumentBarViewModel userInput)
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

		public InstrumentBarViewModel PlayPatch(InstrumentBarViewModel userInput, int patchID)
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

		public InstrumentBarViewModel DeleteMidiMappingGroup(InstrumentBarViewModel userInput, int midiMappingGroupID)
		{
			return ExecuteAction(userInput, entities => entities.midiMappings.RemoveFirst(x => x.ID == midiMappingGroupID));
		}

		public InstrumentBarViewModel DeletePatch(InstrumentBarViewModel userInput, int patchID)
		{
			return ExecuteAction(userInput, entities => entities.patches.RemoveFirst(x => x.ID == patchID));
		}

		public InstrumentBarViewModel SetScale(InstrumentBarViewModel userInput, int scaleID)
		{
			return ExecuteAction(
				userInput,
				() =>
				{
					(Document document, _, IList<MidiMappingGroup> midiMappings, IList<Patch> patches) = GetEntity(userInput);

					Scale scale = _scaleRepository.Get(scaleID);

					return (document, scale, midiMappings, patches);
				});
		}

		[Obsolete("Call OpenDocument instead.", true)]
		public override void Show(InstrumentBarViewModel viewModel) => throw new NotSupportedException("Call OpenDocument instead.");
	}
}