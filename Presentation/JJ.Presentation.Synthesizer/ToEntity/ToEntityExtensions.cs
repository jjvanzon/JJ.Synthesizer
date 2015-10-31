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
using System;
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

            sourceViewModel.Channels.ToAudioFileOutputChannels(destAudioFileOutput, repositories);

            return destAudioFileOutput;
        }

        public static AudioFileOutput ToEntity(this AudioFileOutputViewModel viewModel, AudioFileOutputRepositories repositories)
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

            bool audioFileFormatIsFilledIn = viewModel.AudioFileFormat != null && viewModel.AudioFileFormat.ID != 0;
            if (audioFileFormatIsFilledIn)
            {
                AudioFileFormat audioFileFormat = repositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
                entity.LinkTo(audioFileFormat);
            }
            else
            {
                entity.UnlinkAudioFileFormat();
            }

            bool sampleDataTypeIsFilledIn = viewModel.AudioFileFormat != null && viewModel.AudioFileFormat.ID != 0;
            if (sampleDataTypeIsFilledIn)
            {
                AudioFileFormat sampleDataType = repositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
                entity.LinkTo(sampleDataType);
            }
            else
            {
                entity.UnlinkAudioFileFormat();
            }

            bool speakerSetupIsFilledIn = viewModel.AudioFileFormat != null && viewModel.AudioFileFormat.ID != 0;
            if (speakerSetupIsFilledIn)
            {
                AudioFileFormat speakerSetup = repositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
                entity.LinkTo(speakerSetup);
            }
            else
            {
                entity.UnlinkAudioFileFormat();
            }

            return entity;
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

            bool outletIsFilledIn = viewModel.Outlet != null && viewModel.Outlet.ID != 0;
            if (outletIsFilledIn)
            {
                Outlet outlet = outletRepository.Get(viewModel.Outlet.ID);
                entity.LinkTo(outlet);
            }
            else
            {
                entity.UnlinkOutlet();
            }

            return entity;
        }

        public static void ToAudioFileOutputsWithRelatedEntities(
            this IList<AudioFileOutputPropertiesViewModel> viewModelList,
            Document destDocument,
            AudioFileOutputRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (AudioFileOutputPropertiesViewModel viewModel in viewModelList)
            {
                AudioFileOutput entity = viewModel.Entity.ToEntityWithRelatedEntities(repositories);
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
                audioFileOutputManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        public static void ToAudioFileOutputChannels(
            this IList<AudioFileOutputChannelViewModel> viewModelList,
            AudioFileOutput destAudioFileOutput,
            AudioFileOutputRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destAudioFileOutput == null) throw new NullException(() => destAudioFileOutput);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (AudioFileOutputChannelViewModel viewModel in viewModelList)
            {
                AudioFileOutputChannel entity = viewModel.ToEntity(repositories.AudioFileOutputChannelRepository, repositories.OutletRepository);
                entity.LinkTo(destAudioFileOutput);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var audioFileOutputManager = new AudioFileOutputManager(repositories);

            IList<int> existingIDs = destAudioFileOutput.AudioFileOutputChannels.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                audioFileOutputManager.DeleteAudioFileOutputChannel(idToDelete);
            }
        }

        // Child Document

        public static Document ToEntityWithRelatedEntities(this ChildDocumentViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            Document destDocument = userInput.ToEntity(repositories.DocumentRepository, repositories.ChildDocumentTypeRepository);

            var curveRepositories = new CurveRepositories(repositories);
            userInput.CurvePropertiesList.ToEntities(destDocument, curveRepositories);
            userInput.CurveDetailsList.ToEntitiesWithRelatedEntities(destDocument, curveRepositories);
            userInput.PatchDetailsList.ToPatchesWithRelatedEntities(destDocument, new PatchRepositories(repositories));
            userInput.SamplePropertiesList.ToSamples(destDocument, new SampleRepositories(repositories));

            // Operator Properties
            // (Operators are converted with the PatchDetails view models, but may not contain all properties.)
            foreach (OperatorPropertiesViewModel propertiesViewModel in userInput.OperatorPropertiesList)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForBundle propertiesViewModel in userInput.OperatorPropertiesList_ForBundles)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in userInput.OperatorPropertiesList_ForCurves)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.CurveRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in userInput.OperatorPropertiesList_ForCustomOperators)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DocumentRepository);
            }

            foreach (OperatorPropertiesViewModel_ForNumber operatorPropertiesViewModel_ForNumber in userInput.OperatorPropertiesList_ForNumbers)
            {
                operatorPropertiesViewModel_ForNumber.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
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

            foreach (OperatorPropertiesViewModel_ForUnbundle propertiesViewModel in userInput.OperatorPropertiesList_ForUnbundles)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            return destDocument;
        }

        public static Document ToEntity(this ChildDocumentViewModel viewModel, IDocumentRepository documentRepository, IChildDocumentTypeRepository childDocumentTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);

            Document entity = documentRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Document();
                entity.ID = viewModel.ID;
                documentRepository.Insert(entity);
            }

            // Leave setting the simple properties to the the properties view model (properties such as Name and ChildDocumentType).

            return entity;
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

        /// <summary> Leading for saving when it comes to the simple properties. </summary>
        public static void ToChildDocuments(
            this IList<ChildDocumentPropertiesViewModel> sourceViewModelList,
            Document destParentDocument,
            RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModelList == null) throw new NullException(() => sourceViewModelList);
            if (destParentDocument == null) throw new NullException(() => destParentDocument);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var idsToKeep = new HashSet<int>();

            foreach (ChildDocumentPropertiesViewModel propertiesViewModel in sourceViewModelList)
            {
                Document entity = propertiesViewModel.ToEntityWithMainPatchReference(
                    repositoryWrapper.DocumentRepository,
                    repositoryWrapper.ChildDocumentTypeRepository,
                    repositoryWrapper.PatchRepository);

                entity.LinkToParentDocument(destParentDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            DocumentManager documentManager = new DocumentManager(repositoryWrapper);

            IList<int> existingIDs = destParentDocument.ChildDocuments.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        /// <summary> Leading for saving child entities, not leading for saving the simple properties. </summary>
        public static void ToChildDocumentsWithRelatedEntities(
            this IList<ChildDocumentViewModel> sourceViewModelList,
            Document destParentDocument,
            RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModelList == null) throw new NullException(() => sourceViewModelList);
            if (destParentDocument == null) throw new NullException(() => destParentDocument);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var idsToKeep = new HashSet<int>();

            foreach (ChildDocumentViewModel childDocumentViewModel in sourceViewModelList)
            {
                Document entity = childDocumentViewModel.ToEntityWithRelatedEntities(repositoryWrapper);

                entity.LinkToParentDocument(destParentDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            DocumentManager documentManager = new DocumentManager(repositoryWrapper);

            IList<int> existingIDs = destParentDocument.ChildDocuments.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        // Curve

        public static void ToEntitiesWithRelatedEntities(this IList<CurveDetailsViewModel> viewModelList, Document destDocument, CurveRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (CurveDetailsViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.Entity.ToEntityWithRelatedEntities(repositories);
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

        public static void ToEntities(this IList<CurvePropertiesViewModel> viewModelList, Document destDocument, CurveRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (CurvePropertiesViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.Entity.ToCurve(repositories.CurveRepository);
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

        public static Curve ToEntityWithRelatedEntities(this CurveDetailsViewModel viewModel, CurveRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            return viewModel.Entity.ToEntityWithRelatedEntities(repositories);
        }

        public static Curve ToEntity(this CurvePropertiesViewModel viewModel, ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            return viewModel.Entity.ToCurve(curveRepository);
        }

        public static Curve ToEntityWithRelatedEntities(this CurveViewModel viewModel, CurveRepositories repositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositories == null) throw new NullException(() => repositories);

            Curve curve = viewModel.ToEntity(repositories.CurveRepository);

            viewModel.Nodes.ToNodes(curve, repositories);

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

        public static Curve ToCurve(this IDAndName idAndName, ICurveRepository curveRepository)
        {
            if (idAndName == null) throw new NullException(() => idAndName);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            Curve entity = curveRepository.TryGet(idAndName.ID);
            if (entity == null)
            {
                entity = new Curve();
                entity.ID = idAndName.ID;
                curveRepository.Insert(entity);
            }
            entity.Name = idAndName.Name;

            return entity;
        }

        // Document

        public static Document ToEntityWithRelatedEntities(this MainViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            return userInput.Document.ToEntityWithRelatedEntities(repositories);
        }

        public static Document ToEntityWithRelatedEntities(this DocumentViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            var curveRepositories = new CurveRepositories(repositories);
            var scaleRepositories = new ScaleRepositories(repositories);

            Document destDocument = userInput.ToEntity(repositories.DocumentRepository);

            // NOTE: 
            // There is order dependency between converting ChildDocumentList and ChildDocumentPropertiesList.
            // ChildDocumentProperties can reference a MainPatch, converted from the ChildDocumentList.
            userInput.ChildDocumentList.ToChildDocumentsWithRelatedEntities(destDocument, repositories);
            userInput.ChildDocumentPropertiesList.ToChildDocuments(destDocument, repositories);

            userInput.AudioFileOutputPropertiesList.ToAudioFileOutputsWithRelatedEntities(destDocument, new AudioFileOutputRepositories(repositories));
            userInput.CurvePropertiesList.ToEntities(destDocument, curveRepositories);
            userInput.CurveDetailsList.ToEntitiesWithRelatedEntities(destDocument, curveRepositories);
            // userInput.NodePropertiesList's data is also present in the CurveDetails so nothing additional needs to be converted.
            userInput.PatchDetailsList.ToPatchesWithRelatedEntities(destDocument, new PatchRepositories(repositories));
            userInput.SamplePropertiesList.ToSamples(destDocument, new SampleRepositories(repositories));
            userInput.ScalePropertiesList.ToEntities(scaleRepositories, destDocument);
            userInput.ToneGridEditList.ForEach(x => x.ToEntityWithRelatedEntities(scaleRepositories));

            // Operator Properties
            // (Operators are converted with the PatchDetails view models, but may not contain all properties.)
            foreach (OperatorPropertiesViewModel propertiesViewModel in userInput.OperatorPropertiesList)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForBundle propertiesViewModel in userInput.OperatorPropertiesList_ForBundles)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in userInput.OperatorPropertiesList_ForCurves)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.CurveRepository);
            }

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in userInput.OperatorPropertiesList_ForCustomOperators)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository, repositories.DocumentRepository);
            }

            foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in userInput.OperatorPropertiesList_ForNumbers)
            {
                propertiesViewModel.ToEntity(repositories.OperatorRepository, repositories.OperatorTypeRepository);
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

            foreach (OperatorPropertiesViewModel_ForUnbundle propertiesViewModel in userInput.OperatorPropertiesList_ForUnbundles)
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

        /// <summary>
        /// Used for OperatorProperties view for Sample operators, to partially convert to entity,
        /// just enoughto make a few entity validations work.
        /// </summary> 
        public static Document ToHollowDocumentWithHollowChildDocumentsWithHollowSamplesWithName(
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
                sample.Name = samplePropertiesViewModel.Entity.Name;
                sample.LinkTo(rootDocument);
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in viewModel.ChildDocumentList)
            {
                Document childDocument = childDocumentViewModel.ToEntity(documentRepository, childDocumentTypeRepository);
                childDocument.LinkToParentDocument(rootDocument);

                foreach (SamplePropertiesViewModel samplePropertiesViewModel in childDocumentViewModel.SamplePropertiesList)
                {
                    Sample sample = samplePropertiesViewModel.Entity.ToHollowEntity(sampleRepository);
                    sample.Name = samplePropertiesViewModel.Entity.Name;
                    sample.LinkTo(childDocument);
                }
            }

            return rootDocument;
        }

        /// <summary>
        /// Used for OperatorProperties view for Curve operators, to partially convert to entity,
        /// just enoughto make a few entity validations work.
        /// </summary> 
        public static Document ToHollowDocumentWithHollowChildDocumentsWithCurvesWithName(
            this DocumentViewModel viewModel,
            IDocumentRepository documentRepository,
            IChildDocumentTypeRepository childDocumentTypeRepository,
            ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            Document rootDocument = viewModel.ToEntity(documentRepository);

            // TODO: Use CurvePropertiesViewModel instead of CurveDetailsViewModel instead, after you programmed those.

            foreach (CurveDetailsViewModel curveDetailsViewModel in viewModel.CurveDetailsList)
            {
                Curve curve = curveDetailsViewModel.Entity.ToEntity(curveRepository);
                curve.Name = curveDetailsViewModel.Entity.Name;
                curve.LinkTo(rootDocument);
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in viewModel.ChildDocumentList)
            {
                Document childDocument = childDocumentViewModel.ToEntity(documentRepository, childDocumentTypeRepository);
                childDocument.LinkToParentDocument(rootDocument);

                foreach (CurveDetailsViewModel curveDetailsViewModel in childDocumentViewModel.CurveDetailsList)
                {
                    Curve curve = curveDetailsViewModel.Entity.ToEntity(curveRepository);
                    curve.Name = curveDetailsViewModel.Entity.Name;
                    curve.LinkTo(childDocument);
                }
            }

            return rootDocument;
        }

        // Nodes

        public static void ToNodes(this IList<NodeViewModel> viewModelList, Curve destCurve, CurveRepositories repositories)
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
            entity.Time = viewModel.Time;
            entity.Value = viewModel.Value;

            bool nodeTypeIsFilledIn = viewModel.NodeType != null && viewModel.NodeType.ID != 0;
            if (nodeTypeIsFilledIn)
            {
                NodeType nodeType = nodeTypeRepository.Get(viewModel.NodeType.ID);
                entity.LinkTo(nodeType);
            }
            else
            {
                entity.UnlinkNodeType();
            }

            return entity;
        }

        public static Node ToEntity(this NodePropertiesViewModel viewModel, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Node entity = viewModel.Entity.ToEntity(nodeRepository, nodeTypeRepository);
            return entity;
        }

        // Operator 

        public static Operator ToEntityWithInletsAndOutlets(this OperatorViewModel viewModel, PatchRepositories patchRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepositories == null) throw new NullException(() => patchRepositories);

            Operator op = viewModel.ToEntity(patchRepositories.OperatorRepository, patchRepositories.OperatorTypeRepository);

            // TOOD: Make sure you do not repeat so much code here and in RecursiveToEntityConverter.

            var patchManager = new PatchManager(patchRepositories);

            // Inlets
            IList<Inlet> inletsToKeep = new List<Inlet>(viewModel.Inlets.Count + 1);

            foreach (InletViewModel inletViewModel in viewModel.Inlets)
            {
                Inlet inlet = inletViewModel.ToEntity(patchRepositories.InletRepository);
                inlet.LinkTo(op);

                inletsToKeep.Add(inlet);
            }

            Inlet HACK_patchInletInlet = ToEntityHelper.HACK_CreatePatchInletInletIfNeeded(op, patchRepositories.InletRepository, patchRepositories.IDRepository);
            if (HACK_patchInletInlet != null)
            {
                inletsToKeep.Add(HACK_patchInletInlet);
            }

            IList<Inlet> inletsToDelete = op.Inlets.Except(inletsToKeep).ToArray();
            foreach (Inlet inletToDelete in inletsToDelete)
            {
                patchManager.DeleteInlet(inletToDelete);
            }

            // Outlets
            IList<Outlet> outletsToKeep = new List<Outlet>(viewModel.Outlets.Count + 1);

            foreach (OutletViewModel outletViewModel in viewModel.Outlets)
            {
                Outlet outlet = outletViewModel.ToEntity(patchRepositories.OutletRepository);
                outlet.LinkTo(op);

                outletsToKeep.Add(outlet);
            }

            Outlet HACK_patchOutletOutlet = ToEntityHelper.HACK_CreatePatchOutletOutletIfNeeded(op, patchRepositories.OutletRepository, patchRepositories.IDRepository);
            if (HACK_patchOutletOutlet != null)
            {
                outletsToKeep.Add(HACK_patchOutletOutlet);
            }

            IList<Outlet> outletsToDelete = op.Outlets.Except(outletsToKeep).ToArray();
            foreach (Outlet outletToDelete in outletsToDelete)
            {
                patchManager.DeleteOutlet(outletToDelete);
            }

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

            bool operatorTypeIsFilledIn = viewModel.OperatorType != null && viewModel.OperatorType.ID != 0;
            if (operatorTypeIsFilledIn)
            {
                OperatorType operatorType = operatorTypeRepository.Get(viewModel.OperatorType.ID);
                entity.LinkTo(operatorType);
            }
            else
            {
                entity.UnlinkOperatorType();
            }

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

            entity.Name = viewModel.Name;

            // Added this so operator properties lose focus on a new operator would be able to do some basic validation.
            bool operatorTypeIsFilledIn = viewModel.OperatorType != null && viewModel.OperatorType.ID != 0;
            if (operatorTypeIsFilledIn)
            {
                OperatorType operatorType = operatorTypeRepository.Get(viewModel.OperatorType.ID);
                entity.LinkTo(operatorType);
            }
            else
            {
                entity.UnlinkOperatorType();
            }

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForBundle viewModel,
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
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Bundle, operatorTypeRepository);

            return entity;
        }

        public static Operator ToEntity(
            this OperatorPropertiesViewModel_ForCurve viewModel,
            IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository, ICurveRepository curveRepository)
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
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Curve, operatorTypeRepository);

            // Curve
            var wrapper = new OperatorWrapper_Curve(entity, curveRepository);
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
            var wrapper = new OperatorWrapper_CustomOperator(entity, documentRepository);
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
            this OperatorPropertiesViewModel_ForNumber viewModel,
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
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Number, operatorTypeRepository);

            entity.Data = viewModel.Number;

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

            var wrapper = new OperatorWrapper_PatchInlet(entity);
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

            var wrapper = new OperatorWrapper_PatchOutlet(entity);
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
            var wrapper = new OperatorWrapper_Sample(entity, sampleRepository);
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
            this OperatorPropertiesViewModel_ForUnbundle viewModel,
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
            entity.SetOperatorTypeEnum(OperatorTypeEnum.Unbundle, operatorTypeRepository);

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

            var converter = new RecursiveToEntityConverter(repositories);
            var convertedOperators = new HashSet<Operator>();

            Patch patch = viewModel.ToEntity(repositories.PatchRepository);

            foreach (OperatorViewModel operatorViewModel in viewModel.Operators)
            {
                Operator op = converter.Convert(operatorViewModel);
                op.LinkTo(patch);

                convertedOperators.Add(op);
            }

            var patchManager = new PatchManager(patch, repositories);

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

        public static void ToPatchesWithRelatedEntities(
            this IList<PatchDetailsViewModel> viewModelList,
            Document destDocument,
            PatchRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (PatchDetailsViewModel viewModel in viewModelList)
            {
                Patch entity = viewModel.Entity.ToEntityWithRelatedEntities(repositories);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destDocument.Patches.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                Patch patchToDelete = repositories.PatchRepository.Get(idToDelete);
                PatchManager patchManager = new PatchManager(patchToDelete, repositories);
                IResult result = patchManager.DeleteWithRelatedEntities();
                ResultHelper.Assert(result);
            }
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

            Sample entity = sampleRepositories.SampleRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = new Sample();
                entity.ID = viewModel.ID;
                sampleRepositories.SampleRepository.Insert(entity);
            }
            entity.Name = viewModel.Name;
            entity.Amplifier = viewModel.Amplifier;
            entity.TimeMultiplier = viewModel.TimeMultiplier;
            entity.IsActive = viewModel.IsActive;
            entity.SamplingRate = viewModel.SamplingRate;
            entity.BytesToSkip = viewModel.BytesToSkip;
            entity.OriginalLocation = viewModel.OriginalLocation;

            // AudioFileFormat
            bool audioFileFormatIsFilledIn = viewModel.AudioFileFormat != null && viewModel.AudioFileFormat.ID != 0;
            if (audioFileFormatIsFilledIn)
            {
                AudioFileFormat audioFileFormat = sampleRepositories.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
                entity.LinkTo(audioFileFormat);
            }
            else
            {
                entity.UnlinkAudioFileFormat();
            }

            // InterpolationType
            bool interpolationTypeIsFilledIn = viewModel.InterpolationType != null && viewModel.InterpolationType.ID != 0;
            if (interpolationTypeIsFilledIn)
            {
                InterpolationType interpolationType = sampleRepositories.InterpolationTypeRepository.Get(viewModel.InterpolationType.ID);
                entity.LinkTo(interpolationType);
            }
            else
            {
                entity.UnlinkInterpolationType();
            }

            // SampleDataType
            bool sampleDataTypeIsFilledIn = viewModel.SampleDataType != null && viewModel.SampleDataType.ID != 0;
            if (sampleDataTypeIsFilledIn)
            {
                SampleDataType sampleDataType = sampleRepositories.SampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
                entity.LinkTo(sampleDataType);
            }
            else
            {
                entity.UnlinkSampleDataType();
            }

            // SpeakerSetup
            bool speakerSetupIsFilledIn = viewModel.SpeakerSetup != null && viewModel.SpeakerSetup.ID != 0;
            if (speakerSetupIsFilledIn)
            {
                SpeakerSetup speakerSetup = sampleRepositories.SpeakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
                entity.LinkTo(speakerSetup);
            }
            else
            {
                entity.UnlinkSpeakerSetup();
            }

            sampleRepositories.SampleRepository.SetBytes(viewModel.ID, viewModel.Bytes);

            return entity;
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

        public static void ToSamples(this IList<SamplePropertiesViewModel> viewModelList, Document destDocument, SampleRepositories repositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (SamplePropertiesViewModel viewModel in viewModelList)
            {
                Sample entity = viewModel.Entity.ToEntity(repositories);
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

        public static void ToEntities(this IList<ScalePropertiesViewModel> viewModelList, ScaleRepositories repositories, Document destDocument)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var idsToKeep = new HashSet<int>();

            foreach (ScalePropertiesViewModel viewModel in viewModelList)
            {
                Scale entity = viewModel.Entity.ToEntity(repositories.ScaleRepository, repositories.ScaleTypeRepository);
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

            bool scaleTypeIsFilledIn = viewModel.ScaleType != null && viewModel.ScaleType.ID != 0;
            if (scaleTypeIsFilledIn)
            {
                ScaleType scaleType = scaleTypeRepository.Get(viewModel.ScaleType.ID);
                entity.LinkTo(scaleType);
            }
            else
            {
                entity.UnlinkScaleType();
            }

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
            if (!Double.TryParse(viewModel.Number, out number))
            {
                throw new NotDoubleException(() => viewModel.Number);
            }
            entity.Number = number;

            int octave;
            if (!Int32.TryParse(viewModel.Octave, out octave))
            {
                throw new NotIntegerException(() => viewModel.Octave);
            }
            entity.Octave = octave;

            return entity;
        }
    }
}