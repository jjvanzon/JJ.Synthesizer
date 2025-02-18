using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JJ.Persistence.Synthesizer;
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable RedundantEmptyObjectOrCollectionInitializer

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class WavHeaderWishesTests
    {
        // Test Data
        
        private int Tolerance { get; } = 1;
        
        // TODO: CaseBase without MainProp to omit the <int> type argument?
        private class Case : CaseBase<int>
        {
            public CaseProp<int> SamplingRate   { get; set; }
            public CaseProp<int> Bits           { get; set; }
            public CaseProp<int> Channels       { get; set; }
            public CaseProp<int> CourtesyFrames { get; set; }
            public CaseProp<int> FrameCount     { get; set; }
        }
        
        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case> 
        
            { AllowDuplicates = true };
        
        static CaseCollection<Case> InvariantCases { get; } = Cases.FromTemplate(new Case
        
            { SamplingRate = 48000, Bits = 32, Channels = 1, CourtesyFrames = 3, FrameCount = 100+3 },
            
            new Case {                        },
            new Case { Bits           =    16 },
            new Case { Bits           =     8 },
            new Case { SamplingRate   = 96000 },
            new Case { Channels       =     2 },
            new Case { FrameCount     =   256 },
            new Case { CourtesyFrames =     5 }) .FromTemplate(new Case
        
            { SamplingRate = 44100, Bits = 16, Channels = 2, CourtesyFrames = 5, FrameCount = 256 },
            
            new Case {                        },
            new Case { Bits           =    32 },
            new Case { SamplingRate   = 48000 },
            new Case { Channels       =     1 },
            new Case { FrameCount     = 100+5 },
            new Case { CourtesyFrames =     3 }
        );
        
        static CaseCollection<Case> TransitionCases { get; } = Cases.FromTemplate(new Case
        
            { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100+3 },
            
            new Case { Bits           = { To =     8 } },
            new Case { Bits           = { To =    16 } },
            new Case { Channels       = { To =     1 } },
            new Case { Channels       = { From =   1 } },
            new Case { Channels       =            1   },
            new Case { SamplingRate   = { To = 96000 } },
            new Case { FrameCount     = { To =   256 } },
            new Case { CourtesyFrames = { To =     4 } }
        );

        private TestEntities CreateEntities(Case test, bool wipeBuff = true, bool withDisk = false)
        {
            var testEntities = new TestEntities(withDisk, x => x.WithBits(test.Bits.Init)
                                                                .WithChannels(test.Channels.Init)
                                                                .WithSamplingRate(test.SamplingRate.Init)
                                                                .WithCourtesyFrames(test.CourtesyFrames.Init)
                                                                .WithFrameCount(test.FrameCount.Init));
            
            AdjustBufferState(testEntities, wipeBuff);
            
            return testEntities;
        }
        
        private TestEntities CreateChangedEntities(Case test, bool wipeBuff = true, bool withDisk = false)
        {
            var testEntities = new TestEntities(withDisk, x => x.WithBits(test.Bits.To)
                                                                .WithChannels(test.Channels.To)
                                                                .WithSamplingRate(test.SamplingRate.To)
                                                                .WithCourtesyFrames(test.CourtesyFrames.To)
                                                                .WithFrameCount(test.FrameCount.To));
            
            AdjustBufferState(testEntities, wipeBuff);
            
            return testEntities;
        }
        
        private static void AdjustBufferState(TestEntities testEntities, bool wipeBuff)
        {
            if (wipeBuff)
            {
                testEntities.BuffBound.Buff.Bytes = null; // Unbuff it so FrameCounts can be set and not calculated from Buff.
            }
        }
        
        // Test Code
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_ToWish(string caseKey)
        { 
            Case test = Cases[caseKey];
            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            
            var intTuple      = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typeTuple     = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typelessTuple = (                                x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var enumTuple     = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            var entityTuple   = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);
            
            // TODO: Use existing getter assertion helper to check 4 props at once?
            AreEqual(test.Bits,           () => x.SynthBound .SynthWishes    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.SynthBound .SynthWishes    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.SynthBound .SynthWishes    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.SynthBound .SynthWishes    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .FlowNode       .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.SynthBound .FlowNode       .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.SynthBound .FlowNode       .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.SynthBound .FlowNode       .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Bits        );
            AreEqual(test.Channels,       () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Channels    );
            AreEqual(test.SamplingRate,   () => x.SynthBound .ConfigResolver .ToWish(synthWishes).SamplingRate);
            AreEqual(test.FrameCount,     () => x.SynthBound .ConfigResolver .ToWish(synthWishes).FrameCount, -Tolerance);
            AreEqual(DefaultBits,         () => x.SynthBound .ConfigSection  .ToWish().Bits                  );
            AreEqual(DefaultChannels,     () => x.SynthBound .ConfigSection  .ToWish().Channels              );
            AreEqual(DefaultSamplingRate, () => x.SynthBound .ConfigSection  .ToWish().SamplingRate          );
            AreEqual(DefaultFrameCount,   () => x.SynthBound .ConfigSection  .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .Tape           .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound  .Tape           .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .Tape           .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound  .Tape           .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeConfig     .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeConfig     .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeConfig     .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeConfig     .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeActions    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeActions    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeActions    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeActions    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeAction     .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeAction     .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeAction     .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeAction     .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish().SamplingRate          );
            AreEqual(0,                   () => x.BuffBound  .Buff           .ToWish().FrameCount, -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Bits     );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Channels );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWish(courtesyFrames).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish().FrameCount(frameCount).Bits);
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish().FrameCount(frameCount).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish().FrameCount(frameCount).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWish().FrameCount(frameCount).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().SamplingRate          );
            AreEqual(0,                   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount, -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Bits    );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Bits);
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.Sample         .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Independent.Sample         .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Independent.Sample         .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Independent.Sample         .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.AudioFileInfo  .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Independent.AudioFileInfo  .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo  .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo  .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable  .WavHeader      .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable  .WavHeader      .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable  .WavHeader      .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable  .WavHeader      .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => intTuple                     .ToWish().Bits                  );
            AreEqual(test.Channels,       () => intTuple                     .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => intTuple                     .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => intTuple                     .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => typeTuple                    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => typeTuple                    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => typeTuple                    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => typeTuple                    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => enumTuple                    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => enumTuple                    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => enumTuple                    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => enumTuple                    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => entityTuple                  .ToWish().Bits                  );
            AreEqual(test.Channels,       () => entityTuple                  .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => entityTuple                  .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => entityTuple                  .ToWish().FrameCount, -Tolerance);
            
            if (test.Bits == 8)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<byte> ().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<byte> ().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<byte> ().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<byte> ().FrameCount, -Tolerance);
            }
            else if (test.Bits == 16)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<short>().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<short>().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<short>().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<short>().FrameCount, -Tolerance);
            }
            else if (test.Bits == 32)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<float>().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<float>().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<float>().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<float>().FrameCount, -Tolerance);
            }
            else
            {   // ncrunch: no coverage start
                throw new Exception(NotSupportedMessage(nameof(test.Bits), test.Bits, ValidBits));
                // ncrunch: no coverage end
            }
        }

        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void WavHeader_FromWish(string caseKey)
        { 
            Case test = Cases[caseKey];
            SynthWishes synthWishes;
            IContext context;
            int courtesy = test.CourtesyFrames;

            void TestProp(Action<TestEntities, AudioInfoWish> setter)
            {
                TestEntities  x = CreateEntities(test);
                AssertIsInit (x, test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;
                
                var infoWish = new AudioInfoWish
                {
                    Bits         = test.Bits,
                    Channels     = test.Channels,
                    SamplingRate = test.SamplingRate,
                    FrameCount   = test.FrameCount  
                };
                
                setter(x, infoWish);
            }
            
            TestProp((x, info) => { x.SynthBound .SynthWishes    .FromWish(info)                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, info) => { x.SynthBound .FlowNode       .FromWish(info)                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, info) => { x.SynthBound .ConfigResolver .FromWish(info, synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, info) => { x.TapeBound  .Tape           .FromWish(info)                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, info) => { x.TapeBound  .TapeConfig     .FromWish(info)                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, info) => { x.TapeBound  .TapeActions    .FromWish(info)                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, info) => { x.TapeBound  .TapeAction     .FromWish(info)                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, info) => { x.BuffBound  .Buff           .FromWish(info, courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, info) => { x.BuffBound  .AudioFileOutput.FromWish(info, courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, info) => { x.Independent.Sample         .FromWish(info,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, info) => { x.Independent.AudioFileInfo  .FromWish(info)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, info) => { x.Independent.AudioInfoWish  .FromWish(info)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp((x, info) => { info.ApplyTo(x.SynthBound .SynthWishes)                        ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, info) => { info.ApplyTo(x.SynthBound .FlowNode)                           ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, info) => { info.ApplyTo(x.SynthBound .ConfigResolver, synthWishes)        ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, info) => { info.ApplyTo(x.TapeBound  .Tape)                               ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeConfig)                         ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeActions)                        ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeAction)                         ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, info) => { info.ApplyTo(x.BuffBound  .Buff,             courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, info) => { info.ApplyTo(x.BuffBound  .AudioFileOutput,  courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, info) => { info.ApplyTo(x.Independent.Sample,                     context); Assert(x.Independent.Sample,          test); });
            TestProp((x, info) => { info.ApplyTo(x.Independent.AudioFileInfo)                      ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, info) => { info.ApplyTo(x.Independent.AudioInfoWish)                      ; Assert(x.Independent.AudioInfoWish,   test); });
        }

        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_ToWavHeader(string caseKey)
        { 
            Case test = Cases[caseKey];
            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            
            var intTuple      = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typeTuple     = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typelessTuple = (                                x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var enumTuple     = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            var entityTuple   = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);
            
            AreEqual(test.Bits,           () => x.SynthBound .SynthWishes    .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.SynthBound .SynthWishes    .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.SynthBound .SynthWishes    .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.SynthBound .SynthWishes    .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .FlowNode       .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.SynthBound .FlowNode       .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.SynthBound .FlowNode       .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.SynthBound .FlowNode       .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .ConfigResolver .ToWavHeader(synthWishes).BitsPerValue);
            AreEqual(test.Channels,       () => x.SynthBound .ConfigResolver .ToWavHeader(synthWishes).ChannelCount);
            AreEqual(test.SamplingRate,   () => x.SynthBound .ConfigResolver .ToWavHeader(synthWishes).SamplingRate);
            AreEqual(test.FrameCount,     () => x.SynthBound .ConfigResolver .ToWavHeader(synthWishes).FrameCount(), -Tolerance);
            AreEqual(DefaultBits,         () => x.SynthBound .ConfigSection  .ToWavHeader().BitsPerValue           );
            AreEqual(DefaultChannels,     () => x.SynthBound .ConfigSection  .ToWavHeader().ChannelCount           );
            AreEqual(DefaultSamplingRate, () => x.SynthBound .ConfigSection  .ToWavHeader().SamplingRate           );
            AreEqual(DefaultFrameCount,   () => x.SynthBound .ConfigSection  .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .Tape           .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.TapeBound  .Tape           .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .Tape           .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .Tape           .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeConfig     .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeConfig     .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeConfig     .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeConfig     .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeActions    .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeActions    .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeActions    .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeActions    .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeAction     .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeAction     .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeAction     .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeAction     .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWavHeader().SamplingRate           );
            AreEqual(0,                   () => x.BuffBound  .Buff           .ToWavHeader().FrameCount(), -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWavHeader(courtesyFrames).Bits()   );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWavHeader(courtesyFrames).Channels());
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWavHeader(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWavHeader(courtesyFrames).FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWavHeader().FrameCount(frameCount, courtesyFrames).Bits()); // TODO: Why is courtesyFrames needed here?
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWavHeader().FrameCount(frameCount, courtesyFrames).Channels());
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWavHeader().FrameCount(frameCount, courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWavHeader().FrameCount(frameCount, courtesyFrames).FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWavHeader().SamplingRate           );
            AreEqual(0,                   () => x.BuffBound  .AudioFileOutput.ToWavHeader().FrameCount(), -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).Bits()   );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).Channels());
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWavHeader().FrameCount(frameCount, courtesyFrames).Bits()); // TODO: Why is courtesyFrames needed here?
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWavHeader().FrameCount(frameCount, courtesyFrames).Channels());
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWavHeader().FrameCount(frameCount, courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWavHeader().FrameCount(frameCount, courtesyFrames).FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).Bits()   );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).Channels());
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWavHeader(courtesyFrames).FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.Sample         .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.Independent.Sample         .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.Independent.Sample         .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.Independent.Sample         .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.AudioFileInfo  .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => x.Independent.AudioFileInfo  .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo  .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo  .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => intTuple                     .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => intTuple                     .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => intTuple                     .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => intTuple                     .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => typeTuple                    .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => typeTuple                    .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => typeTuple                    .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => typeTuple                    .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => enumTuple                    .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => enumTuple                    .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => enumTuple                    .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => enumTuple                    .ToWavHeader().FrameCount(), -Tolerance);
            AreEqual(test.Bits,           () => entityTuple                  .ToWavHeader().BitsPerValue           );
            AreEqual(test.Channels,       () => entityTuple                  .ToWavHeader().ChannelCount           );
            AreEqual(test.SamplingRate,   () => entityTuple                  .ToWavHeader().SamplingRate           );
            AreEqual(test.FrameCount,     () => entityTuple                  .ToWavHeader().FrameCount(), -Tolerance);
            
            if (test.Bits == 8)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWavHeader<byte> ().BitsPerValue           );
                AreEqual(test.Channels,     () => typelessTuple.ToWavHeader<byte> ().ChannelCount           );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWavHeader<byte> ().SamplingRate           );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWavHeader<byte> ().FrameCount(), -Tolerance);
            }
            else if (test.Bits == 16)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWavHeader<short>().BitsPerValue           );
                AreEqual(test.Channels,     () => typelessTuple.ToWavHeader<short>().ChannelCount           );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWavHeader<short>().SamplingRate           );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWavHeader<short>().FrameCount(), -Tolerance);
            }
            else if (test.Bits == 32)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWavHeader<float>().BitsPerValue           );
                AreEqual(test.Channels,     () => typelessTuple.ToWavHeader<float>().ChannelCount           );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWavHeader<float>().SamplingRate           );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWavHeader<float>().FrameCount(), -Tolerance);
            }
            else
            {   // ncrunch: no coverage start
                throw new Exception(NotSupportedMessage(nameof(test.Bits), test.Bits, ValidBits));
                // ncrunch: no coverage end
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void WavHeader_FromWavHeader(string caseKey)
        { 
            Case test = Cases[caseKey];
            SynthWishes synthWishes;
            IContext context;
            int courtesy = test.CourtesyFrames;

            void TestProp(Action<TestEntities, WavHeaderStruct> setter)
            {
                TestEntities  x = CreateEntities(test);
                AssertIsInit (x, test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;

                var wavHeader = new AudioInfoWish
                {
                    Bits         = test.Bits,
                    Channels     = test.Channels,
                    SamplingRate = test.SamplingRate,
                    FrameCount   = test.FrameCount  
                }.ToWavHeader();
                
                setter(x, wavHeader);
            }
            
            TestProp((x, wav) => { x.SynthBound .SynthWishes    .FromWavHeader(wav)                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, wav) => { x.SynthBound .FlowNode       .FromWavHeader(wav)                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, wav) => { x.SynthBound .ConfigResolver .FromWavHeader(wav, synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, wav) => { x.TapeBound  .Tape           .FromWavHeader(wav)                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, wav) => { x.TapeBound  .TapeConfig     .FromWavHeader(wav)                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, wav) => { x.TapeBound  .TapeActions    .FromWavHeader(wav)                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, wav) => { x.TapeBound  .TapeAction     .FromWavHeader(wav)                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, wav) => { x.BuffBound  .Buff           .FromWavHeader(wav, courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, wav) => { x.BuffBound  .AudioFileOutput.FromWavHeader(wav, courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, wav) => { x.Independent.Sample         .FromWavHeader(wav,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, wav) => { x.Independent.AudioFileInfo  .FromWavHeader(wav)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, wav) => { x.Independent.AudioInfoWish  .FromWavHeader(wav)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp((x, wav) => { wav.ApplyTo(x.SynthBound .SynthWishes)                        ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, wav) => { wav.ApplyTo(x.SynthBound .FlowNode)                           ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, wav) => { wav.ApplyTo(x.SynthBound .ConfigResolver, synthWishes)        ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, wav) => { wav.ApplyTo(x.TapeBound  .Tape)                               ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, wav) => { wav.ApplyTo(x.TapeBound  .TapeConfig)                         ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, wav) => { wav.ApplyTo(x.TapeBound  .TapeActions)                        ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, wav) => { wav.ApplyTo(x.TapeBound  .TapeAction)                         ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, wav) => { wav.ApplyTo(x.BuffBound  .Buff,             courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, wav) => { wav.ApplyTo(x.BuffBound  .AudioFileOutput,  courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, wav) => { wav.ApplyTo(x.Independent.Sample,                     context); Assert(x.Independent.Sample,          test); });
            TestProp((x, wav) => { wav.ApplyTo(x.Independent.AudioFileInfo)                      ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, wav) => { wav.ApplyTo(x.Independent.AudioInfoWish)                      ; Assert(x.Independent.AudioInfoWish,   test); });
        }

        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void WavHeader_ReadWavHeader(string caseKey)
        { 
            Case test = Cases[caseKey];
            SynthWishes synthWishes = null;
            IContext context = null;
            int courtesy = test.CourtesyFrames;

            void TestProp(Action<TestEntities, BuffBoundEntities> setter)
            {
                TestEntities  x = CreateEntities(test);
                AssertIsInit (x, test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;

                using (var y = CreateChangedEntities(test, withDisk: true))
                {
                    AssertIsDest(y, test);
                    setter(x, y.BuffBound);
                }
            }
            
            TestProp((x, y) => { x.SynthBound .SynthWishes    .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, y) => { x.SynthBound .SynthWishes    .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, y) => { x.SynthBound .SynthWishes    .ReadWavHeader(y.SourceStream  )                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, y) => { x.SynthBound .SynthWishes    .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.SynthBound .SynthWishes,     test); });
            TestProp((x, y) => { x.SynthBound .FlowNode       .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, y) => { x.SynthBound .FlowNode       .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, y) => { x.SynthBound .FlowNode       .ReadWavHeader(y.SourceStream  )                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, y) => { x.SynthBound .FlowNode       .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.SynthBound .FlowNode,        test); });
            TestProp((x, y) => { x.SynthBound .ConfigResolver .ReadWavHeader(y.SourceFilePath, synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, y) => { x.SynthBound .ConfigResolver .ReadWavHeader(y.SourceBytes   , synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, y) => { x.SynthBound .ConfigResolver .ReadWavHeader(y.SourceStream  , synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, y) => { x.SynthBound .ConfigResolver .ReadWavHeader(y.BinaryReader  , synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            TestProp((x, y) => { x.TapeBound  .Tape           .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, y) => { x.TapeBound  .Tape           .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, y) => { x.TapeBound  .Tape           .ReadWavHeader(y.SourceStream  )                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, y) => { x.TapeBound  .Tape           .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.TapeBound  .Tape,            test); });
            TestProp((x, y) => { x.TapeBound  .TapeConfig     .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeConfig     .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeConfig     .ReadWavHeader(y.SourceStream  )                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeConfig     .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeActions    .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, y) => { x.TapeBound  .TapeActions    .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, y) => { x.TapeBound  .TapeActions    .ReadWavHeader(y.SourceStream  )                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, y) => { x.TapeBound  .TapeActions    .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.TapeBound  .TapeActions,     test); });
            TestProp((x, y) => { x.TapeBound  .TapeAction     .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeAction     .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeAction     .ReadWavHeader(y.SourceStream  )                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, y) => { x.TapeBound  .TapeAction     .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.TapeBound  .TapeAction,      test); });
            TestProp((x, y) => { x.BuffBound  .Buff           .ReadWavHeader(y.SourceFilePath, courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, y) => { x.BuffBound  .Buff           .ReadWavHeader(y.SourceBytes   , courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, y) => { x.BuffBound  .Buff           .ReadWavHeader(y.SourceStream  , courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, y) => { x.BuffBound  .Buff           .ReadWavHeader(y.BinaryReader  , courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            TestProp((x, y) => { x.BuffBound  .AudioFileOutput.ReadWavHeader(y.SourceFilePath, courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, y) => { x.BuffBound  .AudioFileOutput.ReadWavHeader(y.SourceBytes   , courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, y) => { x.BuffBound  .AudioFileOutput.ReadWavHeader(y.SourceStream  , courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, y) => { x.BuffBound  .AudioFileOutput.ReadWavHeader(y.BinaryReader  , courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            TestProp((x, y) => { x.Independent.Sample         .ReadWavHeader(y.SourceFilePath,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, y) => { x.Independent.Sample         .ReadWavHeader(y.SourceBytes   ,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, y) => { x.Independent.Sample         .ReadWavHeader(y.SourceStream  ,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, y) => { x.Independent.Sample         .ReadWavHeader(y.BinaryReader  ,           context); Assert(x.Independent.Sample,          test); });
            TestProp((x, y) => { x.Independent.AudioFileInfo  .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, y) => { x.Independent.AudioFileInfo  .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, y) => { x.Independent.AudioFileInfo  .ReadWavHeader(y.SourceStream  )                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, y) => { x.Independent.AudioFileInfo  .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp((x, y) => { x.Independent.AudioInfoWish  .ReadWavHeader(y.SourceFilePath)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp((x, y) => { x.Independent.AudioInfoWish  .ReadWavHeader(y.SourceBytes   )                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp((x, y) => { x.Independent.AudioInfoWish  .ReadWavHeader(y.SourceStream  )                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp((x, y) => { x.Independent.AudioInfoWish  .ReadWavHeader(y.BinaryReader  )                   ; Assert(x.Independent.AudioInfoWish,   test); });
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_ReadAudioInfo(string caseKey)
        { 
            Case test = Cases[caseKey];
            
            using (var x = CreateEntities(test, withDisk: true))
            {
                AssertInvariant(x, test);
                Assert(x.BuffBound.SourceFilePath.ReadAudioInfo(), test);
                Assert(x.BuffBound.SourceBytes   .ReadAudioInfo(), test);
                Assert(x.BuffBound.SourceStream  .ReadAudioInfo(), test);
                Assert(x.BuffBound.BinaryReader  .ReadAudioInfo(), test);
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_WriteWavHeader(string caseKey)
        {
            Case test = Cases[caseKey];
            int frameCount = test.FrameCount;
            SynthWishes synthWishes = null;
            TestEntities entities = null;
            BuffBoundEntities binaries = null;
            
            void AssertBytes(Action setter)
            {
                entities = CreateEntities(test);
                synthWishes = entities.SynthBound.SynthWishes;
                AssertInvariant(entities, test);

                using (var changedEntities = CreateChangedEntities(test, withDisk: true))
                {
                    binaries = changedEntities.BuffBound;
                    AssertInvariant(changedEntities, test);
                    
                    setter();
                    
                    Assert(binaries.SourceBytes, test);
                }
            }

            AssertBytes(() => entities.SynthBound .SynthWishes    .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.SynthBound .FlowNode       .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.SynthBound .ConfigResolver .WriteWavHeader(binaries.DestBytes, synthWishes));
            AssertBytes(() => entities.SynthBound .ConfigSection  .WriteWavHeader(binaries.DestBytes)); // TODO: Was expecting exception, since ConfigSection should return defaults.
            AssertBytes(() => entities.TapeBound  .Tape           .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.TapeBound  .TapeConfig     .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.TapeBound  .TapeActions    .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.TapeBound  .TapeAction     .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.BuffBound  .Buff           .WriteWavHeader(binaries.DestBytes, frameCount));
            AssertBytes(() => entities.BuffBound  .AudioFileOutput.WriteWavHeader(binaries.DestBytes, frameCount));
            AssertBytes(() => entities.Independent.Sample         .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.Independent.AudioInfoWish  .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.Independent.AudioFileInfo  .WriteWavHeader(binaries.DestBytes));
            AssertBytes(() => entities.Immutable  .WavHeader      .WriteWavHeader(binaries.DestBytes));
                                 
            AssertBytes(() => binaries.DestBytes                  .WriteWavHeader(entities.Immutable.WavHeader));
            AssertBytes(() => entities.Immutable.WavHeader        .Write(binaries.DestBytes));
            AssertBytes(() => binaries.DestBytes                  .Write(entities.Immutable.WavHeader));

            using (var x = CreateEntities(test, withDisk: true))
            {
                //x.Immutable.WavHeader   .WriteWavHeader(x.BuffBound.DestBytes   );
                x.Immutable.WavHeader   .WriteWavHeader(x.BuffBound.DestFilePath);
                x.Immutable.WavHeader   .WriteWavHeader(x.BuffBound.DestStream  );
                x.Immutable.WavHeader   .WriteWavHeader(x.BuffBound.BinaryWriter);
                //x.BuffBound.DestBytes   .WriteWavHeader(x.Immutable.WavHeader   );
                x.BuffBound.DestFilePath.WriteWavHeader(x.Immutable.WavHeader   );
                x.BuffBound.DestStream  .WriteWavHeader(x.Immutable.WavHeader   );
                x.BuffBound.BinaryWriter.WriteWavHeader(x.Immutable.WavHeader   );
            }
        }
        
        [TestMethod]
        public void WavHeader_EdgeCases()
        {
            var test = new Case { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100 };
            var x = CreateEntities(test, wipeBuff: false);
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;

            // Weird Buff case
                        
            // Buff's too Buff to budge: always returns fixed FrameCount instead of using parameterization.
            AreEqual(100, () => x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount, - Tolerance);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(frameCount    ).FrameCount, - Tolerance);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(123           ).FrameCount, - Tolerance);
            
            // Unbuff the Buff; loosens him up and he'll budge.
            x.BuffBound.Buff.Bytes = null;
            
                                    AreEqual(100, () => x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount, - Tolerance - test.CourtesyFrames);
            ThrowsException(() => { AreEqual(100, () => x.BuffBound.Buff.ToWish(frameCount    ).FrameCount, - Tolerance - test.CourtesyFrames); });
            ThrowsException(() => { AreEqual(100, () => x.BuffBound.Buff.ToWish(123           ).FrameCount, - Tolerance - test.CourtesyFrames); });
        }

        // Getter Assertions
        
        private static void AssertInvariant(TestEntities source, Case test)
        {
            AssertIsInit(source, test);
            AssertIsDest(source, test);
        }

        private static void AssertIsInit(TestEntities source, Case test)
        {
            SamplingRateWishesTests.Assert_All_Getters(source, test.SamplingRate.From);
            BitWishesTests         .Assert_All_Getters(source, test.Bits        .From);
            ChannelsWishesTests    .Assert_All_Getters(source, test.Channels    .From);
            FrameCountWishesTests  .Assert_All_Getters(source, test.FrameCount  .From);
        }

        private static void AssertIsDest(TestEntities source, Case test)
        {
            SamplingRateWishesTests.Assert_All_Getters(source, test.SamplingRate.To);
            BitWishesTests         .Assert_All_Getters(source, test.Bits        .To);
            ChannelsWishesTests    .Assert_All_Getters(source, test.Channels    .To);
            FrameCountWishesTests  .Assert_All_Getters(source, test.FrameCount  .To);
        }

        private void Assert(SynthWishes entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits);
            AreEqual(test.Channels,     () => entity.GetChannels);
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.GetFrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(FlowNode entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits);
            AreEqual(test.Channels,     () => entity.GetChannels);
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.GetFrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(ConfigResolverAccessor entity, Case test, SynthWishes synthWishes)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits);
            AreEqual(test.Channels,     () => entity.GetChannels);
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.GetFrameCount(synthWishes), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(Tape entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(TapeConfig entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits);
            AreEqual(test.Channels,     () => entity.Channels);
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(TapeActions entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(TapeAction entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(Buff entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            int courtesyFrames = test.CourtesyFrames;
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFrames), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(AudioFileOutput entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            int courtesyFrames = test.CourtesyFrames;
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFrames), - Tolerance - test.CourtesyFrames);
        }
        
        private void Assert(Sample entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            // Sample ignores FrameCount changes—either its own value or 0.
            //AreEqual(test.FrameCount, () => entity.FrameCount(), -Tolerance);
        }

        private void Assert(AudioFileInfo entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        private void Assert(AudioInfoWish entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits);
            AreEqual(test.Channels,     () => entity.Channels);
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount, - Tolerance - test.CourtesyFrames);
        }

        private void Assert(WavHeaderStruct entity, Case test)
        {
            IsNotNull(() => test);
            AreEqual(test.Bits,         () => entity.Bits());
            AreEqual(test.Channels,     () => entity.Channels());
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }
        
        void Assert(string entity, Case test)
        {
            if (!Has(test)) throw new NullException(() => test);
            if (entity == null) throw new NullException(() => entity);
            Assert(entity.ReadWavHeader(), test);
        }
        
        void Assert(byte[] entity, Case test)
        {
            if (test  == null) throw new NullException(() => test);
            if (entity == null) throw new NullException(() => entity);
            Assert(entity.ReadWavHeader(), test);
        }
        
        void Assert(Stream entity, Case test)
        {
            if (test  == null) throw new NullException(() => test);
            if (entity == null) throw new NullException(() => entity);
            Assert(entity.ReadWavHeader(), test);
        }
        
        void Assert(BinaryWriter entity, Case test)
        {
            if (test  == null) throw new NullException(() => test);
            if (entity == null) throw new NullException(() => entity);
            if (entity.BaseStream == null) throw new NullException(() => entity.BaseStream);
            Assert(entity.BaseStream.ReadWavHeader(), test);
        }
        
        // Helpers
        
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression) => AreEqual<int>(expected, actualExpression);
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression, int delta) => AssertWishes.AreEqual(expected, actualExpression, delta);
        private static void AreEqual(int expected, int actual) => AreEqual<int>(expected, actual);
    }
}
