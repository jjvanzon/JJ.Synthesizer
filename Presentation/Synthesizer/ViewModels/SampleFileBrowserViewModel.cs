using System.IO;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public class SampleFileBrowserViewModel : ViewModelBase
    {
        public byte[] Bytes { get; set; }
        public int DestPatchID { get; set; }
        public string FilePath { get; set; }
    }
}