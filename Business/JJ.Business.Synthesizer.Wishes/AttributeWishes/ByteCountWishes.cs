using System;
using System.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.IO.File;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // A Duration Attribute

        public static int ByteCount(byte[] bytes, string filePath)
        {
            if (Has(bytes))
            {
                return bytes.Length;
            }

            if (Exists(filePath))
            {
                long fileSize = new FileInfo(filePath).Length;
                int maxSize = int.MaxValue;
                if (fileSize > maxSize) throw new Exception($"File is too large. Max size = {PrettyByteCount(maxSize)}");
                return (int)fileSize;
            }

            return 0;
        }

        public static int ByteCount(int frameCount, int frameSize, int headerLength, int courtesyFrames = 0)
            => frameCount * frameSize + headerLength + CourtesyBytes(courtesyFrames, frameSize);

        public static int ByteCount(this SynthWishes obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static SynthWishes ByteCount(this SynthWishes obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this FlowNode obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
        
        public static FlowNode ByteCount(this FlowNode obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));

        public static int ByteCount(this ConfigWishes obj, SynthWishes synthWishes) 
            => ByteCount(FrameCount(obj, synthWishes), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
       
        public static ConfigWishes ByteCount(this ConfigWishes obj, int value, SynthWishes synthWishes)
        {
            double audioLength = AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj));
            AudioLength(obj, audioLength, synthWishes);
            return obj;
        }

        internal static int ByteCount(this ConfigSection obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static int ByteCount(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ByteCount(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), obj.Config.CourtesyFrames);
            }
        }

        public static Tape ByteCount(this Tape obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeConfig ByteCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }
        
        public static int ByteCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeActions ByteCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeAction ByteCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this Buff obj, int courtesyFrames = 0)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ByteCount(obj.Bytes, obj.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return BytesNeeded(obj.UnderlyingAudioFileOutput, courtesyFrames);
            }

            return 0;
        }

        public static int ByteCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Bytes, obj.Location);
        }

        public static int ByteCount(this AudioFileOutput obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames = 0) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), courtesyFrames);

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames = 0) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), courtesyFrames));

        public static int ByteCount(this WavHeaderStruct obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value, int courtesyFrames = 0)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLength(value, FrameSize(wish), SamplingRate(wish), HeaderLength(Wav), courtesyFrames);
            return wish.AudioLength(audioLength).ToWavHeader();
        }
    }
}
