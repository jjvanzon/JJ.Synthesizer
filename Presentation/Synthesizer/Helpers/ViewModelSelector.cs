using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions.Aggregates;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable InlineOutVariableDeclaration

namespace JJ.Presentation.Synthesizer.Helpers
{
	internal static class ViewModelSelector
	{
		// AudioFileOutput

		public static AudioFileOutputPropertiesViewModel GetAudioFileOutputPropertiesViewModel(DocumentViewModel documentViewModel, int audioFileOutputID)
		{
			AudioFileOutputPropertiesViewModel viewModel = TryGetAudioFileOutputPropertiesViewModel(documentViewModel, audioFileOutputID);

			if (viewModel == null)
			{
				throw new NotFoundException<AudioFileOutputPropertiesViewModel>(audioFileOutputID);
			}

			return viewModel;
		}

		public static AudioFileOutputPropertiesViewModel TryGetAudioFileOutputPropertiesViewModel(DocumentViewModel documentViewModel, int audioFileOutputID)
		{
			documentViewModel.AudioFileOutputPropertiesDictionary.TryGetValue(audioFileOutputID, out AudioFileOutputPropertiesViewModel viewModel);

			return viewModel;
		}

		// Curve

		public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel documentViewModel, int curveID)
		{
			CurveDetailsViewModel viewModel = TryGetCurveDetailsViewModel(documentViewModel, curveID);

			if (viewModel == null)
			{
				throw new NotFoundException<CurveDetailsViewModel>(curveID);
			}

			return viewModel;
		}

		public static CurveDetailsViewModel TryGetCurveDetailsViewModel(DocumentViewModel documentViewModel, int curveID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out CurveDetailsViewModel viewModel);

			return viewModel;
		}

		// Library

		public static LibraryPropertiesViewModel GetLibraryPropertiesViewModel(DocumentViewModel documentViewModel, int documentReferenceID)
		{
			LibraryPropertiesViewModel propertiesViewModel = TryGetLibraryPropertiesViewModel(documentViewModel, documentReferenceID);

			if (propertiesViewModel == null)
			{
				throw new NotFoundException<LibraryPropertiesViewModel>(documentReferenceID);
			}

			return propertiesViewModel;
		}

		public static LibraryPropertiesViewModel TryGetLibraryPropertiesViewModel(DocumentViewModel documentViewModel, int documentReferenceID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.LibraryPropertiesDictionary.TryGetValue(documentReferenceID, out LibraryPropertiesViewModel viewModel);

			return viewModel;
		}

		// MidiMapping

		public static MidiMappingDetailsViewModel GetMidiMappingDetailsViewModel(DocumentViewModel documentViewModel, int id)
		{
			MidiMappingDetailsViewModel viewModel = TryGetMidiMappingDetailsViewModel(documentViewModel, id);

			if (viewModel == null)
			{
				throw new NotFoundException<MidiMappingDetailsViewModel>(id);
			}

			return viewModel;
		}

		public static MidiMappingDetailsViewModel TryGetMidiMappingDetailsViewModel(DocumentViewModel documentViewModel, int id)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.MidiMappingDetailsDictionary.TryGetValue(id, out MidiMappingDetailsViewModel viewModel);

			return viewModel;
		}

		public static MidiMappingElementPropertiesViewModel GetMidiMappingElementPropertiesViewModel(DocumentViewModel documentViewModel, int id)
		{
			MidiMappingElementPropertiesViewModel viewModel = TryGetMidiMappingElementPropertiesViewModel(documentViewModel, id);

			if (viewModel == null)
			{
				throw new NotFoundException<MidiMappingElementPropertiesViewModel>(id);
			}

			return viewModel;
		}

		public static MidiMappingElementPropertiesViewModel TryGetMidiMappingElementPropertiesViewModel(DocumentViewModel documentViewModel, int id)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.MidiMappingElementPropertiesDictionary.TryGetValue(id, out MidiMappingElementPropertiesViewModel viewModel);

			return viewModel;
		}

		public static IEnumerable<MidiMappingElementPropertiesViewModel> EnumerateMidiMappingElementPropertiesViewModel_ByMidiMappingID(DocumentViewModel documentViewModel, int midiMappingID)
		{
			if (documentViewModel == null) throw new ArgumentNullException(nameof(documentViewModel));

			foreach (MidiMappingElementPropertiesViewModel mappingElementPropertiesViewModel in documentViewModel.MidiMappingElementPropertiesDictionary.Values)
			{
				if (mappingElementPropertiesViewModel.MidiMappingID == midiMappingID)
				{
					yield return mappingElementPropertiesViewModel;
				}
			}
		}

		// Node

		public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel documentViewModel, int nodeID)
		{
			NodePropertiesViewModel viewModel = TryGetNodePropertiesViewModel(documentViewModel, nodeID);

			if (viewModel == null)
			{
				throw new NotFoundException<NodePropertiesViewModel>(nodeID);
			}

			return viewModel;
		}

		public static NodePropertiesViewModel TryGetNodePropertiesViewModel(DocumentViewModel documentViewModel, int nodeID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.NodePropertiesDictionary.TryGetValue(nodeID, out NodePropertiesViewModel viewModel);

			return viewModel;
		}

		public static Dictionary<int, NodePropertiesViewModel> GetNodePropertiesViewModelDictionary_ByCurveID(DocumentViewModel documentViewModel, int curveID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			if (documentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
			{
				return documentViewModel.NodePropertiesDictionary;
			}

			throw new NotFoundException<Dictionary<int, NodePropertiesViewModel>>(new { curveID });
		}

		// Operator

		public static IEnumerable<OperatorPropertiesViewModelBase> EnumerateAllOperatorPropertiesViewModels(DocumentViewModel documentViewModel)
		{
			if (documentViewModel == null) throw new ArgumentNullException(nameof(documentViewModel));

			return documentViewModel.OperatorPropertiesDictionary.Values.Cast<OperatorPropertiesViewModelBase>()
									.Union(documentViewModel.OperatorPropertiesDictionary_ForCaches.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForCurves.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForInletsToDimension.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForNumbers.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForPatchInlets.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_ForSamples.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_WithInterpolation.Values)
									.Union(documentViewModel.OperatorPropertiesDictionary_WithCollectionRecalculation.Values);
		}

		public static OperatorViewModel GetOperatorViewModel(DocumentViewModel documentViewModel, int patchID, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			PatchDetailsViewModel patchDetailsViewModel = GetPatchDetailsViewModel(documentViewModel, patchID);

			if (patchDetailsViewModel.Entity.OperatorDictionary.TryGetValue(operatorID, out OperatorViewModel operatorViewModel))
			{
				return operatorViewModel;
			}

			throw new NotFoundException<OperatorViewModel>(new { patchID, operatorID });
		}

		public static OperatorPropertiesViewModelBase GetOperatorPropertiesViewModelPolymorphic(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModelBase viewModel = TryGetOperatorPropertiesViewModelPolymorphic(documentViewModel, operatorID);

			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModelBase>(new { operatorID });
			}

			return viewModel;
		}

		public static OperatorPropertiesViewModelBase TryGetOperatorPropertiesViewModelPolymorphic(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModelBase viewModel =
				TryGetOperatorPropertiesViewModel(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForCache(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForInletsToDimension(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForNumber(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForPatchInlet(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForPatchOutlet(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_ForSample(documentViewModel, operatorID) ??
				TryGetOperatorPropertiesViewModel_WithInterpolation(documentViewModel, operatorID) ??
				(OperatorPropertiesViewModelBase)TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(documentViewModel, operatorID);

			return viewModel;

		}

		public static OperatorPropertiesViewModel GetOperatorPropertiesViewModel(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel viewModel = TryGetOperatorPropertiesViewModel(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel viewModel;

			if (documentViewModel.OperatorPropertiesDictionary.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForCache GetOperatorPropertiesViewModel_ForCache(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForCache viewModel = TryGetOperatorPropertiesViewModel_ForCache(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForCache>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForCache TryGetOperatorPropertiesViewModel_ForCache(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForCache viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForCaches.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCaches.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForCurve GetOperatorPropertiesViewModel_ForCurve_ByOperatorID(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForCurve viewModel = TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForCurve>(new {operatorID});
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForCurve viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForCurves.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCurves.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForCurve GetOperatorPropertiesViewModel_ForCurve_ByCurveID(DocumentViewModel documentViewModel, int curveID)
		{
			OperatorPropertiesViewModel_ForCurve viewModel = TryGetOperatorPropertiesViewModel_ForCurve_ByCurveID(documentViewModel, curveID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForCurve>(new {curveID});
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve_ByCurveID(DocumentViewModel documentViewModel, int curveID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			IEnumerable<OperatorPropertiesViewModel_ForCurve> viewModelList = Enumerable.Union(
				documentViewModel.OperatorPropertiesDictionary_ForCurves.Select(x => x.Value),
				documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCurves.Select(x => x.Value));

			OperatorPropertiesViewModel_ForCurve viewModel = viewModelList.FirstOrDefault(x => x.CurveID == curveID);

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForInletsToDimension GetOperatorPropertiesViewModel_ForInletsToDimension(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForInletsToDimension viewModel = TryGetOperatorPropertiesViewModel_ForInletsToDimension(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForInletsToDimension>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForInletsToDimension TryGetOperatorPropertiesViewModel_ForInletsToDimension(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForInletsToDimension viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForInletsToDimension.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForInletsToDimension.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForNumber GetOperatorPropertiesViewModel_ForNumber(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForNumber viewModel = TryGetOperatorPropertiesViewModel_ForNumber(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForNumber>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForNumber viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForNumbers.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForNumbers.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForPatchInlet GetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForPatchInlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchInlet(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForPatchInlet>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForPatchInlet viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForPatchInlets.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchInlets.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForPatchOutlet GetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForPatchOutlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchOutlet(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForPatchOutlet>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForPatchOutlet viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchOutlets.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_ForSample GetOperatorPropertiesViewModel_ForSample(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_ForSample viewModel = TryGetOperatorPropertiesViewModel_ForSample(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_ForSample>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_ForSample viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_ForSamples.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForSamples.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_WithInterpolation GetOperatorPropertiesViewModel_WithInterpolation(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_WithInterpolation viewModel = TryGetOperatorPropertiesViewModel_WithInterpolation(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_WithInterpolation>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_WithInterpolation TryGetOperatorPropertiesViewModel_WithInterpolation(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_WithInterpolation viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_WithInterpolation.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithInterpolation.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		public static OperatorPropertiesViewModel_WithCollectionRecalculation GetOperatorPropertiesViewModel_WithCollectionRecalculation(DocumentViewModel documentViewModel, int operatorID)
		{
			OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(documentViewModel, operatorID);
			if (viewModel == null)
			{
				throw new NotFoundException<OperatorPropertiesViewModel_WithCollectionRecalculation>(operatorID);
			}
			return viewModel;
		}

		public static OperatorPropertiesViewModel_WithCollectionRecalculation TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(DocumentViewModel documentViewModel, int operatorID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			OperatorPropertiesViewModel_WithCollectionRecalculation viewModel;

			if (documentViewModel.OperatorPropertiesDictionary_WithCollectionRecalculation.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			if (documentViewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithCollectionRecalculation.TryGetValue(operatorID, out viewModel))
			{
				return viewModel;
			}

			return null;
		}

		// Patch

		public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel documentViewModel, int patchID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			PatchDetailsViewModel viewModel = TryGetPatchDetailsViewModel(documentViewModel, patchID);

			if (viewModel == null)
			{
				throw new NotFoundException<PatchDetailsViewModel>(new { patchID });
			}

			return viewModel;
		}

		public static PatchDetailsViewModel TryGetPatchDetailsViewModel(DocumentViewModel documentViewModel, int patchID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			if (documentViewModel.PatchDetailsDictionary.TryGetValue(patchID, out PatchDetailsViewModel viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.PatchDetails.Entity.ID == patchID)
			{
				return documentViewModel.AutoPatchPopup.PatchDetails;
			}

			return null;
		}

		public static PatchPropertiesViewModel GetPatchPropertiesViewModel(DocumentViewModel documentViewModel, int patchID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			PatchPropertiesViewModel viewModel = TryGetPatchPropertiesViewModel(documentViewModel, patchID);

			if (viewModel == null)
			{
				throw new NotFoundException<PatchPropertiesViewModel>(new { patchID });
			}

			return viewModel;	   
		}

		public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel(DocumentViewModel documentViewModel, int patchID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			if (documentViewModel.PatchPropertiesDictionary.TryGetValue(patchID, out PatchPropertiesViewModel viewModel))
			{
				return viewModel;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (documentViewModel.AutoPatchPopup.PatchProperties.ID == patchID)
			{
				return documentViewModel.AutoPatchPopup.PatchProperties;
			}

			return null;
		}

		// Scale

		public static ScalePropertiesViewModel GetScalePropertiesViewModel(DocumentViewModel documentViewModel, int scaleID)
		{
			ScalePropertiesViewModel viewModel = TryGetScalePropertiesViewModel(documentViewModel, scaleID);

			if (viewModel == null)
			{
				throw new NotFoundException<ScalePropertiesViewModel>(scaleID);
			}

			return viewModel;
		}

		public static ScalePropertiesViewModel TryGetScalePropertiesViewModel(DocumentViewModel documentViewModel, int scaleID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.ScalePropertiesDictionary.TryGetValue(scaleID, out ScalePropertiesViewModel viewModel);

			return viewModel;
		}

		// Tone

		public static ToneGridEditViewModel GetToneGridEditViewModel(DocumentViewModel documentViewModel, int scaleID)
		{
			ToneGridEditViewModel viewModel = TryGetToneGridEditViewModel(documentViewModel, scaleID);

			if (viewModel == null)
			{
				throw new NotFoundException<ToneGridEditViewModel>(new { scaleID });
			}

			return viewModel;
		}

		public static ToneGridEditViewModel TryGetToneGridEditViewModel(DocumentViewModel documentViewModel, int scaleID)
		{
			if (documentViewModel == null) throw new NullException(() => documentViewModel);

			documentViewModel.ToneGridEditDictionary.TryGetValue(scaleID, out ToneGridEditViewModel viewModel);

			return viewModel;
		}
	}
}