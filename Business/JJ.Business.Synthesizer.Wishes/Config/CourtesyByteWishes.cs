using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // Derived from CourtesyFrames

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class CourtesyByteExtensionWishes
    {
        // Synth-Bound
        
        public static int CourtesyBytes(this SynthWishes obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this SynthWishes obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static SynthWishes CourtesyBytes(this SynthWishes obj, int? value) => ConfigWishes.CourtesyBytes(obj, value);
        public static SynthWishes WithCourtesyBytes(this SynthWishes obj, int? value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static SynthWishes SetCourtesyBytes(this SynthWishes obj, int? value) => ConfigWishes.SetCourtesyBytes(obj, value);

        public static int CourtesyBytes(this FlowNode obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this FlowNode obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static FlowNode CourtesyBytes(this FlowNode obj, int? value) => ConfigWishes.CourtesyBytes(obj, value);
        public static FlowNode WithCourtesyBytes(this FlowNode obj, int? value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static FlowNode SetCourtesyBytes(this FlowNode obj, int? value) => ConfigWishes.SetCourtesyBytes(obj, value);

        [UsedImplicitly] internal static int CourtesyBytes(this ConfigResolver obj) => ConfigWishes.CourtesyBytes(obj);
        [UsedImplicitly] internal static int GetCourtesyBytes(this ConfigResolver obj) => ConfigWishes.GetCourtesyBytes(obj);

        [UsedImplicitly] internal static ConfigResolver CourtesyBytes(this ConfigResolver obj, int? value) => ConfigWishes.CourtesyBytes(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithCourtesyBytes(this ConfigResolver obj, int? value) => ConfigWishes.WithCourtesyBytes(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetCourtesyBytes(this ConfigResolver obj, int? value) => ConfigWishes.SetCourtesyBytes(obj, value);

        // Global-Bound

        internal static int? CourtesyBytes(this ConfigSection obj) => ConfigWishes.CourtesyBytes(obj);
        internal static int? GetCourtesyBytes(this ConfigSection obj) => ConfigWishes.GetCourtesyBytes(obj);

        // Tape-Bound

        public static int CourtesyBytes(this Tape obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this Tape obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static Tape CourtesyBytes(this Tape obj, int value) => ConfigWishes.CourtesyBytes(obj, value);
        public static Tape WithCourtesyBytes(this Tape obj, int value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static Tape SetCourtesyBytes(this Tape obj, int value) => ConfigWishes.SetCourtesyBytes(obj, value);

        public static int CourtesyBytes(this TapeConfig obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this TapeConfig obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static TapeConfig CourtesyBytes(this TapeConfig obj, int value) => ConfigWishes.CourtesyBytes(obj, value);
        public static TapeConfig WithCourtesyBytes(this TapeConfig obj, int value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static TapeConfig SetCourtesyBytes(this TapeConfig obj, int value) => ConfigWishes.SetCourtesyBytes(obj, value);

        public static int CourtesyBytes(this TapeActions obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this TapeActions obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static TapeActions CourtesyBytes(this TapeActions obj, int value) => ConfigWishes.CourtesyBytes(obj, value);
        public static TapeActions WithCourtesyBytes(this TapeActions obj, int value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static TapeActions SetCourtesyBytes(this TapeActions obj, int value) => ConfigWishes.SetCourtesyBytes(obj, value);

        public static int CourtesyBytes(this TapeAction obj) => ConfigWishes.CourtesyBytes(obj);
        public static int GetCourtesyBytes(this TapeAction obj) => ConfigWishes.GetCourtesyBytes(obj);

        public static TapeAction CourtesyBytes(this TapeAction obj, int value) => ConfigWishes.CourtesyBytes(obj, value);
        public static TapeAction WithCourtesyBytes(this TapeAction obj, int value) => ConfigWishes.WithCourtesyBytes(obj, value);
        public static TapeAction SetCourtesyBytes(this TapeAction obj, int value) => ConfigWishes.SetCourtesyBytes(obj, value);
        
        public static int CourtesyBytes(this AudioFileOutput obj, int courtesyFrames) => ConfigWishes.CourtesyBytes(obj, courtesyFrames);
        public static int GetCourtesyBytes(this AudioFileOutput obj, int courtesyFrames) => ConfigWishes.GetCourtesyBytes(obj, courtesyFrames);
        
        public static int CourtesyBytes(this Buff obj, int courtesyFrames) => ConfigWishes.CourtesyBytes(obj, courtesyFrames);
        public static int GetCourtesyBytes(this Buff obj, int courtesyFrames) => ConfigWishes.GetCourtesyBytes(obj, courtesyFrames);
        
        // Conversion Formulas
        
        // Courtesy Frames to Bytes
        
        public static int CourtesyBytes(this int courtesyFrames, int bits, int channels) => ConfigWishes.CourtesyBytes(courtesyFrames, bits, channels);
        public static int GetCourtesyBytes(this int courtesyFrames, int bits, int channels) => ConfigWishes.GetCourtesyBytes(courtesyFrames, bits, channels);
        public static int ToCourtesyBytes(this int courtesyFrames, int bits, int channels) => ConfigWishes.ToCourtesyBytes(courtesyFrames, bits, channels);
        public static int CourtesyFramesToBytes(this int courtesyFrames, int bits, int channels) => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, bits, channels);

        public static int CourtesyBytes(this int? courtesyFrames, int? bits, int? channels) => ConfigWishes.CourtesyBytes(courtesyFrames, bits, channels);
        public static int GetCourtesyBytes(this int? courtesyFrames, int? bits, int? channels) => ConfigWishes.GetCourtesyBytes(courtesyFrames, bits, channels);
        public static int ToCourtesyBytes(this int? courtesyFrames, int? bits, int? channels) => ConfigWishes.ToCourtesyBytes(courtesyFrames, bits, channels);
        public static int CourtesyFramesToBytes(this int? courtesyFrames, int? bits, int? channels) => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, bits, channels);


        public static int CourtesyBytes(this int courtesyFrames, int frameSize) => ConfigWishes.CourtesyBytes(courtesyFrames, frameSize);
        public static int GetCourtesyBytes(this int courtesyFrames, int frameSize) => ConfigWishes.GetCourtesyBytes(courtesyFrames, frameSize);
        public static int ToCourtesyBytes(this int courtesyFrames, int frameSize) => ConfigWishes.ToCourtesyBytes(courtesyFrames, frameSize);
        public static int CourtesyFramesToBytes(this int courtesyFrames, int frameSize) => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize);


        public static int CourtesyBytes(this int? courtesyFrames, int? frameSize) => ConfigWishes.CourtesyBytes(courtesyFrames, frameSize);
        public static int GetCourtesyBytes(this int? courtesyFrames, int? frameSize) => ConfigWishes.GetCourtesyBytes(courtesyFrames, frameSize);
        public static int ToCourtesyBytes(this int? courtesyFrames, int? frameSize) => ConfigWishes.ToCourtesyBytes(courtesyFrames, frameSize);
        public static int CourtesyFramesToBytes(this int? courtesyFrames, int? frameSize) => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static int CourtesyBytes(SynthWishes obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(SynthWishes obj)
        {
            return GetCourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        }
        
        public static SynthWishes CourtesyBytes(SynthWishes obj, int? value) => SetCourtesyBytes(obj, value);
        public static SynthWishes WithCourtesyBytes(SynthWishes obj, int? value) => SetCourtesyBytes(obj, value);
        public static SynthWishes SetCourtesyBytes(SynthWishes obj, int? value)
        {
            return obj.SetCourtesyFrames(GetCourtesyFrames(value, obj.FrameSize()));
        }
        
        public static int CourtesyBytes(FlowNode obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(FlowNode obj)
        {
            return GetCourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        }
        
        public static FlowNode CourtesyBytes(FlowNode obj, int? value) => SetCourtesyBytes(obj, value);
        public static FlowNode WithCourtesyBytes(FlowNode obj, int? value) => SetCourtesyBytes(obj, value);
        public static FlowNode SetCourtesyBytes(FlowNode obj, int? value)
        {
            return obj.WithCourtesyFrames(GetCourtesyFrames(value, obj.FrameSize()));
        }
        
        [UsedImplicitly] internal static int CourtesyBytes(ConfigResolver obj) => GetCourtesyBytes(obj);
        [UsedImplicitly] internal static int GetCourtesyBytes(ConfigResolver obj)
        {
            return GetCourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        }
        
        [UsedImplicitly] internal static ConfigResolver CourtesyBytes(ConfigResolver obj, int? value) => SetCourtesyBytes(obj, value);
        [UsedImplicitly] internal static ConfigResolver WithCourtesyBytes(ConfigResolver obj, int? value) => SetCourtesyBytes(obj, value);
        [UsedImplicitly] internal static ConfigResolver SetCourtesyBytes(ConfigResolver obj, int? value)
        {
            return obj.WithCourtesyFrames(GetCourtesyFrames(value, obj.FrameSize()));
        }
        
        // Global-Bound
        
        internal static int? CourtesyBytes(ConfigSection obj) => GetCourtesyBytes(obj);
        internal static int? GetCourtesyBytes(ConfigSection obj)
        {
            if (obj.CourtesyFrames() == null) return null;
            if (obj.FrameSize() == null) return null;
            return CourtesyFramesToBytes(obj.CourtesyFrames().Value, obj.FrameSize().Value);
        }
        
        // Tape-Bound
        
        public static int CourtesyBytes(Tape obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(Tape obj)
        {
            return CourtesyFramesToBytes(obj.CourtesyFrames(), obj.FrameSize());
        }
        
        public static Tape CourtesyBytes(Tape obj, int value) => SetCourtesyBytes(obj, value);
        public static Tape WithCourtesyBytes(Tape obj, int value) => SetCourtesyBytes(obj, value);
        public static Tape SetCourtesyBytes(Tape obj, int value)
        {
            return obj.CourtesyFrames(GetCourtesyFrames(value, obj.FrameSize()));
        }
        
        public static int CourtesyBytes(TapeConfig obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.GetCourtesyBytes();
        }
        
        public static TapeConfig CourtesyBytes(TapeConfig obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeConfig WithCourtesyBytes(TapeConfig obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeConfig SetCourtesyBytes(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.SetCourtesyBytes(value);
            return obj;
        }
        
        public static int CourtesyBytes(TapeActions obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.GetCourtesyBytes();
        }
        
        public static TapeActions CourtesyBytes(TapeActions obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeActions WithCourtesyBytes(TapeActions obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeActions SetCourtesyBytes(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.SetCourtesyBytes(value);
            return obj;
        }
        
        public static int CourtesyBytes(TapeAction obj) => GetCourtesyBytes(obj);
        public static int GetCourtesyBytes(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.GetCourtesyBytes();
        }
        
        public static TapeAction CourtesyBytes(TapeAction obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeAction WithCourtesyBytes(TapeAction obj, int value) => SetCourtesyBytes(obj, value);
        public static TapeAction SetCourtesyBytes(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.SetCourtesyBytes(value);
            return obj;
        }
        
        public static int CourtesyBytes(AudioFileOutput obj, int courtesyFrames) => GetCourtesyBytes(obj, courtesyFrames);
        public static int GetCourtesyBytes(AudioFileOutput obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            AssertCourtesyFrames(courtesyFrames);
            return courtesyFrames * obj.FrameSize();
        }

        public static int CourtesyBytes(Buff obj, int courtesyFrames) => GetCourtesyBytes(obj, courtesyFrames);
        public static int GetCourtesyBytes(Buff obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            return GetCourtesyBytes(obj.UnderlyingAudioFileOutput, courtesyFrames);
        }

        // Conversion Formulas
        
        // Courtesy Frames to Bytes
        
        public static int CourtesyBytes(int courtesyFrames, int bits, int channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int GetCourtesyBytes(int courtesyFrames, int bits, int channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int ToCourtesyBytes(int courtesyFrames, int bits, int channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int CourtesyFramesToBytes(int courtesyFrames, int bits, int channels) 
            => AssertCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);

        public static int CourtesyBytes(int? courtesyFrames, int? bits, int? channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int GetCourtesyBytes(int? courtesyFrames, int? bits, int? channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int ToCourtesyBytes(int? courtesyFrames, int? bits, int? channels) => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        public static int CourtesyFramesToBytes(int? courtesyFrames, int? bits, int? channels) 
            => CoalesceCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);
        
        public static int CourtesyBytes(int courtesyFrames, int frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int GetCourtesyBytes(int courtesyFrames, int frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int ToCourtesyBytes(int courtesyFrames, int frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int CourtesyFramesToBytes(int courtesyFrames, int frameSize) 
            => AssertCourtesyFrames(courtesyFrames) * AssertFrameSize(frameSize);
        
        public static int CourtesyBytes(int? courtesyFrames, int? frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int GetCourtesyBytes(int? courtesyFrames, int? frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int ToCourtesyBytes(int? courtesyFrames, int? frameSize) => CourtesyFramesToBytes(courtesyFrames, frameSize);
        public static int CourtesyFramesToBytes(int? courtesyFrames, int? frameSize) 
            => CoalesceCourtesyFrames(courtesyFrames) * CoalesceFrameSize(frameSize);
    }
}