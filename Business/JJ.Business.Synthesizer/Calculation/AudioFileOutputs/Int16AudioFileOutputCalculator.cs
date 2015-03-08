using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        private class ChannelTuple
        {
            public AudioFileOutputChannel AudioFileOutputChannel { get; set; }
            public OperatorCalculator OperatorCalculator { get; set; } 
        }

        public Int16AudioFileOutputCalculator(AudioFileOutput audioFileOutput, string filePath)
            : base(audioFileOutput, filePath)
        { }

        public override void Execute()
        {
            IList<ChannelTuple> channelTuples = new List<ChannelTuple>();
            foreach (AudioFileOutputChannel audioFileOutputChannel in _audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.Index))
            {
                var channelTuple = new ChannelTuple
                {
                    AudioFileOutputChannel = audioFileOutputChannel,
                    OperatorCalculator = new OperatorCalculator(audioFileOutputChannel.Index)
                };
                channelTuples.Add(channelTuple);
            }

            double dt = 1.0 / _audioFileOutput.SamplingRate / _audioFileOutput.TimeMultiplier;
            double endTime = _audioFileOutput.GetEndTime();
            int channelCount = _audioFileOutput.GetChannelCount();

            using (Stream stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (double t = 0; t <= endTime; t += dt)
                    {
                        for (int i = 0; i < channelCount; i++)
                        {
                            ChannelTuple channelTuple = channelTuples[i];

                            short value = 0;

                            // TODO: I do not like this 'if'.
                            // TODO: Could making separate arrays instead of a tuple improve performance?
                            if (channelTuple.AudioFileOutputChannel.Outlet != null)
                            {
                                double d = channelTuple.OperatorCalculator.CalculateValue(channelTuple.AudioFileOutputChannel.Outlet, t);
                                d *= _audioFileOutput.Amplifier;
                                value = (short)d;
                            }

                            writer.Write(value);
                        }
                    }
                }
            }
        }
    }
}
