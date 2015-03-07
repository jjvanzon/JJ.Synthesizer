using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Infrastructure.Synthesizer
{
    internal class StereoSampleLoader : SampleLoaderBase
    {
        public StereoSampleLoader(Sample sample)
            : base(sample)
        {
            if (sample.SampleChannels.Count != 2)
            {
                throw new Exception("sample.SampleChannels.Count must be 2.");
            }

            if (sample.SampleChannels[0].GetChannelEnum() != ChannelEnum.Left)
            {
                throw new Exception("sample.SampleChannels[0].Channel must be 'Left'.");
            }

            if (sample.SampleChannels[1].GetChannelEnum() != ChannelEnum.Right)
            {
                throw new Exception("sample.SampleChannels[1].Channel must be 'Right'.");
            }
        }

        public override void Load(Stream stream)
        {
            if (stream == null) throw new NullException(() => stream);

            stream.Position = _sample.BytesToSkip;
            byte[] sourceBytes = StreamHelper.StreamToBytes(stream);

            long destLength = sourceBytes.Length / 2;
            byte[] destBytes1 = new byte[destLength];
            byte[] destBytes2 = new byte[destLength];

            int i = 0;
            for (int j = 0; j < sourceBytes.Length - 1; j++)
            {
                destBytes1[i] = sourceBytes[j];
                j++;
                destBytes2[i] = sourceBytes[j];
                i++;
            }
             
            _sample.SampleChannels[0].RawBytes = destBytes1;
            _sample.SampleChannels[1].RawBytes = destBytes2;
        }
    }
}
