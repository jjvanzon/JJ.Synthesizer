//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Presentation.Synthesizer.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.ToEntity
//{
//    internal static class ToEntityHelper
//    {
//        public static void ConvertToEntitiesWithRelatedEntities(
//            IEnumerable<PatchDetailsViewModel> patchDetailsViewModelCollection,
//            IEnumerable<PatchPropertiesViewModel> patchPropertiesViewModelCollection,
//            Document destDocument,
//            PatchRepositories repositories)
//        {
//            if (patchDetailsViewModelCollection == null) throw new NullException(() => patchDetailsViewModelCollection);
//            if (patchPropertiesViewModelCollection == null) throw new NullException(() => patchPropertiesViewModelCollection);
//            if (destDocument == null) throw new NullException(() => destDocument);
//            if (repositories == null) throw new NullException(() => repositories);
            
//            var idsToKeep = new HashSet<int>();

//            var tuples = from patchDetailsViewModel in patchDetailsViewModelCollection
//                         join patchPropertiesViewModel in patchPropertiesViewModelCollection
//                         on patchDetailsViewModel.Entity.ID equals patchPropertiesViewModel.ID
//                         select new { patchDetailsViewModel, patchPropertiesViewModel };

//            foreach (var tuple in tuples)
//            {
//                Patch patch = ConvertToEntityWithRelatedEntities(
//                    tuple.patchDetailsViewModel,
//                    tuple.patchPropertiesViewModel,
//                    repositories);

//                patch.LinkTo(destDocument);

//                idsToKeep.Add(patch.ID);
//            }

//            var patchManager = new PatchManager(repositories);

//            IList<int> existingIDs = destDocument.Patches.Select(x => x.ID).ToArray();
//            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();
//            foreach (int idToDelete in idsToDelete)
//            {
//                patchManager.PatchID = idToDelete;
//                patchManager.DeletePatchWithRelatedEntities();
//            }
//        }

//        public static Patch ConvertToEntityWithRelatedEntities(
//            PatchDetailsViewModel patchDetailsViewModel,
//            PatchPropertiesViewModel patchPropertiesViewModel,
//            PatchRepositories repositories)
//        {
//            if (patchDetailsViewModel == null) throw new NullException(() => patchDetailsViewModel);
//            if (patchPropertiesViewModel == null) throw new NullException(() => patchPropertiesViewModel);
//            if (repositories == null) throw new NullException(() => repositories);

//            ToPatchWithRelatedEntitiesResult result = patchDetailsViewModel.ToEntityWithRelatedEntities(repositories);
//            Patch patch = result.Patch;
//            IList<Operator> operatorsToDelete = result.OperatorsToDelete;

//            patchPropertiesViewModel.ToEntity(repositories.PatchRepository);

//            // Order-Dependence: 
//            // Deleting operators is deferred from converting PatchDetails to after converting operator property boxes,
//            // because deleting an operator has the side-effect of updating the dependent CustomOperators,
//            // which requires data from the PatchInlet and PatchOutlet PropertiesViewModels to be
//            // converted first.
//            var patchManager = new PatchManager(patch, repositories);

//            foreach (Operator op in operatorsToDelete)
//            {
//                patchManager.DeleteOperatorWithRelatedEntities(op);
//            }

//            return patch;
//        }
//    }
//}
