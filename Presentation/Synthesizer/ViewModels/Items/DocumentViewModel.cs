using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class DocumentViewModel
    {
        public bool IsOpen { get; set; }
        public int ID { get; set; }
        public AudioFileOutputGridViewModel AudioFileOutputGrid { get; set; }
        public AudioFileOutputPropertiesViewModel VisibleAudioFileOutputProperties { get; set; }
        public Dictionary<int, AudioFileOutputPropertiesViewModel> AudioFileOutputPropertiesDictionary { get; set; }
        public AudioOutputPropertiesViewModel AudioOutputProperties { get; set; }
        public PatchDetailsViewModel AutoPatchDetails { get; set; }
        public CurrentPatchesViewModel CurrentPatches { get; set; }
        public CurveDetailsViewModel VisibleCurveDetails { get; set; }
        public Dictionary<int, CurveDetailsViewModel> CurveDetailsDictionary { get; set; }
        public CurveGridViewModel CurveGrid { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCurves. </summary>
        public IList<IDAndName> CurveLookup { get; set; }
        public CurvePropertiesViewModel VisibleCurveProperties { get; set; }
        public Dictionary<int, CurvePropertiesViewModel> CurvePropertiesDictionary { get; set; }
        public DocumentPropertiesViewModel DocumentProperties { get; set; }
        public LibraryGridViewModel LibraryGrid { get; set; }
        public LibrarySelectionPopupViewModel LibrarySelectionPopup { get; set; }
        public Dictionary<int, DocumentReferencePropertiesViewModel> DocumentReferencePropertiesDictionary { get; set; }
        public DocumentReferencePropertiesViewModel VisibleDocumentReferencePropertiesViewModel { get; set; }
        public DocumentTreeViewModel DocumentTree { get; set; }
        public NodePropertiesViewModel VisibleNodeProperties { get; set; }
        public Dictionary<int, NodePropertiesViewModel> NodePropertiesDictionary { get; set; }
        public OperatorPropertiesViewModel VisibleOperatorProperties { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel> OperatorPropertiesDictionary { get; set; }
        public OperatorPropertiesViewModel_ForCache VisibleOperatorProperties_ForCache { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCache> OperatorPropertiesDictionary_ForCaches { get; set; }
        public OperatorPropertiesViewModel_ForCurve VisibleOperatorProperties_ForCurve { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCurve> OperatorPropertiesDictionary_ForCurves { get; set; }
        public OperatorPropertiesViewModel_ForCustomOperator VisibleOperatorProperties_ForCustomOperator { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForCustomOperator> OperatorPropertiesDictionary_ForCustomOperators { get; set; }
        public OperatorPropertiesViewModel_ForInletsToDimension VisibleOperatorProperties_ForInletsToDimension { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForInletsToDimension> OperatorPropertiesDictionary_ForInletsToDimension { get; set; }
        public OperatorPropertiesViewModel_ForNumber VisibleOperatorProperties_ForNumber { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForNumber> OperatorPropertiesDictionary_ForNumbers { get; set; }
        public OperatorPropertiesViewModel_ForPatchInlet VisibleOperatorProperties_ForPatchInlet { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesDictionary_ForPatchInlets { get; set; }
        public OperatorPropertiesViewModel_ForPatchOutlet VisibleOperatorProperties_ForPatchOutlet { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesDictionary_ForPatchOutlets { get; set; }
        public OperatorPropertiesViewModel_ForSample VisibleOperatorProperties_ForSample { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_ForSample> OperatorPropertiesDictionary_ForSamples { get; set; }
        public OperatorPropertiesViewModel_WithInterpolation VisibleOperatorProperties_WithInterpolation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithInterpolation> OperatorPropertiesDictionary_WithInterpolation { get; set; }
        public OperatorPropertiesViewModel_WithCollectionRecalculation VisibleOperatorProperties_WithCollectionRecalculation { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithCollectionRecalculation> OperatorPropertiesDictionary_WithCollectionRecalculation { get; set; }
        public OperatorPropertiesViewModel_WithInletCount VisibleOperatorProperties_WithInletCount { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithInletCount> OperatorPropertiesDictionary_WithInletCount { get; set; }
        public OperatorPropertiesViewModel_WithOutletCount VisibleOperatorProperties_WithOutletCount { get; set; }
        public Dictionary<int, OperatorPropertiesViewModel_WithOutletCount> OperatorPropertiesDictionary_WithOutletCount { get; set; }
        public PatchDetailsViewModel VisiblePatchDetails { get; set; }
        public Dictionary<int, PatchDetailsViewModel> PatchDetailsDictionary { get; set; }
        public PatchGridViewModel VisiblePatchGrid { get; set; }
        /// <summary> Key is GroupName lower case. Groupless patches have key "". </summary>
        public Dictionary<string, PatchGridViewModel> PatchGridDictionary { get; set; }
        public PatchPropertiesViewModel VisiblePatchProperties { get; set; }
        public Dictionary<int, PatchPropertiesViewModel> PatchPropertiesDictionary { get; set; }
        public SampleGridViewModel SampleGrid { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForSamples. </summary>
        public IList<IDAndName> SampleLookup { get; set; }
        public SamplePropertiesViewModel VisibleSampleProperties { get; set; }
        public Dictionary<int, SamplePropertiesViewModel> SamplePropertiesDictionary { get; set; }
        public ScaleGridViewModel ScaleGrid { get; set; }
        public ScalePropertiesViewModel VisibleScaleProperties { get; set; }
        public Dictionary<int, ScalePropertiesViewModel> ScalePropertiesDictionary { get; set; }
        public ToneGridEditViewModel VisibleToneGridEdit { get; set; }
        /// <summary> Key is Scale ID. </summary>
        public Dictionary<int, ToneGridEditViewModel> ToneGridEditDictionary { get; set; }
        /// <summary> This lookup is used by OperatorProperties_ForCustomOperators. </summary>
        public IList<IDAndName> UnderlyingPatchLookup { get; set; }
    }
}
