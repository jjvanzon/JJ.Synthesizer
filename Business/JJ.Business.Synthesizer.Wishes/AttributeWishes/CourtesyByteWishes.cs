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
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));
        
        public static SynthWishes CourtesyBytes(this SynthWishes obj, int value)
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this FlowNode obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));
        
        public static FlowNode CourtesyBytes(this FlowNode obj, int value)
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this ConfigWishes obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));
        
        public static ConfigWishes CourtesyBytes(this ConfigWishes obj, int value)
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        internal static int CourtesyBytes(this ConfigSection obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));
        
        public static int CourtesyBytes(this Tape obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));
        
        public static Tape CourtesyBytes(this Tape obj, int value)
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }
        
        public static TapeConfig CourtesyBytes(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }
        
        public static int CourtesyBytes(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }
        
        public static TapeActions CourtesyBytes(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }
        
        public static int CourtesyBytes(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }
        
        public static TapeAction CourtesyBytes(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }
    }
}