using System;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Primary Audio Attribute
        
        public static int CourtesyFrames(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static ConfigWishes CourtesyFrames(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        internal static int CourtesyFrames(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames ?? ConfigWishes.DefaultCourtesyFrames;
        }
        
        public static int CourtesyFrames(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.CourtesyFrames;
        }
        
        public static Tape CourtesyFrames(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.CourtesyFrames = value;
            return obj;
        }
        
        public static int CourtesyFrames(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }
        
        public static TapeConfig CourtesyFrames(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.CourtesyFrames = value;
            return obj;
        }
        
        public static int CourtesyFrames(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeActions CourtesyFrames(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
        
        public static int CourtesyFrames(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeAction CourtesyFrames(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
    }
}