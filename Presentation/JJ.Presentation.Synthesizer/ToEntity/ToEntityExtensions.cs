using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.Converters;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        public static Document ToEntityWithRelatedEntities(this DocumentViewModel sourceViewModel, RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Document destDocument = sourceViewModel.ToEntity(repositoryWrapper.DocumentRepository);

            ConvertToInstrumentsWithRelatedEntities(sourceViewModel, destDocument, repositoryWrapper);
            ConvertToEffectsWithRelatedEntities(sourceViewModel, destDocument, repositoryWrapper);
            ConvertToSamples(sourceViewModel.SamplePropertiesList, destDocument, repositoryWrapper);
            ConvertToCurvesWithRelatedEntities(sourceViewModel.CurveDetailsList, destDocument, repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository);
            ConvertToPatchesWithRelatedEntities(sourceViewModel.PatchDetailsList, destDocument, repositoryWrapper);
            ConvertToAudioFileOutputsWithRelatedEntities(sourceViewModel.AudioFileOutputPropertiesList, destDocument, repositoryWrapper);

            return destDocument;
        }

        public static Document ToEntity(this DocumentViewModel viewModel, IDocumentRepository documentRepository)
        {
            Document document = documentRepository.TryGet(viewModel.ID);
            if (document == null)
            {
                document = documentRepository.Create();
            }
            document.Name = viewModel.DocumentProperties.Document.Name;
            return document;
        }

        private static void ConvertToInstrumentsWithRelatedEntities(DocumentViewModel sourceViewModel, Document destDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (ChildDocumentPropertiesViewModel instrumentPropertiesViewModel in sourceViewModel.InstrumentPropertiesList)
            {
                Document entity = instrumentPropertiesViewModel.ToEntity(repositoryWrapper.DocumentRepository);

                entity.LinkInstrumentToDocument(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            foreach (ChildDocumentViewModel instrumentViewModel in sourceViewModel.Instruments)
            {
                Document entity = instrumentViewModel.ToEntityWithRelatedEntities(repositoryWrapper);

                entity.LinkInstrumentToDocument(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            DocumentManager documentManager = new DocumentManager(repositoryWrapper);

            IList<int> existingIDs = destDocument.Instruments.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        private static void ConvertToEffectsWithRelatedEntities(DocumentViewModel sourceViewModel, Document destDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (ChildDocumentPropertiesViewModel effectPropertiesViewModel in sourceViewModel.EffectPropertiesList)
            {
                Document entity = effectPropertiesViewModel.ToEntity(repositoryWrapper.DocumentRepository);
                entity.LinkEffectToDocument(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            foreach (ChildDocumentViewModel effectViewModel in sourceViewModel.Effects)
            {
                Document entity = effectViewModel.ToEntityWithRelatedEntities(repositoryWrapper);
                entity.LinkEffectToDocument(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var documentManager = new DocumentManager(repositoryWrapper);

            IEnumerable<int> existingIDs = destDocument.Effects.Select(x => x.ID).ToArray();
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        public static Document ToEntity(this ChildDocumentPropertiesViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document entity = documentRepository.TryGet(viewModel.Document.ID);
            if (entity == null)
            {
                entity = documentRepository.Create();
            }
            entity.Name = viewModel.Document.Name;

            return entity;
        }

        public static Document ToEntityWithRelatedEntities(this ChildDocumentViewModel sourceViewModel, RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Document destDocument = sourceViewModel.DocumentProperties.ToEntity(repositoryWrapper.DocumentRepository);

            ConvertToSamples(sourceViewModel.SamplePropertiesList, destDocument, repositoryWrapper);
            ConvertToCurvesWithRelatedEntities(sourceViewModel.CurveDetailsList, destDocument, repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository);
            ConvertToPatchesWithRelatedEntities(sourceViewModel.PatchDetailsList, destDocument, repositoryWrapper);

            return destDocument;
        }

        /// <summary>
        /// TODO: Does not use all repositories out of the RepositoryWrapper.
        /// </summary>
        private static void ConvertToSamples(IList<SamplePropertiesViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (SamplePropertiesViewModel viewModel in viewModelList)
            {
                Sample entity = viewModel.Sample.ToEntity(repositoryWrapper);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            // Delete
            IList<int> existingIDs = destDocument.Samples.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                Sample entityToDelete = repositoryWrapper.SampleRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                repositoryWrapper.SampleRepository.Delete(entityToDelete);
            }
        }

        /// <summary>
        /// TODO: We do not need all the repositories in repositoryWrapper, just 5.
        /// </summary>
        public static Sample ToEntity(this SampleViewModel viewModel, RepositoryWrapper repositoryWrapper)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Sample sample = repositoryWrapper.SampleRepository.TryGet(viewModel.ID);
            if (sample == null)
            {
                sample = repositoryWrapper.SampleRepository.Create();
            }
            sample.Name = viewModel.Name;
            sample.Amplifier = viewModel.Amplifier;
            sample.TimeMultiplier = viewModel.TimeMultiplier;
            sample.IsActive = viewModel.IsActive;
            sample.SamplingRate = viewModel.SamplingRate;
            sample.BytesToSkip = viewModel.BytesToSkip;
            sample.Location = viewModel.Location;

            if (viewModel.AudioFileFormat != null)
            {
                sample.AudioFileFormat = repositoryWrapper.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
            }

            if (viewModel.InterpolationType != null)
            {
                sample.InterpolationType = repositoryWrapper.InterpolationTypeRepository.Get(viewModel.InterpolationType.ID);
            }

            if (viewModel.SampleDataType != null)
            {
                sample.SampleDataType = repositoryWrapper.SampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
            }

            if (viewModel.SpeakerSetup != null)
            {
                sample.SpeakerSetup = repositoryWrapper.SpeakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
            }

            return sample;
        }

        private static void ConvertToCurvesWithRelatedEntities(IList<CurveDetailsViewModel> viewModelList, Document destDocument, ICurveRepository curveRepository, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            var idsToKeep = new HashSet<int>();

            foreach (CurveDetailsViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.Curve.ToEntityWithRelatedEntities(curveRepository, nodeRepository, nodeTypeRepository);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destDocument.Samples.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                Curve entityToDelete = curveRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                entityToDelete.DeleteRelatedEntities(nodeRepository);
                curveRepository.Delete(entityToDelete);
            }
        }

        public static Curve ToEntityWithRelatedEntities(this CurveViewModel viewModel, ICurveRepository curveRepository, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            Curve curve = viewModel.ToEntity(curveRepository);

            ConvertToNodes(viewModel.Nodes, curve, nodeRepository, nodeTypeRepository);

            return curve;
        }

        public static Curve ToEntity(this CurveViewModel viewModel, ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => viewModel);

            Curve curve = curveRepository.TryGet(viewModel.ID);
            if (curve == null)
            {
                curve = curveRepository.Create();
            }
            curve.Name = viewModel.Name;

            return curve;
        }

        private static void ConvertToNodes(IList<NodeViewModel> viewModelList, Curve destCurve, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            var idsToKeep = new HashSet<int>();

            foreach (NodeViewModel viewModel in viewModelList)
            {
                Node entity = viewModel.ToEntity(nodeRepository, nodeTypeRepository);
                entity.LinkTo(destCurve);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destCurve.Nodes.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                Node entityToDelete = nodeRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                nodeRepository.Delete(entityToDelete);
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
                entity = nodeRepository.Create();
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

        /// <summary>
        /// TODO: You might want to pass repositories explicitly,
        /// since you do not need all of the ones in RepositoryWrapper.
        /// </summary>
        private static void ConvertToPatchesWithRelatedEntities(IList<PatchDetailsViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (PatchDetailsViewModel viewModel in viewModelList)
            {
                Patch entity = viewModel.Patch.ToEntityWithRelatedEntities(repositoryWrapper.PatchRepository, repositoryWrapper.OperatorRepository, repositoryWrapper.InletRepository, repositoryWrapper.OutletRepository, repositoryWrapper.EntityPositionRepository);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destDocument.Samples.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                Patch entityToDelete = repositoryWrapper.PatchRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                entityToDelete.DeleteRelatedEntities(repositoryWrapper.OperatorRepository, repositoryWrapper.InletRepository, repositoryWrapper.OutletRepository, repositoryWrapper.EntityPositionRepository);
                repositoryWrapper.PatchRepository.Delete(entityToDelete);
            }
        }

        /// <summary>
        /// TODO: Does not use all repositories out of the RepositoryWrapper.
        /// </summary>
        private static void ConvertToAudioFileOutputsWithRelatedEntities(IList<AudioFileOutputPropertiesViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (AudioFileOutputPropertiesViewModel viewModel in viewModelList)
            {
                AudioFileOutput entity = viewModel.AudioFileOutput.ToEntityWithRelatedEntities(repositoryWrapper);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destDocument.AudioFileOutputs.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                AudioFileOutput entityToDelete = repositoryWrapper.AudioFileOutputRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                repositoryWrapper.AudioFileOutputRepository.Delete(entityToDelete);
            }
        }

        /// <summary>
        /// TODO: We do not need all the repositories in repositoryWrapper.
        /// </summary>
        public static AudioFileOutput ToEntityWithRelatedEntities(this AudioFileOutputViewModel sourceViewModel, RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            AudioFileOutput destAudioFileOutput = sourceViewModel.ToEntity(repositoryWrapper);

            ConvertAudioFileOutputChannels(sourceViewModel.Channels, destAudioFileOutput, repositoryWrapper.AudioFileOutputChannelRepository, repositoryWrapper.OutletRepository);

            return destAudioFileOutput;
        }

        /// <summary>
        /// TODO: We do not need all the repositories in repositoryWrapper.
        /// </summary>
        public static AudioFileOutput ToEntity(this AudioFileOutputViewModel viewModel, RepositoryWrapper repositoryWrapper)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            AudioFileOutput audioFileOutput = repositoryWrapper.AudioFileOutputRepository.TryGet(viewModel.ID);
            if (audioFileOutput == null)
            {
                audioFileOutput = repositoryWrapper.AudioFileOutputRepository.Create();
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
                audioFileOutput.AudioFileFormat = repositoryWrapper.AudioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
            }

            if (viewModel.SampleDataType != null)
            {
                audioFileOutput.SampleDataType = repositoryWrapper.SampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
            }

            if (viewModel.SpeakerSetup != null)
            {
                audioFileOutput.SpeakerSetup = repositoryWrapper.SpeakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
            }

            return audioFileOutput;
        }

        private static void ConvertAudioFileOutputChannels(IList<AudioFileOutputChannelViewModel> viewModelList, AudioFileOutput destAudioFileOutput, IAudioFileOutputChannelRepository audioFileOutputChannelRepository, IOutletRepository outletRepository)
        {
            var idsToKeep = new HashSet<int>();

            foreach (AudioFileOutputChannelViewModel viewModel in viewModelList)
            {
                AudioFileOutputChannel entity = viewModel.ToEntity(audioFileOutputChannelRepository, outletRepository);
                entity.LinkTo(destAudioFileOutput);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destAudioFileOutput.AudioFileOutputChannels.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                AudioFileOutputChannel entityToDelete = audioFileOutputChannelRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                audioFileOutputChannelRepository.Delete(entityToDelete);
            }
        }

        public static AudioFileOutputChannel ToEntity(this AudioFileOutputChannelViewModel viewModel, IAudioFileOutputChannelRepository audioFileOutputChannelRepository, IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            AudioFileOutputChannel entity = audioFileOutputChannelRepository.Get(viewModel.ID);
            if (entity == null)
            {
                audioFileOutputChannelRepository.Create();
            }

            entity.IndexNumber = viewModel.IndexNumber;

            if (viewModel.Outlet != null)
            {
                entity.Outlet = outletRepository.Get(viewModel.Outlet.ID);
            }

            return entity;
        }

        public static Patch ToEntity(
            this PatchDetailsViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            Patch patch = viewModel.Patch.ToEntityWithRelatedEntities(patchRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository);

            return patch;
        }

        public static Patch ToEntityWithRelatedEntities(
            this PatchViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Patch patch = viewModel.ToEntity(patchRepository);

            RecursiveViewModelToEntityConverter converter = new RecursiveViewModelToEntityConverter(operatorRepository, inletRepository, outletRepository, entityPositionRepository);

            var convertedOperators = new HashSet<Operator>();

            foreach (OperatorViewModel operatorViewModel in viewModel.Operators)
            {
                Operator op = converter.Convert(operatorViewModel);
                op.LinkTo(patch);

                convertedOperators.Add(op);
            }

            IList<Operator> operatorsToDelete = patch.Operators.Except(convertedOperators).ToArray();
            foreach (Operator op in operatorsToDelete)
            {
                op.UnlinkRelatedEntities();
                op.DeleteRelatedEntities(inletRepository, outletRepository, entityPositionRepository);
                operatorRepository.Delete(op);
            }

            return patch;
        }

        public static Patch ToEntity(this PatchViewModel viewModel, IPatchRepository patchRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch entity = patchRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = patchRepository.Create();
            }
            entity.Name = viewModel.PatchName;

            return entity;
        }

        public static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Operator entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            entity.OperatorTypeName = viewModel.OperatorTypeName;
            return entity;
        }

        public static Inlet ToEntity(this InletViewModel viewModel, IInletRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Inlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            Outlet entity = repository.TryGet(viewModel.ID);
            if (entity == null)
            {
                entity = repository.Create();
            }
            entity.Name = viewModel.Name;
            return entity;
        }

        public static EntityPosition ToEntityPosition(this OperatorViewModel viewModel, IEntityPositionRepository repository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repository == null) throw new NullException(() => repository);

            var manager = new EntityPositionManager(repository);
            EntityPosition entityPosition = manager.SetOrCreateOperatorPosition(viewModel.ID, viewModel.CenterX, viewModel.CenterY);
            return entityPosition;
        }

        public static Document ToEntity(this DocumentPropertiesViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = viewModel.Document.ToDocument(documentRepository);
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
                document = documentRepository.Create();
            }

            document.Name = idAndName.Name;

            return document;
        }

        public static Document ToEntity(this ChildDocumentListViewModel viewModel, RepositoryWrapper repositoryWrapper)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Document destDocument = repositoryWrapper.DocumentRepository.Get(viewModel.ParentDocumentID);

            // TODO: Use the DocumentManager to do the CRUD operations?

            foreach (IDNameAndTemporaryID sourceListItemViewModel in viewModel.List)
            {
                Document destInstrument = repositoryWrapper.DocumentRepository.TryGet(sourceListItemViewModel.ID);
                if (destInstrument == null)
                {
                    destInstrument = repositoryWrapper.DocumentRepository.Create();
                    destInstrument.LinkInstrumentToDocument(destDocument);
                }

                destInstrument.Name = sourceListItemViewModel.Name;
            }

            var entityIDs = new HashSet<int>(destDocument.Instruments.Where(x => x.ID != 0).Select(x => x.ID));
            var viewModelIDs = new HashSet<int>(viewModel.List.Where(x => x.ID != 0).Select(x => x.ID));

            IList<int> idsToDelete = entityIDs.Except(viewModelIDs).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                Document instrumentToDelete = repositoryWrapper.DocumentRepository.Get(idToDelete);
                instrumentToDelete.DeleteRelatedEntities(repositoryWrapper);
                instrumentToDelete.UnlinkRelatedEntities();

                repositoryWrapper.DocumentRepository.Delete(instrumentToDelete);
            }

            return destDocument;
        }
    }
}
