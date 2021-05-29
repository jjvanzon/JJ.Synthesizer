using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public class SampleFileBrowserViewModel : ScreenViewModelBase
    {
        public byte[] Bytes { get; set; }
        public int DestPatchID { get; set; }
        public string FilePath { get; set; }
        internal int CreatedMainOperatorID { get; set; }
        internal IList<int> AutoCreatedNumberOperatorIDs { get; set; }
    }
}