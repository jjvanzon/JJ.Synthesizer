using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Helpers
{
    // TODO: Low priority. A lot of sequential lookups are done here. In the future it might be an idea to use Dictionaries instead
    // of Lists in these view models, to make it O(1) instead of O(n)
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

        public static ChildDocumentPropertiesViewModel TryGetChildDocumentPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.ChildDocumentPropertiesList.Where(x => x.ID == childDocumentID).FirstOrDefault();
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

        public static CurvePropertiesViewModel GetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurvePropertiesViewModel propertiesViewModel = ChildDocumentHelper.EnumerateCurvePropertiesViewModels(rootDocumentViewModel)
                                                                               .FirstOrDefault(x => x.Entity.ID == curveID); // First for performance.
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("CurvePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", curveID));
            }

            return propertiesViewModel;
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel_ByNodeID(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                        .Where(x => x.Entity.Nodes.Any(y => y.ID == nodeID))
                                                                        .First();
            return detailsViewModel;
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

        public static IList<CurvePropertiesViewModel> GetCurvePropertiesViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurvePropertiesList;
            }
            else
            {
                ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);
                return childDocumentViewModel.CurvePropertiesList;
            }
        }

        public static CurveGridViewModel GetCurveGridViewModel_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveGridViewModel gridViewModel = ChildDocumentHelper.EnumerateCurveGridViewModels(rootDocumentViewModel)
                                                                   .Where(x => x.List.Any(y => y.ID == curveID))
                                                                   .First();
            return gridViewModel;
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

        private static IEnumerable<CurveGridViewModel> EnumerateCurveGridViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            yield return rootDocumentViewModel.CurveGrid;

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                yield return childDocumentViewModel.CurveGrid;
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

        private static IEnumerable<CurvePropertiesViewModel> EnumerateCurvePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurvePropertiesViewModel curvePropertiesViewModel in rootDocumentViewModel.CurvePropertiesList)
            {
                yield return curvePropertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (CurvePropertiesViewModel curvePropertiesViewModel in childDocumentViewModel.CurvePropertiesList)
                {
                    yield return curvePropertiesViewModel;
                }
            }
        }

        // Node

        public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            NodePropertiesViewModel viewModel = ChildDocumentHelper.EnumerateNodePropertiesViewModels(rootDocumentViewModel)
                                                                   .FirstOrDefault(x => x.Entity.ID == nodeID); // First for performance.
            if (viewModel == null)
            {
                throw new Exception(String.Format("NodePropertiesViewModel with Entity.ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", nodeID));
            }

            return viewModel;
        }

        private static IEnumerable<NodePropertiesViewModel> EnumerateNodePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (NodePropertiesViewModel propertiesViewModel in rootDocumentViewModel.NodePropertiesList)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (NodePropertiesViewModel propertiesViewModel in childDocumentViewModel.NodePropertiesList)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        public static IList<NodePropertiesViewModel> GetNodePropertiesViewModelList_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurveDetailsList.Any(x => x.Entity.ID == curveID))
            {
                return rootDocumentViewModel.NodePropertiesList;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.CurveDetailsList.Any(x => x.Entity.ID == curveID))
                {
                    return childDocumentViewModel.NodePropertiesList;
                }
            }

            throw new Exception(String.Format("IList<NodePropertiesViewModel> for Curve ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", curveID));
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

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorViewModel operatorViewModel in childDocumentViewModel.PatchDetails.Entity.Operators)
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

        public static OperatorPropertiesViewModel_ForBundle TryGetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForBundle viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForBundles(rootDocumentViewModel)
                                                                                 .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCurve viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForCurves(rootDocumentViewModel)
                                                                                .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator TryGetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCustomOperator viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForCustomOperators(rootDocumentViewModel)
                                                                                         .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForNumber viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForNumbers(rootDocumentViewModel)
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

        public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForSample viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForSamples(rootDocumentViewModel)
                                                                                 .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForUnbundle TryGetOperatorPropertiesViewModel_ForUnbundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForUnbundle viewModel = ChildDocumentHelper.EnumerateOperatorPropertiesViewModels_ForUnbundles(rootDocumentViewModel)
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

        private static IEnumerable<OperatorPropertiesViewModel_ForBundle> EnumerateOperatorPropertiesViewModels_ForBundles(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForBundle propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForBundles)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForBundle propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForBundles)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForCurve> EnumerateOperatorPropertiesViewModels_ForCurves(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForCurves)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForCurve propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForCurves)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForCustomOperator> EnumerateOperatorPropertiesViewModels_ForCustomOperators(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForCustomOperators)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForCustomOperators)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForNumber> EnumerateOperatorPropertiesViewModels_ForNumbers(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForNumbers)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForNumber propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForNumbers)
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

        private static IEnumerable<OperatorPropertiesViewModel_ForSample> EnumerateOperatorPropertiesViewModels_ForSamples(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForSamples)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForSample propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForSamples)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForUnbundle> EnumerateOperatorPropertiesViewModels_ForUnbundles(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (OperatorPropertiesViewModel_ForUnbundle propertiesViewModel in rootDocumentViewModel.OperatorPropertiesList_ForUnbundles)
            {
                yield return propertiesViewModel;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForUnbundle propertiesViewModel in childDocumentViewModel.OperatorPropertiesList_ForUnbundles)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        public static IList<OperatorPropertiesViewModel> GetOperatorPropertiesViewModelList_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel> for Patch ID '{0}' not found in any of the ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForBundle> GetOperatorPropertiesViewModelList_ForBundles_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForBundles;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForBundle> for Patch ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForCurve> GetOperatorPropertiesViewModelList_ForCurves_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForCurves;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCurve> for Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> GetOperatorPropertiesViewModelList_ForCustomOperators_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForCustomOperators;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCustomOperator> for Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForNumber> GetOperatorPropertiesViewModelList_ForNumbers_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForNumbers;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForNumber> for Patch ID '{0}' not found in rootDocumentViewModel nor its ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForPatchInlets;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchInlet> for Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForPatchOutlets;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchOutlet> for Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForSample> GetOperatorPropertiesViewModelList_ForSamples_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForSamples;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForSample> with Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForUnbundle> GetOperatorPropertiesViewModelList_ForUnbundles_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                if (childDocumentViewModel.PatchDetails.Entity.ID == patchID)
                {
                    return childDocumentViewModel.OperatorPropertiesList_ForUnbundles;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForUnbundle> for Patch ID '{0}' not found in ChildDocumentViewModels.", patchID));
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.ID == patchID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("PatchDetailsViewModel with ID '{0}' not found in ChildDocumentViewModels.", patchID));
            }

            return detailsViewModel;
        }

        public static IList<PatchDetailsViewModel> GetPatchDetailsViewModels_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ChildDocumentViewModel childDocumentViewModel = GetChildDocumentViewModel(rootDocumentViewModel, documentID);

            // TODO: Turn this method into something that returns an item instead of a list.
            return new List<PatchDetailsViewModel> { childDocumentViewModel.PatchDetails };
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .Where(x => x.Entity.Operators.Any(y => y.ID == operatorID))
                                                                        .First();
            return detailsViewModel;
        }

        private static IEnumerable<PatchDetailsViewModel> EnumeratePatchDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (ChildDocumentViewModel childDocumentViewModel in rootDocumentViewModel.ChildDocumentList)
            {
                yield return childDocumentViewModel.PatchDetails;
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
                                                                   .First();
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

        // Scale

        public static ToneGridEditViewModel GetToneGridEditViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ToneGridEditViewModel viewModel = rootDocumentViewModel.ToneGridEditList.Where(x => x.ScaleID == scaleID).FirstOrDefault();

            if (viewModel == null)
            {
                throw new Exception(String.Format("ToneGridEditViewModel with ScaleID '{0}' not found in rootDocumentViewModel.", scaleID));
            }

            return viewModel;
        }
    }
}