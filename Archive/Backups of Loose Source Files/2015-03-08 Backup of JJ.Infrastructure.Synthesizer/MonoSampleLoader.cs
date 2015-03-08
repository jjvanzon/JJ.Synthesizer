using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.IO;
using JJ.Framework.Reflection;

namespace JJ.Infrastructure.Synthesizer
{
    internal class MonoSampleLoader : SampleLoaderBase
    {
        public MonoSampleLoader(Sample sample)
            : base(sample)
        {
            if (sample.SampleChannels.Count != 1)
            {
                throw new Exception("sample.SampleChannels.Count must be 1.");
            }

            if (sample.SampleChannels[0].GetChannelEnum() != ChannelEnum.Single)
            {
                throw new Exception("sample.SampleChannels[0].Channel must be 'Single'.");
            }
        }

        public override void Load(Stream stream)
        {
            if (stream == null) throw new NullException(() => stream);

            stream.Position = _sample.BytesToSkip;

            _sample.SampleChannels[0].RawBytes = StreamHelper.StreamToBytes(stream);
        }
    }
}
