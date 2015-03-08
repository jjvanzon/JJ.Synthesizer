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
    internal class ByteAudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public ByteAudioFileOutputCalculator(AudioFileOutput audioFileOutput, string filePath)
            : base(audioFileOutput, filePath)
        { }

        public override void Execute()
        {
            // More code because of being paranoid the C# or IL compiler might not optimize it out.
            double endTime = _endTime;
            double channelCount = _channelCount;
            double dt = _dt;

            using (Stream stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (double t = 0; t <= endTime; t += dt)
                    {
                        for (int i = 0; i < channelCount; i++)
                        {
                            Outlet outlet = _outlets[i];

                            byte value = 0;
                            if (outlet != null) // TODO: I do not like this 'if'.
                            {
                                double d = _operatorCalculators[i].CalculateValue(outlet, t);
                                d *= _audioFileOutput.Amplifier;
                                d += 128;
                                value = (byte)d;
                            }

                            writer.Write(value);
                        }
                    }
                }
            }
        }
    }
}
