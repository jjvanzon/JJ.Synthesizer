using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // A Primary Audio Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class CourtesyFrameExtensionWishes
    {
        // Synth-Bound
        
        public static int CourtesyFrames(this SynthWishes obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(this SynthWishes obj, int? value) => SetCourtesyFrames(obj, value);
        public static SynthWishes WithCourtesyFrames(this SynthWishes obj, int? value) => SetCourtesyFrames(obj, value);
        public static SynthWishes SetCourtesyFrames(this SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this FlowNode obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(this FlowNode obj, int? value) => SetCourtesyFrames(obj, value);
        public static FlowNode WithCourtesyFrames(this FlowNode obj, int? value) => SetCourtesyFrames(obj, value);
        public static FlowNode SetCourtesyFrames(this FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        internal static int CourtesyFrames(this ConfigResolver obj) => GetCourtesyFrames(obj);
        internal static int GetCourtesyFrames(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        internal static ConfigResolver CourtesyFrames(this ConfigResolver obj, int? value) => SetCourtesyFrames(obj, value);
        internal static ConfigResolver WithCourtesyFrames(this ConfigResolver obj, int? value) => SetCourtesyFrames(obj, value);
        internal static ConfigResolver SetCourtesyFrames(this ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        // Global-Bound
        
        internal static int? CourtesyFrames(this ConfigSection obj) => GetCourtesyFrames(obj);
        internal static int? GetCourtesyFrames(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }
        
        // Tape-Bound
       
        public static int CourtesyFrames(this Tape obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.CourtesyFrames;
        }
       
        public static Tape CourtesyFrames(this Tape obj, int value) => SetCourtesyFrames(obj, value);
        public static Tape WithCourtesyFrames(this Tape obj, int value) => SetCourtesyFrames(obj, value);
        public static Tape SetCourtesyFrames(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.CourtesyFrames = value;
            return obj;
        }
       
        public static int CourtesyFrames(this TapeConfig obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }
        
        public static TapeConfig CourtesyFrames(this TapeConfig obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeConfig WithCourtesyFrames(this TapeConfig obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeConfig SetCourtesyFrames(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.CourtesyFrames = value;
            return obj;
        }
        
        public static int CourtesyFrames(this TapeActions obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeActions CourtesyFrames(this TapeActions obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeActions WithCourtesyFrames(this TapeActions obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeActions SetCourtesyFrames(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
        
        public static int CourtesyFrames(this TapeAction obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeAction CourtesyFrames(this TapeAction obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeAction WithCourtesyFrames(this TapeAction obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeAction SetCourtesyFrames(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
    }
}