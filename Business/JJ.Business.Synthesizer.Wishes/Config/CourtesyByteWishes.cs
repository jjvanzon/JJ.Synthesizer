using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class CourtesyByteExtensionWishes
    {
        // Derived from CourtesyFrames
        
        // Synth-Bound
        
        public static int CourtesyBytes(this SynthWishes obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static SynthWishes CourtesyBytes(this SynthWishes obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        public static int CourtesyBytes(this FlowNode obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        public static FlowNode CourtesyBytes(this FlowNode obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        [UsedImplicitly]
        internal static int CourtesyBytes(this ConfigResolver obj)
            => ConfigWishes.CourtesyBytes(obj.CourtesyFrames(), obj.FrameSize());
        
        [UsedImplicitly]
        internal static ConfigResolver CourtesyBytes(this ConfigResolver obj, int? value)
            => obj.CourtesyFrames(ConfigWishes.CourtesyFrames(value, obj.FrameSize()));
        
        // Global-Bound
        
        internal static int? CourtesyBytes(this ConfigSection obj)
        {
            if (obj.CourtesyFrames() == null) return null;
            if (obj.FrameSize() == null) return null;
            return ConfigWishes.CourtesyBytes(obj.CourtesyFrames().Value, obj.FrameSize().Value);
        }
        
        // Tape-Bound
        
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

    public partial class ConfigWishes
    {
        // Conversion-Style
        
        // Courtesy Frames to Bytes
        
        public static int CourtesyFramesToBytes(int courtesyFrames, int bits, int channels) 
            => AssertCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);

        public static int CourtesyFramesToBytes(int? courtesyFrames, int? bits, int? channels) 
            => CoalesceCourtesyFrames(courtesyFrames) * FrameSize(bits, channels);
        
        public static int CourtesyFramesToBytes(int courtesyFrames, int frameSize) 
            => AssertCourtesyFrames(courtesyFrames) * AssertFrameSize(frameSize);
        
        public static int CourtesyFramesToBytes(int? courtesyFrames, int? frameSize) 
            => CoalesceCourtesyFrames(courtesyFrames) * CoalesceFrameSize(frameSize);

        // Courtesy Bytes to Frames
        
        public static int? CourtesyBytesToFrames(int? courtesyBytes, int? frameSize) 
            => CourtesyBytesToFrames(courtesyBytes.CoalesceCourtesyBytes(), frameSize.CoalesceFrameSize());
        
        public static int CourtesyBytesToFrames(int courtesyBytes, int frameSize)
        {
            courtesyBytes.AssertCourtesyBytes(frameSize);
            return courtesyBytes / frameSize;
        }
        
        // Synonyms
        
        // CourtesyBytes
        
        public static int CourtesyBytes(int courtesyFrames, int bits, int channels) 
            => CourtesyFramesToBytes(courtesyFrames, bits, channels);
        
        public static int CourtesyBytes(int? courtesyFrames, int? bits, int? channels) 
            => CourtesyFramesToBytes(courtesyFrames, bits, channels);

        public static int CourtesyBytes(int courtesyFrames, int frameSize) 
            => CourtesyFramesToBytes(courtesyFrames, frameSize);

        public static int CourtesyBytes(int? courtesyFrames, int? frameSize) 
            => CourtesyFramesToBytes(courtesyFrames, frameSize);
        
        // CourtesyFrames
        
        public static int? CourtesyFrames(int? courtesyBytes, int frameSize)
            => CourtesyBytesToFrames(courtesyBytes, frameSize);
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize)
            => CourtesyBytesToFrames(courtesyBytes, frameSize);
    }
}