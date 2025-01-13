using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable RedundantNameQualifier

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    // ConfigWishes
    
    public partial class ConfigWishes
    {
        // Environment Variables

        internal const string NCrunchEnvironmentVariableName         = "NCrunch";
        internal const string AzurePipelinesEnvironmentVariableValue = "True";
        internal const string AzurePipelinesEnvironmentVariableName  = "TF_BUILD";
        internal const string NCrunchEnvironmentVariableValue        = "1";
                        
        // Constants
        
        public const int ChannelsEmpty = 0;
        public const int MonoChannels = 1;
        public const int StereoChannels = 2;
        public const int CenterChannel = 0;
        public const int LeftChannel = 0;
        public const int RightChannel = 1;
        public static readonly int? ChannelEmpty = null;
        public static readonly int? EveryChannel = null;

        // Coalesce Defaults
        
        public static int CoalesceBits(int? bits)
        {
            if (!Has(bits)) return DefaultBits;
            return bits.Value;
        }

        public static int CoalesceChannels(int? channels)
        {
            if (!Has(channels)) return DefaultChannels;
            return channels.Value;
        }
    }
}
