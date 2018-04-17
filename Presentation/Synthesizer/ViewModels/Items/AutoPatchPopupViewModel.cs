using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	/// <summary>
	/// Main screen inside this view model is the PatchDetails,
	/// but the other accompanying screens are necessary to be able to save the complete Patch 
	/// object graph: PatchProperties and all the different variations of the OperatorProperties view models.
	/// </summary>
	public sealed class AutoPatchPopupViewModel : ScreenViewModelBase
	{
		public PatchDetailsViewModel PatchDetails { get; set; }
		public PatchPropertiesViewModel PatchProperties { get; set; }

		public Dictionary<int, OperatorPropertiesViewModel> OperatorPropertiesDictionary { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForCache> OperatorPropertiesDictionary_ForCaches { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForCurve> OperatorPropertiesDictionary_ForCurves { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForInletsToDimension> OperatorPropertiesDictionary_ForInletsToDimension { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForNumber> OperatorPropertiesDictionary_ForNumbers { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForPatchInlet> OperatorPropertiesDictionary_ForPatchInlets { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForPatchOutlet> OperatorPropertiesDictionary_ForPatchOutlets { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_ForSample> OperatorPropertiesDictionary_ForSamples { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_WithInterpolation> OperatorPropertiesDictionary_WithInterpolation { get; set; }
		public Dictionary<int, OperatorPropertiesViewModel_WithCollectionRecalculation> OperatorPropertiesDictionary_WithCollectionRecalculation { get; set; }
	}
}
