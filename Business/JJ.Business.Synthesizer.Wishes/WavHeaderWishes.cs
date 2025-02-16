using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.IO;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;

#pragma warning disable CS0618
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public class WavHeaderWishes
    {
        public static AudioInfoWish ToWish(int bits, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bits        .AssertBits        (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToWish<TBits>(int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = TypeToBits<TBits>(),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToWish(Type bitsType, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsType    .ToBits            (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToWish(SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEnum    .ToBits            (),
            Channels     = channelsEnum.ToChannels        (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToWish(SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEntity    .ToBits            (),
            Channels     = channelsEntity.ToChannels        (),
            SamplingRate = samplingRate  .AssertSamplingRate(),
            FrameCount   = frameCount    .AssertFrameCount  ()
        };

        public static AudioInfoWish ToWish((int bits, int channels, int samplingRate, int frameCount) x) 
            => ToWish(x.bits, x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToWish<TBits>((int channels, int samplingRate, int frameCount) x) 
            => ToWish<TBits>(x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToWish((Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => ToWish(x.bitsType, x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToWish((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => ToWish(x.bitsEnum, x.channelsEnum, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToWish((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x)
            => ToWish(x.bitsEntity, x.channelsEntity, x.samplingRate, x.frameCount);
    }

    public static class ToWishExtensions
    {
        public static AudioInfoWish ToWish(this SynthWishes entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this FlowNode entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        internal static AudioInfoWish ToWish(this ConfigResolver entity, SynthWishes synthWishes) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(synthWishes)
        };

        internal static AudioInfoWish ToWish(this ConfigSection entity) => new AudioInfoWish
        {
            Bits         = entity.Bits        ().CoalesceBits        (),
            Channels     = entity.Channels    ().CoalesceChannels    (),
            SamplingRate = entity.SamplingRate().CoalesceSamplingRate(),
            FrameCount   = entity.FrameCount  ().CoalesceFrameCount  ()
        };

        public static AudioInfoWish ToWish(this Tape entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeConfig entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeActions entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeAction entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this Buff entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToWish(this Buff entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };                
                
        public static AudioInfoWish ToWish(this AudioFileOutput entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToWish(this AudioFileOutput entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };
        
        public static AudioInfoWish ToWish(this Sample entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this AudioFileInfo info) => new AudioInfoWish
        {
            Bits         = info.Bits(),
            Channels     = info.Channels(),
            FrameCount   = info.FrameCount(),
            SamplingRate = info.SamplingRate()
        };

        public static AudioInfoWish ToWish(this WavHeaderStruct wavHeader)
        {
            var info = WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader);
            return info.ToWish();
        }
        
        public static AudioInfoWish ToWish(this (int bits, int channels, int samplingRate, int frameCount) x) 
            => WavHeaderWishes.ToWish(x);
        public static AudioInfoWish ToWish<TBits>(this (int channels, int samplingRate, int frameCount) x) 
            => WavHeaderWishes.ToWish<TBits>(x);
        public static AudioInfoWish ToWish(this (Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => WavHeaderWishes.ToWish(x);
        public static AudioInfoWish ToWish(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => WavHeaderWishes.ToWish(x);
        public static AudioInfoWish ToWish(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => WavHeaderWishes.ToWish(x);
    }

    public static class FromWishExtensions
    {
        public static void ApplyTo(this AudioInfoWish infoWish, SynthWishes entity) => entity.FromWish(infoWish);
        public static void FromWish(this SynthWishes entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
        
        public static void ApplyTo(this AudioInfoWish infoWish, FlowNode entity) => entity.FromWish(infoWish);
        public static void FromWish(this FlowNode entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
        
        internal static void ApplyTo(this AudioInfoWish infoWish, ConfigResolver entity, SynthWishes synthWishes) 
            => entity.FromWish(infoWish, synthWishes);
        internal static void FromWish(this ConfigResolver entity, AudioInfoWish infoWish, SynthWishes synthWishes)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, synthWishes);
        }
                
        public static void ApplyTo(this AudioInfoWish infoWish, Tape entity) => entity.FromWish(infoWish);
        public static void FromWish(this Tape entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(this AudioInfoWish infoWish, TapeConfig entity) => entity.FromWish(infoWish);
        public static void FromWish(this TapeConfig entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(this AudioInfoWish infoWish, TapeActions entity) => entity.FromWish(infoWish);
        public static void FromWish(this TapeActions entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(this AudioInfoWish infoWish, TapeAction entity) => entity.FromWish(infoWish);
        public static void FromWish(this TapeAction entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                        
        public static void ApplyTo(this AudioInfoWish infoWish, Buff entity, int courtesyFrames, IContext context) 
            => entity.FromWish(infoWish, courtesyFrames, context);
        public static void FromWish(this Buff entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
        }
                        
        public static void ApplyTo(this AudioInfoWish infoWish, AudioFileOutput entity, int courtesyFrames, IContext context) 
            => entity.FromWish(infoWish, courtesyFrames, context);
        public static void FromWish(this AudioFileOutput entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
        }
                        
        public static void ApplyTo(this AudioInfoWish infoWish, Sample entity, IContext context) 
            => entity.FromWish(infoWish, context);
        public static void FromWish(this Sample entity, AudioInfoWish infoWish, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            // TODO: FrameCount not settable, but this might be the one time that the byte buffer should be scalable.
        }

        public static void ApplyTo(this AudioInfoWish infoWish, AudioFileInfo entity) => entity.FromWish(infoWish);
        public static void FromWish(this AudioFileInfo entity, AudioInfoWish infoWish) 
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }

        public static void ApplyTo(this AudioInfoWish source, AudioInfoWish dest) => dest.FromWish(source);
        public static void FromWish(this AudioInfoWish dest, AudioInfoWish source) 
        {
            if (source == null) throw new NullException(() => source);
            dest.SetBits        (source.Bits        );
            dest.SetChannels    (source.Channels    );
            dest.SetSamplingRate(source.SamplingRate);
            dest.SetFrameCount  (source.FrameCount  );
        }

        public static AudioFileInfo FromWish(this AudioInfoWish wish) 
        {
            var dest = new AudioFileInfo();
            dest.FromWish(wish);
            return dest;
        }
    }

    public static class ToWavHeaderExtensions
    {
        public static WavHeaderStruct ToWavHeader(this SynthWishes entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this FlowNode entity)
            => entity.ToWish().ToWavHeader();
        
        internal static WavHeaderStruct ToWavHeader(this ConfigResolver entity, SynthWishes synthWishes)
            => entity.ToWish(synthWishes).ToWavHeader();
        
        internal static WavHeaderStruct ToWavHeader(this ConfigSection entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Tape entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeConfig entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeActions entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeAction entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Buff entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Buff entity, int courtesyFrames)
            => entity.ToWish(courtesyFrames).ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this AudioFileOutput entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this AudioFileOutput entity, int courtesyFrames)
            => entity.ToWish(courtesyFrames).ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Sample entity)
            => entity.ToWish().ToWavHeader();

        public static WavHeaderStruct ToWavHeader(this AudioInfoWish entity)
            => WavHeaderManager.CreateWavHeaderStruct(entity.FromWish());
        
        public static WavHeaderStruct ToWavHeader(this AudioFileInfo entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x) 
            => x.ToWish<TBits>().ToWavHeader();
        public static WavHeaderStruct ToWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();

    }
    
    public static class FromWavHeaderExtensions
    { 
        public static void ApplyTo(this WavHeaderStruct wavHeader, SynthWishes entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this SynthWishes entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, FlowNode entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this FlowNode entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        internal static void ApplyTo(this WavHeaderStruct wavHeader, ConfigResolver entity, SynthWishes synthWishes) 
            => entity.FromWavHeader(wavHeader, synthWishes);
        internal static void FromWavHeader(this ConfigResolver entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => wavHeader.ToWish().ApplyTo(entity, synthWishes);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, Tape entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this Tape entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, TapeConfig entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this TapeConfig entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, TapeActions entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this TapeActions entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, TapeAction entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this TapeAction entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, Buff entity, int courtesyFrames, IContext context) 
            => entity.FromWavHeader(wavHeader, courtesyFrames, context);
        public static void FromWavHeader(this Buff entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, courtesyFrames, context);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, AudioFileOutput entity, int courtesyFrames, IContext context) 
            => entity.FromWavHeader(wavHeader, courtesyFrames, context);
        public static void FromWavHeader(this AudioFileOutput entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, courtesyFrames, context);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, Sample entity, IContext context) 
            => entity.FromWavHeader(wavHeader, context);
        public static void FromWavHeader(this Sample entity, WavHeaderStruct wavHeader, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, context);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, AudioFileInfo entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this AudioFileInfo entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyTo(this WavHeaderStruct wavHeader, AudioInfoWish entity) => entity.FromWavHeader(wavHeader);
        public static void FromWavHeader(this AudioInfoWish entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
    }
            
    public static class ReadWavHeaderExtensions
    {
        public static void ReadWavHeader(this SynthWishes entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this SynthWishes entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this SynthWishes entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this SynthWishes entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
        
        public static void ReadWavHeader(this FlowNode entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this FlowNode entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this FlowNode entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this FlowNode entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
        
        internal static void ReadWavHeader(this ConfigResolver entity, string filePath, SynthWishes synthWishes)
            => filePath.ReadWavHeader().ApplyTo(entity, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyTo(entity, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, Stream source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyTo(entity, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, BinaryReader source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyTo(entity, synthWishes);
        
        public static void ReadWavHeader(this Tape entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this Tape entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this Tape entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this Tape entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
        
        public static void ReadWavHeader(this TapeConfig entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeConfig entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeConfig entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeConfig entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
                
        public static void ReadWavHeader(this TapeActions entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeActions entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeActions entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeActions entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
                
        public static void ReadWavHeader(this TapeAction entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeAction entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeAction entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this TapeAction entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);
                
        public static void ReadWavHeader(this Buff entity, string filePath, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, byte[] source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, Stream source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity,courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, BinaryReader source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
                
        public static void ReadWavHeader(this AudioFileOutput entity, string filePath, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, byte[] source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, Stream source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, BinaryReader source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, courtesyFrames, context);
                
        public static void ReadWavHeader(this Sample entity, string filePath, IContext context)
            => filePath.ReadWavHeader().ApplyTo(entity, context);
        public static void ReadWavHeader(this Sample entity, byte[] source, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, context);
        public static void ReadWavHeader(this Sample entity, Stream source, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, context);
        public static void ReadWavHeader(this Sample entity, BinaryReader source, IContext context)
            => source.ReadWavHeader().ApplyTo(entity, context);
                
        public static void ReadWavHeader(this AudioFileInfo entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioFileInfo entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioFileInfo entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioFileInfo entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);

        public static void ReadWavHeader(this AudioInfoWish entity, string filePath)
            => filePath.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioInfoWish entity, byte[] source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioInfoWish entity, Stream source)
            => source.ReadWavHeader().ApplyTo(entity);
        public static void ReadWavHeader(this AudioInfoWish entity, BinaryReader source)
            => source.ReadWavHeader().ApplyTo(entity);

        public static WavHeaderStruct ReadWavHeader(this string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return ReadWavHeader(fileStream);
        }
        
        public static WavHeaderStruct ReadWavHeader(this byte[] bytes)
            => new MemoryStream(bytes).ReadWavHeader();
        
        public static WavHeaderStruct ReadWavHeader(this Stream stream)
            => new BinaryReader(stream).ReadWavHeader();
        
        public static WavHeaderStruct ReadWavHeader(this BinaryReader reader)
            => reader.ReadStruct<WavHeaderStruct>();
    }

    public static class ReadAudioInfoExtensions
    {
        public static AudioInfoWish ReadAudioInfo(this string       source) => source.ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(this byte[]       source) => source.ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(this Stream       source) => source.ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(this BinaryReader source) => source.ReadWavHeader().ToWish();
    }
    
    public static class WriteWavHeaderExtensions
    {
        public static void WriteWavHeader(this SynthWishes  entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this SynthWishes  entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this SynthWishes  entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this SynthWishes  entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, SynthWishes  entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        
        public static void WriteWavHeader(this FlowNode     entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this FlowNode     entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this FlowNode     entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this FlowNode     entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, FlowNode     entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        
        internal static void WriteWavHeader(this ConfigResolver entity,   string         filePath, SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(filePath);
        internal static void WriteWavHeader(this ConfigResolver entity,   byte[]         dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        internal static void WriteWavHeader(this ConfigResolver entity,   BinaryWriter   dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        internal static void WriteWavHeader(this ConfigResolver entity,   Stream         dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        internal static void WriteWavHeader(this string         filePath, ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(filePath);
        internal static void WriteWavHeader(this byte[]         dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        internal static void WriteWavHeader(this Stream         dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        internal static void WriteWavHeader(this BinaryWriter   dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        
        internal static void WriteWavHeader(this ConfigSection entity,   string        filePath) => entity.ToWavHeader().Write(filePath);
        internal static void WriteWavHeader(this ConfigSection entity,   byte[]        dest    ) => entity.ToWavHeader().Write(dest    );
        internal static void WriteWavHeader(this ConfigSection entity,   BinaryWriter  dest    ) => entity.ToWavHeader().Write(dest    );
        internal static void WriteWavHeader(this ConfigSection entity,   Stream        dest    ) => entity.ToWavHeader().Write(dest    );
        internal static void WriteWavHeader(this string        filePath, ConfigSection entity  ) => entity.ToWavHeader().Write(filePath);
        internal static void WriteWavHeader(this byte[]        dest,     ConfigSection entity  ) => entity.ToWavHeader().Write(dest    );
        internal static void WriteWavHeader(this Stream        dest,     ConfigSection entity  ) => entity.ToWavHeader().Write(dest    );
        internal static void WriteWavHeader(this BinaryWriter  dest,     ConfigSection entity  ) => entity.ToWavHeader().Write(dest    );
                
        public static void WriteWavHeader(this Tape         entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this Tape         entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Tape         entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Tape         entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, Tape         entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     Tape         entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     Tape         entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     Tape         entity  ) => entity.ToWavHeader().Write(dest    );
                
        public static void WriteWavHeader(this TapeConfig   entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this TapeConfig   entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeConfig   entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeConfig   entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, TapeConfig   entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     TapeConfig   entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     TapeConfig   entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     TapeConfig   entity  ) => entity.ToWavHeader().Write(dest    );
                
        public static void WriteWavHeader(this TapeActions  entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this TapeActions  entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeActions  entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeActions  entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, TapeActions  entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     TapeActions  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     TapeActions  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     TapeActions  entity  ) => entity.ToWavHeader().Write(dest    );
                
        public static void WriteWavHeader(this TapeAction   entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this TapeAction   entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeAction   entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this TapeAction   entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string       filePath, TapeAction   entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     TapeAction   entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     TapeAction   entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     TapeAction   entity  ) => entity.ToWavHeader().Write(dest    );

        public static void WriteWavHeader(this Buff         entity,   string       filePath, int frameCount) => entity.ToWavHeader(frameCount).Write(filePath);
        public static void WriteWavHeader(this Buff         entity,   byte[]       dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this Buff         entity,   BinaryWriter dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this Buff         entity,   Stream       dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this string       filePath, Buff         entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(filePath);
        public static void WriteWavHeader(this byte[]       dest,     Buff         entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this Stream       dest,     Buff         entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this BinaryWriter dest,     Buff         entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        
        public static void WriteWavHeader(this AudioFileOutput entity,   string          filePath, int frameCount) => entity.ToWavHeader(frameCount).Write(filePath);
        public static void WriteWavHeader(this AudioFileOutput entity,   byte[]          dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this AudioFileOutput entity,   BinaryWriter    dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this AudioFileOutput entity,   Stream          dest,     int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this string          filePath, AudioFileOutput entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(filePath);
        public static void WriteWavHeader(this byte[]          dest,     AudioFileOutput entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this Stream          dest,     AudioFileOutput entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        public static void WriteWavHeader(this BinaryWriter    dest,     AudioFileOutput entity,   int frameCount) => entity.ToWavHeader(frameCount).Write(dest    );
        
        public static void WriteWavHeader(this Sample entity,   string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this Sample entity,   byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Sample entity,   BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Sample entity,   Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string          filePath, Sample entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]          dest,     Sample entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream          dest,     Sample entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter    dest,     Sample entity  ) => entity.ToWavHeader().Write(dest    );
        
        public static void WriteWavHeader(this AudioInfoWish entity,   string        filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this AudioInfoWish entity,   byte[]        dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this AudioInfoWish entity,   Stream        dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this AudioInfoWish entity,   BinaryWriter  dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string        filePath, AudioInfoWish entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]        dest,     AudioInfoWish entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream        dest,     AudioInfoWish entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter  dest,     AudioInfoWish entity  ) => entity.ToWavHeader().Write(dest    );
        
        public static void WriteWavHeader(this AudioFileInfo entity,   string        filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this AudioFileInfo entity,   byte[]        dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this AudioFileInfo entity,   Stream        dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this AudioFileInfo entity,   BinaryWriter  dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this string        filePath, AudioFileInfo entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(this byte[]        dest,     AudioFileInfo entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this Stream        dest,     AudioFileInfo entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(this BinaryWriter  dest,     AudioFileInfo entity  ) => entity.ToWavHeader().Write(dest    );
        
        public static void Write(this WavHeaderStruct wavHeader, BinaryWriter    dest    ) => dest.WriteStruct(wavHeader);
        public static void Write(this WavHeaderStruct wavHeader, Stream          dest    ) => new BinaryWriter(dest).Write(wavHeader);
        public static void Write(this WavHeaderStruct wavHeader, byte[]          dest    ) => new MemoryStream(dest).Write(wavHeader);
        public static void Write(this WavHeaderStruct wavHeader, string          filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                fileStream.Write(wavHeader);
        }
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, BinaryWriter dest     ) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, Stream       dest     ) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, byte[]       dest     ) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, string       filePath ) => wavHeader.Write(filePath);
        public static void WriteWavHeader(this BinaryWriter      dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this Stream            dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this byte[]            dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void WriteWavHeader(this string        filePath, WavHeaderStruct wavHeader) => wavHeader.Write(filePath);
        public static void Write         (this BinaryWriter      dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void Write         (this Stream            dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void Write         (this byte[]            dest, WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void Write         (this string        filePath, WavHeaderStruct wavHeader) => wavHeader.Write(filePath);
        
        // With Values
        
        // TODO: Tuples
        // TODO: Tuple as this arguments (switch arguments).
        
        public static void WriteWavHeader(this string filePath, int bits, int channels, int samplingRate, int frameCount)
            => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(this byte[] dest, int bits, int channels, int samplingRate, int frameCount)
            => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this Stream dest, int bits, int channels, int samplingRate, int frameCount)
            => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this BinaryWriter dest, int bits, int channels, int samplingRate, int frameCount)
            => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);

        public static void WriteWavHeader<TBits>(this string filePath, int channels, int samplingRate, int frameCount)
            => (typeof(TBits), channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>(this byte[] dest, int channels, int samplingRate, int frameCount)
            => (typeof(TBits), channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader<TBits>(this Stream dest, int channels, int samplingRate, int frameCount)
            => (typeof(TBits), channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader<TBits>(this BinaryWriter dest, int channels, int samplingRate, int frameCount)
            => (typeof(TBits), channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        
        public static void WriteWavHeader(this string filePath, Type bitsType, int channels, int samplingRate, int frameCount)
            => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(this byte[] dest, Type bitsType, int channels, int samplingRate, int frameCount)
            => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this Stream dest, Type bitsType, int channels, int samplingRate, int frameCount)
            => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this BinaryWriter dest, Type bitsType, int channels, int samplingRate, int frameCount)
            => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest);

        public static void WriteWavHeader(this string filePath, SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(this byte[] dest, SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this Stream dest, SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this BinaryWriter dest, SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest);

        public static void WriteWavHeader(this string filePath, SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount)
            => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(this byte[] dest, SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount)
            => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this Stream dest, SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount)
            => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
        public static void WriteWavHeader(this BinaryWriter dest, SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount)
            => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest);
    }
}