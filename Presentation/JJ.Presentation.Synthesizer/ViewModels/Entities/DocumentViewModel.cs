using JJ.Business.CanonicalModel;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
       
        public int ID { get; set; }

        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }

        /// <summary> Leading for saving child entities, not leading for saving the simple properties. </summary>
        public IList<ChildDocumentViewModel> ChildDocumentList { get; set; }

        /// <summary> Leading for saving when it comes to the simple properties. </summary>
        public IList<ChildDocumentPropertiesViewModel> ChildDocumentPropertiesList { get; set; }

        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }

        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurvePropertiesViewModel> CurvePropertiesList { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }
        public IList<NodePropertiesViewModel> NodePropertiesList { get; set; }

        public ChildDocumentGridViewModel EffectGrid { get; set; }
        public ChildDocumentGridViewModel InstrumentGrid { get; set; }

        public SampleGridViewModel SampleGrid { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }

        public ScaleGridViewModel ScaleGrid { get; set; }
        public List<ScalePropertiesViewModel> ScalePropertiesList { get; internal set; }
        public IList<ToneGridEditViewModel> ToneGridEditList { get; set; }

        // Central Lookups

        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators in both root Document and ChildDocuments. </summary>
        public IList<IDAndName> UnderlyingDocumentLookup { get; set; }

        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in the root Document.
        /// (The child documents have their own sample collection.)
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }

        /// <summary> 
        /// This lookup is used by OperatorProperties_ForCurves in the root Document.
        /// (The child documents have their own sample collection.)
        /// </summary>
        public IList<IDAndName> CurveLookup { get; set; }
    }
}
