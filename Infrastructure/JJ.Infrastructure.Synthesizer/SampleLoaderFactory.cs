using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Infrastructure.Synthesizer
{
    internal static class SampleLoaderFactory
    {
        public static ISampleLoader CreateSampleLoader(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            SampleDataTypeEnum sampleDataType = sample.GetSampleDataTypeEnum();
            ChannelSetupEnum channelSetup = sample.GetChannelSetupEnum();

            switch (channelSetup)
            {
                case ChannelSetupEnum.Mono:
                    return new MonoSampleLoader(sample);

                case ChannelSetupEnum.Stereo:
                    return new StereoSampleLoader(sample);

                default:
                    throw new ValueNotSupportedException(channelSetup);
            }
        }
    }
}
