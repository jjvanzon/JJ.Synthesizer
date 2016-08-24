using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common.Exceptions;

namespace JJ.Presentation.Synthesizer.Helpers
{
    internal static class ViewModelSelector
    {
        // AudioFileOutput

        public static AudioFileOutputPropertiesViewModel GetAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel = TryGetAudioFileOutputPropertiesViewModel(rootDocumentViewModel, audioFileOutputID);

            if (viewModel == null)
            {
                throw new NotFoundException<AudioFileOutputPropertiesViewModel>(audioFileOutputID);
            }

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel TryGetAudioFileOutputPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel;
            rootDocumentViewModel.AudioFileOutputPropertiesDictionary.TryGetValue(audioFileOutputID, out viewModel);
            return viewModel;
        }

        // PatchDocument

        public static PatchDocumentViewModel GetPatchDocumentViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            PatchDocumentViewModel viewModel = TryGetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            if (viewModel == null)
            {
                throw new NotFoundException<PatchDetailsViewModel>(new { childDocumentID });
            }

            return viewModel;
        }

        public static PatchDocumentViewModel TryGetPatchDocumentViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel vieModel;

            rootDocumentViewModel.PatchDocumentDictionary.TryGetValue(childDocumentID, out vieModel);

            return vieModel;
        }

        public static PatchGridViewModel GetPatchGridViewModel_ByGroup(DocumentViewModel rootDocumentViewModel, string group)
        {
            PatchGridViewModel viewModel = TryGetPatchGridViewModel_ByGroup(rootDocumentViewModel, group);

            if (viewModel == null)
            {
                throw new NotFoundException<PatchGridViewModel>(new { group });
            }

            return viewModel;
        }

        public static PatchGridViewModel TryGetPatchGridViewModel_ByGroup(DocumentViewModel rootDocumentViewModel, string group)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            string key = group?.ToLower() ?? "";

            PatchGridViewModel viewModel;

            rootDocumentViewModel.PatchGridDictionary.TryGetValue(key, out viewModel);

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            CurveDetailsViewModel viewModel = TryGetCurveDetailsViewModel(rootDocumentViewModel, curveID);

            if (viewModel == null)
            {
                throw new NotFoundException<CurveDetailsViewModel>(curveID);
            }

            return viewModel;
        }

        public static CurveDetailsViewModel TryGetCurveDetailsViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurveDetailsViewModel viewModel;

            if (rootDocumentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out viewModel))
            {
                return viewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
        }

        public static CurvePropertiesViewModel GetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            CurvePropertiesViewModel propertiesViewModel = TryGetCurvePropertiesViewModel(rootDocumentViewModel, curveID);

            if (propertiesViewModel == null)
            {
                throw new NotFoundException<CurvePropertiesViewModel>(curveID);
            }

            return propertiesViewModel;
        }

        public static CurvePropertiesViewModel TryGetCurvePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int curveID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            CurvePropertiesViewModel viewModel;
            if (rootDocumentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out viewModel))
            {
                return viewModel;
            }

            foreach (PatchDocumentViewModel patchDocumentViewModel in rootDocumentViewModel.PatchDocumentDictionary.Values)
            {
                if (patchDocumentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out viewModel))
                {
                    return viewModel;
                }
            }

            return null;
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

        public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int nodeID)
        {
            NodePropertiesViewModel viewModel = TryGetNodePropertiesViewModel(rootDocumentViewModel, nodeID);

            if (viewModel == null)
            {
                throw new NotFoundException<NodePropertiesViewModel>(nodeID);
            }

            return viewModel;
        }

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

            throw new NotFoundException<Dictionary<int, NodePropertiesViewModel>>(new { curveID });
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

            throw new NotFoundException<Dictionary<int, NodePropertiesViewModel>>(new { nodeID });
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

            throw new NotFoundException<OperatorViewModel>(new { childDocumentID, operatorID });
        }

        public static OperatorPropertiesViewModel GetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel viewModel = TryGetOperatorPropertiesViewModel(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle GetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForBundle viewModel = TryGetOperatorPropertiesViewModel_ForBundle(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForBundle>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle TryGetOperatorPropertiesViewModel_ForBundle(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForBundle viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForBundles.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache GetOperatorPropertiesViewModel_ForCache(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCache viewModel = TryGetOperatorPropertiesViewModel_ForCache(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCache>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache TryGetOperatorPropertiesViewModel_ForCache(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCache viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForCaches.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve GetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = TryGetOperatorPropertiesViewModel_ForCurve(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCurve>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCurve viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForCurves.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator GetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TryGetOperatorPropertiesViewModel_ForCustomOperator(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCustomOperator>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator TryGetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForCustomOperator viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForCustomOperators.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous GetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = TryGetOperatorPropertiesViewModel_ForMakeContinuous(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForMakeContinuous>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous TryGetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForMakeContinuous viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForMakeContinuous.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber GetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForNumber viewModel = TryGetOperatorPropertiesViewModel_ForNumber(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForNumber>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForNumber viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForNumbers.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet GetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchInlet(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForPatchInlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchInlet viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForPatchInlets.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet GetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchOutlet(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForPatchOutlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForPatchOutlet viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample GetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForSample viewModel = TryGetOperatorPropertiesViewModel_ForSample(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForSample>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_ForSample viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_ForSamples.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension GetOperatorPropertiesViewModel_WithDimension(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimension viewModel = TryGetOperatorPropertiesViewModel_WithDimension(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimension>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension TryGetOperatorPropertiesViewModel_WithDimension(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimension viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_WithDimension.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation GetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndInterpolation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation GetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount GetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndOutletCount>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount GetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = TryGetOperatorPropertiesViewModel_WithInletCount(rootDocumentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithInletCount>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount TryGetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel rootDocumentViewModel, int operatorID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            OperatorPropertiesViewModel_WithInletCount viewModel;
            rootDocumentViewModel.OperatorPropertiesDictionary_WithInletCount.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel = GetPatchDocumentViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            return patchDocumentViewModel.PatchDetails;
        }

        public static PatchPropertiesViewModel GetPatchPropertiesViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            PatchPropertiesViewModel viewModel = TryGetPatchPropertiesViewModel_ByChildDocumentID(rootDocumentViewModel, childDocumentID);

            if (viewModel == null)
            {
                throw new NotFoundException<PatchPropertiesViewModel>(new { childDocumentID });
            }

            return viewModel;       
        }

        public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel_ByChildDocumentID(DocumentViewModel rootDocumentViewModel, int childDocumentID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchDocumentViewModel patchDocumentViewModel;
            rootDocumentViewModel.PatchDocumentDictionary.TryGetValue(childDocumentID, out patchDocumentViewModel);

            return patchDocumentViewModel.PatchProperties;
        }

        public static PatchGridViewModel GetPatchGridViewModel(DocumentViewModel rootDocumentViewModel, string group)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            PatchGridViewModel viewModel;
            if (rootDocumentViewModel.PatchGridDictionary.TryGetValue(group, out viewModel))
            {
                return viewModel;
            }

            throw new NotFoundException<PatchGridViewModel>(new { group });
        }

        // Sample

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int sampleID)
        {
            SamplePropertiesViewModel viewModel = TryGetSamplePropertiesViewModel(rootDocumentViewModel, sampleID);

            if (viewModel == null)
            {
                throw new NotFoundException<SamplePropertiesViewModel>(sampleID);
            }

            return viewModel;
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
                if (patchDocumentViewModel.SamplePropertiesDictionary.TryGetValue(sampleID, out samplePropertiesViewModel))
                {
                    return samplePropertiesViewModel;
                }
            }

            return null;
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

        public static ScalePropertiesViewModel GetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            ScalePropertiesViewModel viewModel = TryGetScalePropertiesViewModel(rootDocumentViewModel, scaleID);

            if (viewModel == null)
            {
                throw new NotFoundException<ScalePropertiesViewModel>(scaleID);
            }

            return viewModel;
        }

        public static ScalePropertiesViewModel TryGetScalePropertiesViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ScalePropertiesViewModel viewModel;
            rootDocumentViewModel.ScalePropertiesDictionary.TryGetValue(scaleID, out viewModel);

            return viewModel;
        }

        // Tone

        public static ToneGridEditViewModel GetToneGridEditViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            ToneGridEditViewModel viewModel = TryGetToneGridEditViewModel(rootDocumentViewModel, scaleID);

            if (viewModel == null)
            {
                throw new NotFoundException<ToneGridEditViewModel>(new { scaleID });
            }

            return viewModel;
        }

        public static ToneGridEditViewModel TryGetToneGridEditViewModel(DocumentViewModel rootDocumentViewModel, int scaleID)
        {
            if (rootDocumentViewModel == null) throw new NullException(() => rootDocumentViewModel);

            ToneGridEditViewModel viewModel;
            rootDocumentViewModel.ToneGridEditDictionary.TryGetValue(scaleID, out viewModel);

            return viewModel;
        }
    }
}