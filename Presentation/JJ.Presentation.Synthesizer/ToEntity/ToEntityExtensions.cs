using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Business.Synthesizer;
using System;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Canonical;

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
                entity = new AudioFileOutput();
                entity.ID = viewModel.ID;
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
                entity = new AudioOutput();
                entity.ID = viewModel.ID;
                audioOutputRepository.Insert(entity);
            }

            entity.SamplingRate = viewModel.SamplingRate;
            entity.MaxConcurrentNotes = viewModel.MaxConcurrentNotes;
            entity.DesiredBufferDuration = viewModel.DesiredBufferDuration;

            var speakerSetupEnum = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);
            entity.SetSpeakerSetupEnum(speakerSetupEnum, speakerSetupRepository);

            return entity;
        }

        // CurrentPatches

        public static IList<Patch> ToEntities(this CurrentPatchesViewModel viewModel, IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            var underlyingPatches = new List<Patch>(viewModel.List.Count);
            foreach (IDAndName itemViewModel in viewModel.List)
            {
                Patch patch = patchRepository.Get(itemViewModel.ID);
                underlyingPatches.Add(patch);
            }

            return underlyingPatches;
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

                ResultHelper.Assert(result);
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

                ResultHelper.Assert(result);
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
                entity = new Curve();
                entity.ID = viewModel.ID;
                curveRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;

            return entity;
        }

        public static Curve ToEntityWithNodes(this CurveDetailsViewModel viewModel, CurveRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Curve curve = repositories.CurveRepository.TryGet(viewModel.CurveID);
            if (curve == null)
            {
                curve = new Curve();
                curve.ID = viewModel.CurveID;
                repositories.CurveRepository.Insert(curve);
            }

            viewModel.Nodes.Values.ToEntities(curve, repositories);

            return curve;
        }

        // Document

        public static Document ToEntityWithRelatedEntities(this MainViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (userInput.Document == null) throw new NullException(() => userInput.Document);
            if (repositories == null) throw new NullException(() => repositories);

            return userInput.Document.ToEntityWithRelatedEntities(repositories);
        }

        public static Document ToEntityWithRelatedEntities(this DocumentViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            var curveRepositories = new CurveRepositories(repositories);
            var patchRepositories = new PatchRepositories(repositories);
            var scaleRepositories = new ScaleRepositories(repositories);

            Document destDocument = repositories.DocumentRepository.TryGetComplete(userInput.ID); // Eager loading
            destDocument = userInput.ToEntity(repositories.DocumentRepository);
            userInput.DocumentProperties.ToEntity(repositories.DocumentRepository);

            ToEntityHelper.ConvertToEntitiesWithRelatedEntities(
                userInput.PatchDetailsDictionary.Values,
                userInput.PatchPropertiesDictionary.Values,
                destDocument,
                patchRepositories);

            // Operator Properties
            // (Operators are converted with the PatchDetails view models, 
            //  but data the from property boxes would be leading or missing from PatchDetails.)
            foreach (OperatorPropertiesViewModel propertiesViewModel in userInput.OperatorPropertiesDictionary.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForBundle propertiesViewModel in userInput.OperatorPropertiesDictionary_ForBundles.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCache propertiesViewModel in userInput.OperatorPropertiesDictionary_ForCaches.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in userInput.OperatorPropertiesDictionary_ForCurves.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.CurveRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in userInput.OperatorPropertiesDictionary_ForCustomOperators.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.PatchRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForMakeContinuous propertiesViewModel in userInput.OperatorPropertiesDictionary_ForMakeContinuous.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in userInput.OperatorPropertiesDictionary_ForNumbers.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in userInput.OperatorPropertiesDictionary_ForPatchInlets.Values)
            {
                propertiesViewModel.ToOperatorWithInlet(patchRepositories);
            }

            foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in userInput.OperatorPropertiesDictionary_ForPatchOutlets.Values)
            {
                propertiesViewModel.ToOperatorWithOutlet(patchRepositories);
            }

            foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in userInput.OperatorPropertiesDictionary_ForSamples.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.SampleRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_WithDimensionAndInterpolation propertiesViewModel in userInput.OperatorPropertiesDictionary_WithDimensionAndInterpolation.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation propertiesViewModel in userInput.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_WithDimensionAndOutletCount propertiesViewModel in userInput.OperatorPropertiesDictionary_WithDimensionAndOutletCount.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            foreach (OperatorPropertiesViewModel_WithInletCount propertiesViewModel in userInput.OperatorPropertiesDictionary_WithInletCount.Values)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);
            }

            userInput.AudioFileOutputPropertiesDictionary.Values.ToEntities(destDocument, new AudioFileOutputRepositories(repositories));
            userInput.AudioOutputProperties.ToEntity(repositories.AudioOutputRepository, repositories.SpeakerSetupRepository);
            userInput.CurvePropertiesDictionary.Values.ToEntities(destDocument, curveRepositories);
            // Order-Dependence: NodeProperties are leading over the CurveDetails Nodes.
            userInput.CurveDetailsDictionary.Values.ToEntitiesWithNodes(destDocument, curveRepositories);
            // TODO: Low priority: It is not tidy to not have a plural variation that also does the delete operations,
            // even though the CurveDetailsList ToEntity already covers deletion.
            userInput.NodePropertiesDictionary.Values.ForEach(x => x.ToEntity(repositories.NodeRepository, repositories.NodeTypeRepository));
            userInput.SamplePropertiesDictionary.Values.ToEntities(destDocument, new SampleRepositories(repositories));
            userInput.ScalePropertiesDictionary.Values.ToEntities(scaleRepositories, destDocument);
            userInput.ToneGridEditDictionary.Values.ForEach(x => x.ToEntityWithRelatedEntities(scaleRepositories));

            return destDocument;
        }

        public static Document ToEntity(this DocumentViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document document = documentRepository.TryGet(viewModel.ID);
            if (document == null)
            {
                document = new Document();
                document.ID = viewModel.ID;
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

            return document;
        }

        public static Document ToDocument(this IDAndName idAndName, IDocumentRepository documentRepository)
        {
            if (idAndName == null) throw new NullException(() => idAndName);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document entity = documentRepository.TryGet(idAndName.ID);
            if (entity == null)
            {
                entity = new Document();
                entity.ID = idAndName.ID;
                documentRepository.Insert(entity);
            }

            entity.Name = idAndName.Name;

            return entity;
        }

        // EntityPosition

        public static EntityPosition ToEntityPosition(this OperatorViewModel viewModel, IEntityPositionRepository entityPositionRepository)
        {
            // TODO: Low priority: Delegate ToEntityPosition to the EntityPositionManager?
            // Because now the ToEntityPosition method needs to know too much about how EntityPosition works. 

            if (viewModel == null) throw new NullException(() => viewModel);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            EntityPosition entityPosition = entityPositionRepository.TryGet(viewModel.EntityPositionID);
            if (entityPosition == null)
            {
                entityPosition = new EntityPosition();
                entityPosition.ID = viewModel.EntityPositionID;
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

            Inlet entity = inletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Inlet();
                entity.ID = viewModel.ID;
                inletRepository.Insert(entity);
            }
            entity.ListIndex = viewModel.ListIndex;
            entity.Name = viewModel.Name;
            entity.DefaultValue = viewModel.DefaultValue;

            DimensionEnum dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
            entity.SetDimensionEnum(dimensionEnum, dimensionRepository);

            return entity;
        }

        // Nodes

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
                entity = new Node();
                entity.ID = viewModel.ID;
                nodeRepository.Insert(entity);
            }
            entity.X = viewModel.X;
            entity.Y = viewModel.Y;

            NodeTypeEnum nodeTypeEnum = (NodeTypeEnum)(viewModel.NodeType?.ID ?? 0);
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
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel viewModel,
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForBundle viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForCache viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new Cache_OperatorWrapper(entity);
            wrapper.InterpolationType = (InterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0);
            wrapper.SpeakerSetup = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForCurve viewModel,
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository, 
            ICurveRepository curveRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new Curve_OperatorWrapper(entity, curveRepository);

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
            this OperatorPropertiesViewModel_ForCustomOperator viewModel,
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository, 
            IPatchRepository patchRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new CustomOperator_OperatorWrapper(entity, patchRepository);

            bool underlyingPatchIsFilledIn = viewModel.UnderlyingPatch != null && viewModel.UnderlyingPatch.ID != 0;
            if (underlyingPatchIsFilledIn)
            {
                wrapper.UnderlyingPatchID = viewModel.UnderlyingPatch.ID;
            }
            else
            {
                wrapper.UnderlyingPatchID = null;
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForMakeContinuous viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new MakeContinuous_OperatorWrapper(entity);
            wrapper.InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForNumber viewModel,
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            // TODO: Low Priority: ViewModel validator to detect, that it is not a valid integer number.
            var wrapper = new Number_OperatorWrapper(entity);
            double number;
            if (Double.TryParse(viewModel.Number, out number))
            {
                wrapper.Number = number;
            }

            return entity;
        }

        public static Operator ToOperatorWithInlet(
            this OperatorPropertiesViewModel_ForPatchInlet viewModel,
            PatchRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Operator op = ConvertToOperator_Base(viewModel, repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);

            var wrapper = new PatchInlet_OperatorWrapper(op);
            wrapper.ListIndex = viewModel.Number - 1;

            Inlet inlet = op.Inlets.FirstOrDefault();
            if (inlet == null)
            {
                inlet = new Inlet();
                inlet.ID = repositories.IDRepository.GetID();
                repositories.InletRepository.Insert(inlet);
            }

            if (!String.IsNullOrEmpty(viewModel.DefaultValue))
            {
                // Tolerance, to make ToEntity not fail, before view model validation goes off.
                double defaultValue;
                if (Double.TryParse(viewModel.DefaultValue, out defaultValue))
                {
                    inlet.DefaultValue = defaultValue;
                }
            }
            else
            {
                inlet.DefaultValue = null;
            }

            // HACK: Clear dimension of operator.
            op.SetDimensionEnum(DimensionEnum.Undefined, repositories.DimensionRepository);
            op.CustomDimensionName = null;
            // Set dimension of inlet.
            var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
            inlet.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);

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
            PatchRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Operator op = ConvertToOperator_Base(viewModel, repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DimensionRepository);

            var wrapper = new PatchOutlet_OperatorWrapper(op);
            wrapper.ListIndex = viewModel.Number - 1;

            Outlet outlet = op.Outlets.FirstOrDefault();
            if (outlet == null)
            {
                outlet = new Outlet();
                outlet.ID = repositories.IDRepository.GetID();
                repositories.OutletRepository.Insert(outlet);
            }

            // HACK: Clear dimension of operator.
            op.SetDimensionEnum(DimensionEnum.Undefined, repositories.DimensionRepository);
            op.CustomDimensionName = null;
            // Set dimension of inlet.
            var dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
            outlet.SetDimensionEnum(dimensionEnum, repositories.DimensionRepository);

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
            IOperatorRepository operatorRepository, 
            IOperatorTypeRepository operatorTypeRepository,
            ISampleRepository sampleRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new Sample_OperatorWrapper(entity, sampleRepository);

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
            this OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new Resample_OperatorWrapper(entity);
            wrapper.InterpolationType = (ResampleInterpolationTypeEnum)(viewModel.Interpolation?.ID ?? 0);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            var wrapper = new SumOverDimension_OperatorWrapper(entity);
            wrapper.CollectionRecalculation = (CollectionRecalculationEnum)(viewModel.CollectionRecalculation?.ID ?? 0);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_WithInletCount viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator entity = ConvertToOperator_Base(viewModel, operatorRepository, operatorTypeRepository, dimensionRepository);

            return entity;
        }

        private static Operator ConvertToOperator_Base(
            OperatorPropertiesViewModel viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;
            entity.CustomDimensionName = viewModel.CustomDimensionName;

            DimensionEnum dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
            entity.SetDimensionEnum(dimensionEnum, dimensionRepository);

            OperatorTypeEnum operatorTypeEnum = (OperatorTypeEnum)(viewModel.OperatorType?.ID ?? 0);
            entity.SetOperatorTypeEnum(operatorTypeEnum, operatorTypeRepository);

            return entity;
        }

        // Outlet 

        public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository outletRepository, IDimensionRepository dimensionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

            Outlet entity = outletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Outlet();
                entity.ID = viewModel.ID;
                outletRepository.Insert(entity);
            }
            entity.ListIndex = viewModel.ListIndex;
            entity.Name = viewModel.Name;

            DimensionEnum dimensionEnum = (DimensionEnum)(viewModel.Dimension?.ID ?? 0);
            entity.SetDimensionEnum(dimensionEnum, dimensionRepository);

            return entity;
        }

        // Patch

        public static Patch ToEntity(this PatchViewModel viewModel, IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Patch entity = patchRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Patch();
                entity.ID = viewModel.ID;
                patchRepository.Insert(entity);
            }

            return entity;
        }

        public static ToPatchWithRelatedEntitiesResult ToEntityWithRelatedEntities(
            this PatchViewModel viewModel, 
            PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            var converter = new RecursiveToEntityConverter(repositories);
            var convertedOperators = new HashSet<Operator>();

            Patch patch = viewModel.ToEntity(repositories.PatchRepository);

            foreach (OperatorViewModel operatorViewModel in viewModel.OperatorDictionary.Values)
            {
                Operator op = converter.Convert(operatorViewModel);
                op.LinkTo(patch);

                convertedOperators.Add(op);
            }

            IList<Operator> operatorsToDelete = patch.Operators.Except(convertedOperators).ToArray();

            var result = new ToPatchWithRelatedEntitiesResult
            {
                Patch = patch,
                OperatorsToDelete = operatorsToDelete
            };

            return result;
        }

        public static Patch ToEntity(this PatchPropertiesViewModel viewModel, IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Patch patch = patchRepository.TryGet(viewModel.ID);
            if (patch == null)
            {
                patch = new Patch();
                patch.ID = viewModel.ID;
                patchRepository.Insert(patch);
            }
            patch.Name = viewModel.Name;
            patch.GroupName = viewModel.Group;

            return patch;
        }

        public static ToPatchWithRelatedEntitiesResult ToEntityWithRelatedEntities(this PatchDetailsViewModel viewModel, PatchRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            ToPatchWithRelatedEntitiesResult result = viewModel.Entity.ToEntityWithRelatedEntities(repositories);
            Patch patch = result.Patch;
            IList<Operator> operatorsToDelete = result.OperatorsToDelete;

            return result;
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
                entity = new Sample();
                entity.ID = viewModel.ID;
                repositories.SampleRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;
            entity.Amplifier = viewModel.Amplifier;
            entity.TimeMultiplier = viewModel.TimeMultiplier;
            entity.IsActive = viewModel.IsActive;
            entity.SamplingRate = viewModel.SamplingRate;
            entity.BytesToSkip = viewModel.BytesToSkip;
            entity.OriginalLocation = viewModel.OriginalLocation;

            AudioFileFormatEnum audioFileFormatEnum = (AudioFileFormatEnum)(viewModel.AudioFileFormat?.ID ?? 0);
            entity.SetAudioFileFormatEnum(audioFileFormatEnum, repositories.AudioFileFormatRepository);

            InterpolationTypeEnum interpolationTypeEnum = (InterpolationTypeEnum)(viewModel.InterpolationType?.ID ?? 0);
            entity.SetInterpolationTypeEnum(interpolationTypeEnum, repositories.InterpolationTypeRepository);

            SampleDataTypeEnum sampleDataTypeEnum = (SampleDataTypeEnum)(viewModel.SampleDataType?.ID ?? 0);
            entity.SetSampleDataTypeEnum(sampleDataTypeEnum, repositories.SampleDataTypeRepository);

            SpeakerSetupEnum speakerSetupEnum = (SpeakerSetupEnum)(viewModel.SpeakerSetup?.ID ?? 0);
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
                entity = new Scale();
                entity.ID = viewModel.ID;
                scaleRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.BaseFrequency = viewModel.BaseFrequency;

            ScaleTypeEnum scaleTypeEnum = (ScaleTypeEnum)(viewModel.ScaleType?.ID ?? 0);
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
                entity = new Tone();
                entity.ID = viewModel.ID;
                toneRepository.Insert(entity);
            }

            double number;
            if (Double.TryParse(viewModel.Number, out number))
            {
                entity.Number = number;
            }

            int octave;
            if (Int32.TryParse(viewModel.Octave, out octave))
            {
                entity.Octave = octave;
            }

            return entity;
        }
    }
}