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
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static SynthWishes ByteCount(this SynthWishes obj, int? value) 
            => obj.AudioLength(value.AudioLength(obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        public static int ByteCount(this FlowNode obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
        
        public static FlowNode ByteCount(this FlowNode obj, int? value) 
            => obj.AudioLength(value.AudioLength(obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        internal static int ByteCount(this ConfigResolver obj, SynthWishes synthWishes) 
            => ConfigWishes.ByteCount(obj.FrameCount(synthWishes), obj.FrameSize(), obj.HeaderLength());
        
        internal static ConfigResolver ByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) 
            => obj.AudioLength(value.AudioLength(obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()), synthWishes);
        
        // Global-Bound
        
        internal static int? ByteCount(this ConfigSection obj)
        {
            if (obj.FrameCount() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ConfigWishes.ByteCount(obj.FrameCount().Value, obj.FrameSize().Value, CoalesceHeaderLength(obj.HeaderLength()));
        }
        
        // Tape-Bound
        
        public static int ByteCount(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ConfigWishes.ByteCount(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());
            }
        }

        public static Tape ByteCount(this Tape obj, int value) 
            => obj.AudioLength(value.AudioLength(obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
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
        
        public static int ByteCount(this Buff obj, int courtesyFrames = 0)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ConfigWishes.ByteCount(obj.Bytes, obj.FilePath);

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
            return ConfigWishes.ByteCount(obj.Bytes, obj.Location);
        }

        public static int ByteCount(this AudioFileOutput obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(courtesyFrames: 0), obj.FrameSize(), obj.HeaderLength());

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames = 0) 
            => ConfigWishes.ByteCount(obj.FrameCount(courtesyFrames), obj.FrameSize(), obj.HeaderLength());

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames = 0) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), courtesyFrames));

        // Immutable
        
        public static int ByteCount(this WavHeaderStruct obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value, int courtesyFrames = 0)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLength(value, wish.FrameSize(), wish.SamplingRate(), Wav.HeaderLength(), courtesyFrames);
            return wish.AudioLength(audioLength, courtesyFrames).ToWavHeader();
        }
    }
    
    // Conversion Formula
    
    public partial class ConfigWishes
    {
        public static int ByteCount(byte[] bytes, string filePath) => Has(bytes) ? bytes.Length : AssertFileSize(filePath);
        
        public static int ByteCount(int frameCount, int frameSize, int headerLength)
        {
            AssertFrameCount(frameCount);
            AssertFrameSize(frameSize);
            AssertHeaderLength(headerLength);
            
            return frameCount * frameSize + headerLength;
        }
        

    }
}
