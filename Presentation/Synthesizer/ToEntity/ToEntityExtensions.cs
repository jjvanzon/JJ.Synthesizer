using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Conversion;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable ObjectCreationAsStatement

namespace JJ.Presentation.Synthesizer.ToEntity
{
	internal static class ToEntityExtensions
	{
		// AudioFileOutput

		public static AudioFileOutput ToEntity(
			this AudioFileOutputPropertiesViewModel viewModel,
			AudioFileOutputRepositories audioFileOutputRepositories)
		{
			return viewModel.Entity.ToEntity(audioFileOutputRepositories);
		}

		public static AudioFileOutput ToEntity(
			this AudioFileOutputViewModel viewModel,
			AudioFileOutputRepositories repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			AudioFileOutput entity = repositories.AudioFileOutputRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new AudioFileOutput { ID = viewModel.ID };
				repositories.AudioFileOutputRepository.Insert(entity);
			}

			entity.Name = viewModel.Name;
			entity.Amplifier = viewModel.Amplifier;
			entity.TimeMultiplier = viewModel.TimeMultiplier;
			entity.StartTime = viewModel.StartTime;
			entity.Duration = viewModel.Duration;
			entity.SamplingRate = viewModel.SamplingRate;
			entity.FilePath = viewModel.FilePath;

			var audioFileFormatEnum = (AudioFileFormatEnum)(viewModel.AudioFileFormat?.ID ?? 0);
			entity.SetAudioFileFormatEnum(audioFileFormatEnum, repositories.AudioFileFormatRepository);

			var sampleDataTypeEnum = (SampleDataTypeEnum)(viewModel.SampleDataType?.ID ?? 0);
			entity.SetSampleDataTypeEnum(sampleDataTypeEnum, repositories.SampleDataTypeRepository);

			var speakerSetupEnum = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);
			entity.SetSpeakerSetupEnum(speakerSetupEnum, repositories.SpeakerSetupRepository);

			bool outletIsFilledIn = viewModel.Outlet != null && viewModel.Outlet.ID != 0;
			if (outletIsFilledIn)
			{
				Outlet outlet = repositories.OutletRepository.Get(viewModel.Outlet.ID);
				entity.LinkTo(outlet);
			}
			else
			{
				entity.UnlinkOutlet();
			}

			return entity;
		}

		public static void ToEntities(
			this IEnumerable<AudioFileOutputPropertiesViewModel> viewModelList,
			Document destDocument,
			AudioFileOutputRepositories repositories)
		{
			if (viewModelList == null) throw new NullException(() => viewModelList);
			if (destDocument == null) throw new NullException(() => destDocument);
			if (repositories == null) throw new NullException(() => repositories);

			var idsToKeep = new HashSet<int>();

			foreach (AudioFileOutputPropertiesViewModel viewModel in viewModelList)
			{
				AudioFileOutput entity = viewModel.ToEntity(repositories);
				entity.LinkTo(destDocument);

				idsToKeep.Add(entity.ID);
			}

			var audioFileOutputFacade = new AudioFileOutputFacade(repositories);

			IList<int> existingIDs = destDocument.AudioFileOutputs.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				audioFileOutputFacade.Delete(idToDelete);
			}
		}

		// AudioOutput

		public static AudioOutput ToEntity(
			this AudioOutputPropertiesViewModel viewModel,
			IAudioOutputRepository audioOutputRepository,
			ISpeakerSetupRepository speakerSetupRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			AudioOutput entity = viewModel.Entity.ToEntity(audioOutputRepository, speakerSetupRepository);

			return entity;
		}

		public static AudioOutput ToEntity(
			this AudioOutputViewModel viewModel,
			IAudioOutputRepository audioOutputRepository,
			ISpeakerSetupRepository speakerSetupRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (audioOutputRepository == null) throw new NullException(() => audioOutputRepository);
			if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

			AudioOutput entity = audioOutputRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new AudioOutput { ID = viewModel.ID };
				audioOutputRepository.Insert(entity);
			}

			entity.SamplingRate = viewModel.SamplingRate;
			entity.MaxConcurrentNotes = viewModel.MaxConcurrentNotes;
			entity.DesiredBufferDuration = viewModel.DesiredBufferDuration;

			var speakerSetupEnum = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);
			entity.SetSpeakerSetupEnum(speakerSetupEnum, speakerSetupRepository);

			return entity;
		}

		// AutoPatch

		public static Patch ToEntityWithRelatedEntities(this AutoPatchPopupViewModel viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			var converter = new RecursiveToEntityConverter(repositories);

			Patch patch = converter.ConvertToEntityWithRelatedEntities(viewModel.PatchDetails, viewModel.PatchProperties);

			// Operator Properties
			// (Operators are converted with the PatchDetails view models, 
			//  but data the from property boxes would be leading or missing from PatchDetails.)
			foreach (OperatorPropertiesViewModel propertiesViewModel in viewModel.OperatorPropertiesDictionary.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForCache propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForCaches.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForCurves.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForInletsToDimension propertiesViewModel in viewModel
				                                                                                 .OperatorPropertiesDictionary_ForInletsToDimension
				                                                                                 .Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForNumbers.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForPatchInlets.Values)
			{
				propertiesViewModel.ToOperatorWithInlet(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values)
			{
				propertiesViewModel.ToOperatorWithOutlet(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForSamples.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_WithInterpolation propertiesViewModel in viewModel
				                                                                              .OperatorPropertiesDictionary_WithInterpolation.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_WithCollectionRecalculation propertiesViewModel in viewModel
				                                                                                        .OperatorPropertiesDictionary_WithCollectionRecalculation
				                                                                                        .Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			return patch;
		}

		// Curve

		public static void ToEntitiesWithNodes(
			this IEnumerable<CurveDetailsViewModel> viewModelList,
			IList<Curve> existingEntities,
			CurveRepositories repositories)
		{
			if (viewModelList == null) throw new NullException(() => viewModelList);
			if (existingEntities == null) throw new NullException(() => existingEntities);
			if (repositories == null) throw new NullException(() => repositories);

			var idsToKeep = new HashSet<Curve>();

			foreach (CurveDetailsViewModel viewModel in viewModelList)
			{
				Curve entity = viewModel.ToEntityWithNodes(repositories);
				idsToKeep.Add(entity);
			}

			IEnumerable<Curve> entitiesToDelete = existingEntities.Except(idsToKeep);
			foreach (Curve entityToDelete in entitiesToDelete.ToArray())
			{
				entityToDelete.DeleteRelatedEntities(repositories.NodeRepository);
				repositories.CurveRepository.Delete(entityToDelete);
			}
		}

		public static Curve ToEntityWithNodes(this CurveDetailsViewModel viewModel, CurveRepositories repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Curve curve = repositories.CurveRepository.TryGet(viewModel.Curve.ID);
			if (curve == null)
			{
				curve = new Curve { ID = viewModel.Curve.ID };
				repositories.CurveRepository.Insert(curve);
			}

			viewModel.Nodes.Values.ToEntities(curve, repositories);

			return curve;
		}

		// Document

		public static Document ToEntityWithRelatedEntities(this MainViewModel viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (viewModel.Document == null) throw new NullException(() => viewModel.Document);
			if (repositories == null) throw new NullException(() => repositories);

			return viewModel.Document.ToEntityWithRelatedEntities(repositories);
		}

		public static Document ToEntityWithRelatedEntities(this DocumentViewModel viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			var curveRepositories = new CurveRepositories(repositories);
			var scaleRepositories = new ScaleRepositories(repositories);
			var midiMappingRepositories = new MidiMappingRepositories(repositories);

			repositories.DocumentRepository.TryGetComplete(viewModel.ID); // Eager loading
			Document destDocument = viewModel.ToEntity(repositories.DocumentRepository);
			viewModel.DocumentProperties.ToEntity(repositories.DocumentRepository);

			var converter = new RecursiveToEntityConverter(repositories);
			converter.ConvertToEntitiesWithRelatedEntities(
				viewModel.PatchDetailsDictionary.Values,
				viewModel.PatchPropertiesDictionary.Values,
				destDocument);

			// Operator Properties
			// (Operators are converted with the PatchDetails view models, 
			//  but data the from property boxes would be leading or missing from PatchDetails.)
			foreach (OperatorPropertiesViewModel propertiesViewModel in viewModel.OperatorPropertiesDictionary.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForCache propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForCaches.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForCurves.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForInletsToDimension propertiesViewModel in viewModel
				                                                                                 .OperatorPropertiesDictionary_ForInletsToDimension
				                                                                                 .Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForNumbers.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForPatchInlets.Values)
			{
				propertiesViewModel.ToOperatorWithInlet(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values)
			{
				propertiesViewModel.ToOperatorWithOutlet(repositories);
			}

			foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForSamples.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_WithInterpolation propertiesViewModel in viewModel
				                                                                              .OperatorPropertiesDictionary_WithInterpolation.Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			foreach (OperatorPropertiesViewModel_WithCollectionRecalculation propertiesViewModel in viewModel
				                                                                                        .OperatorPropertiesDictionary_WithCollectionRecalculation
				                                                                                        .Values)
			{
				propertiesViewModel.ToEntity(repositories);
			}

			viewModel.AudioFileOutputPropertiesDictionary.Values.ToEntities(destDocument, new AudioFileOutputRepositories(repositories));
			viewModel.AudioOutputProperties.ToEntity(repositories.AudioOutputRepository, repositories.SpeakerSetupRepository);
			viewModel.LibraryPropertiesDictionary.Values.ToEntities(destDocument, repositories);
			viewModel.ScalePropertiesDictionary.Values.ToEntities(scaleRepositories, destDocument);
			viewModel.ToneGridEditDictionary.Values.ForEach(x => x.ToEntityWithRelatedEntities(scaleRepositories));
			viewModel.AutoPatchPopup?.ToEntityWithRelatedEntities(repositories);

			// Order-Dependence: OperatorPropertiesViewModel_ForCurve should be converted before CurveDetails.
			viewModel.CurveDetailsDictionary.Values.ToEntitiesWithNodes(destDocument.GetCurves(), curveRepositories);
			// Order-Dependence: NodeProperties are leading over the CurveDetails Nodes.
			viewModel.NodePropertiesDictionary.Values.ForEach(x => x.ToEntity(repositories.NodeRepository, repositories.NodeTypeRepository));

			viewModel.MidiMappingGroupDetailsDictionary.Values.ToEntitiesWithRelatedEntities(destDocument, midiMappingRepositories);
			// Order-Dependence: MidiMappingProperties are leading over MidiMappingGroupDetails items.
			viewModel.MidiMappingPropertiesDictionary.Values.ForEach(x => x.ToEntity(midiMappingRepositories));

			return destDocument;
		}

		public static Document ToEntity(this DocumentViewModel viewModel, IDocumentRepository documentRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (documentRepository == null) throw new NullException(() => documentRepository);

			Document document = documentRepository.TryGet(viewModel.ID);
			if (document == null)
			{
				document = new Document { ID = viewModel.ID };
				documentRepository.Insert(document);
			}

			document.Name = viewModel.DocumentProperties.Entity.Name;

			return document;
		}

		public static Document ToEntity(this DocumentPropertiesViewModel viewModel, IDocumentRepository documentRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Document document = viewModel.Entity.ToDocument(documentRepository);

			return document;
		}

		public static Document ToEntityWithAudioOutput(
			this DocumentDetailsViewModel viewModel,
			IDocumentRepository documentRepository,
			IDocumentReferenceRepository documentReferenceRepository,
			IAudioOutputRepository audioOutputRepository,
			IPatchRepository patchRepository,
			ISpeakerSetupRepository speakerSetupRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			// Order-Dependence: AudioOutput must be created first and then Document, or you get a null constraint violation.

			AudioOutput audioOutput = viewModel.AudioOutput.ToEntity(audioOutputRepository, speakerSetupRepository);

			Document document = viewModel.Document.ToDocument(documentRepository);
			document.LinkTo(audioOutput);

			Patch patch = viewModel.Patch.ToPatch(patchRepository);
			patch.LinkTo(document);

			DocumentReference documentReference = viewModel.SystemLibraryProperties.ToEntity(documentReferenceRepository, documentRepository);
			documentReference.LinkToHigherDocument(document);

			return document;
		}

		public static Document ToDocument(this IDAndName idAndName, IDocumentRepository documentRepository)
		{
			if (idAndName == null) throw new NullException(() => idAndName);
			if (documentRepository == null) throw new NullException(() => documentRepository);

			Document entity = documentRepository.TryGet(idAndName.ID);
			if (entity == null)
			{
				entity = new Document { ID = idAndName.ID };
				documentRepository.Insert(entity);
			}

			entity.Name = idAndName.Name;

			return entity;
		}

		// EntityPosition

		public static EntityPosition ToEntity(this PositionViewModel viewModel, IEntityPositionRepository entityPositionRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

			EntityPosition entityPosition = entityPositionRepository.TryGet(viewModel.ID);
			if (entityPosition == null)
			{
				entityPosition = new EntityPosition { ID = viewModel.ID };
				entityPositionRepository.Insert(entityPosition);
			}

			entityPosition.X = viewModel.CenterX;
			entityPosition.Y = viewModel.CenterY;

			return entityPosition;
		}

		// Inlet

		public static Inlet ToEntity(this InletViewModel viewModel, IInletRepository inletRepository, IDimensionRepository dimensionRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (inletRepository == null) throw new NullException(() => inletRepository);
			if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

			Inlet inlet = inletRepository.TryGet(viewModel.ID);
			if (inlet == null)
			{
				inlet = new Inlet { ID = viewModel.ID };
				inletRepository.Insert(inlet);
			}

			inlet.Position = viewModel.Position;
			inlet.Name = viewModel.Name;
			inlet.DefaultValue = viewModel.DefaultValue;
			inlet.IsObsolete = viewModel.IsObsolete;
			inlet.WarnIfEmpty = viewModel.WarnIfEmpty;
			inlet.NameOrDimensionHidden = viewModel.NameOrDimensionHidden;
			inlet.IsRepeating = viewModel.IsRepeating;
			inlet.RepetitionPosition = viewModel.RepetitionPosition;

			var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
			inlet.SetDimensionEnum(dimensionEnum, dimensionRepository);

			return inlet;
		}

		// Library

		public static void ToEntities(
			this IEnumerable<LibraryPropertiesViewModel> viewModels,
			Document destDocument,
			RepositoryWrapper repositories)
		{
			if (viewModels == null) throw new NullException(() => viewModels);
			if (destDocument == null) throw new NullException(() => destDocument);

			var idsToKeep = new HashSet<int>();

			foreach (LibraryPropertiesViewModel viewModel in viewModels)
			{
				DocumentReference entity = viewModel.ToEntity(repositories.DocumentReferenceRepository, repositories.DocumentRepository);
				entity.LinkToHigherDocument(destDocument);

				idsToKeep.Add(entity.ID);
			}

			var documentFacade = new DocumentFacade(repositories);

			IList<int> existingIDs = destDocument.LowerDocumentReferences.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				IResult result = documentFacade.DeleteDocumentReference(idToDelete);
				result.Assert();
			}
		}

		public static DocumentReference ToEntity(
			this LibraryPropertiesViewModel viewModel,
			IDocumentReferenceRepository documentReferenceRepository,
			IDocumentRepository documentRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (documentReferenceRepository == null) throw new NullException(() => documentReferenceRepository);
			if (documentRepository == null) throw new NullException(() => documentRepository);

			DocumentReference entity = documentReferenceRepository.TryGet(viewModel.DocumentReferenceID);
			if (entity == null)
			{
				entity = new DocumentReference { ID = viewModel.DocumentReferenceID };
				documentReferenceRepository.Insert(entity);
			}

			Document lowerDocument = documentRepository.Get(viewModel.LowerDocumentID);
			entity.LinkToLowerDocument(lowerDocument);

			entity.Alias = viewModel.Alias;

			return entity;
		}

		// MidiMapping

		public static void ToEntitiesWithRelatedEntities(
			this IEnumerable<MidiMappingGroupDetailsViewModel> viewModelList,
			Document destDocument,
			MidiMappingRepositories repositories)
		{
			if (viewModelList == null) throw new ArgumentNullException(nameof(viewModelList));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			var idsToKeep = new HashSet<int>();

			foreach (MidiMappingGroupDetailsViewModel viewModel in viewModelList)
			{
				MidiMappingGroup entity = viewModel.ToEntityWithRelatedEntities(repositories);
				entity.LinkTo(destDocument);

				idsToKeep.Add(entity.ID);
			}

			var midiMappingFacade = new MidiMappingFacade(repositories);

			IList<int> existingIDs = destDocument.MidiMappingGroups.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				midiMappingFacade.DeleteMidiMappingGroup(idToDelete);
			}
		}

		public static MidiMappingGroup ToEntityWithRelatedEntities(this MidiMappingGroupDetailsViewModel viewModel, MidiMappingRepositories repositories)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			MidiMappingGroup midiMapping = viewModel.MidiMappingGroup.ToEntity(repositories.MidiMappingGroupRepository);

			viewModel.MidiMappings.Values.ToEntitiesWithRelatedEntities(midiMapping, repositories);

			return midiMapping;
		}

		public static MidiMappingGroup ToEntity(this IDAndName idAndName, IMidiMappingGroupRepository midiMappingRepository)
		{
			if (idAndName == null) throw new ArgumentNullException(nameof(idAndName));
			if (midiMappingRepository == null) throw new ArgumentNullException(nameof(midiMappingRepository));

			MidiMappingGroup entity = midiMappingRepository.TryGet(idAndName.ID);
			if (entity == null)
			{
				entity = new MidiMappingGroup { ID = idAndName.ID };
				midiMappingRepository.Insert(entity);
			}

			entity.Name = idAndName.Name;

			return entity;
		}

		public static void ToEntitiesWithRelatedEntities(
			this IEnumerable<MidiMappingItemViewModel> viewModelList,
			MidiMappingGroup destMidiMappingGroup,
			MidiMappingRepositories repositories)
		{
			if (viewModelList == null) throw new ArgumentNullException(nameof(viewModelList));
			if (destMidiMappingGroup == null) throw new ArgumentNullException(nameof(destMidiMappingGroup));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			var idsToKeep = new HashSet<int>();

			foreach (MidiMappingItemViewModel viewModel in viewModelList)
			{
				MidiMapping entity = viewModel.ToEntityWithRelatedEntities(
					repositories.MidiMappingRepository,
					repositories.EntityPositionRepository);

				entity.LinkTo(destMidiMappingGroup);

				idsToKeep.Add(entity.ID);
			}

			var midiMappingFacade = new MidiMappingFacade(repositories);

			IList<int> existingIDs = destMidiMappingGroup.MidiMappings.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				midiMappingFacade.DeleteMidiMapping(idToDelete);
			}
		}

		public static MidiMapping ToEntityWithRelatedEntities(
			this MidiMappingItemViewModel viewModel,
			IMidiMappingRepository midiMappingElementRepository,
			IEntityPositionRepository entityPositionRepository)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			// Order-Dependenc2: EntityPosition must be created first and then Operator, or you get a null constraint violation.
			EntityPosition entityPosition = viewModel.Position.ToEntity(entityPositionRepository);

			MidiMapping entity = viewModel.ToEntity(midiMappingElementRepository);
			entity.LinkTo(entityPosition);

			return entity;
		}

		public static MidiMapping ToEntity(
			this MidiMappingItemViewModel viewModel,
			IMidiMappingRepository midiMappingElementRepository)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			MidiMapping entity = midiMappingElementRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new MidiMapping { ID = viewModel.ID };
				midiMappingElementRepository.Insert(entity);
			}

			return entity;
		}

		public static MidiMapping ToEntity(this MidiMappingPropertiesViewModel viewModel, MidiMappingRepositories repositories)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			MidiMapping entity = repositories.MidiMappingRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new MidiMapping { ID = viewModel.ID };
				repositories.MidiMappingRepository.Insert(entity);
			}

			entity.MidiControllerCode = viewModel.MidiControllerCode;
			entity.FromMidiControllerValue = viewModel.FromMidiControllerValue;
			entity.TillMidiControllerValue = viewModel.TillMidiControllerValue;
			entity.FromMidiNoteNumber = viewModel.FromMidiNoteNumber;
			entity.TillMidiNoteNumber = viewModel.TillMidiNoteNumber;
			entity.FromMidiVelocity = viewModel.FromMidiVelocity;
			entity.TillMidiVelocity = viewModel.TillMidiVelocity;
			entity.CustomDimensionName = viewModel.CustomDimensionName;
			entity.FromToneNumber = viewModel.FromToneNumber;
			entity.TillToneNumber = viewModel.TillToneNumber;
			entity.IsActive = viewModel.IsActive;
			entity.IsRelative = viewModel.IsRelative;
			entity.MidiMappingGroup = repositories.MidiMappingGroupRepository.Get(viewModel.MidiMappingGroupID);

			if (DoubleParser.TryParse(viewModel.FromDimensionValue, out double? fromDimensionValue))
			{
				entity.FromDimensionValue = fromDimensionValue;
			}

			if (DoubleParser.TryParse(viewModel.TillDimensionValue, out double? tillDimensionValue))
			{
				entity.TillDimensionValue = tillDimensionValue;
			}

			if (DoubleParser.TryParse(viewModel.MinDimensionValue, out double? minDimensionValue))
			{
				entity.MinDimensionValue = minDimensionValue;
			}

			if (DoubleParser.TryParse(viewModel.MaxDimensionValue, out double? maxDimensionValue))
			{
				entity.MaxDimensionValue = maxDimensionValue;
			}

			if (Int32Parser.TryParse(viewModel.FromPosition, out int? fromPosition))
			{
				entity.FromPosition = fromPosition;
			}

			if (Int32Parser.TryParse(viewModel.TillPosition, out int? tillPosition))
			{
				entity.TillPosition = tillPosition;
			}

			bool standardDimensionIsFilledIn = viewModel.StandardDimension != null && viewModel.StandardDimension.ID != 0;
			if (standardDimensionIsFilledIn)
			{
				Dimension dimension = repositories.DimensionRepository.Get(viewModel.StandardDimension.ID);
				entity.LinkTo(dimension);
			}
			else
			{
				entity.UnlinkStandardDimension();
			}

			bool scaleIsFilledIn = viewModel.Scale != null && viewModel.Scale.ID != 0;
			if (scaleIsFilledIn)
			{
				Scale scale = repositories.ScaleRepository.Get(viewModel.Scale.ID);
				entity.LinkTo(scale);
			}
			else
			{
				entity.UnlinkScale();
			}

			return entity;
		}

		// Node

		public static void ToEntities(
			this IEnumerable<NodeViewModel> viewModelList,
			Curve destCurve,
			CurveRepositories repositories)
		{
			if (viewModelList == null) throw new NullException(() => viewModelList);
			if (destCurve == null) throw new NullException(() => destCurve);
			if (repositories == null) throw new NullException(() => repositories);

			var idsToKeep = new HashSet<int>();

			foreach (NodeViewModel viewModel in viewModelList)
			{
				Node entity = viewModel.ToEntity(repositories.NodeRepository, repositories.NodeTypeRepository);
				entity.LinkTo(destCurve);

				idsToKeep.Add(entity.ID);
			}

			var curveFacade = new CurveFacade(repositories);

			IList<int> existingIDs = destCurve.Nodes.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				curveFacade.DeleteNode(idToDelete);
			}
		}

		public static Node ToEntity(this NodeViewModel viewModel, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (nodeRepository == null) throw new NullException(() => nodeRepository);
			if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

			Node entity = nodeRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new Node { ID = viewModel.ID };
				nodeRepository.Insert(entity);
			}

			entity.X = viewModel.X;
			entity.Y = viewModel.Y;

			var nodeTypeEnum = (NodeTypeEnum)(viewModel.NodeType?.ID ?? 0);
			entity.SetNodeTypeEnum(nodeTypeEnum, nodeTypeRepository);

			return entity;
		}

		public static Node ToEntity(this NodePropertiesViewModel viewModel, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Node entity = viewModel.Entity.ToEntity(nodeRepository, nodeTypeRepository);
			return entity;
		}

		// Operator 

		public static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository operatorRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (operatorRepository == null) throw new NullException(() => operatorRepository);

			Operator entity = operatorRepository.TryGet(viewModel.ID);
			// ReSharper disable once InvertIf
			if (entity == null)
			{
				entity = new Operator { ID = viewModel.ID };
				operatorRepository.Insert(entity);
			}

			return entity;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			return entity;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_ForCache viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			new Cache_OperatorWrapper(entity)
			{
				InterpolationType = (InterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0),
				SpeakerSetup = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0)
			};

			return entity;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_ForCurve viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator op = viewModel.ToOperator_Base(repositories);

			Curve curve = viewModel.ToCurve(repositories);

			op.Curve = curve;

			return op;
		}

		private static Curve ToCurve(this OperatorPropertiesViewModel_ForCurve viewModel, RepositoryWrapper repositories)
		{
			Curve curve = repositories.CurveRepository.TryGet(viewModel.CurveID);

			if (curve == null)
			{
				curve = new Curve { ID = viewModel.CurveID };
				repositories.CurveRepository.Insert(curve);
			}

			curve.Name = viewModel.Name;

			return curve;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_ForInletsToDimension viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			new InletsToDimension_OperatorWrapper(entity)
			{
				InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0)
			};

			return entity;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_ForNumber viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			// ReSharper disable once InvertIf
			if (double.TryParse(viewModel.Number, out double number))
			{
				new Number_OperatorWrapper(entity)
				{
					Number = number
				};
			}

			return entity;
		}

		public static Operator ToOperatorWithInlet(this OperatorPropertiesViewModel_ForPatchInlet viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Operator op = viewModel.ToOperator_Base(repositories);

			Inlet inlet = op.Inlets.FirstOrDefault();
			if (inlet == null)
			{
				inlet = new Inlet { ID = repositories.IDRepository.GetID() };
				repositories.InletRepository.Insert(inlet);
			}

			inlet.WarnIfEmpty = viewModel.WarnIfEmpty;
			inlet.NameOrDimensionHidden = viewModel.NameOrDimensionHidden;
			inlet.IsRepeating = viewModel.IsRepeating;

			// Set Default value
			if (!string.IsNullOrEmpty(viewModel.DefaultValue))
			{
				// Tolerance, to make ToEntity not fail, before view model validation goes off.
				if (double.TryParse(viewModel.DefaultValue, out double defaultValue))
				{
					inlet.DefaultValue = defaultValue;
				}
			}
			else
			{
				inlet.DefaultValue = null;
			}

			inlet.Position = viewModel.Position;

			// Set Dimension of Inlet instead.
			var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
			inlet.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);
			op.SetStandardDimensionEnum(DimensionEnum.Undefined, repositories.DimensionRepository);

			// Set Name of Inlet instead.
			inlet.Name = viewModel.Name;
			op.Name = null;

			// Delete excessive inlets.
			var patchFacade = new PatchFacade(repositories);
			IList<Inlet> inletsToDelete = op.Inlets.Except(inlet).ToArray();
			foreach (Inlet inletToDelete in inletsToDelete)
			{
				patchFacade.DeleteInlet(inletToDelete);
			}

			return op;
		}

		public static Operator ToOperatorWithOutlet(this OperatorPropertiesViewModel_ForPatchOutlet viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Operator op = viewModel.ToOperator_Base(repositories);

			Outlet outlet = op.Outlets.FirstOrDefault();
			if (outlet == null)
			{
				outlet = new Outlet { ID = repositories.IDRepository.GetID() };
				repositories.OutletRepository.Insert(outlet);
			}

			outlet.NameOrDimensionHidden = viewModel.NameOrDimensionHidden;
			outlet.Position = viewModel.Position;
			outlet.IsRepeating = viewModel.IsRepeating;

			// Set Dimension of Outlet instead.
			var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
			outlet.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);
			op.SetStandardDimensionEnum(DimensionEnum.Undefined, repositories.DimensionRepository);

			// Set Name of Outlet instead.
			outlet.Name = viewModel.Name;
			op.Name = null;

			// Delete excessive outlets.
			var patchFacade = new PatchFacade(repositories);
			IList<Outlet> outletsToDelete = op.Outlets.Except(outlet).ToArray();
			foreach (Outlet outletToDelete in outletsToDelete)
			{
				patchFacade.DeleteOutlet(outletToDelete);
			}

			return op;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_ForSample viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Operator op = viewModel.ToOperator_Base(repositories);

			Sample sample = viewModel.Sample.ToEntity(new SampleRepositories(repositories));
			op.LinkTo(sample);

			return op;
		}

		public static Operator ToEntity(this OperatorPropertiesViewModel_WithInterpolation viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			new Interpolate_OperatorWrapper(entity) { InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0) };

			return entity;
		}

		public static Operator ToEntity(
			this OperatorPropertiesViewModel_WithCollectionRecalculation viewModel,
			RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);

			Operator entity = viewModel.ToOperator_Base(repositories);

			new OperatorWrapper_WithCollectionRecalculation(entity)
			{
				CollectionRecalculation = (CollectionRecalculationEnum)(viewModel.CollectionRecalculation?.ID ?? 0)
			};

			return entity;
		}

		private static Operator ToOperator_Base(this OperatorPropertiesViewModelBase viewModel, RepositoryWrapper repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Operator entity = repositories.OperatorRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new Operator { ID = viewModel.ID };
				repositories.OperatorRepository.Insert(entity);
			}

			entity.Name = viewModel.Name;
			entity.CustomDimensionName = viewModel.CustomDimensionName;
			entity.HasDimension = viewModel.HasDimension;

			var standardDimensionEnum = (DimensionEnum)(viewModel.StandardDimension?.ID ?? 0);
			entity.SetStandardDimensionEnum(standardDimensionEnum, repositories.DimensionRepository);

			bool underlyingPatchIsFilledIn = viewModel.UnderlyingPatch != null && viewModel.UnderlyingPatch.ID != 0;
			if (underlyingPatchIsFilledIn)
			{
				Patch underlyingPatch = repositories.PatchRepository.Get(viewModel.UnderlyingPatch.ID);
				entity.LinkToUnderlyingPatch(underlyingPatch);
			}
			else
			{
				entity.UnlinkUnderlyingPatch();
			}

			return entity;
		}

		// Outlet 

		public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository outletRepository, IDimensionRepository dimensionRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (outletRepository == null) throw new NullException(() => outletRepository);
			if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

			Outlet outlet = outletRepository.TryGet(viewModel.ID);
			if (outlet == null)
			{
				outlet = new Outlet { ID = viewModel.ID };
				outletRepository.Insert(outlet);
			}

			outlet.Position = viewModel.Position;
			outlet.Name = viewModel.Name;
			outlet.IsObsolete = viewModel.IsObsolete;
			outlet.NameOrDimensionHidden = viewModel.NameOrDimensionHidden;
			outlet.IsRepeating = viewModel.IsRepeating;
			outlet.RepetitionPosition = viewModel.RepetitionPosition;

			var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
			outlet.SetDimensionEnum(dimensionEnum, dimensionRepository);

			return outlet;
		}

		// Patch

		public static Patch ToEntity(this PatchViewModel viewModel, IPatchRepository patchRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (patchRepository == null) throw new NullException(() => patchRepository);

			Patch entity = patchRepository.TryGet(viewModel.ID);
			// ReSharper disable once InvertIf
			if (entity == null)
			{
				entity = new Patch { ID = viewModel.ID };
				patchRepository.Insert(entity);
			}

			return entity;
		}

		public static Patch ToEntity(
			this PatchPropertiesViewModel viewModel,
			IPatchRepository patchRepository,
			IDimensionRepository dimensionRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (patchRepository == null) throw new NullException(() => patchRepository);

			Patch patch = patchRepository.TryGet(viewModel.ID);
			if (patch == null)
			{
				patch = new Patch { ID = viewModel.ID };
				patchRepository.Insert(patch);
			}

			patch.Name = viewModel.Name;
			patch.GroupName = viewModel.Group;
			patch.Hidden = viewModel.Hidden;
			patch.HasDimension = viewModel.HasDimension;
			patch.CustomDimensionName = viewModel.CustomDimensionName;

			Dimension dimension = dimensionRepository.TryGet(viewModel.StandardDimension?.ID ?? 0);
			patch.LinkTo(dimension);

			return patch;
		}

		public static Patch ToPatch(this IDAndName idAndName, IPatchRepository patchRepository)
		{
			if (idAndName == null) throw new NullException(() => idAndName);
			if (patchRepository == null) throw new NullException(() => patchRepository);

			Patch entity = patchRepository.TryGet(idAndName.ID);
			if (entity == null)
			{
				entity = patchRepository.Create();
				entity.ID = idAndName.ID;
			}

			entity.Name = idAndName.Name;

			return entity;
		}

		// Sample

		public static Sample ToEntity(this SampleViewModel viewModel, SampleRepositories repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Sample entity = repositories.SampleRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new Sample { ID = viewModel.ID };
				repositories.SampleRepository.Insert(entity);
			}

			entity.Name = viewModel.Name;
			entity.Amplifier = viewModel.Amplifier;
			entity.TimeMultiplier = viewModel.TimeMultiplier;
			entity.SamplingRate = viewModel.SamplingRate;
			entity.BytesToSkip = viewModel.BytesToSkip;

			var audioFileFormatEnum = (AudioFileFormatEnum)(viewModel.AudioFileFormat?.ID ?? 0);
			entity.SetAudioFileFormatEnum(audioFileFormatEnum, repositories.AudioFileFormatRepository);

			var interpolationTypeEnum = (InterpolationTypeEnum)(viewModel.InterpolationType?.ID ?? 0);
			entity.SetInterpolationTypeEnum(interpolationTypeEnum, repositories.InterpolationTypeRepository);

			var sampleDataTypeEnum = (SampleDataTypeEnum)(viewModel.SampleDataType?.ID ?? 0);
			entity.SetSampleDataTypeEnum(sampleDataTypeEnum, repositories.SampleDataTypeRepository);

			var speakerSetupEnum = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);
			entity.SetSpeakerSetupEnum(speakerSetupEnum, repositories.SpeakerSetupRepository);

			repositories.SampleRepository.SetBytes(viewModel.ID, viewModel.Bytes);

			return entity;
		}

		// Scale

		public static Scale ToEntityWithRelatedEntities(this ToneGridEditViewModel viewModel, ScaleRepositories repositories)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (repositories == null) throw new NullException(() => repositories);

			Scale scale = repositories.ScaleRepository.Get(viewModel.ScaleID);
			viewModel.Tones.ToEntities(repositories, scale);

			return scale;
		}

		public static void ToEntities(this IEnumerable<ScalePropertiesViewModel> viewModels, ScaleRepositories repositories, Document destDocument)
		{
			if (viewModels == null) throw new NullException(() => viewModels);
			if (destDocument == null) throw new NullException(() => destDocument);
			if (repositories == null) throw new NullException(() => repositories);

			var idsToKeep = new HashSet<int>();

			foreach (ScalePropertiesViewModel viewModel in viewModels)
			{
				Scale entity = viewModel.ToEntity(repositories.ScaleRepository, repositories.ScaleTypeRepository);
				entity.LinkTo(destDocument);

				idsToKeep.Add(entity.ID);
			}

			var scaleFacade = new ScaleFacade(repositories);

			IList<int> existingIDs = destDocument.Scales.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				IResult result = scaleFacade.DeleteWithRelatedEntities(idToDelete);
				result.Assert();
			}
		}

		public static Scale ToEntity(
			this ScalePropertiesViewModel viewModel,
			IScaleRepository scaleRepository,
			IScaleTypeRepository scaleTypeRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (scaleRepository == null) throw new NullException(() => scaleRepository);
			if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);

			Scale entity = viewModel.Entity.ToEntity(scaleRepository, scaleTypeRepository);
			return entity;
		}

		public static Scale ToEntity(this ScaleViewModel viewModel, IScaleRepository scaleRepository, IScaleTypeRepository scaleTypeRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (scaleRepository == null) throw new NullException(() => scaleRepository);
			if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);

			Scale entity = scaleRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new Scale { ID = viewModel.ID };
				scaleRepository.Insert(entity);
			}

			entity.Name = viewModel.Name;
			entity.BaseFrequency = viewModel.BaseFrequency;

			var scaleTypeEnum = (ScaleTypeEnum)(viewModel.ScaleType?.ID ?? 0);
			entity.SetScaleTypeEnum(scaleTypeEnum, scaleTypeRepository);

			return entity;
		}

		// Tone

		public static void ToEntities(this IList<ToneViewModel> viewModelList, ScaleRepositories repositories, Scale destScale)
		{
			if (viewModelList == null) throw new NullException(() => viewModelList);
			if (destScale == null) throw new NullException(() => destScale);
			if (repositories == null) throw new NullException(() => repositories);

			var idsToKeep = new HashSet<int>();

			foreach (ToneViewModel viewModel in viewModelList)
			{
				Tone entity = viewModel.ToEntity(repositories.ToneRepository);
				entity.LinkTo(destScale);

				idsToKeep.Add(entity.ID);
			}

			var scaleFacade = new ScaleFacade(repositories);

			IList<int> existingIDs = destScale.Tones.Select(x => x.ID).ToArray();
			IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
			foreach (int idToDelete in idsToDelete)
			{
				scaleFacade.DeleteTone(idToDelete);
			}
		}

		public static Tone ToEntity(this ToneViewModel viewModel, IToneRepository toneRepository)
		{
			if (viewModel == null) throw new NullException(() => viewModel);
			if (toneRepository == null) throw new NullException(() => toneRepository);

			Tone entity = toneRepository.TryGet(viewModel.ID);
			if (entity == null)
			{
				entity = new Tone { ID = viewModel.ID };
				toneRepository.Insert(entity);
			}

			if (double.TryParse(viewModel.Value, out double value))
			{
				entity.Value = value;
			}

			if (int.TryParse(viewModel.Octave, out int octave))
			{
				entity.Octave = octave;
			}

			return entity;
		}
	}
}