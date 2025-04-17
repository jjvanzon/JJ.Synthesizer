using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Existence.Core.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // ByteCount: A Duration Attribute

    /// <inheritdoc cref="_configextensionwishes"/>
    public static class ByteCountExtensionWishes
    {
        // Synth-Bound

        public static int GetByteCount(this SynthWishes obj) => ConfigWishes.GetByteCount(obj);

        public static int ByteCount(this    FlowNode obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this FlowNode obj) => ConfigWishes.GetByteCount(obj);

        public static FlowNode ByteCount(this     FlowNode obj, int? value) => ConfigWishes.ByteCount(obj, value);
        public static FlowNode WithByteCount(this FlowNode obj, int? value) => ConfigWishes.WithByteCount(obj, value);
        public static FlowNode SetByteCount(this  FlowNode obj, int? value) => ConfigWishes.SetByteCount(obj, value);

        [UsedImplicitly] internal static int ByteCount(this    ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.ByteCount(obj, synthWishes);
        [UsedImplicitly] internal static int GetByteCount(this ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.GetByteCount(obj, synthWishes);

        [UsedImplicitly] internal static ConfigResolver ByteCount(this     ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.ByteCount(obj, value, synthWishes);
        [UsedImplicitly] internal static ConfigResolver WithByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.WithByteCount(obj, value, synthWishes);
        [UsedImplicitly] internal static ConfigResolver SetByteCount(this  ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.SetByteCount(obj, value, synthWishes);

        // Global-Bound

        [UsedImplicitly] internal static int? ByteCount(this ConfigSection obj) => ConfigWishes.ByteCount(obj);
        [UsedImplicitly] internal static int? GetByteCount(this ConfigSection obj) => ConfigWishes.GetByteCount(obj);

        // Tape-Bound

        public static int ByteCount(this Tape obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this Tape obj) => ConfigWishes.GetByteCount(obj);

        public static Tape ByteCount(this     Tape obj, int value) => ConfigWishes.ByteCount(obj, value);
        public static Tape WithByteCount(this Tape obj, int value) => ConfigWishes.WithByteCount(obj, value);
        public static Tape SetByteCount(this  Tape obj, int value) => ConfigWishes.SetByteCount(obj, value);

        public static int ByteCount(this TapeConfig obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this TapeConfig obj) => ConfigWishes.GetByteCount(obj);

        public static TapeConfig ByteCount(this     TapeConfig obj, int value) => ConfigWishes.ByteCount(obj, value);
        public static TapeConfig WithByteCount(this TapeConfig obj, int value)  => ConfigWishes.WithByteCount(obj, value);
        public static TapeConfig SetByteCount(this TapeConfig obj, int value) => ConfigWishes.SetByteCount(obj, value);

        public static int ByteCount(this TapeActions obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this TapeActions obj) => ConfigWishes.GetByteCount(obj);

        public static TapeActions ByteCount(this     TapeActions obj, int value)  => ConfigWishes.ByteCount(obj, value);
        public static TapeActions WithByteCount(this TapeActions obj, int value)  => ConfigWishes.WithByteCount(obj, value);
        public static TapeActions SetByteCount(this TapeActions obj, int value) => ConfigWishes.SetByteCount(obj, value);

        public static int ByteCount(this TapeAction obj)  => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this TapeAction obj) => ConfigWishes.GetByteCount(obj);

        public static TapeAction ByteCount(this     TapeAction obj, int value)  => ConfigWishes.ByteCount(obj, value);
        public static TapeAction WithByteCount(this TapeAction obj, int value)  => ConfigWishes.WithByteCount(obj, value);
        public static TapeAction SetByteCount(this TapeAction obj, int value) => ConfigWishes.SetByteCount(obj, value);

        // Buff-Bound

        public static int ByteCount(this    Buff obj)  => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this Buff obj) => ConfigWishes.GetByteCount(obj);

        public static Buff ByteCount(this     Buff obj, int value)  => ConfigWishes.ByteCount(obj, value);
        public static Buff WithByteCount(this Buff obj, int value)  => ConfigWishes.WithByteCount(obj, value);
        public static Buff SetByteCount(this Buff obj, int value) => ConfigWishes.SetByteCount(obj, value);

        public static int ByteCount(this    AudioFileOutput obj)  => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this AudioFileOutput obj) => ConfigWishes.GetByteCount(obj);

        public static int BytesNeeded(this    AudioFileOutput obj)  => ConfigWishes.BytesNeeded(obj);
        public static int GetBytesNeeded(this AudioFileOutput obj)  => ConfigWishes.GetBytesNeeded(obj);

        public static AudioFileOutput ByteCount(this     AudioFileOutput obj, int value)  => ConfigWishes.ByteCount(obj, value);
        public static AudioFileOutput WithByteCount(this AudioFileOutput obj, int value)  => ConfigWishes.WithByteCount(obj, value);
        public static AudioFileOutput SetByteCount(this  AudioFileOutput obj, int value)  => ConfigWishes.SetByteCount(obj, value);

        // Independent after Taping

        public static int ByteCount(this Sample obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this Sample obj) => ConfigWishes.GetByteCount(obj);

        // Immutable

        public static int ByteCount(this    WavHeaderStruct obj) => ConfigWishes.ByteCount(obj);
        public static int GetByteCount(this WavHeaderStruct obj) => ConfigWishes.GetByteCount(obj);

        public static WavHeaderStruct ByteCount(this     WavHeaderStruct wavHeader, int value)  => ConfigWishes.ByteCount(wavHeader, value);
        public static WavHeaderStruct WithByteCount(this WavHeaderStruct wavHeader, int value)  => ConfigWishes.WithByteCount(wavHeader, value);
        public static WavHeaderStruct SetByteCount(this WavHeaderStruct wavHeader, int value) => ConfigWishes.SetByteCount(wavHeader, value);

        [Obsolete(ObsoleteMessage)] public static int ByteCount(this SampleDataTypeEnum obj)    => ConfigWishes.ByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(this SampleDataTypeEnum obj)  => ConfigWishes.ToByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(this SampleDataTypeEnum obj)  => ConfigWishes.AsByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(this SampleDataTypeEnum obj) => ConfigWishes.GetByteCount(obj);

        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum ByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount)  => ConfigWishes.ByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount) => ConfigWishes.WithByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount) => ConfigWishes.SetByteCount(oldEnumValue, newByteCount);

        [Obsolete(ObsoleteMessage)] public static int ByteCount(this SampleDataType obj)  => ConfigWishes.ByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(this SampleDataType obj)  => ConfigWishes.ToByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(this SampleDataType obj)  => ConfigWishes.AsByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(this SampleDataType obj) => ConfigWishes.GetByteCount(obj);

        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType ByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context) => ConfigWishes.ByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType WithByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context)  => ConfigWishes.WithByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType SetByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context) => ConfigWishes.SetByteCount(oldSampleDataType, newByteCount, context);

        public static int ByteCount(this    Type type)  => ConfigWishes.ByteCount(type);
        public static int GetByteCount(this Type type)  => ConfigWishes.GetByteCount(type);
        public static int AsByteCount(this  Type type)  => ConfigWishes.AsByteCount(type);
        public static int ToByteCount(this  Type type)  => ConfigWishes.ToByteCount(type);
        public static int TypeToByteCount(this Type type) => ConfigWishes.TypeToByteCount(type);

        /// <inheritdoc cref="_quasisetter" />
        public static Type ByteCount(this Type oldType, int newByteCount) => ConfigWishes.ByteCount(oldType, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static Type WithByteCount(this Type oldType, int newByteCount) => ConfigWishes.WithByteCount(oldType, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static Type SetByteCount(this Type oldType, int newByteCount) => ConfigWishes.SetByteCount(oldType, newByteCount);

        public static int ByteCount(this int bits) => ConfigWishes.ByteCount(bits);
        public static int GetByteCount(this int bits) => ConfigWishes.GetByteCount(bits);
        public static int AsByteCount(this int bits)  => ConfigWishes.AsByteCount(bits);
        public static int ToByteCount(this int bits)  => ConfigWishes.ToByteCount(bits);
        public static int BitsToByteCount(this int bits) => ConfigWishes.BitsToByteCount(bits);

        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCount(this int oldBits, int newByteCount) => ConfigWishes.ByteCount(oldBits, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int SetByteCount(this int oldBits, int newByteCount) => ConfigWishes.SetByteCount(oldBits, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int WithByteCount(this int oldBits, int newByteCount)  => ConfigWishes.WithByteCount(oldBits, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCountToBits(this int oldBits, int newByteCount) => ConfigWishes.ByteCountToBits(oldBits, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCountToBits(this int byteCount) => ConfigWishes.ByteCountToBits(byteCount);

        // Conversion Formula

        // From Buff

        public static int ByteCountFromBuff(this (byte[] bytes, string filePath) buffTuple) 
            => ConfigWishes.ByteCountFromBuff(buffTuple.bytes, buffTuple.filePath);
        
        public static int ByteCount(this (byte[] bytes, string filePath) buffTuple)
            => ConfigWishes.ByteCount(buffTuple.bytes, buffTuple.filePath);
        
        // From AudioLength
            
        /// <inheritdoc cref="_bytecountfromprimaries" />
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength);
                
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int frameSize, int headerLength)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength);

        /// <inheritdoc cref="_bytecountfromprimaries" />
        public static int ByteCount(this double audioLength, int samplingRate, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength);

        public static int ByteCount(this double audioLength, int samplingRate, int frameSize, int headerLength)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength);

        // From FrameCount

        public static int ByteCountFromFrameCount(this int frameCount, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, bits, channels, headerLength);

        public static int ByteCountFromFrameCount(this int frameCount, int frameSize, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, frameSize, headerLength);

        public static int ByteCount(this int frameCount, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, bits, channels, headerLength);

        public static int ByteCount(this int frameCount, int frameSize, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, frameSize, headerLength);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static int ByteCount(SynthWishes obj) => GetByteCount(obj);
        public static int GetByteCount(SynthWishes obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static SynthWishes ByteCount(SynthWishes obj, int? value) => SetByteCount(obj, value);
        public static SynthWishes WithByteCount(SynthWishes obj, int? value) => SetByteCount(obj, value);
        public static SynthWishes SetByteCount(SynthWishes obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength()));
        }
        
        public static int ByteCount(FlowNode obj) => GetByteCount(obj);
        public static int GetByteCount(FlowNode obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static FlowNode ByteCount(FlowNode obj, int? value) => SetByteCount(obj, value);
        public static FlowNode WithByteCount(FlowNode obj, int? value) => SetByteCount(obj, value);
        public static FlowNode SetByteCount(FlowNode obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength()));
        }
        
        internal static int ByteCount(ConfigResolver obj, SynthWishes synthWishes) => GetByteCount(obj, synthWishes);
        internal static int GetByteCount(ConfigResolver obj, SynthWishes synthWishes)
        {
            return ByteCountFromFrameCount(obj.FrameCount(synthWishes), obj.FrameSize(), obj.HeaderLength());
        }
        
        internal static ConfigResolver WithByteCount(ConfigResolver obj, int? value, SynthWishes synthWishes) => SetByteCount(obj, value, synthWishes);
        internal static ConfigResolver ByteCount(ConfigResolver obj, int? value, SynthWishes synthWishes) => SetByteCount(obj, value, synthWishes);
        internal static ConfigResolver SetByteCount(ConfigResolver obj, int? value, SynthWishes synthWishes)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength()), synthWishes);
        }
        
        // Global-Bound
        
        internal static int? ByteCount(ConfigSection obj) => GetByteCount(obj);
        internal static int? GetByteCount(ConfigSection obj)
        {
            if (obj.FrameCount() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ByteCountFromFrameCount(obj.FrameCount().Value, obj.FrameSize().Value, CoalesceHeaderLength(obj.HeaderLength()));
        }
        
        // Tape-Bound
        
        public static int ByteCount(Tape obj) => GetByteCount(obj);
        public static int GetByteCount(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
            }
        }

        public static Tape ByteCount(Tape obj, int value) => SetByteCount(obj, value);
        public static Tape WithByteCount(Tape obj, int value) => SetByteCount(obj, value);
        public static Tape SetByteCount(Tape obj, int value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength()));
        }
        
        public static int ByteCount(TapeConfig obj) => GetByteCount(obj);
        public static int GetByteCount(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeConfig ByteCount(TapeConfig obj, int value) => SetByteCount(obj, value);
        public static TapeConfig WithByteCount(TapeConfig obj, int value) => SetByteCount(obj, value);
        public static TapeConfig SetByteCount(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }
        
        public static int ByteCount(TapeActions obj) => GetByteCount(obj);
        public static int GetByteCount(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeActions ByteCount(TapeActions obj, int value) => SetByteCount(obj, value);
        public static TapeActions WithByteCount(TapeActions obj, int value) => SetByteCount(obj, value);
        public static TapeActions SetByteCount(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        public static int ByteCount(TapeAction obj) => GetByteCount(obj);
        public static int GetByteCount(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeAction ByteCount(TapeAction obj, int value) => SetByteCount(obj, value);
        public static TapeAction WithByteCount(TapeAction obj, int value) => SetByteCount(obj, value);
        public static TapeAction SetByteCount(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        // Buff-Bound
        
        public static int ByteCount(   Buff obj) => GetByteCount(obj);
        public static int GetByteCount(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return obj.UnderlyingAudioFileOutput.BytesNeeded();
            }

            return 0;
        }

        public static Buff ByteCount(Buff obj, int value) => SetByteCount(obj, value);
        public static Buff WithByteCount(Buff obj, int value) => SetByteCount(obj, value);
        public static Buff SetByteCount(Buff obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            // Buff is too Buff to change his ByteCount,
            // but he can still send the message to his buddy "Out", who does the books.
            obj.UnderlyingAudioFileOutput.SetByteCount(value);
            return obj;
        }

        public static int ByteCount(AudioFileOutput obj) => GetByteCount(obj);
        public static int GetByteCount(AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return Coalesce(AssertFileSize(obj.FilePath), GetBytesNeeded(obj));
        }

        public static int BytesNeeded(AudioFileOutput obj) => GetBytesNeeded(obj);
        public static int GetBytesNeeded(AudioFileOutput obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static AudioFileOutput ByteCount(AudioFileOutput obj, int value) => SetByteCount(obj, value);
        public static AudioFileOutput WithByteCount(AudioFileOutput obj, int value) => SetByteCount(obj, value);
        public static AudioFileOutput SetByteCount(AudioFileOutput obj, int value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength()));
        }
        
        // Independent after Taping
        
        public static int ByteCount(Sample obj) => GetByteCount(obj);
        public static int GetByteCount(Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.Location);
        }

        // Immutable
        
        public static int ByteCount(WavHeaderStruct obj) => GetByteCount(obj);
        public static int GetByteCount(WavHeaderStruct obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static WavHeaderStruct ByteCount(WavHeaderStruct wavHeader, int value) => SetByteCount(wavHeader, value);
        public static WavHeaderStruct WithByteCount(WavHeaderStruct wavHeader, int value) => SetByteCount(wavHeader, value);
        public static WavHeaderStruct SetByteCount(WavHeaderStruct wavHeader, int byteCount)
        {
            if (!Has(wavHeader)) throw new Exception("No WAV header.");
            var info = wavHeader.ToInfo();
            double audioLength = byteCount.AudioLength(info.FrameSize(), info.SamplingRate(), Wav.HeaderLength());
            return info.AudioLength(audioLength).ToWavHeader();
        }
        
        
        [Obsolete(ObsoleteMessage)] public static int ByteCount(SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(SampleDataTypeEnum obj)
        {
            return obj.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum ByteCount(SampleDataTypeEnum oldEnumValue, int newByteCount) => SetByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithByteCount(SampleDataTypeEnum oldEnumValue, int newByteCount) => SetByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetByteCount(SampleDataTypeEnum oldEnumValue, int newByteCount)
        {
            return oldEnumValue.SizeOfBitDepth(newByteCount);
        }
        
        [Obsolete(ObsoleteMessage)] public static int ByteCount(SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(SampleDataType obj)
        {
            return obj.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType ByteCount(SampleDataType oldSampleDataType, int newByteCount, IContext context) => SetByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType WithByteCount(SampleDataType oldSampleDataType, int newByteCount, IContext context) => SetByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="_quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType SetByteCount(SampleDataType oldSampleDataType, int newByteCount, IContext context)
        {
            return oldSampleDataType.SizeOfBitDepth(newByteCount, context);
        }
        
        public static int ByteCount(Type type) => TypeToByteCount(type);
        public static int GetByteCount(Type type) => TypeToByteCount(type);
        public static int AsByteCount(Type type) => TypeToByteCount(type);
        public static int ToByteCount(Type type) => TypeToByteCount(type);
        public static int TypeToByteCount(Type type)
        {
            return type.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="_quasisetter" />
        public static Type ByteCount(Type oldType, int newByteCount) => SetByteCount(oldType, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static Type WithByteCount(Type oldType, int newByteCount) => SetByteCount(oldType, newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static Type SetByteCount(Type oldType, int newByteCount)
        {
            return oldType.SizeOfBitDepth(newByteCount);
        }
        
        public static int ByteCount(int bits)=> BitsToByteCount(bits);
        public static int GetByteCount(int bits) => BitsToByteCount(bits);
        public static int AsByteCount(int bits) => BitsToByteCount(bits);
        public static int ToByteCount(int bits) => BitsToByteCount(bits);
        public static int BitsToByteCount(int bits)
        {
            return BitsToSizeOfBitDepth(bits);
        }
        
        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCount(int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int SetByteCount(int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int WithByteCount(int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCountToBits(int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="_quasisetter" />
        public static int ByteCountToBits(int byteCount)
        {
            return SizeOfBitDepthToBits(byteCount);
        }

        // Conversion Formula
        // From Buff
        
        public static int ByteCountFromBuff(byte[] bytes, string filePath) 
            => Has(bytes) ? bytes.Length : AssertFileSize(filePath);

        public static int ByteCount(byte[] bytes, string filePath) 
            => ByteCountFromBuff(bytes, filePath);
        
        // From AudioLength
            
        /// <inheritdoc cref="_bytecountfromprimaries" />
        public static int ByteCountFromAudioLength(double audioLength, int samplingRate, int bits, int channels, int headerLength)
        {
            AssertAudioLength(audioLength);
            AssertSamplingRate(samplingRate);
            AssertBits(bits);   
            AssertChannels(channels);
            AssertHeaderLength(headerLength);
            return (int)(audioLength * samplingRate) * bits / 8 * channels + headerLength;
        }
                
        public static int ByteCountFromAudioLength(double audioLength, int samplingRate, int frameSize, int headerLength)
        {
            AssertAudioLength(audioLength);
            AssertSamplingRate(samplingRate);
            AssertFrameSize(frameSize);
            AssertHeaderLength(headerLength);
            return (int)(audioLength * samplingRate) * frameSize + headerLength;
        }

        /// <inheritdoc cref="_bytecountfromprimaries" />
        public static int ByteCount(double audioLength, int samplingRate, int bits, int channels, int headerLength)
            => ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength);

        public static int ByteCount(double audioLength, int samplingRate, int frameSize, int headerLength)
            => ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength);

        // From FrameCount

        public static int ByteCountFromFrameCount(int frameCount, int bits, int channels, int headerLength)
        {
            AssertFrameCount(frameCount);
            AssertBits(bits);
            AssertChannels(channels);
            AssertHeaderLength(headerLength);
            return frameCount * bits / 8 * channels + headerLength;
        }

        public static int ByteCountFromFrameCount(int frameCount, int frameSize, int headerLength)
        {
            AssertFrameCount(frameCount);
            AssertFrameSize(frameSize);
            AssertHeaderLength(headerLength);
            return frameCount * frameSize + headerLength;
        }

        public static int ByteCount(int frameCount, int bits, int channels, int headerLength)
            => ByteCountFromFrameCount(frameCount, bits, channels, headerLength);

        public static int ByteCount(int frameCount, int frameSize, int headerLength)
            => ByteCountFromFrameCount(frameCount, frameSize, headerLength);
    }
}
