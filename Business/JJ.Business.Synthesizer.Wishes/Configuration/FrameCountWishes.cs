using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWish;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static partial class FrameCountExtensionWishes
    {
        // A Duration Attribute
        
        public static int FrameCount(this SynthWishes obj)
            => ConfigWish.FrameCount(obj.AudioLength(), obj.SamplingRate());
        
        public static SynthWishes FrameCount(this SynthWishes obj, int? value)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate()));
        
        public static int FrameCount(this FlowNode obj)
            => ConfigWish.FrameCount(obj.AudioLength(), obj.SamplingRate());
        
        public static FlowNode FrameCount(this FlowNode obj, int? value)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate()));
        
        public static int FrameCount(this ConfigWishes obj, SynthWishes synthWishes)
            => ConfigWish.FrameCount(obj.AudioLength(synthWishes), obj.SamplingRate());
        
        public static ConfigWishes FrameCount(this ConfigWishes obj, int? value, SynthWishes synthWishes)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate()), synthWishes);
        
        internal static int? FrameCount(this ConfigSection obj)
        {
            if (obj.AudioLength() == null) return null;
            if (obj.SamplingRate() == null) return null;
            return ConfigWish.FrameCount(obj.AudioLength().Value, obj.SamplingRate().Value);
        }
        
        public static int FrameCount(this Tape obj)
        {
            if (obj.IsBuff)
            {
                return ConfigWish.FrameCount(obj.Bytes, obj.FilePathResolved, obj.FrameSize(), obj.HeaderLength());
            }
            else
            {
                return ConfigWish.FrameCount(obj.AudioLength(), obj.SamplingRate());
            }
        }
        
        public static Tape FrameCount(this Tape obj, int value)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate()));
        
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
        
        public static int FrameCount(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            int frameCount = ConfigWish.FrameCount(obj.Bytes, obj.FilePath, obj.FrameSize(), obj.HeaderLength());
            
            if (Has(frameCount))
            {
                return frameCount;
            }
            
            if (obj.UnderlyingAudioFileOutput != null)
            {
                return obj.UnderlyingAudioFileOutput.FrameCount();
            }
            
            return 0;
        }
        
        public static int FrameCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWish.FrameCount(obj.Bytes, obj.Location, obj.FrameSize(), obj.HeaderLength());
        }
        
        public static int FrameCount(this AudioFileOutput obj)
            => ConfigWish.FrameCount(obj.AudioLength(), obj.SamplingRate());
        
        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value)
            => obj.AudioLength(AudioLength(value, obj.SamplingRate()));
        
        public static int FrameCount(this WavHeaderStruct obj)
            => obj.ToWish().FrameCount();
        
        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value)
        {
            double audioLength = AudioLength(value, obj.SamplingRate());
            return obj.AudioLength(audioLength);
        }
        
        public static int FrameCount(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.FrameCount;
        }
        
        public static AudioInfoWish FrameCount(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = value;
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
            info.SampleCount = value;
            return info;
        }
    }
    
    public partial class ConfigWish
    {
        public static int FrameCount(int byteCount, int frameSize, int headerLength)
            => (byteCount - headerLength) / frameSize;
        
        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength)
            => (ByteCount(bytes, filePath) - headerLength) / frameSize;

        public static int FrameCount(double audioLength, int samplingRate)
            => (int)(audioLength * samplingRate);
    }
}