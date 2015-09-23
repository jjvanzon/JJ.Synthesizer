using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
       
        public int ID { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }

        public ChildDocumentGridViewModel InstrumentGrid { get; set; }
        public ChildDocumentGridViewModel EffectGrid { get; set; }

        /// <summary> Leading for saving when it comes to the simple properties and the MainPatch. </summary>
        public IList<ChildDocumentPropertiesViewModel> ChildDocumentPropertiesList { get; set; }

        /// <summary> Leading for saving child entities, not leading for saving the somple properties. </summary>
        public IList<ChildDocumentViewModel> ChildDocumentList { get; set; }

        public SampleGridViewModel SampleGrid { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }

        public PatchGridViewModel PatchGrid { get; set; }
        public IList<PatchDetailsViewModel> PatchDetailsList { get; set; }

        public ScaleGridViewModel ScaleGrid { get; set; }
        public IList<ScaleDetailsViewModel> ScaleDetailsList { get; set; }

        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }

        public IList<OperatorPropertiesViewModel> OperatorPropertiesList { get; set; }
        public IList<OperatorPropertiesViewModel_ForCustomOperator> OperatorPropertiesList_ForCustomOperators { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesList_ForPatchInlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesList_ForPatchOutlets { get; set; }
        public IList<OperatorPropertiesViewModel_ForSample> OperatorPropertiesList_ForSamples { get; set; }
        public IList<OperatorPropertiesViewModel_ForNumber> OperatorPropertiesList_ForNumbers { get; set; }

        // Central Lookups

        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators in both root Document and ChildDocuments. </summary>
        public IList<IDAndName> UnderlyingDocumentLookup { get; set; }

        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in the root Document.
        /// (The child documents have their own sample collection.)
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }
    }
}
