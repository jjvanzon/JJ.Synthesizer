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
        /// This lookup is used by OperatorProperties_ForCurves in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public Dictionary<int, CurvePropertiesViewModel> CurvePropertiesDictionary { get; set; }
        public Dictionary<int, NodePropertiesViewModel> NodePropertiesDictionary { get; set; }
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
