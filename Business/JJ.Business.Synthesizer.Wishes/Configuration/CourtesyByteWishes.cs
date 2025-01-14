using System;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class CourtesyByteExtensionWishes
    {
        // Derived from CourtesyFrames
        
        public static int CourtesyBytes(this SynthWishes obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static SynthWishes CourtesyBytes(this SynthWishes obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        public static int CourtesyBytes(this FlowNode obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static FlowNode CourtesyBytes(this FlowNode obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        internal static int CourtesyBytes(this ConfigResolver obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        internal static ConfigResolver CourtesyBytes(this ConfigResolver obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        internal static int? CourtesyBytes(this ConfigSection obj)
        {
            if (obj.CourtesyFrames() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ConfigWishes.CourtesyBytes(obj.CourtesyFrames().Value, obj.FrameSize().Value);
        }
        
        public static int CourtesyBytes(this Tape obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static Tape CourtesyBytes(this Tape obj, int value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        public static int CourtesyBytes(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyBytes();
        }
        
        public static TapeConfig CourtesyBytes(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyBytes(value);
            return obj;
        }
        
        public static int CourtesyBytes(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyBytes();
        }
        
        public static TapeActions CourtesyBytes(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyBytes(value);
            return obj;
        }
        
        public static int CourtesyBytes(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyBytes();
        }
        
        public static TapeAction CourtesyBytes(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyBytes(value);
            return obj;
        }

    }

    // Conversion formulas        
    
    public partial class ConfigWishes
    {
        public static int CourtesyBytes(int courtesyFrames, int frameSize) 
            => AssertCourtesyFrames(courtesyFrames) * AssertFrameSize(frameSize);
        
        public static int CourtesyBytes(int courtesyFrames, int bits, int channels) 
            => AssertCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);
        
        public static int CourtesyBytes(int? courtesyFrames, int? bits, int? channels) 
            => CoalesceCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);
        
        public static int? CourtesyFrames(int? courtesyBytes, int frameSize)
        {
            if (courtesyBytes == null) return null;
            return CourtesyFrames(courtesyBytes.Value, frameSize);
        }
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize)
        {
            courtesyBytes = AssertCourtesyBytes(courtesyBytes);
            frameSize = AssertFrameSize(frameSize);
            
            // TODO: Composite assertion method?
            if (courtesyBytes % frameSize != 0) 
            {
                throw new Exception($"{nameof(courtesyBytes)} not a multiple of {nameof(frameSize)}: " +
                                    $"{new{courtesyBytes, frameSize}}");
            }
            
            return courtesyBytes / frameSize;
        }

    }
}