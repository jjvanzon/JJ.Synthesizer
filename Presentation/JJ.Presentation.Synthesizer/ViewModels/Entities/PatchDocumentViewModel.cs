using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    /// <summary> Leading for saving related entities, not leading for saving the simple properties. </summary>
    public sealed class PatchDocumentViewModel
    {
        public int ChildDocumentID { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }
        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public IList<CurvePropertiesViewModel> CurvePropertiesList { get; set; }
        public IList<NodePropertiesViewModel> NodePropertiesList { get; set; }
        public IList<OperatorPropertiesViewModel> OperatorPropertiesList { get; set; }
        public IList<OperatorPropertiesViewModel_ForBundle> OperatorPropertiesList_ForBundles { get; set; }
        public IList<OperatorPropertiesViewModel_ForCurve> OperatorPropertiesList_ForCurves { get; set; }
        public IList<OperatorPropertiesViewModel_ForCustomOperator> OperatorPropertiesList_ForCustomOperators { get; set; }
        public IList<OperatorPropertiesViewModel_ForNumber> OperatorPropertiesList_ForNumbers { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesList_ForPatchInlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesList_ForPatchOutlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForSample> OperatorPropertiesList_ForSamples { get; set; }
        public IList<OperatorPropertiesViewModel_ForUnbundle> OperatorPropertiesList_ForUnbundles { get; set; }
        public PatchDetailsViewModel PatchDetails { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }
    }
}
