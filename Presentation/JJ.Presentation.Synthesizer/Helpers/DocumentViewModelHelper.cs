using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Helpers
{
    // TODO: Low priority. A lot of sequential lookups are done here. In the future it might be an idea to use Dictionaries instead
    // of Lists in these view models, to make it O(1) instead of O(n)
    internal static class DocumentViewModelHelper
    {
        // AudioFileOutput

        public static AudioFileOutputPropertiesViewModel TryGetAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel = rootDocumentViewModel.AudioFileOutputPropertiesList
                                                                                .FirstOrDefault(x => x.Entity.ID == audioFileOutputID);
            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel GetAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel = TryGetAudioFileOutputPropertiesViewModel(rootDocumentViewModel, audioFileOutputID);

            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<AudioFileOutputPropertiesViewModel>(audioFileOutputID);
            }

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel GetVisibleAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            AudioFileOutputPropertiesViewModel viewModel = rootDocumentViewModel.AudioFileOutputPropertiesList.Where(x => x.Visible)
                                                                                                              .FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible AudioFileOutputPropertiesViewModel found in rootDocumentViewModel.");
            }

            return viewModel;
        }

        // PatchDocument

        public static PatchDocumentViewModel TryGetPatchDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = rootDocumentViewModel.PatchDocumentList
                                                                                 .Where(x => x.ChildDocumentID == childDocumentID)
                                                                                 .FirstOrDefault(); // First for performance.
            return patchDocumentViewModel;
        }

        public static PatchDocumentViewModel GetPatchDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            PatchDocumentViewModel patchDocumentViewModel = TryGetPatchDocumentViewModel(rootDocumentViewModel, childDocumentID);

            if (patchDocumentViewModel == null)
            {
                throw new Exception(String.Format("PatchDocumentViewModel with childDocumentID '{0}' not found in documentViewModel.PatchDocumentList.", childDocumentID));
            }

            return patchDocumentViewModel;
        }

        public static PatchGridViewModel TryGetPatchGridViewModel_ByGroup(DocumentViewModel rootDocumentViewModel, string group)
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

            return viewModel;
        }


        public static PatchGridViewModel GetPatchGridViewModel_ByGroup(DocumentViewModel rootDocumentViewModel, string group)
        {
            PatchGridViewModel viewModel = TryGetPatchGridViewModel_ByGroup(rootDocumentViewModel, group);

            if (viewModel == null)
            {
                throw new Exception(String.Format("PatchGridViewModel for Group '{0}' not found in documentViewModel.PatchGridList.", group));
            }

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel TryGetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel viewModel = DocumentViewModelHelper.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                     .FirstOrDefault(x => x.ID == curveID);
            return viewModel;
        }

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            CurveDetailsViewModel viewModel = TryGetCurveDetailsViewModel(rootDocumentViewModel, curveID);

            if (viewModel == null)
            {
                throw new Exception(String.Format("CurveDetailsViewModel with ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
            }

            return viewModel;
        }

        public static CurvePropertiesViewModel TryGetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurvePropertiesViewModel viewModel = DocumentViewModelHelper.EnumerateCurvePropertiesViewModels(rootDocumentViewModel)
                                                                        .FirstOrDefault(x => x.ID == curveID); // First for performance.
            return viewModel;
        }

        public static CurvePropertiesViewModel GetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            CurvePropertiesViewModel propertiesViewModel = TryGetCurvePropertiesViewModel(rootDocumentViewModel, curveID);

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

            if (rootDocumentViewModel.CurvePropertiesList.Any(x => x.ID == curveID))
            {
                return rootDocumentViewModel.CurvePropertiesList;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.CurvePropertiesList.Any(x => x.ID == curveID))
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

        public static CurvePropertiesViewModel GetVisibleCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurvePropertiesViewModel viewModel = EnumerateCurvePropertiesViewModels(rootDocumentViewModel).Where(x => x.Visible)
                                                                                                          .FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible CurvePropertiesViewModel found in rootDocumentViewModel or any of its PatchDocumentViewModels.");
            }

            return viewModel;
        }

        public static CurveGridViewModel GetVisibleCurveGridViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveGridViewModel viewModel = EnumerateCurveGridViewModels(rootDocumentViewModel).Where(x => x.Visible)
                                                                                              .FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible CurveGridViewModel found in rootDocumentViewModel or any of its PatchDocumentViewModels.");
            }

            return viewModel;
        }

        public static CurveDetailsViewModel GetVisibleCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel viewModel = EnumerateCurveDetailsViewModels(rootDocumentViewModel).Where(x => x.Visible)
                                                                                              .FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible CurveDetailsViewModel found in rootDocumentViewModel or any of its PatchDocumentViewModels.");
            }

            return viewModel;
        }

        // Node

        public static NodePropertiesViewModel TryGetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            NodePropertiesViewModel viewModel = DocumentViewModelHelper.EnumerateNodePropertiesViewModels(rootDocumentViewModel)
                                                                       .FirstOrDefault(x => x.Entity.ID == nodeID); // First for performance.
            return viewModel;
        }

        public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            NodePropertiesViewModel viewModel = TryGetNodePropertiesViewModel(rootDocumentViewModel, nodeID);

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

        public static NodePropertiesViewModel GetVisibleNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            NodePropertiesViewModel viewModel = EnumerateNodePropertiesViewModels(rootDocumentViewModel).Where(x => x.Visible)
                                                                                                        .FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible NodePropertiesViewModel found in rootDocumentViewModel or any of its PatchDocumentViewModels.");
            }

            return viewModel;
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

        public static OperatorPropertiesViewModel GetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel viewModel = TryGetOperatorPropertiesViewModel(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle TryGetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForBundle viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForBundles(rootDocumentViewModel)
                                                                                 .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle GetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForBundle viewModel = TryGetOperatorPropertiesViewModel_ForBundle(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForBundle>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache TryGetOperatorPropertiesViewModel_ForCache(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCache viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForCaches(rootDocumentViewModel)
                                                                                    .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache GetOperatorPropertiesViewModel_ForCache(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCache viewModel = TryGetOperatorPropertiesViewModel_ForCache(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForCache>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCurve viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForCurves(rootDocumentViewModel)
                                                                                    .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve GetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = TryGetOperatorPropertiesViewModel_ForCurve(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForCurve>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator TryGetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCustomOperator viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForCustomOperators(rootDocumentViewModel)
                                                                                         .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator GetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TryGetOperatorPropertiesViewModel_ForCustomOperator(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForCustomOperator>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous TryGetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForMakeContinuous viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForMakeContinuous(rootDocumentViewModel)
                                                                                             .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous GetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = TryGetOperatorPropertiesViewModel_ForMakeContinuous(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForMakeContinuous>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForNumber viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForNumbers(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber GetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForNumber viewModel = TryGetOperatorPropertiesViewModel_ForNumber(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForNumber>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchInlet viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForPatchInlets(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet GetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchInlet(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForPatchInlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchOutlet viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForPatchOutlets(rootDocumentViewModel)
                                                                                      .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet GetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchOutlet(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForPatchOutlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForSample viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_ForSamples(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample GetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForSample viewModel = TryGetOperatorPropertiesViewModel_ForSample(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_ForSample>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension TryGetOperatorPropertiesViewModel_WithDimension(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimension viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_WithDimension(rootDocumentViewModel)
                                                                                         .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension GetOperatorPropertiesViewModel_WithDimension(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimension viewModel = TryGetOperatorPropertiesViewModel_WithDimension(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_WithDimension>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_WithDimensionAndInterpolation(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation GetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_WithDimensionAndInterpolation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_WithDimensionAndCollectionRecalculation(rootDocumentViewModel)
                                                                                     .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation GetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_WithDimensionAndOutletCount(rootDocumentViewModel)
                                                                                   .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount GetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_WithDimensionAndOutletCount>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount TryGetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithInletCount viewModel = DocumentViewModelHelper.EnumerateOperatorPropertiesViewModels_WithInletCount(rootDocumentViewModel)
                                                                                   .FirstOrDefault(x => x.ID == operatorID); // First for performance.
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount GetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = TryGetOperatorPropertiesViewModel_WithInletCount(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new ViewModelNotFoundByIDException<OperatorPropertiesViewModel_WithInletCount>(operatorID);
            }
            return viewModel;
        }

        private static IEnumerable<OperatorPropertiesViewModel> EnumerateOperatorPropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForBundle> EnumerateOperatorPropertiesViewModels_ForBundles(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForBundles);
        }

        private static IEnumerable<OperatorPropertiesViewModel_ForCache> EnumerateOperatorPropertiesViewModels_ForCaches(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCaches);
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

        private static IEnumerable<OperatorPropertiesViewModel_ForMakeContinuous> EnumerateOperatorPropertiesViewModels_ForMakeContinuous(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForMakeContinuous);
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

        private static IEnumerable<OperatorPropertiesViewModel_ForSample> EnumerateOperatorPropertiesViewModels_ForSamples(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSamples);
        }

        private static IEnumerable<OperatorPropertiesViewModel_WithDimension> EnumerateOperatorPropertiesViewModels_WithDimension(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_WithDimension);
        }

        private static IEnumerable<OperatorPropertiesViewModel_WithDimensionAndInterpolation> EnumerateOperatorPropertiesViewModels_WithDimensionAndInterpolation(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_WithDimensionAndInterpolation);
        }

        private static IEnumerable<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> EnumerateOperatorPropertiesViewModels_WithDimensionAndCollectionRecalculation(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_WithDimensionAndCollectionRecalculation);
        }

        private static IEnumerable<OperatorPropertiesViewModel_WithDimensionAndOutletCount> EnumerateOperatorPropertiesViewModels_WithDimensionAndOutletCount(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_WithDimensionAndOutletCount);
        }

        private static IEnumerable<OperatorPropertiesViewModel_WithInletCount> EnumerateOperatorPropertiesViewModels_WithInletCount(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_WithInletCount);
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

        public static IList<OperatorPropertiesViewModel_ForCache> GetOperatorPropertiesViewModelList_ForCaches_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForCaches;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForCache> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
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

        public static IList<OperatorPropertiesViewModel_ForMakeContinuous> GetOperatorPropertiesViewModelList_ForMakeContinuous_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_ForMakeContinuous;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_ForMakeContinuous> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
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

        public static IList<OperatorPropertiesViewModel_WithDimension> GetOperatorPropertiesViewModelList_WithDimension_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_WithDimension;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_WithDimension> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndInterpolation> GetOperatorPropertiesViewModelList_WithDimensionAndInterpolation_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_WithDimensionAndInterpolation;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_WithDimensionAndInterpolation> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> GetOperatorPropertiesViewModelList_WithDimensionAndCollectionRecalculation_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_WithDimensionAndCollectionRecalculation;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_WithDimensionAndOutletCount> GetOperatorPropertiesViewModelList_WithDimensionAndOutletCount_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_WithDimensionAndOutletCount;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_WithDimensionAndOutletCount> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static IList<OperatorPropertiesViewModel_WithInletCount> GetOperatorPropertiesViewModelList_WithInletCount_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentList)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesList_WithInletCount;
                }
            }

            throw new Exception(String.Format("IList<OperatorPropertiesViewModel_WithInletCount> for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
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

        public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentList.Where(x => x.ChildDocumentID == childDocumentID)
                                                          .Select(x => x.PatchProperties)
                                                          .FirstOrDefault();
        }

        public static PatchPropertiesViewModel GetPatchPropertiesViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            PatchPropertiesViewModel viewModel = TryGetPatchPropertiesViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            if (viewModel == null)
            {
                throw new Exception(String.Format("PatchPropertiesViewModel for childDocmentID '{0}' not found.", childDocumentID));
            }

            return viewModel;       
        }

        public static PatchPropertiesViewModel GetVisiblePatchPropertiesViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchPropertiesViewModel viewModel = rootDocumentViewModel.PatchDocumentList.Select(x => x.PatchProperties).Where(x => x.Visible).FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible PatchPropertiesViewModel found in the PatchDocumentViewModels.");
            }

            return viewModel;
        }

        public static PatchGridViewModel GetVisiblePatchGridViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchGridViewModel userInput = rootDocumentViewModel.PatchGridList
                                                                .Where(x => x.Visible)
                                                                .FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible PatchGridViewModel found in rootDocumentViewModel.");
            }

            return userInput;
        }

        public static PatchDetailsViewModel GetVisiblePatchDetailsViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel viewModel = rootDocumentViewModel.PatchDocumentList.Select(x => x.PatchDetails).Where(x => x.Visible).FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible PatchDetailsViewModel found in the PatchDocumentViewModels.");
            }

            return viewModel;
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

        public static SampleGridViewModel GetVisibleSampleGridViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel userInput = DocumentViewModelHelper.EnumerateSampleGridViewModels(rootDocumentViewModel)
                                                                   .Where(x => x.Visible)
                                                                   .FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible SampleGridViewModel found in rootDocumentViewModel nor its PatchDocumentViewModels.");
            }

            return userInput;
        }

        public static SamplePropertiesViewModel TryGetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SamplePropertiesViewModel viewModel = DocumentViewModelHelper.EnumerateSamplePropertiesViewModels(rootDocumentViewModel)
                                                                         .FirstOrDefault(x => x.Entity.ID == sampleID); // First for performance.

            return viewModel;
        }

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            SamplePropertiesViewModel viewModel = TryGetSamplePropertiesViewModel(rootDocumentViewModel, sampleID);

            if (viewModel == null)
            {
                throw new Exception(String.Format("SamplePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", sampleID));
            }

            return viewModel;
        }

        public static IList<SamplePropertiesViewModel> GetSamplePropertiesViewModelList_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
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

        public static ScalePropertiesViewModel TryGetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ScalePropertiesViewModel viewModel = rootDocumentViewModel.ScalePropertiesList.FirstOrDefault(x => x.Entity.ID == scaleID);

            return viewModel;
        }

        public static ScalePropertiesViewModel GetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            ScalePropertiesViewModel viewModel = TryGetScalePropertiesViewModel(rootDocumentViewModel, scaleID);

            if (viewModel == null)
            {
                throw new Exception(String.Format("ScalePropertiesViewModel with ID '{0}' not found in rootDocumentViewModel.", scaleID));
            }

            return viewModel;
        }

        // Tone

        public static ToneGridEditViewModel TryGetToneGridEditViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ToneGridEditViewModel viewModel = rootDocumentViewModel.ToneGridEditList.Where(x => x.ScaleID == scaleID).FirstOrDefault();

            return viewModel;
        }

        public static ToneGridEditViewModel GetToneGridEditViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            ToneGridEditViewModel viewModel = TryGetToneGridEditViewModel(rootDocumentViewModel, scaleID);

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