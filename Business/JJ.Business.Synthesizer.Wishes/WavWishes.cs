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
        public static AudioInfoWish ToWish(SynthWishes entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(FlowNode entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        internal static AudioInfoWish ToWish(ConfigResolver entity, SynthWishes synthWishes) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(synthWishes)
        };

        internal static AudioInfoWish ToWish(ConfigSection entity) => new AudioInfoWish
        {
            Bits         = entity.Bits        ().CoalesceBits        (),
            Channels     = entity.Channels    ().CoalesceChannels    (),
            SamplingRate = entity.SamplingRate().CoalesceSamplingRate(),
            FrameCount   = entity.FrameCount  ().CoalesceFrameCount  ()
        };

        public static AudioInfoWish ToWish(Tape entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(TapeConfig entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(TapeActions entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(TapeAction entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(Buff entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToWish(Buff entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };                
                
        public static AudioInfoWish ToWish(AudioFileOutput entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = default
        };
                
        public static AudioInfoWish ToWish(AudioFileOutput entity, int courtesyFrames) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(courtesyFrames)
        };
        
        public static AudioInfoWish ToWish(Sample entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(AudioFileInfo info) => new AudioInfoWish
        {
            Bits         = info.Bits(),
            Channels     = info.Channels(),
            FrameCount   = info.FrameCount(),
            SamplingRate = info.SamplingRate()
        };

        public static AudioInfoWish ToWish(WavHeaderStruct wavHeader)
        {
            var info = WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader);
            return info.ToWish();
        }
        
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

        public static void ApplyTo(AudioInfoWish infoWish, SynthWishes entity) => entity.FromWish(infoWish);
        public static void FromWish(SynthWishes entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
        
        public static void ApplyTo(AudioInfoWish infoWish, FlowNode entity) => entity.FromWish(infoWish);
        public static void FromWish(FlowNode entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
        
        internal static void ApplyTo(AudioInfoWish infoWish, ConfigResolver entity, SynthWishes synthWishes) 
            => entity.FromWish(infoWish, synthWishes);
        internal static void FromWish(ConfigResolver entity, AudioInfoWish infoWish, SynthWishes synthWishes)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, synthWishes);
        }
                
        public static void ApplyTo(AudioInfoWish infoWish, Tape entity) => entity.FromWish(infoWish);
        public static void FromWish(Tape entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(AudioInfoWish infoWish, TapeConfig entity) => entity.FromWish(infoWish);
        public static void FromWish(TapeConfig entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(AudioInfoWish infoWish, TapeActions entity) => entity.FromWish(infoWish);
        public static void FromWish(TapeActions entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                
        public static void ApplyTo(AudioInfoWish infoWish, TapeAction entity) => entity.FromWish(infoWish);
        public static void FromWish(TapeAction entity, AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }
                        
        public static void ApplyTo(AudioInfoWish infoWish, Buff entity, int courtesyFrames, IContext context) 
            => entity.FromWish(infoWish, courtesyFrames, context);
        public static void FromWish(Buff entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
        }
                        
        public static void ApplyTo(AudioInfoWish infoWish, AudioFileOutput entity, int courtesyFrames, IContext context) 
            => entity.FromWish(infoWish, courtesyFrames, context);
        public static void FromWish(AudioFileOutput entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount, courtesyFrames);
        }
                        
        public static void ApplyTo(AudioInfoWish infoWish, Sample entity, IContext context) 
            => entity.FromWish(infoWish, context);
        public static void FromWish(Sample entity, AudioInfoWish infoWish, IContext context)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits, context);
            entity.SetChannels    (infoWish.Channels, context);
            entity.SetSamplingRate(infoWish.SamplingRate);
            // TODO: FrameCount not settable, but this might be the one time that the byte buffer should be scalable.
        }

        public static void ApplyTo(AudioInfoWish infoWish, AudioFileInfo entity) => entity.FromWish(infoWish);
        public static void FromWish(AudioFileInfo entity, AudioInfoWish infoWish) 
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            entity.SetBits        (infoWish.Bits        );
            entity.SetChannels    (infoWish.Channels    );
            entity.SetSamplingRate(infoWish.SamplingRate);
            entity.SetFrameCount  (infoWish.FrameCount  );
        }

        public static void ApplyTo(AudioInfoWish source, AudioInfoWish dest) => dest.FromWish(source);
        public static void FromWish(AudioInfoWish dest, AudioInfoWish source) 
        {
            if (source == null) throw new NullException(() => source);
            dest.SetBits        (source.Bits        );
            dest.SetChannels    (source.Channels    );
            dest.SetSamplingRate(source.SamplingRate);
            dest.SetFrameCount  (source.FrameCount  );
        }

        public static AudioFileInfo FromWish(AudioInfoWish wish) 
        {
            var dest = new AudioFileInfo();
            dest.FromWish(wish);
            return dest;
        }

        public   static WavHeaderStruct ToWavHeader(SynthWishes     entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(FlowNode        entity)                          => entity.ToWish()              .ToWavHeader();
        internal static WavHeaderStruct ToWavHeader(ConfigResolver  entity, SynthWishes synthWishes) => entity.ToWish(synthWishes)   .ToWavHeader();
        internal static WavHeaderStruct ToWavHeader(ConfigSection   entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Tape            entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeConfig      entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeActions     entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(TapeAction      entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Buff            entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Buff            entity, int courtesyFrames)      => entity.ToWish(courtesyFrames).ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioFileOutput entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioFileOutput entity, int courtesyFrames)      => entity.ToWish(courtesyFrames).ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(Sample          entity)                          => entity.ToWish()              .ToWavHeader();
        public   static WavHeaderStruct ToWavHeader(AudioInfoWish   entity)                          => WavHeaderManager.CreateWavHeaderStruct(entity.FromWish());
        public   static WavHeaderStruct ToWavHeader(AudioFileInfo   entity)                          => entity.ToWish()              .ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader((int bits, int channels, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader<TBits>((int channels, int samplingRate, int frameCount) x) 
            => x.ToWish<TBits>().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();
        public static WavHeaderStruct ToWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => x.ToWish().ToWavHeader();

        public static void ApplyWavHeader(WavHeaderStruct wavHeader, SynthWishes entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(SynthWishes entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, FlowNode entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(FlowNode entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        internal static void ApplyWavHeader(WavHeaderStruct wavHeader, ConfigResolver entity, SynthWishes synthWishes) 
            => entity.ApplyWavHeader(wavHeader, synthWishes);
        internal static void ApplyWavHeader(ConfigResolver entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => wavHeader.ToWish().ApplyTo(entity, synthWishes);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, Tape entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(Tape entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, TapeConfig entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(TapeConfig entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, TapeActions entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(TapeActions entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, TapeAction entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(TapeAction entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, Buff entity, int courtesyFrames, IContext context) 
            => entity.ApplyWavHeader(wavHeader, courtesyFrames, context);
        public static void ApplyWavHeader(Buff entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, courtesyFrames, context);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, AudioFileOutput entity, int courtesyFrames, IContext context) 
            => entity.ApplyWavHeader(wavHeader, courtesyFrames, context);
        public static void ApplyWavHeader(AudioFileOutput entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, courtesyFrames, context);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, Sample entity, IContext context) 
            => entity.ApplyWavHeader(wavHeader, context);
        public static void ApplyWavHeader(Sample entity, WavHeaderStruct wavHeader, IContext context) 
            => wavHeader.ToWish().ApplyTo(entity, context);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, AudioFileInfo entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(AudioFileInfo entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);
        
        public static void ApplyWavHeader(WavHeaderStruct wavHeader, AudioInfoWish entity) => entity.ApplyWavHeader(wavHeader);
        public static void ApplyWavHeader(AudioInfoWish entity, WavHeaderStruct wavHeader) 
            => wavHeader.ToWish().ApplyTo(entity);

        public static void ReadWavHeader(SynthWishes  entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(SynthWishes  entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(SynthWishes  entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(SynthWishes  entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, SynthWishes  entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   SynthWishes  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   SynthWishes  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   SynthWishes  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(FlowNode     entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(FlowNode     entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(FlowNode     entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(FlowNode     entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, FlowNode     entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   FlowNode     entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   FlowNode     entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   FlowNode     entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        
        internal static void ReadWavHeader(ConfigResolver entity, string filePath, SynthWishes synthWishes)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(ConfigResolver entity, Stream source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(ConfigResolver entity, BinaryReader source, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(string filePath, ConfigResolver entity, SynthWishes synthWishes)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(byte[] source, ConfigResolver entity, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(Stream source, ConfigResolver entity, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);
        internal static void ReadWavHeader(BinaryReader source, ConfigResolver entity, SynthWishes synthWishes)
            => source.ReadWavHeader().ApplyWavHeader(entity, synthWishes);

        public static void ReadWavHeader(Tape         entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Tape         entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Tape         entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Tape         entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, Tape         entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   Tape         entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   Tape         entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   Tape         entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeConfig   entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeConfig   entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeConfig   entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeConfig   entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, TapeConfig   entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   TapeConfig   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   TapeConfig   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   TapeConfig   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeActions  entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeActions  entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeActions  entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeActions  entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, TapeActions  entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   TapeActions  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   TapeActions  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   TapeActions  entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeAction   entity,   string       filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeAction   entity,   byte[]       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeAction   entity,   Stream       source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(TapeAction   entity,   BinaryReader source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string       filePath, TapeAction   entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]       source,   TapeAction   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream       source,   TapeAction   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader source,   TapeAction   entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
                
        public static void ReadWavHeader(Buff entity, string filePath, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(Buff entity, byte[] source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(Buff entity, Stream source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context);
        public static void ReadWavHeader(Buff entity, BinaryReader source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(string filePath, Buff entity, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(byte[] source, Buff entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(Stream source, Buff entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context);
        public static void ReadWavHeader(BinaryReader source, Buff entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
                
        public static void ReadWavHeader(AudioFileOutput entity, string filePath, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(AudioFileOutput entity, byte[] source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(AudioFileOutput entity, Stream source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context);
        public static void ReadWavHeader(AudioFileOutput entity, BinaryReader source, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(string filePath, AudioFileOutput entity, int courtesyFrames, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(byte[] source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
        public static void ReadWavHeader(Stream source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity,courtesyFrames, context);
        public static void ReadWavHeader(BinaryReader source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, courtesyFrames, context);
                
        public static void ReadWavHeader(Sample entity, string filePath, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(Sample entity, byte[] source, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(Sample entity, Stream source, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(Sample entity, BinaryReader source, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(string filePath, Sample entity, IContext context)
            => filePath.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(byte[] source, Sample entity, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(Stream source, Sample entity, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);
        public static void ReadWavHeader(BinaryReader source, Sample entity, IContext context)
            => source.ReadWavHeader().ApplyWavHeader(entity, context);

        
        public static void ReadWavHeader(AudioFileInfo entity,   string        filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioFileInfo entity,   byte[]        source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioFileInfo entity,   Stream        source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioFileInfo entity,   BinaryReader  source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string        filePath, AudioFileInfo entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]        source,   AudioFileInfo entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream        source,   AudioFileInfo entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader  source,   AudioFileInfo entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
                
        public static void ReadWavHeader(AudioInfoWish entity,   string        filePath) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioInfoWish entity,   byte[]        source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioInfoWish entity,   Stream        source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(AudioInfoWish entity,   BinaryReader  source  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(string        filePath, AudioInfoWish entity  ) => filePath.ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(byte[]        source,   AudioInfoWish entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(Stream        source,   AudioInfoWish entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);
        public static void ReadWavHeader(BinaryReader  source,   AudioInfoWish entity  ) => source  .ReadWavHeader().ApplyWavHeader(entity);

        public static WavHeaderStruct ReadWavHeader(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) return ReadWavHeader(fileStream);
        }
        
        public static WavHeaderStruct ReadWavHeader(byte[]       bytes)  => new MemoryStream(bytes).ReadWavHeader();
        public static WavHeaderStruct ReadWavHeader(Stream       stream) => new BinaryReader(stream).ReadWavHeader();
        public static WavHeaderStruct ReadWavHeader(BinaryReader reader) => reader.ReadStruct<WavHeaderStruct>();

        public static AudioInfoWish ReadAudioInfo(string       filePath) => filePath.ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(byte[]       source  ) => source  .ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(Stream       source  ) => source  .ReadWavHeader().ToWish();
        public static AudioInfoWish ReadAudioInfo(BinaryReader source)   => source  .ReadWavHeader().ToWish();

        public static void WriteWavHeader(SynthWishes  entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(SynthWishes  entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(SynthWishes  entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(SynthWishes  entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string       filePath, SynthWishes  entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]       dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream       dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter dest,     SynthWishes  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(FlowNode     entity,   string       filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(FlowNode     entity,   byte[]       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(FlowNode     entity,   BinaryWriter dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(FlowNode     entity,   Stream       dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string       filePath, FlowNode     entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]       dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream       dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter dest,     FlowNode     entity  ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigResolver entity,   string         filePath, SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(filePath);
        [UsedImplicitly] internal static void WriteWavHeader(ConfigResolver entity,   byte[]         dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigResolver entity,   BinaryWriter   dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigResolver entity,   Stream         dest    , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(string         filePath, ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(filePath);
        [UsedImplicitly] internal static void WriteWavHeader(byte[]         dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(Stream         dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(BinaryWriter   dest,     ConfigResolver entity  , SynthWishes synthWishes) => entity.ToWavHeader(synthWishes).Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigSection  entity,   string         filePath) => entity.ToWavHeader().Write(filePath);
        [UsedImplicitly] internal static void WriteWavHeader(ConfigSection  entity,   byte[]         dest    ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigSection  entity,   BinaryWriter   dest    ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(ConfigSection  entity,   Stream         dest    ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(string         filePath, ConfigSection  entity  ) => entity.ToWavHeader().Write(filePath);
        [UsedImplicitly] internal static void WriteWavHeader(byte[]         dest,     ConfigSection  entity  ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(Stream         dest,     ConfigSection  entity  ) => entity.ToWavHeader().Write(dest    );
        [UsedImplicitly] internal static void WriteWavHeader(BinaryWriter   dest,     ConfigSection  entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Tape            entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(Tape            entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Tape            entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Tape            entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  Tape            entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      Tape            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      Tape            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      Tape            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeConfig      entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(TapeConfig      entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeConfig      entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeConfig      entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  TapeConfig      entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      TapeConfig      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      TapeConfig      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      TapeConfig      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeActions     entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(TapeActions     entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeActions     entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeActions     entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  TapeActions     entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      TapeActions     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      TapeActions     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      TapeActions     entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeAction      entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(TapeAction      entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeAction      entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(TapeAction      entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  TapeAction      entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      TapeAction      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      TapeAction      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      TapeAction      entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Buff            entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(Buff            entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Buff            entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Buff            entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  Buff            entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      Buff            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      Buff            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      Buff            entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    string          filePath) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(AudioFileOutput entity,    byte[]          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    BinaryWriter    dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    Stream          dest    ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  AudioFileOutput entity  ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      AudioFileOutput entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      AudioFileOutput entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      AudioFileOutput entity  ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Buff            entity,    string          filePath, int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(filePath);
        public static void WriteWavHeader(Buff            entity,    byte[]          dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(Buff            entity,    BinaryWriter    dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(Buff            entity,    Stream          dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(string          filePath,  Buff            entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      Buff            entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(Stream          dest,      Buff            entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      Buff            entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    string          filePath, int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(filePath);
        public static void WriteWavHeader(AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(AudioFileOutput entity,    Stream          dest,     int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(string          filePath,  AudioFileOutput entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(Stream          dest,      AudioFileOutput entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) => entity.ToWavHeader(courtesyFrames).Write(dest    );
        public static void WriteWavHeader(Sample          entity,    string          filePath ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(Sample          entity,    byte[]          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Sample          entity,    BinaryWriter    dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Sample          entity,    Stream          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  Sample          entity   ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      Sample          entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      Sample          entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      Sample          entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioInfoWish   entity,    string          filePath ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(AudioInfoWish   entity,    byte[]          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioInfoWish   entity,    Stream          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioInfoWish   entity,    BinaryWriter    dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  AudioInfoWish   entity   ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      AudioInfoWish   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      AudioInfoWish   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      AudioInfoWish   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileInfo   entity,    string          filePath ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(AudioFileInfo   entity,    byte[]          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileInfo   entity,    Stream          dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(AudioFileInfo   entity,    BinaryWriter    dest     ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(string          filePath,  AudioFileInfo   entity   ) => entity.ToWavHeader().Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      AudioFileInfo   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(Stream          dest,      AudioFileInfo   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      AudioFileInfo   entity   ) => entity.ToWavHeader().Write(dest    );
        public static void WriteWavHeader(WavHeaderStruct wavHeader, BinaryWriter    dest     ) => wavHeader           .Write(dest    );
        public static void WriteWavHeader(WavHeaderStruct wavHeader, Stream          dest     ) => wavHeader           .Write(dest    );
        public static void WriteWavHeader(WavHeaderStruct wavHeader, byte[]          dest     ) => wavHeader           .Write(dest    );
        public static void WriteWavHeader(WavHeaderStruct wavHeader, string          filePath ) => wavHeader           .Write(filePath);
        public static void WriteWavHeader(string          filePath,  WavHeaderStruct wavHeader) => wavHeader           .Write(filePath);
        public static void WriteWavHeader(byte[]          dest,      WavHeaderStruct wavHeader) => wavHeader           .Write(dest    );
        public static void WriteWavHeader(Stream          dest,      WavHeaderStruct wavHeader) => wavHeader           .Write(dest    );
        public static void WriteWavHeader(BinaryWriter    dest,      WavHeaderStruct wavHeader) => wavHeader           .Write(dest    );
        public static void Write(WavHeaderStruct wavHeader, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)) fileStream.Write(wavHeader);
        }
        public static void Write(WavHeaderStruct wavHeader, byte[] dest)
        {
            new MemoryStream(dest).Write(wavHeader);
        }
        public static void Write(WavHeaderStruct wavHeader, Stream dest)
        {
            new BinaryWriter(dest).Write(wavHeader);
        }
        public static void Write(WavHeaderStruct wavHeader, BinaryWriter dest)
        {
            dest.WriteStruct(wavHeader);
        }
        public static void Write(string       filePath,  WavHeaderStruct wavHeader) => wavHeader.Write(filePath);
        public static void Write(byte[]       dest,      WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void Write(Stream       dest,      WavHeaderStruct wavHeader) => wavHeader.Write(dest    );
        public static void Write(BinaryWriter dest,      WavHeaderStruct wavHeader) => wavHeader.Write(dest    );

        // With Values

        public static void WriteWavHeader(string   filePath,  int bits, int channels, int samplingRate, int frameCount)    => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(string   filePath, (int bits, int channels, int samplingRate, int frameCount) x) => x                                         .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(byte[]       dest,  int bits, int channels, int samplingRate, int frameCount)    => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(byte[]       dest, (int bits, int channels, int samplingRate, int frameCount) x) => x                                         .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest,  int bits, int channels, int samplingRate, int frameCount)    => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest, (int bits, int channels, int samplingRate, int frameCount) x) => x                                         .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest,  int bits, int channels, int samplingRate, int frameCount)    => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest, (int bits, int channels, int samplingRate, int frameCount) x) => x                                         .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    string   filePath) => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, string   filePath) => x                                         .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    byte[]       dest) => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, byte[]       dest) => x                                         .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    Stream       dest) => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, Stream       dest) => x                                         .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( int bits, int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (bits, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((int bits, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => x                                         .ToWish().WriteWavHeader(dest    );

        public static void WriteWavHeader<TBits>(string   filePath,  int channels, int samplingRate, int frameCount   ) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>(string   filePath, (int channels, int samplingRate, int frameCount) x) =>  x                                  .ToWish<TBits>().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>(byte[]       dest,  int channels, int samplingRate, int frameCount   ) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>(byte[]       dest, (int channels, int samplingRate, int frameCount) x) =>  x                                  .ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>(Stream       dest,  int channels, int samplingRate, int frameCount   ) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>(Stream       dest, (int channels, int samplingRate, int frameCount) x) => x                                   .ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>(BinaryWriter dest,  int channels, int samplingRate, int frameCount   ) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>(BinaryWriter dest, (int channels, int samplingRate, int frameCount) x) => x                                   .ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    string   filePath) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, string   filePath) => x                                   .ToWish<TBits>().WriteWavHeader(filePath);
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    byte[]       dest) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, byte[]       dest) => x                                   .ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    Stream       dest) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, Stream       dest) => x                                   .ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>( int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (channels, samplingRate, frameCount).ToWish<TBits>().WriteWavHeader(dest    );
        public static void WriteWavHeader<TBits>((int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => x                                   .ToWish<TBits>().WriteWavHeader(dest    );
        
        public static void WriteWavHeader(string   filePath,  Type bitsType, int channels, int samplingRate, int frameCount   ) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(string   filePath, (Type bitsType, int channels, int samplingRate, int frameCount) x) => x                                             .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(byte[]       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(byte[]       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => x                                             .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => x                                             .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => x                                             .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    string   filePath) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, string   filePath) => x                                             .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    byte[]       dest) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, byte[]       dest) => x                                             .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    Stream       dest) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, Stream       dest) => x                                             .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( Type bitsType, int channels, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsType, channels, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((Type bitsType, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => x                                             .ToWish().WriteWavHeader(dest    );

        public static void WriteWavHeader(string   filePath,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(string   filePath, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => x                                                 .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(byte[]       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(byte[]       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => x                                                 .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => x                                                 .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => x                                                 .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    string   filePath) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, string   filePath) => x                                                 .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    byte[]       dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, byte[]       dest) => x                                                 .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    Stream       dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, Stream       dest) => x                                                 .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsEnum, channelsEnum, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, BinaryWriter dest) => x                                                 .ToWish().WriteWavHeader(dest    );

        public static void WriteWavHeader(string   filePath,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(string   filePath, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => x                                                     .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader(byte[]       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(byte[]       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => x                                                     .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(Stream       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => x                                                     .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader(BinaryWriter dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => x                                                     .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    string   filePath) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, string   filePath) => x                                                     .ToWish().WriteWavHeader(filePath);
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    byte[]       dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, byte[]       dest) => x                                                     .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    Stream       dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, Stream       dest) => x                                                     .ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader( SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount,    BinaryWriter dest) => (bitsEntity, channelsEntity, samplingRate, frameCount).ToWish().WriteWavHeader(dest    );
        public static void WriteWavHeader((SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, BinaryWriter dest) => x                                                     .ToWish().WriteWavHeader(dest    );
    }

    public static class ToWishExtensions
    {
        public static AudioInfoWish ToWish(this SynthWishes entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this FlowNode entity) => WavWishes.ToWish(entity);
        internal static AudioInfoWish ToWish(this ConfigResolver entity, SynthWishes synthWishes) => WavWishes.ToWish(entity, synthWishes);
        internal static AudioInfoWish ToWish(this ConfigSection entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this Tape entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this TapeConfig entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this TapeActions entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this TapeAction entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this Buff entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this Buff entity, int courtesyFrames) => WavWishes.ToWish(entity, courtesyFrames);
        public static AudioInfoWish ToWish(this AudioFileOutput entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this AudioFileOutput entity, int courtesyFrames) => WavWishes.ToWish(entity, courtesyFrames);
        public static AudioInfoWish ToWish(this Sample entity) => WavWishes.ToWish(entity);
        public static AudioInfoWish ToWish(this AudioFileInfo info) => WavWishes.ToWish(info);
        public static AudioInfoWish ToWish(this WavHeaderStruct wavHeader) => WavWishes.ToWish(wavHeader);
        public static AudioInfoWish ToWish(this (int bits, int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToWish(x);
        public static AudioInfoWish ToWish<TBits>(this (int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToWish<TBits>(x);
        public static AudioInfoWish ToWish(this (Type bitsType, int channels, int samplingRate, int frameCount) x) 
            => WavWishes.ToWish(x);
        public static AudioInfoWish ToWish(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) 
            => WavWishes.ToWish(x);
        public static AudioInfoWish ToWish(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) 
            => WavWishes.ToWish(x);
    }
    
    public static class FromWishExtensions
    {
        public static  void ApplyTo(this  AudioInfoWish infoWish, SynthWishes   entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this SynthWishes   entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, FlowNode      entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this FlowNode      entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, Tape          entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this Tape          entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, TapeConfig    entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this TapeConfig    entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, TapeActions   entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this TapeActions   entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, TapeAction    entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this TapeAction    entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        public static  void ApplyTo(this  AudioInfoWish infoWish, AudioFileInfo entity  ) => WavWishes.ApplyTo (infoWish, entity);
        public static  void FromWish(this AudioFileInfo entity,   AudioInfoWish infoWish) => WavWishes.FromWish(entity, infoWish);
        
        internal static void ApplyTo(this AudioInfoWish infoWish, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ApplyTo(infoWish, entity, synthWishes);
        internal static void FromWish(this ConfigResolver entity, AudioInfoWish infoWish, SynthWishes synthWishes)
            => WavWishes.FromWish(entity, infoWish, synthWishes);
        
        public static void ApplyTo(this AudioInfoWish infoWish, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyTo(infoWish, entity, courtesyFrames, context);
        public static void FromWish(this Buff entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
            => WavWishes.FromWish(entity, infoWish, courtesyFrames, context);
        
        public static void ApplyTo(this AudioInfoWish infoWish, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyTo(infoWish, entity, courtesyFrames, context);
        public static void FromWish(this AudioFileOutput entity, AudioInfoWish infoWish, int courtesyFrames, IContext context)
            => WavWishes.FromWish(entity, infoWish, courtesyFrames, context);
        
        public static void ApplyTo(this AudioInfoWish infoWish, Sample entity, IContext context)
            => WavWishes.ApplyTo(infoWish, entity, context);
        public static void FromWish(this Sample entity, AudioInfoWish infoWish, IContext context)
            => WavWishes.FromWish(entity, infoWish, context);
        
        public static void          ApplyTo(this  AudioInfoWish source, AudioInfoWish dest)   => WavWishes.ApplyTo(source, dest);
        public static void          FromWish(this AudioInfoWish dest,   AudioInfoWish source) => WavWishes.FromWish(dest, source);
        public static AudioFileInfo FromWish(this AudioInfoWish wish) => WavWishes.FromWish(wish);
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
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, SynthWishes entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this SynthWishes entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);

        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, FlowNode entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this FlowNode entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        internal static void ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ApplyWavHeader(wavHeader, entity, synthWishes);
        internal static void ApplyWavHeader(this ConfigResolver entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => WavWishes.ApplyWavHeader(entity, wavHeader, synthWishes);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, Tape entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this Tape entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, TapeConfig entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this TapeConfig entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, TapeActions entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this TapeActions entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, TapeAction entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this TapeAction entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, courtesyFrames, context);
        public static void ApplyWavHeader(this Buff entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, courtesyFrames, context);

        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, courtesyFrames, context);
        public static void ApplyWavHeader(this AudioFileOutput entity, WavHeaderStruct wavHeader, int courtesyFrames, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, courtesyFrames, context);

        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, Sample entity, IContext context)
            => WavWishes.ApplyWavHeader(wavHeader, entity, context);
        public static void ApplyWavHeader(this Sample entity, WavHeaderStruct wavHeader, IContext context)
            => WavWishes.ApplyWavHeader(entity, wavHeader, context);

        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, AudioFileInfo entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this AudioFileInfo entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
        
        public static void ApplyWavHeader(this WavHeaderStruct wavHeader, AudioInfoWish entity) => WavWishes.ApplyWavHeader(wavHeader, entity);
        public static void ApplyWavHeader(this AudioInfoWish entity, WavHeaderStruct wavHeader) => WavWishes.ApplyWavHeader(entity, wavHeader);
    }
    
    public static class ReadWavHeaderExtensions
    {
        public static void ReadWavHeader(this SynthWishes  entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this SynthWishes  entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this SynthWishes  entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this SynthWishes  entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, SynthWishes  entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   SynthWishes  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this FlowNode     entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this FlowNode     entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this FlowNode     entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this FlowNode     entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, FlowNode     entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   FlowNode     entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
        internal static void ReadWavHeader(this ConfigResolver entity, string filePath, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, filePath, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, Stream source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        internal static void ReadWavHeader(this ConfigResolver entity, BinaryReader source, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(entity, source, synthWishes);
        internal static void ReadWavHeader(this string filePath, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(filePath, entity, synthWishes);
        internal static void ReadWavHeader(this byte[] source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        internal static void ReadWavHeader(this Stream source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        internal static void ReadWavHeader(this BinaryReader source, ConfigResolver entity, SynthWishes synthWishes)
            => WavWishes.ReadWavHeader(source, entity, synthWishes);
        
        public static void ReadWavHeader(this Tape         entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this Tape         entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this Tape         entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this Tape         entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, Tape         entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   Tape         entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this TapeConfig   entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this TapeConfig   entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeConfig   entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeConfig   entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, TapeConfig   entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   TapeConfig   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this TapeActions  entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this TapeActions  entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeActions  entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeActions  entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, TapeActions  entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   TapeActions  entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this TapeAction   entity,   string       filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this TapeAction   entity,   byte[]       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeAction   entity,   Stream       source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this TapeAction   entity,   BinaryReader source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string       filePath, TapeAction   entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]       source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream       source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader source,   TapeAction   entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
        public static void ReadWavHeader(this Buff entity, string filePath, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, filePath, courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, byte[] source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, Stream source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this Buff entity, BinaryReader source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this string filePath, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(filePath, entity, courtesyFrames, context);
        public static void ReadWavHeader(this byte[] source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static void ReadWavHeader(this Stream source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static void ReadWavHeader(this BinaryReader source, Buff entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        
        public static void ReadWavHeader(this AudioFileOutput entity, string filePath, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, filePath, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, byte[] source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, Stream source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this AudioFileOutput entity, BinaryReader source, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(entity, source, courtesyFrames, context);
        public static void ReadWavHeader(this string filePath, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(filePath, entity, courtesyFrames, context);
        public static void ReadWavHeader(this byte[] source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static void ReadWavHeader(this Stream source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);
        public static void ReadWavHeader(this BinaryReader source, AudioFileOutput entity, int courtesyFrames, IContext context)
            => WavWishes.ReadWavHeader(source, entity, courtesyFrames, context);

        public static void ReadWavHeader(this Sample       entity,   string filePath,     IContext context) => WavWishes.ReadWavHeader(entity, filePath, context);
        public static void ReadWavHeader(this Sample       entity,   byte[] source,       IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        public static void ReadWavHeader(this Sample       entity,   Stream source,       IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        public static void ReadWavHeader(this Sample       entity,   BinaryReader source, IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        public static void ReadWavHeader(this string       filePath, Sample entity,       IContext context) => WavWishes.ReadWavHeader(entity, filePath, context);
        public static void ReadWavHeader(this byte[]       source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        public static void ReadWavHeader(this Stream       source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        public static void ReadWavHeader(this BinaryReader source,   Sample entity,       IContext context) => WavWishes.ReadWavHeader(entity, source,   context);
        
        public static void ReadWavHeader(this AudioInfoWish entity,   string        filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this AudioInfoWish entity,   byte[]        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this AudioInfoWish entity,   Stream        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this AudioInfoWish entity,   BinaryReader  source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string        filePath, AudioInfoWish entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]        source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream        source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader  source,   AudioInfoWish entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this AudioFileInfo entity,   string        filePath) => WavWishes.ReadWavHeader(entity,   filePath);
        public static void ReadWavHeader(this AudioFileInfo entity,   byte[]        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this AudioFileInfo entity,   Stream        source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this AudioFileInfo entity,   BinaryReader  source  ) => WavWishes.ReadWavHeader(entity,   source  );
        public static void ReadWavHeader(this string        filePath, AudioFileInfo entity  ) => WavWishes.ReadWavHeader(filePath, entity  );
        public static void ReadWavHeader(this byte[]        source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this Stream        source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        public static void ReadWavHeader(this BinaryReader  source,   AudioFileInfo entity  ) => WavWishes.ReadWavHeader(source,   entity  );
        
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
        public static void WriteWavHeader(this SynthWishes  entity,   string       filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this SynthWishes  entity,   byte[]       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this SynthWishes  entity,   BinaryWriter dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this SynthWishes  entity,   Stream       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string       filePath, SynthWishes  entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]       dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream       dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter dest,     SynthWishes  entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this FlowNode     entity,   string       filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this FlowNode     entity,   byte[]       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this FlowNode     entity,   BinaryWriter dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this FlowNode     entity,   Stream       dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string       filePath, FlowNode     entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]       dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream       dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter dest,     FlowNode     entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigResolver entity,   string         filePath, SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   filePath, synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigResolver entity,   byte[]         dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigResolver entity,   BinaryWriter   dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigResolver entity,   Stream         dest    , SynthWishes synthWishes) => WavWishes.WriteWavHeader(entity,   dest    , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this string         filePath, ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(filePath, entity  , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this byte[]         dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this Stream         dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this BinaryWriter   dest,     ConfigResolver entity  , SynthWishes synthWishes) => WavWishes.WriteWavHeader(dest,     entity  , synthWishes);
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigSection entity,   string        filePath) => WavWishes.WriteWavHeader(entity,   filePath);
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigSection entity,   byte[]        dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigSection entity,   BinaryWriter  dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static void WriteWavHeader(this ConfigSection entity,   Stream        dest    ) => WavWishes.WriteWavHeader(entity,   dest    );
        [UsedImplicitly] internal static void WriteWavHeader(this string        filePath, ConfigSection entity  ) => WavWishes.WriteWavHeader(filePath, entity  );
        [UsedImplicitly] internal static void WriteWavHeader(this byte[]        dest,     ConfigSection entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static void WriteWavHeader(this Stream        dest,     ConfigSection entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        [UsedImplicitly] internal static void WriteWavHeader(this BinaryWriter  dest,     ConfigSection entity  ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Tape            entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this Tape            entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this Tape            entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this Tape            entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  Tape            entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      Tape            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this TapeConfig      entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this TapeConfig      entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeConfig      entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeConfig      entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  TapeConfig      entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      TapeConfig      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this TapeActions     entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this TapeActions     entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeActions     entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeActions     entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  TapeActions     entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      TapeActions     entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this TapeAction      entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this TapeAction      entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeAction      entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this TapeAction      entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  TapeAction      entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      TapeAction      entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Buff            entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this Buff            entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this Buff            entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this Buff            entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  Buff            entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      Buff            entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this AudioFileOutput entity,    string          filePath  ) => WavWishes.WriteWavHeader(entity,   filePath);
        public static void WriteWavHeader(this AudioFileOutput entity,    byte[]          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this AudioFileOutput entity,    BinaryWriter    dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this AudioFileOutput entity,    Stream          dest      ) => WavWishes.WriteWavHeader(entity,   dest    );
        public static void WriteWavHeader(this string          filePath,  AudioFileOutput entity    ) => WavWishes.WriteWavHeader(filePath, entity  );
        public static void WriteWavHeader(this byte[]          dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Stream          dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this BinaryWriter    dest,      AudioFileOutput entity    ) => WavWishes.WriteWavHeader(dest,     entity  );
        public static void WriteWavHeader(this Buff            entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static void WriteWavHeader(this Buff            entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this Buff            entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this Buff            entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this string          filePath,  Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static void WriteWavHeader(this byte[]          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this Stream          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this BinaryWriter    dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this AudioFileOutput entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static void WriteWavHeader(this AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this AudioFileOutput entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static void WriteWavHeader(this string          filePath,  AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static void WriteWavHeader(this byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this Stream          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static void WriteWavHeader(this Sample          entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static void WriteWavHeader(this Sample          entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this Sample          entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this Sample          entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this string          filePath,  Sample          entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static void WriteWavHeader(this byte[]          dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this Stream          dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this BinaryWriter    dest,      Sample          entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this AudioInfoWish   entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static void WriteWavHeader(this AudioInfoWish   entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this AudioInfoWish   entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this AudioInfoWish   entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this string          filePath,  AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static void WriteWavHeader(this byte[]          dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this Stream          dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this BinaryWriter    dest,      AudioInfoWish   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this AudioFileInfo   entity,    string          filePath ) => WavWishes.WriteWavHeader(entity,    filePath );
        public static void WriteWavHeader(this AudioFileInfo   entity,    byte[]          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this AudioFileInfo   entity,    Stream          dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this AudioFileInfo   entity,    BinaryWriter    dest     ) => WavWishes.WriteWavHeader(entity,    dest     );
        public static void WriteWavHeader(this string          filePath,  AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(filePath,  entity   );
        public static void WriteWavHeader(this byte[]          dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this Stream          dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this BinaryWriter    dest,      AudioFileInfo   entity   ) => WavWishes.WriteWavHeader(dest,      entity   );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, string          filePath ) => WavWishes.WriteWavHeader(wavHeader, filePath );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, byte[]          dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, Stream          dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static void WriteWavHeader(this WavHeaderStruct wavHeader, BinaryWriter    dest     ) => WavWishes.WriteWavHeader(wavHeader, dest     );
        public static void WriteWavHeader(this string           filePath, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(filePath,  wavHeader);
        public static void WriteWavHeader(this byte[]               dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static void WriteWavHeader(this Stream               dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static void WriteWavHeader(this BinaryWriter         dest, WavHeaderStruct wavHeader) => WavWishes.WriteWavHeader(dest,      wavHeader);
        public static void Write         (this WavHeaderStruct wavHeader, string          filePath ) => WavWishes.Write         (wavHeader, filePath );
        public static void Write         (this WavHeaderStruct wavHeader, byte[]          dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static void Write         (this WavHeaderStruct wavHeader, Stream          dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static void Write         (this WavHeaderStruct wavHeader, BinaryWriter    dest     ) => WavWishes.Write         (wavHeader, dest     );
        public static void Write         (this string           filePath, WavHeaderStruct wavHeader) => WavWishes.Write         (filePath,  wavHeader);
        public static void Write         (this byte[]               dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        public static void Write         (this Stream               dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        public static void Write         (this BinaryWriter         dest, WavHeaderStruct wavHeader) => WavWishes.Write         (dest,      wavHeader);
        
        // With Values
        
        public static void WriteWavHeader(this string   filePath,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bits, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this string   filePath, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static void WriteWavHeader(this byte[]       dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this byte[]       dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this Stream       dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this Stream       dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this BinaryWriter dest,  int bits, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bits, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this BinaryWriter dest, (int bits, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static void WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (int bits, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );
        

        public static void WriteWavHeader<TBits>(this string   filePath,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(filePath, channels, samplingRate, frameCount);
        public static void WriteWavHeader<TBits>(this string   filePath, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(filePath, x);
        public static void WriteWavHeader<TBits>(this byte[]       dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static void WriteWavHeader<TBits>(this byte[]       dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static void WriteWavHeader<TBits>(this Stream       dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static void WriteWavHeader<TBits>(this Stream       dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static void WriteWavHeader<TBits>(this BinaryWriter dest,  int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader<TBits>(dest,     channels, samplingRate, frameCount);
        public static void WriteWavHeader<TBits>(this BinaryWriter dest, (int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader<TBits>(dest,     x);
        public static void WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader<TBits>(x, filePath);
        public static void WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );
        public static void WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );
        public static void WriteWavHeader<TBits>(this (int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader<TBits>(x, dest    );

        public static void WriteWavHeader(this string   filePath,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsType, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this string   filePath, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static void WriteWavHeader(this byte[]       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this byte[]       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this Stream       dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this Stream       dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this BinaryWriter dest,  Type bitsType, int channels, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsType, channels, samplingRate, frameCount);
        public static void WriteWavHeader(this BinaryWriter dest, (Type bitsType, int channels, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static void WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (Type bitsType, int channels, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );

        public static void WriteWavHeader(this string   filePath,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsEnum, channelsEnum, samplingRate, frameCount);
        public static void WriteWavHeader(this string   filePath, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static void WriteWavHeader(this byte[]       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static void WriteWavHeader(this byte[]       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this Stream       dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static void WriteWavHeader(this Stream       dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this BinaryWriter dest,  SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEnum, channelsEnum, samplingRate, frameCount);
        public static void WriteWavHeader(this BinaryWriter dest, (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static void WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );

        public static void WriteWavHeader(this string   filePath,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(filePath, bitsEntity, channelsEntity, samplingRate, frameCount);
        public static void WriteWavHeader(this string   filePath, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(filePath, x);
        public static void WriteWavHeader(this byte[]       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static void WriteWavHeader(this byte[]       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this Stream       dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static void WriteWavHeader(this Stream       dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this BinaryWriter dest,  SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount   ) => WavWishes.WriteWavHeader(dest,     bitsEntity, channelsEntity, samplingRate, frameCount);
        public static void WriteWavHeader(this BinaryWriter dest, (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x) => WavWishes.WriteWavHeader(dest,     x);
        public static void WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, string   filePath) => WavWishes.WriteWavHeader(x, filePath);
        public static void WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, byte[]       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, Stream       dest) => WavWishes.WriteWavHeader(x, dest    );
        public static void WriteWavHeader(this (SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) x, BinaryWriter dest) => WavWishes.WriteWavHeader(x, dest    );
    }
}