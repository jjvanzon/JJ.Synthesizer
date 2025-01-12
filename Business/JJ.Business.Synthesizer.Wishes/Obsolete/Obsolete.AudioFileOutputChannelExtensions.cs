using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Wishes.Obsolete.AudioFileOutputChannel_ExtensionObsoleteMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class AudioFileOutputChannel_ExtensionObsoleteMessages
    {
        public const string ObsoleteMessage = "Use AudioFileOutputChannel.AudioFileOutput instead.";
    }

    [Obsolete(ObsoleteMessage)]
    public static class Obsolete_AudioFileExtensionWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static int SizeOfSampleDataType(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.SizeOfBitDepth();
        }

        [Obsolete(ObsoleteMessage)]
        public static int GetBits(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.Bits();
        }

        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.FrameSize();
        }

        /// <inheritdoc cref="docs._fileextension" />
        [Obsolete(ObsoleteMessage)]
        public static string GetFileExtension(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.FileExtension();
        }

        [Obsolete(ObsoleteMessage)]
        public static double GetNominalMax(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.MaxAmplitude();
        }

        /// <inheritdoc cref="docs._headerlength" />
        [Obsolete(ObsoleteMessage)]
        public static int GetHeaderLength(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.HeaderLength();
        }
    }

    /// <inheritdoc cref="docs._alternativeentrypointenumextensionwishes" />
    [Obsolete(ObsoleteMessage)]
    public static class Obsolete_AlternativeEntryPointEnumExtensionWishes
    {
        // AudioFileOutputChannel AudioFormat

        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetAudioFileFormatEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFormat(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetAudioFileFormatEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFormat(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.AudioFormat(enumValue, context);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat GetAudioFileFormatEnumEntity(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.AudioFileFormat;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFileFormatEnumEntity(this AudioFileOutputChannel entity, AudioFileFormat enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);

            entity.AudioFileOutput.AudioFileFormat = enumEntity;
        }

        // AudioFileOutputChannel SampleDataTypeEnum

        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum GetSampleDataTypeEnum(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetSampleDataTypeEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetSampleDataTypeEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetSampleDataTypeEnum(enumValue, context);
        }
    
        [Obsolete(ObsoleteMessage)]
        public static SampleDataType GetSampleDataTypeEnumEntity(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.SampleDataType;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnumEntity(this AudioFileOutputChannel entity, SampleDataType enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SampleDataType = enumEntity;
        }
    }
    
    [Obsolete(ObsoleteMessage)]
    public static class Obsolete_AudioFileOutputChannel_WavHeaderExtensionWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static WavHeaderStruct GetWavHeader(this AudioFileOutputChannel entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.ToWish(frameCount).ToWavHeader();
        }

        [Obsolete(ObsoleteMessage)]
        public static AudioInfoWish GetInfo(this AudioFileOutputChannel entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            var info = entity.AudioFileOutput.ToWish(frameCount);
            info.Channels = 1;
            return info;
        }
    }
}