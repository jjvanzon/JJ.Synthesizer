using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Duration Attribute
        
        public static int FrameCount(int byteCount, int frameSize, int headerLength)
            => (byteCount - headerLength) / frameSize;
        
        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength)
            => (ByteCount(bytes, filePath) - headerLength) / frameSize;
        
        public static int FrameCount(double audioLength, int samplingRate)
            => (int)(audioLength * samplingRate);
        
        public static int FrameCount(this SynthWishes obj)
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static SynthWishes FrameCount(this SynthWishes obj, int value)
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this FlowNode obj)
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static FlowNode FrameCount(this FlowNode obj, int value)
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this ConfigWishes obj, SynthWishes synthWishes)
            => FrameCount(AudioLength(obj, synthWishes), SamplingRate(obj));
        
        public static ConfigWishes FrameCount(this ConfigWishes obj, int value, SynthWishes synthWishes)
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)), synthWishes);
        
        internal static int FrameCount(this ConfigSection obj)
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static int FrameCount(this Tape obj)
        {
            if (obj.IsBuff)
            {
                return FrameCount(obj.Bytes, obj.FilePathResolved, FrameSize(obj), HeaderLength(obj));
            }
            else
            {
                return FrameCount(AudioLength(obj), SamplingRate(obj));
            }
        }
        
        public static Tape FrameCount(this Tape obj, int value)
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }
        
        public static TapeConfig FrameCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }
        
        public static int FrameCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }
        
        public static TapeAction FrameCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }
        
        public static int FrameCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }
        
        public static TapeActions FrameCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }
        
        public static int FrameCount(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            int frameCount = FrameCount(obj.Bytes, obj.FilePath, FrameSize(obj), HeaderLength(obj));
            
            if (FilledInWishes.Has(frameCount))
            {
                return frameCount;
            }
            
            if (obj.UnderlyingAudioFileOutput != null)
            {
                return FrameCount(obj.UnderlyingAudioFileOutput);
            }
            
            return 0;
        }
        
        public static int FrameCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Bytes, obj.Location, FrameSize(obj), HeaderLength(obj));
        }
        
        public static int FrameCount(this AudioFileOutput obj)
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value)
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this WavHeaderStruct obj)
            => obj.ToWish().FrameCount();
        
        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value)
        {
            AudioInfoWish infoWish = obj.ToWish();
            AudioLength(infoWish, AudioLength(value, infoWish.SamplingRate));
            return infoWish.ToWavHeader();
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
}