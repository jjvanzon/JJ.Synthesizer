using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ChildDocumentHelper
    {
        // ChildDocument

        public static ChildDocumentViewModel GetChildDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ChildDocumentViewModel childDocumentViewModel = rootDocumentViewModel.ChildDocumentList
                                                                                 .Where(x => x.ID == childDocumentID)
                                                                                 .FirstOrDefault(); // First for performance.
            if (childDocumentViewModel == null)
            {
                throw new Exception(String.Format("ChildDocumentViewModel with ID '{0}' not found in documentViewModel.ChildDocumentList.", childDocumentID));
            }

            return childDocumentViewModel;
        }

        // Curve

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.ID == curveID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("CurveDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", curveID));
            }

            return detailsViewModel;
        }

        public static CurveGridViewModel GetCurveGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveGrid;
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurveDetailsList;
            }
        }

        private static IEnumerable<CurveDetailsViewModel> EnumerateCurveDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurveDetailsViewModel curveDetailsViewModel in rootDocumentViewModel.CurveDetailsList)
            {
                yield return curveDetailsViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (CurveDetailsViewModel curveDetailsViewModel in childDocumentViewModel.CurveDetailsList)
                {
                    yield return curveDetailsViewModel;
                }
            }
        }

        // Operator

        public static OperatorViewModel GetOperatorViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorViewModel viewModel = ChildDocumentHelper.EnumerateOperatorViewModels(rootDocumentViewModel)
                                                             .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            if (viewModel == null)
            {
                throw new Exception(String.Format("OperatorViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", operatorID));
            }

            return viewModel;
        }

        private static IEnumerable<OperatorViewModel> EnumerateOperatorViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorViewModel operatorViewModel in rootDocumentViewModel.PatchDetailsList.SelectMany(x => x.Entity.Operators))
            {
                yield return operatorViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorViewModel operatorViewModel in childDocumentViewModel.PatchDetailsList.SelectMany(x => x.Entity.Operators))
                {
                    yield return operatorViewModel;
                }
            }
        }

        public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels(rootDocumentViewModel)
                                                                       .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchInlet viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForPatchInlets(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchOutlet viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForPatchOutlets(rootDocumentViewModel)
                                                                                      .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForValue TryGetOperatorPropertiesViewModel_ForValue(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForValue viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForValues(rootDocumentViewModel)
                                                                                      .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        private static IEnumerable<OperatorPropertiesViewModel> EnumerateOperatorPropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel propertiesViewModel in childDocumentViewModel.OperatorPropertiesList)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForPatchInlet> EnumerateOperatorPropertiesViewModels_ForPatchInlets(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForPatchInlets)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForPatchInlets)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForPatchOutlet> EnumerateOperatorPropertiesViewModels_ForPatchOutlets(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForPatchOutlets)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForPatchOutlets)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForValue> EnumerateOperatorPropertiesViewModels_ForValues(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForValue propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForValues)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForValue propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForValues)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        public static IList<OperatorPropertiesViewModel> GetOperatorPropertiesViewModelList_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
            {
                return rootDocumentViewModel.OperatorPropertiesList;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
                {
                    return childDocumentViewModel.OperatorPropertiesList;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
            {
                return rootDocumentViewModel.OperatorPropertiesList_ForPatchInlets;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForPatchInlets;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForPatchInlet with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
            {
                return rootDocumentViewModel.OperatorPropertiesList_ForPatchOutlets;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForPatchOutlets;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForPatchOutlet with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForValue> GetOperatorPropertiesViewModelList_ForValues_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
            {
                return rootDocumentViewModel.OperatorPropertiesList_ForValues;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetailsList.Any(x => x.Entity.ID == patchID))
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForValues;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForValue with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.ID == patchID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("PatchDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
            }

            return detailsViewModel;
        }

        public static PatchGridViewModel GetPatchGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchGrid;
            }
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.PatchDetailsList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.PatchDetailsList;
            }
        }

        private static IEnumerable<PatchDetailsViewModel> EnumeratePatchDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDetailsViewModel patchDetailsViewModel in rootDocumentViewModel.PatchDetailsList)
            {
                yield return patchDetailsViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (PatchDetailsViewModel patchDetailsViewModel in childDocumentViewModel.PatchDetailsList)
                {
                    yield return patchDetailsViewModel;
                }
            }
        }

        // Sample

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.EnumerateSamplePropertiesViewModels(rootDocumentViewModel)
                                                                               .FirstOrDefault(x => x.Entity.ID == sampleID); // First for performance.
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("SamplePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", sampleID));
            }

            return propertiesViewModel;
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SamplePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SamplePropertiesList;
            }
        }

        public static SampleGridViewModel GetSampleGridViewModel_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel gridViewModel = ChildDocumentHelper.EnumerateSampleGridViewModels(rootDocumentViewModel)
                                                                   .Where(x => x.List.Any(y => y.ID == sampleID))
                                                                   .Single();
            return gridViewModel;
        }

        public static SampleGridViewModel GetSampleGridViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SampleGrid;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SampleGridViewModel> EnumerateSampleGridViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            yield return rootDocumentViewModel.SampleGrid;

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                yield return childDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SamplePropertiesViewModel> EnumerateSamplePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (SamplePropertiesViewModel samplePropertiesViewModel in rootDocumentViewModel.SamplePropertiesList)
            {
                yield return samplePropertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (SamplePropertiesViewModel samplePropertiesViewModel in childDocumentViewModel.SamplePropertiesList)
                {
                    yield return samplePropertiesViewModel;
                }
            }

        }
    }
}