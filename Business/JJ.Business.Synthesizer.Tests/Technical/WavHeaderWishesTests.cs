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
            if      (test.Bits ==  8) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> (),  test);
            else if (test.Bits == 16) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>(),  test);
            else if (test.Bits == 32) Assert(x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>(),  test);
            else AssertBits(test.Bits);
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
            
            TestEntities entities = CreateEntities(test);
            AssertInvariant(entities, test);
            
            BuffBoundEntities binaries = null;
            var synthWishes = entities.SynthBound.SynthWishes;
            int courtesy = test.CourtesyFrames;
            
            void AssertSetter(Action setter, TestEntityEnum entity)
            {
                using (var changedEntities = CreateChangedEntities(test, withDisk: entity == ForDestFilePath))
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

            AssertSetter(() => entities.SynthBound .SynthWishes    .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.SynthBound .SynthWishes    .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.SynthBound .SynthWishes    .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.SynthBound .SynthWishes    .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.SynthBound .FlowNode       .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.SynthBound .FlowNode       .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.SynthBound .FlowNode       .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.SynthBound .FlowNode       .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.SynthBound .ConfigResolver .WriteWavHeader(binaries.DestFilePath, synthWishes), ForDestFilePath);
            AssertSetter(() => entities.SynthBound .ConfigResolver .WriteWavHeader(binaries.DestBytes,    synthWishes), ForDestBytes   );
            AssertSetter(() => entities.SynthBound .ConfigResolver .WriteWavHeader(binaries.DestStream,   synthWishes), ForDestStream  );
            AssertSetter(() => entities.SynthBound .ConfigResolver .WriteWavHeader(binaries.BinaryWriter, synthWishes), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound  .Tape           .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound  .Tape           .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound  .Tape           .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound  .Tape           .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound  .TapeConfig     .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound  .TapeConfig     .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound  .TapeConfig     .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound  .TapeConfig     .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound  .TapeActions    .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound  .TapeActions    .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound  .TapeActions    .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound  .TapeActions    .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.TapeBound  .TapeAction     .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.TapeBound  .TapeAction     .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.TapeBound  .TapeAction     .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.TapeBound  .TapeAction     .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.BuffBound  .Buff           .WriteWavHeader(binaries.DestFilePath, courtesy   ), ForDestFilePath);
            AssertSetter(() => entities.BuffBound  .Buff           .WriteWavHeader(binaries.DestBytes,    courtesy   ), ForDestBytes   );
            AssertSetter(() => entities.BuffBound  .Buff           .WriteWavHeader(binaries.DestStream,   courtesy   ), ForDestStream  );
            AssertSetter(() => entities.BuffBound  .Buff           .WriteWavHeader(binaries.BinaryWriter, courtesy   ), ForBinaryWriter);
            AssertSetter(() => entities.BuffBound  .AudioFileOutput.WriteWavHeader(binaries.DestFilePath, courtesy   ), ForDestFilePath);
            AssertSetter(() => entities.BuffBound  .AudioFileOutput.WriteWavHeader(binaries.DestBytes,    courtesy   ), ForDestBytes   );
            AssertSetter(() => entities.BuffBound  .AudioFileOutput.WriteWavHeader(binaries.DestStream,   courtesy   ), ForDestStream  );
            AssertSetter(() => entities.BuffBound  .AudioFileOutput.WriteWavHeader(binaries.BinaryWriter, courtesy   ), ForBinaryWriter);
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
            AssertSetter(() => entities.Immutable  .WavHeader      .WriteWavHeader(binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Immutable  .WavHeader      .WriteWavHeader(binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Immutable  .WavHeader      .WriteWavHeader(binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Immutable  .WavHeader      .WriteWavHeader(binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => entities.Immutable  .WavHeader      .Write         (binaries.DestFilePath             ), ForDestFilePath);
            AssertSetter(() => entities.Immutable  .WavHeader      .Write         (binaries.DestBytes                ), ForDestBytes   );
            AssertSetter(() => entities.Immutable  .WavHeader      .Write         (binaries.DestStream               ), ForDestStream  );
            AssertSetter(() => entities.Immutable  .WavHeader      .Write         (binaries.BinaryWriter             ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.SynthBound .SynthWishes                ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.SynthBound .SynthWishes                ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.SynthBound .SynthWishes                ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.SynthBound .SynthWishes                ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.SynthBound .FlowNode                   ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.SynthBound .FlowNode                   ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.SynthBound .FlowNode                   ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.SynthBound .FlowNode                   ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.SynthBound .ConfigResolver, synthWishes), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.SynthBound .ConfigResolver, synthWishes), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.SynthBound .ConfigResolver, synthWishes), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.SynthBound .ConfigResolver, synthWishes), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.TapeBound  .Tape                       ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.TapeBound  .Tape                       ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.TapeBound  .Tape                       ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.TapeBound  .Tape                       ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.TapeBound  .TapeConfig                 ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.TapeBound  .TapeConfig                 ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.TapeBound  .TapeConfig                 ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.TapeBound  .TapeConfig                 ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.TapeBound  .TapeActions                ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.TapeBound  .TapeActions                ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.TapeBound  .TapeActions                ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.TapeBound  .TapeActions                ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.TapeBound  .TapeAction                 ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.TapeBound  .TapeAction                 ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.TapeBound  .TapeAction                 ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.TapeBound  .TapeAction                 ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.BuffBound  .Buff,            courtesy  ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.BuffBound  .Buff,            courtesy  ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.BuffBound  .Buff,            courtesy  ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.BuffBound  .Buff,            courtesy  ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.BuffBound  .AudioFileOutput, courtesy  ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.BuffBound  .AudioFileOutput, courtesy  ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.BuffBound  .AudioFileOutput, courtesy  ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.BuffBound  .AudioFileOutput, courtesy  ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.Independent.Sample                     ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.Independent.Sample                     ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.Independent.Sample                     ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.Independent.Sample                     ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.Independent.AudioInfoWish              ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.Independent.AudioInfoWish              ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.Independent.AudioInfoWish              ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.Independent.AudioInfoWish              ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.Independent.AudioFileInfo              ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.Independent.AudioFileInfo              ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.Independent.AudioFileInfo              ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.Independent.AudioFileInfo              ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(entities.Immutable  .WavHeader                  ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(entities.Immutable  .WavHeader                  ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(entities.Immutable  .WavHeader                  ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(entities.Immutable  .WavHeader                  ), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .Write         (entities.Immutable  .WavHeader                  ), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .Write         (entities.Immutable  .WavHeader                  ), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .Write         (entities.Immutable  .WavHeader                  ), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .Write         (entities.Immutable  .WavHeader                  ), ForBinaryWriter);
            
            var x = entities.Immutable;
            
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(x.Bits, x.Channels, x.SamplingRate, test.FrameCount), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(x.Bits, x.Channels, x.SamplingRate, test.FrameCount), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(x.Bits, x.Channels, x.SamplingRate, test.FrameCount), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(x.Bits, x.Channels, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(x.Type, x.Channels, x.SamplingRate, test.FrameCount), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(x.Type, x.Channels, x.SamplingRate, test.FrameCount), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(x.Type, x.Channels, x.SamplingRate, test.FrameCount), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(x.Type, x.Channels, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, test.FrameCount), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, test.FrameCount), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, test.FrameCount), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            AssertSetter(() => binaries.DestFilePath  .WriteWavHeader(x.SampleDataType, x.SpeakerSetup, x.SamplingRate, test.FrameCount), ForDestFilePath);
            AssertSetter(() => binaries.DestBytes     .WriteWavHeader(x.SampleDataType, x.SpeakerSetup, x.SamplingRate, test.FrameCount), ForDestBytes   );
            AssertSetter(() => binaries.DestStream    .WriteWavHeader(x.SampleDataType, x.SpeakerSetup, x.SamplingRate, test.FrameCount), ForDestStream  );
            AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader(x.SampleDataType, x.SpeakerSetup, x.SamplingRate, test.FrameCount), ForBinaryWriter);

            if (test.Bits == 8)
            {
                AssertSetter(() => binaries.DestFilePath  .WriteWavHeader<byte>(x.Channels, x.SamplingRate, test.FrameCount), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes     .WriteWavHeader<byte>(x.Channels, x.SamplingRate, test.FrameCount), ForDestBytes   );
                AssertSetter(() => binaries.DestStream    .WriteWavHeader<byte>(x.Channels, x.SamplingRate, test.FrameCount), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader<byte>(x.Channels, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            }
            else if (test.Bits == 16)
            {
                AssertSetter(() => binaries.DestFilePath  .WriteWavHeader<short>(x.Channels, x.SamplingRate, test.FrameCount), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes     .WriteWavHeader<short>(x.Channels, x.SamplingRate, test.FrameCount), ForDestBytes   );
                AssertSetter(() => binaries.DestStream    .WriteWavHeader<short>(x.Channels, x.SamplingRate, test.FrameCount), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader<short>(x.Channels, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            }
            else if (test.Bits == 32)
            {
                AssertSetter(() => binaries.DestFilePath  .WriteWavHeader<float>(x.Channels, x.SamplingRate, test.FrameCount), ForDestFilePath);
                AssertSetter(() => binaries.DestBytes     .WriteWavHeader<float>(x.Channels, x.SamplingRate, test.FrameCount), ForDestBytes   );
                AssertSetter(() => binaries.DestStream    .WriteWavHeader<float>(x.Channels, x.SamplingRate, test.FrameCount), ForDestStream  );
                AssertSetter(() => binaries.BinaryWriter  .WriteWavHeader<float>(x.Channels, x.SamplingRate, test.FrameCount), ForBinaryWriter);
            }
            else
            {   // ncrunch: no coverage start
                throw new Exception(NotSupportedMessage(nameof(test.Bits), test.Bits, ValidBits));
                // ncrunch: no coverage end
            }
            
            // TODO: Test BuffBound overloads without courtesyFrames.
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
