using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    /// <summary> Leading for saving related entities, not leading for saving the simple properties. </summary>
    public sealed class PatchDocumentViewModel
    {
        public int ChildDocumentID { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        public Dictionary<int, CurveDetailsViewModel> CurveDetailsDictionary { get; set; }
        /// <summary>
        /// This lookup is used by OperatorProperties_ForSamples in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public Dictionary<int, CurvePropertiesViewModel> CurvePropertiesDictionary { get; set; }
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
        public Dictionary<int, OperatorPropertiesViewModel_WithDimension> OperatorPropertiesDictionary_WithDimension { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndInterpolation> OperatorPropertiesDictionary_WithDimensionAndInterpolation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation> OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithDimensionAndOutletCount> OperatorPropertiesDictionary_WithDimensionAndOutletCount { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithInletCount> OperatorPropertiesDictionary_WithInletCount { get; set; }
        public PatchPropertiesViewModel PatchProperties { get; set; }
        public PatchDetailsViewModel PatchDetails { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }
        public Dictionary<int, SamplePropertiesViewModel> SamplePropertiesDictionary { get; set; }
    }
}
