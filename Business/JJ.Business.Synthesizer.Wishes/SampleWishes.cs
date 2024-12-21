using System;
using JJ.Persistence.Synthesizer;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.IO.StreamHelper;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;

namespace JJ.Business.Synthesizer.Wishes
{
    // Sample SynthWishes
    
    public partial class SynthWishes
    {
        public FlowNode Sample(
            Tape tape, 
            int bytesToSkip = 0/*, string name = null, [CallerMemberName] string callerMemberName = null*/)
        {
            return SampleFromTape(tape, bytesToSkip);
        }

        /// <inheritdoc cref="docs._sample"/>
        public FlowNode Sample(
            Buff buff,
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new NullException(() => buff);
            return SampleBase(
                null, buff.Bytes, buff.FilePath, 
                bytesToSkip, name, callerMemberName);
        }
                
        /// <inheritdoc cref="docs._sample"/>
        public FlowNode Sample(
            byte[] bytes, string filePath, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, bytes, filePath, 
                bytesToSkip, name, callerMemberName);
        
        /// <inheritdoc cref="docs._sample"/>
        public FlowNode Sample(
            byte[] bytes, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, bytes, null, 
                bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        public FlowNode Sample(
            string filePath, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, null, filePath, 
                bytesToSkip, name, callerMemberName);
        
        /// <inheritdoc cref="docs._sample"/>
        public FlowNode Sample(
            Stream stream, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                stream, null, null, 
                bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="docs._sample"/>
        private FlowNode SampleBase(
            Stream stream, byte[] bytes, string filePath, 
            int bytesToSkip, string name, [CallerMemberName] string callerMemberName = null)
        {
            // Resolve where our data comes from
            name = ResolveName(name, filePath, callerMemberName);
            filePath = ResolveFilePath(GetAudioFormat, filePath, name, callerMemberName);
            stream = ResolveStream(stream, bytes, filePath);
            
            // Wrap it in a Sample
            Sample sample = _sampleManager.CreateSample(stream);
            sample.Amplifier = 1.0 / sample.MaxValue();
            sample.BytesToSkip = bytesToSkip;
            sample.Location = filePath;
            sample.SetInterpolation(GetInterpolation, Context);

            var sampleNode = _[_operatorFactory.Sample(sample)];
            
            sample.Name = name;
            sampleNode.UnderlyingOperator.Name = name;
            
            LogSampleCreated(sample);
            
            return sampleNode;
        }
        
        private static Stream ResolveStream(Stream stream, byte[] bytes, string filePath)
        {
            // Return Stream if supplied
            if (stream != null)
            {
                return stream;
            }

            // Optionally Load Bytes from File
            if (bytes == null)
            { 
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    bytes = StreamToBytes(fileStream);
                }
            }

            // Create Stream from Bytes
            return BytesToStream(bytes);
        }

        // SampleFromTape
        
        private FlowNode SampleFromTape(Tape tape, int bytesToSkip = 0)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            Stream stream = ResolveStream(null, tape.Bytes, tape.FilePathResolved);
            Sample sample = _sampleManager.CreateSample(stream);
            
            sample.BytesToSkip = bytesToSkip;
            sample.Name = tape.GetDescriptor();
            sample.Amplifier = 1.0 / tape.Bits.MaxValue();
            sample.Location = tape.GetFilePath();
            
            if (sample.AudioFormat() == Raw)
            {
                // Not detected from header, so we need to set it manually.
                sample.SamplingRate = tape.SamplingRate;
                sample.SetChannels(tape.Channels, Context);
                sample.SetBits(tape.Bits, Context);
                sample.SetInterpolation(tape.Interpolation, Context);
            }
            
            var sampleNode = _[_operatorFactory.Sample(sample)];
            return sampleNode.SetName(sample.Name);
        }
        
        // SampleFromFluentConfig (currently unused)
        
        /// <inheritdoc cref="docs._samplefromfluentconfig" />
        private FlowNode SampleFromFluentConfig(
            byte[] bytes, int bytesToSkip = 0, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            FlowNode sampleOutlet = SampleFromFluentConfig(name, callerMemberName);
            Sample sample = sampleOutlet.UnderlyingSample();
            sample.Bytes = bytes;
            sample.BytesToSkip = bytesToSkip;

            return sampleOutlet;
        }

        /// <inheritdoc cref="docs._samplefromfluentconfig" />
        private FlowNode SampleFromFluentConfig(
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            name = ResolveName(callerMemberName, explicitNameSource: name);
            name = Path.GetFileNameWithoutExtension(name);
            string location = Path.GetFullPath(FormatAudioFileName(name, GetAudioFormat)); // Back-end wants a path.

            Sample sample = _sampleManager.CreateSample();
            sample.Location =  location;
            sample.Amplifier = 1.0 / GetBits.MaxValue();
            sample.SamplingRate = GetSamplingRate;
            sample.SetBits(GetBits, Context);
            sample.SetChannels(GetChannels, Context);
            sample.SetAudioFormat(GetAudioFormat, Context);
            sample.SetInterpolation(GetInterpolation, Context);
            
            var sampleNode = _[_operatorFactory.Sample(sample)];

            return sampleNode.SetName(name);
        }
    }
}
