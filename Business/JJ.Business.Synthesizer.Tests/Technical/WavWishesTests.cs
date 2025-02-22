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
using JJ.Framework.Testing;
using JJ.Framework.Wishes.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Tests.Accessors.WavWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntityEnum;
using static JJ.Business.Synthesizer.Wishes.WavWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Testing.AssertWishes;
using static JJ.Business.Synthesizer.Tests.ConfigTests.FrameCountWishesTests;
using System.Security.Cryptography;

// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable RedundantEmptyObjectOrCollectionInitializer

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class WavWishesTests
    {
        // Test Data
        
        private static int Tolerance { get; } = 1;
        
        // TODO: CaseBase without MainProp to omit the <int> type argument?
        internal class Case : CaseBase<int>
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
        public void ToInfo_Test(string caseKey)
        { 
            var test = Cases[caseKey];
            
            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            
            int frameCount = test.FrameCount;
            int courtesy = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            
            AssertEntity(x.SynthBound.SynthWishes         .ToInfo(),                        test);
            AssertEntity(x.SynthBound.FlowNode            .ToInfo(),                        test);
            AssertEntity(x.SynthBound.ConfigResolver      .ToInfo(synthWishes),             test);
            AssertEntity(x.SynthBound.ConfigSection       .ToInfo(),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            AssertEntity(x.TapeBound.Tape                 .ToInfo(),                        test);
            AssertEntity(x.TapeBound.TapeConfig           .ToInfo(),                        test);
            AssertEntity(x.TapeBound.TapeActions          .ToInfo(),                        test);
            AssertEntity(x.TapeBound.TapeAction           .ToInfo(),                        test);
            AssertEntity(x.BuffBound.Buff                 .ToInfo(),                        test);
            AssertEntity(x.BuffBound.AudioFileOutput      .ToInfo(),                        test);
            AssertEntity(x.BuffBound.Buff                 .ToInfo().FrameCount(frameCount), test);
            AssertEntity(x.BuffBound.AudioFileOutput      .ToInfo().FrameCount(frameCount), test);
            AssertEntity(x.Independent.Sample             .ToInfo(),                        test);
            AssertEntity(x.Independent.AudioFileInfo      .ToInfo(),                        test);
            AssertEntity(x.Immutable.WavHeader            .ToInfo(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithInts    .ToInfo(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithType    .ToInfo(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithEnums   .ToInfo(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithEntities.ToInfo(),                        test);
            AssertEntity(ToInfo(x.SynthBound.SynthWishes          ),                        test);
            AssertEntity(ToInfo(x.SynthBound.FlowNode             ),                        test);
            AssertEntity(ToInfo(x.SynthBound.ConfigResolver,      synthWishes),             test);
            AssertEntity(ToInfo(x.SynthBound.ConfigSection        ),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            AssertEntity(ToInfo(x.TapeBound.Tape                  ),                        test);
            AssertEntity(ToInfo(x.TapeBound.TapeConfig            ),                        test);
            AssertEntity(ToInfo(x.TapeBound.TapeActions           ),                        test);
            AssertEntity(ToInfo(x.TapeBound.TapeAction            ),                        test);
            AssertEntity(ToInfo(x.BuffBound.Buff                  ),                        test);
            AssertEntity(ToInfo(x.BuffBound.AudioFileOutput       ),                        test);
            AssertEntity(ToInfo(x.BuffBound.Buff                  ).FrameCount(frameCount), test);
            AssertEntity(ToInfo(x.BuffBound.AudioFileOutput       ).FrameCount(frameCount), test);
            AssertEntity(ToInfo(x.Independent.Sample              ),                        test);
            AssertEntity(ToInfo(x.Independent.AudioFileInfo       ),                        test);
            AssertEntity(ToInfo(x.Immutable.WavHeader             ),                        test);
            AssertEntity(ToInfo(x.Immutable.InfoTupleWithInts     ),                        test);
            AssertEntity(ToInfo(x.Immutable.InfoTupleWithType     ),                        test);
            AssertEntity(ToInfo(x.Immutable.InfoTupleWithEnums    ),                        test);
            AssertEntity(ToInfo(x.Immutable.InfoTupleWithEntities ),                        test);
            AssertEntity(WavWishes.ToInfo(x.SynthBound.SynthWishes         ),                        test);
            AssertEntity(WavWishes.ToInfo(x.SynthBound.FlowNode            ),                        test);
            AssertEntity(WavWishesAccessor.ToInfo(x.SynthBound.ConfigResolver,synthWishes),          test);
            AssertEntity(WavWishesAccessor.ToInfo(x.SynthBound.ConfigSection),                DefaultsCase);// By Design: Mocked ConfigSection has default settings.
            AssertEntity(WavWishes.ToInfo(x.TapeBound.Tape                 ),                        test);
            AssertEntity(WavWishes.ToInfo(x.TapeBound.TapeConfig           ),                        test);
            AssertEntity(WavWishes.ToInfo(x.TapeBound.TapeActions          ),                        test);
            AssertEntity(WavWishes.ToInfo(x.TapeBound.TapeAction           ),                        test);
            AssertEntity(WavWishes.ToInfo(x.BuffBound.Buff                 ),                        test);
            AssertEntity(WavWishes.ToInfo(x.BuffBound.AudioFileOutput      ),                        test);
            AssertEntity(WavWishes.ToInfo(x.BuffBound.Buff                 ).FrameCount(frameCount), test);
            AssertEntity(WavWishes.ToInfo(x.BuffBound.AudioFileOutput      ).FrameCount(frameCount), test);
            AssertEntity(WavWishes.ToInfo(x.Independent.Sample             ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Independent.AudioFileInfo      ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Immutable.WavHeader            ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Immutable.InfoTupleWithInts    ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Immutable.InfoTupleWithType    ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Immutable.InfoTupleWithEnums   ),                        test);
            AssertEntity(WavWishes.ToInfo(x.Immutable.InfoTupleWithEntities),                        test);

            if      (test.Bits ==  8) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToInfo<byte> (), test);
            else if (test.Bits == 16) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToInfo<short>(), test);
            else if (test.Bits == 32) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToInfo<float>(), test); else AssertBits(test.Bits);
            if      (test.Bits ==  8) AssertEntity(ToInfo<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) AssertEntity(ToInfo<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) AssertEntity(ToInfo<float>(x.Immutable.InfoTupleWithoutBits), test); else AssertBits(test.Bits);
            if      (test.Bits ==  8) AssertEntity(WavWishes.ToInfo<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) AssertEntity(WavWishes.ToInfo<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) AssertEntity(WavWishes.ToInfo<float>(x.Immutable.InfoTupleWithoutBits), test); else AssertBits(test.Bits);
        }

        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void ApplyInfo_Test(string caseKey)
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

            void TestSetter(Action<TestEntities> setter)
            {
                TestEntities  x = CreateEntities(test);
                AssertIsInit (x, test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;
                
                setter(x);
            }
            
            TestSetter(x => { AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes    .ApplyInfo(info             )); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode       .ApplyInfo(info             )); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver .ApplyInfo(info, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(x.TapeBound.Tape,            () => x.TapeBound.Tape            .ApplyInfo(info             )); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeConfig,      () => x.TapeBound.TapeConfig      .ApplyInfo(info             )); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeActions,     () => x.TapeBound.TapeActions     .ApplyInfo(info             )); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeAction,      () => x.TapeBound.TapeAction      .ApplyInfo(info             )); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff            .ApplyInfo(info,     context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput .ApplyInfo(info,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(x.Independent.Sample       , () => x.Independent.Sample        .ApplyInfo(info,     context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo .ApplyInfo(info             )); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish .ApplyFrom(info             )); AssertEntity(x.Independent.AudioInfoWish, test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.SynthWishes,    () => ApplyInfo(x.SynthBound.SynthWishes,    info             )); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.FlowNode,       () => ApplyInfo(x.SynthBound.FlowNode,       info             )); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.ConfigResolver, () => ApplyInfo(x.SynthBound.ConfigResolver, info, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(x.TapeBound.Tape,            () => ApplyInfo(x.TapeBound.Tape,            info             )); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeConfig,      () => ApplyInfo(x.TapeBound.TapeConfig,      info             )); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeActions,     () => ApplyInfo(x.TapeBound.TapeActions,     info             )); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeAction,      () => ApplyInfo(x.TapeBound.TapeAction,      info             )); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.Buff,            () => ApplyInfo(x.BuffBound.Buff,            info,     context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.AudioFileOutput, () => ApplyInfo(x.BuffBound.AudioFileOutput, info,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(x.Independent.Sample,        () => ApplyInfo(x.Independent.Sample,        info,     context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioFileInfo, () => ApplyInfo(x.Independent.AudioFileInfo, info             )); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioInfoWish, () => ApplyFrom(x.Independent.AudioInfoWish, info             )); AssertEntity(x.Independent.AudioInfoWish, test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.SynthBound.SynthWishes                )); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.SynthBound.FlowNode                   )); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.TapeBound.Tape                        )); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.TapeBound.TapeConfig                  )); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.TapeBound.TapeActions                 )); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.TapeBound.TapeAction                  )); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.BuffBound.Buff,                context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.Independent.Sample,            context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyInfo (x.Independent.AudioFileInfo             )); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(info, () => info.ApplyTo   (x.Independent.AudioInfoWish             )); AssertEntity(x.Independent.AudioInfoWish, test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.SynthBound.SynthWishes                )); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.SynthBound.FlowNode                   )); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.TapeBound.Tape                        )); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.TapeBound.TapeConfig                  )); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.TapeBound.TapeActions                 )); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.TapeBound.TapeAction                  )); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.BuffBound.Buff,                context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.Independent.Sample,            context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyInfo(info, x.Independent.AudioFileInfo             )); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(info, () => ApplyTo  (info, x.Independent.AudioInfoWish             )); AssertEntity(x.Independent.AudioInfoWish, test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.SynthWishes,    () => WavWishes        .ApplyInfo(x.SynthBound.SynthWishes,    info)             ); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.FlowNode,       () => WavWishes        .ApplyInfo(x.SynthBound.FlowNode,       info)             ); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.ApplyInfo(x.SynthBound.ConfigResolver, info, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(x.TapeBound.Tape,            () => WavWishes        .ApplyInfo(x.TapeBound.Tape,            info)             ); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeConfig,      () => WavWishes        .ApplyInfo(x.TapeBound.TapeConfig,      info)             ); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeActions,     () => WavWishes        .ApplyInfo(x.TapeBound.TapeActions,     info)             ); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(x.TapeBound.TapeAction,      () => WavWishes        .ApplyInfo(x.TapeBound.TapeAction,      info)             ); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.Buff,            () => WavWishes        .ApplyInfo(x.BuffBound.Buff,            info,     context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes        .ApplyInfo(x.BuffBound.AudioFileOutput, info,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(x.Independent.Sample,        () => WavWishes        .ApplyInfo(x.Independent.Sample,        info,     context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioFileInfo, () => WavWishes        .ApplyInfo(x.Independent.AudioFileInfo, info)             ); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(x.Independent.AudioInfoWish, () => WavWishes        .ApplyFrom(x.Independent.AudioInfoWish, info)             ); AssertEntity(x.Independent.AudioInfoWish, test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishesAccessor.ApplyInfo(info, x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyInfo(info, x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test             ); });
            TestSetter(x => { AreEqual(info, () => WavWishes        .ApplyTo  (info, x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test             ); });
        }

        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void ToWavHeader_Test(string caseKey)
        { 
            Case test = Cases[caseKey];

            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            
            int frameCount  = test.FrameCount;
            int courtesy    = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
                 
            AssertEntity(x.SynthBound.SynthWishes         .ToWavHeader(),                        test);
            AssertEntity(x.SynthBound.FlowNode            .ToWavHeader(),                        test);
            AssertEntity(x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes),             test);
            AssertEntity(x.SynthBound.ConfigSection       .ToWavHeader(),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            AssertEntity(x.TapeBound.Tape                 .ToWavHeader(),                        test);
            AssertEntity(x.TapeBound.TapeConfig           .ToWavHeader(),                        test);
            AssertEntity(x.TapeBound.TapeActions          .ToWavHeader(),                        test);
            AssertEntity(x.TapeBound.TapeAction           .ToWavHeader(),                        test);
            AssertEntity(x.BuffBound.Buff                 .ToWavHeader(),                        test);
            AssertEntity(x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount), test);
            AssertEntity(x.BuffBound.AudioFileOutput      .ToWavHeader(),                        test);
            AssertEntity(x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount), test);
            AssertEntity(x.Independent.Sample             .ToWavHeader(),                        test);
            AssertEntity(x.Independent.AudioFileInfo      .ToWavHeader(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithInts    .ToWavHeader(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithType    .ToWavHeader(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithEnums   .ToWavHeader(),                        test);
            AssertEntity(x.Immutable.InfoTupleWithEntities.ToWavHeader(),                        test);
            AssertEntity(ToWavHeader(x.SynthBound.SynthWishes          ),                        test);
            AssertEntity(ToWavHeader(x.SynthBound.FlowNode             ),                        test);
            AssertEntity(ToWavHeader(x.SynthBound.ConfigResolver       ,synthWishes),            test);
            AssertEntity(ToWavHeader(x.SynthBound.ConfigSection        ),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            AssertEntity(ToWavHeader(x.TapeBound.Tape                  ),                        test);
            AssertEntity(ToWavHeader(x.TapeBound.TapeConfig            ),                        test);
            AssertEntity(ToWavHeader(x.TapeBound.TapeActions           ),                        test);
            AssertEntity(ToWavHeader(x.TapeBound.TapeAction            ),                        test);
            AssertEntity(ToWavHeader(x.BuffBound.Buff                  ),                        test);
            AssertEntity(ToWavHeader(x.BuffBound.Buff                  ).FrameCount(frameCount), test);
            AssertEntity(ToWavHeader(x.BuffBound.AudioFileOutput       ),                        test);
            AssertEntity(ToWavHeader(x.BuffBound.AudioFileOutput       ).FrameCount(frameCount), test);
            AssertEntity(ToWavHeader(x.Independent.Sample              ),                        test);
            AssertEntity(ToWavHeader(x.Independent.AudioFileInfo       ),                        test);
            AssertEntity(ToWavHeader(x.Immutable.InfoTupleWithInts     ),                        test);
            AssertEntity(ToWavHeader(x.Immutable.InfoTupleWithType     ),                        test);
            AssertEntity(ToWavHeader(x.Immutable.InfoTupleWithEnums    ),                        test);
            AssertEntity(ToWavHeader(x.Immutable.InfoTupleWithEntities ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.SynthBound.SynthWishes           ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.SynthBound.FlowNode              ),                        test);
            AssertEntity(WavWishesAccessor.ToWavHeader(x.SynthBound.ConfigResolver, synthWishes),           test);
            AssertEntity(WavWishesAccessor.ToWavHeader(x.SynthBound.ConfigSection ),                DefaultsCase); // By Design: Mocked ConfigSection has default settings.
            AssertEntity(WavWishes.ToWavHeader(x.TapeBound.Tape                   ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.TapeBound.TapeConfig             ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.TapeBound.TapeActions            ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.TapeBound.TapeAction             ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.BuffBound.Buff                   ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.BuffBound.Buff                   ).FrameCount(frameCount), test);
            AssertEntity(WavWishes.ToWavHeader(x.BuffBound.AudioFileOutput        ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.BuffBound.AudioFileOutput        ).FrameCount(frameCount), test);
            AssertEntity(WavWishes.ToWavHeader(x.Independent.Sample               ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.Independent.AudioFileInfo        ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.Immutable.InfoTupleWithInts      ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.Immutable.InfoTupleWithType      ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.Immutable.InfoTupleWithEnums     ),                        test);
            AssertEntity(WavWishes.ToWavHeader(x.Immutable.InfoTupleWithEntities  ),                        test);
            if      (test.Bits ==  8) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> (), test);
            else if (test.Bits == 16) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>(), test);
            else if (test.Bits == 32) AssertEntity(x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>(), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
            if      (test.Bits ==  8) AssertEntity(ToWavHeader<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) AssertEntity(ToWavHeader<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) AssertEntity(ToWavHeader<float>(x.Immutable.InfoTupleWithoutBits), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
            if      (test.Bits ==  8) AssertEntity(WavWishes.ToWavHeader<byte> (x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 16) AssertEntity(WavWishes.ToWavHeader<short>(x.Immutable.InfoTupleWithoutBits), test);
            else if (test.Bits == 32) AssertEntity(WavWishes.ToWavHeader<float>(x.Immutable.InfoTupleWithoutBits), test);
            else AssertBits(test.Bits); // ncrunch: no coverage
        }
        
        [TestMethod]
        [DynamicData(nameof(TransitionCases))]
        public void ApplyWavHeader_Test(string caseKey)
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
            
            TestProp((x, wav) => { AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes   .ApplyWavHeader(wav)             ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode      .ApplyWavHeader(wav)             ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.ApplyWavHeader(wav, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(x.TapeBound.Tape           , () => x.TapeBound.Tape           .ApplyWavHeader(wav)             ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeConfig     , () => x.TapeBound.TapeConfig     .ApplyWavHeader(wav)             ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeActions    , () => x.TapeBound.TapeActions    .ApplyWavHeader(wav)             ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeAction     , () => x.TapeBound.TapeAction     .ApplyWavHeader(wav)             ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.Buff           , () => x.BuffBound.Buff           .ApplyWavHeader(wav,     context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.ApplyWavHeader(wav,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.Sample       , () => x.Independent.Sample       .ApplyWavHeader(wav,     context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.ApplyWavHeader(wav)             ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.ApplyWavHeader(wav)             ); AssertEntity(x.Independent.AudioInfoWish, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => wav.ApplyWavHeader(x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.SynthWishes,    () => ApplyWavHeader(x.SynthBound.SynthWishes,    wav)             ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.FlowNode,       () => ApplyWavHeader(x.SynthBound.FlowNode,       wav)             ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.ConfigResolver, () => ApplyWavHeader(x.SynthBound.ConfigResolver, wav, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(x.TapeBound.Tape,            () => ApplyWavHeader(x.TapeBound.Tape,            wav)             ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeConfig,      () => ApplyWavHeader(x.TapeBound.TapeConfig,      wav)             ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeActions,     () => ApplyWavHeader(x.TapeBound.TapeActions,     wav)             ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeAction,      () => ApplyWavHeader(x.TapeBound.TapeAction,      wav)             ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.Buff,            () => ApplyWavHeader(x.BuffBound.Buff,            wav,     context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.AudioFileOutput, () => ApplyWavHeader(x.BuffBound.AudioFileOutput, wav,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.Sample,        () => ApplyWavHeader(x.Independent.Sample,        wav,     context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioFileInfo, () => ApplyWavHeader(x.Independent.AudioFileInfo, wav)             ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioInfoWish, () => ApplyWavHeader(x.Independent.AudioInfoWish, wav)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => ApplyWavHeader(wav, x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.SynthWishes,    () => WavWishes.ApplyWavHeader(x.SynthBound.SynthWishes,    wav)                     ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.FlowNode,       () => WavWishes.ApplyWavHeader(x.SynthBound.FlowNode,       wav)                     ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.ApplyWavHeader(x.SynthBound.ConfigResolver, wav, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(x.TapeBound.Tape,            () => WavWishes.ApplyWavHeader(x.TapeBound.Tape,            wav)                     ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeConfig,      () => WavWishes.ApplyWavHeader(x.TapeBound.TapeConfig,      wav)                     ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeActions,     () => WavWishes.ApplyWavHeader(x.TapeBound.TapeActions,     wav)                     ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(x.TapeBound.TapeAction,      () => WavWishes.ApplyWavHeader(x.TapeBound.TapeAction,      wav)                     ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.Buff,            () => WavWishes.ApplyWavHeader(x.BuffBound.Buff,            wav,             context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(x.BuffBound.AudioFileOutput, () => WavWishes.ApplyWavHeader(x.BuffBound.AudioFileOutput, wav,             context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.Sample,        () => WavWishes.ApplyWavHeader(x.Independent.Sample,        wav,             context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioFileInfo, () => WavWishes.ApplyWavHeader(x.Independent.AudioFileInfo, wav)                     ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(x.Independent.AudioInfoWish, () => WavWishes.ApplyWavHeader(x.Independent.AudioInfoWish, wav)                     ); AssertEntity(x.Independent.AudioInfoWish, test); });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.SynthBound.SynthWishes   )                     ); AssertEntity(x.SynthBound.SynthWishes,    test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.SynthBound.FlowNode      )                     ); AssertEntity(x.SynthBound.FlowNode,       test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishesAccessor.ApplyWavHeader(wav, x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.TapeBound.Tape           )                     ); AssertEntity(x.TapeBound.Tape,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.TapeBound.TapeConfig     )                     ); AssertEntity(x.TapeBound.TapeConfig,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.TapeBound.TapeActions    )                     ); AssertEntity(x.TapeBound.TapeActions,     test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.TapeBound.TapeAction     )                     ); AssertEntity(x.TapeBound.TapeAction,      test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.BuffBound.Buff           ,             context)); AssertEntity(x.BuffBound.Buff,            test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.BuffBound.AudioFileOutput,             context)); AssertEntity(x.BuffBound.AudioFileOutput, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.Independent.Sample       ,             context)); AssertEntity(x.Independent.Sample,        test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.Independent.AudioFileInfo)                     ); AssertEntity(x.Independent.AudioFileInfo, test);              });
            TestProp((x, wav) => { AreEqual(wav, () => WavWishes.ApplyWavHeader(wav, x.Independent.AudioInfoWish)                     ); AssertEntity(x.Independent.AudioInfoWish, test);              });
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
                
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes     , () => x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes     , () => x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes     , () => x.SynthBound.SynthWishes     .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes     , () => x.SynthBound.SynthWishes     .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode        , () => x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode        , () => x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode        , () => x.SynthBound.FlowNode        .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode        , () => x.SynthBound.FlowNode        .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver  , () => x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceFilePath    , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver  , () => x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceBytes       , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver  , () => x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.SourceStream      , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver  , () => x.SynthBound.ConfigResolver  .ReadWavHeader(binaries.BinaryReader      , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.TapeBound.Tape             , () => x.TapeBound.Tape             .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape             , () => x.TapeBound.Tape             .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape             , () => x.TapeBound.Tape             .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape             , () => x.TapeBound.Tape             .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig       , () => x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig       , () => x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig       , () => x.TapeBound.TapeConfig       .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig       , () => x.TapeBound.TapeConfig       .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions      , () => x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions      , () => x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions      , () => x.TapeBound.TapeActions      .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions      , () => x.TapeBound.TapeActions      .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction       , () => x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction       , () => x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction       , () => x.TapeBound.TapeAction       .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction       , () => x.TapeBound.TapeAction       .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff             , () => x.BuffBound.Buff             .ReadWavHeader(binaries.SourceFilePath    ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff             , () => x.BuffBound.Buff             .ReadWavHeader(binaries.SourceBytes       ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff             , () => x.BuffBound.Buff             .ReadWavHeader(binaries.SourceStream      ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff             , () => x.BuffBound.Buff             .ReadWavHeader(binaries.BinaryReader      ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput  , () => x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceFilePath    ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput  , () => x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceBytes       ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput  , () => x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.SourceStream      ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput  , () => x.BuffBound.AudioFileOutput  .ReadWavHeader(binaries.BinaryReader      ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.Independent.Sample         , () => x.Independent.Sample         .ReadWavHeader(binaries.SourceFilePath    ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample         , () => x.Independent.Sample         .ReadWavHeader(binaries.SourceBytes       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample         , () => x.Independent.Sample         .ReadWavHeader(binaries.SourceStream      ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample         , () => x.Independent.Sample         .ReadWavHeader(binaries.BinaryReader      ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo  , () => x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo  , () => x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo  , () => x.Independent.AudioFileInfo  .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo  , () => x.Independent.AudioFileInfo  .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish  , () => x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceFilePath    )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish  , () => x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceBytes       )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish  , () => x.Independent.AudioInfoWish  .ReadWavHeader(binaries.SourceStream      )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish  , () => x.Independent.AudioInfoWish  .ReadWavHeader(binaries.BinaryReader      )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath      , () => binaries.SourceFilePath      .ReadWavHeader(x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes         , () => binaries.SourceBytes         .ReadWavHeader(x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceStream        , () => binaries.SourceStream        .ReadWavHeader(x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader        , () => binaries.BinaryReader        .ReadWavHeader(x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => ReadWavHeader(x.SynthBound.SynthWishes,     binaries.SourceFilePath    )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => ReadWavHeader(x.SynthBound.SynthWishes,     binaries.SourceBytes       )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => ReadWavHeader(x.SynthBound.SynthWishes,     binaries.SourceStream      )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => ReadWavHeader(x.SynthBound.SynthWishes,     binaries.BinaryReader      )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => ReadWavHeader(x.SynthBound.FlowNode,        binaries.SourceFilePath    )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => ReadWavHeader(x.SynthBound.FlowNode,        binaries.SourceBytes       )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => ReadWavHeader(x.SynthBound.FlowNode,        binaries.SourceStream      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => ReadWavHeader(x.SynthBound.FlowNode,        binaries.BinaryReader      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => ReadWavHeader(x.SynthBound.ConfigResolver,  binaries.SourceFilePath    , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => ReadWavHeader(x.SynthBound.ConfigResolver,  binaries.SourceBytes       , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => ReadWavHeader(x.SynthBound.ConfigResolver,  binaries.SourceStream      , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => ReadWavHeader(x.SynthBound.ConfigResolver,  binaries.BinaryReader      , synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => ReadWavHeader(x.TapeBound.Tape,             binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => ReadWavHeader(x.TapeBound.Tape,             binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => ReadWavHeader(x.TapeBound.Tape,             binaries.SourceStream      )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => ReadWavHeader(x.TapeBound.Tape,             binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => ReadWavHeader(x.TapeBound.TapeConfig,       binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => ReadWavHeader(x.TapeBound.TapeConfig,       binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => ReadWavHeader(x.TapeBound.TapeConfig,       binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => ReadWavHeader(x.TapeBound.TapeConfig,       binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => ReadWavHeader(x.TapeBound.TapeActions,      binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => ReadWavHeader(x.TapeBound.TapeActions,      binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => ReadWavHeader(x.TapeBound.TapeActions,      binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => ReadWavHeader(x.TapeBound.TapeActions,      binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => ReadWavHeader(x.TapeBound.TapeAction,       binaries.SourceFilePath    )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => ReadWavHeader(x.TapeBound.TapeAction,       binaries.SourceBytes       )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => ReadWavHeader(x.TapeBound.TapeAction,       binaries.SourceStream      )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => ReadWavHeader(x.TapeBound.TapeAction,       binaries.BinaryReader      )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => ReadWavHeader(x.BuffBound.Buff,             binaries.SourceFilePath    ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => ReadWavHeader(x.BuffBound.Buff,             binaries.SourceBytes       ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => ReadWavHeader(x.BuffBound.Buff,             binaries.SourceStream      ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => ReadWavHeader(x.BuffBound.Buff,             binaries.BinaryReader      ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => ReadWavHeader(x.BuffBound.AudioFileOutput,  binaries.SourceFilePath    ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => ReadWavHeader(x.BuffBound.AudioFileOutput,  binaries.SourceBytes       ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => ReadWavHeader(x.BuffBound.AudioFileOutput,  binaries.SourceStream      ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => ReadWavHeader(x.BuffBound.AudioFileOutput,  binaries.BinaryReader      ,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => ReadWavHeader(x.Independent.Sample,         binaries.SourceFilePath    ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => ReadWavHeader(x.Independent.Sample,         binaries.SourceBytes       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => ReadWavHeader(x.Independent.Sample,         binaries.SourceStream      ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => ReadWavHeader(x.Independent.Sample,         binaries.BinaryReader      ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => ReadWavHeader(x.Independent.AudioFileInfo,  binaries.SourceFilePath    )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => ReadWavHeader(x.Independent.AudioFileInfo,  binaries.SourceBytes       )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => ReadWavHeader(x.Independent.AudioFileInfo,  binaries.SourceStream      )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => ReadWavHeader(x.Independent.AudioFileInfo,  binaries.BinaryReader      )             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => ReadWavHeader(x.Independent.AudioInfoWish,  binaries.SourceFilePath    )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => ReadWavHeader(x.Independent.AudioInfoWish,  binaries.SourceBytes       )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => ReadWavHeader(x.Independent.AudioInfoWish,  binaries.SourceStream      )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => ReadWavHeader(x.Independent.AudioInfoWish,  binaries.BinaryReader      )             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.SynthBound.SynthWishes   )             ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.SynthBound.FlowNode      )             ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.TapeBound.Tape           )             ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.TapeBound.TapeConfig     )             ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.TapeBound.TapeActions    )             ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.TapeBound.TapeAction     )             ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.BuffBound.Buff           ,     context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.BuffBound.AudioFileOutput,     context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.Independent.Sample       ,     context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.Independent.AudioFileInfo)             ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => ReadWavHeader(binaries.SourceFilePath,      x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => ReadWavHeader(binaries.SourceBytes,         x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => ReadWavHeader(binaries.SourceStream,        x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => ReadWavHeader(binaries.BinaryReader,        x.Independent.AudioInfoWish)             ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => WavWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceFilePath    )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => WavWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceBytes       )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => WavWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.SourceStream      )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.SynthWishes,      () => WavWishes.ReadWavHeader(x.SynthBound.SynthWishes,      binaries.BinaryReader      )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => WavWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceFilePath    )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => WavWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceBytes       )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => WavWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.SourceStream      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.FlowNode,         () => WavWishes.ReadWavHeader(x.SynthBound.FlowNode,         binaries.BinaryReader      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => WavWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver, binaries.SourceFilePath, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => WavWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver, binaries.SourceBytes,    synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => WavWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver, binaries.SourceStream,   synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.SynthBound.ConfigResolver,   () => WavWishesAccessor.ReadWavHeader(x.SynthBound.ConfigResolver, binaries.BinaryReader,   synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => WavWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceFilePath    )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => WavWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceBytes       )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => WavWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.SourceStream      )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.Tape,              () => WavWishes.ReadWavHeader(x.TapeBound.Tape,              binaries.BinaryReader      )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceFilePath    )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceBytes       )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.SourceStream      )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeConfig,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeConfig,        binaries.BinaryReader      )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => WavWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceFilePath    )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => WavWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceBytes       )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => WavWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.SourceStream      )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeActions,       () => WavWishes.ReadWavHeader(x.TapeBound.TapeActions,       binaries.BinaryReader      )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceFilePath    )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceBytes       )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.SourceStream      )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.TapeBound.TapeAction,        () => WavWishes.ReadWavHeader(x.TapeBound.TapeAction,        binaries.BinaryReader      )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => WavWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceFilePath    ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => WavWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceBytes       ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => WavWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.SourceStream      ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.Buff,              () => WavWishes.ReadWavHeader(x.BuffBound.Buff,              binaries.BinaryReader      ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => WavWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceFilePath    ,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => WavWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceBytes       ,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => WavWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.SourceStream      ,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.BuffBound.AudioFileOutput,   () => WavWishes.ReadWavHeader(x.BuffBound.AudioFileOutput,   binaries.BinaryReader      ,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => WavWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceFilePath    ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => WavWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceBytes       ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => WavWishes.ReadWavHeader(x.Independent.Sample,          binaries.SourceStream      ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.Sample,          () => WavWishes.ReadWavHeader(x.Independent.Sample,          binaries.BinaryReader      ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => WavWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceFilePath    )               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => WavWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceBytes       )               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => WavWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.SourceStream      )               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioFileInfo,   () => WavWishes.ReadWavHeader(x.Independent.AudioFileInfo,   binaries.BinaryReader      )               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => WavWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceFilePath    )               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => WavWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceBytes       )               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => WavWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.SourceStream      )               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(x.Independent.AudioInfoWish,   () => WavWishes.ReadWavHeader(x.Independent.AudioInfoWish,   binaries.BinaryReader      )               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.SynthBound.SynthWishes   )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.SynthBound.SynthWishes   )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.SynthBound.SynthWishes   )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.SynthBound.SynthWishes   )               ); AssertEntity(x.SynthBound.SynthWishes,    test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.SynthBound.FlowNode      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.SynthBound.FlowNode      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.SynthBound.FlowNode      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.SynthBound.FlowNode      )               ); AssertEntity(x.SynthBound.FlowNode,       test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishesAccessor.ReadWavHeader(binaries.SourceFilePath, x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishesAccessor.ReadWavHeader(binaries.SourceBytes,    x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishesAccessor.ReadWavHeader(binaries.SourceStream,   x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishesAccessor.ReadWavHeader(binaries.BinaryReader,   x.SynthBound.ConfigResolver, synthWishes)); AssertEntity(x.SynthBound.ConfigResolver, test, synthWishes); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.TapeBound.Tape           )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.TapeBound.Tape           )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.TapeBound.Tape           )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.TapeBound.Tape           )               ); AssertEntity(x.TapeBound.Tape,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.TapeBound.TapeConfig     )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.TapeBound.TapeConfig     )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.TapeBound.TapeConfig     )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.TapeBound.TapeConfig     )               ); AssertEntity(x.TapeBound.TapeConfig,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.TapeBound.TapeActions    )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.TapeBound.TapeActions    )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.TapeBound.TapeActions    )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.TapeBound.TapeActions    )               ); AssertEntity(x.TapeBound.TapeActions,     test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.TapeBound.TapeAction     )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.TapeBound.TapeAction     )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.TapeBound.TapeAction     )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.TapeBound.TapeAction     )               ); AssertEntity(x.TapeBound.TapeAction,      test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.BuffBound.Buff           ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.BuffBound.Buff           ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.BuffBound.Buff           ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.BuffBound.Buff           ,       context)); AssertEntity(x.BuffBound.Buff,            test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.BuffBound.AudioFileOutput,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.BuffBound.AudioFileOutput,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.BuffBound.AudioFileOutput,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.BuffBound.AudioFileOutput,       context)); AssertEntity(x.BuffBound.AudioFileOutput, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.Independent.Sample       ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.Independent.Sample       ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.Independent.Sample       ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.Independent.Sample       ,       context)); AssertEntity(x.Independent.Sample,        test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.Independent.AudioFileInfo)               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.Independent.AudioFileInfo)               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.Independent.AudioFileInfo)               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.Independent.AudioFileInfo)               ); AssertEntity(x.Independent.AudioFileInfo, test); });
                TestProp(x => { AreEqual(binaries.SourceFilePath,       () => WavWishes.ReadWavHeader(binaries.SourceFilePath,       x.Independent.AudioInfoWish)               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceBytes,          () => WavWishes.ReadWavHeader(binaries.SourceBytes,          x.Independent.AudioInfoWish)               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.SourceStream,         () => WavWishes.ReadWavHeader(binaries.SourceStream,         x.Independent.AudioInfoWish)               ); AssertEntity(x.Independent.AudioInfoWish, test); });
                TestProp(x => { AreEqual(binaries.BinaryReader,         () => WavWishes.ReadWavHeader(binaries.BinaryReader,         x.Independent.AudioInfoWish)               ); AssertEntity(x.Independent.AudioInfoWish, test); });
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
                    AssertEntity(info, test);
                    
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
                AssertRead(() => WavWishes.ReadAudioInfo(x.BuffBound.SourceFilePath));
                AssertRead(() => WavWishes.ReadAudioInfo(x.BuffBound.SourceBytes   ));
                AssertRead(() => WavWishes.ReadAudioInfo(x.BuffBound.SourceStream  ));
                AssertRead(() => WavWishes.ReadAudioInfo(x.BuffBound.BinaryReader  ));
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WriteWavHeader_Test(string caseKey)
        {
            Case test = Cases[caseKey];
            TestEntities x = CreateEntities(test);
            AssertInvariant(x, test);
            var synthWishes = x.SynthBound.SynthWishes;
            int courtesy = test.CourtesyFrames;
            
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes     .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes     .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes     .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes     .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode        .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode        .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode        .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode        .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestFilePath,  synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestBytes,     synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver  .WriteWavHeader(bin.DestStream,    synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver  .WriteWavHeader(bin.BinaryWriter,  synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape           , () => x.TapeBound.Tape             .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape           , () => x.TapeBound.Tape             .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape           , () => x.TapeBound.Tape             .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape           , () => x.TapeBound.Tape             .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig     , () => x.TapeBound.TapeConfig       .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig     , () => x.TapeBound.TapeConfig       .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig     , () => x.TapeBound.TapeConfig       .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig     , () => x.TapeBound.TapeConfig       .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions    , () => x.TapeBound.TapeActions      .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions    , () => x.TapeBound.TapeActions      .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions    , () => x.TapeBound.TapeActions      .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions    , () => x.TapeBound.TapeActions      .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction     , () => x.TapeBound.TapeAction       .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction     , () => x.TapeBound.TapeAction       .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction     , () => x.TapeBound.TapeAction       .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction     , () => x.TapeBound.TapeAction       .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample       , () => x.Independent.Sample         .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample       , () => x.Independent.Sample         .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.Sample       , () => x.Independent.Sample         .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.Sample       , () => x.Independent.Sample         .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish  .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish  .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish  .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish  .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo  .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo  .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo  .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo  .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .WriteWavHeader(bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .WriteWavHeader(bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .WriteWavHeader(bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .WriteWavHeader(bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .Write         (bin.DestFilePath              )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .Write         (bin.DestBytes                 )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .Write         (bin.DestStream                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader      , () => x.Immutable.WavHeader        .Write         (bin.BinaryWriter              )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.SynthBound.SynthWishes                   )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.SynthBound.SynthWishes                   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.SynthBound.SynthWishes                   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.SynthBound.SynthWishes                   )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.SynthBound.FlowNode                      )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.SynthBound.FlowNode                      )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.SynthBound.FlowNode                      )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.SynthBound.FlowNode                      )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.SynthBound.ConfigResolver,    synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.SynthBound.ConfigResolver,    synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.SynthBound.ConfigResolver,    synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.SynthBound.ConfigResolver,    synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.TapeBound.Tape                           )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.TapeBound.Tape                           )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.TapeBound.Tape                           )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.TapeBound.Tape                           )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.TapeBound.TapeConfig                     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.TapeBound.TapeConfig                     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.TapeBound.TapeConfig                     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.TapeBound.TapeConfig                     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.TapeBound.TapeActions                    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.TapeBound.TapeActions                    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.TapeBound.TapeActions                    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.TapeBound.TapeActions                    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.TapeBound.TapeAction                     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.TapeBound.TapeAction                     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.TapeBound.TapeAction                     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.TapeBound.TapeAction                     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.Independent.Sample                       )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.Independent.Sample                       )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.Independent.Sample                       )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.Independent.Sample                       )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.Independent.AudioInfoWish                )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.Independent.AudioInfoWish                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.Independent.AudioInfoWish                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.Independent.AudioInfoWish                )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.Independent.AudioFileInfo                )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.Independent.AudioFileInfo                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.Independent.AudioFileInfo                )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.Independent.AudioFileInfo                )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.WriteWavHeader(x.Immutable.WavHeader                      )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .WriteWavHeader(x.Immutable.WavHeader                      )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .WriteWavHeader(x.Immutable.WavHeader                      )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.WriteWavHeader(x.Immutable.WavHeader                      )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath           , () => bin.DestFilePath.Write         (x.Immutable.WavHeader                      )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes              , () => bin.DestBytes   .Write         (x.Immutable.WavHeader                      )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream             , () => bin.DestStream  .Write         (x.Immutable.WavHeader                      )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter           , () => bin.BinaryWriter.Write         (x.Immutable.WavHeader                      )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WriteWavHeader(x.SynthBound.SynthWishes,      bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WriteWavHeader(x.SynthBound.FlowNode,         bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WriteWavHeader(x.SynthBound.FlowNode,         bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WriteWavHeader(x.SynthBound.FlowNode,         bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WriteWavHeader(x.SynthBound.FlowNode,         bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WriteWavHeader(x.SynthBound.ConfigResolver,   bin.DestFilePath, synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WriteWavHeader(x.SynthBound.ConfigResolver,   bin.DestBytes,    synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WriteWavHeader(x.SynthBound.ConfigResolver,   bin.DestStream,   synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WriteWavHeader(x.SynthBound.ConfigResolver,   bin.BinaryWriter, synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WriteWavHeader(x.TapeBound.Tape,              bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WriteWavHeader(x.TapeBound.Tape,              bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WriteWavHeader(x.TapeBound.Tape,              bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WriteWavHeader(x.TapeBound.Tape,              bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WriteWavHeader(x.TapeBound.TapeConfig,        bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WriteWavHeader(x.TapeBound.TapeActions,       bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WriteWavHeader(x.TapeBound.TapeActions,       bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WriteWavHeader(x.TapeBound.TapeActions,       bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WriteWavHeader(x.TapeBound.TapeActions,       bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WriteWavHeader(x.TapeBound.TapeAction,        bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WriteWavHeader(x.TapeBound.TapeAction,        bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WriteWavHeader(x.TapeBound.TapeAction,        bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WriteWavHeader(x.TapeBound.TapeAction,        bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WriteWavHeader(x.Independent.Sample,          bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WriteWavHeader(x.Independent.Sample,          bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WriteWavHeader(x.Independent.Sample,          bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WriteWavHeader(x.Independent.Sample,          bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WriteWavHeader(x.Independent.AudioInfoWish,   bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WriteWavHeader(x.Independent.AudioFileInfo,   bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WriteWavHeader(x.Immutable.WavHeader,         bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WriteWavHeader(x.Immutable.WavHeader,         bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WriteWavHeader(x.Immutable.WavHeader,         bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WriteWavHeader(x.Immutable.WavHeader,         bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => Write         (x.Immutable.WavHeader,         bin.DestFilePath             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => Write         (x.Immutable.WavHeader,         bin.DestBytes                )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => Write         (x.Immutable.WavHeader,         bin.DestStream               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => Write         (x.Immutable.WavHeader,         bin.BinaryWriter             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.SynthBound.SynthWishes                  )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.SynthBound.SynthWishes                  )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.SynthBound.SynthWishes                  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.SynthBound.SynthWishes                  )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.SynthBound.FlowNode                     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.SynthBound.FlowNode                     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.SynthBound.FlowNode                     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.SynthBound.FlowNode                     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.SynthBound.ConfigResolver,   synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.SynthBound.ConfigResolver,   synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.SynthBound.ConfigResolver,   synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.SynthBound.ConfigResolver,   synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.TapeBound.Tape                          )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.TapeBound.Tape                          )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.TapeBound.Tape                          )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.TapeBound.Tape                          )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeConfig                    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeConfig                    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.TapeBound.TapeConfig                    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeConfig                    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeActions                   )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeActions                   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.TapeBound.TapeActions                   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeActions                   )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeAction                    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeAction                    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.TapeBound.TapeAction                    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeAction                    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.Independent.Sample                      )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.Independent.Sample                      )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.Independent.Sample                      )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.Independent.Sample                      )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.Independent.AudioInfoWish               )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.Independent.AudioInfoWish               )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.Independent.AudioInfoWish               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.Independent.AudioInfoWish               )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.Independent.AudioFileInfo               )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.Independent.AudioFileInfo               )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.Independent.AudioFileInfo               )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.Independent.AudioFileInfo               )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WriteWavHeader(bin.DestFilePath, x.Immutable.WavHeader                     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WriteWavHeader(bin.DestBytes,    x.Immutable.WavHeader                     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WriteWavHeader(bin.DestStream,   x.Immutable.WavHeader                     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WriteWavHeader(bin.BinaryWriter, x.Immutable.WavHeader                     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => Write         (bin.DestFilePath, x.Immutable.WavHeader                     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => Write         (bin.DestBytes,    x.Immutable.WavHeader                     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => Write         (bin.DestStream,   x.Immutable.WavHeader                     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => Write         (bin.BinaryWriter, x.Immutable.WavHeader                     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WavWishes.WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WavWishes.WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WavWishes.WriteWavHeader(x.SynthBound.SynthWishes,      bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.SynthWishes,    () => WavWishes.WriteWavHeader(x.SynthBound.SynthWishes,      bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WavWishes.WriteWavHeader(x.SynthBound.FlowNode,         bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WavWishes.WriteWavHeader(x.SynthBound.FlowNode,         bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WavWishes.WriteWavHeader(x.SynthBound.FlowNode,         bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.FlowNode,       () => WavWishes.WriteWavHeader(x.SynthBound.FlowNode,         bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.WriteWavHeader(x.SynthBound.ConfigResolver, bin.DestFilePath, synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.WriteWavHeader(x.SynthBound.ConfigResolver, bin.DestBytes,    synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.WriteWavHeader(x.SynthBound.ConfigResolver, bin.DestStream,   synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.SynthBound.ConfigResolver, () => WavWishesAccessor.WriteWavHeader(x.SynthBound.ConfigResolver, bin.BinaryWriter, synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WavWishes.WriteWavHeader(x.TapeBound.Tape,              bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WavWishes.WriteWavHeader(x.TapeBound.Tape,              bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WavWishes.WriteWavHeader(x.TapeBound.Tape,              bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.Tape,            () => WavWishes.WriteWavHeader(x.TapeBound.Tape,              bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeConfig,        bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeConfig,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeConfig,        bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WavWishes.WriteWavHeader(x.TapeBound.TapeActions,       bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WavWishes.WriteWavHeader(x.TapeBound.TapeActions,       bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WavWishes.WriteWavHeader(x.TapeBound.TapeActions,       bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeActions,     () => WavWishes.WriteWavHeader(x.TapeBound.TapeActions,       bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeAction,        bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeAction,        bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeAction,        bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.TapeBound.TapeAction,      () => WavWishes.WriteWavHeader(x.TapeBound.TapeAction,        bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WavWishes.WriteWavHeader(x.Independent.Sample,          bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WavWishes.WriteWavHeader(x.Independent.Sample,          bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WavWishes.WriteWavHeader(x.Independent.Sample,          bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.Sample,        () => WavWishes.WriteWavHeader(x.Independent.Sample,          bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WavWishes.WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WavWishes.WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WavWishes.WriteWavHeader(x.Independent.AudioInfoWish,   bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioInfoWish, () => WavWishes.WriteWavHeader(x.Independent.AudioInfoWish,   bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WavWishes.WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WavWishes.WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WavWishes.WriteWavHeader(x.Independent.AudioFileInfo,   bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Independent.AudioFileInfo, () => WavWishes.WriteWavHeader(x.Independent.AudioFileInfo,   bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.WriteWavHeader(x.Immutable.WavHeader,         bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.WriteWavHeader(x.Immutable.WavHeader,         bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.WriteWavHeader(x.Immutable.WavHeader,         bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.WriteWavHeader(x.Immutable.WavHeader,         bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.Write         (x.Immutable.WavHeader,         bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.Write         (x.Immutable.WavHeader,         bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.Write         (x.Immutable.WavHeader,         bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.Immutable.WavHeader,       () => WavWishes.Write         (x.Immutable.WavHeader,         bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.SynthBound.SynthWishes     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.SynthBound.SynthWishes     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.SynthBound.SynthWishes     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.SynthBound.SynthWishes     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.SynthBound.FlowNode        )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.SynthBound.FlowNode        )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.SynthBound.FlowNode        )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.SynthBound.FlowNode        )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishesAccessor.WriteWavHeader(bin.DestFilePath, x.SynthBound.ConfigResolver, synthWishes)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishesAccessor.WriteWavHeader(bin.DestBytes,    x.SynthBound.ConfigResolver, synthWishes)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishesAccessor.WriteWavHeader(bin.DestStream,   x.SynthBound.ConfigResolver, synthWishes)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishesAccessor.WriteWavHeader(bin.BinaryWriter, x.SynthBound.ConfigResolver, synthWishes)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.TapeBound.Tape             )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.TapeBound.Tape             )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.TapeBound.Tape             )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.TapeBound.Tape             )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeConfig       )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeConfig       )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.TapeBound.TapeConfig       )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeConfig       )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeActions      )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeActions      )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.TapeBound.TapeActions      )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeActions      )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.TapeBound.TapeAction       )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.TapeBound.TapeAction       )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.TapeBound.TapeAction       )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.TapeBound.TapeAction       )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.Independent.Sample         )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.Independent.Sample         )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.Independent.Sample         )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.Independent.Sample         )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.Independent.AudioInfoWish  )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.Independent.AudioInfoWish  )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.Independent.AudioInfoWish  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.Independent.AudioInfoWish  )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.Independent.AudioFileInfo  )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.Independent.AudioFileInfo  )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.Independent.AudioFileInfo  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.Independent.AudioFileInfo  )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.WriteWavHeader(bin.DestFilePath, x.Immutable.WavHeader        )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.WriteWavHeader(bin.DestBytes,    x.Immutable.WavHeader        )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.WriteWavHeader(bin.DestStream,   x.Immutable.WavHeader        )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.Immutable.WavHeader        )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,            () => WavWishes.Write         (bin.DestFilePath, x.Immutable.WavHeader        )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,               () => WavWishes.Write         (bin.DestBytes,    x.Immutable.WavHeader        )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,              () => WavWishes.Write         (bin.DestStream,   x.Immutable.WavHeader        )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,            () => WavWishes.Write         (bin.BinaryWriter, x.Immutable.WavHeader        )), ForBinaryWriter, test);
        }
        
        [TestMethod]
        [DynamicData(nameof(InvariantCases))]
        public void WriteWavHeader_BuffBound(string caseKey)
        {
            Case test = Cases[caseKey];
            
            TestEntities entities = CreateEntities(test);
            AssertInvariant(entities, test);
            
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => entities.BuffBound.Buff           .WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => entities.BuffBound.Buff           .WriteWavHeader(bin.DestBytes    )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => entities.BuffBound.Buff           .WriteWavHeader(bin.DestStream   )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => entities.BuffBound.Buff           .WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => entities.BuffBound.AudioFileOutput.WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => entities.BuffBound.AudioFileOutput.WriteWavHeader(bin.DestBytes    )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => entities.BuffBound.AudioFileOutput.WriteWavHeader(bin.DestStream   )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => entities.BuffBound.AudioFileOutput.WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => bin.DestFilePath.WriteWavHeader(entities.BuffBound.Buff            )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => bin.DestBytes   .WriteWavHeader(entities.BuffBound.Buff            )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => bin.DestStream  .WriteWavHeader(entities.BuffBound.Buff            )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => bin.BinaryWriter.WriteWavHeader(entities.BuffBound.Buff            )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => bin.DestFilePath.WriteWavHeader(entities.BuffBound.AudioFileOutput )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => bin.DestBytes   .WriteWavHeader(entities.BuffBound.AudioFileOutput )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => bin.DestStream  .WriteWavHeader(entities.BuffBound.AudioFileOutput )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => bin.BinaryWriter.WriteWavHeader(entities.BuffBound.AudioFileOutput )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WriteWavHeader(bin.DestFilePath, entities.BuffBound.Buff           )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WriteWavHeader(bin.DestBytes,    entities.BuffBound.Buff           )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WriteWavHeader(bin.DestStream,   entities.BuffBound.Buff           )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.Buff           )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WriteWavHeader(bin.DestFilePath, entities.BuffBound.AudioFileOutput)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WriteWavHeader(bin.DestBytes,    entities.BuffBound.AudioFileOutput)), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WriteWavHeader(bin.DestStream,   entities.BuffBound.AudioFileOutput)), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.AudioFileOutput)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WriteWavHeader(entities.BuffBound.Buff,            bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WriteWavHeader(bin.DestFilePath, entities.BuffBound.Buff           )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WriteWavHeader(bin.DestBytes,    entities.BuffBound.Buff           )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WriteWavHeader(bin.DestStream,   entities.BuffBound.Buff           )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.Buff           )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WriteWavHeader(bin.DestFilePath, entities.BuffBound.AudioFileOutput)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WriteWavHeader(bin.DestBytes,    entities.BuffBound.AudioFileOutput)), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WriteWavHeader(bin.DestStream,   entities.BuffBound.AudioFileOutput)), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WriteWavHeader(bin.BinaryWriter, entities.BuffBound.AudioFileOutput)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WavWishes.WriteWavHeader(entities.BuffBound.Buff,            bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WavWishes.WriteWavHeader(entities.BuffBound.Buff,            bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WavWishes.WriteWavHeader(entities.BuffBound.Buff,            bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.Buff           , () => WavWishes.WriteWavHeader(entities.BuffBound.Buff,            bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestBytes   )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.DestStream  )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(entities.BuffBound.AudioFileOutput, () => WavWishes.WriteWavHeader(entities.BuffBound.AudioFileOutput, bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WavWishes.WriteWavHeader(bin.DestFilePath, entities.BuffBound.Buff           )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WavWishes.WriteWavHeader(bin.DestBytes,    entities.BuffBound.Buff           )), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WavWishes.WriteWavHeader(bin.DestStream,   entities.BuffBound.Buff           )), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WavWishes.WriteWavHeader(bin.BinaryWriter, entities.BuffBound.Buff           )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath                  , () => WavWishes.WriteWavHeader(bin.DestFilePath, entities.BuffBound.AudioFileOutput)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes                     , () => WavWishes.WriteWavHeader(bin.DestBytes,    entities.BuffBound.AudioFileOutput)), ForDestBytes   , test);
            AssertWrite(bin => AreEqual(bin.DestStream                    , () => WavWishes.WriteWavHeader(bin.DestStream,   entities.BuffBound.AudioFileOutput)), ForDestStream  , test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter                  , () => WavWishes.WriteWavHeader(bin.BinaryWriter, entities.BuffBound.AudioFileOutput)), ForBinaryWriter, test);
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
            
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => x.InfoTupleWithInts    .WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => x.InfoTupleWithInts    .WriteWavHeader(bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => x.InfoTupleWithInts    .WriteWavHeader(bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => x.InfoTupleWithInts    .WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => x.InfoTupleWithType    .WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => x.InfoTupleWithType    .WriteWavHeader(bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => x.InfoTupleWithType    .WriteWavHeader(bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => x.InfoTupleWithType    .WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => x.InfoTupleWithEnums   .WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => x.InfoTupleWithEnums   .WriteWavHeader(bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => x.InfoTupleWithEnums   .WriteWavHeader(bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => x.InfoTupleWithEnums   .WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => x.InfoTupleWithEntities.WriteWavHeader(bin.DestFilePath )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => x.InfoTupleWithEntities.WriteWavHeader(bin.DestBytes    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => x.InfoTupleWithEntities.WriteWavHeader(bin.DestStream   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => x.InfoTupleWithEntities.WriteWavHeader(bin.BinaryWriter )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader(x.InfoTupleWithInts     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader(x.InfoTupleWithInts     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader(x.InfoTupleWithInts     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader(x.InfoTupleWithInts     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader(x.InfoTupleWithType     )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader(x.InfoTupleWithType     )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader(x.InfoTupleWithType     )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader(x.InfoTupleWithType     )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader(x.InfoTupleWithEnums    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader(x.InfoTupleWithEnums    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader(x.InfoTupleWithEnums    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader(x.InfoTupleWithEnums    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader(x.InfoTupleWithEntities )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader(x.InfoTupleWithEntities )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader(x.InfoTupleWithEntities )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader(x.InfoTupleWithEntities )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WriteWavHeader(x.InfoTupleWithInts    , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WriteWavHeader(x.InfoTupleWithInts    , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WriteWavHeader(x.InfoTupleWithInts    , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WriteWavHeader(x.InfoTupleWithInts    , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WriteWavHeader(x.InfoTupleWithType    , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WriteWavHeader(x.InfoTupleWithType    , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WriteWavHeader(x.InfoTupleWithType    , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WriteWavHeader(x.InfoTupleWithType    , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WriteWavHeader(x.InfoTupleWithEnums   , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WriteWavHeader(x.InfoTupleWithEnums   , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WriteWavHeader(x.InfoTupleWithEnums   , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WriteWavHeader(x.InfoTupleWithEnums   , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WriteWavHeader(x.InfoTupleWithEntities, bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WriteWavHeader(x.InfoTupleWithEntities, bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WriteWavHeader(x.InfoTupleWithEntities, bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WriteWavHeader(x.InfoTupleWithEntities, bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,        () => WriteWavHeader(bin.DestFilePath, x.InfoTupleWithInts    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,           () => WriteWavHeader(bin.DestBytes   , x.InfoTupleWithInts    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,          () => WriteWavHeader(bin.DestStream  , x.InfoTupleWithInts    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,        () => WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithInts    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,        () => WriteWavHeader(bin.DestFilePath, x.InfoTupleWithType    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,           () => WriteWavHeader(bin.DestBytes   , x.InfoTupleWithType    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,          () => WriteWavHeader(bin.DestStream  , x.InfoTupleWithType    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,        () => WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithType    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,        () => WriteWavHeader(bin.DestFilePath, x.InfoTupleWithEnums   )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,           () => WriteWavHeader(bin.DestBytes   , x.InfoTupleWithEnums   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,          () => WriteWavHeader(bin.DestStream  , x.InfoTupleWithEnums   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,        () => WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithEnums   )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath,        () => WriteWavHeader(bin.DestFilePath, x.InfoTupleWithEntities)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes,           () => WriteWavHeader(bin.DestBytes   , x.InfoTupleWithEntities)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream,          () => WriteWavHeader(bin.DestStream  , x.InfoTupleWithEntities)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter,        () => WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithEntities)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => bin.DestFilePath.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => bin.DestBytes   .WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => bin.DestStream  .WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => bin.BinaryWriter.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WriteWavHeader(bin.DestFilePath, x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WriteWavHeader(bin.DestBytes   , x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WriteWavHeader(bin.DestStream  , x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WriteWavHeader(bin.BinaryWriter, x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WriteWavHeader(bin.DestFilePath, x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WriteWavHeader(bin.DestBytes   , x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WriteWavHeader(bin.DestStream  , x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WriteWavHeader(bin.BinaryWriter, x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WriteWavHeader(bin.DestFilePath, x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WriteWavHeader(bin.DestBytes   , x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WriteWavHeader(bin.DestStream  , x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WriteWavHeader(bin.BinaryWriter, x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WriteWavHeader(bin.DestFilePath, x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WriteWavHeader(bin.DestBytes   , x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WriteWavHeader(bin.DestStream  , x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WriteWavHeader(bin.BinaryWriter, x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WriteWavHeader(x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WavWishes.WriteWavHeader(x.InfoTupleWithInts    , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WavWishes.WriteWavHeader(x.InfoTupleWithInts    , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WavWishes.WriteWavHeader(x.InfoTupleWithInts    , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithInts    , () => WavWishes.WriteWavHeader(x.InfoTupleWithInts    , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WavWishes.WriteWavHeader(x.InfoTupleWithType    , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WavWishes.WriteWavHeader(x.InfoTupleWithType    , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WavWishes.WriteWavHeader(x.InfoTupleWithType    , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithType    , () => WavWishes.WriteWavHeader(x.InfoTupleWithType    , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WavWishes.WriteWavHeader(x.InfoTupleWithEnums   , bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WavWishes.WriteWavHeader(x.InfoTupleWithEnums   , bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WavWishes.WriteWavHeader(x.InfoTupleWithEnums   , bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEnums   , () => WavWishes.WriteWavHeader(x.InfoTupleWithEnums   , bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WavWishes.WriteWavHeader(x.InfoTupleWithEntities, bin.DestFilePath)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WavWishes.WriteWavHeader(x.InfoTupleWithEntities, bin.DestBytes   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WavWishes.WriteWavHeader(x.InfoTupleWithEntities, bin.DestStream  )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(x.InfoTupleWithEntities, () => WavWishes.WriteWavHeader(x.InfoTupleWithEntities, bin.BinaryWriter)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath, x.InfoTupleWithInts    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,    x.InfoTupleWithInts    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,   x.InfoTupleWithInts    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithInts    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath, x.InfoTupleWithType    )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,    x.InfoTupleWithType    )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,   x.InfoTupleWithType    )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithType    )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath, x.InfoTupleWithEnums   )), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,    x.InfoTupleWithEnums   )), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,   x.InfoTupleWithEnums   )), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithEnums   )), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath, x.InfoTupleWithEntities)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,    x.InfoTupleWithEntities)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,   x.InfoTupleWithEntities)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter, x.InfoTupleWithEntities)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath,  x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,     x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,    x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter,  x.Bits,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath,  x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,     x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,    x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter,  x.Type,               x.Channels,         x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,     x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,    x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter,  x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => AreEqual(bin.DestFilePath       , () => WavWishes.WriteWavHeader(bin.DestFilePath,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestFilePath, test);
            AssertWrite(bin => AreEqual(bin.DestBytes          , () => WavWishes.WriteWavHeader(bin.DestBytes,     x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestBytes,    test);
            AssertWrite(bin => AreEqual(bin.DestStream         , () => WavWishes.WriteWavHeader(bin.DestStream,    x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForDestStream,   test);
            AssertWrite(bin => AreEqual(bin.BinaryWriter       , () => WavWishes.WriteWavHeader(bin.BinaryWriter,  x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount)), ForBinaryWriter, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Bits,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.Type,               x.Channels,         x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataTypeEnum, x.SpeakerSetupEnum, x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestFilePath), ForDestFilePath, test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestBytes   ), ForDestBytes,    test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.DestStream  ), ForDestStream,   test);
            AssertWrite(bin => WavWishes.WriteWavHeader( x.SampleDataType,     x.SpeakerSetup,     x.SamplingRate, frameCount,  bin.BinaryWriter), ForBinaryWriter, test);
            
            if (test.Bits == 8)
            {
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>(bin.DestFilePath                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>(bin.DestBytes                     )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>(bin.DestStream                    )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<byte>(bin.BinaryWriter                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      , bin.DestFilePath.WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , bin.DestBytes   .WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , bin.DestStream  .WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , bin.BinaryWriter.WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount)                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      , bin.DestFilePath.WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount                   )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , bin.DestBytes   .WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount                   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , bin.DestStream  .WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount                   )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , bin.BinaryWriter.WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount                   )), ForBinaryWriter, test);
                AssertWrite(bin =>                                                   WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                                   WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                                   WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                                   WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits,                  WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits,                  WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits,                  WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits,                  WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      ,                  WriteWavHeader<byte>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         ,                  WriteWavHeader<byte>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        ,                  WriteWavHeader<byte>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      ,                  WriteWavHeader<byte>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      ,                  WriteWavHeader<byte>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         ,                  WriteWavHeader<byte>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        ,                  WriteWavHeader<byte>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      ,                  WriteWavHeader<byte>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
                AssertWrite(bin =>                                  WavWishes.       WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                  WavWishes.       WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                  WavWishes.       WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                  WavWishes.       WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WavWishes.       WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WavWishes.       WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WavWishes.       WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WavWishes.       WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      , WavWishes.       WriteWavHeader<byte>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , WavWishes.       WriteWavHeader<byte>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , WavWishes.       WriteWavHeader<byte>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , WavWishes.       WriteWavHeader<byte>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      , WavWishes.       WriteWavHeader<byte>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , WavWishes.       WriteWavHeader<byte>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , WavWishes.       WriteWavHeader<byte>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , WavWishes.       WriteWavHeader<byte>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
            }
            else if (test.Bits == 16)
            {
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short>(bin.DestFilePath                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short>(bin.DestBytes                     )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short>(bin.DestStream                    )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<short>(bin.BinaryWriter                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , bin.DestFilePath.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , bin.DestBytes   .WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , bin.DestStream  .WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , bin.BinaryWriter.WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount)                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , bin.DestFilePath.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount                   )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , bin.DestBytes   .WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount                   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , bin.DestStream  .WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount                   )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , bin.BinaryWriter.WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount                   )), ForBinaryWriter, test);
                AssertWrite(bin =>                                                                     WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                                                     WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                                                     WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                                                     WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        ,                  WriteWavHeader<short>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           ,                  WriteWavHeader<short>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          ,                  WriteWavHeader<short>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        ,                  WriteWavHeader<short>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        ,                  WriteWavHeader<short>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           ,                  WriteWavHeader<short>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          ,                  WriteWavHeader<short>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        ,                  WriteWavHeader<short>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<short>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<short>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , WavWishes.       WriteWavHeader<short>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , WavWishes.       WriteWavHeader<short>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , WavWishes.       WriteWavHeader<short>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , WavWishes.       WriteWavHeader<short>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , WavWishes.       WriteWavHeader<short>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , WavWishes.       WriteWavHeader<short>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , WavWishes.       WriteWavHeader<short>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , WavWishes.       WriteWavHeader<short>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
            }
            else if (test.Bits == 32)
            {
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float>(bin.DestFilePath                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float>(bin.DestBytes                     )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float>(bin.DestStream                    )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), (x.Channels, x.SamplingRate, frameCount).WriteWavHeader<float>(bin.BinaryWriter                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , bin.DestFilePath.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , bin.DestBytes   .WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , bin.DestStream  .WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount)                  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , bin.BinaryWriter.WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount)                  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , bin.DestFilePath.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount                   )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , bin.DestBytes   .WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount                   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , bin.DestStream  .WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount                   )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , bin.BinaryWriter.WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount                   )), ForBinaryWriter, test);
                AssertWrite(bin =>                                                                     WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                                                     WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                                                     WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                                                     WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount),                  WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        ,                  WriteWavHeader<float>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           ,                  WriteWavHeader<float>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          ,                  WriteWavHeader<float>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        ,                  WriteWavHeader<float>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        ,                  WriteWavHeader<float>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           ,                  WriteWavHeader<float>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          ,                  WriteWavHeader<float>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        ,                  WriteWavHeader<float>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                                    WavWishes.       WriteWavHeader<float>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual((x.Channels, x.SamplingRate, frameCount), WavWishes.       WriteWavHeader<float>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , WavWishes.       WriteWavHeader<float>(bin.DestFilePath,  x.Channels, x.SamplingRate, frameCount )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , WavWishes.       WriteWavHeader<float>(bin.DestBytes,     x.Channels, x.SamplingRate, frameCount )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , WavWishes.       WriteWavHeader<float>(bin.DestStream,    x.Channels, x.SamplingRate, frameCount )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , WavWishes.       WriteWavHeader<float>(bin.BinaryWriter,  x.Channels, x.SamplingRate, frameCount )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath                        , WavWishes.       WriteWavHeader<float>(bin.DestFilePath, (x.Channels, x.SamplingRate, frameCount))), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes                           , WavWishes.       WriteWavHeader<float>(bin.DestBytes,    (x.Channels, x.SamplingRate, frameCount))), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream                          , WavWishes.       WriteWavHeader<float>(bin.DestStream,   (x.Channels, x.SamplingRate, frameCount))), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter                        , WavWishes.       WriteWavHeader<float>(bin.BinaryWriter, (x.Channels, x.SamplingRate, frameCount))), ForBinaryWriter, test);
            }
            else AssertBits(test.Bits); // ncrunch: no coverage
        }
        
        [TestMethod]
        public void WriteWavHeader_Test_WithConfigSection()
        {
            ConfigSectionAccessor configSection = null;
            BuffBoundEntities bin = null;

            void TestSetter(Action action)
            {
                using (var entities = CreateEntities(NonDefaultCase, withDisk: true))
                {
                    AssertInvariant(entities, NonDefaultCase);

                    configSection = entities.SynthBound.ConfigSection;
                    bin = entities.BuffBound;

                    action();
                }
            }
                
            // By Design: Mocked ConfigSection has default settings.
            TestSetter(() => { AreEqual(configSection,    () => configSection.WriteWavHeader(bin.DestFilePath )); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => configSection.WriteWavHeader(bin.DestBytes    )); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => configSection.WriteWavHeader(bin.DestStream   )); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => configSection.WriteWavHeader(bin.BinaryWriter )); AssertEntity(bin.BinaryWriter, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestFilePath, () => bin.DestFilePath.WriteWavHeader(configSection )); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestBytes,    () => bin.DestBytes   .WriteWavHeader(configSection )); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestStream,   () => bin.DestStream  .WriteWavHeader(configSection )); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(bin.BinaryWriter, () => bin.BinaryWriter.WriteWavHeader(configSection )); AssertEntity(bin.BinaryWriter, DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WriteWavHeader(configSection, bin.DestFilePath)); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WriteWavHeader(configSection, bin.DestBytes   )); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WriteWavHeader(configSection, bin.DestStream  )); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WriteWavHeader(configSection, bin.BinaryWriter)); AssertEntity(bin.BinaryWriter, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestFilePath, () => WriteWavHeader(bin.DestFilePath, configSection)); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestBytes,    () => WriteWavHeader(bin.DestBytes,    configSection)); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestStream,   () => WriteWavHeader(bin.DestStream,   configSection)); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(bin.BinaryWriter, () => WriteWavHeader(bin.BinaryWriter, configSection)); AssertEntity(bin.BinaryWriter, DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WavWishesAccessor.WriteWavHeader(configSection, bin.DestFilePath)); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WavWishesAccessor.WriteWavHeader(configSection, bin.DestBytes   )); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WavWishesAccessor.WriteWavHeader(configSection, bin.DestStream  )); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(configSection,    () => WavWishesAccessor.WriteWavHeader(configSection, bin.BinaryWriter)); AssertEntity(bin.BinaryWriter, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestFilePath, () => WavWishesAccessor.WriteWavHeader(bin.DestFilePath, configSection)); AssertEntity(bin.DestFilePath, DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestBytes,    () => WavWishesAccessor.WriteWavHeader(bin.DestBytes,    configSection)); AssertEntity(bin.DestBytes,    DefaultsCase); });
            TestSetter(() => { AreEqual(bin.DestStream,   () => WavWishesAccessor.WriteWavHeader(bin.DestStream,   configSection)); AssertEntity(bin.DestStream,   DefaultsCase); });
            TestSetter(() => { AreEqual(bin.BinaryWriter, () => WavWishesAccessor.WriteWavHeader(bin.BinaryWriter, configSection)); AssertEntity(bin.BinaryWriter, DefaultsCase); });
        }
        
        
        [TestMethod]
        public void WavHeader_EdgeCase_Test()
        {
            var test = EdgeCase;
            var x = CreateEntities(test, wipeBuff: false);

            // Weird Buff case
                        
            // Buff's too Buff to budge: always returns fixed FrameCount instead of using parameterization.
            AreEqual(103, () => x.BuffBound.Buff.ToInfo().FrameCount, -Tolerance);
            AreEqual(103, () => x.BuffBound.Buff.ToInfo().FrameCount, -Tolerance);
            AreEqual(103, () => x.BuffBound.Buff.ToInfo().FrameCount, -Tolerance);
            
            // Unbuff the Buff; loosens him up and he'll budge.
            x.BuffBound.Buff.Bytes = null;
            AreEqual(100, () => x.BuffBound.Buff.ToInfo().FrameCount);
        }
         
        // Assertions Helpers
       
        private void AssertWrite(Action<BuffBoundEntities> setter, TestEntityEnum entity, Case test)
        {
            using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
            {
                var binaries = changedEntities.BuffBound;
                AssertInvariant(changedEntities, test);
                
                setter(binaries);
                
                if (entity == ForDestFilePath) AssertEntity(binaries.DestFilePath, test);
                if (entity == ForDestBytes)    AssertEntity(binaries.DestBytes,    test);
                if (entity == ForDestStream)   AssertEntity(binaries.DestStream,   test);
                if (entity == ForBinaryWriter) AssertEntity(binaries.BinaryWriter, test);
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
            FrameCountWishesTests  .Assert_All_Getters(source, test.FrameCount  .From, test.CourtesyFrames.From);
        }

        private static void AssertIsDest(TestEntities source, Case test)
        {
            SamplingRateWishesTests.Assert_All_Getters(source, test.SamplingRate.To);
            BitWishesTests         .Assert_All_Getters(source, test.Bits        .To);
            ChannelsWishesTests    .Assert_All_Getters(source, test.Channels    .To);
            FrameCountWishesTests  .Assert_All_Getters(source, test.FrameCount  .To, test.CourtesyFrames.To);
        }

        internal static void AssertEntity(SynthWishes synthWishes, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => synthWishes);
            AreEqualInt(test.Bits,         () => synthWishes.GetBits);
            AreEqualInt(test.Channels,     () => synthWishes.GetChannels);
            AreEqualInt(test.SamplingRate, () => synthWishes.GetSamplingRate);
            AreEqualInt(test.FrameCount,   () => synthWishes.GetFrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(FlowNode flowNode, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => flowNode);
            AreEqualInt(test.Bits,         () => flowNode.GetBits);
            AreEqualInt(test.Channels,     () => flowNode.GetChannels);
            AreEqualInt(test.SamplingRate, () => flowNode.GetSamplingRate);
            AreEqualInt(test.FrameCount,   () => flowNode.GetFrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(ConfigResolverAccessor configResolver, Case test, SynthWishes synthWishes)
        {
            IsNotNull(() => test);
            IsNotNull(() => configResolver);
            AreEqualInt(test.Bits,         () => configResolver.GetBits);
            AreEqualInt(test.Channels,     () => configResolver.GetChannels);
            AreEqualInt(test.SamplingRate, () => configResolver.GetSamplingRate);
            AreEqualInt(test.FrameCount,   () => configResolver.GetFrameCount(synthWishes), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(Tape tape, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => tape);
            AreEqualInt(test.Bits,         () => tape.Bits());
            AreEqualInt(test.Channels,     () => tape.Channels());
            AreEqualInt(test.SamplingRate, () => tape.SamplingRate());
            AreEqualInt(test.FrameCount,   () => tape.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(TapeConfig tapeConfig, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => tapeConfig);
            AreEqualInt(test.Bits,         () => tapeConfig.Bits);
            AreEqualInt(test.Channels,     () => tapeConfig.Channels);
            AreEqualInt(test.SamplingRate, () => tapeConfig.SamplingRate);
            AreEqualInt(test.FrameCount,   () => tapeConfig.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(TapeActions tapeActions, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => tapeActions);
            AreEqualInt(test.Bits,         () => tapeActions.Bits());
            AreEqualInt(test.Channels,     () => tapeActions.Channels());
            AreEqualInt(test.SamplingRate, () => tapeActions.SamplingRate());
            AreEqualInt(test.FrameCount,   () => tapeActions.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(TapeAction tapeAction, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => tapeAction);
            AreEqualInt(test.Bits,         () => tapeAction.Bits());
            AreEqualInt(test.Channels,     () => tapeAction.Channels());
            AreEqualInt(test.SamplingRate, () => tapeAction.SamplingRate());
            AreEqualInt(test.FrameCount,   () => tapeAction.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(Buff buff, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => buff);
            AreEqualInt(test.Bits,         () => buff.Bits());
            AreEqualInt(test.Channels,     () => buff.Channels());
            AreEqualInt(test.SamplingRate, () => buff.SamplingRate());
            AreEqualInt(test.FrameCount,   () => buff.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(AudioFileOutput audioFileOutput, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => audioFileOutput);
            AreEqualInt(test.Bits,         () => audioFileOutput.Bits());
            AreEqualInt(test.Channels,     () => audioFileOutput.Channels());
            AreEqualInt(test.SamplingRate, () => audioFileOutput.SamplingRate());
            AreEqualInt(test.FrameCount,   () => audioFileOutput.FrameCount(), - Tolerance - test.CourtesyFrames);
        }
        
        internal static void AssertEntity(Sample sample, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => sample);
            AreEqualInt(test.Bits,         () => sample.Bits());
            AreEqualInt(test.Channels,     () => sample.Channels());
            AreEqualInt(test.SamplingRate, () => sample.SamplingRate());
            // Sample ignores FrameCount changes—either its own value or 0.
            //AreEqual(test.FrameCount, () => entity.FrameCount(), -Tolerance);
        }

        internal static void AssertEntity(AudioFileInfo infoLegacy, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => infoLegacy);
            AreEqualInt(test.Bits,         () => infoLegacy.Bits());
            AreEqualInt(test.Channels,     () => infoLegacy.Channels());
            AreEqualInt(test.SamplingRate, () => infoLegacy.SamplingRate());
            AreEqualInt(test.FrameCount,   () => infoLegacy.FrameCount(), - Tolerance - test.CourtesyFrames);
        }

        internal static void AssertEntity(AudioInfoWish infoWish, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => infoWish);
            AreEqualInt(test.Bits,         () => infoWish.Bits);
            AreEqualInt(test.Channels,     () => infoWish.Channels);
            AreEqualInt(test.SamplingRate, () => infoWish.SamplingRate);
            AreEqualInt(test.FrameCount,   () => infoWish.FrameCount, test.CourtesyFrames);
        }

        internal static void AssertEntity(WavHeaderStruct wavHeader, Case test)
        {
            IsNotNull(() => test);
            AreEqualInt(test.Bits,         () => wavHeader.BitsPerValue);
            AreEqualInt(test.Channels,     () => wavHeader.ChannelCount);
            AreEqualInt(test.SamplingRate, () => wavHeader.SamplingRate);
            AreEqualInt(test.FrameCount,   () => wavHeader.FrameCount(), test.CourtesyFrames);
        }
        
        internal static void AssertEntity(string filePath, Case test)
        {
            if (!Has(filePath)) throw new NullException(() => filePath);
            if (test == null) throw new NullException(() => test);
            AssertEntity(filePath.ReadWavHeader(), test);
        }
        
        internal static void AssertEntity(byte[] bytes, Case test)
        {
            if (bytes == null) throw new NullException(() => bytes);
            if (test == null) throw new NullException(() => test);
            AssertEntity(bytes.ReadWavHeader(), test);
        }
        
        internal static void AssertEntity(Stream stream, Case test)
        {
            if (stream == null) throw new NullException(() => stream);
            if (test == null) throw new NullException(() => test);
            stream.Position = 0;
            AssertEntity(stream.ReadWavHeader(), test);
        }
        
        internal static void AssertEntity(BinaryWriter writer, Case test)
        {
            if (writer == null) throw new NullException(() => writer);
            if (writer.BaseStream == null) throw new NullException(() => writer.BaseStream);
            if (test == null) throw new NullException(() => test);
            writer.BaseStream.Position = 0;
            AssertEntity(writer.BaseStream.ReadWavHeader(), test);
        }

        /// <inheritdoc cref="docs._areequalint />
        private static void AreEqualInt(int expected, Expression<Func<int>> actualExpression) => AreEqual(expected, actualExpression);
        /// <inheritdoc cref="docs._areequalint />
        private static void AreEqualInt(int expected, Expression<Func<int>> actualExpression, int delta) => AreEqual(expected, actualExpression, delta);
        /// <inheritdoc cref="docs._areequalint />
        private static void AreEqualInt(int expected, int actual) => AreEqual(expected, actual);
    }
}
