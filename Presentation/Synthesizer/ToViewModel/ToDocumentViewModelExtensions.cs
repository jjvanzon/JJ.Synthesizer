using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
	internal static class ToDocumentViewModelExtensions
	{
		public static DocumentViewModel ToViewModel(this Document document, RepositoryWrapper repositories)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			var viewModel = new DocumentViewModel
			{
				ID = document.ID,
				AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
				AudioFileOutputPropertiesDictionary = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
				AutoPatchPopup = ToViewModelHelper.CreateEmptyAutoPatchViewModel(),
				InstrumentBar = document.ToInstrumentBarViewModel(),
				CurveDetailsDictionary = document.GetCurves().Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.Curve.ID),
				DocumentProperties = document.ToPropertiesViewModel(),
				LibrarySelectionPopup = document.ToEmptyLibrarySelectionPopupViewModel(),
				LibraryPropertiesDictionary = document.LowerDocumentReferences.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.DocumentReferenceID),
				MidiMappingGroupDetailsDictionary = document.MidiMappingGroups.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.MidiMappingGroup.ID),
				MidiMappingPropertiesDictionary = document.MidiMappingGroups.SelectMany(x => x.MidiMappings).Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
				NodePropertiesDictionary = document.GetCurves().SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositories.InterpolationTypeRepository)).ToDictionary(x => x.Entity.ID),
				OperatorPropertiesDictionary = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_WitStandardPropertiesView()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForCaches = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCaches(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForCurves = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCurves()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForInletsToDimension = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForInletsToDimension(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForNumbers = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForPatchInlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForPatchOutlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForSamples = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository, repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_WithCollectionRecalculation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithCollectionRecalculation()).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_WithInterpolation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithInterpolation(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
				PatchDetailsDictionary = document.Patches.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.Entity.ID),
				PatchPropertiesDictionary = document.Patches.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
				SampleFileBrowser = ToViewModelHelper.CreateEmptySampleFileBrowserViewModel(),
				SaveChangesPopup = ToViewModelHelper.CreateEmptySaveChangesPopupViewModel(),
				ScalePropertiesDictionary = document.Scales.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
				ToneGridEditDictionary = document.Scales.Select(x => x.ToToneGridEditViewModel()).ToDictionary(x => x.ScaleID),
                TopButtonBar = ToViewModelHelper.CreateTopButtonBarViewModel(documentIsOpen: true),
				UnderlyingPatchLookup = document.ToUnderlyingPatchLookupViewModel(),
				UndoHistory = new Stack<UndoItemViewModelBase>(),
				RedoFuture = new Stack<UndoItemViewModelBase>(),
			};

			var converter = new RecursiveDocumentTreeViewModelFactory();
			viewModel.DocumentTree = converter.ToTreeViewModel(document);

			if (document.AudioOutput != null)
			{
				viewModel.AudioOutputProperties = document.AudioOutput.ToPropertiesViewModel();
			}
			else
			{
				viewModel.AudioOutputProperties = ToViewModelHelper.CreateEmptyAudioOutputPropertiesViewModel();
			}

			return viewModel;
		}

		public static AutoPatchPopupViewModel ToAutoPatchViewModel(
			this Patch patch,
			ISampleRepository sampleRepository,
			ICurveRepository curveRepository,
			IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (patch == null) throw new NullException(() => patch);
			if (sampleRepository == null) throw new NullException(() => sampleRepository);
			if (curveRepository == null) throw new NullException(() => curveRepository);
			if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

			var viewModel = new AutoPatchPopupViewModel
			{
				OperatorPropertiesDictionary = patch.ToOperatorPropertiesViewModelList_WitStandardPropertiesView().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForCaches = patch.ToPropertiesViewModelList_ForCaches(interpolationTypeRepository).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForCurves = patch.ToPropertiesViewModelList_ForCurves().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForInletsToDimension = patch.ToPropertiesViewModelList_ForInletsToDimension(interpolationTypeRepository).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForNumbers = patch.ToPropertiesViewModelList_ForNumbers().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForPatchInlets = patch.ToPropertiesViewModelList_ForPatchInlets().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForPatchOutlets = patch.ToPropertiesViewModelList_ForPatchOutlets().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_ForSamples = patch.ToPropertiesViewModelList_ForSamples(sampleRepository, interpolationTypeRepository).ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_WithCollectionRecalculation = patch.ToPropertiesViewModelList_WithCollectionRecalculation().ToDictionary(x => x.ID),
				OperatorPropertiesDictionary_WithInterpolation = patch.ToPropertiesViewModelList_WithInterpolation(interpolationTypeRepository).ToDictionary(x => x.ID),
				PatchDetails = patch.ToDetailsViewModel(),
				PatchProperties = patch.ToPropertiesViewModel(),
				ValidationMessages = new List<string>()
			};

			viewModel.PatchDetails.CanSave = true;

			return viewModel;
		}
	}
}