using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Framework.Wishes.Common;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        internal static string DebuggerDisplay(SynthBoundEntities obj) 
            => nameof(SynthBoundEntities) + ": " + obj?.SynthWishes.Coalesce("<null>");

        internal static string DebuggerDisplay(TapeBoundEntities obj) 
            => nameof(TapeBoundEntities) + ": " + obj?.Tape.Coalesce("<null>");

        internal static string DebuggerDisplay(BuffBoundEntities obj) 
            => nameof(BuffBoundEntities) + ": " + obj?.Buff.Coalesce("<null>");

        internal static string DebuggerDisplay(IndependentEntities obj)
            => nameof(IndependentEntities) + ": " + obj?.AudioInfoWish.Coalesce("<null>");

        internal static string DebuggerDisplay(ImmutableEntities obj)
        {
            string descriptor;
            if (obj == null)
            {
                descriptor = "<null>";
            }
            else
            {
                string audioFormatDescriptor = AudioFormatDescriptor(obj.SamplingRate, obj.Bits, obj.Channels, obj.Channel, obj.AudioFormat, obj.Interpolation);
                descriptor = ConfigLog(title: "", audioFormatDescriptor, DurationsDescriptor(obj.AudioLength), sep: " | ");
            }

            return nameof(ImmutableEntities) + ": " + descriptor;
        }

        internal static string DebuggerDisplay(TapeEntities obj) 
            => nameof(TapeEntities) + ": " + obj?.TapeBound.Coalesce("<null>");

        internal static string DebuggerDisplay(ConfigTestEntities obj) 
            => nameof(ConfigTestEntities) + ": " + obj?.SynthBound?.SynthWishes.Coalesce("<null>");
    }
}
