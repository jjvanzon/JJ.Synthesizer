using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteCalculationWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteCalculationWishesMessages
    {
        public const string ObsoleteMessage = 
            "Use of Channel entities and ChannelEnum is discourage. Use the integers 0 and 1 instead. 0 is center channel in case of mono, and left channel in case of stereo. 1 is right channel in case of stereo.";
    }

    public static class ObsoleteCalculationExtensionWishes
    {
        public static double Calculate(this Sample sample, double time, Channel channel)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return sample.Calculate(time, channel.Index);
        }

        public static double Calculate(this Sample sample, double time, ChannelEnum channelEnum)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return sample.Calculate(time, channelEnum.ToIndex());
        }
        
        public static double Calculate(this Outlet outlet, double time, ChannelEnum channelEnum)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return outlet.Calculate(time, channelEnum.ToIndex());
        }
        
        public static double Calculate(this Operator op, double time, ChannelEnum channelEnum)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return op.Calculate(time, channelEnum.ToIndex());
        }
    }
}
