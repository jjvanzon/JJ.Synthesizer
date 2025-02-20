using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
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
    public class WavWishes
    {
        public static AudioInfoWish ToInfo(SynthWishes entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(FlowNode entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        internal static AudioInfoWish ToInfo(ConfigResolver entity, SynthWishes synthWishes) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(synthWishes)
        };

        internal static AudioInfoWish ToInfo(ConfigSection entity) => new AudioInfoWish
        {
            Bits         = entity.Bits        ().CoalesceBits        (),
            Channels     = entity.Channels    ().CoalesceChannels    (),
            SamplingRate = entity.SamplingRate().CoalesceSamplingRate(),
            FrameCount   = entity.FrameCount  ().CoalesceFrameCount  ()
        };

        public static AudioInfoWish ToInfo(Tape entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(TapeConfig entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(TapeActions entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(TapeAction entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(Buff entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToInfo(Buff entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };                
                
        public static AudioInfoWish ToInfo(AudioFileOutput entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToInfo(AudioFileOutput entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };
        
        public static AudioInfoWish ToInfo(Sample entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToInfo(AudioFileInfo info) => new AudioInfoWish
        {
            Bits         = info.Bits(),
            Channels     = info.Channels(),
            FrameCount   = info.FrameCount(),
            SamplingRate = info.SamplingRate()
        };

        public static AudioInfoWish ToInfo(WavHeaderStruct wavHeader)
        {
            var info = WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader);
            return info.ToInfo();
        }
        
        public static AudioInfoWish ToInfo(int bits, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bits        .AssertBits        (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToInfo<TBits>(int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = TypeToBits<TBits>(),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToInfo(Type bitsType, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsType    .ToBits            (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToInfo(SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEnum    .ToBits            (),
            Channels     = channelsEnum.ToChannels        (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToInfo(SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEntity    .ToBits            (),
            Channels     = channelsEntity.ToChannels        (),
            SamplingRate = samplingRate  .AssertSamplingRate(),
            FrameCount   = frameCount    .AssertFrameCount  ()
        };

        public static AudioInfoWish ToInfo((int bits, int channels, int samplingRate, int frameCount) x) 
            => ToInfo(x.bits, x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToInfo<TBits>((int channels, int samplingRate, int frameCount) x) 
            => ToInfo<TBits>(x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToInfo((Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => ToInfo(x.bitsType, x.channels, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToInfo((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => ToInfo(x.bitsEnum, x.channelsEnum, x.samplingRate, x.frameCount);
        public static AudioInfoWish ToInfo((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x)
            => ToInfo(x.bitsEntity, x.channelsEntity, x.samplingRate, x.frameCount);

        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, SynthWishes entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static SynthWishes ApplyInfo(SynthWishes entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
        
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, FlowNode entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static FlowNode ApplyInfo(FlowNode entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
        
        internal static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, ConfigResolver entity, SynthWishes synthWishes) 
            { entity.ApplyInfo(infoWish, synthWishes); return infoWish; }
        internal static ConfigResolver ApplyInfo(ConfigResolver entity, AudioInfoWish infoWish, SynthWishes synthWishes)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, synthWishes);
            return entity;
        }
                
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, Tape entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static Tape ApplyInfo(Tape entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
                
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, TapeConfig entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static TapeConfig ApplyInfo(TapeConfig entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
                
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, TapeActions entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static TapeActions ApplyInfo(TapeActions entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
                
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, TapeAction entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static TapeAction ApplyInfo(TapeAction entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }
                        
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, Buff entity, int courtesyFrames, IContext context) 
            { entity.ApplyInfo(infoWish, courtesyFrames, context); return infoWish; }
        public static Buff ApplyInfo(Buff entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
            return entity;
        }
                        
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, AudioFileOutput entity, int courtesyFrames, IContext context) 
            { entity.ApplyInfo(infoWish, courtesyFrames, context); return infoWish; }
        public static AudioFileOutput ApplyInfo(AudioFileOutput entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
            return entity;
        }
                        
        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, Sample entity, IContext context) 
            { entity.ApplyInfo(infoWish, context); return infoWish; }
        public static Sample ApplyInfo(Sample entity, AudioInfoWish infoWish, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            // TODO: FrameCount not settable, but this might be the one time that the byte buffer should be scalable.
            return entity;
        }

        public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, AudioFileInfo entity) { entity.ApplyInfo(infoWish); return infoWish; }
        public static AudioFileInfo ApplyInfo(AudioFileInfo entity, AudioInfoWish infoWish) 
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
            return entity;
        }

        public static AudioInfoWish ApplyTo(AudioInfoWish source, AudioInfoWish dest) { dest.ApplyInfo(source); return source; }
        public static AudioInfoWish ApplyInfo(AudioInfoWish dest, AudioInfoWish source) 
        {
            if (source == null) throw new NullException(() => source);
            dest.SetBits        (source.Bits        );
            dest.SetChannels    (source.Channels    );
            dest.SetSamplingRate(source.SamplingRate);
            dest.SetFrameCount  (source.FrameCount  );
            return dest;
        }

        public static AudioFileInfo ApplyInfo(AudioInfoWish wish) 
        {
            var dest = new AudioFileInfo();
            dest.ApplyInfo(wish);
            return dest;
        }

        public   static WavHeaderStruct ToWavHeader(SynthWishes     entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(FlowNode        entity)                          => entity.ToInfo()              .ToWavHeader();
        internal static WavHeaderStruct ToWavHeader(ConfigResolver  entity, SynthWishes synthWishes) => entity.ToInfo(synthWishes)   .ToWavHeader();
        internal static WavHeaderStruct ToWavHeader(ConfigSection   entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Tape            entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeConfig      entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeActions     entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeAction      entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Buff            entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Buff            entity, int courtesyFrames)      => entity.ToInfo(courtesyFrames).ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioFileOutput entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioFileOutput entity, int courtesyFrames)      => entity.ToInfo(courtesyFrames).ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Sample          entity)                          => entity.ToInfo()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioInfoWish   entity)                          => WavHeaderManager.CreateWavHeaderStruct(entity.ApplyInfo());
        public   static WavHeaderStruct ToWavHeader(AudioFileInfo   entity)                          => entity.ToInfo()              .ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader((int bits, int channels, int samplingRate, int frameCount) x) 
            => x.ToInfo().ToWavHeader();
        public static WavHeaderStruct ToWavHeader<TBits>((int channels, int samplingRate, int frameCount) x) 
            => x.ToInfo<TBits>().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => x.ToInfo().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => x.ToInfo().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => x.ToInfo().ToWavHeader();

        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, SynthWishes entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static SynthWishes ApplyWavHeader(SynthWishes entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, FlowNode entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static FlowNode ApplyWavHeader(FlowNode entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        internal static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, ConfigResolver entity, SynthWishes synthWishes) 
            { entity.ApplyWavHeader(wavHeader, synthWishes); return wavHeader; }
        internal static ConfigResolver ApplyWavHeader(ConfigResolver entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            { wavHeader.ToInfo().ApplyInfo(entity, synthWishes); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, Tape entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static Tape ApplyWavHeader(Tape entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, TapeConfig entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static TapeConfig ApplyWavHeader(TapeConfig entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, TapeActions entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static TapeActions ApplyWavHeader(TapeActions entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, TapeAction entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static TapeAction ApplyWavHeader(TapeAction entity, WavHeaderStruct wavHeader) { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, Buff entity, int courtesyFrames, IContext context) 
            { entity.ApplyWavHeader(wavHeader, courtesyFrames, context); return wavHeader; }
        public static Buff ApplyWavHeader(Buff entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            { wavHeader.ToInfo().ApplyInfo(entity, courtesyFrames, context); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, AudioFileOutput entity, int courtesyFrames, IContext context) 
            { entity.ApplyWavHeader(wavHeader, courtesyFrames, context); return wavHeader; }
        public static AudioFileOutput ApplyWavHeader(AudioFileOutput entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            { wavHeader.ToInfo().ApplyInfo(entity, courtesyFrames, context); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, Sample entity, IContext context) 
            { entity.ApplyWavHeader(wavHeader, context); return wavHeader; }
        public static Sample ApplyWavHeader(Sample entity, WavHeaderStruct wavHeader, IContext context) 
            { wavHeader.ToInfo().ApplyInfo(entity, context); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, AudioFileInfo entity)
            { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static AudioFileInfo ApplyWavHeader(AudioFileInfo entity, WavHeaderStruct wavHeader) 
            { wavHeader.ToInfo().ApplyInfo(entity); return entity; }
        
        public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wavHeader, AudioInfoWish entity) { entity.ApplyWavHeader(wavHeader); return wavHeader; }
        public static AudioInfoWish ApplyWavHeader(AudioInfoWish entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToInfo().ApplyTo(entity);

        public static SynthWishes  ReadWavHeader(SynthWishes  entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static SynthWishes  ReadWavHeader(SynthWishes  entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static SynthWishes  ReadWavHeader(SynthWishes  entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static SynthWishes  ReadWavHeader(SynthWishes  entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, SynthWishes  entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   SynthWishes  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   SynthWishes  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   SynthWishes  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static FlowNode     ReadWavHeader(FlowNode     entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static FlowNode     ReadWavHeader(FlowNode     entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static FlowNode     ReadWavHeader(FlowNode     entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static FlowNode     ReadWavHeader(FlowNode     entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, FlowNode     entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   FlowNode     entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   FlowNode     entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   FlowNode     entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(ConfigResolver entity, string filePath, SynthWishes synthWishes)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return entity; }
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return entity; }
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(ConfigResolver entity, Stream source, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return entity; }
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(ConfigResolver entity, BinaryReader source, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return entity; }
        [UsedImplicitly] internal static string ReadWavHeader(string filePath, ConfigResolver entity, SynthWishes synthWishes)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return filePath; }
        [UsedImplicitly] internal static byte[] ReadWavHeader(byte[] source, ConfigResolver entity, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return source; }
        [UsedImplicitly] internal static Stream ReadWavHeader(Stream source, ConfigResolver entity, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return source; }
        [UsedImplicitly] internal static BinaryReader ReadWavHeader(BinaryReader source, ConfigResolver entity, SynthWishes synthWishes)
            { source.ReadWavHeader().ApplyWavHeader(entity, synthWishes); return source; }

        public static Tape         ReadWavHeader(Tape         entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static Tape         ReadWavHeader(Tape         entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static Tape         ReadWavHeader(Tape         entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static Tape         ReadWavHeader(Tape         entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, Tape         entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   Tape         entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   Tape         entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   Tape         entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static TapeConfig   ReadWavHeader(TapeConfig   entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeConfig   ReadWavHeader(TapeConfig   entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeConfig   ReadWavHeader(TapeConfig   entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeConfig   ReadWavHeader(TapeConfig   entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, TapeConfig   entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   TapeConfig   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   TapeConfig   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   TapeConfig   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static TapeActions  ReadWavHeader(TapeActions  entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeActions  ReadWavHeader(TapeActions  entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeActions  ReadWavHeader(TapeActions  entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeActions  ReadWavHeader(TapeActions  entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, TapeActions  entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   TapeActions  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   TapeActions  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   TapeActions  entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static TapeAction   ReadWavHeader(TapeAction   entity,   string       filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeAction   ReadWavHeader(TapeAction   entity,   byte[]       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeAction   ReadWavHeader(TapeAction   entity,   Stream       source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static TapeAction   ReadWavHeader(TapeAction   entity,   BinaryReader source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string       ReadWavHeader(string       filePath, TapeAction   entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]       ReadWavHeader(byte[]       source,   TapeAction   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream       ReadWavHeader(Stream       source,   TapeAction   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader ReadWavHeader(BinaryReader source,   TapeAction   entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
                
        public static Buff ReadWavHeader(Buff entity, string filePath, int courtesyFrames, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static Buff ReadWavHeader(Buff entity, byte[] source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static Buff ReadWavHeader(Buff entity, Stream source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context); return entity; }
        public static Buff ReadWavHeader(Buff entity, BinaryReader source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static string ReadWavHeader(string filePath, Buff entity, int courtesyFrames, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return filePath; }
        public static byte[] ReadWavHeader(byte[] source, Buff entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return source; }
        public static Stream ReadWavHeader(Stream source, Buff entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context); return source; }
        public static BinaryReader ReadWavHeader(BinaryReader source, Buff entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return source; }
                
        public static AudioFileOutput ReadWavHeader(AudioFileOutput entity, string filePath, int courtesyFrames, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static AudioFileOutput ReadWavHeader(AudioFileOutput entity, byte[] source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static AudioFileOutput ReadWavHeader(AudioFileOutput entity, Stream source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context); return entity; }
        public static AudioFileOutput ReadWavHeader(AudioFileOutput entity, BinaryReader source, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return entity; }
        public static string ReadWavHeader(string filePath, AudioFileOutput entity, int courtesyFrames, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return filePath; }
        public static byte[] ReadWavHeader(byte[] source, AudioFileOutput entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return source; }
        public static Stream ReadWavHeader(Stream source, AudioFileOutput entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context); return source; }
        public static BinaryReader ReadWavHeader(BinaryReader source, AudioFileOutput entity, int courtesyFrames, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context); return source; }
                
        public static Sample ReadWavHeader(Sample entity, string filePath, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, context); return entity; }
        public static Sample ReadWavHeader(Sample entity, byte[] source, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return entity; }
        public static Sample ReadWavHeader(Sample entity, Stream source, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return entity; }
        public static Sample ReadWavHeader(Sample entity, BinaryReader source, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return entity; }
        public static string ReadWavHeader(string filePath, Sample entity, IContext context)
            { filePath.ReadWavHeader().ApplyWavHeader(entity, context); return filePath; }
        public static byte[] ReadWavHeader(byte[] source, Sample entity, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return source; }
        public static Stream ReadWavHeader(Stream source, Sample entity, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return source; }
        public static BinaryReader ReadWavHeader(BinaryReader source, Sample entity, IContext context)
            { source.ReadWavHeader().ApplyWavHeader(entity, context); return source; }

        
        public static AudioFileInfo ReadWavHeader(AudioFileInfo entity,   string        filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioFileInfo ReadWavHeader(AudioFileInfo entity,   byte[]        source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioFileInfo ReadWavHeader(AudioFileInfo entity,   Stream        source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioFileInfo ReadWavHeader(AudioFileInfo entity,   BinaryReader  source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string        ReadWavHeader(string        filePath, AudioFileInfo entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]        ReadWavHeader(byte[]        source,   AudioFileInfo entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream        ReadWavHeader(Stream        source,   AudioFileInfo entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader  ReadWavHeader(BinaryReader  source,   AudioFileInfo entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
                
        public static AudioInfoWish ReadWavHeader(AudioInfoWish entity,   string        filePath) { filePath.ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioInfoWish ReadWavHeader(AudioInfoWish entity,   byte[]        source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioInfoWish ReadWavHeader(AudioInfoWish entity,   Stream        source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static AudioInfoWish ReadWavHeader(AudioInfoWish entity,   BinaryReader  source  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return entity;   }
        public static string        ReadWavHeader(string        filePath, AudioInfoWish entity  ) { filePath.ReadWavHeader().ApplyWavHeader(entity); return filePath; }
        public static byte[]        ReadWavHeader(byte[]        source,   AudioInfoWish entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static Stream        ReadWavHeader(Stream        source,   AudioInfoWish entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }
        public static BinaryReader  ReadWavHeader(BinaryReader  source,   AudioInfoWish entity  ) { source  .ReadWavHeader().ApplyWavHeader(entity); return source;   }

        public static WavHeaderStruct ReadWavHeader(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) return ReadWavHeader(fileStream);
        }
        
        public static WavHeaderStruct ReadWavHeader(byte[]       bytes)  => new MemoryStream(bytes).ReadWavHeader();
        public static WavHeaderStruct ReadWavHeader(Stream       stream) => new BinaryReader(stream).ReadWavHeader();
        public static WavHeaderStruct ReadWavHeader(BinaryReader reader) => reader.ReadStruct<WavHeaderStruct>();

        public static AudioInfoWish ReadAudioInfo(string       filePath) => filePath.ReadWavHeader().ToInfo();
        public static AudioInfoWish ReadAudioInfo(byte[]       source  ) => source  .ReadWavHeader().ToInfo();
        public static AudioInfoWish ReadAudioInfo(Stream       source  ) => source  .ReadWavHeader().ToInfo();
        public static AudioInfoWish ReadAudioInfo(BinaryReader source)   => source  .ReadWavHeader().ToInfo();

        public static SynthWishes  WriteWavHeader(SynthWishes  entity,   string       filePath) { entity.ToWavHeader().Write(filePath); return entity;   }
        public static SynthWishes  WriteWavHeader(SynthWishes  entity,   byte[]       dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static SynthWishes  WriteWavHeader(SynthWishes  entity,   BinaryWriter dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static SynthWishes  WriteWavHeader(SynthWishes  entity,   Stream       dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static string       WriteWavHeader(string       filePath, SynthWishes  entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,     SynthWishes  entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest,     SynthWishes  entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,     SynthWishes  entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        public static FlowNode     WriteWavHeader(FlowNode     entity,   string       filePath) { entity.ToWavHeader().Write(filePath); return entity;   }
        public static FlowNode     WriteWavHeader(FlowNode     entity,   byte[]       dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static FlowNode     WriteWavHeader(FlowNode     entity,   BinaryWriter dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static FlowNode     WriteWavHeader(FlowNode     entity,   Stream       dest    ) { entity.ToWavHeader().Write(dest    ); return entity;   }
        public static string       WriteWavHeader(string       filePath, FlowNode     entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,     FlowNode     entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest,     FlowNode     entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,     FlowNode     entity  ) { entity.ToWavHeader().Write(dest    ); return dest;     }
        [UsedImplicitly] internal static ConfigResolver  WriteWavHeader(ConfigResolver entity,   string         filePath, SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(filePath); return entity;   }
        [UsedImplicitly] internal static ConfigResolver  WriteWavHeader(ConfigResolver entity,   byte[]         dest    , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return entity;   }
        [UsedImplicitly] internal static ConfigResolver  WriteWavHeader(ConfigResolver entity,   BinaryWriter   dest    , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return entity;   }
        [UsedImplicitly] internal static ConfigResolver  WriteWavHeader(ConfigResolver entity,   Stream         dest    , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return entity;   }
        [UsedImplicitly] internal static string          WriteWavHeader(string         filePath, ConfigResolver entity  , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(filePath); return filePath; }
        [UsedImplicitly] internal static byte[]          WriteWavHeader(byte[]         dest,     ConfigResolver entity  , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return dest; }
        [UsedImplicitly] internal static Stream          WriteWavHeader(Stream         dest,     ConfigResolver entity  , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return dest; }
        [UsedImplicitly] internal static BinaryWriter    WriteWavHeader(BinaryWriter   dest,     ConfigResolver entity  , SynthWishes synthWishes) { entity.ToWavHeader(synthWishes).Write(dest    ); return dest; }
        [UsedImplicitly] internal static ConfigSection   WriteWavHeader(ConfigSection  entity,   string         filePath) { entity.ToWavHeader().Write(filePath); return  entity;   }
        [UsedImplicitly] internal static ConfigSection   WriteWavHeader(ConfigSection  entity,   byte[]         dest    ) { entity.ToWavHeader().Write(dest    ); return  entity;   }
        [UsedImplicitly] internal static ConfigSection   WriteWavHeader(ConfigSection  entity,   BinaryWriter   dest    ) { entity.ToWavHeader().Write(dest    ); return  entity;   }
        [UsedImplicitly] internal static ConfigSection   WriteWavHeader(ConfigSection  entity,   Stream         dest    ) { entity.ToWavHeader().Write(dest    ); return  entity;   }
        [UsedImplicitly] internal static string          WriteWavHeader(string         filePath, ConfigSection  entity  ) { entity.ToWavHeader().Write(filePath); return  filePath; }
        [UsedImplicitly] internal static byte[]          WriteWavHeader(byte[]         dest,     ConfigSection  entity  ) { entity.ToWavHeader().Write(dest    ); return  dest;     }
        [UsedImplicitly] internal static Stream          WriteWavHeader(Stream         dest,     ConfigSection  entity  ) { entity.ToWavHeader().Write(dest    ); return  dest;     }
        [UsedImplicitly] internal static BinaryWriter    WriteWavHeader(BinaryWriter   dest,     ConfigSection  entity  ) { entity.ToWavHeader().Write(dest    ); return  dest;     }
        public static Tape            WriteWavHeader(Tape            entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static Tape            WriteWavHeader(Tape            entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Tape            WriteWavHeader(Tape            entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Tape            WriteWavHeader(Tape            entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  Tape            entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      Tape            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      Tape            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      Tape            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static TapeConfig      WriteWavHeader(TapeConfig      entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static TapeConfig      WriteWavHeader(TapeConfig      entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeConfig      WriteWavHeader(TapeConfig      entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeConfig      WriteWavHeader(TapeConfig      entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  TapeConfig      entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      TapeConfig      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      TapeConfig      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      TapeConfig      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static TapeActions     WriteWavHeader(TapeActions     entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static TapeActions     WriteWavHeader(TapeActions     entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeActions     WriteWavHeader(TapeActions     entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeActions     WriteWavHeader(TapeActions     entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  TapeActions     entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      TapeActions     entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      TapeActions     entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      TapeActions     entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static TapeAction      WriteWavHeader(TapeAction      entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static TapeAction      WriteWavHeader(TapeAction      entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeAction      WriteWavHeader(TapeAction      entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static TapeAction      WriteWavHeader(TapeAction      entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  TapeAction      entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      TapeAction      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      TapeAction      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      TapeAction      entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Buff            WriteWavHeader(Buff            entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  Buff            entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      Buff            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      Buff            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      Buff            entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    string          filePath) { entity.ToWavHeader().Write(filePath); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    byte[]          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    BinaryWriter    dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    Stream          dest    ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  AudioFileOutput entity  ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      AudioFileOutput entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      AudioFileOutput entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      AudioFileOutput entity  ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Buff            WriteWavHeader(Buff            entity,    string          filePath, int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(filePath); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    byte[]          dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    BinaryWriter    dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    Stream          dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  Buff            entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    string          filePath, int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(filePath); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    Stream          dest,     int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader(courtesyFrames).Write(dest    ); return dest; }
        public static Sample          WriteWavHeader(Sample          entity,    string          filePath ) { entity.ToWavHeader().Write(filePath); return entity; }
        public static Sample          WriteWavHeader(Sample          entity,    byte[]          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Sample          WriteWavHeader(Sample          entity,    BinaryWriter    dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Sample          WriteWavHeader(Sample          entity,    Stream          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  Sample          entity   ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      Sample          entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      Sample          entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      Sample          entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static AudioInfoWish   WriteWavHeader(AudioInfoWish   entity,    string          filePath ) { entity.ToWavHeader().Write(filePath); return entity; }
        public static AudioInfoWish   WriteWavHeader(AudioInfoWish   entity,    byte[]          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioInfoWish   WriteWavHeader(AudioInfoWish   entity,    Stream          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioInfoWish   WriteWavHeader(AudioInfoWish   entity,    BinaryWriter    dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  AudioInfoWish   entity   ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      AudioInfoWish   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      AudioInfoWish   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      AudioInfoWish   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static AudioFileInfo   WriteWavHeader(AudioFileInfo   entity,    string          filePath ) { entity.ToWavHeader().Write(filePath); return entity; }
        public static AudioFileInfo   WriteWavHeader(AudioFileInfo   entity,    byte[]          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileInfo   WriteWavHeader(AudioFileInfo   entity,    Stream          dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileInfo   WriteWavHeader(AudioFileInfo   entity,    BinaryWriter    dest     ) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  AudioFileInfo   entity   ) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      AudioFileInfo   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      AudioFileInfo   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      AudioFileInfo   entity   ) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static WavHeaderStruct WriteWavHeader(WavHeaderStruct wavHeader, BinaryWriter    dest     ) { wavHeader           .Write(dest    ); return wavHeader; }
        public static WavHeaderStruct WriteWavHeader(WavHeaderStruct wavHeader, Stream          dest     ) { wavHeader           .Write(dest    ); return wavHeader; }
        public static WavHeaderStruct WriteWavHeader(WavHeaderStruct wavHeader, byte[]          dest     ) { wavHeader           .Write(dest    ); return wavHeader; }
        public static WavHeaderStruct WriteWavHeader(WavHeaderStruct wavHeader, string          filePath ) { wavHeader           .Write(filePath); return wavHeader; }
        public static string          WriteWavHeader(string          filePath,  WavHeaderStruct wavHeader) { wavHeader           .Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      WavHeaderStruct wavHeader) { wavHeader           .Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      WavHeaderStruct wavHeader) { wavHeader           .Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      WavHeaderStruct wavHeader) { wavHeader           .Write(dest    ); return dest; }
        public static WavHeaderStruct Write(WavHeaderStruct wavHeader, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)) fileStream.Write(wavHeader);
            return wavHeader;
        }
        public static WavHeaderStruct Write(WavHeaderStruct wavHeader, byte[] dest)
        {
            new MemoryStream(dest).Write(wavHeader);
            return wavHeader;
        }
        public static WavHeaderStruct Write(WavHeaderStruct wavHeader, Stream dest)
        {
            new BinaryWriter(dest).Write(wavHeader);
            return wavHeader;
        }
        public static WavHeaderStruct Write(WavHeaderStruct wavHeader, BinaryWriter dest)
        {
            dest.WriteStruct(wavHeader);
            return wavHeader;
        }
        public static string       Write(string       filePath,  WavHeaderStruct wavHeader) { wavHeader.Write(filePath); return filePath; }
        public static byte[]       Write(byte[]       dest,      WavHeaderStruct wavHeader) { wavHeader.Write(dest    ); return dest; }
        public static Stream       Write(Stream       dest,      WavHeaderStruct wavHeader) { wavHeader.Write(dest    ); return dest; }
        public static BinaryWriter Write(BinaryWriter dest,      WavHeaderStruct wavHeader) { wavHeader.Write(dest    ); return dest; }

        // With Values

        public static string       WriteWavHeader(string   filePath,  int bits, int channels, int samplingRate, int frameCount)    { (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,  int bits, int channels, int samplingRate, int frameCount)    { (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static Stream       WriteWavHeader(Stream       dest,  int bits, int channels, int samplingRate, int frameCount)    { (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,  int bits, int channels, int samplingRate, int frameCount)    { (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static string       WriteWavHeader(string   filePath, (int bits, int channels, int samplingRate, int frameCount) x) { x                                         .ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest, (int bits, int channels, int samplingRate, int frameCount) x) { x                                         .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static Stream       WriteWavHeader(Stream       dest, (int bits, int channels, int samplingRate, int frameCount) x) { x                                         .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest, (int bits, int channels, int samplingRate, int frameCount) x) { x                                         .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    string   filePath) => (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath);
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    byte[]       dest) => (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    Stream       dest) => (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (bits, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, string   filePath) { x.ToInfo().WriteWavHeader(filePath); return x; }
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, byte[]       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, Stream       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }

        public static string       WriteWavHeader<TBits>(string   filePath,  int channels, int samplingRate, int frameCount   ) { (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(filePath); return filePath; }
        public static string       WriteWavHeader<TBits>(string   filePath, (int channels, int samplingRate, int frameCount) x) {  x                                  .ToInfo<TBits>().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader<TBits>(byte[]       dest,  int channels, int samplingRate, int frameCount   ) { (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static byte[]       WriteWavHeader<TBits>(byte[]       dest, (int channels, int samplingRate, int frameCount) x) {  x                                  .ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader<TBits>(Stream       dest,  int channels, int samplingRate, int frameCount   ) { (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader<TBits>(Stream       dest, (int channels, int samplingRate, int frameCount) x) { x                                   .ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader<TBits>(BinaryWriter dest,  int channels, int samplingRate, int frameCount   ) { (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader<TBits>(BinaryWriter dest, (int channels, int samplingRate, int frameCount) x) { x                                   .ToInfo<TBits>().WriteWavHeader(dest    ); return dest;     }
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    string   filePath) => (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    byte[]       dest) => (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    Stream       dest) => (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (channels, samplingRate, frameCount).ToInfo<TBits>().WriteWavHeader(dest    );
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, string   filePath) { x.ToInfo<TBits>().WriteWavHeader(filePath); return x; }
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, byte[]       dest) { x.ToInfo<TBits>().WriteWavHeader(dest    ); return x; }
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, Stream       dest) { x.ToInfo<TBits>().WriteWavHeader(dest    ); return x; }
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, BinaryWriter dest) { x.ToInfo<TBits>().WriteWavHeader(dest    ); return x; }
        
        public static string       WriteWavHeader(string   filePath,  Type bitsType, int channels, int samplingRate, int frameCount   ) { (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) { (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) { (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) { (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static string       WriteWavHeader(string   filePath, (Type bitsType, int channels, int samplingRate, int frameCount) x) { x.ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) { x.ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) { x.ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) { x.ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    string   filePath) => (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath);
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    byte[]       dest) => (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    Stream       dest) => (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsType, channels, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, string   filePath) { x.ToInfo().WriteWavHeader(filePath); return x; }
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, byte[]       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, Stream       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }

        public static string       WriteWavHeader(string   filePath,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) { (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) { (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static Stream       WriteWavHeader(Stream       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) { (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) { (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest; }
        public static string       WriteWavHeader(string   filePath, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) { x                                                 .ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) { x                                                 .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static Stream       WriteWavHeader(Stream       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) { x                                                 .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) { x                                                 .ToInfo().WriteWavHeader(dest    ); return dest; }
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    string   filePath) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath);
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    byte[]       dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    Stream       dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, string   filePath) { x.ToInfo().WriteWavHeader(filePath); return x; }
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, byte[]       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, Stream       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, BinaryWriter dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }

        public static string       WriteWavHeader(string   filePath,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) { (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) { (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) { (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) { (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static string       WriteWavHeader(string   filePath, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) { x                                                     .ToInfo().WriteWavHeader(filePath); return filePath; }
        public static byte[]       WriteWavHeader(byte[]       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) { x                                                     .ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static Stream       WriteWavHeader(Stream       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) { x                                                     .ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static BinaryWriter WriteWavHeader(BinaryWriter dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) { x                                                     .ToInfo().WriteWavHeader(dest    ); return dest;     }
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    string   filePath) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(filePath);
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    byte[]       dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    Stream       dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToInfo().WriteWavHeader(dest    );
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, string   filePath) { x.ToInfo().WriteWavHeader(filePath); return x; }
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, byte[]       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, Stream       dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, BinaryWriter dest) { x.ToInfo().WriteWavHeader(dest    ); return x; }
    }

    public static class ToInfoExtensions
    {
        public static AudioInfoWish ToInfo(this SynthWishes entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this FlowNode entity) => WavWishes.ToInfo(entity);
        internal static AudioInfoWish ToInfo(this ConfigResolver entity, SynthWishes synthWishes) => WavWishes.ToInfo(entity, synthWishes);
        internal static AudioInfoWish ToInfo(this ConfigSection entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this Tape entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this TapeConfig entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this TapeActions entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this TapeAction entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this Buff entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this Buff entity, int courtesyFrames) => WavWishes.ToInfo(entity, courtesyFrames);
        public static AudioInfoWish ToInfo(this AudioFileOutput entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this AudioFileOutput entity, int courtesyFrames) => WavWishes.ToInfo(entity, courtesyFrames);
        public static AudioInfoWish ToInfo(this Sample entity) => WavWishes.ToInfo(entity);
        public static AudioInfoWish ToInfo(this AudioFileInfo info) => WavWishes.ToInfo(info);
        public static AudioInfoWish ToInfo(this WavHeaderStruct wavHeader) => WavWishes.ToInfo(wavHeader);
        public static AudioInfoWish ToInfo(this (int bits, int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToInfo(x);
        public static AudioInfoWish ToInfo<TBits>(this (int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToInfo<TBits>(x);
        public static AudioInfoWish ToInfo(this (Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToInfo(x);
        public static AudioInfoWish ToInfo(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => WavWishes.ToInfo(x);
        public static AudioInfoWish ToInfo(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => WavWishes.ToInfo(x);
    }
    
    public static class ApplyInfoExtensions
    {
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, SynthWishes   entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static SynthWishes   ApplyInfo(this SynthWishes   entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, FlowNode      entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static FlowNode      ApplyInfo(this FlowNode      entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, Tape          entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static Tape          ApplyInfo(this Tape          entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, TapeConfig    entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static TapeConfig    ApplyInfo(this TapeConfig    entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, TapeActions   entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static TapeActions   ApplyInfo(this TapeActions   entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, TapeAction    entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static TapeAction    ApplyInfo(this TapeAction    entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, AudioFileInfo entity  ) => WavWishes.ApplyInfo (infoWish, entity);
        public static AudioFileInfo ApplyInfo(this AudioFileInfo entity,   AudioInfoWish infoWish) => WavWishes.ApplyInfo(entity, infoWish);
        
        internal static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ApplyInfo(infoWish, entity, synthWishes);
        internal static ConfigResolver ApplyInfo(this ConfigResolver entity, AudioInfoWish infoWish, SynthWishes synthWishes)
            => WavWishes.ApplyInfo(entity, infoWish, synthWishes);
        
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyInfo(infoWish, entity, courtesyFrames, context);
        public static Buff ApplyInfo(this Buff entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
            => WavWishes.ApplyInfo(entity, infoWish, courtesyFrames, context);
        
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyInfo(infoWish, entity, courtesyFrames, context);
        public static AudioFileOutput ApplyInfo(this AudioFileOutput entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
            => WavWishes.ApplyInfo(entity, infoWish, courtesyFrames, context);
        
        public static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, Sample entity, IContext context)
            => WavWishes.ApplyInfo(infoWish, entity, context);
        public static Sample ApplyInfo(this Sample entity, AudioInfoWish infoWish, IContext context)
            => WavWishes.ApplyInfo(entity, infoWish, context);
        
        public static AudioInfoWish ApplyTo  (this AudioInfoWish source, AudioInfoWish dest)   => WavWishes.ApplyTo(source, dest);
        public static AudioInfoWish ApplyInfo(this AudioInfoWish dest,   AudioInfoWish source) => WavWishes.ApplyInfo(dest, source);
        public static AudioFileInfo ApplyInfo(this AudioInfoWish wish) => WavWishes.ApplyInfo(wish);
    }
    
    public static class ToWavHeaderExtensions
    {
        public   static WavHeaderStruct ToWavHeader(this SynthWishes     entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this FlowNode        entity)                          => WavWishes.ToWavHeader(entity);
        internal static WavHeaderStruct ToWavHeader(this ConfigResolver  entity, SynthWishes synthWishes) => WavWishes.ToWavHeader(entity, synthWishes);
        internal static WavHeaderStruct ToWavHeader(this ConfigSection   entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this Tape            entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this TapeConfig      entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this TapeActions     entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this TapeAction      entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this Buff            entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this Buff            entity, int courtesyFrames)      => WavWishes.ToWavHeader(entity, courtesyFrames);
        public   static WavHeaderStruct ToWavHeader(this AudioFileOutput entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this AudioFileOutput entity, int courtesyFrames)      => WavWishes.ToWavHeader(entity, courtesyFrames);
        public   static WavHeaderStruct ToWavHeader(this Sample          entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this AudioInfoWish   entity)                          => WavWishes.ToWavHeader(entity);
        public   static WavHeaderStruct ToWavHeader(this AudioFileInfo   entity)                          => WavWishes.ToWavHeader(entity);
        
        public static WavHeaderStruct ToWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x)
            => WavWishes.ToWavHeader(x);
        public static WavHeaderStruct ToWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToWavHeader<TBits>(x);
        public static WavHeaderStruct ToWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToWavHeader(x);
        public static WavHeaderStruct ToWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => WavWishes.ToWavHeader(x);
        public static WavHeaderStruct ToWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => WavWishes.ToWavHeader(x);
    }
    
    public static class ApplyWavHeaderExtensions
    { 
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, SynthWishes entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static SynthWishes ApplyWavHeader(this SynthWishes entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);

        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, FlowNode entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static FlowNode ApplyWavHeader(this FlowNode entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        internal static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ApplyWavHeader(wavHeader, entity, synthWishes);
        internal static ConfigResolver ApplyWavHeader(this ConfigResolver entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => WavWishes.ApplyWavHeader(entity, wavHeader, synthWishes);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, Tape entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static Tape ApplyWavHeader(this Tape entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, TapeConfig entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static TapeConfig ApplyWavHeader(this TapeConfig entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, TapeActions entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static TapeActions ApplyWavHeader(this TapeActions entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, TapeAction entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static TapeAction ApplyWavHeader(this TapeAction entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, courtesyFrames, context);
        public static Buff ApplyWavHeader(this Buff entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, courtesyFrames, context);

        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, courtesyFrames, context);
        public static AudioFileOutput ApplyWavHeader(this AudioFileOutput entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, courtesyFrames, context);

        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, Sample entity, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, context);
        public static Sample ApplyWavHeader(this Sample entity, WavHeaderStruct wavHeader, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, context);

        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, AudioFileInfo entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static AudioFileInfo ApplyWavHeader(this AudioFileInfo entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, AudioInfoWish entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static AudioInfoWish ApplyWavHeader(this AudioInfoWish entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
    }
    
    public static class ReadWavHeaderExtensions
    {
        public static SynthWishes  ReadWavHeader(this SynthWishes  entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static SynthWishes  ReadWavHeader(this SynthWishes  entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static SynthWishes  ReadWavHeader(this SynthWishes  entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static SynthWishes  ReadWavHeader(this SynthWishes  entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, SynthWishes  entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static FlowNode     ReadWavHeader(this FlowNode     entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static FlowNode     ReadWavHeader(this FlowNode     entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static FlowNode     ReadWavHeader(this FlowNode     entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static FlowNode     ReadWavHeader(this FlowNode     entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, FlowNode     entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(this ConfigResolver entity, string filePath, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, filePath, synthWishes);
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(this ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(this ConfigResolver entity, Stream source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        [UsedImplicitly] internal static ConfigResolver ReadWavHeader(this ConfigResolver entity, BinaryReader source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        [UsedImplicitly] internal static string ReadWavHeader(this string filePath, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(filePath, entity, synthWishes);
        [UsedImplicitly] internal static byte[] ReadWavHeader(this byte[] source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        [UsedImplicitly] internal static Stream ReadWavHeader(this Stream source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        [UsedImplicitly] internal static BinaryReader ReadWavHeader(this BinaryReader source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        
        public static Tape         ReadWavHeader(this Tape         entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static Tape         ReadWavHeader(this Tape         entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static Tape         ReadWavHeader(this Tape         entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static Tape         ReadWavHeader(this Tape         entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, Tape         entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static TapeConfig   ReadWavHeader(this TapeConfig   entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static TapeConfig   ReadWavHeader(this TapeConfig   entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeConfig   ReadWavHeader(this TapeConfig   entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeConfig   ReadWavHeader(this TapeConfig   entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, TapeConfig   entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static TapeActions  ReadWavHeader(this TapeActions  entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static TapeActions  ReadWavHeader(this TapeActions  entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeActions  ReadWavHeader(this TapeActions  entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeActions  ReadWavHeader(this TapeActions  entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, TapeActions  entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static TapeAction   ReadWavHeader(this TapeAction   entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static TapeAction   ReadWavHeader(this TapeAction   entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeAction   ReadWavHeader(this TapeAction   entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static TapeAction   ReadWavHeader(this TapeAction   entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string       ReadWavHeader(this string       filePath, TapeAction   entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]       ReadWavHeader(this byte[]       source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream       ReadWavHeader(this Stream       source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader ReadWavHeader(this BinaryReader source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
        public static Buff ReadWavHeader(this Buff entity, string filePath, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, filePath, courtesyFrames, context);
        public static Buff ReadWavHeader(this Buff entity, byte[] source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static Buff ReadWavHeader(this Buff entity, Stream source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static Buff ReadWavHeader(this Buff entity, BinaryReader source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static string ReadWavHeader(this string filePath, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(filePath, entity, courtesyFrames, context);
        public static byte[] ReadWavHeader(this byte[] source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static Stream ReadWavHeader(this Stream source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static BinaryReader ReadWavHeader(this BinaryReader source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        
        public static AudioFileOutput ReadWavHeader(this AudioFileOutput entity, string filePath, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, filePath, courtesyFrames, context);
        public static AudioFileOutput ReadWavHeader(this AudioFileOutput entity, byte[] source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static AudioFileOutput ReadWavHeader(this AudioFileOutput entity, Stream source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static AudioFileOutput ReadWavHeader(this AudioFileOutput entity, BinaryReader source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static string ReadWavHeader(this string filePath, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(filePath, entity, courtesyFrames, context);
        public static byte[] ReadWavHeader(this byte[] source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static Stream ReadWavHeader(this Stream source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static BinaryReader ReadWavHeader(this BinaryReader source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);

        public static Sample       ReadWavHeader(this Sample       entity,   string filePath,     IContext context) => WavWishes.ReadWavHeader(entity,   filePath, context);
        public static Sample       ReadWavHeader(this Sample       entity,   byte[] source,       IContext context) => WavWishes.ReadWavHeader(entity,   source,   context);
        public static Sample       ReadWavHeader(this Sample       entity,   Stream source,       IContext context) => WavWishes.ReadWavHeader(entity,   source,   context);
        public static Sample       ReadWavHeader(this Sample       entity,   BinaryReader source, IContext context) => WavWishes.ReadWavHeader(entity,   source,   context);
        public static string       ReadWavHeader(this string       filePath, Sample entity,       IContext context) => WavWishes.ReadWavHeader(filePath, entity,   context);
        public static byte[]       ReadWavHeader(this byte[]       source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(source,   entity,   context);
        public static Stream       ReadWavHeader(this Stream       source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(source,   entity,   context);
        public static BinaryReader ReadWavHeader(this BinaryReader source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(source,   entity,   context);
        
        public static AudioInfoWish ReadWavHeader(this AudioInfoWish entity,   string        filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static AudioInfoWish ReadWavHeader(this AudioInfoWish entity,   byte[]        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static AudioInfoWish ReadWavHeader(this AudioInfoWish entity,   Stream        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static AudioInfoWish ReadWavHeader(this AudioInfoWish entity,   BinaryReader  source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string        ReadWavHeader(this string        filePath, AudioInfoWish entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]        ReadWavHeader(this byte[]        source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream        ReadWavHeader(this Stream        source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader  ReadWavHeader(this BinaryReader  source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static AudioFileInfo ReadWavHeader(this AudioFileInfo entity,   string        filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static AudioFileInfo ReadWavHeader(this AudioFileInfo entity,   byte[]        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static AudioFileInfo ReadWavHeader(this AudioFileInfo entity,   Stream        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static AudioFileInfo ReadWavHeader(this AudioFileInfo entity,   BinaryReader  source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static string        ReadWavHeader(this string        filePath, AudioFileInfo entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static byte[]        ReadWavHeader(this byte[]        source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static Stream        ReadWavHeader(this Stream        source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static BinaryReader  ReadWavHeader(this BinaryReader  source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
        public static WavHeaderStruct ReadWavHeader(this string filePath) => WavWishes.ReadWavHeader(filePath);
        public static WavHeaderStruct ReadWavHeader(this byte[] bytes) => WavWishes.ReadWavHeader(bytes);
        public static WavHeaderStruct ReadWavHeader(this Stream stream) => WavWishes.ReadWavHeader(stream);
        public static WavHeaderStruct ReadWavHeader(this BinaryReader reader) => WavWishes.ReadWavHeader(reader);
    }
    
    public static class ReadAudioInfoExtensions
    {
        public static AudioInfoWish ReadAudioInfo(this string       filePath) => WavWishes.ReadAudioInfo(filePath);
        public static AudioInfoWish ReadAudioInfo(this byte[]       source  ) => WavWishes.ReadAudioInfo(source  );
        public static AudioInfoWish ReadAudioInfo(this Stream       source  ) => WavWishes.ReadAudioInfo(source  );
        public static AudioInfoWish ReadAudioInfo(this BinaryReader source)   => WavWishes.ReadAudioInfo(source  );
    }

    public static class WriteWavHeaderExtensions
    {
        public static SynthWishes  WriteWavHeader(this SynthWishes  entity,   string       filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        public static SynthWishes  WriteWavHeader(this SynthWishes  entity,   byte[]       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static SynthWishes  WriteWavHeader(this SynthWishes  entity,   BinaryWriter dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static SynthWishes  WriteWavHeader(this SynthWishes  entity,   Stream       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string       WriteWavHeader(this string       filePath, SynthWishes  entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]       WriteWavHeader(this byte[]       dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream       WriteWavHeader(this Stream       dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static FlowNode     WriteWavHeader(this FlowNode     entity,   string       filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        public static FlowNode     WriteWavHeader(this FlowNode     entity,   byte[]       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static FlowNode     WriteWavHeader(this FlowNode     entity,   BinaryWriter dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static FlowNode     WriteWavHeader(this FlowNode     entity,   Stream       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string       WriteWavHeader(this string       filePath, FlowNode     entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]       WriteWavHeader(this byte[]       dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream       WriteWavHeader(this Stream       dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static ConfigResolver WriteWavHeader(this ConfigResolver entity,   string         filePath, SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   filePath, synthWishes);
        [UsedImplicitly] internal static ConfigResolver WriteWavHeader(this ConfigResolver entity,   byte[]         dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static ConfigResolver WriteWavHeader(this ConfigResolver entity,   BinaryWriter   dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static ConfigResolver WriteWavHeader(this ConfigResolver entity,   Stream         dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static string         WriteWavHeader(this string         filePath, ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(filePath, entity  , synthWishes);
        [UsedImplicitly] internal static byte[]         WriteWavHeader(this byte[]         dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static Stream         WriteWavHeader(this Stream         dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static BinaryWriter   WriteWavHeader(this BinaryWriter   dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static ConfigSection  WriteWavHeader(this ConfigSection  entity,   string         filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        [UsedImplicitly] internal static ConfigSection  WriteWavHeader(this ConfigSection  entity,   byte[]         dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static ConfigSection  WriteWavHeader(this ConfigSection  entity,   BinaryWriter   dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static ConfigSection  WriteWavHeader(this ConfigSection  entity,   Stream         dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static string         WriteWavHeader(this string         filePath, ConfigSection  entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        [UsedImplicitly] internal static byte[]         WriteWavHeader(this byte[]         dest,     ConfigSection  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static Stream         WriteWavHeader(this Stream         dest,     ConfigSection  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static BinaryWriter   WriteWavHeader(this BinaryWriter   dest,     ConfigSection  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Tape            WriteWavHeader(this Tape            entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static Tape            WriteWavHeader(this Tape            entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static Tape            WriteWavHeader(this Tape            entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static Tape            WriteWavHeader(this Tape            entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  Tape            entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static TapeConfig      WriteWavHeader(this TapeConfig      entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static TapeConfig      WriteWavHeader(this TapeConfig      entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeConfig      WriteWavHeader(this TapeConfig      entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeConfig      WriteWavHeader(this TapeConfig      entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  TapeConfig      entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static TapeActions     WriteWavHeader(this TapeActions     entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static TapeActions     WriteWavHeader(this TapeActions     entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeActions     WriteWavHeader(this TapeActions     entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeActions     WriteWavHeader(this TapeActions     entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  TapeActions     entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static TapeAction      WriteWavHeader(this TapeAction      entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static TapeAction      WriteWavHeader(this TapeAction      entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeAction      WriteWavHeader(this TapeAction      entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static TapeAction      WriteWavHeader(this TapeAction      entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  TapeAction      entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Buff            WriteWavHeader(this Buff            entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static Buff            WriteWavHeader(this Buff            entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static Buff            WriteWavHeader(this Buff            entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static Buff            WriteWavHeader(this Buff            entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  Buff            entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static string          WriteWavHeader(this string          filePath,  AudioFileOutput entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static byte[]          WriteWavHeader(this byte[]          dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Stream          WriteWavHeader(this Stream          dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static Buff            WriteWavHeader(this Buff            entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static string          WriteWavHeader(this string          filePath,  Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static byte[]          WriteWavHeader(this byte[]          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static Stream          WriteWavHeader(this Stream          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static string          WriteWavHeader(this string          filePath,  AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static byte[]          WriteWavHeader(this byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static Stream          WriteWavHeader(this Stream          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static Sample          WriteWavHeader(this Sample          entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static Sample          WriteWavHeader(this Sample          entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static Sample          WriteWavHeader(this Sample          entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static Sample          WriteWavHeader(this Sample          entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static string          WriteWavHeader(this string          filePath,  Sample          entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static byte[]          WriteWavHeader(this byte[]          dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static Stream          WriteWavHeader(this Stream          dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static AudioInfoWish   WriteWavHeader(this AudioInfoWish   entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static AudioInfoWish   WriteWavHeader(this AudioInfoWish   entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static AudioInfoWish   WriteWavHeader(this AudioInfoWish   entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static AudioInfoWish   WriteWavHeader(this AudioInfoWish   entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static string          WriteWavHeader(this string          filePath,  AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static byte[]          WriteWavHeader(this byte[]          dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static Stream          WriteWavHeader(this Stream          dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static AudioFileInfo   WriteWavHeader(this AudioFileInfo   entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static AudioFileInfo   WriteWavHeader(this AudioFileInfo   entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static AudioFileInfo   WriteWavHeader(this AudioFileInfo   entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static AudioFileInfo   WriteWavHeader(this AudioFileInfo   entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static string          WriteWavHeader(this string          filePath,  AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static byte[]          WriteWavHeader(this byte[]          dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static Stream          WriteWavHeader(this Stream          dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static WavHeaderStruct WriteWavHeader(this WavHeaderStruct wavHeader, string          filePath ) => WavWishes.WriteWavHeader(wavHeader, filePath );
        public static WavHeaderStruct WriteWavHeader(this WavHeaderStruct wavHeader, byte[]          dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static WavHeaderStruct WriteWavHeader(this WavHeaderStruct wavHeader, Stream          dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static WavHeaderStruct WriteWavHeader(this WavHeaderStruct wavHeader, BinaryWriter    dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static string          WriteWavHeader(this string           filePath, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(filePath,  wavHeader);
        public static byte[]          WriteWavHeader(this byte[]               dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static Stream          WriteWavHeader(this Stream               dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static BinaryWriter    WriteWavHeader(this BinaryWriter         dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static WavHeaderStruct Write         (this WavHeaderStruct wavHeader, string          filePath ) => WavWishes.Write         (wavHeader, filePath );
        public static WavHeaderStruct Write         (this WavHeaderStruct wavHeader, byte[]          dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static WavHeaderStruct Write         (this WavHeaderStruct wavHeader, Stream          dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static WavHeaderStruct Write         (this WavHeaderStruct wavHeader, BinaryWriter    dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static string          Write         (this string           filePath, WavHeaderStruct wavHeader) => WavWishes.Write         (filePath,  wavHeader);
        public static byte[]          Write         (this byte[]               dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        public static Stream          Write         (this Stream               dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        public static BinaryWriter    Write         (this BinaryWriter         dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        
        // With Values
        
        public static string       WriteWavHeader(this string   filePath,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bits, channels, samplingRate, frameCount);
        public static string       WriteWavHeader(this string   filePath, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static byte[]       WriteWavHeader(this byte[]       dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static byte[]       WriteWavHeader(this byte[]       dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static Stream       WriteWavHeader(this Stream       dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static Stream       WriteWavHeader(this Stream       dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (int bits, int channels, int samplingRate, int frameCount) WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );
        

        public static string       WriteWavHeader<TBits>(this string   filePath,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(filePath, channels, samplingRate, frameCount);
        public static string       WriteWavHeader<TBits>(this string   filePath, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(filePath, x);
        public static byte[]       WriteWavHeader<TBits>(this byte[]       dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static byte[]       WriteWavHeader<TBits>(this byte[]       dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static Stream       WriteWavHeader<TBits>(this Stream       dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static Stream       WriteWavHeader<TBits>(this Stream       dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static BinaryWriter WriteWavHeader<TBits>(this BinaryWriter dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static BinaryWriter WriteWavHeader<TBits>(this BinaryWriter dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader<TBits>(x, filePath);
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );
        public static (int channels, int samplingRate, int frameCount) WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );

        public static string       WriteWavHeader(this string   filePath,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsType, channels, samplingRate, frameCount);
        public static string       WriteWavHeader(this string   filePath, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static byte[]       WriteWavHeader(this byte[]       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static byte[]       WriteWavHeader(this byte[]       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static Stream       WriteWavHeader(this Stream       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static Stream       WriteWavHeader(this Stream       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (Type bitsType, int channels, int samplingRate, int frameCount) WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );

        public static string       WriteWavHeader(this string   filePath,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsEnum, channelsEnum, samplingRate, frameCount);
        public static string       WriteWavHeader(this string   filePath, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static byte[]       WriteWavHeader(this byte[]       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static byte[]       WriteWavHeader(this byte[]       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static Stream       WriteWavHeader(this Stream       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static Stream       WriteWavHeader(this Stream       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static BinaryWriter WriteWavHeader(this BinaryWriter dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );

        public static string         WriteWavHeader(this string   filePath,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsEntity, channelsEntity, samplingRate, frameCount);
        public static string         WriteWavHeader(this string   filePath, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static byte[]         WriteWavHeader(this byte[]       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static byte[]         WriteWavHeader(this byte[]       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static Stream         WriteWavHeader(this Stream       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static Stream         WriteWavHeader(this Stream       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static BinaryWriter   WriteWavHeader(this BinaryWriter dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static BinaryWriter   WriteWavHeader(this BinaryWriter dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );
    }
}