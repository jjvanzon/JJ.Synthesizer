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

        public static PatchDocumentViewModel TryGetPatchDocumentViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel;
            rootDocumentViewModel.PatchDocumentDictionary.TryGetValue(childDocumentID, out patchDocumentViewModel);

            return patchDocumentViewModel;
        }

        public static PatchDocumentViewModel GetPatchDocumentViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            PatchDocumentViewModel patchDocumentViewModel = TryGetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

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
                if (patchDocumentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out curveDetailsViewModel))
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
                if (patchDocumentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out curvePropertiesViewModel))
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

        public static Dictionary<int, CurveDetailsViewModel> GetCurveDetailsViewModelDictionary_ByDocumentID(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveDetailsDictionary;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, documentID);
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
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, documentID);
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

        public static CurveGridViewModel GetCurveGridViewModel(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.CurveGrid;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.CurveGrid;
            }
        }

        // Node

        public static NodePropertiesViewModel TryGetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            NodePropertiesViewModel viewModel;

            if (rootDocumentViewModel.NodePropertiesDictionary.TryGetValue(nodeID, out viewModel))
            {
                return viewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.NodePropertiesDictionary.TryGetValue(nodeID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

        public static OperatorViewModel GetOperatorViewModel(DocumentViewModel rootDocumentViewModel, int childDocumentID, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            OperatorViewModel operatorViewModel;
            if (patchDocumentViewModel.PatchDetails.Entity.OperatorDictionary.TryGetValue(operatorID, out operatorViewModel))
            {
                return operatorViewModel;
            }

            throw new Exception(String.Format("OperatorViewModel with key '{0}' not found in PatchDocumentViewModels.", new { childDocumentID, operatorID }));
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

        public static Dictionary<int, OperatorPropertiesViewModel> GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForBundle> GetOperatorPropertiesViewModelDictionary_ForBundles_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForBundles, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCache> GetOperatorPropertiesViewModelDictionary_ForCaches_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForCaches, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCurve> GetOperatorPropertiesViewModelDictionary_ForCurves_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForCurves, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForCustomOperator> GetOperatorPropertiesViewModelDictionary_ForCustomOperators_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForCustomOperators, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForMakeContinuous> GetOperatorPropertiesViewModelDictionary_ForMakeContinuous_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForMakeContinuous, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForNumber> GetOperatorPropertiesViewModelDictionary_ForNumbers_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForNumbers, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet> GetOperatorPropertiesViewModelDictionary_ForPatchInlets_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForPatchInlets, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet> GetOperatorPropertiesViewModelDictionary_ForPatchOutlets_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForPatchOutlets, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_ForSample> GetOperatorPropertiesViewModelDictionary_ForSamples_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_ForSamples, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimension> GetOperatorPropertiesViewModelDictionary_WithDimension_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_WithDimension, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndInterpolation> GetOperatorPropertiesViewModelDictionary_WithDimensionAndInterpolation_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_WithDimensionAndInterpolation, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> GetOperatorPropertiesViewModelDictionary_WithDimensionAndCollectionRecalculation_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndOutletCount> GetOperatorPropertiesViewModelDictionary_WithDimensionAndOutletCount_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_WithDimensionAndOutletCount, rootDocumentViewModel, childDocumentID);
        }

        public static Dictionary<int, OperatorPropertiesViewModel_WithInletCount> GetOperatorPropertiesViewModelDictionary_WithInletCount_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            return Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID(x => x.OperatorPropertiesDictionary_WithInletCount, rootDocumentViewModel, childDocumentID);
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            return patchDocumentViewModel.PatchDetails;
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

        public static PatchGridViewModel GetPatchGridViewModel(DocumentViewModel rootDocumentViewModel, string group)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchGridViewModel viewModel;
            if (rootDocumentViewModel.PatchGridDictionary.TryGetValue(group, out viewModel))
            {
                return viewModel;
            }

            throw new Exception($"{nameof(PatchGridViewModel)} for group '{group}' not found");
        }

        /// <summary> This terrible delegitis code prevents a whole bunch of code repetition. </summary>
        private static Dictionary<int, TOperatorPropertiesViewModel> Base_GetOperatorPropertiesViewModelDictionary_ByChildDocumentID<TOperatorPropertiesViewModel>(
            Func<PatchDocumentViewModel, Dictionary<int, TOperatorPropertiesViewModel>> getOperatorPropertiesDictionaryDelegate,
            DocumentViewModel rootDocumentViewModel,
            int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = TryGetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            if (patchDocumentViewModel != null)
            {
                return getOperatorPropertiesDictionaryDelegate(patchDocumentViewModel);
            }

            throw new Exception(String.Format(
                "{0} for childDocumentID '{1}' not found in any of the PatchDocumentViewModels.",
                typeof(TOperatorPropertiesViewModel).Name,
                childDocumentID));
        }

        // Sample

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
                if (patchDocumentViewModel.SamplePropertiesDictionary.TryGetValue(sampleID, out samplePropertiesViewModel))
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

        public static Dictionary<int, SamplePropertiesViewModel> GetSamplePropertiesViewModelDictionary(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SamplePropertiesDictionary;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.SamplePropertiesDictionary;
            }
        }

        public static SampleGridViewModel GetSampleGridViewModel(DocumentViewModel rootDocumentViewModel, int documentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            if (rootDocumentViewModel.ID == documentID)
            {
                return rootDocumentViewModel.SampleGrid;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, documentID);
                return patchDocumentViewModel.SampleGrid;
            }
        }

        // Scale

        public static ScalePropertiesViewModel TryGetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ScalePropertiesViewModel viewModel;
            rootDocumentViewModel.ScalePropertiesDictionary.TryGetValue(scaleID, out viewModel);

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
    }
}