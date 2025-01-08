using System;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Derived Attribute
        
        public static int CourtesyBytes(int courtesyFrames, int frameSize)
        {
            if (courtesyFrames < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyFrames * frameSize;
        }
        
        public static int CourtesyBytes(this SynthWishes obj)
            => CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static SynthWishes CourtesyBytes(this SynthWishes obj, int value)
            => obj.CourtesyFrames(CourtesyFrames(value, obj.FrameSize()));
        
        public static int CourtesyBytes(this FlowNode obj)
            => CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static FlowNode CourtesyBytes(this FlowNode obj, int value)
            => obj.CourtesyFrames(CourtesyFrames(value, obj.FrameSize()));
        
        public static int CourtesyBytes(this ConfigWishes obj)
            => CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static ConfigWishes CourtesyBytes(this ConfigWishes obj, int value)
            => obj.CourtesyFrames(CourtesyFrames(value, obj.FrameSize()));
        
        internal static int CourtesyBytes(this ConfigSection obj)
            => CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static int CourtesyBytes(this Tape obj)
            => CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static Tape CourtesyBytes(this Tape obj, int value)
            => obj.CourtesyFrames(CourtesyFrames(value, obj.FrameSize()));
        
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
}