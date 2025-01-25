using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ByteCountExtensionWishes
    {
        // A Duration Attribute

        // Synth-Bound
        
        public static int ByteCount(this SynthWishes obj) 
            => ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static SynthWishes ByteCount(this SynthWishes obj, int? value) 
            => obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        public static int ByteCount(this FlowNode obj) 
            => ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        
        public static FlowNode ByteCount(this FlowNode obj, int? value) 
            => obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        internal static int ByteCount(this ConfigResolver obj, SynthWishes synthWishes) 
            => ByteCountFromFrameCount(obj.FrameCount(synthWishes), obj.FrameSize(), obj.HeaderLength());
        
        internal static ConfigResolver ByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) 
            => obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()), synthWishes);
        
        // Global-Bound
        
        internal static int? ByteCount(this ConfigSection obj)
        {
            if (obj.FrameCount() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ByteCountFromFrameCount(obj.FrameCount().Value, obj.FrameSize().Value, CoalesceHeaderLength(obj.HeaderLength()));
        }
        
        // Tape-Bound
        
        public static int ByteCount(this Tape obj)
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

        public static Tape ByteCount(this Tape obj, int value) 
            => obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        public static int ByteCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeConfig ByteCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }
        
        public static int ByteCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeActions ByteCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        public static int ByteCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.ByteCount();
        }

        public static TapeAction ByteCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.ByteCount(value);
            return obj;
        }

        // Buff-Bound
        
        public static int ByteCount(this Buff obj, int courtesyFrames)
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

        public static int ByteCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWishes.ByteCountFromBuff(obj.Bytes, obj.Location);
        }

        public static int ByteCount(this AudioFileOutput obj, int courtesyFrames) 
            => ByteCountFromFrameCount(obj.FrameCount(courtesyFrames), obj.FrameSize(), obj.HeaderLength());

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames) 
            => ByteCountFromFrameCount(obj.FrameCount(courtesyFrames), obj.FrameSize(), obj.HeaderLength());

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames) 
            => obj.AudioLength(AudioLengthFromByteCount(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), courtesyFrames));

        // Immutable
        
        public static int ByteCount(this WavHeaderStruct obj) 
            => ByteCountFromFrameCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLengthFromByteCount(value, wish.FrameSize(), wish.SamplingRate(), Wav.HeaderLength(), DefaultCourtesyFrames);
            return wish.AudioLength(audioLength, DefaultCourtesyFrames).ToWavHeader();
        }
        
        // Conversion Formula
        
        // From Buff
        
        public static int ByteCountFromBuff(this (byte[] bytes, string filePath) tuple) 
            => ConfigWishes.ByteCountFromBuff(tuple.bytes, tuple.filePath);
        
        public static int ByteCount(this (byte[] bytes, string filePath) tuple)
            => ConfigWishes.ByteCount(tuple.bytes, tuple.filePath);
        
        // From AudioLength
            
        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength, courtesyFrames);
                
        public static int ByteCountFromAudioLength(this double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength, courtesyFrames);

        /// <inheritdoc cref="docs._bytecountfromprimaries" />
        public static int ByteCount(this double audioLength, int samplingRate, int bits, int channels, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCount(audioLength, samplingRate, bits, channels, headerLength, courtesyFrames);

        public static int ByteCount(this double audioLength, int samplingRate, int frameSize, int headerLength, int courtesyFrames)
            => ConfigWishes.ByteCount(audioLength, samplingRate, frameSize, headerLength, courtesyFrames);

        // From FrameCount

        public static int ByteCountFromFrameCount(this int frameCount, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, bits, channels, headerLength);

        public static int ByteCountFromFrameCount(this int frameCount, int frameSize, int headerLength)
            => ConfigWishes.ByteCountFromFrameCount(frameCount, frameSize, headerLength);

        public static int ByteCount(this int frameCount, int bits, int channels, int headerLength)
            => ConfigWishes.ByteCount(frameCount, bits, channels, headerLength);

        public static int ByteCount(this int frameCount, int frameSize, int headerLength)
            => ConfigWishes.ByteCount(frameCount, frameSize, headerLength);
    }
    
    // Conversion Formula
    
    public partial class ConfigWishes
    {
        // From Buff
        
        public static int ByteCountFromBuff(byte[] bytes, string filePath) => Has(bytes) ? bytes.Length : AssertFileSize(filePath);

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
