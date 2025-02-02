using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ByteCountExtensionWishes
    {
        // A Duration Attribute

        // Synth-Bound
        
        public static int ByteCount(this SynthWishes obj) => GetByteCount(obj);
        public static int GetByteCount(this SynthWishes obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static SynthWishes ByteCount(this SynthWishes obj, int? value) => SetByteCount(obj, value);
        public static SynthWishes WithByteCount(this SynthWishes obj, int? value) => SetByteCount(obj, value);
        public static SynthWishes SetByteCount(this SynthWishes obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        }
        
        public static int ByteCount(this FlowNode obj) => GetByteCount(obj);
        public static int GetByteCount(this FlowNode obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static FlowNode ByteCount(this FlowNode obj, int? value) => SetByteCount(obj, value);
        public static FlowNode WithByteCount(this FlowNode obj, int? value) => SetByteCount(obj, value);
        public static FlowNode SetByteCount(this FlowNode obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        }
        
        internal static int ByteCount(this ConfigResolver obj, SynthWishes synthWishes) => GetByteCount(obj, synthWishes);
        internal static int GetByteCount(this ConfigResolver obj, SynthWishes synthWishes)
        {
            return ByteCountFromFrameCount(obj.FrameCount(synthWishes), obj.FrameSize(), obj.HeaderLength());
        }
        
        internal static ConfigResolver WithByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => SetByteCount(obj, value, synthWishes);
        internal static ConfigResolver ByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => SetByteCount(obj, value, synthWishes);
        internal static ConfigResolver SetByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()), synthWishes);
        }
        
        // Global-Bound
        
        internal static int? ByteCount(this ConfigSection obj) => GetByteCount(obj);
        internal static int? GetByteCount(this ConfigSection obj)
        {
            if (obj.FrameCount() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ByteCountFromFrameCount(obj.FrameCount().Value, obj.FrameSize().Value, CoalesceHeaderLength(obj.HeaderLength()));
        }
        
        // Tape-Bound
        
        public static int ByteCount(this Tape obj) => GetByteCount(obj);
        public static int GetByteCount(this Tape obj)
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

        public static Tape ByteCount(this Tape obj, int value) => SetByteCount(obj, value);
        public static Tape WithByteCount(this Tape obj, int value) => SetByteCount(obj, value);
        public static Tape SetByteCount(this Tape obj, int value)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        }
        
        public static int ByteCount(this TapeConfig obj) => GetByteCount(obj);
        public static int GetByteCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeConfig ByteCount(this TapeConfig obj, int value) => SetByteCount(obj, value);
        public static TapeConfig WithByteCount(this TapeConfig obj, int value) => SetByteCount(obj, value);
        public static TapeConfig SetByteCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }
        
        public static int ByteCount(this TapeActions obj) => GetByteCount(obj);
        public static int GetByteCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeActions ByteCount(this TapeActions obj, int value) => SetByteCount(obj, value);
        public static TapeActions WithByteCount(this TapeActions obj, int value) => SetByteCount(obj, value);
        public static TapeActions SetByteCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        public static int ByteCount(this TapeAction obj) => GetByteCount(obj);
        public static int GetByteCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeAction ByteCount(this TapeAction obj, int value) => SetByteCount(obj, value);
        public static TapeAction WithByteCount(this TapeAction obj, int value) => SetByteCount(obj, value);
        public static TapeAction SetByteCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        // Buff-Bound
        
        public static int ByteCount(this    Buff obj, int? courtesyFrames) => GetByteCount(obj, courtesyFrames);
        public static int ByteCount(this    Buff obj, int  courtesyFrames) => GetByteCount(obj, courtesyFrames);
        public static int GetByteCount(this Buff obj, int? courtesyFrames) => GetByteCount(obj, CoalesceCourtesyFrames(courtesyFrames));
        public static int GetByteCount(this Buff obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return obj.UnderlyingAudioFileOutput.BytesNeeded(courtesyFrames);
            }

            return 0;
        }

        public static Buff ByteCount(this Buff obj, int value, int? courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static Buff ByteCount(this Buff obj, int value, int courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static Buff WithByteCount(this Buff obj, int value, int? courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static Buff WithByteCount(this Buff obj, int value, int courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static Buff SetByteCount(this Buff obj, int value, int? courtesyFrames) => SetByteCount(obj, value, CoalesceCourtesyFrames(courtesyFrames));
        public static Buff SetByteCount(this Buff obj, int value, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            // Buff is too Buff to change his ByteCount,
            // but he can still send the message to his buddy "Out", who does the books.
            obj.UnderlyingAudioFileOutput.ByteCount(value, courtesyFrames);
            return obj;
        }

        public static int ByteCount(this AudioFileOutput obj, int? courtesyFrames) => GetByteCount(obj, courtesyFrames);
        public static int ByteCount(this AudioFileOutput obj, int courtesyFrames) => GetByteCount(obj, courtesyFrames);
        public static int GetByteCount(this AudioFileOutput obj, int? courtesyFrames) => GetByteCount(obj, CoalesceCourtesyFrames(courtesyFrames));
        public static int GetByteCount(this AudioFileOutput obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            return Coalesce(AssertFileSize(obj.FilePath), GetBytesNeeded(obj, courtesyFrames));
        }

        public static int BytesNeeded(this AudioFileOutput obj, int? courtesyFrames) => GetBytesNeeded(obj, courtesyFrames);
        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames) => GetBytesNeeded(obj, courtesyFrames);
        public static int GetBytesNeeded(this AudioFileOutput obj, int? courtesyFrames) => GetBytesNeeded(obj, CoalesceCourtesyFrames(courtesyFrames));
        public static int GetBytesNeeded(this AudioFileOutput obj, int courtesyFrames)
        {
            return ByteCountFromFrameCount(obj.FrameCount(courtesyFrames), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int? courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithByteCount(this AudioFileOutput obj, int value, int? courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithByteCount(this AudioFileOutput obj, int value, int courtesyFrames) => SetByteCount(obj, value, courtesyFrames);
        public static AudioFileOutput SetByteCount(this AudioFileOutput obj, int value, int? courtesyFrames) => SetByteCount(obj, value, CoalesceCourtesyFrames(courtesyFrames));
        public static AudioFileOutput SetByteCount(this AudioFileOutput obj, int value, int courtesyFrames)
        {
            return obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), courtesyFrames));
        }
        
        // Independent after Taping
        
        public static int ByteCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.Location);
        }

        // Immutable
        
        public static int ByteCount(this WavHeaderStruct obj) => GetByteCount(obj);
        public static int GetByteCount(this WavHeaderStruct obj)
        {
            return ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        }
        
        public static WavHeaderStruct ByteCount(this WavHeaderStruct wavHeader, int value) => SetByteCount(wavHeader, value);
        public static WavHeaderStruct WithByteCount(this WavHeaderStruct wavHeader, int value) => SetByteCount(wavHeader, value);
        public static WavHeaderStruct SetByteCount(this WavHeaderStruct wavHeader, int value)
        {
            if (!Has(wavHeader)) throw new Exception("No WAV header.");
            var wish = wavHeader.ToWish();
            double audioLength = AudioLengthFromByteCount(value, wish.FrameSize(), wish.SamplingRate(), Wav.HeaderLength(), DefaultCourtesyFrames);
            return wish.AudioLength(audioLength, DefaultCourtesyFrames).ToWavHeader();
        }
        
        
        [Obsolete(ObsoleteMessage)] public static int ByteCount(this SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(this SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(this SampleDataTypeEnum obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(this SampleDataTypeEnum obj)
        {
            return obj.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum ByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount) => SetByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum WithByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount) => SetByteCount(oldEnumValue, newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SetByteCount(this SampleDataTypeEnum oldEnumValue, int newByteCount)
        {
            return oldEnumValue.SizeOfBitDepth(newByteCount);
        }
        
        [Obsolete(ObsoleteMessage)] public static int ByteCount(this SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int ToByteCount(this SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int AsByteCount(this SampleDataType obj) => GetByteCount(obj);
        [Obsolete(ObsoleteMessage)] public static int GetByteCount(this SampleDataType obj)
        {
            return obj.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType ByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context) => SetByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType WithByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context) => SetByteCount(oldSampleDataType, newByteCount, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)] public static SampleDataType SetByteCount(this SampleDataType oldSampleDataType, int newByteCount, IContext context)
        {
            return oldSampleDataType.SizeOfBitDepth(newByteCount, context);
        }
        
        public static int ByteCount(this Type type) => TypeToByteCount(type);
        public static int GetByteCount(this Type type) => TypeToByteCount(type);
        public static int AsByteCount(this Type type) => TypeToByteCount(type);
        public static int ToByteCount(this Type type) => TypeToByteCount(type);
        public static int TypeToByteCount(this Type type)
        {
            return type.SizeOfBitDepth();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type ByteCount(this Type oldType, int newByteCount) => SetByteCount(oldType, newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type WithByteCount(this Type oldType, int newByteCount) => SetByteCount(oldType, newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static Type SetByteCount(this Type oldType, int newByteCount)
        {
            return oldType.SizeOfBitDepth(newByteCount);
        }
        
        public static int ByteCount(this int bits)=> BitsToByteCount(bits);
        public static int GetByteCount(this int bits) => BitsToByteCount(bits);
        public static int AsByteCount(this int bits) => BitsToByteCount(bits);
        public static int ToByteCount(this int bits) => BitsToByteCount(bits);
        public static int BitsToByteCount(this int bits)
        {
            return BitsToSizeOfBitDepth(bits);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static int ByteCount(this int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int SetByteCount(this int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int WithByteCount(this int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int ByteCountToBits(this int oldBits, int newByteCount) => ByteCountToBits(newByteCount);
        /// <inheritdoc cref="docs._quasisetter" />
        public static int ByteCountToBits(this int byteCount)
        {
            return SizeOfBitDepthToBits(byteCount);
        }
        
        // Conversion Formula
        
        // From Buff
        
        public static int ByteCountFromBuff(this (byte[] bytes, string filePath) buffTuple) 
            => ConfigWishes.ByteCountFromBuff(buffTuple.bytes, buffTuple.filePath);
        
        public static int ByteCount(this (byte[] bytes, string filePath) buffTuple)
            => ConfigWishes.ByteCount(buffTuple.bytes, buffTuple.filePath);
        
        // From AudioLength
            
        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength, courtesyFrames);
                
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength, courtesyFrames);

        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCount(this double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength, courtesyFrames);

        public static int ByteCount(this double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength, courtesyFrames);

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
    
    // Conversion Formula
    
    public partial class ConfigWishes
    {
        // From Buff
        
        public static int ByteCountFromBuff(byte[] bytes, string filePath) 
            => Has(bytes) ? bytes.Length : AssertFileSize(filePath);

        public static int ByteCount(byte[] bytes, string filePath) 
            => ByteCountFromBuff(bytes, filePath);
        
        // From AudioLength
            
        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCountFromAudioLength(double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
        {
            AssertAudioLength(audioLength);
            AssertSamplingRate(samplingRate);
            AssertBits(bits);   
            AssertChannels(channels);
            AssertHeaderLength(headerLength);
            AssertCourtesyFrames(courtesyFrames);
            return ((int)(audioLength * samplingRate) + courtesyFrames) * bits / 8 * channels + headerLength;
        }
                
        public static int ByteCountFromAudioLength(double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
        {
            AssertAudioLength(audioLength);
            AssertSamplingRate(samplingRate);
            AssertFrameSize(frameSize);
            AssertHeaderLength(headerLength);
            AssertCourtesyFrames(courtesyFrames);
            return ((int)(audioLength * samplingRate) + courtesyFrames) * frameSize + headerLength;
        }

        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCount(double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
            => ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength, courtesyFrames);

        public static int ByteCount(double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
            => ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength, courtesyFrames);

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
