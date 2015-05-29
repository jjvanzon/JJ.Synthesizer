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

            ToEntityHelper.ToInstrumentsWithRelatedEntities(sourceViewModel.InstrumentDocumentList, destDocument, repositoryWrapper);
            ToEntityHelper.ToEffectsWithRelatedEntities(sourceViewModel.EffectDocumentList, destDocument, repositoryWrapper);
            ToEntityHelper.ToSamples(sourceViewModel.SamplePropertiesList, destDocument, repositoryWrapper);
            ToEntityHelper.ToCurvesWithRelatedEntities(sourceViewModel.CurveDetailsList, destDocument, repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository);
            ToEntityHelper.ToPatchesWithRelatedEntities(sourceViewModel.PatchDetailsList, destDocument, repositoryWrapper);
            ToEntityHelper.ToAudioFileOutputsWithRelatedEntities(sourceViewModel.AudioFileOutputPropertiesList, destDocument, repositoryWrapper.AudioFileOutputRepository, repositoryWrapper.AudioFileFormatRepository, repositoryWrapper.SampleDataTypeRepository, repositoryWrapper.SpeakerSetupRepository, repositoryWrapper.AudioFileOutputChannelRepository, repositoryWrapper.OutletRepository);

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

            Document destDocument = sourceViewModel.Document.ToEntity(repositoryWrapper.DocumentRepository);

            ToEntityHelper.ToSamples(sourceViewModel.SamplePropertiesList, destDocument, repositoryWrapper);
            ToEntityHelper.ToCurvesWithRelatedEntities(sourceViewModel.CurveDetailsList, destDocument, repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository);
            ToEntityHelper.ToPatchesWithRelatedEntities(sourceViewModel.PatchDetailsList, destDocument, repositoryWrapper);

            return destDocument;
        }

        public static Document ToEntity(this IDNameAndListIndexViewModel idNameAndListIndex, IDocumentRepository documentRepository)
        {
            if (idNameAndListIndex == null) throw new NullException(() => idNameAndListIndex);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            Document document = documentRepository.TryGet(idNameAndListIndex.ID);
            if (document == null)
            {
                document = documentRepository.Create();
            }

            document.Name = idNameAndListIndex.Name;

            return document;
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

        public static Curve ToEntityWithRelatedEntities(this CurveDetailsViewModel viewModel, ICurveRepository curveRepository, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            return viewModel.Curve.ToEntityWithRelatedEntities(curveRepository, nodeRepository, nodeTypeRepository);
        }

        public static Curve ToEntityWithRelatedEntities(this CurveViewModel viewModel, ICurveRepository curveRepository, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            Curve curve = viewModel.ToEntity(curveRepository);

            ToEntityHelper.ToNodes(viewModel.Nodes, curve, nodeRepository, nodeTypeRepository);

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

        public static AudioFileOutput ToEntityWithRelatedEntities(
            this AudioFileOutputPropertiesViewModel viewModel,
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOutletRepository outletRepository)
        {
            return viewModel.AudioFileOutput.ToEntityWithRelatedEntities(audioFileOutputRepository, audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository, audioFileOutputChannelRepository, outletRepository);
        }

        public static AudioFileOutput ToEntityWithRelatedEntities(
            this AudioFileOutputViewModel sourceViewModel,
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOutletRepository outletRepository)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            AudioFileOutput destAudioFileOutput = sourceViewModel.ToEntity(audioFileOutputRepository, audioFileFormatRepository, sampleDataTypeRepository, speakerSetupRepository);

            ToEntityHelper.ToAudioFileOutputChannels(sourceViewModel.Channels, destAudioFileOutput, audioFileOutputChannelRepository, outletRepository);

            return destAudioFileOutput;
        }

        public static AudioFileOutput ToEntity(
            this AudioFileOutputViewModel viewModel,
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            AudioFileOutput audioFileOutput = audioFileOutputRepository.TryGet(viewModel.ID);
            if (audioFileOutput == null)
            {
                audioFileOutput = audioFileOutputRepository.Create();
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
                audioFileOutput.AudioFileFormat = audioFileFormatRepository.Get(viewModel.AudioFileFormat.ID);
            }

            if (viewModel.SampleDataType != null)
            {
                audioFileOutput.SampleDataType = sampleDataTypeRepository.Get(viewModel.SampleDataType.ID);
            }

            if (viewModel.SpeakerSetup != null)
            {
                audioFileOutput.SpeakerSetup = speakerSetupRepository.Get(viewModel.SpeakerSetup.ID);
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
                entity = audioFileOutputChannelRepository.Create();
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
    }
}
