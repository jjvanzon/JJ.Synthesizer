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
    internal static class ViewModelSelector
    {
        // AudioFileOutput

        public static AudioFileOutputPropertiesViewModel TryGetAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel;
            rootDocumentViewModel.AudioFileOutputPropertiesDictionary.TryGetValue(audioFileOutputID, out viewModel);
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

        // PatchDocument

        public static PatchDocumentViewModel TryGetPatchDocumentViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel;
            rootDocumentViewModel.PatchDocumentDictionary.TryGetValue(childDocumentID, out patchDocumentViewModel);

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

            string key = group?.ToLower() ?? "";

            PatchGridViewModel viewModel;
            rootDocumentViewModel.PatchGridDictionary.TryGetValue(key, out viewModel);

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

            CurveDetailsViewModel curveDetailsViewModel;
            if (rootDocumentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out curveDetailsViewModel))
            {
                return curveDetailsViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                CurveDetailsViewModel curveDetailsViewModel2;
                if (patchDocumentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out curveDetailsViewModel2))
                {
                    return curveDetailsViewModel;
                }
            }

            return null;
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

            CurvePropertiesViewModel curvePropertiesViewModel;
            if (rootDocumentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out curvePropertiesViewModel))
            {
                return curvePropertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                CurvePropertiesViewModel curvePropertiesViewModel2;
                if (patchDocumentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out curvePropertiesViewModel2))
                {
                    return curvePropertiesViewModel;
                }
            }

            return null;
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

            CurveDetailsViewModel detailsViewModel = ViewModelSelector.EnumerateCurveDetailsViewModels(rootDocumentViewModel)
                                                                      .Where(x => x.Nodes.Any(y => y.ID == nodeID))
                                                                      .First();
            return detailsViewModel;
        }

        public static Dictionary<int, CurveDetailsViewModel> GetCurveDetailsViewModelDictionary_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveDetailsDictionary;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurveDetailsDictionary;
            }
        }

        public static Dictionary<int, CurveDetailsViewModel> GetCurveDetailsViewModelDictionary_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
            {
                return rootDocumentViewModel.CurveDetailsDictionary;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
                {
                    return patchDocumentViewModel.CurveDetailsDictionary;
                }
            }

            throw new Exception(String.Format("IList<CurveDetailsViewModel> for curveID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static Dictionary<int, CurvePropertiesViewModel> GetCurvePropertiesViewModelDictionary_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurvePropertiesDictionary;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurvePropertiesDictionary;
            }
        }

        public static Dictionary<int, CurvePropertiesViewModel> GetCurvePropertiesViewModelDictionary_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurvePropertiesDictionary.ContainsKey(curveID))
            {
                return rootDocumentViewModel.CurvePropertiesDictionary;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.CurvePropertiesDictionary.ContainsKey(curveID))
                {
                    return patchDocumentViewModel.CurvePropertiesDictionary;
                }
            }

            throw new Exception(String.Format("CurvePropertiesViewModel collection for curveID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static CurveGridViewModel GetCurveGridViewModel_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveGridViewModel gridViewModel = ViewModelSelector.EnumerateCurveGridViewModels(rootDocumentViewModel)
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                yield return patchDocumentViewModel.CurveGrid;
            }
        }

        private static IEnumerable<CurveDetailsViewModel> EnumerateCurveDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurveDetailsViewModel curveDetailsViewModel in rootDocumentViewModel.CurveDetailsDictionary.Values)
            {
                yield return curveDetailsViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                foreach (CurveDetailsViewModel curveDetailsViewModel in patchDocumentViewModel.CurveDetailsDictionary.Values)
                {
                    yield return curveDetailsViewModel;
                }
            }
        }

        private static IEnumerable<CurvePropertiesViewModel> EnumerateCurvePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (CurvePropertiesViewModel curvePropertiesViewModel in rootDocumentViewModel.CurvePropertiesDictionary.Values)
            {
                yield return curvePropertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                foreach (CurvePropertiesViewModel curvePropertiesViewModel in patchDocumentViewModel.CurvePropertiesDictionary.Values)
                {
                    yield return curvePropertiesViewModel;
                }
            }
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

            NodePropertiesViewModel viewModel = ViewModelSelector.EnumerateNodePropertiesViewModels(rootDocumentViewModel)
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

            foreach (NodePropertiesViewModel propertiesViewModel in rootDocumentViewModel.NodePropertiesDictionary.Values)
            {
                yield return propertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                foreach (NodePropertiesViewModel propertiesViewModel in patchDocumentViewModel.NodePropertiesDictionary.Values)
                {
                    yield return propertiesViewModel;
                }
            }
        }

        public static Dictionary<int, NodePropertiesViewModel> GetNodePropertiesViewModelDictionary_ByCurveID(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
            {
                return rootDocumentViewModel.NodePropertiesDictionary;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
                {
                    return patchDocumentViewModel.NodePropertiesDictionary;
                }
            }

            throw new Exception(String.Format("IList<NodePropertiesViewModel> for Curve ID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", curveID));
        }

        public static Dictionary<int, NodePropertiesViewModel> GetNodePropertiesViewModelDictionary_ByNodeID(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.NodePropertiesDictionary.ContainsKey(nodeID))
            {
                return rootDocumentViewModel.NodePropertiesDictionary;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.NodePropertiesDictionary.ContainsKey(nodeID))
                {
                    return patchDocumentViewModel.NodePropertiesDictionary;
                }
            }

            throw new Exception(String.Format("IList<NodePropertiesViewModel> for nodeID '{0}' not found in rootDocumentViewModel nor any of its PatchDocumentViewModels.", nodeID));
        }

        // Operator

        public static OperatorViewModel GetOperatorViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorViewModel viewModel = ViewModelSelector.EnumerateOperatorViewModels(rootDocumentViewModel)
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

            return rootDocumentViewModel.PatchDocumentDictionary.Values.SelectMany(x => x.PatchDetails.Entity.Operators);
        }

        public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForBundle viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForBundles.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForCache viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForCaches.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForCurve viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForCurves.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForCustomOperator viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForMakeContinuous viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForNumber viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForNumbers.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_ForSample viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_ForSamples.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_WithDimension viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_WithDimension.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                OperatorPropertiesViewModel_WithInletCount viewModel;
                if (patchDocumentViewModel.OperatorPropertiesDictionary_WithInletCount.TryGetValue(operatorID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

        public static Dictionary<int, OperatorPropertiesViewModel> GetOperatorPropertiesViewModelDictionary_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                // The key of the dictionary is ChildDocumentID.
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForBundle> GetOperatorPropertiesViewModelDictionary_ForBundles_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForBundles;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForBundle collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCache> GetOperatorPropertiesViewModelDictionary_ForCaches_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForCaches;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForCache collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCurve> GetOperatorPropertiesViewModelDictionary_ForCurves_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForCurves;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForCurve collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCustomOperator> GetOperatorPropertiesViewModelDictionary_ForCustomOperators_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForCustomOperator collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForMakeContinuous> GetOperatorPropertiesViewModelDictionary_ForMakeContinuous_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForMakeContinuous collection with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForNumber> GetOperatorPropertiesViewModelDictionary_ForNumbers_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForNumbers;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForNumber collection for Patch ID '{0}' not found any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelDictionary_ForPatchInlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForPatchInlet collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelDictionary_ForPatchOutlets_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForPatchOutlet collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForSample> GetOperatorPropertiesViewModelDictionary_ForSamples_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_ForSamples;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_ForSample collection with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimension> GetOperatorPropertiesViewModelDictionary_WithDimension_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_WithDimension;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_WithDimension collection with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndInterpolation> GetOperatorPropertiesViewModelDictionary_WithDimensionAndInterpolation_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_WithDimensionAndInterpolation collection with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> GetOperatorPropertiesViewModelDictionary_WithDimensionAndCollectionRecalculation_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation collection with Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndOutletCount> GetOperatorPropertiesViewModelDictionary_WithDimensionAndOutletCount_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_WithDimensionAndOutletCount collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithInletCount> GetOperatorPropertiesViewModelDictionary_WithInletCount_ByPatchID(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.PatchDetails.Entity.PatchID == patchID)
                {
                    return patchDocumentViewModel.OperatorPropertiesDictionary_WithInletCount;
                }
            }

            throw new Exception(String.Format("OperatorPropertiesViewModel_WithInletCount collection for Patch ID '{0}' not found in any of the PatchDocumentViewModels.", patchID));
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel rootDocumentViewModel, int patchID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDetailsViewModel detailsViewModel = ViewModelSelector.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
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

            PatchDetailsViewModel detailsViewModel = ViewModelSelector.EnumeratePatchDetailsViewModels(rootDocumentViewModel)
                                                                        .Where(x => x.Entity.Operators.Any(y => y.ID == operatorID))
                                                                        .First();
            return detailsViewModel;
        }

        private static IEnumerable<PatchDetailsViewModel> EnumeratePatchDetailsViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            return rootDocumentViewModel.PatchDocumentDictionary.Values.Select(x => x.PatchDetails);
        }

        public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel;
            rootDocumentViewModel.PatchDocumentDictionary.TryGetValue(childDocumentID, out patchDocumentViewModel);

            return patchDocumentViewModel.PatchProperties;
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

        public static PatchGridViewModel GetVisiblePatchGridViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchGridViewModel userInput = rootDocumentViewModel.PatchGridDictionary.Values
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

            PatchDetailsViewModel viewModel = rootDocumentViewModel.PatchDocumentDictionary.Values.Select(x => x.PatchDetails).Where(x => x.Visible).FirstOrDefault();
            if (viewModel == null)
            {
                throw new Exception("No visible PatchDetailsViewModel found in the PatchDocumentViewModels.");
            }

            return viewModel;
        }

        // Sample

        public static SampleGridViewModel GetVisibleSampleGridViewModel(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel userInput = ViewModelSelector.EnumerateSampleGridViewModels(rootDocumentViewModel)
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

            SamplePropertiesViewModel samplePropertiesViewModel;
            if (rootDocumentViewModel.SamplePropertiesDictionary.TryGetValue(sampleID, out samplePropertiesViewModel))
            {
                return samplePropertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                SamplePropertiesViewModel samplePropertiesViewModel2;
                if (patchDocumentViewModel.SamplePropertiesDictionary.TryGetValue(sampleID, out samplePropertiesViewModel2))
                {
                    return samplePropertiesViewModel;
                }
            }

            return null;
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

        public static Dictionary<int, SamplePropertiesViewModel> GetSamplePropertiesViewModelDictionary_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SamplePropertiesDictionary;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.SamplePropertiesDictionary;
            }
        }

        public static Dictionary<int, SamplePropertiesViewModel> GetSamplePropertiesViewModelDictionary_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.SamplePropertiesDictionary.ContainsKey(sampleID))
            {
                return rootDocumentViewModel.SamplePropertiesDictionary;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.SamplePropertiesDictionary.ContainsKey(sampleID))
                {
                    return patchDocumentViewModel.SamplePropertiesDictionary;
                }
            }

            throw new Exception(String.Format("SamplePropertiesViewModel collection for sampleID '{0}' not found in rootDocumentViewModel nor its PatchDocumentViewModels.", sampleID));
        }

        public static SampleGridViewModel GetSampleGridViewModel_BySampleID(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            SampleGridViewModel gridViewModel = ViewModelSelector.EnumerateSampleGridViewModels(rootDocumentViewModel)
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

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                yield return patchDocumentViewModel.SampleGrid;
            }
        }

        private static IEnumerable<SamplePropertiesViewModel> EnumerateSamplePropertiesViewModels(DocumentViewModel rootDocumentViewModel)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            foreach (SamplePropertiesViewModel samplePropertiesViewModel in rootDocumentViewModel.SamplePropertiesDictionary.Values)
            {
                yield return samplePropertiesViewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                foreach (SamplePropertiesViewModel samplePropertiesViewModel in patchDocumentViewModel.SamplePropertiesDictionary.Values)
                {
                    yield return samplePropertiesViewModel;
                }
            }
        }

        // Scale

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

            ToneGridEditViewModel viewModel;
            rootDocumentViewModel.ToneGridEditDictionary.TryGetValue(scaleID, out viewModel);

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

            ToneGridEditViewModel userInput = rootDocumentViewModel.ToneGridEditDictionary.Values.Where(x => x.Visible).FirstOrDefault();
            if (userInput == null)
            {
                throw new Exception("No Visible ToneGridEditViewModel found in rootDocumentViewModel.");
            }

            return userInput;
        }
    }
}