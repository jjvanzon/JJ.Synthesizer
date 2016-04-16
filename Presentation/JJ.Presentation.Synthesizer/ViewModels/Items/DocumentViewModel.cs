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
        public IList<AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesList { get; set; }
        public AudioOutputPropertiesViewModel AudioOutputProperties { get; set; }
        public CurrentPatchesViewModel CurrentPatches { get; set; }
        public IList<CurveDetailsViewModel> CurveDetailsList { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        public IList<CurvePropertiesViewModel> CurvePropertiesList { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public IList<NodePropertiesViewModel> NodePropertiesList { get; set; }
        public IList<PatchGridViewModel> PatchGridList { get; set; }
        public IList<PatchDocumentViewModel> PatchDocumentList { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        public IList<SamplePropertiesViewModel> SamplePropertiesList { get; set; }
        public ScaleGridViewModel ScaleGrid { get; set; }
        public List<ScalePropertiesViewModel> ScalePropertiesList { get; internal set; }
        public IList<ToneGridEditViewModel> ToneGridEditList { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators in all PatchDocuments. </summary>
        public IList<ChildDocumentIDAndNameViewModel> UnderlyingPatchLookup { get; set; }
        public PatchDetailsViewModel AutoPatchDetails { get; set; }
    }
}
