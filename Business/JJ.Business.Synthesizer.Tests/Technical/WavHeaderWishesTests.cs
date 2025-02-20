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
using static JJ.Business.Synthesizer.Tests.Accessors.WavHeaderWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntityEnum;
using static JJ.Business.Synthesizer.Wishes.WavHeaderWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static System.Threading.Thread;
// ReSharper disable ArrangeStaticMemberQualifier
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

        static Case DefaultsCase { get; } = new Case
        {
            SamplingRate   = 48000,
            Bits           = 32,
            Channels       = 1,
            CourtesyFrames = 4,
            FrameCount     = 48000+4
        };
                
        static Case NonDefaultCase { get; } = new Case
        {
            SamplingRate   = 44100,
            Bits           = 16,
            Channels       = 2,
            CourtesyFrames = 5,
            FrameCount     = 100
        };
        
        static Case EdgeCase { get; } = new Case
        {
            SamplingRate   = 48000,
            Bits           = 32,
            Channels       = 2,
            CourtesyFrames = 3,
            FrameCount     = 100
        };
            

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
        
        private TestEntities CreateModifiedEntities(Case test, bool wipeBuff = true, bool withDisk = false)
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
            var test = Cases[caseKey];
            var zeroFramesCase = new Case
            {
                SamplingRate   = test.SamplingRate,
                Bits           = test.Bits,
                Channels       = test.Channels,
                CourtesyFrames = test.CourtesyFrames,
                FrameCount     = 0
            };
            
            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            
            int frameCount = test.FrameCount;
            int courtesy = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            
            Assert(x.SynthBound.SynthWishes         .ToWish(),                        test);
            Assert(x.SynthBound.FlowNode            .ToWish(),                        test);
            Assert(x.SynthBound.ConfigResolver      .ToWish(synthWishes),             test);
            Assert(x.SynthBound.ConfigSection       .ToWish(),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            Assert(x.TapeBound.Tape                 .ToWish(),                        test);
            Assert(x.TapeBound.TapeConfig           .ToWish(),                        test);
            Assert(x.TapeBound.TapeActions          .ToWish(),                        test);
            Assert(x.TapeBound.TapeAction           .ToWish(),                        test);
            Assert(x.BuffBound.Buff                 .ToWish(),              zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(x.BuffBound.AudioFileOutput      .ToWish(),              zeroFramesCase);
            Assert(x.BuffBound.Buff                 .ToWish(courtesy),                test);
            Assert(x.BuffBound.AudioFileOutput      .ToWish(courtesy),                test);
            Assert(x.BuffBound.Buff                 .ToWish().FrameCount(frameCount), test);
            Assert(x.BuffBound.AudioFileOutput      .ToWish().FrameCount(frameCount), test);
            Assert(x.Independent.Sample             .ToWish(),                        test);
            Assert(x.Independent.AudioFileInfo      .ToWish(),                        test);
            Assert(x.Immutable.WavHeader            .ToWish(),                        test);
            Assert(x.Immutable.InfoTupleWithInts    .ToWish(),                        test);
            Assert(x.Immutable.InfoTupleWithType    .ToWish(),                        test);
            Assert(x.Immutable.InfoTupleWithEnums   .ToWish(),                        test);
            Assert(x.Immutable.InfoTupleWithEntities.ToWish(),                        test);
            Assert(ToWish(x.SynthBound.SynthWishes          ),                        test);
            Assert(ToWish(x.SynthBound.FlowNode             ),                        test);
            Assert(ToWish(x.SynthBound.ConfigResolver,      synthWishes),             test);
            Assert(ToWish(x.SynthBound.ConfigSection        ),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            Assert(ToWish(x.TapeBound.Tape                  ),                        test);
            Assert(ToWish(x.TapeBound.TapeConfig            ),                        test);
            Assert(ToWish(x.TapeBound.TapeActions           ),                        test);
            Assert(ToWish(x.TapeBound.TapeAction            ),                        test);
            Assert(ToWish(x.BuffBound.Buff                  ),              zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(ToWish(x.BuffBound.AudioFileOutput       ),              zeroFramesCase);
            Assert(ToWish(x.BuffBound.Buff,                 courtesy),                test);
            Assert(ToWish(x.BuffBound.AudioFileOutput,      courtesy),                test);
            Assert(ToWish(x.BuffBound.Buff                  ).FrameCount(frameCount), test);
            Assert(ToWish(x.BuffBound.AudioFileOutput       ).FrameCount(frameCount), test);
            Assert(ToWish(x.Independent.Sample              ),                        test);
            Assert(ToWish(x.Independent.AudioFileInfo       ),                        test);
            Assert(ToWish(x.Immutable.WavHeader             ),                        test);
            Assert(ToWish(x.Immutable.InfoTupleWithInts     ),                        test);
            Assert(ToWish(x.Immutable.InfoTupleWithType     ),                        test);
            Assert(ToWish(x.Immutable.InfoTupleWithEnums    ),                        test);
            Assert(ToWish(x.Immutable.InfoTupleWithEntities ),                        test);
            Assert(WavHeaderWishes.ToWish(x.SynthBound.SynthWishes         ),                        test);
            Assert(WavHeaderWishes.ToWish(x.SynthBound.FlowNode            ),                        test);
            Assert(WavHeaderWishesAccessor.ToWish(x.SynthBound.ConfigResolver,synthWishes),          test);
            Assert(WavHeaderWishesAccessor.ToWish(x.SynthBound.ConfigSection),                DefaultsCase);// By Design: Mocked ConfigSection has default settings.
            Assert(WavHeaderWishes.ToWish(x.TapeBound.Tape                 ),                        test);
            Assert(WavHeaderWishes.ToWish(x.TapeBound.TapeConfig           ),                        test);
            Assert(WavHeaderWishes.ToWish(x.TapeBound.TapeActions          ),                        test);
            Assert(WavHeaderWishes.ToWish(x.TapeBound.TapeAction           ),                        test);
            Assert(WavHeaderWishes.ToWish(x.BuffBound.Buff                 ),              zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(WavHeaderWishes.ToWish(x.BuffBound.AudioFileOutput      ),              zeroFramesCase);
            Assert(WavHeaderWishes.ToWish(x.BuffBound.Buff,                courtesy),                test);
            Assert(WavHeaderWishes.ToWish(x.BuffBound.AudioFileOutput,     courtesy),                test);
            Assert(WavHeaderWishes.ToWish(x.BuffBound.Buff                 ).FrameCount(frameCount), test);
            Assert(WavHeaderWishes.ToWish(x.BuffBound.AudioFileOutput      ).FrameCount(frameCount), test);
            Assert(WavHeaderWishes.ToWish(x.Independent.Sample             ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Independent.AudioFileInfo      ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Immutable.WavHeader            ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Immutable.InfoTupleWithInts    ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Immutable.InfoTupleWithType    ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Immutable.InfoTupleWithEnums   ),                        test);
            Assert(WavHeaderWishes.ToWish(x.Immutable.InfoTupleWithEntities),                        test);

            if      (test.Bits ==  8) Assert(x.Immutable.InfoTupleWithoutBits.ToWish<byte> (), test);
            else if (test.Bits == 16) Assert(x.Immutable.InfoTupleWithoutBits.ToWish<short>(), test);
            else if (test.Bits == 32) Assert(x.Immutable.InfoTupleWithoutBits.ToWish<float>(), test); else AssertBits(test.Bits);
            if      (test.Bits ==  8) Assert(ToWish<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) Assert(ToWish<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) Assert(ToWish<float>(x.Immutable.InfoTupleWithoutBits), test); else AssertBits(test.Bits);
            if      (test.Bits ==  8) Assert(WavHeaderWishes.ToWish<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) Assert(WavHeaderWishes.ToWish<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) Assert(WavHeaderWishes.ToWish<float>(x.Immutable.InfoTupleWithoutBits), test); else AssertBits(test.Bits);
        }

        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void WavHeader_FromWish(string caseKey)
        { 
            Case test = Cases[caseKey];
            int courtesy = test.CourtesyFrames;
            var info = new AudioInfoWish
            {
                Bits         = test.Bits,
                Channels     = test.Channels,
                SamplingRate = test.SamplingRate,
                FrameCount   = test.FrameCount  
            };
            
            SynthWishes synthWishes;
            IContext context;

            void TestProp(Action<TestEntities> setter)
            {
                TestEntities  x = CreateEntities(test);
                AssertIsInit (x, test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;
                
                setter(x);
            }
            
            TestProp(x => { x.SynthBound.SynthWishes     .FromWish(info)                    ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { x.SynthBound.FlowNode        .FromWish(info)                    ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { x.SynthBound.ConfigResolver  .FromWish(info,        synthWishes); Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { x.TapeBound.Tape             .FromWish(info)                    ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { x.TapeBound.TapeConfig       .FromWish(info)                    ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { x.TapeBound.TapeActions      .FromWish(info)                    ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { x.TapeBound.TapeAction       .FromWish(info)                    ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { x.BuffBound.Buff             .FromWish(info,  courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { x.BuffBound.AudioFileOutput  .FromWish(info,  courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { x.Independent.Sample         .FromWish(info,            context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { x.Independent.AudioFileInfo  .FromWish(info)                    ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { x.Independent.AudioInfoWish  .FromWish(info)                    ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp(x => { info.ApplyTo(x.SynthBound.SynthWishes)                          ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { info.ApplyTo(x.SynthBound.FlowNode)                             ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { info.ApplyTo(x.SynthBound.ConfigResolver,           synthWishes); Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { info.ApplyTo(x.TapeBound.Tape)                                  ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { info.ApplyTo(x.TapeBound.TapeConfig)                            ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { info.ApplyTo(x.TapeBound.TapeActions)                           ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { info.ApplyTo(x.TapeBound.TapeAction)                            ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { info.ApplyTo(x.BuffBound.Buff,                courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { info.ApplyTo(x.BuffBound.AudioFileOutput,     courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { info.ApplyTo(x.Independent.Sample,                      context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { info.ApplyTo(x.Independent.AudioFileInfo)                       ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { info.ApplyTo(x.Independent.AudioInfoWish)                       ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp(x => { FromWish(x.SynthBound.SynthWishes,      info)                   ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { FromWish(x.SynthBound.FlowNode,         info)                   ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { FromWish(x.SynthBound.ConfigResolver,   info,       synthWishes); Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { FromWish(x.TapeBound.Tape,              info)                   ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { FromWish(x.TapeBound.TapeConfig,        info)                   ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { FromWish(x.TapeBound.TapeActions,       info)                   ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { FromWish(x.TapeBound.TapeAction,        info)                   ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { FromWish(x.BuffBound.Buff,              info, courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { FromWish(x.BuffBound.AudioFileOutput,   info, courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { FromWish(x.Independent.Sample,          info,           context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { FromWish(x.Independent.AudioFileInfo,   info)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { FromWish(x.Independent.AudioInfoWish,   info)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp(x => { ApplyTo(info, x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { ApplyTo(info, x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { ApplyTo(info, x.SynthBound.ConfigResolver,          synthWishes); Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { ApplyTo(info, x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { ApplyTo(info, x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { ApplyTo(info, x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { ApplyTo(info, x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { ApplyTo(info, x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { ApplyTo(info, x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { ApplyTo(info, x.Independent.Sample,                     context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { ApplyTo(info, x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { ApplyTo(info, x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.SynthBound.SynthWishes,      info)                   ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.SynthBound.FlowNode,         info)                   ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { WavHeaderWishesAccessor.FromWish(x.SynthBound.ConfigResolver,   info, synthWishes)      ; Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.TapeBound.Tape,              info)                   ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.TapeBound.TapeConfig,        info)                   ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.TapeBound.TapeActions,       info)                   ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.TapeBound.TapeAction,        info)                   ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.BuffBound.Buff,              info, courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.BuffBound.AudioFileOutput,   info, courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.Independent.Sample,          info,           context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.Independent.AudioFileInfo,   info)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { WavHeaderWishes        .FromWish(x.Independent.AudioInfoWish,   info)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,      test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,         test); });
            TestProp(x => { WavHeaderWishesAccessor.ApplyTo(info, x.SynthBound.ConfigResolver,          synthWishes); Assert(x.SynthBound.ConfigResolver,   test, synthWishes); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,              test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,        test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,       test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,        test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,              test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput,   test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.Independent.Sample,                     context); Assert(x.Independent.Sample,          test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo,   test); });
            TestProp(x => { WavHeaderWishes        .ApplyTo(info, x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish,   test); });
        }

        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_ToWavHeader(string caseKey)
        { 
            Case test = Cases[caseKey];
            var zeroFramesCase = new Case
            {
                SamplingRate   = test.SamplingRate,
                Bits           = test.Bits,
                Channels       = test.Channels,
                CourtesyFrames = test.CourtesyFrames,
                FrameCount     = 0
            };

            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            
            int frameCount  = test.FrameCount;
            int courtesy    = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
                 
            Assert(x.SynthBound.SynthWishes         .ToWavHeader(),                                  test);
            Assert(x.SynthBound.FlowNode            .ToWavHeader(),                                  test);
            Assert(x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes),                       test);
            Assert(x.SynthBound.ConfigSection       .ToWavHeader(),                          DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            Assert(x.TapeBound.Tape                 .ToWavHeader(),                                  test);
            Assert(x.TapeBound.TapeConfig           .ToWavHeader(),                                  test);
            Assert(x.TapeBound.TapeActions          .ToWavHeader(),                                  test);
            Assert(x.TapeBound.TapeAction           .ToWavHeader(),                                  test);
            Assert(x.BuffBound.Buff                 .ToWavHeader(),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(x.BuffBound.Buff                 .ToWavHeader(courtesy),                          test);
            Assert(x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount, courtesy), test);
            Assert(x.BuffBound.AudioFileOutput      .ToWavHeader(),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy),                          test);
            Assert(x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount, courtesy), test);
            Assert(x.Independent.Sample             .ToWavHeader(),                                  test);
            Assert(x.Independent.AudioFileInfo      .ToWavHeader(),                                  test);
            Assert(x.Immutable.InfoTupleWithInts    .ToWavHeader(),                                  test);
            Assert(x.Immutable.InfoTupleWithType    .ToWavHeader(),                                  test);
            Assert(x.Immutable.InfoTupleWithEnums   .ToWavHeader(),                                  test);
            Assert(x.Immutable.InfoTupleWithEntities.ToWavHeader(),                                  test);
            Assert(ToWavHeader(x.SynthBound.SynthWishes          ),                                  test);
            Assert(ToWavHeader(x.SynthBound.FlowNode             ),                                  test);
            Assert(ToWavHeader(x.SynthBound.ConfigResolver       ,synthWishes),                      test);
            Assert(ToWavHeader(x.SynthBound.ConfigSection        ),                          DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            Assert(ToWavHeader(x.TapeBound.Tape                  ),                                  test);
            Assert(ToWavHeader(x.TapeBound.TapeConfig            ),                                  test);
            Assert(ToWavHeader(x.TapeBound.TapeActions           ),                                  test);
            Assert(ToWavHeader(x.TapeBound.TapeAction            ),                                  test);
            Assert(ToWavHeader(x.BuffBound.Buff                  ),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(ToWavHeader(x.BuffBound.Buff                  ,courtesy),                         test);
            Assert(ToWavHeader(x.BuffBound.Buff                  ).FrameCount(frameCount, courtesy), test);
            Assert(ToWavHeader(x.BuffBound.AudioFileOutput       ),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(ToWavHeader(x.BuffBound.AudioFileOutput       ,courtesy),                         test);
            Assert(ToWavHeader(x.BuffBound.AudioFileOutput       ).FrameCount(frameCount, courtesy), test);
            Assert(ToWavHeader(x.Independent.Sample              ),                                  test);
            Assert(ToWavHeader(x.Independent.AudioFileInfo       ),                                  test);
            Assert(ToWavHeader(x.Immutable.InfoTupleWithInts     ),                                  test);
            Assert(ToWavHeader(x.Immutable.InfoTupleWithType     ),                                  test);
            Assert(ToWavHeader(x.Immutable.InfoTupleWithEnums    ),                                  test);
            Assert(ToWavHeader(x.Immutable.InfoTupleWithEntities ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.SynthBound.SynthWishes           ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.SynthBound.FlowNode              ),                                  test);
            Assert(WavHeaderWishesAccessor.ToWavHeader(x.SynthBound.ConfigResolver, synthWishes),                     test);
            Assert(WavHeaderWishesAccessor.ToWavHeader(x.SynthBound.ConfigSection ),                          DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            Assert(WavHeaderWishes.ToWavHeader(x.TapeBound.Tape                   ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.TapeBound.TapeConfig             ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.TapeBound.TapeActions            ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.TapeBound.TapeAction             ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.Buff                   ),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.Buff                   ,courtesy),                         test);
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.Buff                   ).FrameCount(frameCount, courtesy), test);
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.AudioFileOutput        ),                        zeroFramesCase); // By Design: FrameCount stays 0 without courtesyBytes
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.AudioFileOutput        ,courtesy),                         test);
            Assert(WavHeaderWishes.ToWavHeader(x.BuffBound.AudioFileOutput        ).FrameCount(frameCount, courtesy), test);
            Assert(WavHeaderWishes.ToWavHeader(x.Independent.Sample               ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.Independent.AudioFileInfo        ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.Immutable.InfoTupleWithInts      ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.Immutable.InfoTupleWithType      ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.Immutable.InfoTupleWithEnums     ),                                  test);
            Assert(WavHeaderWishes.ToWavHeader(x.Immutable.InfoTupleWithEntities  ),                                  test);
            if      (test.Bits ==  8) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> (), test);
            else if (test.Bits == 16) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>(), test);
            else if (test.Bits == 32) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>(), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
            if      (test.Bits ==  8) Assert(ToWavHeader<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) Assert(ToWavHeader<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) Assert(ToWavHeader<float>(x.Immutable.InfoTupleWithoutBits), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
            if      (test.Bits ==  8) Assert(WavHeaderWishes.ToWavHeader<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) Assert(WavHeaderWishes.ToWavHeader<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) Assert(WavHeaderWishes.ToWavHeader<float>(x.Immutable.InfoTupleWithoutBits), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
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
            
            TestProp((x, wav) => { x.SynthBound.SynthWishes   .ApplyWavHeader(wav )                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { x.SynthBound.FlowNode      .ApplyWavHeader(wav )                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { x.SynthBound.ConfigResolver.ApplyWavHeader(wav ,       synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { x.TapeBound.Tape           .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { x.TapeBound.TapeConfig     .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { x.TapeBound.TapeActions    .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { x.TapeBound.TapeAction     .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { x.BuffBound.Buff           .ApplyWavHeader(wav , courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { x.BuffBound.AudioFileOutput.ApplyWavHeader(wav , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { x.Independent.Sample       .ApplyWavHeader(wav ,           context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { x.Independent.AudioFileInfo.ApplyWavHeader(wav )                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { x.Independent.AudioInfoWish.ApplyWavHeader(wav )                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.SynthWishes    )                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.FlowNode       )                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.ConfigResolver ,       synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.Tape            )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeConfig      )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeActions     )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeAction      )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.BuffBound.Buff            , courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.BuffBound.AudioFileOutput , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.Sample        ,           context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.AudioFileInfo )                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.AudioInfoWish )                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.SynthWishes   , wav)                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.FlowNode      , wav)                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.ConfigResolver, wav,       synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.Tape           , wav)                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeConfig     , wav)                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeActions    , wav)                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeAction     , wav)                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { ApplyWavHeader(x.BuffBound.Buff           , wav, courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { ApplyWavHeader(x.BuffBound.AudioFileOutput, wav, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.Sample       , wav,           context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.AudioFileInfo, wav)                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.AudioInfoWish, wav)                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.SynthWishes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.FlowNode      )                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.ConfigResolver, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.Tape           )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeConfig     )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeActions    )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeAction     )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.BuffBound.Buff           , courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.BuffBound.AudioFileOutput, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.Sample       ,           context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.AudioFileInfo)                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.AudioInfoWish)                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.SynthBound.SynthWishes   , wav)                     ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.SynthBound.FlowNode      , wav)                     ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { WavHeaderWishesAccessor.ApplyWavHeader(x.SynthBound.ConfigResolver, wav, synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.Tape           , wav)                     ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeConfig     , wav)                     ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeActions    , wav)                     ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeAction     , wav)                     ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.BuffBound.Buff           , wav,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.BuffBound.AudioFileOutput, wav,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.Independent.Sample       , wav,             context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.Independent.AudioFileInfo, wav)                     ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.Independent.AudioInfoWish, wav)                     ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.SynthBound.SynthWishes   )                     ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.SynthBound.FlowNode      )                     ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { WavHeaderWishesAccessor.ApplyWavHeader(wav, x.SynthBound.ConfigResolver, synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.TapeBound.Tape           )                     ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.TapeBound.TapeConfig     )                     ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.TapeBound.TapeActions    )                     ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.TapeBound.TapeAction     )                     ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.Independent.AudioFileInfo)                     ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(wav, x.Independent.AudioInfoWish)                     ; Assert(x.Independent.AudioInfoWish, test); });
        }

        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void WavHeader_ReadWavHeader(string caseKey)
        {
            Case              test        = Cases[caseKey];
            int               courtesy    = test.CourtesyFrames;
            SynthWishes       synthWishes = null;
            IContext          context     = null;
            BuffBoundEntities binaries        = null;
            
            using (TestEntities modifiedEntities = CreateModifiedEntities(test, withDisk: true))
            {
                AssertIsDest(modifiedEntities, test);
                binaries = modifiedEntities.BuffBound;
                
                void TestProp(Action<TestEntities> setter)
                {
                    TestEntities entities = CreateEntities(test);
                    AssertIsInit(entities, test);
                    
                    synthWishes = entities.SynthBound.SynthWishes;
                    context     = entities.SynthBound.Context;
                    
                    setter(entities);
                    
                    binaries.SourceStream.Position = 0;
                    binaries.BinaryReader.BaseStream.Position = 0;
                }
                
                TestProp(x => { x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { x.SynthBound.SynthWishes     .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { x.SynthBound.FlowNode        .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceFilePath , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceBytes    , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceStream   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.BinaryReader   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.TapeBound.Tape             .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { x.TapeBound.Tape             .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { x.TapeBound.Tape             .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { x.TapeBound.Tape             .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { x.TapeBound.TapeConfig       .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { x.TapeBound.TapeActions      .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { x.TapeBound.TapeAction       .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceFilePath , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceBytes    , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceStream   , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.BinaryReader   , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceFilePath , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceBytes    , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceStream   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.BinaryReader   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceFilePath ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceBytes    ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceStream   ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.BinaryReader   ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { x.Independent.AudioFileInfo  .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceFilePath )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceBytes    )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceStream   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { x.Independent.AudioInfoWish  .ReadWavHeader(binaries.BinaryReader   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.SynthBound.SynthWishes      )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.SynthBound.FlowNode         )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.SynthBound.ConfigResolver   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.SynthBound.ConfigResolver   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.SynthBound.ConfigResolver   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.SynthBound.ConfigResolver   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.TapeBound.Tape              )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.TapeBound.TapeConfig        )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.TapeBound.TapeActions       )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.TapeBound.TapeAction        )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.BuffBound.Buff              , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.BuffBound.Buff              , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.BuffBound.Buff              , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.BuffBound.Buff              , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.BuffBound.AudioFileOutput   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.BuffBound.AudioFileOutput   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.BuffBound.AudioFileOutput   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.BuffBound.AudioFileOutput   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.Sample          ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.Sample          ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.Sample          ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.Sample          ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceFilePath)                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceBytes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceStream  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes     , binaries.BinaryReader  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceFilePath)                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceBytes   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceStream  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode        , binaries.BinaryReader  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceFilePath, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceBytes   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceStream  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.BinaryReader  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape             , binaries.SourceFilePath)                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape             , binaries.SourceBytes   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape             , binaries.SourceStream  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape             , binaries.BinaryReader  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig       , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions      , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction       , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff             , binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff             , binaries.SourceBytes   , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff             , binaries.SourceStream  , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff             , binaries.BinaryReader  , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceBytes   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceStream  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.BinaryReader  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample         , binaries.SourceFilePath,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample         , binaries.SourceBytes   ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample         , binaries.SourceStream  ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample         , binaries.BinaryReader  ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceFilePath)                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceBytes   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceStream  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo  , binaries.BinaryReader  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceFilePath)                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceBytes   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceStream  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish  , binaries.BinaryReader  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes   , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream  , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader  , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceFilePath)                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceBytes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes     , binaries.SourceStream  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes     , binaries.BinaryReader  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceFilePath)                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceBytes   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode        , binaries.SourceStream  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode        , binaries.BinaryReader  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceFilePath, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceBytes   , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.SourceStream  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver  , binaries.BinaryReader  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape             , binaries.SourceFilePath)                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape             , binaries.SourceBytes   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape             , binaries.SourceStream  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape             , binaries.BinaryReader  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig       , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig       , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions      , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions      , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction       , binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction       , binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff             , binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff             , binaries.SourceBytes   , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff             , binaries.SourceStream  , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff             , binaries.BinaryReader  , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceBytes   , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.SourceStream  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput  , binaries.BinaryReader  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample         , binaries.SourceFilePath,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample         , binaries.SourceBytes   ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample         , binaries.SourceStream  ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample         , binaries.BinaryReader  ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceFilePath)                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceBytes   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo  , binaries.SourceStream  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo  , binaries.BinaryReader  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceFilePath)                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceBytes   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish  , binaries.SourceStream  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish  , binaries.BinaryReader  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceBytes   , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceStream  , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.BinaryReader  , x.SynthBound.ConfigResolver  , synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.BuffBound.Buff             , courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.BuffBound.AudioFileOutput  , courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.Independent.Sample         ,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes   , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream  , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader  , x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_ReadAudioInfo(string caseKey)
        { 
            Case test = Cases[caseKey];
            
            using (var x = CreateEntities(test, withDisk: true))
            {
                AssertInvariant(x, test);
                
                void AssertRead(Func<AudioInfoWish> getter)
                {
                    AudioInfoWish info = getter();
                    Assert(info, test);
                    
                    x.BuffBound.SourceStream.Position = 0;
                    x.BuffBound.BinaryReader.BaseStream.Position = 0;
                }
                
                AssertRead(() => x.BuffBound.SourceFilePath.ReadAudioInfo());
                AssertRead(() => x.BuffBound.SourceBytes   .ReadAudioInfo());
                AssertRead(() => x.BuffBound.SourceStream  .ReadAudioInfo());
                AssertRead(() => x.BuffBound.BinaryReader  .ReadAudioInfo());
                AssertRead(() => ReadAudioInfo(x.BuffBound.SourceFilePath));
                AssertRead(() => ReadAudioInfo(x.BuffBound.SourceBytes   ));
                AssertRead(() => ReadAudioInfo(x.BuffBound.SourceStream  ));
                AssertRead(() => ReadAudioInfo(x.BuffBound.BinaryReader  ));
                AssertRead(() => WavHeaderWishes.ReadAudioInfo(x.BuffBound.SourceFilePath));
                AssertRead(() => WavHeaderWishes.ReadAudioInfo(x.BuffBound.SourceBytes   ));
                AssertRead(() => WavHeaderWishes.ReadAudioInfo(x.BuffBound.SourceStream  ));
                AssertRead(() => WavHeaderWishes.ReadAudioInfo(x.BuffBound.BinaryReader  ));
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_WriteWavHeader(string caseKey)
        {
            Case test = Cases[caseKey];
            
            TestEntities entities = CreateEntities(test);
            AssertInvariant(entities, test);
            
            BuffBoundEntities binaries = null;
            var synthWishes = entities.SynthBound.SynthWishes;
            int courtesy = test.CourtesyFrames;
            int frameCount = test.FrameCount;
            
            void AssertSetter(Action setter, TestEntityEnum entity)
            {
                using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
                {
                    binaries = changedEntities.BuffBound;
                    AssertInvariant(changedEntities, test);
                    
                    setter();
                    
                    if (entity == ForDestFilePath) Assert(binaries.DestFilePath, test);
                    if (entity == ForDestBytes)    Assert(binaries.DestBytes,    test);
                    if (entity == ForDestStream)   Assert(binaries.DestStream,   test);
                    if (entity == ForBinaryWriter) Assert(binaries.BinaryWriter, test);
                }
            }

            AssertSetter(() => entities.SynthBound.SynthWishes     .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.SynthBound.SynthWishes     .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.SynthBound.SynthWishes     .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.SynthBound.SynthWishes     .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.SynthBound.FlowNode        .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.SynthBound.FlowNode        .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.SynthBound.FlowNode        .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.SynthBound.FlowNode        .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.SynthBound.ConfigResolver  .WriteWavHeader(binaries.DestFilePath, synthWishes), ForDestFilePath);
            AssertSetter(() => entities.SynthBound.ConfigResolver  .WriteWavHeader(binaries.DestBytes,    synthWishes), ForDestBytes   );
            AssertSetter(() => entities.SynthBound.ConfigResolver  .WriteWavHeader(binaries.DestStream,   synthWishes), ForDestStream  );
            AssertSetter(() => entities.SynthBound.ConfigResolver  .WriteWavHeader(binaries.BinaryWriter, synthWishes), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound.Tape             .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound.Tape             .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound.Tape             .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound.Tape             .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound.TapeConfig       .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound.TapeConfig       .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound.TapeConfig       .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound.TapeConfig       .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound.TapeActions      .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound.TapeActions      .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound.TapeActions      .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound.TapeActions      .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound.TapeAction       .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound.TapeAction       .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound.TapeAction       .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound.TapeAction       .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.BuffBound.Buff             .WriteWavHeader(binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => entities.BuffBound.Buff             .WriteWavHeader(binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => entities.BuffBound.Buff             .WriteWavHeader(binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => entities.BuffBound.Buff             .WriteWavHeader(binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => entities.BuffBound.AudioFileOutput  .WriteWavHeader(binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => entities.BuffBound.AudioFileOutput  .WriteWavHeader(binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => entities.BuffBound.AudioFileOutput  .WriteWavHeader(binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => entities.BuffBound.AudioFileOutput  .WriteWavHeader(binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => entities.Independent.Sample         .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Independent.Sample         .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Independent.Sample         .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Independent.Sample         .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.Independent.AudioInfoWish  .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Independent.AudioInfoWish  .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Independent.AudioInfoWish  .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Independent.AudioInfoWish  .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.Independent.AudioFileInfo  .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Independent.AudioFileInfo  .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Independent.AudioFileInfo  .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Independent.AudioFileInfo  .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.Immutable.WavHeader        .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Immutable.WavHeader        .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Immutable.WavHeader        .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Immutable.WavHeader        .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.Immutable.WavHeader        .Write         (binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Immutable.WavHeader        .Write         (binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Immutable.WavHeader        .Write         (binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Immutable.WavHeader        .Write         (binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.SynthBound.FlowNode                     ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.TapeBound.Tape                          ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.TapeBound.Tape                          ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.TapeBound.Tape                          ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.TapeBound.Tape                          ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeActions                   ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeAction                    ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.Independent.Sample                      ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.Independent.Sample                      ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.Independent.Sample                      ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.Independent.Sample                      ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.Independent.AudioInfoWish               ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.Independent.AudioFileInfo               ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(entities.Immutable.WavHeader                     ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.Write         (entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .Write         (entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .Write         (entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.Write         (entities.Immutable.WavHeader                     ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.FlowNode,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.ConfigResolver,   binaries.DestFilePath, synthWishes), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.SynthBound.ConfigResolver,   binaries.DestBytes,    synthWishes), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.ConfigResolver,   binaries.DestStream,   synthWishes), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.SynthBound.ConfigResolver,   binaries.BinaryWriter, synthWishes), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.Tape,              binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.Tape,              binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.Tape,              binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.Tape,              binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeActions,       binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.TapeBound.TapeAction,        binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.BuffBound.Buff,              binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.BuffBound.Buff,              binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.BuffBound.Buff,              binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.BuffBound.Buff,              binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.Independent.Sample,          binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.Independent.Sample,          binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.Independent.Sample,          binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.Independent.Sample,          binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(entities.Immutable.WavHeader,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => Write         (entities.Immutable.WavHeader,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => Write         (entities.Immutable.WavHeader,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => Write         (entities.Immutable.WavHeader,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => Write         (entities.Immutable.WavHeader,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.SynthBound.SynthWishes                  ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.SynthBound.SynthWishes                  ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.SynthBound.SynthWishes                  ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.SynthWishes                  ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.SynthBound.FlowNode                     ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.SynthBound.FlowNode                     ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.SynthBound.FlowNode                     ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.FlowNode                     ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.SynthBound.ConfigResolver,   synthWishes), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.SynthBound.ConfigResolver,   synthWishes), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.SynthBound.ConfigResolver,   synthWishes), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.ConfigResolver,   synthWishes), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.TapeBound.Tape                          ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.TapeBound.Tape                          ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.TapeBound.Tape                          ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.Tape                          ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeConfig                    ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeConfig                    ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeConfig                    ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeConfig                    ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeActions                   ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeActions                   ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeActions                   ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeActions                   ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeAction                    ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeAction                    ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeAction                    ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeAction                    ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.BuffBound.Buff,                 courtesy), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.BuffBound.Buff,                 courtesy), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.BuffBound.Buff,                 courtesy), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.BuffBound.Buff,                 courtesy), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.Independent.Sample                      ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.Independent.Sample                      ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.Independent.Sample                      ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.Independent.Sample                      ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.Independent.AudioInfoWish               ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.Independent.AudioInfoWish               ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.Independent.AudioInfoWish               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.Independent.AudioInfoWish               ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.Independent.AudioFileInfo               ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.Independent.AudioFileInfo               ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.Independent.AudioFileInfo               ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.Independent.AudioFileInfo               ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter);
            AssertSetter(() => Write         (binaries.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => Write         (binaries.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => Write         (binaries.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => Write         (binaries.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, binaries.DestFilePath, synthWishes), ForDestFilePath);
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, binaries.DestBytes,    synthWishes), ForDestBytes   );
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, binaries.DestStream,   synthWishes), ForDestStream  );
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, binaries.BinaryWriter, synthWishes), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestFilePath,    courtesy), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestBytes,       courtesy), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.DestStream,      courtesy), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   binaries.BinaryWriter,    courtesy), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.SynthBound.SynthWishes                  ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.SynthBound.SynthWishes                  ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.SynthBound.SynthWishes                  ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.SynthWishes                  ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.SynthBound.FlowNode                     ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.SynthBound.FlowNode                     ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.SynthBound.FlowNode                     ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.FlowNode                     ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(binaries.DestFilePath, entities.SynthBound.ConfigResolver, synthWishes), ForDestFilePath);
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(binaries.DestBytes,    entities.SynthBound.ConfigResolver, synthWishes), ForDestBytes   );
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(binaries.DestStream,   entities.SynthBound.ConfigResolver, synthWishes), ForDestStream  );
            AssertSetter(() => WavHeaderWishesAccessor.WriteWavHeader(binaries.BinaryWriter, entities.SynthBound.ConfigResolver, synthWishes), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.TapeBound.Tape                          ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.TapeBound.Tape                          ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.TapeBound.Tape                          ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.Tape                          ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeConfig                    ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeConfig                    ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeConfig                    ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeConfig                    ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeActions                   ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeActions                   ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeActions                   ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeActions                   ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.TapeBound.TapeAction                    ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.TapeBound.TapeAction                    ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.TapeBound.TapeAction                    ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.TapeBound.TapeAction                    ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.BuffBound.Buff,                 courtesy), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.BuffBound.Buff,                 courtesy), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.BuffBound.Buff,                 courtesy), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.BuffBound.Buff,                 courtesy), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.Independent.Sample                      ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.Independent.Sample                      ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.Independent.Sample                      ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.Independent.Sample                      ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.Independent.AudioInfoWish               ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.Independent.AudioInfoWish               ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.Independent.AudioInfoWish               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.Independent.AudioInfoWish               ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.Independent.AudioFileInfo               ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.Independent.AudioFileInfo               ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.Independent.AudioFileInfo               ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.Independent.AudioFileInfo               ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.Write         (binaries.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.Write         (binaries.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.Write         (binaries.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.Write         (binaries.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter);

            // TODO: Test BuffBound overloads without courtesyFrames.
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WavHeader_WriteWavHeader_LooseValues(string caseKey)
        {
            Case test = Cases[caseKey];
            int frameCount = test.FrameCount;
            
            TestEntities entities = CreateEntities(test);
            var x = entities.Immutable;
            AssertInvariant(entities, test);

            BuffBoundEntities binaries = null;
            
            void AssertSetter(Action setter, TestEntityEnum entity)
            {
                using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
                {
                    binaries = changedEntities.BuffBound;
                    AssertInvariant(changedEntities, test);
                    
                    setter();
                    
                    if (entity == ForDestFilePath) Assert(binaries.DestFilePath, test);
                    if (entity == ForDestBytes)    Assert(binaries.DestBytes,    test);
                    if (entity == ForDestStream)   Assert(binaries.DestStream,   test);
                    if (entity == ForBinaryWriter) Assert(binaries.BinaryWriter, test);
                }
            }
            
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath.WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes   .WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => binaries.DestStream  .WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter.WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,     x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,    x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,     x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,    x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,     x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,    x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,     x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,    x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WriteWavHeader(binaries.DestFilePath, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WriteWavHeader(binaries.DestBytes,    (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WriteWavHeader(binaries.DestStream,   (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WriteWavHeader(binaries.BinaryWriter, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,     x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,    x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,     x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,    x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,     x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,    x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,     x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,    x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestFilePath, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath);
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestBytes,    (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes   );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.DestStream,   (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream  );
            AssertSetter(() => WavHeaderWishes.WriteWavHeader(binaries.BinaryWriter, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter);
            
            if (test.Bits == 8)
            {
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WriteWavHeader<byte> (binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<byte> (binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WriteWavHeader<byte> (binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<byte> (binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
            }
            else if (test.Bits == 16)
            {
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<short>(binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<short>(binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<short>(binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WriteWavHeader<short>(binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<short>(binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<short>(binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<short>(binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WriteWavHeader<short>(binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<short>(binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
            }
            else if (test.Bits == 32)
            {
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => binaries.DestFilePath.WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes   .WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => binaries.DestStream  .WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter.WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<float>(binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<float>(binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<float>(binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WriteWavHeader<float>(binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WriteWavHeader<float>(binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WriteWavHeader<float>(binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WriteWavHeader<float>(binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WriteWavHeader<float>(binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath);
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes   );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream  );
                AssertSetter(() => WavHeaderWishes.WriteWavHeader<float>(binaries.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter);
            }
            else AssertBits(test.Bits); // ncrunch: no coverage
        }
        
        [TestMethod]
        public void WavHeader_WriteWavHeader_WithConfigSection()
        {
            ConfigSectionAccessor configSection = null;
            BuffBoundEntities binaries = null;

            void TestSetter(Action action)
            {
                using (var entities = CreateEntities(NonDefaultCase, withDisk: true))
                {
                    AssertInvariant(entities, NonDefaultCase);

                    configSection = entities.SynthBound.ConfigSection;
                    binaries = entities.BuffBound;

                    action();
                }
            }
            
            // By Design: Mocked ConfigSection has default settings.
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestFilePath); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestBytes   ); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestStream  ); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.BinaryWriter); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { binaries.DestFilePath.WriteWavHeader(configSection); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { binaries.DestBytes   .WriteWavHeader(configSection); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { binaries.DestStream  .WriteWavHeader(configSection); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { binaries.BinaryWriter.WriteWavHeader(configSection); Assert(binaries.BinaryWriter, DefaultsCase); });
        }
        
        
        [TestMethod]
        public void WavHeader_EdgeCases()
        {
            var test = EdgeCase;
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
            AreEqual(test.Bits,         () => entity.BitsPerValue);
            AreEqual(test.Channels,     () => entity.ChannelCount);
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount(), - Tolerance - test.CourtesyFrames);
        }
        
        void Assert(string filePath, Case test)
        {
            if (!Has(filePath)) throw new NullException(() => filePath);
            if (test == null) throw new NullException(() => test);
            Assert(filePath.ReadWavHeader(), test);
        }
        
        void Assert(byte[] bytes, Case test)
        {
            if (bytes == null) throw new NullException(() => bytes);
            if (test == null) throw new NullException(() => test);
            Assert(bytes.ReadWavHeader(), test);
        }
        
        void Assert(Stream stream, Case test)
        {
            if (stream == null) throw new NullException(() => stream);
            if (test == null) throw new NullException(() => test);
            stream.Position = 0;
            Assert(stream.ReadWavHeader(), test);
        }
        
        void Assert(BinaryWriter writer, Case test)
        {
            if (writer == null) throw new NullException(() => writer);
            if (writer.BaseStream == null) throw new NullException(() => writer.BaseStream);
            if (test == null) throw new NullException(() => test);
            writer.BaseStream.Position = 0;
            Assert(writer.BaseStream.ReadWavHeader(), test);
        }
        
        // Helpers
        
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression) => AreEqual<int>(expected, actualExpression);
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression, int delta) => AssertWishes.AreEqual(expected, actualExpression, delta);
        private static void AreEqual(int expected, int actual) => AreEqual<int>(expected, actual);
    }
}
