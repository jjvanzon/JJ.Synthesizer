﻿using System;
using JJ.Persistence.Synthesizer;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Framework.Reflection;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.IO.Legacy.StreamHelper;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogActions;

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

        /// <inheritdoc cref="_sample"/>
        public FlowNode Sample(
            Buff buff,
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new NullException(() => buff);
            return SampleBase(
                null, buff.Bytes, buff.FilePath, 
                bytesToSkip, name, callerMemberName);
        }
                
        /// <inheritdoc cref="_sample"/>
        public FlowNode Sample(
            byte[] bytes, string filePath, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, bytes, filePath, 
                bytesToSkip, name, callerMemberName);
        
        /// <inheritdoc cref="_sample"/>
        public FlowNode Sample(
            byte[] bytes, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, bytes, null, 
                bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="_sample"/>
        public FlowNode Sample(
            string filePath, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                null, null, filePath, 
                bytesToSkip, name, callerMemberName);
        
        /// <inheritdoc cref="_sample"/>
        public FlowNode Sample(
            Stream stream, 
            int bytesToSkip = 0, string name = null, [CallerMemberName] string callerMemberName = null)
            => SampleBase(
                stream, null, null, 
                bytesToSkip, name, callerMemberName);

        /// <inheritdoc cref="_sample"/>
        private FlowNode SampleBase(
            Stream stream, byte[] bytes, string filePath, 
            int bytesToSkip, string name, [CallerMemberName] string callerMemberName = null)
            => SampleBaseOld(stream, bytes, filePath, bytesToSkip, name, callerMemberName);

        // TODO: Still gives audio problems in Metallophone tests.
        /// <inheritdoc cref="_sample"/>
        private FlowNode SampleBaseLegacy(
            Stream stream, byte[] bytes, string filePath, 
            int bytesToSkip, string name, [CallerMemberName] string callerMemberName = null)
        {
            Tape dummyTape = CloneTape(this);
            
            dummyTape.Bytes = bytes;
            dummyTape.FilePathSuggested = filePath;
            dummyTape.FilePathResolved = filePath;
            dummyTape.FallbackName = ResolveName(name, callerMemberName);
            
            dummyTape.Config.Channel = default; // ???

            dummyTape.LogAction(Create, "Sample Dummy");

            return SampleFromTape(dummyTape, bytesToSkip, stream);
        }
        
        /// <inheritdoc cref="_sample"/>
        private FlowNode SampleBaseOld(
            Stream stream, byte[] bytes, string filePath, 
            int bytesToSkip, string name, [CallerMemberName] string callerMemberName = null)
        {
            // Resolve where our data comes from
            name = ResolveName(name, filePath, callerMemberName);
            filePath = ResolveFilePath(GetAudioFormat, filePath, name);
            stream = ResolveStream(stream, bytes, filePath);
            
            // Wrap it in a Sample
            Sample sample = _sampleManager.CreateSample(stream);
            
            if (sample.AudioFormat() == Raw)
            {
                // Not detected from header, so we need to set it manually.
                sample.SamplingRate = GetSamplingRate;
                sample.Bits(GetBits, Context);
                sample.Channels(GetChannels, Context);
            }

            sample.Amplifier = 1.0 / sample.MaxAmplitude();
            sample.BytesToSkip = bytesToSkip;
            sample.Location = filePath;
            sample.SetInterpolationTypeEnum(GetInterpolation, Context);

            var sampleNode = _[_operatorFactory.Sample(sample)];
            
            sample.Name = name;
            sampleNode.UnderlyingOperator.Name = name;

            sample.LogAction(this, Create);
            
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
        
        private FlowNode SampleFromTape(Tape tape, int bytesToSkip = 0, Stream stream = null)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            stream = stream ?? ResolveStream(null, tape.Bytes, tape.FilePathResolved);
            Sample sample = _sampleManager.CreateSample(stream);
            
            if (sample.AudioFormat() == Raw)
            {
                // Not detected from header, so we need to set it manually.
                sample.SamplingRate = tape.Config.SamplingRate;
                sample.Channels(tape.Config.Channels, Context);
                sample.Bits(tape.Config.Bits, Context);
            }
            
            sample.BytesToSkip = bytesToSkip;
            sample.Name = tape.Descriptor();
            sample.Amplifier = 1.0 / tape.Config.Bits.MaxAmplitude();
            sample.Location = tape.GetFilePath();
            sample.SetInterpolationTypeEnum(tape.Config.Interpolation, Context);
            
            var sampleNode = _[_operatorFactory.Sample(sample)];
            sampleNode.SetName(sample.Name);
            
            sample.LogAction(this, Create);
            
            return sampleNode;
        }
        
        // SampleFromFluentConfig (currently unused)
        
        /// <inheritdoc cref="_samplefromfluentconfig" />
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

        /// <inheritdoc cref="_samplefromfluentconfig" />
        private FlowNode SampleFromFluentConfig(
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            name = ResolveName(name, callerMemberName);
            name = Path.GetFileNameWithoutExtension(name);
            string location = Path.GetFullPath(FormatAudioFileName(name, GetAudioFormat)); // Back-end wants a path.

            Sample sample = _sampleManager.CreateSample();
            sample.Location =  location;
            sample.Amplifier = 1.0 / GetBits.MaxAmplitude();
            sample.SamplingRate = GetSamplingRate;
            sample.Bits(GetBits, Context);
            sample.Channels(GetChannels, Context);
            sample.AudioFormat(GetAudioFormat, Context);
            sample.Interpolation(GetInterpolation, Context);
            
            var sampleNode = _[_operatorFactory.Sample(sample)];
            sampleNode.SetName(sample.Name);
            
            sample.LogAction(this, Create);
            
            return sampleNode;
        }
    }
}
