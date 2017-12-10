using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
	public class SampleFileBrowserViewModel : ViewModelBase
	{
		public byte[] Bytes { get; set; }
		public int DestPatchID { get; set; }
		public string FilePath { get; set; }
		public IList<int> CreatedOperatorIDs { get; set; }
	}
}