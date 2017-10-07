using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
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
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        // AudioFileOutput

        public static AudioFileOutput ToEntity(this AudioFileOutputPropertiesViewModel viewModel, AudioFileOutputRepositories audioFileOutputRepositories)
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

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var audioFileOutputManager = new AudioFileOutputManager(repositories);

            IList<int> existingIDs = destDocument.AudioFileOutputs.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                audioFileOutputManager.Delete(idToDelete);
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

            foreach (OperatorPropertiesViewModel_ForInletsToDimension propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForInletsToDimension.Values)
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

            foreach (OperatorPropertiesViewModel_WithInterpolation propertiesViewModel in viewModel.OperatorPropertiesDictionary_WithInterpolation.Values)
            {
                propertiesViewModel.ToEntity(repositories);
            }

            foreach (OperatorPropertiesViewModel_WithCollectionRecalculation propertiesViewModel in viewModel
                .OperatorPropertiesDictionary_WithCollectionRecalculation.Values)
            {
                propertiesViewModel.ToEntity(repositories);
            }

            return patch;
        }

        // Curve

        public static void ToEntitiesWithNodes(
            this IEnumerable<CurveDetailsViewModel> viewModelList, 
            Document destDocument, 
            CurveRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (CurveDetailsViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.ToEntityWithNodes(repositories);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var curveManager = new CurveManager(repositories);

            IList<int> existingIDs = destDocument.Curves.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                IResult result = curveManager.DeleteWithRelatedEntities(idToDelete);
                result.Assert();
            }
        }

        public static void ToEntities(
            this IEnumerable<CurvePropertiesViewModel> viewModelList, 
            Document destDocument, 
            CurveRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (CurvePropertiesViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.ToEntity(repositories.CurveRepository);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var curveManager = new CurveManager(repositories);

            IList<int> existingIDs = destDocument.Curves.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                IResult result = curveManager.DeleteWithRelatedEntities(idToDelete);
                result.Assert();
            }
        }

        public static Curve ToEntity(
            this CurvePropertiesViewModel viewModel, 
            ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            Curve entity = curveRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Curve { ID = viewModel.ID };
                curveRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;

            return entity;
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

            // ReSharper disable once RedundantAssignment
            Document destDocument = repositories.DocumentRepository.TryGetComplete(viewModel.ID); // Eager loading
            destDocument = viewModel.ToEntity(repositories.DocumentRepository);
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

            foreach (OperatorPropertiesViewModel_ForInletsToDimension propertiesViewModel in viewModel.OperatorPropertiesDictionary_ForInletsToDimension.Values)
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

            foreach (OperatorPropertiesViewModel_WithInterpolation propertiesViewModel in viewModel.OperatorPropertiesDictionary_WithInterpolation.Values)
            {
                propertiesViewModel.ToEntity(repositories);
            }

            foreach (OperatorPropertiesViewModel_WithCollectionRecalculation propertiesViewModel in viewModel.OperatorPropertiesDictionary_WithCollectionRecalculation.Values)
            {
                propertiesViewModel.ToEntity(repositories);
            }

            viewModel.AudioFileOutputPropertiesDictionary.Values.ToEntities(destDocument, new AudioFileOutputRepositories(repositories));
            viewModel.AudioOutputProperties.ToEntity(repositories.AudioOutputRepository, repositories.SpeakerSetupRepository);
            viewModel.CurvePropertiesDictionary.Values.ToEntities(destDocument, curveRepositories);
            // Order-Dependence: NodeProperties are leading over the CurveDetails Nodes.
            viewModel.CurveDetailsDictionary.Values.ToEntitiesWithNodes(destDocument, curveRepositories);
            // TODO: Low priority: It is not tidy to not have a plural variation that also does the delete operations,
            // even though the CurveDetailsList ToEntity already covers deletion.
            viewModel.NodePropertiesDictionary.Values.ForEach(x => x.ToEntity(repositories.NodeRepository, repositories.NodeTypeRepository));
            viewModel.LibraryPropertiesDictionary.Values.ToEntities(destDocument, repositories);
            viewModel.SamplePropertiesDictionary.Values.ToEntities(destDocument, new SampleRepositories(repositories));
            viewModel.ScalePropertiesDictionary.Values.ToEntities(scaleRepositories, destDocument);
            viewModel.ToneGridEditDictionary.Values.ForEach(x => x.ToEntityWithRelatedEntities(scaleRepositories));

            viewModel.AutoPatchPopup?.ToEntityWithRelatedEntities(repositories);

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

            // NOTE: AudioOutput must be created first and then Document, or you get a FK constraint violation.

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

        public static EntityPosition ToEntityPosition(this OperatorViewModel viewModel, IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            EntityPosition entityPosition = entityPositionRepository.TryGet(viewModel.EntityPositionID);
            if (entityPosition == null)
            {
                entityPosition = new EntityPosition { ID = viewModel.EntityPositionID };
                entityPositionRepository.Insert(entityPosition);
            }
            entityPosition.X = viewModel.CenterX;
            entityPosition.Y = viewModel.CenterY;
            entityPosition.EntityTypeName = typeof(Operator).Name;
            entityPosition.EntityID = viewModel.ID;

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

        public static void ToEntities(this IEnumerable<LibraryPropertiesViewModel> viewModels, Document destDocument, RepositoryWrapper repositories)
        {
            if (viewModels == null) throw new NullException(() => viewModels);
            if (destDocument == null) throw new NullException(() => destDocument);

            var idsToKeep = new HashSet<int>();

            foreach (LibraryPropertiesViewModel viewModel in viewModels)
            {
                DocumentReference entity = viewModel.ToEntity(repositories.DocumentReferenceRepository, repositories.DocumentRepository);
                entity.LinkToHigherDocument(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var documentManager = new DocumentManager(repositories);

            IList<int> existingIDs = destDocument.LowerDocumentReferences.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                IResult result = documentManager.DeleteDocumentReference(idToDelete);
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

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var curveManager = new CurveManager(repositories);

            IList<int> existingIDs = destCurve.Nodes.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                curveManager.DeleteNode(idToDelete);
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

        public static Operator ToEntity(
            this OperatorViewModel viewModel, 
            IOperatorRepository operatorRepository)
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

        public static Operator ToEntity(
            this OperatorPropertiesViewModel viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            return entity;
        }

        public static Operator ToEntity(this OperatorPropertiesViewModel_ForCache viewModel, RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            new Cache_OperatorWrapper(entity)
            {
                InterpolationType = (InterpolationTypeEnum) (viewModel.Interpolation?.ID ?? 0),
                SpeakerSetup = (SpeakerSetupEnum) (viewModel.SpeakerSetup?.ID ?? 0)
            };

            return entity;
        }

        public static Operator ToEntity(this OperatorPropertiesViewModel_ForCurve viewModel, RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            var wrapper = new Curve_OperatorWrapper(entity, repositories.CurveRepository);

            bool curveIsFilledIn = viewModel.Curve != null && viewModel.Curve.ID != 0;
            if (curveIsFilledIn)
            {
                wrapper.CurveID = viewModel.Curve.ID;
            }
            else
            {
                wrapper.CurveID = null;
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForInletsToDimension viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            new InletsToDimension_OperatorWrapper(entity)
            {
                InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0)
            };

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForNumber viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

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

        public static Operator ToOperatorWithInlet(
            this OperatorPropertiesViewModel_ForPatchInlet viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Operator op = ConvertToOperator_Base(viewModel, repositories);

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
            var patchManager = new PatchManager(repositories);
            IList<Inlet> inletsToDelete = op.Inlets.Except(inlet).ToArray();
            foreach (Inlet inletToDelete in inletsToDelete)
            {
                patchManager.DeleteInlet(inletToDelete);
            }

            return op;
        }

        public static Operator ToOperatorWithOutlet(
            this OperatorPropertiesViewModel_ForPatchOutlet viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Operator op = ConvertToOperator_Base(viewModel, repositories);

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
            var patchManager = new PatchManager(repositories);
            IList<Outlet> outletsToDelete = op.Outlets.Except(outlet).ToArray();
            foreach (Outlet outletToDelete in outletsToDelete)
            {
                patchManager.DeleteOutlet(outletToDelete);
            }

            return op;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForSample viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            var wrapper = new Sample_OperatorWrapper(entity, repositories.SampleRepository);

            bool sampleIsFilledIn = viewModel.Sample != null && viewModel.Sample.ID != 0;
            if (sampleIsFilledIn)
            {
                wrapper.SampleID = viewModel.Sample.ID;
            }
            else
            {
                wrapper.SampleID = null;
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_WithInterpolation viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            new Interpolate_OperatorWrapper(entity) { InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0) };

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_WithCollectionRecalculation viewModel,
            RepositoryWrapper repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, repositories);

            new OperatorWrapper_WithCollectionRecalculation(entity)
            {
                CollectionRecalculation = (CollectionRecalculationEnum)(viewModel.CollectionRecalculation?.ID ?? 0)
            };

            return entity;
        }

        private static Operator ConvertToOperator_Base(
            OperatorPropertiesViewModelBase viewModel,
            RepositoryWrapper repositories)
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

        public static Patch ToEntity(this PatchPropertiesViewModel viewModel, IPatchRepository patchRepository, IDimensionRepository dimensionRepository)
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
            patch.DefaultCustomDimensionName = viewModel.DefaultCustomDimensionName;

            Dimension dimension = dimensionRepository.TryGet(viewModel.DefaultStandardDimension?.ID ?? 0);
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

        public static Sample ToEntity(this SamplePropertiesViewModel viewModel, SampleRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            return viewModel.Entity.ToEntity(repositories);
        }

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
            entity.IsActive = viewModel.IsActive;
            entity.SamplingRate = viewModel.SamplingRate;
            entity.BytesToSkip = viewModel.BytesToSkip;
            entity.OriginalLocation = viewModel.OriginalLocation;

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

        public static void ToEntities(this IEnumerable<SamplePropertiesViewModel> viewModelList, Document destDocument, SampleRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (SamplePropertiesViewModel viewModel in viewModelList)
            {
                Sample entity = viewModel.ToEntity(repositories);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var sampleManager = new SampleManager(repositories);

            IList<int> existingIDs = destDocument.Samples.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                sampleManager.Delete(idToDelete);
            }
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

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var sampleManager = new ScaleManager(repositories);

            IList<int> existingIDs = destDocument.Scales.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                sampleManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        public static Scale ToEntity(this ScalePropertiesViewModel viewModel, IScaleRepository scaleRepository, IScaleTypeRepository scaleTypeRepository)
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

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var scaleManager = new ScaleManager(repositories);

            IList<int> existingIDs = destScale.Tones.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                scaleManager.DeleteTone(idToDelete);
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

            if (double.TryParse(viewModel.Number, out double number))
            {
                entity.Number = number;
            }

            if (int.TryParse(viewModel.Octave, out int octave))
            {
                entity.Octave = octave;
            }

            return entity;
        }
    }
}