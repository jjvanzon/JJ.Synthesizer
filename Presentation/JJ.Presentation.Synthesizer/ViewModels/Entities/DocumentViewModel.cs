using JJ.Business.CanonicalModel;
using System.Collections.Generic;
using System;

namespace JJ.Presentation.Synthesizer.ViewModels.Entities
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
        public int ID { get; set; }
        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        /// <summary> 
        /// This lookup is used by OperatorProperties_ForCurves in the root Document.
        /// (The child documents have their own sample collection.)
        /// </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public IList<CurvePropertiesViewModel> CurvePropertiesList { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public IList<NodePropertiesViewModel> NodePropertiesList { get; set; }
        public IList<PatchGridViewModel> PatchGridList { get; set; }
        /// <summary> Leading for saving child entities, not leading for saving the simple properties. </summary>
        public IList<PatchDocumentViewModel> PatchDocumentList { get; set; }
        /// <summary> Leading for saving when it comes to the simple properties. </summary>
        public IList<PatchPropertiesViewModel> PatchPropertiesList { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        /// <summary> 
        /// This lookup is used by OperatorProperties_ForSamples in the root Document.
        /// (The child documents have their own sample collection.)
        /// </summary>
        public IList<IDAndName> SampleLookup { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }
        public ScaleGridViewModel ScaleGrid { get; set; }
        public List<ScalePropertiesViewModel> ScalePropertiesList { get; internal set; }
        public IList<ToneGridEditViewModel> ToneGridEditList { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators in both root Document and PatchDocuments. </summary>
        public IList<IDAndName> UnderlyingDocumentLookup { get; set; }

        [Obsolete]
        public PatchGridViewModel EffectGrid { get; set; }
        [Obsolete]
        public PatchGridViewModel InstrumentGrid { get; set; }
    }
}
