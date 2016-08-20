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
        public Dictionary<int, CurvePropertiesViewModel> CurvePropertiesDictionary { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public Dictionary<int, NodePropertiesViewModel> NodePropertiesDictionary { get; set; }
        /// <summary> Key is GroupName lower case. Groupless patches have key "". </summary>
        public Dictionary<string, PatchGridViewModel> PatchGridDictionary { get; set; }
        /// <summary> Key is ChildDocument ID. </summary>
        public Dictionary<int, PatchDocumentViewModel> PatchDocumentDictionary { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        public Dictionary<int, SamplePropertiesViewModel> SamplePropertiesDictionary { get; set; }
        public ScaleGridViewModel ScaleGrid { get; set; }
        public List<ScalePropertiesViewModel> ScalePropertiesList { get; internal set; }
        /// <summary> Key is Scale ID. </summary>
        public Dictionary<int, ToneGridEditViewModel> ToneGridEditDictionary { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators in all PatchDocuments. </summary>
        public IList<ChildDocumentIDAndNameViewModel> UnderlyingPatchLookup { get; set; }
    }
}
