using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // CourtesyFrames: A Primary Audio Attribute

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class CourtesyFrameExtensionWishes
    {
        // Synth-Bound
        
        public static int CourtesyFrames(this SynthWishes obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this SynthWishes obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static SynthWishes CourtesyFrames(this SynthWishes obj, int? value) => ConfigWishes.CourtesyFrames(obj, value);
        public static SynthWishes WithCourtesyFrames(this SynthWishes obj, int? value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static SynthWishes SetCourtesyFrames(this SynthWishes obj, int? value) => ConfigWishes.SetCourtesyFrames(obj, value);

        public static int CourtesyFrames(this FlowNode obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this FlowNode obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static FlowNode CourtesyFrames(this FlowNode obj, int? value) => ConfigWishes.CourtesyFrames(obj, value);
        public static FlowNode WithCourtesyFrames(this FlowNode obj, int? value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static FlowNode SetCourtesyFrames(this FlowNode obj, int? value) => ConfigWishes.SetCourtesyFrames(obj, value);

        internal static int CourtesyFrames(this ConfigResolver obj) => ConfigWishes.CourtesyFrames(obj);
        internal static int GetCourtesyFrames(this ConfigResolver obj) => ConfigWishes.GetCourtesyFrames(obj);

        internal static ConfigResolver CourtesyFrames(this ConfigResolver obj, int? value) => ConfigWishes.CourtesyFrames(obj, value);
        internal static ConfigResolver WithCourtesyFrames(this ConfigResolver obj, int? value) => ConfigWishes.WithCourtesyFrames(obj, value);
        internal static ConfigResolver SetCourtesyFrames(this ConfigResolver obj, int? value) => ConfigWishes.SetCourtesyFrames(obj, value);

        // Global-Bound

        internal static int? CourtesyFrames(this ConfigSection obj) => ConfigWishes.CourtesyFrames(obj);
        internal static int? GetCourtesyFrames(this ConfigSection obj) => ConfigWishes.GetCourtesyFrames(obj);

        // Tape-Bound

        public static int CourtesyFrames(this Tape obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this Tape obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static Tape CourtesyFrames(this Tape obj, int value) => ConfigWishes.CourtesyFrames(obj, value);
        public static Tape WithCourtesyFrames(this Tape obj, int value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static Tape SetCourtesyFrames(this Tape obj, int value) => ConfigWishes.SetCourtesyFrames(obj, value);

        public static int CourtesyFrames(this TapeConfig obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeConfig obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static TapeConfig CourtesyFrames(this TapeConfig obj, int value) => ConfigWishes.CourtesyFrames(obj, value);
        public static TapeConfig WithCourtesyFrames(this TapeConfig obj, int value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static TapeConfig SetCourtesyFrames(this TapeConfig obj, int value) => ConfigWishes.SetCourtesyFrames(obj, value);

        public static int CourtesyFrames(this TapeActions obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeActions obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static TapeActions CourtesyFrames(this TapeActions obj, int value) => ConfigWishes.CourtesyFrames(obj, value);
        public static TapeActions WithCourtesyFrames(this TapeActions obj, int value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static TapeActions SetCourtesyFrames(this TapeActions obj, int value) => ConfigWishes.SetCourtesyFrames(obj, value);

        public static int CourtesyFrames(this TapeAction obj) => ConfigWishes.CourtesyFrames(obj);
        public static int GetCourtesyFrames(this TapeAction obj) => ConfigWishes.GetCourtesyFrames(obj);

        public static TapeAction CourtesyFrames(this TapeAction obj, int value) => ConfigWishes.CourtesyFrames(obj, value);
        public static TapeAction WithCourtesyFrames(this TapeAction obj, int value) => ConfigWishes.WithCourtesyFrames(obj, value);
        public static TapeAction SetCourtesyFrames(this TapeAction obj, int value) => ConfigWishes.SetCourtesyFrames(obj, value);
        
        // Conversion Formulas

        // Courtesy Bytes to Frames

        public static int? CourtesyFrames(this int? courtesyBytes, int frameSize) => ConfigWishes.CourtesyFrames(courtesyBytes, frameSize);
        public static int? GetCourtesyFrames(this int? courtesyBytes, int frameSize) => ConfigWishes.GetCourtesyFrames(courtesyBytes, frameSize);
        public static int? ToCourtesyFrames(this int? courtesyBytes, int frameSize) => ConfigWishes.ToCourtesyFrames(courtesyBytes, frameSize);
        public static int? CourtesyBytesToFrames(this int? courtesyBytes, int? frameSize) => ConfigWishes.CourtesyBytesToFrames(courtesyBytes, frameSize);


        public static int CourtesyFrames(this int courtesyBytes, int frameSize) => ConfigWishes.CourtesyFrames(courtesyBytes, frameSize);
        public static int GetCourtesyFrames(this int courtesyBytes, int frameSize) => ConfigWishes.GetCourtesyFrames(courtesyBytes, frameSize);
        public static int ToCourtesyFrames(this int courtesyBytes, int frameSize) => ConfigWishes.ToCourtesyFrames(courtesyBytes, frameSize);
        public static int CourtesyBytesToFrames(this int courtesyBytes, int frameSize) => ConfigWishes.CourtesyBytesToFrames(courtesyBytes, frameSize);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static int CourtesyFrames(SynthWishes obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(SynthWishes obj, int? value) => SetCourtesyFrames(obj, value);
        public static SynthWishes WithCourtesyFrames(SynthWishes obj, int? value) => SetCourtesyFrames(obj, value);
        public static SynthWishes SetCourtesyFrames(SynthWishes obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(FlowNode obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(FlowNode obj, int? value) => SetCourtesyFrames(obj, value);
        public static FlowNode WithCourtesyFrames(FlowNode obj, int? value) => SetCourtesyFrames(obj, value);
        public static FlowNode SetCourtesyFrames(FlowNode obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        internal static int CourtesyFrames(ConfigResolver obj) => GetCourtesyFrames(obj);
        internal static int GetCourtesyFrames(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        internal static ConfigResolver CourtesyFrames(ConfigResolver obj, int? value) => SetCourtesyFrames(obj, value);
        internal static ConfigResolver WithCourtesyFrames(ConfigResolver obj, int? value) => SetCourtesyFrames(obj, value);
        internal static ConfigResolver SetCourtesyFrames(ConfigResolver obj, int? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        // Global-Bound
        
        internal static int? CourtesyFrames(ConfigSection obj) => GetCourtesyFrames(obj);
        internal static int? GetCourtesyFrames(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }
        
        // Tape-Bound
       
        public static int CourtesyFrames(Tape obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.CourtesyFrames;
        }
       
        public static Tape CourtesyFrames(Tape obj, int value) => SetCourtesyFrames(obj, value);
        public static Tape WithCourtesyFrames(Tape obj, int value) => SetCourtesyFrames(obj, value);
        public static Tape SetCourtesyFrames(Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.CourtesyFrames = value;
            return obj;
        }
       
        public static int CourtesyFrames(TapeConfig obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }
        
        public static TapeConfig CourtesyFrames(TapeConfig obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeConfig WithCourtesyFrames(TapeConfig obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeConfig SetCourtesyFrames(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.CourtesyFrames = value;
            return obj;
        }
        
        public static int CourtesyFrames(TapeActions obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeActions CourtesyFrames(TapeActions obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeActions WithCourtesyFrames(TapeActions obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeActions SetCourtesyFrames(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
        
        public static int CourtesyFrames(TapeAction obj) => GetCourtesyFrames(obj);
        public static int GetCourtesyFrames(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.CourtesyFrames();
        }
        
        public static TapeAction CourtesyFrames(TapeAction obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeAction WithCourtesyFrames(TapeAction obj, int value) => SetCourtesyFrames(obj, value);
        public static TapeAction SetCourtesyFrames(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.CourtesyFrames(value);
            return obj;
        }
 
        // Conversion Formulas
        
        // Courtesy Bytes to Frames
        
        public static int? CourtesyFrames(int? courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int? GetCourtesyFrames(int? courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int? ToCourtesyFrames(int? courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int? CourtesyBytesToFrames(int? courtesyBytes, int? frameSize) 
            => CourtesyBytesToFrames(courtesyBytes.CoalesceCourtesyBytes(), frameSize.CoalesceFrameSize());
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int GetCourtesyFrames(int courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int ToCourtesyFrames(int courtesyBytes, int frameSize) => CourtesyBytesToFrames(courtesyBytes, frameSize);
        public static int CourtesyBytesToFrames(int courtesyBytes, int frameSize)
        {
            courtesyBytes.AssertCourtesyBytes(frameSize);
            return courtesyBytes / frameSize;
        }
    }
}