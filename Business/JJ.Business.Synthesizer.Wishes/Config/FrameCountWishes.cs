using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;
// ReSharper disable ArrangeStaticMemberQualifier
#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Wishes.Config
{
    // FrameCount: A Duration Property

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class FrameCountExtensionWishes
    {
        // Synth-Bound
        
        public static int FrameCount(this SynthWishes obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this SynthWishes obj) => ConfigWishes.GetFrameCount(obj);

        public static SynthWishes FrameCount(this SynthWishes obj, int? value) => ConfigWishes.FrameCount(obj, value);
        public static SynthWishes WithFrameCount(this SynthWishes obj, int? value) => ConfigWishes.WithFrameCount(obj, value);
        public static SynthWishes SetFrameCount(this SynthWishes obj, int? value) => ConfigWishes.SetFrameCount(obj, value);

        public static int FrameCount(this FlowNode obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this FlowNode obj) => ConfigWishes.GetFrameCount(obj);

        public static FlowNode FrameCount(this FlowNode obj, int? value) => ConfigWishes.FrameCount(obj, value);
        public static FlowNode WithFrameCount(this FlowNode obj, int? value) => ConfigWishes.WithFrameCount(obj, value);
        public static FlowNode SetFrameCount(this FlowNode obj, int? value) => ConfigWishes.SetFrameCount(obj, value);

        [UsedImplicitly] internal static int FrameCount(this ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.FrameCount(obj, synthWishes);
        [UsedImplicitly] internal static int GetFrameCount(this ConfigResolver obj, SynthWishes synthWishes) => ConfigWishes.GetFrameCount(obj, synthWishes);

        [UsedImplicitly] internal static ConfigResolver FrameCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.FrameCount(obj, value, synthWishes);
        [UsedImplicitly] internal static ConfigResolver WithFrameCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.WithFrameCount(obj, value, synthWishes);
        [UsedImplicitly] internal static ConfigResolver SetFrameCount(this ConfigResolver obj, int? value, SynthWishes synthWishes) => ConfigWishes.SetFrameCount(obj, value, synthWishes);

        // Global-Bound

        [UsedImplicitly] internal static int? FrameCount(this ConfigSection obj) => ConfigWishes.FrameCount(obj);
        [UsedImplicitly] internal static int? GetFrameCount(this ConfigSection obj) => ConfigWishes.GetFrameCount(obj);

        // Tape-Bound

        public static int FrameCount(this Tape obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this Tape obj) => ConfigWishes.GetFrameCount(obj);

        public static Tape FrameCount(this Tape obj, int value) => ConfigWishes.FrameCount(obj, value);
        public static Tape WithFrameCount(this Tape obj, int value) => ConfigWishes.WithFrameCount(obj, value);
        public static Tape SetFrameCount(this Tape obj, int value) => ConfigWishes.SetFrameCount(obj, value);

        public static int FrameCount(this TapeConfig obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this TapeConfig obj) => ConfigWishes.GetFrameCount(obj);

        public static TapeConfig FrameCount(this TapeConfig obj, int value) => ConfigWishes.FrameCount(obj, value);
        public static TapeConfig WithFrameCount(this TapeConfig obj, int value) => ConfigWishes.WithFrameCount(obj, value);
        public static TapeConfig SetFrameCount(this TapeConfig obj, int value) => ConfigWishes.SetFrameCount(obj, value);

        public static int FrameCount(this TapeAction obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this TapeAction obj) => ConfigWishes.GetFrameCount(obj);

        public static TapeAction FrameCount(this TapeAction obj, int value) => ConfigWishes.FrameCount(obj, value);
        public static TapeAction WithFrameCount(this TapeAction obj, int value) => ConfigWishes.WithFrameCount(obj, value);
        public static TapeAction SetFrameCount(this TapeAction obj, int value) => ConfigWishes.SetFrameCount(obj, value);

        public static int FrameCount(this TapeActions obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this TapeActions obj) => ConfigWishes.GetFrameCount(obj);

        public static TapeActions FrameCount(this TapeActions obj, int value) => ConfigWishes.FrameCount(obj, value);
        public static TapeActions WithFrameCount(this TapeActions obj, int value) => ConfigWishes.WithFrameCount(obj, value);
        public static TapeActions SetFrameCount(this TapeActions obj, int value) => ConfigWishes.SetFrameCount(obj, value);

        // Buff-Bound

        public static int FrameCount(this Buff obj, int courtesyFrames) => ConfigWishes.FrameCount(obj, courtesyFrames);
        public static int GetFrameCount(this Buff obj, int courtesyFrames) => ConfigWishes.GetFrameCount(obj, courtesyFrames);

        public static Buff FrameCount(this Buff obj, int value, int? courtesyFrames) => ConfigWishes.FrameCount(obj, value, courtesyFrames);
        public static Buff WithFrameCount(this Buff obj, int value, int? courtesyFrames) => ConfigWishes.WithFrameCount(obj, value, courtesyFrames);
        public static Buff SetFrameCount(this Buff obj, int value, int? courtesyFrames) => ConfigWishes.SetFrameCount(obj, value, courtesyFrames);

        public static Buff FrameCount(this Buff obj, int value, int courtesyFrames) => ConfigWishes.FrameCount(obj, value, courtesyFrames);
        public static Buff WithFrameCount(this Buff obj, int value, int courtesyFrames) => ConfigWishes.WithFrameCount(obj, value, courtesyFrames);
        public static Buff SetFrameCount(this Buff obj, int value, int courtesyFrames) => ConfigWishes.SetFrameCount(obj, value, courtesyFrames);

        public static int FrameCount(this AudioFileOutput obj, int courtesyFrames) => ConfigWishes.FrameCount(obj, courtesyFrames);
        public static int GetFrameCount(this AudioFileOutput obj, int courtesyFrames) => ConfigWishes.GetFrameCount(obj, courtesyFrames);

        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value, int? courtesyFrames) => ConfigWishes.FrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value, int courtesyFrames) => ConfigWishes.FrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithFrameCount(this AudioFileOutput obj, int value, int? courtesyFrames) => ConfigWishes.WithFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithFrameCount(this AudioFileOutput obj, int value, int courtesyFrames) => ConfigWishes.WithFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput SetFrameCount(this AudioFileOutput obj, int value, int? courtesyFrames) => ConfigWishes.SetFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput SetFrameCount(this AudioFileOutput obj, int value, int courtesyFrames) => ConfigWishes.SetFrameCount(obj, value, courtesyFrames);

        // Independent after Taping

        public static int FrameCount(this Sample obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this Sample obj) => ConfigWishes.GetFrameCount(obj);

        public static int FrameCount(this AudioInfoWish infoWish) => ConfigWishes.FrameCount(infoWish);
        public static int GetFrameCount(this AudioInfoWish infoWish) => ConfigWishes.GetFrameCount(infoWish);

        public static AudioInfoWish FrameCount(this AudioInfoWish infoWish, int value) => ConfigWishes.FrameCount(infoWish, value);
        public static AudioInfoWish WithFrameCount(this AudioInfoWish infoWish, int value) => ConfigWishes.WithFrameCount(infoWish, value);
        public static AudioInfoWish SetFrameCount(this AudioInfoWish infoWish, int value) => ConfigWishes.SetFrameCount(infoWish, value);

        public static int FrameCount(this AudioFileInfo info) => ConfigWishes.FrameCount(info);
        public static int GetFrameCount(this AudioFileInfo info) => ConfigWishes.GetFrameCount(info);

        public static AudioFileInfo FrameCount(this AudioFileInfo info, int value) => ConfigWishes.FrameCount(info, value);
        public static AudioFileInfo WithFrameCount(this AudioFileInfo info, int value) => ConfigWishes.WithFrameCount(info, value);
        public static AudioFileInfo SetFrameCount(this AudioFileInfo info, int value) => ConfigWishes.SetFrameCount(info, value);

        // Immutable

        public static int FrameCount(this WavHeaderStruct obj) => ConfigWishes.FrameCount(obj);
        public static int GetFrameCount(this WavHeaderStruct obj) => ConfigWishes.GetFrameCount(obj);

        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value, int courtesyFrames) => ConfigWishes.FrameCount(obj, value, courtesyFrames);
        public static WavHeaderStruct WithFrameCount(this WavHeaderStruct obj, int value, int courtesyFrames) => ConfigWishes.WithFrameCount(obj, value, courtesyFrames);
        public static WavHeaderStruct SetFrameCount(this WavHeaderStruct obj, int value, int courtesyFrames) => ConfigWishes.SetFrameCount(obj, value, courtesyFrames);

        // Conversion Formulas

        // From AudioLength

        public static int FrameCount               (this double audioLength, int samplingRate, int courtesyFrames) 
            => ConfigWishes.FrameCount(audioLength, samplingRate, courtesyFrames);
        public static int ToFrameCount             (this double audioLength, int samplingRate, int courtesyFrames) 
            => ConfigWishes.FrameCount(audioLength, samplingRate, courtesyFrames);
        public static int GetFrameCount            (this double audioLength, int samplingRate, int courtesyFrames) 
            => ConfigWishes.FrameCount(audioLength, samplingRate, courtesyFrames);
        public static int FrameCountFromAudioLength(this double audioLength, int samplingRate, int courtesyFrames) 
            => ConfigWishes.FrameCountFromAudioLength(audioLength, samplingRate, courtesyFrames);
        
        // From ByteCount

        public static int FrameCount             (this int byteCount, int frameSize, int headerLength) 
            => ConfigWishes.FrameCount(byteCount, frameSize, headerLength);
        public static int GetFrameCount          (this int byteCount, int frameSize, int headerLength) 
            => ConfigWishes.GetFrameCount(byteCount, frameSize, headerLength);
        public static int ToFrameCount           (this int byteCount, int frameSize, int headerLength) 
            => ConfigWishes.ToFrameCount(byteCount, frameSize, headerLength);
        public static int FrameCountFromByteCount(this int byteCount, int frameSize, int headerLength) 
            => ConfigWishes.FrameCountFromByteCount(byteCount, frameSize, headerLength);
        
        public static int FrameCount             (this int byteCount, int bits, int channels, int headerLength) 
            => ConfigWishes.FrameCount(byteCount, bits, channels, headerLength);
        public static int GetFrameCount          (this int byteCount, int bits, int channels, int headerLength) 
            => ConfigWishes.GetFrameCount(byteCount, bits, channels, headerLength);
        public static int ToFrameCount           (this int byteCount, int bits, int channels, int headerLength) 
            => ConfigWishes.ToFrameCount(byteCount, bits, channels, headerLength);
        public static int FrameCountFromByteCount(this int byteCount, int bits, int channels, int headerLength) 
            => ConfigWishes.FrameCountFromByteCount(byteCount, bits, channels, headerLength);
        
        // From Bytes

        public static int FrameCount         ((byte[] bytes, string filePath) buffTuple, int frameSize, int headerLength) 
            => ConfigWishes.FrameCount(buffTuple.bytes, buffTuple.filePath, frameSize, headerLength);
        public static int ToFrameCount       ((byte[] bytes, string filePath) buffTuple, int frameSize, int headerLength) 
            => ConfigWishes.ToFrameCount(buffTuple.bytes, buffTuple.filePath, frameSize, headerLength);
        public static int FrameCountFromBuff ((byte[] bytes, string filePath) buffTuple, int frameSize, int headerLength) 
            => ConfigWishes.FrameCountFromBuff(buffTuple.bytes, buffTuple.filePath, frameSize, headerLength);
        public static int FrameCountFromBytes((byte[] bytes, string filePath) buffTuple, int frameSize, int headerLength) 
            => ConfigWishes.FrameCountFromBytes(buffTuple.bytes, buffTuple.filePath, frameSize, headerLength);
    }
    
    public partial class ConfigWishes
    {
        // Synth-Bound
        
        public static int FrameCount(SynthWishes obj) => GetFrameCount(obj);
        public static int GetFrameCount(SynthWishes obj)
        {
            return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength().Value, obj.SamplingRate(), obj.CourtesyFrames());
        }
        
        public static SynthWishes FrameCount(SynthWishes obj, int? value) => SetFrameCount(obj, value);
        public static SynthWishes WithFrameCount(SynthWishes obj, int? value) => SetFrameCount(obj, value);
        public static SynthWishes SetFrameCount(SynthWishes obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromFrameCount(value, obj.SamplingRate(), obj.CourtesyFrames()));
        }
        
        public static int FrameCount(FlowNode obj) => GetFrameCount(obj);
        public static int GetFrameCount(FlowNode obj)
        {
            return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength().Value, obj.SamplingRate(), obj.CourtesyFrames());
        }
        
        public static FlowNode FrameCount(FlowNode obj, int? value) => SetFrameCount(obj, value);
        public static FlowNode WithFrameCount(FlowNode obj, int? value) => SetFrameCount(obj, value);
        public static FlowNode SetFrameCount(FlowNode obj, int? value)
        {
            return obj.AudioLength(AudioLengthFromFrameCount(value, obj.SamplingRate(), obj.CourtesyFrames()));
        }
        
        internal static int FrameCount(ConfigResolver obj, SynthWishes synthWishes) => GetFrameCount(obj, synthWishes);
        internal static int GetFrameCount(ConfigResolver obj, SynthWishes synthWishes)
        {
            return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength(synthWishes).Value, obj.SamplingRate(), obj.CourtesyFrames());
        }
        
        internal static ConfigResolver FrameCount(ConfigResolver obj, int? value, SynthWishes synthWishes) => SetFrameCount(obj, value, synthWishes);
        internal static ConfigResolver WithFrameCount(ConfigResolver obj, int? value, SynthWishes synthWishes) => SetFrameCount(obj, value, synthWishes);
        internal static ConfigResolver SetFrameCount(ConfigResolver obj, int? value, SynthWishes synthWishes)
        {
            return obj.AudioLength(AudioLengthFromFrameCount(value, obj.SamplingRate(), obj.CourtesyFrames()), synthWishes);
        }
        
        // Global-Bound
        
        internal static int? FrameCount(ConfigSection obj) => GetFrameCount(obj);
        internal static int? GetFrameCount(ConfigSection obj)
        {
            if (obj.AudioLength() == null) return null;
            if (obj.SamplingRate() == null) return null;
            if (obj.CourtesyFrames() == null) return null;
            return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength().Value, obj.SamplingRate().Value, obj.CourtesyFrames().Value);
        }
        
        // Tape-Bound
        
        public static int FrameCount(Tape obj) => GetFrameCount(obj);
        public static int GetFrameCount(Tape obj)
        {
            if (obj.IsBuff)
            {
                return ConfigWishes.FrameCountFromBytes(obj.Bytes, obj.FilePathResolved, obj.FrameSize(), obj.HeaderLength());
            }
            else
            {
                return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength(), obj.SamplingRate(), obj.CourtesyFrames());
            }
        }
        
        public static Tape FrameCount(Tape obj, int value) => SetFrameCount(obj, value);
        public static Tape WithFrameCount(Tape obj, int value) => SetFrameCount(obj, value);
        public static Tape SetFrameCount(Tape obj, int value)
        {
            return obj.AudioLength(AudioLengthFromFrameCount(value, obj.SamplingRate(), obj.CourtesyFrames()));
        }
        
        public static int FrameCount(TapeConfig obj) => GetFrameCount(obj);
        public static int GetFrameCount(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeConfig FrameCount(TapeConfig obj, int value) => SetFrameCount(obj, value);
        public static TapeConfig WithFrameCount(TapeConfig obj, int value) => SetFrameCount(obj, value);
        public static TapeConfig SetFrameCount(TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        public static int FrameCount(TapeAction obj) => GetFrameCount(obj);
        public static int GetFrameCount(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeAction FrameCount(TapeAction obj, int value) => SetFrameCount(obj, value);
        public static TapeAction WithFrameCount(TapeAction obj, int value) => SetFrameCount(obj, value);
        public static TapeAction SetFrameCount(TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        public static int FrameCount(TapeActions obj) => GetFrameCount(obj);
        public static int GetFrameCount(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.FrameCount();
        }
        
        public static TapeActions FrameCount(TapeActions obj, int value) => SetFrameCount(obj, value);
        public static TapeActions WithFrameCount(TapeActions obj, int value) => SetFrameCount(obj, value);
        public static TapeActions SetFrameCount(TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.FrameCount(value);
            return obj;
        }
        
        // Buff-Bound
        
        public static int FrameCount(Buff obj, int courtesyFrames) => GetFrameCount(obj, courtesyFrames);
        public static int GetFrameCount(Buff obj, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            
            int frameCount = ConfigWishes.FrameCountFromBytes(obj.Bytes, obj.FilePath, obj.FrameSize(), obj.HeaderLength());
            
            if (Has(frameCount))
            {
                return frameCount;
            }
            
            if (obj.UnderlyingAudioFileOutput != null)
            {
                return obj.UnderlyingAudioFileOutput.FrameCount(courtesyFrames);
            }
            
            return 0;
        }

        public static Buff FrameCount(Buff obj, int value, int? courtesyFrames) => SetFrameCount(obj, value, courtesyFrames);
        public static Buff WithFrameCount(Buff obj, int value, int? courtesyFrames) => SetFrameCount(obj, value, courtesyFrames);
        public static Buff SetFrameCount(Buff obj, int value, int? courtesyFrames)
        {
            return FrameCount(obj, value, CoalesceCourtesyFrames(courtesyFrames));
        }
        
        public static Buff FrameCount(Buff obj, int value, int courtesyFrames) => SetFrameCount(obj, value, courtesyFrames);
        public static Buff WithFrameCount(Buff obj, int value, int courtesyFrames) => SetFrameCount(obj, value, courtesyFrames);
        public static Buff SetFrameCount(Buff obj, int value, int courtesyFrames)
        {
            if (obj == null) throw new NullException(() => obj);
            // Buff is too Buff to change his FrameCount,
            // but he can still send the message to his buddy "Out", who does the books.
            obj.UnderlyingAudioFileOutput.FrameCount(value, courtesyFrames);
            return obj;
        }
        
        public static int FrameCount(AudioFileOutput obj, int courtesyFrames) => GetFrameCount(obj, courtesyFrames);
        public static int GetFrameCount(AudioFileOutput obj, int courtesyFrames)
        {
            return ConfigWishes.FrameCountFromAudioLength(obj.AudioLength(), obj.SamplingRate(), courtesyFrames);
        }
        
        public static AudioFileOutput FrameCount(AudioFileOutput obj, int value, int? courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput FrameCount(AudioFileOutput obj, int value, int courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithFrameCount(AudioFileOutput obj, int value, int? courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput WithFrameCount(AudioFileOutput obj, int value, int courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static AudioFileOutput SetFrameCount(AudioFileOutput obj, int value, int? courtesyFrames)
        {
            return SetFrameCount(obj, value, CoalesceCourtesyFrames(courtesyFrames));
        }
        public static AudioFileOutput SetFrameCount(AudioFileOutput obj, int value, int courtesyFrames)
        {
            return obj.AudioLength(AudioLengthFromFrameCount(value, obj.SamplingRate(), courtesyFrames));
        }
        
        // Independent after Taping
                
        public static int FrameCount(Sample obj) => GetFrameCount(obj);
        public static int GetFrameCount(Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ConfigWishes.FrameCountFromBytes(obj.Bytes, obj.Location, obj.FrameSize(), obj.HeaderLength());
        }
                
        public static int FrameCount(AudioInfoWish infoWish) => GetFrameCount(infoWish);
        public static int GetFrameCount(AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.FrameCount;
        }
        
        public static AudioInfoWish FrameCount(AudioInfoWish infoWish, int value) => SetFrameCount(infoWish, value);
        public static AudioInfoWish WithFrameCount(AudioInfoWish infoWish, int value) => SetFrameCount(infoWish, value);
        public static AudioInfoWish SetFrameCount(AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = AssertFrameCount(value);
            return infoWish;
        }
        
        public static int FrameCount(AudioFileInfo info) => GetFrameCount(info);
        public static int GetFrameCount(AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SampleCount;
        }
        
        public static AudioFileInfo FrameCount(AudioFileInfo info, int value) => SetFrameCount(info, value);
        public static AudioFileInfo WithFrameCount(AudioFileInfo info, int value) => SetFrameCount(info, value);
        public static AudioFileInfo SetFrameCount(AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = AssertFrameCount(value);
            return info;
        }

        // Immutable
        
        public static int FrameCount(WavHeaderStruct obj) => GetFrameCount(obj);
        public static int GetFrameCount(WavHeaderStruct obj)
        {
            return obj.ToInfo().FrameCount();
        }
        
        public static WavHeaderStruct FrameCount(WavHeaderStruct obj, int value, int courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static WavHeaderStruct WithFrameCount(WavHeaderStruct obj, int value, int courtesyFrames)
            => SetFrameCount(obj, value, courtesyFrames);
        public static WavHeaderStruct SetFrameCount(WavHeaderStruct obj, int value, int courtesyFrames)
        {
            double audioLength = AudioLengthFromFrameCount(value, obj.SamplingRate(), courtesyFrames);
            return obj.AudioLength(audioLength, courtesyFrames);
        }
        
        // Conversion Formulas

        // From AudioLength
        
        public static int FrameCount               (double audioLength, int samplingRate, int courtesyFrames)
            => FrameCountFromAudioLength(audioLength, samplingRate, courtesyFrames);
        public static int GetFrameCount            (double audioLength, int samplingRate, int courtesyFrames)
            => FrameCountFromAudioLength(audioLength, samplingRate, courtesyFrames);
        public static int ToFrameCount             (double audioLength, int samplingRate, int courtesyFrames)
            => FrameCountFromAudioLength(audioLength, samplingRate, courtesyFrames);
        public static int FrameCountFromAudioLength(double audioLength, int samplingRate, int courtesyFrames)
        {
            AssertAudioLength(audioLength);
            AssertSamplingRate(samplingRate);
            AssertCourtesyFrames(courtesyFrames);
            
            return (int)(audioLength * samplingRate) + courtesyFrames;
        }

        // From ByteCount

        public static int FrameCount             (int byteCount, int frameSize, int headerLength)
            => FrameCountFromByteCount(byteCount, frameSize, headerLength);
        public static int GetFrameCount           (int byteCount, int frameSize, int headerLength)
            => FrameCountFromByteCount(byteCount, frameSize, headerLength);
        public static int ToFrameCount           (int byteCount, int frameSize, int headerLength)
            => FrameCountFromByteCount(byteCount, frameSize, headerLength);
        public static int FrameCountFromByteCount(int byteCount, int frameSize, int headerLength)
        {
            AssertByteCount(byteCount);
            AssertHeaderLength(headerLength);
            AssertFrameSize(frameSize);
            
            int frameCount = (byteCount - headerLength) / frameSize;
            
            AssertFrameCount(frameCount);
            
            return frameCount;
        }

        public static int FrameCount             (int byteCount, int bits, int channels, int headerLength)
            => FrameCountFromByteCount(byteCount, bits, channels, headerLength);
        public static int GetFrameCount           (int byteCount, int bits, int channels, int headerLength)
            => FrameCountFromByteCount(byteCount, bits, channels, headerLength);
        public static int ToFrameCount           (int byteCount, int bits, int channels, int headerLength)
            => FrameCountFromByteCount(byteCount, bits, channels, headerLength);
        public static int FrameCountFromByteCount(int byteCount, int bits, int channels, int headerLength)
        {
            int frameSize = FrameSize(bits, channels);
            return FrameCountFromByteCount(byteCount, frameSize, headerLength);
        }

        // From Bytes

        public static int FrameCount         (byte[] bytes, string filePath, int frameSize, int headerLength)
            => FrameCountFromBytes(bytes, filePath, frameSize, headerLength);
        public static int ToFrameCount       (byte[] bytes, string filePath, int frameSize, int headerLength)
            => FrameCountFromBytes(bytes, filePath, frameSize, headerLength);
        public static int FrameCountFromBuff (byte[] bytes, string filePath, int frameSize, int headerLength)
            => FrameCountFromBytes(bytes, filePath, frameSize, headerLength);
        public static int FrameCountFromBytes(byte[] bytes, string filePath, int frameSize, int headerLength)
        {
            AssertHeaderLength(headerLength);
            AssertFrameSize(frameSize);
            
            int byteCount  = ByteCountFromBuff(bytes, filePath);
            
            if (!Has(byteCount)) return 0;
            
            int frameCount = (byteCount - headerLength) / frameSize;
            
            AssertFrameCount(frameCount);
            
            return frameCount;
        }
    }
}