using JJ.Business.CanonicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    /// <summary> Leading for saving child entities, not leading for saving the simple properties. </summary>
    public sealed class ChildDocumentViewModel
    {
        public int ID { get; set; }

        public SampleGridViewModel SampleGrid { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }

        public PatchGridViewModel PatchGrid { get; set; }
        public IList<PatchDetailsViewModel> PatchDetailsList { get; set; }

        public IList<OperatorPropertiesViewModel> OperatorPropertiesList { get; set; }
        public IList<OperatorPropertiesViewModel_ForCustomOperator> OperatorPropertiesList_ForCustomOperators { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesList_ForPatchInlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesList_ForPatchOutlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForSample> OperatorPropertiesList_ForSamples { get; set; }
        public IList<OperatorPropertiesViewModel_ForValue> OperatorPropertiesList_ForValues { get; set; }

        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in this child Document.
        /// That sample collection should include both samples in the root document as well as samples
        /// in this child document.
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }
    }
}
