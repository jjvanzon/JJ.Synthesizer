using System;
using JJ.Persistence.Synthesizer;
using System.IO;
using System.Runtime.CompilerServices;
using static JJ.Framework.IO.StreamHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    // Sample SynthWishes
    
    public partial class SynthWishes
    {
        // TODO: Overloads with SaveResult, SaveResultData, AudioFileOutput, Stream?
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, int bytesToSkip = 0,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(null, bytes, bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(Stream stream, int bytesToSkip = 0, [CallerMemberName] string callerMemberName = null)
            => SampleBase(stream, null, bytesToSkip, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(string fileName = null, int bytesToSkip = 0, [CallerMemberName] string callerMemberName = null)
            => SampleBase(null, null, bytesToSkip, fileName, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(Stream stream, byte[] bytes, int bytesToSkip, string name1, string name2 = null)
        {
            // Resolve FilePath
            string name = FetchName(name1, name2);
            name = Path.GetFileNameWithoutExtension(name);
            string filePath = FormatAudioFileName(name, GetAudioFormat);

            // Resolve a Stream
            if (stream == null)
            {
                if (bytes != null)
                {
                    // Load Stream from Bytes
                    stream = BytesToStream(bytes);
                }
                else
                {
                    // Load Bytes from File
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        bytes = StreamToBytes(fileStream);
                    }

                    // Load Bytes in Memory Stream (unfortunately more resilient).
                    stream = BytesToStream(bytes);
                }
            }
            
            // Wrap 

            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.GetNominalMax();
            sample.BytesToSkip = bytesToSkip;
            sample.SetInterpolation(GetInterpolation, Context);

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                sample.Location = Path.GetFullPath(filePath);
            }

            var sampleOutlet = _[_operatorFactory.Sample(sample)];
            
            sample.Name = name;
            sampleOutlet.Operator.Name = name;

            return sampleOutlet;
        }

        // SampleFromFluentConfig (currently unused)
        
        /// <inheritdoc cref="docs._samplefromfluentconfig" />
        private FluentOutlet SampleFromFluentConfig(
            byte[] bytes, int bytesToSkip = 0, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            FluentOutlet sampleOutlet = SampleFromFluentConfig(name, callerMemberName);
            Sample sample = sampleOutlet.UnderlyingSample();
            sample.Bytes = bytes;
            sample.BytesToSkip = bytesToSkip;

            return sampleOutlet;
        }

        /// <inheritdoc cref="docs._samplefromfluentconfig" />
        private FluentOutlet SampleFromFluentConfig(
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            name = FetchName(name, callerMemberName);
            name = Path.GetFileNameWithoutExtension(name);
            string location = Path.GetFullPath(FormatAudioFileName(name, GetAudioFormat)); // Back-end wants a path.

            Sample sample = _sampleManager.CreateSample();
            sample.Location =  location;
            sample.Amplifier = 1.0 / GetBitDepth.GetNominalMax();
            sample.SamplingRate = ResolveSamplingRate().Data;
            sample.SetBitDepth(GetBitDepth, Context);
            sample.SetSpeakerSetup(GetSpeakerSetup, Context);
            sample.SetAudioFormat(GetAudioFormat, Context);
            sample.SetInterpolation(GetInterpolation, Context);
            
            var sampleOutlet = _[_operatorFactory.Sample(sample)];

            return sampleOutlet.SetName(name);
        }
    }
}
