using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class FrameCountExtensionWishes
    {
        // A Duration Property
        
        // Synth-Bound
        
        public static int FrameCount(this SynthWishes obj) => ConfigWishes.FrameCount(obj.AudioLength(), obj.SamplingRate(), obj.CourtesyFrames());
        public static SynthWishes FrameCount(this SynthWishes obj, int? value) => obj.AudioLength(AudioLength(value, obj.SamplingRate(), obj.CourtesyFrames()));
        
        public static int FrameCount(this FlowNode obj) => ConfigWishes.FrameCount(obj.AudioLength(), obj.SamplingRate(), obj.CourtesyFrames());
        public static FlowNode FrameCount(this FlowNode obj, int? value) => obj.AudioLength(AudioLength(value, obj.SamplingRate(), obj.CourtesyFrames()));
        
        internal static int FrameCount(this ConfigResolver obj, SynthWishes synthWishes)
            => ConfigWishes.FrameCount(obj.AudioLength(synthWishes), obj.SamplingRate(), obj.CourtesyFrames());
        
        internal static ConfigResolver FrameCount(this ConfigResolver obj, int? value, SynthWishes synthWishes)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate(), obj.CourtesyFrames()), synthWishes);
        
        // Global-Bound
        
        internal static int? FrameCount(this ConfigSection obj)
        {
            if (obj.AudioLength() == null) return null;
            if (obj.SamplingRate() == null) return null;
            if (obj.CourtesyFrames() == null) return null;
            return ConfigWishes.FrameCount(obj.AudioLength().Value, obj.SamplingRate().Value, obj.CourtesyFrames().Value);
        }
        
        // Tape-Bound
        
        public static int FrameCount(this Tape obj)
        {
            if (obj.IsBuff)
            {
                return ConfigWishes.FrameCount(obj.Bytes, obj.FilePathResolved, obj.FrameSize(), obj.HeaderLength());
            }
            else
            {
                return ConfigWishes.FrameCount(obj.AudioLength(), obj.SamplingRate(), obj.CourtesyFrames());
            }
        }
        
        public static Tape FrameCount(this Tape obj, int value) 
            => obj.AudioLength(AudioLength(value, obj.SamplingRate(), obj.CourtesyFrames()));
        
        public static int FrameCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeConfig FrameCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        public static int FrameCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeAction FrameCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        public static int FrameCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeActions FrameCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        // Buff-Bound
        
        public static int FrameCount(this Buff obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            
            int frameCount = ConfigWishes.FrameCount(obj.Bytes, obj.FilePath, obj.FrameSize(), obj.HeaderLength());
            
            if (Has(frameCount))
            {
                return frameCount;
            }
            
            if (obj.UnderlyingAudioFileOutput != null)
            {
                return obj.UnderlyingAudioFileOutput.FrameCount(courtesyFrames);
            }
            
            return 0;
        }
        
        public static int FrameCount(this AudioFileOutput obj, int courtesyFrames)
            => ConfigWishes.FrameCount(obj.AudioLength(), obj.SamplingRate(), courtesyFrames);
        
        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value, int courtesyFrames)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate(), courtesyFrames));
        
        // Independent after Taping
                
        public static int FrameCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWishes.FrameCount(obj.Bytes, obj.Location, obj.FrameSize(), obj.HeaderLength());
        }
                
        public static int FrameCount(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.FrameCount;
        }
        
        public static AudioInfoWish FrameCount(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = AssertFrameCount(value);
            return infoWish;
        }
        
        public static int FrameCount(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SampleCount;
        }
        
        public static AudioFileInfo FrameCount(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = AssertFrameCount(value);
            return info;
        }

        // Immutable
        
        public static int FrameCount(this WavHeaderStruct obj) => obj.ToWish().FrameCount();
        
        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value, int courtesyFrames)
        {
            double audioLength = AudioLength(value, obj.SamplingRate(), courtesyFrames);
            return obj.AudioLength(audioLength, courtesyFrames);
        }
    }
    
    public partial class ConfigWishes
    {
        // Conversion Formula
        
        //public static int FrameCount(int byteCount, int frameSize, int headerLength)
        //    => (AssertByteCount(byteCount) - AssertHeaderLength(headerLength)) / AssertFrameSize(frameSize);
        
        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength)
            => (ByteCount(bytes, filePath) - AssertHeaderLength(headerLength)) / AssertFrameSize(frameSize);

        public static int FrameCount(double audioLength, int samplingRate, int courtesyFrames)
            => (int)(AssertAudioLength(audioLength) * AssertSamplingRate(samplingRate)) + AssertCourtesyFrames(courtesyFrames);
    }
}