using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
	internal static class ToScreenViewModelExtensions
	{
		// AudioFileOutput

		public static AudioFileOutputPropertiesViewModel ToPropertiesViewModel(this AudioFileOutput entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			AudioFileOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static AudioFileOutputPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this AudioFileOutput entity)
		{
			var viewModel = new AudioFileOutputPropertiesViewModel
			{
				Entity = entity.ToViewModel(),
				AudioFileFormatLookup = ToViewModelHelper.GetAudioFileFormatLookupViewModel(),
				SampleDataTypeLookup = ToViewModelHelper.GetSampleDataTypeLookupViewModel(),
				SpeakerSetupLookup = ToViewModelHelper.GetSpeakerSetupLookupViewModel(),
				ValidationMessages = new List<string>()
			};

			if (entity.Document == null) throw new NullException(() => entity.Document);

			// TODO: Delegate to something in ViewModelHelper_Lookups.cs?
			IList<Outlet> outlets = entity.Document.Patches
												   .SelectMany(x => x.Operators)
												   .Where(x => x.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
												   .SelectMany(x => x.Outlets)
												   .ToArray();

			// TODO: This will not cut it, because you only see the operator name on screen, not the patch name.
			viewModel.OutletLookup = outlets.Select(x => x.ToIDAndName())
											.OrderBy(x => x.Name)
											.ToArray();
			return viewModel;
		}

		public static AudioFileOutputGridViewModel ToAudioFileOutputGridViewModel(this Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			IList<AudioFileOutput> sortedEntities = document.AudioFileOutputs.OrderBy(x => x.Name).ToList();

			var viewModel = new AudioFileOutputGridViewModel
			{
				List = sortedEntities.ToListItemViewModels(),
				DocumentID = document.ID,
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		// AudioOutput

		public static AudioOutputPropertiesViewModel ToPropertiesViewModel(this AudioOutput entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			AudioOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static AudioOutputPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this AudioOutput entity)
		{
			return new AudioOutputPropertiesViewModel
			{
				Entity = entity.ToViewModel(),
				SpeakerSetupLookup = ToViewModelHelper.GetSpeakerSetupLookupViewModel(),
				ValidationMessages = new List<string>()
			};
		}

		// CurrentInstrument

		public static CurrentInstrumentBarViewModel ToCurrentInstrumentBarViewModel(this Document higherDocument)
		{
			if (higherDocument == null) throw new ArgumentNullException(nameof(higherDocument));

			var viewModel = new CurrentInstrumentBarViewModel
			{
				DocumentID = higherDocument.ID,
				Scale = ToViewModelHelper.CreateEmptyIDAndName(),
				Patches = new List<CurrentInstrumentItemViewModel>(),
				MidiMappingGroups = new List<CurrentInstrumentItemViewModel>(),
				ValidationMessages = new List<string>(),
				Visible = true
			};

			return viewModel;
		}

		public static CurrentInstrumentBarViewModel ToCurrentInstrumentBarViewModel(
			this Document higherDocument,
			Scale scale,
			IList<MidiMappingGroup> midiMappings,
			IList<Patch> patches)
		{
			if (patches == null) throw new ArgumentNullException(nameof(patches));
			if (higherDocument == null) throw new ArgumentNullException(nameof(higherDocument));

			CurrentInstrumentBarViewModel viewModel = higherDocument.ToCurrentInstrumentBarViewModel();
			bool hasPatches = patches.Count != 0;
			viewModel.CanPlay = hasPatches;
			viewModel.CanExpand = hasPatches;

			// Lookup for Aliases (of DocumentReference by Document).
			Dictionary<int, DocumentReference> documentReferenceDictionary = higherDocument.LowerDocumentReferences
																						   .ToDictionary(x => x.LowerDocument.ID);
			int lastPatchIndex = patches.Count - 1;

			viewModel.Patches = patches.Select((x, i) => new CurrentInstrumentItemViewModel
			{
				EntityID = x.ID,
				Name = getName(x),
				CanGoBackward = i != 0,
				CanGoForward = i != lastPatchIndex,
				CanPlay = true,
				CanDelete = true,
				CanExpand = true,
			}).ToList();

			int lastMidiMappingGroupIndex = midiMappings.Count - 1;

			viewModel.MidiMappingGroups = midiMappings.Select((x, i) => new CurrentInstrumentItemViewModel
			{
				EntityID = x.ID,
				Name = x.Name,
				CanGoBackward = i != 0,
				CanGoForward = i != lastMidiMappingGroupIndex,
				CanPlay = false,
				CanDelete = true,
				CanExpand = true
			}).ToList();

			string getName(Patch patch)
			{
				Document lowerDocument = patch.Document;

				// Not using Document Name or Alias
				bool mustHideDocumentAliasOrName = lowerDocument.ID == higherDocument.ID ||
												   lowerDocument.IsSystemDocument();
				if (mustHideDocumentAliasOrName)
				{
					return ResourceFormatter.GetDisplayName(patch);
				}

				// Using Document Name or Alias
				DocumentReference documentReference = documentReferenceDictionary[lowerDocument.ID];
				return $"{documentReference.GetAliasOrName()} | {patch.Name}";
			}

			if (scale != null)
			{
				viewModel.Scale = scale.ToIDAndName();
			}
			else
			{
				viewModel.Scale = ToViewModelHelper.CreateEmptyIDAndName();
			}

			return viewModel;
		}

		// Curve

		public static CurveDetailsViewModel ToDetailsViewModel(this Curve entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			CurveDetailsViewModel viewModel = entity.ToDetailsViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToDetailsViewModel_WithoutOriginalState();

			return viewModel;
		}

		private static CurveDetailsViewModel ToDetailsViewModel_WithoutOriginalState(this Curve entity)
		{

			var viewModel = new CurveDetailsViewModel
			{
				Curve = entity.ToIDAndName(),
				Nodes = entity.Nodes.ToViewModelDictionary(),
				NodeTypeLookup = ToViewModelHelper.GetNodeTypeLookupViewModel(),
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		public static NodePropertiesViewModel ToPropertiesViewModel(this Node entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static NodePropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this Node entity)
		{
			return new NodePropertiesViewModel
			{
				CurveID = entity.Curve?.ID ?? default, // Null after delete action.
				Entity = entity.ToViewModel(),
				ValidationMessages = new List<string>(),
				NodeTypeLookup = ToViewModelHelper.GetNodeTypeLookupViewModel()
			};
		}

		// Document

		public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
		{
			var viewModel = new DocumentDetailsViewModel
			{
				Document = document.ToIDAndName(),
				AudioOutput = document.AudioOutput.ToViewModel(),
				SystemLibraryProperties = document.LowerDocumentReferences.Single().ToPropertiesViewModel(),
				// Single Patch, because this is only used upon creating a new document.
				Patch = document.Patches.Single().ToIDAndName(),
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		public static DocumentPropertiesViewModel ToPropertiesViewModel(this Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			DocumentPropertiesViewModel viewModel = document.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = document.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static DocumentPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this Document document)
		{
			return new DocumentPropertiesViewModel
			{
				Entity = document.ToIDAndName(),
				ValidationMessages = new List<string>()
			};
		}

		public static DocumentDeleteViewModel ToDeleteViewModel(this Document entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var viewModel = new DocumentDeleteViewModel
			{
				ValidationMessages = new List<string>(),
				Document = new IDAndName
				{
					ID = entity.ID,
					Name = entity.Name,
				}
			};

			return viewModel;
		}

		public static DocumentCannotDeleteViewModel ToCannotDeleteViewModel(this Document entity, IList<string> messages)
		{
			if (messages == null) throw new ArgumentNullException(nameof(messages));

			var viewModel = new DocumentCannotDeleteViewModel
			{
				Document = entity.ToIDAndName(),
				ValidationMessages = messages
			};

			return viewModel;
		}

		public static DocumentGridViewModel ToGridViewModel(this IList<Document> entities)
		{
			if (entities == null) throw new ArgumentNullException(nameof(entities));

			var viewModel = new DocumentGridViewModel
			{
				List = entities.Select(x => x.ToIDAndName()).ToList(),
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		// Library

		public static LibraryPropertiesViewModel ToPropertiesViewModel(this DocumentReference documentReference)
		{
			if (documentReference == null) throw new ArgumentNullException(nameof(documentReference));

			LibraryPropertiesViewModel viewModel = documentReference.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = documentReference.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static LibraryPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this DocumentReference documentReference)
		{
			return new LibraryPropertiesViewModel
			{
				DocumentReferenceID = documentReference.ID,
				LowerDocumentID = documentReference.LowerDocument?.ID ?? default, // Null after delete action.
				Name = documentReference.LowerDocument?.Name,
				Alias = documentReference.Alias,
				ValidationMessages = new List<string>()
			};
		}

		public static LibrarySelectionPopupViewModel ToLibrarySelectionPopupViewModel(
			this Document higherDocument, 
			IList<Document> lowerDocumentCandidates)
		{
			if (higherDocument == null) throw new ArgumentNullException(nameof(higherDocument));
			if (lowerDocumentCandidates == null) throw new ArgumentNullException(nameof(lowerDocumentCandidates));

			var viewModel = new LibrarySelectionPopupViewModel
			{
				HigherDocumentID = higherDocument.ID,
				List = ToListItemViewModelExtensions.ToIDAndNameList(lowerDocumentCandidates),
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		/// <summary> The list will be empty, but still the HigherDocumentID property will be filled in. </summary>
		public static LibrarySelectionPopupViewModel ToEmptyLibrarySelectionPopupViewModel(this Document higherDocument)
		{
			if (higherDocument == null) throw new ArgumentNullException(nameof(higherDocument));

			LibrarySelectionPopupViewModel viewModel = ToViewModelHelper.CreateEmptyLibrarySelectionPopupViewModel();
			viewModel.HigherDocumentID = higherDocument.ID;

			return viewModel;
		}

		// MidiMapping

		public static MidiMappingGroupDetailsViewModel ToDetailsViewModel(this MidiMappingGroup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			MidiMappingGroupDetailsViewModel viewModel = entity.ToDetailsViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToDetailsViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static MidiMappingGroupDetailsViewModel ToDetailsViewModel_WithoutOriginalState(this MidiMappingGroup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var viewModel = new MidiMappingGroupDetailsViewModel
			{
				MidiMappingGroup = entity.ToIDAndName(),
				MidiMappings = entity.MidiMappings.Select(x => x.ToItemViewModel()).ToDictionary(x => x.ID),
				ValidationMessages = new List<string>()
			};

			return viewModel;
		}

		public static MidiMappingPropertiesViewModel ToPropertiesViewModel(this MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			MidiMappingPropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();

			return viewModel;
		}

		private static MidiMappingPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var viewModel = new MidiMappingPropertiesViewModel
			{
				ID = entity.ID,
				MidiMappingGroupID = entity.MidiMappingGroup?.ID ?? default, // Null after delete action. 
				MidiControllerCode = entity.MidiControllerCode,

				FromMidiValue = entity.FromMidiValue,
				TillMidiValue = entity.TillMidiValue,

				Name = entity.Name,
				Position = $"{entity.Position}",

				FromDimensionValue = $"{entity.FromDimensionValue}",
				TillDimensionValue = $"{entity.TillDimensionValue}",
				MinDimensionValue = $"{entity.MinDimensionValue}",
				MaxDimensionValue = $"{entity.MaxDimensionValue}",

				IsActive = entity.IsActive,
				IsRelative = entity.IsRelative,
				ValidationMessages = new List<string>(),
				DimensionLookup = ToViewModelHelper.GetDimensionLookupViewModel(),
				MidiMappingTypeLookup = ToViewModelHelper.GetMidiMappingTypeLookupViewModel()
			};

			if (entity.MidiMappingType != null)
			{
				viewModel.MidiMappingType = entity.MidiMappingType.ToIDAndDisplayName();
			}
			else
			{
				viewModel.MidiMappingType = ToViewModelHelper.CreateEmptyIDAndName();
			}

			if (entity.Dimension != null)
			{
				viewModel.Dimension = entity.Dimension.ToIDAndDisplayName();
			}
			else
			{
				viewModel.Dimension = ToViewModelHelper.CreateEmptyIDAndName();
			}

			return viewModel;
		}

		// Operator

		// Lists

		/// <summary> Converts to properties view models, the operators that do not have a specialized properties view. </summary>
		public static IList<OperatorPropertiesViewModel> ToOperatorPropertiesViewModelList_WitStandardPropertiesView(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			var viewModels = new List<OperatorPropertiesViewModel>();

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (Operator op in patch.Operators)
			{
				// ReSharper disable once InvertIf
				if (ToViewModelHelper.OperatorTypeEnums_WithStandardPropertiesView.Contains(op.GetOperatorTypeEnum()))
				{
					OperatorPropertiesViewModel viewModel = op.ToPropertiesViewModel();
					viewModels.Add(viewModel);
				}
			}

			return viewModels;
		}

		public static IList<OperatorPropertiesViewModel_ForCache> ToPropertiesViewModelList_ForCaches(
			this Patch patch,
			IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.Cache)
						.Select(x => x.ToPropertiesViewModel_ForCache(interpolationTypeRepository))
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForCurve> ToPropertiesViewModelList_ForCurves(
			this Patch patch,
			ICurveRepository curveRepository)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.Curve)
						.Select(x => x.ToPropertiesViewModel_ForCurve(curveRepository))
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForInletsToDimension> ToPropertiesViewModelList_ForInletsToDimension(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.InletsToDimension)
						.Select(x => x.ToPropertiesViewModel_ForInletsToDimension())
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForNumber> ToPropertiesViewModelList_ForNumbers(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.Number)
						.Select(x => x.ToPropertiesViewModel_ForNumber())
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForPatchInlet> ToPropertiesViewModelList_ForPatchInlets(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
						.Select(x => x.ToPropertiesViewModel_ForPatchInlet())
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForPatchOutlet> ToPropertiesViewModelList_ForPatchOutlets(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
						.Select(x => x.ToPropertiesViewModel_ForPatchOutlet())
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_ForSample> ToPropertiesViewModelList_ForSamples(
			this Patch patch,
			ISampleRepository sampleRepository,
			IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.GetOperatorsOfType(OperatorTypeEnum.Sample)
						.Select(x => x.ToPropertiesViewModel_ForSample(sampleRepository, interpolationTypeRepository))
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_WithInterpolation> ToPropertiesViewModelList_WithInterpolation(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.Operators
						.Where(x => ToViewModelHelper.OperatorTypeEnums_WithInterpolationPropertyViews.Contains(x.GetOperatorTypeEnum()))
						.Select(x => x.ToPropertiesViewModel_WithInterpolation())
						.ToList();
		}

		public static IList<OperatorPropertiesViewModel_WithCollectionRecalculation> ToPropertiesViewModelList_WithCollectionRecalculation(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			return patch.Operators.Where(x => ToViewModelHelper.OperatorTypeEnums_WithCollectionRecalculationPropertyViews.Contains(x.GetOperatorTypeEnum()))
						.Select(x => x.ToPropertiesViewModel_WithCollectionRecalculation())
						.ToList();
		}

		// Items

		public static OperatorPropertiesViewModel ToPropertiesViewModel(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();

			return viewModel;
		}

		private static OperatorPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel>(entity);

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForCache ToPropertiesViewModel_ForCache(this Operator entity, IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForCache viewModel = entity.ToPropertiesViewModel_ForCache_WithoutOriginalState(interpolationTypeRepository);
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForCache_WithoutOriginalState(interpolationTypeRepository);
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForCache ToPropertiesViewModel_ForCache_WithoutOriginalState(this Operator entity, IInterpolationTypeRepository interpolationTypeRepository)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForCache>(entity);

			var wrapper = new Cache_OperatorWrapper(entity);

			viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
			viewModel.InterpolationLookup = ToViewModelHelper.GetInterpolationTypeLookupViewModel(interpolationTypeRepository);
			viewModel.SpeakerSetup = wrapper.SpeakerSetup.ToIDAndDisplayName();
			viewModel.SpeakerSetupLookup = ToViewModelHelper.GetSpeakerSetupLookupViewModel();

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForCurve ToPropertiesViewModel_ForCurve(this Operator entity, ICurveRepository curveRepository)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForCurve viewModel = entity.ToPropertiesViewModel_ForCurve_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForCurve_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForCurve ToPropertiesViewModel_ForCurve_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForCurve>(entity);

			Curve curve = entity.Curve;
			if (curve != null)
			{
				viewModel.Name = curve.Name;
				viewModel.CurveID = curve.ID;
			}

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForInletsToDimension ToPropertiesViewModel_ForInletsToDimension(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForInletsToDimension viewModel = entity.ToPropertiesViewModel_ForInletsToDimension_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForInletsToDimension_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForInletsToDimension ToPropertiesViewModel_ForInletsToDimension_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForInletsToDimension>(entity);

			var wrapper = new InletsToDimension_OperatorWrapper(entity);

			viewModel.InletCount = entity.Inlets.Count;
			viewModel.CanEditInletCount = true;
			viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
			viewModel.InterpolationLookup = ToViewModelHelper.GetResampleInterpolationLookupViewModel();

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForNumber ToPropertiesViewModel_ForNumber(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForNumber viewModel = entity.ToPropertiesViewModel_ForNumber_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForNumber_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForNumber ToPropertiesViewModel_ForNumber_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForNumber>(entity);

			var wrapper = new Number_OperatorWrapper(entity);

			viewModel.Number = wrapper.Number.ToString();

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForPatchInlet ToPropertiesViewModel_ForPatchInlet(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForPatchInlet viewModel = entity.ToPropertiesViewModel_ForPatchInlet_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForPatchInlet_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForPatchInlet ToPropertiesViewModel_ForPatchInlet_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForPatchInlet>(entity);

			Inlet inlet = new PatchInletOrOutlet_OperatorWrapper(entity).Inlet;

			// Use Inlet.Name instead of Operator.Name.
			viewModel.Name = inlet.Name;
			viewModel.DefaultValue = Convert.ToString(inlet.DefaultValue);
			viewModel.Position = inlet.Position;
			viewModel.WarnIfEmpty = inlet.WarnIfEmpty;
			viewModel.NameOrDimensionHidden = inlet.NameOrDimensionHidden;
			viewModel.IsRepeating = inlet.IsRepeating;

			// In case of PatchInlet Dimension has to come from Inlet, not Operator.
			viewModel.DimensionLookup = ToViewModelHelper.GetDimensionLookupViewModel();
			DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
			if (dimensionEnum != DimensionEnum.Undefined)
			{
				viewModel.Dimension = dimensionEnum.ToIDAndDisplayName();
			}
			else
			{
				viewModel.Dimension = new IDAndName();
			}

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForPatchOutlet ToPropertiesViewModel_ForPatchOutlet(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_ForPatchOutlet viewModel = entity.ToPropertiesViewModel_ForPatchOutlet_WithoutOriginalViewModel();
			viewModel.OriginalState = entity.ToPropertiesViewModel_ForPatchOutlet_WithoutOriginalViewModel();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForPatchOutlet ToPropertiesViewModel_ForPatchOutlet_WithoutOriginalViewModel(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForPatchOutlet>(entity);

			Outlet outlet = new PatchInletOrOutlet_OperatorWrapper(entity).Outlet;

			viewModel.Name = outlet.Name;
			viewModel.Position = outlet.Position;
			viewModel.NameOrDimensionHidden = outlet.NameOrDimensionHidden;
			viewModel.IsRepeating = outlet.IsRepeating;

			// In case of PatchInlet Dimension has to come from Outlet, not Operator.
			viewModel.DimensionLookup = ToViewModelHelper.GetDimensionLookupViewModel();
			DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
			if (dimensionEnum != DimensionEnum.Undefined)
			{
				viewModel.Dimension = dimensionEnum.ToIDAndDisplayName();
			}
			else
			{
				viewModel.Dimension = new IDAndName();
			}

			return viewModel;
		}

		public static OperatorPropertiesViewModel_ForSample ToPropertiesViewModel_ForSample(
			this Operator op,
			ISampleRepository sampleRepository,
			IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));

			OperatorPropertiesViewModel_ForSample viewModel = op.OperatorPropertiesViewModel_ForSample_WithoutOriginalState(sampleRepository, interpolationTypeRepository);
			viewModel.OriginalState = op.OperatorPropertiesViewModel_ForSample_WithoutOriginalState(sampleRepository, interpolationTypeRepository);
			return viewModel;
		}

		private static OperatorPropertiesViewModel_ForSample OperatorPropertiesViewModel_ForSample_WithoutOriginalState(
			this Operator op,
			ISampleRepository sampleRepository,
			IInterpolationTypeRepository interpolationTypeRepository)
		{
			if (sampleRepository == null) throw new ArgumentNullException(nameof(sampleRepository));

			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_ForSample>(op);

			viewModel.AudioFileFormatLookup = ToViewModelHelper.GetAudioFileFormatLookupViewModel();
			viewModel.SampleDataTypeLookup = ToViewModelHelper.GetSampleDataTypeLookupViewModel();
			viewModel.SpeakerSetupLookup = ToViewModelHelper.GetSpeakerSetupLookupViewModel();
			viewModel.InterpolationTypeLookup = ToViewModelHelper.GetInterpolationTypeLookupViewModel(interpolationTypeRepository);

			byte[] bytes = sampleRepository.GetBytes(op.Sample.ID);
			viewModel.Sample = op.Sample.ToViewModel(bytes);

			return viewModel;
		}

		public static OperatorPropertiesViewModel_WithInterpolation ToPropertiesViewModel_WithInterpolation(this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_WithInterpolation viewModel = entity.ToPropertiesViewModel_WithInterpolation_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithInterpolation_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_WithInterpolation ToPropertiesViewModel_WithInterpolation_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_WithInterpolation>(entity);

			var wrapper = new Interpolate_OperatorWrapper(entity);

			viewModel.Interpolation = wrapper.InterpolationType.ToIDAndDisplayName();
			viewModel.InterpolationLookup = ToViewModelHelper.GetResampleInterpolationLookupViewModel();

			return viewModel;
		}

		public static OperatorPropertiesViewModel_WithCollectionRecalculation ToPropertiesViewModel_WithCollectionRecalculation(
			this Operator entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = entity.ToPropertiesViewModel_WithCollectionRecalculation_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithCollectionRecalculation_WithoutOriginalState();
			return viewModel;
		}

		private static OperatorPropertiesViewModel_WithCollectionRecalculation ToPropertiesViewModel_WithCollectionRecalculation_WithoutOriginalState(this Operator entity)
		{
			var viewModel = CreateOperatorPropertiesViewModel_Generic<OperatorPropertiesViewModel_WithCollectionRecalculation>(entity);

			var wrapper = new OperatorWrapper_WithCollectionRecalculation(entity);

			viewModel.CollectionRecalculation = wrapper.CollectionRecalculation.ToIDAndDisplayName();
			viewModel.CollectionRecalculationLookup = ToViewModelHelper.GetCollectionRecalculationLookupViewModel();

			return viewModel;
		}

		private static TViewModel CreateOperatorPropertiesViewModel_Generic<TViewModel>(Operator entity)
			where TViewModel : OperatorPropertiesViewModelBase, new()
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var viewModel = new TViewModel
			{
				ID = entity.ID,
				PatchID = entity.Patch?.ID ?? default, // Null after delete action.
				Name = entity.Name,
				CustomDimensionName = entity.CustomDimensionName,
				HasDimension = entity.HasDimension,
				InletCount = entity.Inlets.Count,
				CanEditInletCount = entity.CanSetInletCount(),
				OutletCount = entity.Outlets.Count,
				CanEditOutletCount = entity.CanSetOutletCount(),
				ValidationMessages = new List<string>()
			};

			DimensionEnum dimensionEnum = entity.GetStandardDimensionEnum();
			if (dimensionEnum != DimensionEnum.Undefined)
			{
				viewModel.StandardDimension = dimensionEnum.ToIDAndDisplayName();
			}
			else
			{
				viewModel.StandardDimension = new IDAndName();
			}

			// ReSharper disable once InvertIf
			if (entity.HasDimension)
			{
				viewModel.CanEditCustomDimensionName = true;
				viewModel.CanSelectStandardDimension = true;
				viewModel.StandardDimensionLookup = ToViewModelHelper.GetDimensionLookupViewModel();
			}

			if (entity.UnderlyingPatch != null)
			{
				viewModel.UnderlyingPatch = entity.UnderlyingPatch.ToIDAndName();
			}

			viewModel.UnderlyingPatch = viewModel.UnderlyingPatch ?? ToViewModelHelper.CreateEmptyIDAndName();

			return viewModel;
		}

		// Patch

		public static PatchDetailsViewModel ToDetailsViewModel(this Patch patch, ICurveRepository curveRepository)
		{
			var converter = new RecursiveToPatchViewModelConverter(curveRepository);

			PatchDetailsViewModel viewModel = converter.ConvertToDetailsViewModel(patch);
			viewModel.OriginalState = converter.ConvertToDetailsViewModel(patch);

			return viewModel;
		}

		public static PatchPropertiesViewModel ToPropertiesViewModel(this Patch patch)
		{
			PatchPropertiesViewModel viewModel = patch.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = patch.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static PatchPropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this Patch patch)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));

			var viewModel = new PatchPropertiesViewModel
			{
				ID = patch.ID,
				Name = patch.Name,
				Group = patch.GroupName,
				Hidden = patch.Hidden,
				HasDimension = patch.HasDimension,
				CustomDimensionNameEnabled = patch.HasDimension,
				CustomDimensionName = patch.CustomDimensionName,
				StandardDimensionEnabled = patch.HasDimension,
				StandardDimensionLookup = ToViewModelHelper.GetDimensionLookupViewModel(),
				ValidationMessages = new List<string>()
			};

			if (patch.StandardDimension != null)
			{
				viewModel.StandardDimension = patch.StandardDimension.ToIDAndDisplayName();
			}
			else
			{
				viewModel.StandardDimension = ToViewModelHelper.CreateEmptyIDAndName();
			}

			return viewModel;
		}

		// Scale

		public static ScalePropertiesViewModel ToPropertiesViewModel(this Scale entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ScalePropertiesViewModel viewModel = entity.ToPropertiesViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToPropertiesViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static ScalePropertiesViewModel ToPropertiesViewModel_WithoutOriginalState(this Scale entity)
		{
			return new ScalePropertiesViewModel
			{
				Entity = entity.ToViewModel(),
				ScaleTypeLookup = ToViewModelHelper.GetScaleTypeLookupViewModel(),
				ValidationMessages = new List<string>()
			};
		}

		// Tone

		public static ToneGridEditViewModel ToToneGridEditViewModel(this Scale entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ToneGridEditViewModel viewModel = entity.ToToneGridEditViewModel_WithoutOriginalState();
			viewModel.OriginalState = entity.ToToneGridEditViewModel_WithoutOriginalState();
			return viewModel;
		}

		private static ToneGridEditViewModel ToToneGridEditViewModel_WithoutOriginalState(this Scale entity)
		{
			return new ToneGridEditViewModel
			{
				ScaleID = entity.ID,
				ValueTitle = ToViewModelHelper.GetToneGridEditValueTitle(entity),
				Tones = entity.Tones.ToToneViewModels(),
				FrequencyVisible = entity.GetScaleTypeEnum() != ScaleTypeEnum.LiteralFrequency,
				ValidationMessages = new List<string>()
			};
		}
	}
}