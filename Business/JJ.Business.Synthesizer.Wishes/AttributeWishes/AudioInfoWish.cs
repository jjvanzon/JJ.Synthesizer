using System.Diagnostics;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._audioinfowish"/>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class AudioInfoWish
    {
        string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
        
        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }
}