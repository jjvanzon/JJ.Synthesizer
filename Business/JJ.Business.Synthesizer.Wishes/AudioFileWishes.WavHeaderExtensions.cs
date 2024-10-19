using System;
using System.IO;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    public static class WavHeaderExtensionsWishes
    {
        // WriteWavHeader (with BinaryWriter)
        
        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter writer,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WriteWavHeader(
                writer, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                speakerSetupEnum, samplingRate, frameCount);

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter writer,
            int channelCount, int samplingRate, int frameCount)
            => WriteWavHeader(
                writer, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                channelCount, samplingRate, frameCount);

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WriteWavHeader(
                writer, sampleDataTypeEnum, speakerSetupEnum.GetChannelCount(), samplingRate, frameCount);

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            WriteWavHeader(writer, new AudioFileInfoWish
            {
                Bits = sampleDataTypeEnum.GetBits(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            });
        }

        public static void WriteWavHeader(
            this BinaryWriter writer, 
            AudioFileOutput audioFileOutput, int frameCount)
        {
            var info = new AudioFileInfoWish
            {
                Bits = audioFileOutput.GetBits(),
                ChannelCount = audioFileOutput.GetChannelCount(),
                SamplingRate = audioFileOutput.SamplingRate,
                FrameCount = frameCount
            };
            
            WriteWavHeader(writer, info);
        }

        public static void WriteWavHeader(
            this BinaryWriter writer, 
            AudioFileInfoWish info)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (info == null) throw new ArgumentNullException(nameof(info));

            var wavHeaderStruct = info.GetWavHeaderStruct();

            writer.WriteStruct(wavHeaderStruct);
        }
        
        // WriteWavHeader (with Stream)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader<TSampleDataType>(speakerSetupEnum, samplingRate, frameCount);

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            int channelCount, int samplingRate, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader<TSampleDataType>(channelCount, samplingRate, frameCount);

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader(sampleDataTypeEnum, speakerSetupEnum, samplingRate, frameCount);

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader(sampleDataTypeEnum, channelCount, samplingRate, frameCount);

        public static void WriteWavHeader(
            this Stream stream,
            AudioFileOutput audioFileOutput, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader(audioFileOutput, frameCount);

        public static void WriteWavHeader(
            this Stream stream,
            AudioFileInfoWish info)
            => new BinaryWriter(stream).WriteWavHeader(info);

        // WriteWavHeader (with FilePath)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader<TSampleDataType>(fileStream, speakerSetupEnum, samplingRate, frameCount);
        }

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            int channelCount, int samplingRate, int frameCount)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader<TSampleDataType>(fileStream, channelCount, samplingRate, frameCount);
        }

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, sampleDataTypeEnum, speakerSetupEnum, samplingRate, frameCount);
        }

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, sampleDataTypeEnum, channelCount, samplingRate, frameCount);
        }
        
        public static void WriteWavHeader(
            this string filePath,
            AudioFileOutput audioFileOutput, int frameCount)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, audioFileOutput, frameCount);
        }

        public static void WriteWavHeader(
            this string filePath,
            AudioFileInfoWish info)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, info);
        }

        // WriteWavHeader (with AudioFileOutput)
        
        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, BinaryWriter writer, int frameCount)
            => WriteWavHeader(writer, audioFileOutput, frameCount);

        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, Stream stream, int frameCount)
            => WriteWavHeader(stream, audioFileOutput, frameCount);

        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, string filePath, int frameCount)
            => WriteWavHeader(filePath, audioFileOutput, frameCount);

        // WriteWavHeader (with AudioFileInfoWish)

        public static void WriteWavHeader(
            this AudioFileInfoWish audioFileInfo, BinaryWriter writer)
            => WriteWavHeader(writer, audioFileInfo);

        public static void WriteWavHeader(
            this AudioFileInfoWish audioFileInfo, Stream stream)
            => WriteWavHeader(stream, audioFileInfo);

        public static void WriteWavHeader(
            this AudioFileInfoWish audioFileInfo, string filePath)
            => WriteWavHeader(filePath, audioFileInfo);

        // Read AudioFileInfoWish
        
        public static AudioFileInfoWish ReadAudioFileInfo(this string filePath)
        {
            var wavHeaderStruct = ReadWavHeader(filePath);
            return wavHeaderStruct.GetAudioFileInfo();
        }

        public static AudioFileInfoWish ReadAudioFileInfo(this Stream stream)
        {
            var wavHeaderStruct = ReadWavHeader(stream);
            return wavHeaderStruct.GetAudioFileInfo();
        }

        public static AudioFileInfoWish ReadAudioFileInfo(this BinaryReader reader)
        {
            var wavHeaderStruct = ReadWavHeader(reader);
            return wavHeaderStruct.GetAudioFileInfo();
        }

        // Reading Wav Header

        public static WavHeaderStruct ReadWavHeader(this string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return ReadWavHeader(fileStream);
        }

        public static WavHeaderStruct ReadWavHeader(this Stream stream)
            => ReadWavHeader(new BinaryReader(stream));

        public static WavHeaderStruct ReadWavHeader(this BinaryReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.ReadStruct<WavHeaderStruct>();
        }

        // Get WavHeader
        
        public static WavHeaderStruct GetWavHeaderStruct(this Sample sample)
            => GetAudioFileInfo(sample).GetWavHeaderStruct();

        public static WavHeaderStruct GetWavHeaderStruct(this SampleOperator sampleOperator)
            => GetAudioFileInfo(sampleOperator).GetWavHeaderStruct();

        public static WavHeaderStruct GetWavHeaderStruct(this SampleOperatorWrapper wrapper)
            => GetAudioFileInfo(wrapper).GetWavHeaderStruct();

        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileOutput audioFileOutput, int frameCount)
            => GetAudioFileInfo(audioFileOutput, frameCount).GetWavHeaderStruct();

        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileOutputChannel audioFileOutputChannel, int frameCount)
            => GetAudioFileInfo(audioFileOutputChannel, frameCount).GetWavHeaderStruct();
        
        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileInfoWish info) 
            => WavHeaderManager.CreateWavHeaderStruct(info.FromWish());
 
        // Get AudioFileInfoWish (the intermediary)
        
        public static AudioFileInfoWish GetAudioFileInfo(this WavHeaderStruct wavHeader) 
            => WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader).ToWish();

        public static AudioFileInfoWish GetAudioFileInfo(this Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            
            var info = new AudioFileInfoWish
            { 
                Bits = sample.GetBits(),
                ChannelCount = sample.GetChannelCount(),
                SamplingRate = sample.SamplingRate,
                FrameCount = sample.GetFrameCount()
            };
            return info;
        }

        public static AudioFileInfoWish GetAudioFileInfo(this SampleOperator sampleOperator)
        {
            if (sampleOperator == null) throw new ArgumentNullException(nameof(sampleOperator));
            return GetAudioFileInfo(sampleOperator.Sample);
        }

        public static AudioFileInfoWish GetAudioFileInfo(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetAudioFileInfo(wrapper.Sample);
        }

        public static AudioFileInfoWish GetAudioFileInfo(this AudioFileOutput audioFileOutput, int frameCount)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            var info = new AudioFileInfoWish
            { 
                Bits = audioFileOutput.GetBits(),
                ChannelCount = audioFileOutput.GetChannelCount(),
                SamplingRate = audioFileOutput.SamplingRate,
                FrameCount = frameCount
            };
            return info;
        }

        public static AudioFileInfoWish GetAudioFileInfo(this AudioFileOutputChannel audioFileOutputChannel, int frameCount)
        {
            if (audioFileOutputChannel == null) throw new ArgumentNullException(nameof(audioFileOutputChannel));

            var info = GetAudioFileInfo(audioFileOutputChannel.AudioFileOutput, frameCount);
            info.ChannelCount = 1;
            return info;
        }

        public static AudioFileInfoWish ToWish(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            
            return new AudioFileInfoWish
            {
                Bits = info.BytesPerValue * 8,
                ChannelCount = info.ChannelCount,
                FrameCount = info.SampleCount,
                SamplingRate = info.SamplingRate
            };
        }

        public static AudioFileInfo FromWish(this AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return new AudioFileInfo
            {
                BytesPerValue = info.Bits / 8,
                ChannelCount = info.ChannelCount,
                SampleCount = info.FrameCount,
                SamplingRate = info.SamplingRate
            };
        }
    }
}