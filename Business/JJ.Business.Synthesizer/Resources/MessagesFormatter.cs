using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Resources
{
    public static class MessagesFormatter
    {
        public static string InletNotSet(string operatorTypeName, string operatorName, string operandName)
        {
            return String.Format(Messages.InletNotSet, operatorTypeName, operatorName, operandName);
        }

        public static string ValueOperatorValueIs0(string valueOperatorName)
        {
            return String.Format(Messages.ValueOperatorValueIs0, valueOperatorName);
        }

        public static string UnsupportedOperatorTypeName(string operatorTypeName)
        {
            return String.Format(Messages.UnsupportedOperatorTypeName, operatorTypeName);
        }

        public static string ChannelNotAllowedForSpeakerSetup(string channelName, string speakerSetupName)
        {
            return String.Format(Messages.ChannelNotAllowedForSpeakerSetup, channelName, speakerSetupName);
        }

        public static string SampleChannelNotLoaded(string sampleName, string channelName)
        {
            return String.Format(Messages.SampleChannelNotLoaded, sampleName, channelName);
        }

        public static string ObjectAmplifier0(string obectTypeName, string objectName)
        {
            return String.Format(Messages.ObjectAmplifier0, obectTypeName, objectName);
        }

        public static string SampleNotActive(string sampleName)
        {
            return String.Format(Messages.SampleNotActive, sampleName);
        }

        public static string SampleCount0(string sampleName, string channelName)
        {
            return String.Format(Messages.SampleCount0, sampleName, channelName);
        }
    }
}
