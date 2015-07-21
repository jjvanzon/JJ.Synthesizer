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
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityExtensions
    {
        // Document

        public static Document ToEntityWithRelatedEntities(this MainViewModel userInput, RepositoryWrapper repositoryWrapper)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            return userInput.Document.ToEntityWithRelatedEntities(repositoryWrapper);
        }

        public static Document ToEntityWithRelatedEntities(this DocumentViewModel userInput, RepositoryWrapper repositoryWrapper)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Document destDocument = userInput.ToEntity(repositoryWrapper.DocumentRepository);

            ToEntityHelper.ToInstrumentsWithRelatedEntities(userInput.InstrumentDocumentList, destDocument, repositoryWrapper);
            ToEntityHelper.ToEffectsWithRelatedEntities(userInput.EffectDocumentList, destDocument, repositoryWrapper);
            ToEntityHelper.ToSamples(userInput.SamplePropertiesList, destDocument, new SampleRepositories(repositoryWrapper));
            ToEntityHelper.ToCurvesWithRelatedEntities(
                userInput.CurveDetailsList,
                destDocument,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.NodeRepository,
                repositoryWrapper.NodeTypeRepository);
            ToEntityHelper.ToPatchesWithRelatedEntities(userInput.PatchDetailsList, destDocument, repositoryWrapper);
            ToEntityHelper.ToAudioFileOutputsWithRelatedEntities(userInput.AudioFileOutputPropertiesList, destDocument, new AudioFileOutputRepositories(repositoryWrapper));

            return destDocument;
        }

        public static Document ToEntity(this DocumentViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            bool isNew = false;
            Document document = documentRepository.TryGet(viewModel.ID);
            if (document == null)
            {
                isNew = true;
                document = new Document();
                document.ID = viewModel.ID;
            }
            document.Name = viewModel.DocumentProperties.Document.Name;

            if (isNew)
            {
                documentRepository.Insert(document);
            }
            else
            {
                documentRepository.Update(document);
            }

            return document;
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

            bool isNew = false;
            Document document = documentRepository.TryGet(idAndName.ID);
            if (document == null)
            {
                isNew = true;
                document = new Document();
                document.ID = idAndName.ID;
            }

            document.Name = idAndName.Name;

            if (isNew)
            {
                documentRepository.Insert(document);
            }
            else
            {
                documentRepository.Update(document);
            }


            return document;
        }

        // Child Document

        public static Document ToEntity(this ChildDocumentPropertiesViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            bool isNew = false;
            Document entity = documentRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Document();
                entity.ID = viewModel.ID;
            }
            entity.Name = viewModel.Name;

            if (isNew)
            {
                documentRepository.Insert(entity);
            }
            else
            {
                documentRepository.Update(entity);
            }

            return entity;
        }

        public static Document ToEntityWithRelatedEntities(this ChildDocumentViewModel sourceViewModel, RepositoryWrapper repositoryWrapper)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            Document destDocument = sourceViewModel.ToEntity(repositoryWrapper.DocumentRepository);

            ToEntityHelper.ToSamples(sourceViewModel.SamplePropertiesList, destDocument, new SampleRepositories(repositoryWrapper));
            ToEntityHelper.ToCurvesWithRelatedEntities(
                sourceViewModel.CurveDetailsList,
                destDocument,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.NodeRepository,
                repositoryWrapper.NodeTypeRepository);
            ToEntityHelper.ToPatchesWithRelatedEntities(sourceViewModel.PatchDetailsList, destDocument, repositoryWrapper);

            return destDocument;
        }

        public static Document ToEntity(this ChildDocumentViewModel viewModel, IDocumentRepository documentRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            bool isNew = false;
            Document childDocument = documentRepository.TryGet(viewModel.ID);
            if (childDocument == null)
            {
                isNew = true;
                childDocument = new Document();
                childDocument.ID = viewModel.ID;
            }

            if (isNew)
            {
                documentRepository.Insert(childDocument);
            }
            else
            {
                documentRepository.Update(childDocument);
            }

            childDocument.Name = viewModel.Name;

            return childDocument;
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

            bool isNew = false;
            Sample sample = sampleRepositories.SampleRepository.TryGet(viewModel.ID);
            if (sample == null)
            {
                isNew = true;
                sample = new Sample();
                sample.ID = viewModel.ID;
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

            if (isNew)
            {
                sampleRepositories.SampleRepository.Insert(sample);
            }
            else
            {
                sampleRepositories.SampleRepository.Update(sample);
            }

            return sample;
        }

        // Curve

        public static Curve ToEntityWithRelatedEntities(
            this CurveDetailsViewModel viewModel,
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            return viewModel.Entity.ToEntityWithRelatedEntities(curveRepository, nodeRepository, nodeTypeRepository);
        }

        public static Curve ToEntityWithRelatedEntities(
            this CurveViewModel viewModel,
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Curve curve = viewModel.ToEntity(curveRepository);

            ToEntityHelper.ToNodes(viewModel.Nodes, curve, nodeRepository, nodeTypeRepository);

            return curve;
        }

        public static Curve ToEntity(this CurveViewModel viewModel, ICurveRepository curveRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (curveRepository == null) throw new NullException(() => viewModel);

            bool isNew = false;
            Curve curve = curveRepository.TryGet(viewModel.ID);
            if (curve == null)
            {
                isNew = true;
                curve = new Curve();
                curve.ID = viewModel.ID;
            }
            curve.Name = viewModel.Name;

            if (isNew)
            {
                curveRepository.Insert(curve);
            }
            else
            {
                curveRepository.Update(curve);
            }

            return curve;
        }

        public static Node ToEntity(this NodeViewModel viewModel, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            bool isNew = false;
            Node entity = nodeRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Node();
                entity.ID = viewModel.ID;
            }
            entity.Time = viewModel.Time;
            entity.Value = viewModel.Value;
            entity.Direction = viewModel.Direction;

            if (entity.NodeType != null)
            {
                entity.NodeType = nodeTypeRepository.Get(viewModel.NodeType.ID);
            }

            if (isNew)
            {
                nodeRepository.Insert(entity);
            }
            else
            {
                nodeRepository.Update(entity);
            }

            return entity;
        }

        // AudioFileOutput

        public static AudioFileOutput ToEntityWithRelatedEntities(this AudioFileOutputPropertiesViewModel viewModel, AudioFileOutputRepositories audioFileOutputRepositories)
        {
            return viewModel.Entity.ToEntityWithRelatedEntities(audioFileOutputRepositories);
        }

        public static AudioFileOutput ToEntityWithRelatedEntities(this AudioFileOutputViewModel sourceViewModel, AudioFileOutputRepositories audioFileOutputRepositories)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (audioFileOutputRepositories == null) throw new NullException(() => audioFileOutputRepositories);

            AudioFileOutput destAudioFileOutput = sourceViewModel.ToEntity(audioFileOutputRepositories);

            ToEntityHelper.ToAudioFileOutputChannels(
                sourceViewModel.Channels,
                destAudioFileOutput,
                audioFileOutputRepositories.AudioFileOutputChannelRepository,
                audioFileOutputRepositories.OutletRepository);

            return destAudioFileOutput;
        }

        public static AudioFileOutput ToEntity(this AudioFileOutputViewModel viewModel, AudioFileOutputRepositories audioFileOutputRepositories)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputRepositories == null) throw new NullException(() => audioFileOutputRepositories);

            bool isNew = false;
            AudioFileOutput audioFileOutput = audioFileOutputRepositories.AudioFileOutputRepository.TryGet(viewModel.ID);
            if (audioFileOutput == null)
            {
                isNew = true;
                audioFileOutput = new AudioFileOutput();
                audioFileOutput.ID = viewModel.ID;
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

            //audioFileOutput.Document = audioFileOutputRepositories.DocumentRepository.TryGet(viewModel.Keys.DocumentID);

            if (isNew)
            {
                audioFileOutputRepositories.AudioFileOutputRepository.Insert(audioFileOutput);
            }
            else
            {
                audioFileOutputRepositories.AudioFileOutputRepository.Update(audioFileOutput);
            }

            return audioFileOutput;
        }

        public static AudioFileOutputChannel ToEntity(this AudioFileOutputChannelViewModel viewModel, IAudioFileOutputChannelRepository audioFileOutputChannelRepository, IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            bool isNew = false;
            AudioFileOutputChannel entity = audioFileOutputChannelRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new AudioFileOutputChannel();
                entity.ID = viewModel.ID;
            }

            entity.IndexNumber = viewModel.IndexNumber;

            if (viewModel.Outlet != null)
            {
                entity.Outlet = outletRepository.Get(viewModel.Outlet.ID);
            }

            if (isNew)
            {
                audioFileOutputChannelRepository.Insert(entity);
            }
            else
            {
                audioFileOutputChannelRepository.Update(entity);
            }

            return entity;
        }

        // Patch

        public static Patch ToEntity(
            this PatchDetailsViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.Entity.ToEntityWithRelatedEntities(
                patchRepository,
                operatorRepository,
                operatorTypeRepository,
                inletRepository,
                outletRepository,
                entityPositionRepository);

            return patch;
        }

        public static Patch ToEntityWithRelatedEntities(
            this PatchViewModel viewModel,
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            Patch patch = viewModel.ToEntity(patchRepository);

            RecursiveViewModelToEntityConverter converter = new RecursiveViewModelToEntityConverter(
                operatorRepository, 
                operatorTypeRepository, 
                inletRepository, 
                outletRepository, 
                entityPositionRepository);

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
            if (patchRepository == null) throw new NullException(() => patchRepository);

            bool isNew = false;
            Patch entity = patchRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Patch();
                entity.ID = viewModel.ID;
            }
            entity.Name = viewModel.Name;

            if (isNew)
            {
                patchRepository.Insert(entity);
            }
            else
            {
                patchRepository.Update(entity);
            }

            return entity;
        }

        public static Operator ToEntityWithInletsAndOutlets(
            this OperatorViewModel viewModel,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Operator op = viewModel.ToEntity(operatorRepository, operatorTypeRepository);

            foreach (InletViewModel inletViewModel in viewModel.Inlets)
            {
                Inlet inlet = inletViewModel.ToEntity(inletRepository);
                inlet.LinkTo(op);
            }

            foreach (OutletViewModel outletViewModel in viewModel.Outlets)
            {
                Outlet outlet = outletViewModel.ToEntity(outletRepository);
                outlet.LinkTo(op);
            }

            return op;
        }

        public static Operator ToEntity(this OperatorViewModel viewModel, IOperatorRepository operatorRepository, IOperatorTypeRepository operatorTypeRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);

            bool isNew = false;
            Operator entity = operatorRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Operator();
                entity.ID = viewModel.ID;
            }

            entity.Name = viewModel.Name;
            entity.OperatorType = operatorTypeRepository.TryGet(viewModel.OperatorTypeID);

            if (entity.GetOperatorTypeEnum() == OperatorTypeEnum.Value)
            {
                entity.Data = viewModel.Value;
            }

            if (isNew)
            {
                operatorRepository.Insert(entity);
            }
            else
            {
                operatorRepository.Update(entity);
            }

            return entity;
        }

        public static Inlet ToEntity(this InletViewModel viewModel, IInletRepository inletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (inletRepository == null) throw new NullException(() => inletRepository);

            bool isNew = false;
            Inlet entity = inletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Inlet();
                entity.ID = viewModel.ID;
            }
            entity.Name = viewModel.Name;

            if (isNew)
            {
                inletRepository.Insert(entity);
            }
            else
            {
                inletRepository.Update(entity);
            }

            return entity;
        }

        public static Outlet ToEntity(this OutletViewModel viewModel, IOutletRepository outletRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            bool isNew = false;
            Outlet entity = outletRepository.TryGet(viewModel.ID);
            if (entity == null)
            {
                isNew = true;
                entity = new Outlet();
                entity.ID = viewModel.ID;
            }
            entity.Name = viewModel.Name;

            if (isNew)
            {
                outletRepository.Insert(entity);
            }
            else
            {
                outletRepository.Update(entity);
            }

            return entity;
        }

        public static EntityPosition ToEntityPosition(this OperatorViewModel viewModel, IEntityPositionRepository entityPositionRepository)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            bool isNew = false;
            EntityPosition entityPosition = entityPositionRepository.TryGet(viewModel.EntityPositionID);
            if (entityPosition == null)
            {
                isNew = true;
                entityPosition = new EntityPosition();
                entityPosition.ID = viewModel.EntityPositionID;
            }
            entityPosition.X = viewModel.CenterX;
            entityPosition.Y = viewModel.CenterY;
            entityPosition.EntityTypeName = typeof(Operator).Name;
            entityPosition.EntityID = viewModel.ID;

            if (isNew)
            {
                entityPositionRepository.Insert(entityPosition);
            }
            else
            {
                entityPositionRepository.Update(entityPosition);
            }

            return entityPosition;
        }
    }
}