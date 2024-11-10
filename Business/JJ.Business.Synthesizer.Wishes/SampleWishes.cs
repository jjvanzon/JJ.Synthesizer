using System;
using JJ.Persistence.Synthesizer;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.IO.StreamHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    // Sample SynthWishes
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, int bytesToSkip = 0, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(null, bytes, null, bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(Stream stream, int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(stream, null, null, bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(string filePath, int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(null, null, filePath, bytesToSkip, name, callerMemberName);
        
        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            byte[] bytes, string filePath, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, bytes, filePath, 
                bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            Result<StreamAudioData> result,
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (result == null) throw new NullException(() => result);
            return Sample(
                result.Data,
                bytesToSkip, name,callerMemberName);
        }

        /// <inheritdoc cref="docs._sample"/>
        public FluentOutlet Sample(
            StreamAudioData data,
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (data == null) throw new NullException(() => data);
            return SampleBase(
                null, data.Bytes, data.AudioFileOutput?.FilePath, 
                bytesToSkip, name, callerMemberName);
        }

        /// <inheritdoc cref="docs._sample"/>
        private FluentOutlet SampleBase(Stream stream, byte[] bytes, string explicitFilePath, int bytesToSkip, string nameOrFilePath, [CallerMemberName] string callerMemberName = null)
        {
            // Resolve Name (with priority given to nameOrFilePath)
            nameOrFilePath = FetchName(nameOrFilePath, callerMemberName);
            string name = PrettifyName(nameOrFilePath);

            // Resolve FilePath (with priority given to explicitFilePath)
            string filePath;
            if (!string.IsNullOrWhiteSpace(explicitFilePath))
            {
                string fileName = FormatAudioFileName(explicitFilePath, GetAudioFormat);
                string folderPath = Path.GetDirectoryName(explicitFilePath);
                if (string.IsNullOrWhiteSpace(folderPath)) folderPath = Directory.GetCurrentDirectory();
                folderPath = Path.GetFullPath(folderPath);
                filePath = Path.Combine(folderPath, fileName);
            }
            else
            {
                filePath = Path.GetFullPath(FormatAudioFileName(nameOrFilePath, GetAudioFormat));
            }
            
            // Resolve a Stream (back-end needs it)
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
            sample.Location = filePath;
            sample.SetInterpolation(GetInterpolation, Context);

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
