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
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Helpers;

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

        public static void ToSamples(IList<SamplePropertiesViewModel> viewModelList, Document destDocument, SampleRepositories repositories)
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

        public static void ToCurvesWithRelatedEntities(IList<CurveDetailsViewModel> viewModelList, Document destDocument, CurveRepositories repositories)
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

        /// <summary>
        /// TODO: You might want to pass repositories explicitly,
        /// since you do not need all of the ones in RepositoryWrapper.
        /// </summary>
        public static void ToPatchesWithRelatedEntities(
            IList<PatchDetailsViewModel> viewModelList, 
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

        /// <summary>
        /// TODO: Does not use all repositories out of the RepositoryWrapper.
        /// </summary>
        public static void ToAudioFileOutputsWithRelatedEntities(
            IList<AudioFileOutputPropertiesViewModel> viewModelList, 
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
            IList<AudioFileOutputChannelViewModel> viewModelList, 
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

        public static void ToNodes(IList<NodeViewModel> viewModelList, Curve destCurve, CurveRepositories repositories)
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

        /// <summary>
        /// Convert OperatorViewModel from PatchDetail to entity, because we are about to validate
        /// the inlets and outlets too, which are not defined in the OperatorPropertiesViewModel.
        /// Also, we need to associate the Operator with its patch (derived from the PatchDetailsViewModel),
        /// because other code uses the PatchManager.
        /// 
        /// TODO: If lookups in view models were fast, you might not need to return the OperatorViewModel for performance,
        /// but instead do a second lookups, so it becomes a regular ToEntity method,
        /// which return the entity and move this to the ToEntityExcentions.
        /// </summary>
        public static OperatorEntityAndViewModel ToOperatorWithInletsAndOutletsAndPatch(DocumentViewModel documentViewModel, int operatorID, PatchRepositories repositories)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);
            if (repositories == null) throw new NullException(() => repositories);

            PatchDetailsViewModel patchDetailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel_ByOperatorID(documentViewModel, operatorID);
            OperatorViewModel operatorViewModel = patchDetailsViewModel.Entity.Operators.Where(x => x.ID == operatorID).First();
            Patch patch = patchDetailsViewModel.Entity.ToEntity(repositories.PatchRepository);
            Operator op = operatorViewModel.ToEntityWithInletsAndOutlets(repositories);
            PatchManager patchManager = new PatchManager(patch, repositories);
            patchManager.SaveOperator(op);

            return new OperatorEntityAndViewModel(op, operatorViewModel);
        }
    }
}
