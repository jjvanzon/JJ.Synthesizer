using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Config;
using static JJ.Framework.Core.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteCalculationWishesMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteCalculationWishesMessages
    {
        public const string ObsoleteMessage = 
            "The use of Channel entities and ChannelEnum is discourage. " +
            "Use the integers 0 and 1 instead. 0 is center channel in case of mono, " +
            "and in case of stereo, left is 0 and right is 1.";
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteCalculationExtensionWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Sample sample, double time, Channel channel)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return sample.Calculate(time, channel.Index);
        }

        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Sample sample, double time, ChannelEnum channelEnum)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return sample.Calculate(time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Outlet outlet, double time, ChannelEnum channelEnum)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return outlet.Calculate(time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this Operator op, double time, ChannelEnum channelEnum)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return op.Calculate(time, channelEnum.Channel().Value);
        }
                
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this FlowNode flowNode, double time, ChannelEnum channelEnum)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            return flowNode.UnderlyingOutlet.Calculate(time, channelEnum);
        }
    }
}
