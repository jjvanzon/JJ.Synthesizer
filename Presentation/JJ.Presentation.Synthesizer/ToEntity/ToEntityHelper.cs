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
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.ToEntity
{
    internal static class ToEntityHelper
    {
        /// <summary> Leading for saving when it comes to the simple properties. </summary>
        public static void ToChildDocuments(
            IList<ChildDocumentPropertiesViewModel> sourceViewModelList,
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
            IList<ChildDocumentViewModel> sourceViewModelList,
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

        public static void ToSamples(IList<SamplePropertiesViewModel> viewModelList, Document destDocument, SampleRepositories sampleRepositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (sampleRepositories == null) throw new NullException(() => sampleRepositories);

            var idsToKeep = new HashSet<int>();

            foreach (SamplePropertiesViewModel viewModel in viewModelList)
            {
                Sample entity = viewModel.Entity.ToEntity(sampleRepositories);
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
                Sample entityToDelete = sampleRepositories.SampleRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                sampleRepositories.SampleRepository.Delete(entityToDelete);
            }
        }

        public static void ToCurvesWithRelatedEntities(
            IList<CurveDetailsViewModel> viewModelList, 
            Document destDocument, 
            ICurveRepository curveRepository,
            INodeRepository nodeRepository, 
            INodeTypeRepository nodeTypeRepository)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            var idsToKeep = new HashSet<int>();

            foreach (CurveDetailsViewModel viewModel in viewModelList)
            {
                Curve entity = viewModel.Entity.ToEntityWithRelatedEntities(curveRepository, nodeRepository, nodeTypeRepository);
                entity.LinkTo(destDocument);

                if (!idsToKeep.Contains(entity.ID))
                {
                    idsToKeep.Add(entity.ID);
                }
            }

            IList<int> existingIDs = destDocument.Curves.Select(x => x.ID).ToArray();
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
        public static void ToPatchesWithRelatedEntities(
            IList<PatchDetailsViewModel> viewModelList, 
            Document destDocument,
            PatchRepositories patchRepositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (patchRepositories == null) throw new NullException(() => patchRepositories);

            var idsToKeep = new HashSet<int>();

            foreach (PatchDetailsViewModel viewModel in viewModelList)
            {
                Patch entity = viewModel.Entity.ToEntityWithRelatedEntities(patchRepositories);
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
                Patch entityToDelete = patchRepositories.PatchRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                entityToDelete.DeleteRelatedEntities(
                    patchRepositories.OperatorRepository, 
                    patchRepositories.InletRepository, 
                    patchRepositories.OutletRepository, 
                    patchRepositories.EntityPositionRepository);
                patchRepositories.PatchRepository.Delete(entityToDelete);
            }
        }

        /// <summary>
        /// TODO: Does not use all repositories out of the RepositoryWrapper.
        /// </summary>
        public static void ToAudioFileOutputsWithRelatedEntities(
            IList<AudioFileOutputPropertiesViewModel> viewModelList, 
            Document destDocument, 
            AudioFileOutputRepositories audioFileOutputRepositories)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destDocument == null) throw new NullException(() => destDocument);
            if (audioFileOutputRepositories == null) throw new NullException(() => audioFileOutputRepositories);

            var idsToKeep = new HashSet<int>();

            foreach (AudioFileOutputPropertiesViewModel viewModel in viewModelList)
            {
                AudioFileOutput entity = viewModel.Entity.ToEntityWithRelatedEntities(audioFileOutputRepositories);
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
                AudioFileOutput entityToDelete = audioFileOutputRepositories.AudioFileOutputRepository.Get(idToDelete);
                entityToDelete.UnlinkRelatedEntities();
                entityToDelete.DeleteRelatedEntities(audioFileOutputRepositories.AudioFileOutputChannelRepository);
                audioFileOutputRepositories.AudioFileOutputRepository.Delete(entityToDelete);
            }
        }

        public static void ToAudioFileOutputChannels(
            IList<AudioFileOutputChannelViewModel> viewModelList, 
            AudioFileOutput destAudioFileOutput, 
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository, 
            IOutletRepository outletRepository)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destAudioFileOutput == null) throw new NullException(() => destAudioFileOutput);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

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

        public static void ToNodes(
            IList<NodeViewModel> viewModelList, 
            Curve destCurve, 
            INodeRepository nodeRepository, 
            INodeTypeRepository
            nodeTypeRepository)
        {
            if (viewModelList == null) throw new NullException(() => viewModelList);
            if (destCurve == null) throw new NullException(() => destCurve);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

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

        /// <summary>
        /// Hack back in a PatchOutlet's Outlet, that was excluded from the view model.
        /// </summary>
        public static Outlet HACK_CreatePatchOutletOutletIfNeeded(
            Operator op, IOutletRepository outletRepository, IIDRepository idRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
            {
                Outlet outlet = op.Outlets.Where(x => String.Equals(x.Name, PropertyNames.Result)).FirstOrDefault();
                if (outlet == null)
                {
                    outlet = new Outlet();
                    outlet.ID = idRepository.GetID();
                    outlet.Name = PropertyNames.Result;
                    outlet.SortOrder = 1;
                    outletRepository.Insert(outlet);
                    outlet.LinkTo(op);
                }

                return outlet;
            }

            return null;
        }

        /// <summary>
        /// Hack back in a PatchInlet's Inlet, that was excluded from the view model.
        /// </summary>
        public static Inlet HACK_CreatePatchInletInletIfNeeded(Operator op, IInletRepository inletRepository, IIDRepository idRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            // 
            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet)
            {
                Inlet inlet = op.Inlets.Where(x => String.Equals(x.Name, PropertyNames.Input)).FirstOrDefault();
                if (inlet == null)
                {
                    inlet = new Inlet();
                    inlet.ID = idRepository.GetID();
                    inlet.Name = PropertyNames.Input;
                    inlet.SortOrder = 1;
                    inletRepository.Insert(inlet);
                    inlet.LinkTo(op);
                }
                return inlet;
            }

            return null;
        }
    }
}
