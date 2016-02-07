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
    internal static class DocumentViewModelHelper
    {
        // PatchDocument

        public static PatchDocumentViewModel GetPatchDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = rootDocumentViewModel.PatchDocumentList
                                                                                 .Where(x => x.ChildDocumentID == childDocumentID)
                                                                                 .FirstOrDefault(); // First for performance.
            if (patchDocumentViewModel == null)
            {
                throw new Exception(String.Format("PatchDocumentViewModel with childDocumentID '{0}' not found in documentViewModel.PatchDocumentList.", childDocumentID));
            }

            return patchDocumentViewModel;
        }

        public static PatchGridViewModel GetPatchGridViewModel_ByGroup(DocumentViewModel rootDocumentViewModel, string group)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchGridViewModel viewModel;

            bool isGroupless = String.IsNullOrEmpty(group);
            if (isGroupless)
            {
                viewModel = rootDocumentViewModel.PatchGridList.Where(x => String.IsNullOrEmpty(x.Group)).FirstOrDefault();
            }
            else
            {
                viewModel = rootDocumentViewModel.PatchGridList.Where(x => String.Equals(x.Group, group)).FirstOrDefault();
            }

            if (viewModel == null)
            {
                throw new Exception(String.Format("PatchGridViewModel for Group '{0}' not found in documentViewModel.PatchGridList.", group));
            }

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                            .FirstOrDefault(x => x.ID == curveID);
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("CurveDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
            }

            return detailsViewModel;
        }

        public static CurvePropertiesViewModel GetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurvePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.EnumerateCurvePropertiesViewModels(rootDocumentViewModel)
                                                                                  .FirstOrDefault(x => x.Entity.ID == curveID); // First for performance.
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("CurvePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
            }

            return propertiesViewModel;
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel_ByNodeID(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                            .Where(x => x.Nodes.Any(y => y.ID == nodeID))
                                                                            .First();
            return detailsViewModel;
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModelList_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveDetailsList;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurveDetailsList;
            }
        }

        public static IList<CurveDetailsViewModel> GetCurveDetailsViewModelList_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurveDetailsList.Any(x => x.ID == curveID))
            {
                return rootDocumentViewModel.CurveDetailsList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.CurveDetailsList.Any(x => x.ID == curveID))
                {
                    return patchDocumentViewModel.CurveDetailsList;
                }
            }

            throw new Exception(String.Format("IList<CurveDetailsViewModel> for curveID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static IList<CurvePropertiesViewModel> GetCurvePropertiesViewModelList_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurvePropertiesList;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurvePropertiesList;
            }
        }

        public static IList<CurvePropertiesViewModel> GetCurvePropertiesViewModelList_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurvePropertiesList.Any(x => x.Entity.ID == curveID))
            {
                return rootDocumentViewModel.CurvePropertiesList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.CurvePropertiesList.Any(x => x.Entity.ID == curveID))
                {
                    return patchDocumentViewModel.CurvePropertiesList;
                }
            }

            throw new Exception(String.Format("IList<CurvePropertiesViewModel> for curveID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static CurveGridViewModel GetCurveGridViewModel_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveGridViewModel gridViewModel = DocumentViewModelHelper.EnumerateCurveGridViewModels(rootDocumentViewModel)
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
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurveGrid;
            }
        }

        private static IEnumerable<CurveGridViewModel> EnumerateCurveGridViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            yield return rootDocumentViewModel.CurveGrid;

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                yield return patchDocumentViewModel.CurveGrid;
            }
        }

        private static IEnumerable<CurveDetailsViewModel> EnumerateCurveDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurveDetailsViewModel curveDetailsViewModel in rootDocumentViewModel.CurveDetailsList)
            {
                yield return curveDetailsViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (CurveDetailsViewModel curveDetailsViewModel in patchDocumentViewModel.CurveDetailsList)
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (CurvePropertiesViewModel curvePropertiesViewModel in patchDocumentViewModel.CurvePropertiesList)
                {
                    yield return curvePropertiesViewModel;
                }
            }
        }

        // Node

        public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            NodePropertiesViewModel viewModel = DocumentViewModelHelper.EnumerateNodePropertiesViewModels(rootDocumentViewModel)
                                                                       .FirstOrDefault(x => x.Entity.ID == nodeID); // First for performance.
            if (viewModel == null)
            {
                throw new Exception(String.Format("NodePropertiesViewModel with Node ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", nodeID));
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (NodePropertiesViewModel propertiesViewModel in patchDocumentViewModel.NodePropertiesList)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        public static IList<NodePropertiesViewModel> GetNodePropertiesViewModelList_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurveDetailsList.Any(x => x.ID == curveID))
            {
                return rootDocumentViewModel.NodePropertiesList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.CurveDetailsList.Any(x => x.ID == curveID))
                {
                    return patchDocumentViewModel.NodePropertiesList;
                }
            }

            throw new Exception(String.Format("IList<NodePropertiesViewModel> for Curve ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static IList<NodePropertiesViewModel> GetNodePropertiesViewModelList_ByNodeID(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.NodePropertiesList.Any(x => x.Entity.ID == nodeID))
            {
                return rootDocumentViewModel.NodePropertiesList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.NodePropertiesList.Any(x => x.Entity.ID == nodeID))
                {
                    return patchDocumentViewModel.NodePropertiesList;
                }
            }

            throw new Exception(String.Format("IList<NodePropertiesViewModel> for nodeID '{0}' not found in rootDocumentViewModel nor any of its PatchDocumentViewModels.", nodeID));
        }

        // Operator

        public static OperatorViewModel GetOperatorViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorViewModel viewModel = DocumentViewModelHelper.EnumerateOperatorViewModels(rootDocumentViewModel)
                                                                 .FirstOrDefault(x => x.ID == operatorID);
            if (viewModel == null)
            {
                throw new Exception(String.Format("OperatorViewModel with ID '{0}' not found in PatchDocumentViewModels.", operatorID));
            }

            return viewModel;
        }

        private static IEnumerable<OperatorViewModel> EnumerateOperatorViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.PatchDetails.Entity.Operators);
        }

        public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels(rootDocumentViewModel)
                                                                       .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForAggregate TryGetOperatorPropertiesViewModel_ForAggregate(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForAggregate viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForAggregates(rootDocumentViewModel)
                                                                                        .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle TryGetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForBundle viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForBundles(rootDocumentViewModel)
                                                                                 .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCurve viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForCurves(rootDocumentViewModel)
                                                                                .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator TryGetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCustomOperator viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForCustomOperators(rootDocumentViewModel)
                                                                                         .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForNumber viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForNumbers(rootDocumentViewModel)
                                                                                 .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchInlet viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForPatchInlets(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchOutlet viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForPatchOutlets(rootDocumentViewModel)
                                                                                      .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForResample TryGetOperatorPropertiesViewModel_ForResample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForResample viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForResamples(rootDocumentViewModel)
                                                                                       .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForSample viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForSamples(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSpectrum TryGetOperatorPropertiesViewModel_ForSpectrum(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForSpectrum viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForSpectrums(rootDocumentViewModel)
                                                                                       .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForUnbundle TryGetOperatorPropertiesViewModel_ForUnbundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForUnbundle viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForUnbundles(rootDocumentViewModel)
                                                                                   .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        private static IEnumerable<OperatorPropertiesViewModel> EnumerateOperatorPropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForAggregate> EnumerateOperatorPropertiesViewModels_ForAggregates(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForAggregates);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForBundle> EnumerateOperatorPropertiesViewModels_ForBundles(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForBundles);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForCurve> EnumerateOperatorPropertiesViewModels_ForCurves(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCurves);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForCustomOperator> EnumerateOperatorPropertiesViewModels_ForCustomOperators(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForNumber> EnumerateOperatorPropertiesViewModels_ForNumbers(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForNumbers);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForPatchInlet> EnumerateOperatorPropertiesViewModels_ForPatchInlets(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForPatchOutlet> EnumerateOperatorPropertiesViewModels_ForPatchOutlets(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForResample> EnumerateOperatorPropertiesViewModels_ForResamples(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForResamples);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForSample> EnumerateOperatorPropertiesViewModels_ForSamples(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSamples);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForSpectrum> EnumerateOperatorPropertiesViewModels_ForSpectrums(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSpectrums);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForUnbundle> EnumerateOperatorPropertiesViewModels_ForUnbundles(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForUnbundles);
        }

        public static IList<OperatorPropertiesViewModel> GetOperatorPropertiesViewModelList_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForAggregate> GetOperatorPropertiesViewModelList_ForAggregates_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForAggregates;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForAggregate> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForBundle> GetOperatorPropertiesViewModelList_ForBundles_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForBundles;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForBundle> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForCurve> GetOperatorPropertiesViewModelList_ForCurves_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForCurves;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCurve> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> GetOperatorPropertiesViewModelList_ForCustomOperators_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCustomOperator> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForNumber> GetOperatorPropertiesViewModelList_ForNumbers_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForNumbers;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForNumber> for Patch ID '{0}' not found any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchInlet> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchOutlet> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForResample> GetOperatorPropertiesViewModelList_ForResamples_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForResamples;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForResample> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForSample> GetOperatorPropertiesViewModelList_ForSamples_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForSamples;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForSample> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForSpectrum> GetOperatorPropertiesViewModelList_ForSpectrums_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForSpectrums;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForSpectrum> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_ForUnbundle> GetOperatorPropertiesViewModelList_ForUnbundles_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForUnbundles;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForUnbundle> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel> GetOperatorPropertiesViewModelList_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.OperatorPropertiesList.Any(x => x.ID == operatorID))
                {
                    return patchDocumentViewModel.OperatorPropertiesList;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel> for operatorID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForAggregate> GetOperatorPropertiesViewModelList_ForAggregates_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.OperatorPropertiesList_ForAggregates.Any(x => x.ID == operatorID))
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForAggregates;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForAggregate> for operatorID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForBundle> GetOperatorPropertiesViewModelList_ForBundles_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.OperatorPropertiesList_ForBundles.Any(x => x.ID == operatorID))
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForBundles;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForBundle> for operatorID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForCurve> GetOperatorPropertiesViewModelList_ForCurves_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.OperatorPropertiesList_ForCurves.Any(x => x.ID == operatorID))
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForCurves;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCurve> for operatorID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForCustomOperator> GetOperatorPropertiesViewModelList_ForCustomOperators_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForCustomOperator operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCustomOperator> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelList_ForPatchInlets_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForPatchInlet operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchInlet> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelList_ForPatchOutlets_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForPatchOutlet operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForPatchOutlet> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForResample> GetOperatorPropertiesViewModelList_ForResamples_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForResample operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForResamples)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForResamples;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForResample> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForSample> GetOperatorPropertiesViewModelList_ForSamples_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForSample operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForSamples)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForSamples;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForSample> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForSpectrum> GetOperatorPropertiesViewModelList_ForSpectrums_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForSpectrum operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForSpectrums)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForSpectrums;
                    }
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForSpectrum> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForNumber> GetOperatorPropertiesViewModelList_ForNumbers_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForNumber operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForNumbers)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForNumbers;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForNumber> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        public static IList<OperatorPropertiesViewModel_ForUnbundle> GetOperatorPropertiesViewModelList_ForUnbundles_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (OperatorPropertiesViewModel_ForUnbundle operatorPropertiesViewModel in patchDocumentViewModel.OperatorPropertiesList_ForUnbundles)
                {
                    if (operatorPropertiesViewModel.ID == operatorID)
                    {
                        return patchDocumentViewModel.OperatorPropertiesList_ForUnbundles;
                    }
                }

            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForUnbundle> for Operator ID '{0}' not found in any of the PatchDocumentViewModels.", operatorID));
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = DocumentViewModelHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.Entity.PatchID == patchID); // First for performance.
            if (detailsViewModel == null)
            {
                throw new Exception(String.Format("PatchDetailsViewModel with ID '{0}' not found in PatchDocumentViewModels.", patchID));
            }

            return detailsViewModel;
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);

            return patchDocumentViewModel.PatchDetails;
        }

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByOperatorID(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = DocumentViewModelHelper.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .Where(x => x.Entity.Operators.Any(y => y.ID == operatorID))
                                                                        .First();
            return detailsViewModel;
        }

        private static IEnumerable<PatchDetailsViewModel> EnumeratePatchDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.Select(x => x.PatchDetails);
        }

        public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.Where(x => x.ChildDocumentID == childDocumentID)
                                                          .Select(x => x.PatchProperties)
                                                          .FirstOrDefault();
        }

        // Sample

        public static SamplePropertiesViewModel GetVisibleSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SamplePropertiesViewModel userInput = DocumentViewModelHelper.EnumerateSamplePropertiesViewModels(rootDocumentViewModel)
                                                                         .Where(x => x.Visible)
                                                                         .FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible SamplePropertiesViewModel found in rootDocumentViewModel nor its PatchDocumentViewModels.");
            }

            return userInput;
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SamplePropertiesViewModel propertiesViewModel = DocumentViewModelHelper.EnumerateSamplePropertiesViewModels(rootDocumentViewModel)
                                                                                   .FirstOrDefault(x => x.Entity.ID == sampleID); // First for performance.
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("SamplePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", sampleID));
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
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.SamplePropertiesList;
            }
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModelList_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.SamplePropertiesList.Any(x => x.Entity.ID == sampleID))
            {
                return rootDocumentViewModel.SamplePropertiesList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.SamplePropertiesList.Any(x => x.Entity.ID == sampleID))
                {
                    return patchDocumentViewModel.SamplePropertiesList;
                }
            }

            throw new Exception(String.Format("IList<SamplePropertiesViewModel> for sampleID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", sampleID));
        }

        public static SampleGridViewModel GetSampleGridViewModel_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel gridViewModel = DocumentViewModelHelper.EnumerateSampleGridViewModels(rootDocumentViewModel)
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
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SampleGridViewModel> EnumerateSampleGridViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            yield return rootDocumentViewModel.SampleGrid;

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                yield return patchDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SamplePropertiesViewModel> EnumerateSamplePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (SamplePropertiesViewModel samplePropertiesViewModel in rootDocumentViewModel.SamplePropertiesList)
            {
                yield return samplePropertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                foreach (SamplePropertiesViewModel samplePropertiesViewModel in patchDocumentViewModel.SamplePropertiesList)
                {
                    yield return samplePropertiesViewModel;
                }
            }
        }

        // Scale

        public static ScalePropertiesViewModel GetVisibleScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ScalePropertiesViewModel userInput = rootDocumentViewModel.ScalePropertiesList.Where(x => x.Visible).FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible ScalePropertiesViewModel found in rootDocumentViewModel.");
            }

            return userInput;
        }

        public static ScalePropertiesViewModel GetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ScalePropertiesViewModel propertiesViewModel = rootDocumentViewModel.ScalePropertiesList.FirstOrDefault(x => x.Entity.ID == scaleID);
            if (propertiesViewModel == null)
            {
                throw new Exception(String.Format("ScalePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel.", scaleID));
            }

            return propertiesViewModel;
        }

        // Tone

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

        public static ToneGridEditViewModel GetVisibleToneGridEditViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ToneGridEditViewModel userInput = rootDocumentViewModel.ToneGridEditList.Where(x => x.Visible).FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible ToneGridEditViewModel found in rootDocumentViewModel.");
            }

            return userInput;
        }
    }
}