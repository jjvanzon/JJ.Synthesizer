using JJ.Persistence.Synthesizer;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Wishes
{
    // Sample SynthWishes
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, int bytesToSkip = 0, 
            [CallerMemberName] string callerMemberName = null)
            => SampleBase(new MemoryStream(bytes), bytesToSkip, callerMemberName);
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            Stream stream, int bytesToSkip = 0,
            [CallerMemberName] string callerMemberName = null)
            => SampleBase(stream, bytesToSkip, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(string fileName = null, int bytesToSkip = 0, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, explicitName: fileName);
            name = Path.GetFileNameWithoutExtension(name);
            string filePath = FormatAudioFileName(name, GetAudioFormat);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return SampleBase(stream, bytesToSkip, name, callerMemberName);
        }

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(Stream stream, int bytesToSkip, string name1, string name2 = null)
        {
            string name = FetchName(name1, name2);
            name = Path.GetFileNameWithoutExtension(name);
            string filePath = FormatAudioFileName(name, GetAudioFormat);

            if (stream == null) throw new ArgumentNullException(nameof(stream));

            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.SampleDataType.GetMaxAmplitude();
            sample.TimeMultiplier = 1;
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

    }
}
