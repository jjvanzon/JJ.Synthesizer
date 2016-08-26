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

        public static AudioFileOutputPropertiesViewModel GetAudioFileOutputPropertiesViewModel(DocumentViewModel documentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel = TryGetAudioFileOutputPropertiesViewModel(documentViewModel, audioFileOutputID);

            if (viewModel == null)
            {
                throw new NotFoundException<AudioFileOutputPropertiesViewModel>(audioFileOutputID);
            }

            return viewModel;
        }

        public static AudioFileOutputPropertiesViewModel TryGetAudioFileOutputPropertiesViewModel(DocumentViewModel documentViewModel, int audioFileOutputID)
        {
            AudioFileOutputPropertiesViewModel viewModel;
            documentViewModel.AudioFileOutputPropertiesDictionary.TryGetValue(audioFileOutputID, out viewModel);
            return viewModel;
        }

        // PatchDocument

        public static PatchGridViewModel GetPatchGridViewModel_ByGroup(DocumentViewModel documentViewModel, string group)
        {
            PatchGridViewModel viewModel = TryGetPatchGridViewModel_ByGroup(documentViewModel, group);

            if (viewModel == null)
            {
                throw new NotFoundException<PatchGridViewModel>(new { group });
            }

            return viewModel;
        }

        public static PatchGridViewModel TryGetPatchGridViewModel_ByGroup(DocumentViewModel documentViewModel, string group)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            string key = group?.ToLower() ?? "";

            PatchGridViewModel viewModel;

            documentViewModel.PatchGridDictionary.TryGetValue(key, out viewModel);

            return viewModel;
        }

        // Curve

        public static CurveDetailsViewModel GetCurveDetailsViewModel(DocumentViewModel documentViewModel, int curveID)
        {
            CurveDetailsViewModel viewModel = TryGetCurveDetailsViewModel(documentViewModel, curveID);

            if (viewModel == null)
            {
                throw new NotFoundException<CurveDetailsViewModel>(curveID);
            }

            return viewModel;
        }

        public static CurveDetailsViewModel TryGetCurveDetailsViewModel(DocumentViewModel documentViewModel, int curveID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            CurveDetailsViewModel viewModel;
            documentViewModel.CurveDetailsDictionary.TryGetValue(curveID, out viewModel);
            return viewModel;
        }

        public static CurvePropertiesViewModel GetCurvePropertiesViewModel(DocumentViewModel documentViewModel, int curveID)
        {
            CurvePropertiesViewModel propertiesViewModel = TryGetCurvePropertiesViewModel(documentViewModel, curveID);

            if (propertiesViewModel == null)
            {
                throw new NotFoundException<CurvePropertiesViewModel>(curveID);
            }

            return propertiesViewModel;
        }

        public static CurvePropertiesViewModel TryGetCurvePropertiesViewModel(DocumentViewModel documentViewModel, int curveID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            CurvePropertiesViewModel viewModel;
            documentViewModel.CurvePropertiesDictionary.TryGetValue(curveID, out viewModel);
            return viewModel;
        }

        // Node

        public static NodePropertiesViewModel GetNodePropertiesViewModel(DocumentViewModel documentViewModel, int nodeID)
        {
            NodePropertiesViewModel viewModel = TryGetNodePropertiesViewModel(documentViewModel, nodeID);

            if (viewModel == null)
            {
                throw new NotFoundException<NodePropertiesViewModel>(nodeID);
            }

            return viewModel;
        }

        public static NodePropertiesViewModel TryGetNodePropertiesViewModel(DocumentViewModel documentViewModel, int nodeID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            NodePropertiesViewModel viewModel;

            if (documentViewModel.NodePropertiesDictionary.TryGetValue(nodeID, out viewModel))
            {
                return viewModel;
            }

            return null;
        }

        public static Dictionary<int, NodePropertiesViewModel> GetNodePropertiesViewModelDictionary_ByCurveID(DocumentViewModel documentViewModel, int curveID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (documentViewModel.CurveDetailsDictionary.ContainsKey(curveID))
            {
                return documentViewModel.NodePropertiesDictionary;
            }

            throw new NotFoundException<Dictionary<int, NodePropertiesViewModel>>(new { curveID });
        }

        public static Dictionary<int, NodePropertiesViewModel> GetNodePropertiesViewModelDictionary_ByNodeID(DocumentViewModel documentViewModel, int nodeID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            if (documentViewModel.NodePropertiesDictionary.ContainsKey(nodeID))
            {
                return documentViewModel.NodePropertiesDictionary;
            }

            throw new NotFoundException<Dictionary<int, NodePropertiesViewModel>>(new { nodeID });
        }

        // Operator

        public static OperatorViewModel GetOperatorViewModel(DocumentViewModel documentViewModel, int patchID, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchDetailsViewModel patchDetailsViewModel = GetPatchDetailsViewModel(documentViewModel, patchID);

            OperatorViewModel operatorViewModel;
            if (patchDetailsViewModel.Entity.OperatorDictionary.TryGetValue(operatorID, out operatorViewModel))
            {
                return operatorViewModel;
            }

            throw new NotFoundException<OperatorViewModel>(new { patchID, operatorID });
        }

        public static OperatorPropertiesViewModel GetOperatorPropertiesViewModel(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel viewModel = TryGetOperatorPropertiesViewModel(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel TryGetOperatorPropertiesViewModel(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel viewModel;
            documentViewModel.OperatorPropertiesDictionary.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle GetOperatorPropertiesViewModel_ForBundle(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForBundle viewModel = TryGetOperatorPropertiesViewModel_ForBundle(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForBundle>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForBundle TryGetOperatorPropertiesViewModel_ForBundle(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForBundle viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForBundles.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache GetOperatorPropertiesViewModel_ForCache(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCache viewModel = TryGetOperatorPropertiesViewModel_ForCache(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCache>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCache TryGetOperatorPropertiesViewModel_ForCache(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForCache viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForCaches.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve GetOperatorPropertiesViewModel_ForCurve(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = TryGetOperatorPropertiesViewModel_ForCurve(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCurve>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCurve TryGetOperatorPropertiesViewModel_ForCurve(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForCurve viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForCurves.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator GetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = TryGetOperatorPropertiesViewModel_ForCustomOperator(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForCustomOperator>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForCustomOperator TryGetOperatorPropertiesViewModel_ForCustomOperator(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForCustomOperator viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForCustomOperators.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous GetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = TryGetOperatorPropertiesViewModel_ForMakeContinuous(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForMakeContinuous>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForMakeContinuous TryGetOperatorPropertiesViewModel_ForMakeContinuous(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForMakeContinuous viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForMakeContinuous.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber GetOperatorPropertiesViewModel_ForNumber(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForNumber viewModel = TryGetOperatorPropertiesViewModel_ForNumber(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForNumber>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForNumber TryGetOperatorPropertiesViewModel_ForNumber(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForNumber viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForNumbers.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet GetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchInlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchInlet(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForPatchInlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchInlet TryGetOperatorPropertiesViewModel_ForPatchInlet(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForPatchInlet viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForPatchInlets.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet GetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = TryGetOperatorPropertiesViewModel_ForPatchOutlet(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForPatchOutlet>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForPatchOutlet TryGetOperatorPropertiesViewModel_ForPatchOutlet(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForPatchOutlet viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForPatchOutlets.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample GetOperatorPropertiesViewModel_ForSample(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_ForSample viewModel = TryGetOperatorPropertiesViewModel_ForSample(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_ForSample>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_ForSample TryGetOperatorPropertiesViewModel_ForSample(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_ForSample viewModel;
            documentViewModel.OperatorPropertiesDictionary_ForSamples.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension GetOperatorPropertiesViewModel_WithDimension(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimension viewModel = TryGetOperatorPropertiesViewModel_WithDimension(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimension>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimension TryGetOperatorPropertiesViewModel_WithDimension(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_WithDimension viewModel;
            documentViewModel.OperatorPropertiesDictionary_WithDimension.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation GetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndInterpolation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndInterpolation TryGetOperatorPropertiesViewModel_WithDimensionAndInterpolation(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndInterpolation viewModel;
            documentViewModel.OperatorPropertiesDictionary_WithDimensionAndInterpolation.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation GetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation TryGetOperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation viewModel;
            documentViewModel.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount GetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithDimensionAndOutletCount>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithDimensionAndOutletCount TryGetOperatorPropertiesViewModel_WithDimensionAndOutletCount(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel;
            documentViewModel.OperatorPropertiesDictionary_WithDimensionAndOutletCount.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount GetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel documentViewModel, int operatorID)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = TryGetOperatorPropertiesViewModel_WithInletCount(documentViewModel, operatorID);
            if (viewModel == null)
            {
                throw new NotFoundException<OperatorPropertiesViewModel_WithInletCount>(operatorID);
            }
            return viewModel;
        }

        public static OperatorPropertiesViewModel_WithInletCount TryGetOperatorPropertiesViewModel_WithInletCount(DocumentViewModel documentViewModel, int operatorID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            OperatorPropertiesViewModel_WithInletCount viewModel;
            documentViewModel.OperatorPropertiesDictionary_WithInletCount.TryGetValue(operatorID, out viewModel);
            return viewModel;
        }

        // Patch

        public static PatchDetailsViewModel GetPatchDetailsViewModel(DocumentViewModel documentViewModel, int patchID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchDetailsViewModel viewModel;
            if (!documentViewModel.PatchDetailsDictionary.TryGetValue(patchID, out viewModel))
            {
                throw new NotFoundException<PatchDetailsViewModel>(new { patchID });
            }

            return viewModel;
        }

        public static PatchDetailsViewModel TryGetPatchDetailsViewModel(DocumentViewModel documentViewModel, int patchID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchDetailsViewModel viewModel;
            documentViewModel.PatchDetailsDictionary.TryGetValue(patchID, out viewModel);
            return viewModel;
        }

        public static PatchPropertiesViewModel GetPatchPropertiesViewModel(DocumentViewModel documentViewModel, int patchID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchPropertiesViewModel viewModel = TryGetPatchPropertiesViewModel(documentViewModel, patchID);

            if (viewModel == null)
            {
                throw new NotFoundException<PatchPropertiesViewModel>(new { patchID });
            }

            return viewModel;       
        }

        public static PatchPropertiesViewModel TryGetPatchPropertiesViewModel(DocumentViewModel documentViewModel, int patchID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchPropertiesViewModel viewModel;
            documentViewModel.PatchPropertiesDictionary.TryGetValue(patchID, out viewModel);
            return viewModel;
        }

        public static PatchGridViewModel GetPatchGridViewModel(DocumentViewModel documentViewModel, string group)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            PatchGridViewModel viewModel;
            if (documentViewModel.PatchGridDictionary.TryGetValue(group, out viewModel))
            {
                return viewModel;
            }

            throw new NotFoundException<PatchGridViewModel>(new { group });
        }

        // Sample

        public static SamplePropertiesViewModel GetSamplePropertiesViewModel(DocumentViewModel documentViewModel, int sampleID)
        {
            SamplePropertiesViewModel viewModel = TryGetSamplePropertiesViewModel(documentViewModel, sampleID);

            if (viewModel == null)
            {
                throw new NotFoundException<SamplePropertiesViewModel>(sampleID);
            }

            return viewModel;
        }

        public static SamplePropertiesViewModel TryGetSamplePropertiesViewModel(DocumentViewModel documentViewModel, int sampleID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            SamplePropertiesViewModel samplePropertiesViewModel;
            if (documentViewModel.SamplePropertiesDictionary.TryGetValue(sampleID, out samplePropertiesViewModel))
            {
                return samplePropertiesViewModel;
            }

            return null;
        }

        // Scale

        public static ScalePropertiesViewModel GetScalePropertiesViewModel(DocumentViewModel documentViewModel, int scaleID)
        {
            ScalePropertiesViewModel viewModel = TryGetScalePropertiesViewModel(documentViewModel, scaleID);

            if (viewModel == null)
            {
                throw new NotFoundException<ScalePropertiesViewModel>(scaleID);
            }

            return viewModel;
        }

        public static ScalePropertiesViewModel TryGetScalePropertiesViewModel(DocumentViewModel documentViewModel, int scaleID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            ScalePropertiesViewModel viewModel;
            documentViewModel.ScalePropertiesDictionary.TryGetValue(scaleID, out viewModel);

            return viewModel;
        }

        // Tone

        public static ToneGridEditViewModel GetToneGridEditViewModel(DocumentViewModel documentViewModel, int scaleID)
        {
            ToneGridEditViewModel viewModel = TryGetToneGridEditViewModel(documentViewModel, scaleID);

            if (viewModel == null)
            {
                throw new NotFoundException<ToneGridEditViewModel>(new { scaleID });
            }

            return viewModel;
        }

        public static ToneGridEditViewModel TryGetToneGridEditViewModel(DocumentViewModel documentViewModel, int scaleID)
        {
            if (documentViewModel == null) throw new NullException(() => documentViewModel);

            ToneGridEditViewModel viewModel;
            documentViewModel.ToneGridEditDictionary.TryGetValue(scaleID, out viewModel);

            return viewModel;
        }
    }
}