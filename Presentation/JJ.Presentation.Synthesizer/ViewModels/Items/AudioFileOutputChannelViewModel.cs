using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
    public sealed class AudioFileOutputChannelViewModel
    {
        public int ID { get; set; }
        public int IndexNumber { get; set; }
        public string Name { get; set; }

        /// <summary> nullable </summary>
        public IDAndName Outlet { get; set; }
    }
}
