using JJ.Data.Canonical;
using System.Collections.Generic;
using System;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
        public int ID { get; set; }
        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public Dictionary<int, AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesDictionary { get; set; }
        public AudioOutputPropertiesViewModel AudioOutputProperties { get; set; }
        public PatchDetailsViewModel AutoPatchDetails { get; set; }
        public CurrentPatchesViewModel CurrentPatches { get; set; }
        public Dictionary<int, CurveDetailsViewModel> CurveDetailsDictionary { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCurves. </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public Dictionary<int, CurvePropertiesViewModel> CurvePropertiesDictionary { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public Dictionary<int, NodePropertiesViewModel> NodePropertiesDictionary { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel> OperatorPropertiesDictionary { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForBundle> OperatorPropertiesDictionary_ForBundles { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCache> OperatorPropertiesDictionary_ForCaches { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCurve> OperatorPropertiesDictionary_ForCurves { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCustomOperator> OperatorPropertiesDictionary_ForCustomOperators { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForMakeContinuous> OperatorPropertiesDictionary_ForMakeContinuous { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForNumber> OperatorPropertiesDictionary_ForNumbers { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesDictionary_ForPatchInlets { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesDictionary_ForPatchOutlets { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForSample> OperatorPropertiesDictionary_ForSamples { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndInterpolation> OperatorPropertiesDictionary_WithDimensionAndInterpolation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndOutletCount> OperatorPropertiesDictionary_WithDimensionAndOutletCount { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithInletCount> OperatorPropertiesDictionary_WithInletCount { get; set; }
        public Dictionary<int, PatchDetailsViewModel> PatchDetailsDictionary { get; set; }
        /// <summary> Key is GroupName lower case. Groupless patches have key "". </summary>
        public Dictionary<string, PatchGridViewModel> PatchGridDictionary { get; set; }
        public Dictionary<int, PatchPropertiesViewModel> PatchPropertiesDictionary { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForSamples. </summary>
        public IList<IDAndName> SampleLookup { get; set; }
        public Dictionary<int, SamplePropertiesViewModel> SamplePropertiesDictionary { get; set; }
        public ScaleGridViewModel ScaleGrid { get; set; }
        public Dictionary<int, ScalePropertiesViewModel> ScalePropertiesDictionary { get; set; }
        /// <summary> Key is Scale ID. </summary>
        public Dictionary<int, ToneGridEditViewModel> ToneGridEditDictionary { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators. </summary>
        public IList<IDAndName> UnderlyingPatchLookup { get; set; }
    }
}
