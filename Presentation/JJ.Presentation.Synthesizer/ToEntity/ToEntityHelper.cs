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
    internal static class ToEntityHelper
    {
        /// <summary>
        /// Converts the InstrumentPropertiesList and then Instrument(Document)s to entities.
        /// </summary>
        public static void ConvertToInstrumentsWithRelatedEntities(DocumentViewModel sourceParentDocumentViewModel, Document destParentDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            // TODO: Remove outcommented code if it proves that just using the InstrumentDocumentList
            // for saving is a good solution.

            //foreach (ChildDocumentPropertiesViewModel instrumentPropertiesViewModel in sourceViewModel.InstrumentPropertiesList)
            //{
            //    Document entity = instrumentPropertiesViewModel.ToEntity(repositoryWrapper.DocumentRepository);

            //    entity.LinkInstrumentToDocument(destDocument);

            //    if (!idsToKeep.Contains(entity.ID))
            //    {
            //        idsToKeep.Add(entity.ID);
            //    }
            //}

            foreach (ChildDocumentViewModel instrumentDocumentViewModel in sourceParentDocumentViewModel.InstrumentDocumentList)
            {
                Document entity = instrumentDocumentViewModel.ToEntityWithRelatedEntities(repositoryWrapper);

                entity.LinkInstrumentToDocument(destParentDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            DocumentManager documentManager = new DocumentManager(repositoryWrapper);

            IList<int> existingIDs = destParentDocument.Instruments.Select(x => x.ID).ToArray();
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        /// <summary>
        /// Converts the InstrumentPropertiesList and then Instrument(Document)s to entities.
        /// </summary>
        public static void ConvertToEffectsWithRelatedEntities(DocumentViewModel sourceParentDocumentViewModel, Document destParentDocument, RepositoryWrapper repositoryWrapper)
        {
            var idsToKeep = new HashSet<int>();

            foreach (ChildDocumentPropertiesViewModel effectPropertiesViewModel in sourceParentDocumentViewModel.EffectPropertiesList)
            {
                Document entity = effectPropertiesViewModel.ToEntity(repositoryWrapper.DocumentRepository);
                entity.LinkEffectToDocument(destParentDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            foreach (ChildDocumentViewModel effectDocumentViewModel in sourceParentDocumentViewModel.EffectDocumentList)
            {
                Document entity = effectDocumentViewModel.ToEntityWithRelatedEntities(repositoryWrapper);
                entity.LinkEffectToDocument(destParentDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            var documentManager = new DocumentManager(repositoryWrapper);

            IEnumerable<int> existingIDs = destParentDocument.Effects.Select(x => x.ID).ToArray();
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
            foreach (int idToDelete in idsToDelete)
            {
                documentManager.DeleteWithRelatedEntities(idToDelete);
            }
        }

        /// <summary>
        /// TODO: Does not use all repositories out of the RepositoryWrapper.
        /// </summary>
        public static void ConvertToSamples(IList<SamplePropertiesViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
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

        public static void ConvertToCurvesWithRelatedEntities(IList<CurveDetailsViewModel> viewModelList, Document destDocument, ICurveRepository curveRepository, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
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

        /// <summary>
        /// TODO: You might want to pass repositories explicitly,
        /// since you do not need all of the ones in RepositoryWrapper.
        /// </summary>
        public static void ConvertToPatchesWithRelatedEntities(IList<PatchDetailsViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
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
        public static void ConvertToAudioFileOutputsWithRelatedEntities(IList<AudioFileOutputPropertiesViewModel> viewModelList, Document destDocument, RepositoryWrapper repositoryWrapper)
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

        public static void ConvertAudioFileOutputChannels(IList<AudioFileOutputChannelViewModel> viewModelList, AudioFileOutput destAudioFileOutput, IAudioFileOutputChannelRepository audioFileOutputChannelRepository, IOutletRepository outletRepository)
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

        public static void ConvertToNodes(IList<NodeViewModel> viewModelList, Curve destCurve, INodeRepository nodeRepository, INodeTypeRepository nodeTypeRepository)
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
    }
}
