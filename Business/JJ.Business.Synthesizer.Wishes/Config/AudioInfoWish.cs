using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="_audioinfowish"/>
    public class AudioInfoWish
    {
        public override string ToString() => GetDebuggerDisplay(this);
        
        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="_framecount"/>
        public int FrameCount { get; set; }
    }
}