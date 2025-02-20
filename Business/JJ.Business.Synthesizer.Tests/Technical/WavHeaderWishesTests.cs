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
using static System.Net.Mime.MediaTypeNames;
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
        public void WavHeaderWishes_ToWish_Test(string caseKey)
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
        public void WavHeaderWishes_FromWish_Test(string caseKey)
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
        public void ToWavHeader_Test(string caseKey)
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
        public void FromWavHeader_Test(string caseKey)
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
            TestProp((x, wav) => { x.SynthBound.ConfigResolver.ApplyWavHeader(wav,        synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { x.TapeBound.Tape           .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { x.TapeBound.TapeConfig     .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { x.TapeBound.TapeActions    .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { x.TapeBound.TapeAction     .ApplyWavHeader(wav )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { x.BuffBound.Buff           .ApplyWavHeader(wav,  courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { x.BuffBound.AudioFileOutput.ApplyWavHeader(wav,  courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { x.Independent.Sample       .ApplyWavHeader(wav,            context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { x.Independent.AudioFileInfo.ApplyWavHeader(wav )                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { x.Independent.AudioInfoWish.ApplyWavHeader(wav )                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.SynthWishes    )                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.FlowNode       )                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.SynthBound.ConfigResolver,        synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.Tape            )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeConfig      )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeActions     )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.TapeBound.TapeAction      )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.BuffBound.Buff,             courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.BuffBound.AudioFileOutput,  courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.Sample,                   context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.AudioFileInfo )                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { wav.ApplyWavHeader(x.Independent.AudioInfoWish )                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.SynthWishes,    wav)                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.FlowNode,       wav)                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { ApplyWavHeader(x.SynthBound.ConfigResolver, wav,       synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.Tape,            wav)                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeConfig,      wav)                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeActions,     wav)                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { ApplyWavHeader(x.TapeBound.TapeAction,      wav)                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { ApplyWavHeader(x.BuffBound.Buff,            wav, courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { ApplyWavHeader(x.BuffBound.AudioFileOutput, wav, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.Sample,        wav,           context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.AudioFileInfo, wav)                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { ApplyWavHeader(x.Independent.AudioInfoWish, wav)                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.SynthWishes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.FlowNode      )                   ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.SynthBound.ConfigResolver, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.Tape           )                   ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeConfig     )                   ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeActions    )                   ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.TapeBound.TapeAction     )                   ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.BuffBound.Buff,            courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.BuffBound.AudioFileOutput, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.Sample,                  context); Assert(x.Independent.Sample,        test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.AudioFileInfo)                   ; Assert(x.Independent.AudioFileInfo, test); });
            TestProp((x, wav) => { ApplyWavHeader(wav, x.Independent.AudioInfoWish)                   ; Assert(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.SynthBound.SynthWishes,    wav)                     ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.SynthBound.FlowNode,       wav)                     ; Assert(x.SynthBound.FlowNode,       test); });
            TestProp((x, wav) => { WavHeaderWishesAccessor.ApplyWavHeader(x.SynthBound.ConfigResolver, wav, synthWishes); Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.Tape,            wav)                     ; Assert(x.TapeBound.Tape,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeConfig,      wav)                     ; Assert(x.TapeBound.TapeConfig,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeActions,     wav)                     ; Assert(x.TapeBound.TapeActions,     test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.TapeBound.TapeAction,      wav)                     ; Assert(x.TapeBound.TapeAction,      test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.BuffBound.Buff,            wav,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.BuffBound.AudioFileOutput, wav,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
            TestProp((x, wav) => { WavHeaderWishes.ApplyWavHeader(x.Independent.Sample,        wav,             context); Assert(x.Independent.Sample,        test); });
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
        public void ReadWavHeader_Test(string caseKey)
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
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceFilePath,  synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceBytes,     synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceStream,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.BinaryReader,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
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
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceFilePath,  courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceBytes,     courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.SourceStream,    courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.Buff             .ReadWavHeader(binaries.BinaryReader,    courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceFilePath,  courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceBytes,     courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceStream,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.BinaryReader,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceFilePath,            context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceBytes,               context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.SourceStream,              context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { x.Independent.Sample         .ReadWavHeader(binaries.BinaryReader,              context); Assert(x.Independent.Sample,        test); });
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
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.SynthBound.ConfigResolver,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.SynthBound.ConfigResolver,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.SynthBound.ConfigResolver,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.SynthBound.ConfigResolver,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
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
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.BuffBound.Buff,               courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.BuffBound.AudioFileOutput,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.Sample,                     context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.Sample,                     context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.Sample,                     context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.Sample,                     context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.AudioFileInfo   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { binaries.SourceFilePath.ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.SourceBytes   .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.SourceStream  .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { binaries.BinaryReader  .ReadWavHeader(x.Independent.AudioInfoWish   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceFilePath)                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceBytes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceStream  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.SynthWishes,      binaries.BinaryReader  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceFilePath)                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceBytes   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceStream  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.FlowNode,         binaries.BinaryReader  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceFilePath, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceBytes,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceStream,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.BinaryReader,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape,              binaries.SourceFilePath)                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape,              binaries.SourceBytes   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape,              binaries.SourceStream  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.Tape,              binaries.BinaryReader  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeConfig,        binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeActions,       binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.TapeBound.TapeAction,        binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff,              binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff,              binaries.SourceBytes,    courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff,              binaries.SourceStream,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.Buff,              binaries.BinaryReader,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceBytes,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceStream,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.BinaryReader,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample,          binaries.SourceFilePath,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample,          binaries.SourceBytes,              context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample,          binaries.SourceStream,             context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.Sample,          binaries.BinaryReader,             context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceFilePath)                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceBytes   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceStream  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioFileInfo,   binaries.BinaryReader  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceFilePath)                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceBytes   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceStream  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(x.Independent.AudioInfoWish,   binaries.BinaryReader  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceBytes,    x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.SourceStream,   x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { ReadWavHeader(binaries.BinaryReader,   x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceFilePath)                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceBytes   )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceStream  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.BinaryReader  )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceFilePath)                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceBytes   )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceStream  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.BinaryReader  )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceFilePath, synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceBytes,    synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.SourceStream,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver,   binaries.BinaryReader,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceFilePath)                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceBytes   )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceStream  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.BinaryReader  )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceFilePath)                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceBytes   )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceStream  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.BinaryReader  )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceBytes,    courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceStream,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.BinaryReader,   courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceFilePath, courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceBytes,    courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceStream,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.BinaryReader,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceFilePath,           context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceBytes,              context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceStream,             context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.Sample,          binaries.BinaryReader,             context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceFilePath)                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceBytes   )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceStream  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.BinaryReader  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceFilePath)                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceBytes   )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceStream  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.BinaryReader  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.SynthBound.SynthWishes     )                   ; Assert(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.SynthBound.FlowNode        )                   ; Assert(x.SynthBound.FlowNode,       test); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceBytes,    x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.SourceStream,   x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishesAccessor.ReadWavHeader(binaries.BinaryReader,   x.SynthBound.ConfigResolver,   synthWishes)      ; Assert(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.TapeBound.Tape             )                   ; Assert(x.TapeBound.Tape,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeConfig       )                   ; Assert(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeActions      )                   ; Assert(x.TapeBound.TapeActions,     test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.TapeBound.TapeAction       )                   ; Assert(x.TapeBound.TapeAction,      test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.BuffBound.Buff,              courtesy, context); Assert(x.BuffBound.Buff,            test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.BuffBound.AudioFileOutput,   courtesy, context); Assert(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.Independent.Sample,                    context); Assert(x.Independent.Sample,        test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.Independent.AudioFileInfo  )                   ; Assert(x.Independent.AudioFileInfo, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceFilePath, x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceBytes,    x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.SourceStream,   x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
                TestProp(x => { WavHeaderWishes.ReadWavHeader(binaries.BinaryReader,   x.Independent.AudioInfoWish  )                   ; Assert(x.Independent.AudioInfoWish, test); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void ReadAudioInfo_Test(string caseKey)
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
        public void WriteWavHeader_Test(string caseKey)
        {
            Case test = Cases[caseKey];
            TestEntities entities = CreateEntities(test);
            AssertInvariant(entities, test);
            var synthWishes = entities.SynthBound.SynthWishes;

            AssertWrite(bin => entities.SynthBound.SynthWishes     .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.SynthBound.SynthWishes     .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.SynthBound.SynthWishes     .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.SynthBound.SynthWishes     .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.SynthBound.FlowNode        .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.SynthBound.FlowNode        .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.SynthBound.FlowNode        .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.SynthBound.FlowNode        .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestFilePath, synthWishes), ForDestFilePath, test);
            AssertWrite(bin => entities.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestBytes,    synthWishes), ForDestBytes,    test);
            AssertWrite(bin => entities.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestStream,   synthWishes), ForDestStream,   test);
            AssertWrite(bin => entities.SynthBound.ConfigResolver  .WriteWavHeader(bin.BinaryWriter, synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => entities.TapeBound.Tape             .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.TapeBound.Tape             .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.TapeBound.Tape             .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.TapeBound.Tape             .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.TapeBound.TapeConfig       .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.TapeBound.TapeConfig       .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.TapeBound.TapeConfig       .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.TapeBound.TapeConfig       .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.TapeBound.TapeActions      .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.TapeBound.TapeActions      .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.TapeBound.TapeActions      .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.TapeBound.TapeActions      .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.TapeBound.TapeAction       .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.TapeBound.TapeAction       .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.TapeBound.TapeAction       .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.TapeBound.TapeAction       .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.Independent.Sample         .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.Independent.Sample         .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.Independent.Sample         .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.Independent.Sample         .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.Independent.AudioInfoWish  .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.Independent.AudioInfoWish  .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.Independent.AudioInfoWish  .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.Independent.AudioInfoWish  .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.Independent.AudioFileInfo  .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.Independent.AudioFileInfo  .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.Independent.AudioFileInfo  .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.Independent.AudioFileInfo  .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.Immutable.WavHeader        .WriteWavHeader(bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.Immutable.WavHeader        .WriteWavHeader(bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.Immutable.WavHeader        .WriteWavHeader(bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.Immutable.WavHeader        .WriteWavHeader(bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => entities.Immutable.WavHeader        .Write         (bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => entities.Immutable.WavHeader        .Write         (bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => entities.Immutable.WavHeader        .Write         (bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => entities.Immutable.WavHeader        .Write         (bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.SynthBound.SynthWishes                  ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.SynthBound.FlowNode                     ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.SynthBound.FlowNode                     ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.SynthBound.ConfigResolver,   synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.TapeBound.Tape                          ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.TapeBound.Tape                          ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.TapeBound.Tape                          ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.TapeBound.Tape                          ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeConfig                    ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.TapeBound.TapeActions                   ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeActions                   ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.TapeBound.TapeAction                    ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.TapeBound.TapeAction                    ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.Independent.Sample                      ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.Independent.Sample                      ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.Independent.Sample                      ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.Independent.Sample                      ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.Independent.AudioInfoWish               ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.Independent.AudioInfoWish               ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.Independent.AudioFileInfo               ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.Independent.AudioFileInfo               ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.Write         (entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .Write         (entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .Write         (entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.Write         (entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.SynthWishes,      bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.FlowNode,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.ConfigResolver,   bin.DestFilePath, synthWishes), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.ConfigResolver,   bin.DestBytes,    synthWishes), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.ConfigResolver,   bin.DestStream,   synthWishes), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.SynthBound.ConfigResolver,   bin.BinaryWriter, synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.Tape,              bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.Tape,              bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.Tape,              bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.Tape,              bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeConfig,        bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeActions,       bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.TapeBound.TapeAction,        bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.Sample,          bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.Sample,          bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.Sample,          bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.Sample,          bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioInfoWish,   bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.Independent.AudioFileInfo,   bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.Immutable.WavHeader,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.Immutable.WavHeader,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.Immutable.WavHeader,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.Immutable.WavHeader,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => Write         (entities.Immutable.WavHeader,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => Write         (entities.Immutable.WavHeader,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => Write         (entities.Immutable.WavHeader,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => Write         (entities.Immutable.WavHeader,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.SynthBound.SynthWishes                  ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.SynthBound.SynthWishes                  ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.SynthBound.SynthWishes                  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.SynthBound.SynthWishes                  ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.SynthBound.FlowNode                     ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.SynthBound.FlowNode                     ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.SynthBound.FlowNode                     ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.SynthBound.FlowNode                     ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.SynthBound.ConfigResolver,   synthWishes), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.SynthBound.ConfigResolver,   synthWishes), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.SynthBound.ConfigResolver,   synthWishes), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.SynthBound.ConfigResolver,   synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.TapeBound.Tape                          ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.TapeBound.Tape                          ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.TapeBound.Tape                          ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.TapeBound.Tape                          ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeConfig                    ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeConfig                    ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeConfig                    ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeConfig                    ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeActions                   ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeActions                   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeActions                   ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeActions                   ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeAction                    ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeAction                    ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeAction                    ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeAction                    ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.Independent.Sample                      ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.Independent.Sample                      ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.Independent.Sample                      ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.Independent.Sample                      ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.Independent.AudioInfoWish               ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.Independent.AudioInfoWish               ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.Independent.AudioInfoWish               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.Independent.AudioInfoWish               ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.Independent.AudioFileInfo               ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.Independent.AudioFileInfo               ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.Independent.AudioFileInfo               ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.Independent.AudioFileInfo               ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
            AssertWrite(bin => Write         (bin.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => Write         (bin.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => Write         (bin.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => Write         (bin.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.SynthWishes,      bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.SynthBound.FlowNode,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, bin.DestFilePath, synthWishes), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, bin.DestBytes,    synthWishes), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, bin.DestStream,   synthWishes), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(entities.SynthBound.ConfigResolver, bin.BinaryWriter, synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.Tape,              bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeConfig,        bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeActions,       bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.TapeBound.TapeAction,        bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.Sample,          bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioInfoWish,   bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Independent.AudioFileInfo,   bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.Immutable.WavHeader,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         bin.DestFilePath             ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         bin.DestBytes                ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         bin.DestStream               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.Write         (entities.Immutable.WavHeader,         bin.BinaryWriter             ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.SynthBound.SynthWishes                  ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.SynthBound.SynthWishes                  ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.SynthBound.SynthWishes                  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.SynthBound.SynthWishes                  ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.SynthBound.FlowNode                     ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.SynthBound.FlowNode                     ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.SynthBound.FlowNode                     ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.SynthBound.FlowNode                     ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(bin.DestFilePath, entities.SynthBound.ConfigResolver, synthWishes), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(bin.DestBytes,    entities.SynthBound.ConfigResolver, synthWishes), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(bin.DestStream,   entities.SynthBound.ConfigResolver, synthWishes), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishesAccessor.WriteWavHeader(bin.BinaryWriter, entities.SynthBound.ConfigResolver, synthWishes), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.TapeBound.Tape                          ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.TapeBound.Tape                          ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.TapeBound.Tape                          ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.TapeBound.Tape                          ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeConfig                    ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeConfig                    ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeConfig                    ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeConfig                    ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeActions                   ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeActions                   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeActions                   ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeActions                   ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.TapeBound.TapeAction                    ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.TapeBound.TapeAction                    ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.TapeBound.TapeAction                    ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.TapeBound.TapeAction                    ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.Independent.Sample                      ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.Independent.Sample                      ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.Independent.Sample                      ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.Independent.Sample                      ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.Independent.AudioInfoWish               ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.Independent.AudioInfoWish               ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.Independent.AudioInfoWish               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.Independent.AudioInfoWish               ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.Independent.AudioFileInfo               ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.Independent.AudioFileInfo               ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.Independent.AudioFileInfo               ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.Independent.AudioFileInfo               ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.Write         (bin.DestFilePath, entities.Immutable.WavHeader                     ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.Write         (bin.DestBytes,    entities.Immutable.WavHeader                     ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.Write         (bin.DestStream,   entities.Immutable.WavHeader                     ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.Write         (bin.BinaryWriter, entities.Immutable.WavHeader                     ), ForBinaryWriter, test);
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WriteWavHeader_Test_BuffBound(string caseKey)
        {
            Case test = Cases[caseKey];
            TestEntities entities = CreateEntities(test);
            AssertInvariant(entities, test);
            int courtesy = test.CourtesyFrames;

            AssertWrite(bin => entities.BuffBound.Buff             .WriteWavHeader(bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => entities.BuffBound.Buff             .WriteWavHeader(bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => entities.BuffBound.Buff             .WriteWavHeader(bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => entities.BuffBound.Buff             .WriteWavHeader(bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => entities.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => entities.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => entities.BuffBound.AudioFileOutput  .WriteWavHeader(bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => entities.BuffBound.AudioFileOutput  .WriteWavHeader(bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.BuffBound.Buff,                 courtesy), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.Buff,              bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.Buff,              bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.Buff,              bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.Buff,              bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.BuffBound.Buff,                 courtesy), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.BuffBound.Buff,                 courtesy), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.BuffBound.Buff,                 courtesy), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.Buff,                 courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.Buff,              bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestFilePath,    courtesy), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestBytes,       courtesy), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.DestStream,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput,   bin.BinaryWriter,    courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.BuffBound.Buff,                 courtesy), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.BuffBound.Buff,                 courtesy), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.BuffBound.Buff,                 courtesy), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.BuffBound.Buff,                 courtesy), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, entities.BuffBound.AudioFileOutput,      courtesy), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    entities.BuffBound.AudioFileOutput,      courtesy), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   entities.BuffBound.AudioFileOutput,      courtesy), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, entities.BuffBound.AudioFileOutput,      courtesy), ForBinaryWriter, test);

             // TODO: Test BuffBound overloads without courtesyFrames.
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WriteWavHeader_Test_WithLooseValues(string caseKey)
        {
            Case test = Cases[caseKey];
            int frameCount = test.FrameCount;
            
            TestEntities entities = CreateEntities(test);
            var x = entities.Immutable;
            AssertInvariant(entities, test);
            
            AssertWrite(bin => (x.Bits,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestFilePath ), ForDestFilePath, test);
            AssertWrite(bin => (x.Bits,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestBytes    ), ForDestBytes,    test);
            AssertWrite(bin => (x.Bits,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestStream   ), ForDestStream,   test);
            AssertWrite(bin => (x.Bits,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.BinaryWriter ), ForBinaryWriter, test);
            AssertWrite(bin => (x.Type,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestFilePath ), ForDestFilePath, test);
            AssertWrite(bin => (x.Type,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestBytes    ), ForDestBytes,    test);
            AssertWrite(bin => (x.Type,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.DestStream   ), ForDestStream,   test);
            AssertWrite(bin => (x.Type,               x.Channels,         x.SamplingRate, frameCount).WriteWavHeader(bin.BinaryWriter ), ForBinaryWriter, test);
            AssertWrite(bin => (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount).WriteWavHeader(bin.DestFilePath ), ForDestFilePath, test);
            AssertWrite(bin => (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount).WriteWavHeader(bin.DestBytes    ), ForDestBytes,    test);
            AssertWrite(bin => (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount).WriteWavHeader(bin.DestStream   ), ForDestStream,   test);
            AssertWrite(bin => (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount).WriteWavHeader(bin.BinaryWriter ), ForBinaryWriter, test);
            AssertWrite(bin => (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount).WriteWavHeader(bin.DestFilePath ), ForDestFilePath, test);
            AssertWrite(bin => (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount).WriteWavHeader(bin.DestBytes    ), ForDestBytes,    test);
            AssertWrite(bin => (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount).WriteWavHeader(bin.DestStream   ), ForDestStream,   test);
            AssertWrite(bin => (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount).WriteWavHeader(bin.BinaryWriter ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader( (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader( (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader( (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader(  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => bin.DestFilePath.WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => bin.DestBytes   .WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => bin.DestStream  .WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => bin.BinaryWriter.WriteWavHeader( (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,     x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,    x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,     x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,    x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,     x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,    x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,     x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,    x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(bin.DestFilePath, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(bin.DestBytes,    (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(bin.DestStream,   (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(bin.BinaryWriter, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Bits,               x.Channels,         x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.Type,               x.Channels,         x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader((x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,     x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,    x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter,  x.Bits,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, (x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,     x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,    x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter,  x.Type,               x.Channels,         x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, (x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,     x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,    x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, (x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,     x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,    x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount ), ForBinaryWriter, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestFilePath, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestBytes,    (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.DestStream,   (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => WavHeaderWishes.WriteWavHeader(bin.BinaryWriter, (x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            
            if (test.Bits == 8)
            {
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>  (bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>  (bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>  (bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>  (bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<byte>  ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<byte>  ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<byte> (bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> ((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<byte> (bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            }
            else if (test.Bits == 16)
            {
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short> (bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short> (bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short> (bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short> (bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<short> ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<short> ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<short>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<short>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<short>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<short>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            }
            else if (test.Bits == 32)
            {
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float> (bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float> (bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float> (bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float> (bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<float> ( x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => bin.DestFilePath.WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => bin.DestBytes   .WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => bin.DestStream  .WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => bin.BinaryWriter.WriteWavHeader<float> ((x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<float>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WriteWavHeader<float>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WriteWavHeader<float>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount ), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount ), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount ), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount ), ForBinaryWriter, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount)), ForDestFilePath, test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount)), ForDestBytes,    test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount)), ForDestStream,   test);
                AssertWrite(bin => WavHeaderWishes.WriteWavHeader<float>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            }
            else AssertBits(test.Bits); // ncrunch: no coverage
        }
        
        [TestMethod]
        public void WriteWavHeader_Test_WithConfigSection()
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
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestFilePath ); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestBytes    ); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.DestStream   ); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { configSection.WriteWavHeader(binaries.BinaryWriter ); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { binaries.DestFilePath.WriteWavHeader(configSection ); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { binaries.DestBytes   .WriteWavHeader(configSection ); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { binaries.DestStream  .WriteWavHeader(configSection ); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { binaries.BinaryWriter.WriteWavHeader(configSection ); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { WriteWavHeader(configSection, binaries.DestFilePath); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { WriteWavHeader(configSection, binaries.DestBytes   ); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { WriteWavHeader(configSection, binaries.DestStream  ); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { WriteWavHeader(configSection, binaries.BinaryWriter); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { WriteWavHeader(binaries.DestFilePath, configSection); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { WriteWavHeader(binaries.DestBytes,    configSection); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { WriteWavHeader(binaries.DestStream,   configSection); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { WriteWavHeader(binaries.BinaryWriter, configSection); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(configSection, binaries.DestFilePath); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(configSection, binaries.DestBytes   ); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(configSection, binaries.DestStream  ); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(configSection, binaries.BinaryWriter); Assert(binaries.BinaryWriter, DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(binaries.DestFilePath, configSection); Assert(binaries.DestFilePath, DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(binaries.DestBytes,    configSection); Assert(binaries.DestBytes,    DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(binaries.DestStream,   configSection); Assert(binaries.DestStream,   DefaultsCase); });
            TestSetter(() => { WavHeaderWishesAccessor.WriteWavHeader(binaries.BinaryWriter, configSection); Assert(binaries.BinaryWriter, DefaultsCase); });
        }
        
        
        [TestMethod]
        public void WavHeader_EdgeCase_Test()
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
         
        // Assertions Helpers
       
        private void AssertWrite(Action<BuffBoundEntities> setter, TestEntityEnum entity, Case test)
        {
            using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
            {
                var binaries = changedEntities.BuffBound;
                AssertInvariant(changedEntities, test);
                
                setter(binaries);
                
                if (entity == ForDestFilePath) Assert(binaries.DestFilePath, test);
                if (entity == ForDestBytes)    Assert(binaries.DestBytes,    test);
                if (entity == ForDestStream)   Assert(binaries.DestStream,   test);
                if (entity == ForBinaryWriter) Assert(binaries.BinaryWriter, test);
            }
        }
        
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
        
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression) => AreEqual<int>(expected, actualExpression);
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression, int delta) => AssertWishes.AreEqual(expected, actualExpression, delta);
        private static void AreEqual(int expected, int actual) => AreEqual<int>(expected, actual);
    }
}
