using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Business.Synthesizer.Managers;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        // AudioFileOutput

        public static AudioFileOutput ToEntityWithRelatedEntities(this AudioFileOutputPropertiesViewModel viewModel, AudioFileOutputRepositories audioFileOutputRepositories)
        {
            return viewModel.Entity.ToEntityWithRelatedEntities(audioFileOutputRepositories);
        }

        public static AudioFileOutput ToEntityWithRelatedEntities(
            this AudioFileOutputViewModel sourceViewModel, 
            AudioFileOutputRepositories repositories)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (repositories == null) throw new NullException(() => repositories);

            AudioFileOutput destAudioFileOutput = sourceViewModel.ToEntity(repositories);

            ToEntityHelper.ToAudioFileOutputChannels(sourceViewModel.Channels, destAudioFileOutput, repositories);

            return destAudioFileOutput;
        }

        public static AudioFileOutput ToEntity(this AudioFileOutputViewModel viewModel, AudioFileOutputRepositories audioFileOutputRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputRepositories == null) throw new NullException(() => audioFileOutputRepositories);

            AudioFileOutput audioFileOutput = audioFileOutputRepositories.AudioFileOutputRepository.TryGet(viewModel.ID);
            if (audioFileOutput == null)
            {
                audioFileOutput = new AudioFileOutput();
                audioFileOutput.ID = viewModel.ID;
                audioFileOutputRepositories.AudioFileOutputRepository.Insert(audioFileOutput);
            }
            audioFileOutput.Name = viewModel.Name;
            audioFileOutput.Amplifier = viewModel.Amplifier;
            audioFileOutput.TimeMultiplier = viewModel.TimeMultiplier;
            audioFileOutput.StartTime = viewModel.StartTime;
            audioFileOutput.Duration = viewModel.Duration;
            audioFileOutput.SamplingRate = viewModel.SamplingRate;
            audioFileOutput.FilePath = viewModel.FilePath;

            if (viewModel.AudioFileFormat != null)
            {
                audioFileOutput.AudioFileFormat = audioFileOutputRepositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
            }

            if (viewModel.SampleDataType != null)
            {
                audioFileOutput.SampleDataType = audioFileOutputRepositories.SampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
            }

            if (viewModel.SpeakerSetup != null)
            {
                audioFileOutput.SpeakerSetup = audioFileOutputRepositories.SpeakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
            }

            return audioFileOutput;
        }

        public static AudioFileOutputChannel ToEntity(this AudioFileOutputChannelViewModel viewModel, IAudioFileOutputChannelRepository audioFileOutputChannelRepository, IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            AudioFileOutputChannel entity = audioFileOutputChannelRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new AudioFileOutputChannel();
                entity.ID = viewModel.ID;
                audioFileOutputChannelRepository.Insert(entity);
            }

            entity.IndexNumber = viewModel.IndexNumber;

            if (viewModel.Outlet != null)
            {
                entity.Outlet = outletRepository.Get(viewModel.Outlet.ID);
            }

            return entity;
        }

        // Child Document

        public static Document ToEntityWithRelatedEntities(this ChildDocumentViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            Document destDocument = userInput.ToEntity(repositories.DocumentRepository, repositories.ChildDocumentTypeRepository);

            ToEntityHelper.ToSamples(userInput.SamplePropertiesList, destDocument, new SampleRepositories(repositories));
            ToEntityHelper.ToCurvesWithRelatedEntities(userInput.CurveDetailsList, destDocument, new CurveRepositories(repositories));
            ToEntityHelper.ToPatchesWithRelatedEntities(userInput.PatchDetailsList, destDocument, new PatchRepositories(repositories));

            // Operator Properties
            // (Operators are converted with the PatchDetails view models, but may not contain all properties.)
            foreach (OperatorPropertiesViewModel propertiesViewModel in userInput.OperatorPropertiesList)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in userInput.OperatorPropertiesList_ForCustomOperators)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DocumentRepository);
            }

            foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in userInput.OperatorPropertiesList_ForPatchInlets)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in userInput.OperatorPropertiesList_ForPatchOutlets)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in userInput.OperatorPropertiesList_ForSamples)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.SampleRepository);
            }

            foreach (OperatorPropertiesViewModel_ForValue operatorPropertiesViewModel_ForValue in userInput.OperatorPropertiesList_ForValues)
            {
                operatorPropertiesViewModel_ForValue.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            return destDocument;
        }

        public static Document ToEntity(this ChildDocumentViewModel viewModel, IDocumentRepository documentRepository, IChildDocumentTypeRepository childDocumentTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);

            Document childDocument = documentRepository.TryGet(viewModel.ID);
            if (childDocument == null)
            {
                childDocument = new Document();
                childDocument.ID = viewModel.ID;
                documentRepository.Insert(childDocument);
            }

            // Leave setting the simple properties to the the properties view model (properties such as Name and ChildDocumentType).

            return childDocument;
        }

        public static Document ToEntityWithMainPatchReference(
            this ChildDocumentPropertiesViewModel viewModel, 
            IDocumentRepository documentRepository, 
            IChildDocumentTypeRepository childDocumentTypeRepository,
            IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Document entity = documentRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Document();
                entity.ID = viewModel.ID;
                documentRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;

            // ChildDocumentType
            bool childDocumentTypeIsFilledIn = viewModel.ChildDocumentType != null && viewModel.ChildDocumentType.ID != 0;
            if (childDocumentTypeIsFilledIn)
            {
                ChildDocumentType childDocumentType = childDocumentTypeRepository.Get(viewModel.ChildDocumentType.ID);
                entity.LinkTo(childDocumentType);
            }
            else
            {
                entity.UnlinkChildDocumentType();
            }

            // MainPatch
            bool mainPatchIsFilledIn = viewModel.MainPatch != null && viewModel.MainPatch.ID != 0;
            if (mainPatchIsFilledIn)
            {
                Patch mainPatch = patchRepository.Get(viewModel.MainPatch.ID);
                entity.LinkToMainPatch(mainPatch);
            }
            else
            {
                entity.UnlinkMainPatch();
            }

            return entity;
        }

        // Curve

        public static Curve ToEntityWithRelatedEntities(this CurveDetailsViewModel viewModel, CurveRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            return viewModel.Entity.ToEntityWithRelatedEntities(repositories);
        }

        public static Curve ToEntityWithRelatedEntities(this CurveViewModel viewModel, CurveRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Curve curve = viewModel.ToEntity(repositories.CurveRepository);

            ToEntityHelper.ToNodes(viewModel.Nodes, curve, repositories);

            return curve;
        }

        public static Curve ToEntity(this CurveViewModel viewModel, ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => viewModel);

            Curve curve = curveRepository.TryGet(viewModel.ID);
            if (curve == null)
            {
                curve = new Curve();
                curve.ID = viewModel.ID;
                curveRepository.Insert(curve);
            }
            curve.Name = viewModel.Name;

            return curve;
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
            entity.Time = viewModel.Time;
            entity.Value = viewModel.Value;
            entity.Direction = viewModel.Direction;

            if (entity.NodeType != null)
            {
                entity.NodeType = nodeTypeRepository.Get(viewModel.NodeType.ID);
            }

            return entity;
        }

        // Document

        public static Document ToEntityWithRelatedEntities(this MainViewModel userInput, RepositoryWrapper repositoryWrapper)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            return userInput.Document.ToEntityWithRelatedEntities(repositoryWrapper);
        }

        public static Document ToEntityWithRelatedEntities(this DocumentViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            Document destDocument = userInput.ToEntity(repositories.DocumentRepository);

            ToEntityHelper.ToChildDocumentsWithRelatedEntities(userInput.ChildDocumentList, destDocument, repositories);
            // NOTE: 
            // There is order dependency between converting ChildDocumentList and ChildDocumentPropertiesList.
            // ChildDocumentProperties can reference a MainPatch, converted from the ChildDocumentList.
            ToEntityHelper.ToChildDocuments(userInput.ChildDocumentPropertiesList, destDocument, repositories);
            ToEntityHelper.ToSamples(userInput.SamplePropertiesList, destDocument, new SampleRepositories(repositories));
            ToEntityHelper.ToCurvesWithRelatedEntities(userInput.CurveDetailsList, destDocument, new CurveRepositories(repositories));
            ToEntityHelper.ToPatchesWithRelatedEntities(userInput.PatchDetailsList, destDocument, new PatchRepositories(repositories));
            ToEntityHelper.ToAudioFileOutputsWithRelatedEntities(userInput.AudioFileOutputPropertiesList, destDocument, new AudioFileOutputRepositories(repositories));

            // Operator Properties
            // (Operators are converted with the PatchDetails view models, but may not contain all properties.)
            foreach (OperatorPropertiesViewModel propertiesViewModel in userInput.OperatorPropertiesList)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in userInput.OperatorPropertiesList_ForCustomOperators)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DocumentRepository);
            }

            foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in userInput.OperatorPropertiesList_ForPatchInlets)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in userInput.OperatorPropertiesList_ForPatchOutlets)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in userInput.OperatorPropertiesList_ForSamples)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.SampleRepository);
            }

            foreach (OperatorPropertiesViewModel_ForValue propertiesViewModel in userInput.OperatorPropertiesList_ForValues)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

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

        public static Document ToEntity(this DocumentDetailsViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = viewModel.Document.ToDocument(documentRepository);
            return document;
        }

        public static Document ToDocument(this IDAndName idAndName, IDocumentRepository documentRepository)
        {
            if (idAndName == null) throw new NullException(() => idAndName);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document document = documentRepository.TryGet(idAndName.ID);
            if (document == null)
            {
                document = new Document();
                document.ID = idAndName.ID;
                documentRepository.Insert(document);
            }

            document.Name = idAndName.Name;

            return document;
        }

        /// <summary>
        /// Used for OperatorProperties view for CustomOperators, to partially convert to entity,
        /// just enoughto make a few entity validations work.
        /// </summary> 
        public static Document ToHollowDocumentWithHollowChildDocumentsWithHollowMainPatches(
            this DocumentViewModel viewModel,
            IDocumentRepository documentRepository,
            IChildDocumentTypeRepository childDocumentTypeRepository,
            IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Document rootDocument = viewModel.ToEntity(documentRepository);

            foreach (ChildDocumentPropertiesViewModel childDocumentPropertiesViewModel in viewModel.ChildDocumentPropertiesList)
            {
                bool mainPatchIsFilledIn = childDocumentPropertiesViewModel.MainPatch != null &&
                                           childDocumentPropertiesViewModel.MainPatch.ID != 0;
                if (mainPatchIsFilledIn)
                {
                    Patch mainPatch = childDocumentPropertiesViewModel.MainPatch.ToPatch(patchRepository);
                }

                Document childDocument = childDocumentPropertiesViewModel.ToEntityWithMainPatchReference(
                    documentRepository,
                    childDocumentTypeRepository,
                    patchRepository);
                childDocument.LinkToParentDocument(rootDocument);
            }

            return rootDocument;
        }

        /// <summary>
        /// Used for OperatorProperties view for Sample operators, to partially convert to entity,
        /// just enoughto make a few entity validations work.
        /// </summary> 
        public static Document ToHollowDocumentWithHollowChildDocumentsWithHollowSamples(
            this DocumentViewModel viewModel,
            IDocumentRepository documentRepository,
            IChildDocumentTypeRepository childDocumentTypeRepository,
            ISampleRepository sampleRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            Document rootDocument = viewModel.ToEntity(documentRepository);

            foreach (SamplePropertiesViewModel samplePropertiesViewModel in viewModel.SamplePropertiesList)
            {
                Sample sample = samplePropertiesViewModel.Entity.ToHollowEntity(sampleRepository);
                sample.LinkTo(rootDocument);
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in viewModel.ChildDocumentList)
            {
                Document childDocument = childDocumentViewModel.ToEntity(documentRepository, childDocumentTypeRepository);
                childDocument.LinkToParentDocument(rootDocument);

                foreach (SamplePropertiesViewModel samplePropertiesViewModel in childDocumentViewModel.SamplePropertiesList)
                {
                    Sample sample = samplePropertiesViewModel.Entity.ToHollowEntity(sampleRepository);
                    sample.LinkTo(childDocument);
                }
            }

            return rootDocument;
        }

        // Operator Properties

        public static Operator ToEntity(
            this OperatorPropertiesViewModel viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            // Added this so operator properties lose focus on a new operator would be able to do some basic validation.
            entity.OperatorType = operatorTypeRepository.TryGet(viewModel.OperatorType.ID);

            entity.Name = viewModel.Name;
            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForCustomOperator viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, operatorTypeRepository);

            // UnderlyingDocument
            var wrapper = new Custom_OperatorWrapper(entity, documentRepository);
            bool underlyingDocumentIsFilledIn = viewModel.UnderlyingDocument != null && viewModel.UnderlyingDocument.ID != 0;
            if (underlyingDocumentIsFilledIn)
            {
                wrapper.UnderlyingDocumentID = viewModel.UnderlyingDocument.ID;
            }
            else
            {
                wrapper.UnderlyingDocumentID = null;
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForPatchInlet viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.SetOperatorTypeEnum(OperatorTypeEnum.PatchInlet, operatorTypeRepository);

            var wrapper = new PatchInlet_OperatorWrapper(entity);
            wrapper.SortOrder = viewModel.SortOrder;

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForPatchOutlet viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.SetOperatorTypeEnum(OperatorTypeEnum.PatchOutlet, operatorTypeRepository);

            var wrapper = new PatchOutlet_OperatorWrapper(entity);
            wrapper.SortOrder = viewModel.SortOrder;

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForSample viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository, ISampleRepository sampleRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Sample, operatorTypeRepository);

            // Sample
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
            this OperatorPropertiesViewModel_ForValue viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Operator();
                entity.ID = viewModel.ID;
                operatorRepository.Insert(entity);
            }

            entity.Name = viewModel.Name;
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Value, operatorTypeRepository);

            entity.Data = viewModel.Value;

            return entity;
        }

        // Patch

        public static Patch ToEntityWithRelatedEntities(this PatchDetailsViewModel viewModel, PatchRepositories patchRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.Entity.ToEntityWithRelatedEntities(patchRepositories);

            return patch;
        }

        public static Patch ToEntityWithRelatedEntities(this PatchViewModel viewModel, PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            Patch patch = viewModel.ToEntity(repositories.PatchRepository);

            var converter = new RecursiveToEntityConverter(repositories);

            var convertedOperators = new HashSet<Operator>();

            foreach (OperatorViewModel operatorViewModel in viewModel.Operators)
            {
                Operator op = converter.Convert(operatorViewModel);
                op.LinkTo(patch);

                convertedOperators.Add(op);
            }

            var patchManager = new PatchManager(repositories);

            IList<Operator> operatorsToDelete = patch.Operators.Except(convertedOperators).ToArray();
            foreach (Operator op in operatorsToDelete)
            {
                patchManager.DeleteOperator(op);
            }

            return patch;
        }

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
            entity.Name = viewModel.Name;

            return entity;
        }

        public static Patch ToPatch(this IDAndName idAndName, IPatchRepository patchRepository)
        {
            if (idAndName == null) throw new NullException(() => idAndName);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Patch entity = patchRepository.TryGet(idAndName.ID);
            if (entity == null)
            {
                entity = new Patch();
                entity.ID = idAndName.ID;
                patchRepository.Insert(entity);
            }
            entity.Name = idAndName.Name;

            return entity;
        }

        public static Operator ToEntityWithInletsAndOutlets(this OperatorViewModel viewModel, PatchRepositories patchRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepositories == null) throw new NullException(() => patchRepositories);

            Operator op = viewModel.ToEntity(patchRepositories.OperatorRepository, patchRepositories.OperatorTypeRepository);

            // TODO: Also do delete operations.
            // TOOD: Make sure you do not repeat so much code here and in RecursiveToEntityConverter.
            foreach (InletViewModel inletViewModel in viewModel.Inlets)
            {
                Inlet inlet = inletViewModel.ToEntity(patchRepositories.InletRepository);
                inlet.LinkTo(op);
            }

            ToEntityHelper.HACK_CreatePatchInletInletIfNeeded(op, patchRepositories.InletRepository, patchRepositories.IDRepository);

            foreach (OutletViewModel outletViewModel in viewModel.Outlets)
            {
                Outlet outlet = outletViewModel.ToEntity(patchRepositories.OutletRepository);
                outlet.LinkTo(op);
            }

            ToEntityHelper.HACK_CreatePatchOutletOutletIfNeeded(op, patchRepositories.OutletRepository, patchRepositories.IDRepository);

            return op;
        }

        public static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
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

            entity.Name = viewModel.Name;
            entity.OperatorType = operatorTypeRepository.TryGet(viewModel.OperatorType.ID);

            return entity;
        }

        public static Inlet ToEntity(this InletViewModel viewModel, IInletRepository inletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (inletRepository == null) throw new NullException(() => inletRepository);

            Inlet entity = inletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Inlet();
                entity.ID = viewModel.ID;
                inletRepository.Insert(entity);
            }
            entity.SortOrder = viewModel.SortOrder;
            entity.Name = viewModel.Name;

            return entity;
        }

        public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            Outlet entity = outletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Outlet();
                entity.ID = viewModel.ID;
                outletRepository.Insert(entity);
            }
            entity.SortOrder = viewModel.SortOrder;
            entity.Name = viewModel.Name;

            return entity;
        }

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

        // Sample

        public static Sample ToEntity(this SamplePropertiesViewModel viewModel, SampleRepositories sampleRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (sampleRepositories == null) throw new NullException(() => sampleRepositories);

            return viewModel.Entity.ToEntity(sampleRepositories);
        }

        public static Sample ToEntity(this SampleViewModel viewModel, SampleRepositories sampleRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (sampleRepositories == null) throw new NullException(() => sampleRepositories);

            Sample sample = sampleRepositories.SampleRepository.TryGet(viewModel.ID);
            if (sample == null)
            {
                sample = new Sample();
                sample.ID = viewModel.ID;
                sampleRepositories.SampleRepository.Insert(sample);
            }
            sample.Name = viewModel.Name;
            sample.Amplifier = viewModel.Amplifier;
            sample.TimeMultiplier = viewModel.TimeMultiplier;
            sample.IsActive = viewModel.IsActive;
            sample.SamplingRate = viewModel.SamplingRate;
            sample.BytesToSkip = viewModel.BytesToSkip;
            sample.OriginalLocation = viewModel.OriginalLocation;

            if (viewModel.AudioFileFormat != null)
            {
                sample.AudioFileFormat = sampleRepositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
            }

            if (viewModel.InterpolationType != null)
            {
                sample.InterpolationType = sampleRepositories.InterpolationTypeRepository.Get(viewModel.InterpolationType.ID);
            }

            if (viewModel.SampleDataType != null)
            {
                sample.SampleDataType = sampleRepositories.SampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
            }

            if (viewModel.SpeakerSetup != null)
            {
                sample.SpeakerSetup = sampleRepositories.SpeakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
            }

            sampleRepositories.SampleRepository.SetBytes(viewModel.ID, viewModel.Bytes);

            return sample;
        }

        /// <summary> Converts to a Sample with an ID but no other properties assigned. </summary>
        public static Sample ToHollowEntity(this SampleViewModel viewModel, ISampleRepository sampleRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            Sample sample = sampleRepository.TryGet(viewModel.ID);
            if (sample == null)
            {
                sample = new Sample();
                sample.ID = viewModel.ID;
                sampleRepository.Insert(sample);
            }

            return sample;
        }
    }
}