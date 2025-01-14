using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.IO.File;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class ByteCountExtensionWishes
    {
        // A Duration Attribute

        public static int ByteCount(this SynthWishes obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength(), obj.CourtesyFrames());

        public static SynthWishes ByteCount(this SynthWishes obj, int? value) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        public static int ByteCount(this FlowNode obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength(), obj.CourtesyFrames());
        
        public static FlowNode ByteCount(this FlowNode obj, int? value) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
        internal static int ByteCount(this ConfigResolver obj, SynthWishes synthWishes) 
            => ConfigWishes.ByteCount(obj.FrameCount(synthWishes), obj.FrameSize(), obj.HeaderLength(), obj.CourtesyFrames());
        
        internal static ConfigResolver ByteCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()), synthWishes);
        
        internal static int? ByteCount(this ConfigSection obj)
        {
            if (obj.FrameCount() == null) return null;
            if (obj.FrameSize() == null) return null;
            if (obj.HeaderLength() == null) return null;
            if (obj.CourtesyFrames() == null) return null;
            
            return ConfigWishes.ByteCount(obj.FrameCount().Value, obj.FrameSize().Value, obj.HeaderLength().Value, obj.CourtesyFrames().Value);
        }
        
        public static int ByteCount(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ConfigWishes.ByteCount(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength(), obj.Config.CourtesyFrames);
            }
        }

        public static Tape ByteCount(this Tape obj, int value) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), obj.CourtesyFrames()));
        
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
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames = 0) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength(), courtesyFrames);

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames = 0) 
            => obj.AudioLength(AudioLength(value, obj.FrameSize(), obj.SamplingRate(), obj.HeaderLength(), courtesyFrames));

        public static int ByteCount(this WavHeaderStruct obj) 
            => ConfigWishes.ByteCount(obj.FrameCount(), obj.FrameSize(), obj.HeaderLength());

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value, int courtesyFrames = 0)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLength(value, wish.FrameSize(), wish.SamplingRate(), Wav.HeaderLength(), courtesyFrames);
            return wish.AudioLength(audioLength).ToWavHeader();
        }
    }
    
    // Conversion Formulas
    
    public partial class ConfigWishes
    {
        public static int ByteCount(byte[] bytes, string filePath)
        {
            if (Has(bytes))
            {
                return bytes.Length;
            }

            if (Exists(filePath))
            {
                // TODO: Move to AssertFileSize?
                long fileSize = new FileInfo(filePath).Length;
                int maxSize = int.MaxValue;
                if (fileSize > maxSize) throw new Exception($"File is too large. Max size = {PrettyByteCount(maxSize)}");
                return (int)fileSize;
            }

            return 0;
        }

        public static int ByteCount(int frameCount, int frameSize, int headerLength, int courtesyFrames = 0)
        {
            AssertFrameCount(frameCount);
            AssertFrameSize(frameSize);
            AssertHeaderLength(headerLength);
            
            return frameCount * frameSize + headerLength + CourtesyBytes(courtesyFrames, frameSize);
        }
        
        public static int ByteCount(double audioLength, int samplingRate, int bits, int channels, AudioFileFormatEnum audioFormat, int courtesyFrames = 0)
        {
            int frameCount = FrameCount(audioLength, samplingRate);
            int frameSize = FrameSize(bits, channels);
            int headerLength = HeaderLength(audioFormat);
            int courtesyBytes = CourtesyBytes(courtesyFrames, bits, channels);
            return frameCount * frameSize + headerLength + courtesyBytes;
        }
    }
}
