using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using JJ.Framework.Wishes.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JJ.Framework.Wishes.Testing;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class WavHeaderWishesTests
    {
        // Test Data
        
        private int Tolerance { get; } = -1;
        
        // TODO: CaseBase without MainProp to omit the <int> type argument?
        private class Case : CaseBase<int>
        {
            public CaseProp<int> SamplingRate   { get; set; }
            public CaseProp<int> Bits           { get; set; }
            public CaseProp<int> Channels       { get; set; }
            public CaseProp<int> CourtesyFrames { get; set; }
            public CaseProp<int> FrameCount     { get; set; }
        }
        
        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case>();
        
        static CaseCollection<Case> SimpleCases { get; } = Cases.FromTemplate(new Case
        
            { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100 },
            
            new Case {                        },
            new Case { Bits           =    16 },
            new Case { Bits           =     8 },
            new Case { SamplingRate   = 96000 },
            new Case { Channels       =     1 },
            new Case { FrameCount     =   256 },
            new Case { CourtesyFrames =     4 }
        );
                
        static CaseCollection<Case> TransitionCases { get; } = Cases.FromTemplate(new Case
        
            { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100 },
            
            new Case { Bits           = { To =    16 } },
            new Case { Bits           = { To =     8 } },
            new Case { SamplingRate   = { To = 96000 } },
            new Case { Channels       = { To =     1 } },
            new Case { FrameCount     = { To =   256 } },
            new Case { CourtesyFrames = { To =     4 } }
        );

        private TestEntities CreateEntities(Case test) 
            => new TestEntities(x => x.WithBits          (test.Bits          .From)
                                      .WithChannels      (test.Channels      .From)
                                      .WithSamplingRate  (test.SamplingRate  .From)
                                      .WithFrameCount    (test.FrameCount    .From)
                                      .WithCourtesyFrames(test.CourtesyFrames.From));

        // Test Code
        
        [TestMethod]
        [DynamicData(nameof(SimpleCases))]
        public void WavHeader_ToWish(string caseKey)
        { 
            Case test = Cases[caseKey];
            TestEntities x = CreateEntities(test);
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            
            var intTuple      = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typeTuple     = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typelessTuple = (                                x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var enumTuple     = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            var entityTuple   = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);
            
            AreEqual(test.Bits,           () => x.SynthBound .SynthWishes    .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.SynthBound .SynthWishes    .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.SynthBound .SynthWishes    .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.SynthBound .SynthWishes    .ToWish().FrameCount,   Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .FlowNode       .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.SynthBound .FlowNode       .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.SynthBound .FlowNode       .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.SynthBound .FlowNode       .ToWish().FrameCount,   Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Bits        );
            AreEqual(test.Channels,       () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Channels    );
            AreEqual(test.SamplingRate,   () => x.SynthBound .ConfigResolver .ToWish(synthWishes).SamplingRate);
            AreEqual(test.FrameCount,     () => x.SynthBound .ConfigResolver .ToWish(synthWishes).FrameCount, Tolerance);
            AreEqual(DefaultBits,         () => x.SynthBound .ConfigSection  .ToWish().Bits                   );
            AreEqual(DefaultChannels,     () => x.SynthBound .ConfigSection  .ToWish().Channels               );
            AreEqual(DefaultSamplingRate, () => x.SynthBound .ConfigSection  .ToWish().SamplingRate           );
            AreEqual(DefaultFrameCount,   () => x.SynthBound .ConfigSection  .ToWish().FrameCount,   Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .Tape           .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.TapeBound  .Tape           .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .Tape           .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .Tape           .ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeConfig     .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeConfig     .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeConfig     .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeConfig     .ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeActions    .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeActions    .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeActions    .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeActions    .ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound  .TapeAction     .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeAction     .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeAction     .ToWish().SamplingRate           );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeAction     .ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish().SamplingRate           );
            AreEqual(0,                   () => x.BuffBound  .Buff           .ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Bits     );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Channels );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWish(courtesyFrames).FrameCount, Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().Bits                   );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().Channels               );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().SamplingRate           );
            AreEqual(0,                   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount  , Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Bits     );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Channels );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).FrameCount, Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Bits);
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).FrameCount, Tolerance);
            AreEqual(test.Bits,           () => x.Independent.Sample         .ToWish().Bits                 );
            AreEqual(test.Channels,       () => x.Independent.Sample         .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => x.Independent.Sample         .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => x.Independent.Sample         .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => x.Independent.AudioFileInfo  .ToWish().Bits                 );
            AreEqual(test.Channels,       () => x.Independent.AudioFileInfo  .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo  .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo  .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => x.Immutable  .WavHeader      .ToWish().Bits                 );
            AreEqual(test.Channels,       () => x.Immutable  .WavHeader      .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => x.Immutable  .WavHeader      .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => x.Immutable  .WavHeader      .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => intTuple                     .ToWish().Bits                 );
            AreEqual(test.Channels,       () => intTuple                     .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => intTuple                     .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => intTuple                     .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => typeTuple                    .ToWish().Bits                 );
            AreEqual(test.Channels,       () => typeTuple                    .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => typeTuple                    .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => typeTuple                    .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => enumTuple                    .ToWish().Bits                 );
            AreEqual(test.Channels,       () => enumTuple                    .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => enumTuple                    .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => enumTuple                    .ToWish().FrameCount, Tolerance);
            AreEqual(test.Bits,           () => entityTuple                  .ToWish().Bits                 );
            AreEqual(test.Channels,       () => entityTuple                  .ToWish().Channels             );
            AreEqual(test.SamplingRate,   () => entityTuple                  .ToWish().SamplingRate         );
            AreEqual(test.FrameCount,     () => entityTuple                  .ToWish().FrameCount, Tolerance);
            
            if (test.Bits ==  8)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<byte> ().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<byte> ().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<byte> ().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<byte> ().FrameCount, Tolerance);
            }
            else if (test.Bits == 16)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<short>().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<short>().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<short>().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<short>().FrameCount, Tolerance);
            }
            else if (test.Bits == 32)
            {
                AreEqual(test.Bits,         () => typelessTuple.ToWish<float>().Bits                 );
                AreEqual(test.Channels,     () => typelessTuple.ToWish<float>().Channels             );
                AreEqual(test.SamplingRate, () => typelessTuple.ToWish<float>().SamplingRate         );
                AreEqual(test.FrameCount,   () => typelessTuple.ToWish<float>().FrameCount, Tolerance);
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
            SynthWishes synthWishes = null;
            IContext context = null;
            int courtesy = test.CourtesyFrames;

            void AssertProp(Action<TestEntities, AudioInfoWish> setter)
            {
                TestEntities  x = CreateEntities(test);
                synthWishes = x.SynthBound.SynthWishes;
                context     = x.SynthBound.Context;
                
                x.BuffBound.Buff.Bytes = null; // Unbuff it so FrameCounts can be set and not re-calculated from Buff.

                var infoWish = new AudioInfoWish
                {
                    Bits         = test.Bits,
                    Channels     = test.Channels,
                    SamplingRate = test.SamplingRate,
                    FrameCount   = test.FrameCount  
                };
                
                // TODO: Assert init values?
                
                setter(x, infoWish);
            }
            
            AssertProp((x, info) => { x.SynthBound .SynthWishes    .FromWish(info)                   ; Assert(x.SynthBound .SynthWishes,     test); });
            AssertProp((x, info) => { x.SynthBound .FlowNode       .FromWish(info)                   ; Assert(x.SynthBound .FlowNode,        test); });
            AssertProp((x, info) => { x.SynthBound .ConfigResolver .FromWish(info, synthWishes)      ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            AssertProp((x, info) => { x.TapeBound  .Tape           .FromWish(info)                   ; Assert(x.TapeBound  .Tape,            test); });
            AssertProp((x, info) => { x.TapeBound  .TapeConfig     .FromWish(info)                   ; Assert(x.TapeBound  .TapeConfig,      test); });
            AssertProp((x, info) => { x.TapeBound  .TapeActions    .FromWish(info)                   ; Assert(x.TapeBound  .TapeActions,     test); });
            AssertProp((x, info) => { x.TapeBound  .TapeAction     .FromWish(info)                   ; Assert(x.TapeBound  .TapeAction,      test); });
            AssertProp((x, info) => { x.BuffBound  .Buff           .FromWish(info, courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            AssertProp((x, info) => { x.BuffBound  .AudioFileOutput.FromWish(info, courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            AssertProp((x, info) => { x.Independent.Sample         .FromWish(info,           context); Assert(x.Independent.Sample,          test); });
            AssertProp((x, info) => { x.Independent.AudioFileInfo  .FromWish(info)                   ; Assert(x.Independent.AudioFileInfo,   test); });
            AssertProp((x, info) => { x.Independent.AudioInfoWish  .FromWish(info)                   ; Assert(x.Independent.AudioInfoWish,   test); });
            AssertProp((x, info) => { info.ApplyTo(x.SynthBound .SynthWishes)                        ; Assert(x.SynthBound .SynthWishes,     test); });
            AssertProp((x, info) => { info.ApplyTo(x.SynthBound .FlowNode)                           ; Assert(x.SynthBound .FlowNode,        test); });
            AssertProp((x, info) => { info.ApplyTo(x.SynthBound .ConfigResolver, synthWishes)        ; Assert(x.SynthBound .ConfigResolver,  test, synthWishes); });
            AssertProp((x, info) => { info.ApplyTo(x.TapeBound  .Tape)                               ; Assert(x.TapeBound  .Tape,            test); });
            AssertProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeConfig)                         ; Assert(x.TapeBound  .TapeConfig,      test); });
            AssertProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeActions)                        ; Assert(x.TapeBound  .TapeActions,     test); });
            AssertProp((x, info) => { info.ApplyTo(x.TapeBound  .TapeAction)                         ; Assert(x.TapeBound  .TapeAction,      test); });
            AssertProp((x, info) => { info.ApplyTo(x.BuffBound  .Buff,             courtesy, context); Assert(x.BuffBound  .Buff,            test); });
            AssertProp((x, info) => { info.ApplyTo(x.BuffBound  .AudioFileOutput,  courtesy, context); Assert(x.BuffBound  .AudioFileOutput, test); });
            AssertProp((x, info) => { info.ApplyTo(x.Independent.Sample,                     context); Assert(x.Independent.Sample,          test); });
            AssertProp((x, info) => { info.ApplyTo(x.Independent.AudioFileInfo)                      ; Assert(x.Independent.AudioFileInfo,   test); });
            AssertProp((x, info) => { info.ApplyTo(x.Independent.AudioInfoWish)                      ; Assert(x.Independent.AudioInfoWish,   test); });
        }

        private void Assert(SynthWishes entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits        );
            AreEqual(test.Channels,     () => entity.GetChannels    );
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.GetFrameCount());
        }

        private void Assert(FlowNode entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits        );
            AreEqual(test.Channels,     () => entity.GetChannels    );
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount()   );
        }

        private void Assert(ConfigResolverAccessor entity, Case test, SynthWishes synthWishes)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.GetBits        );
            AreEqual(test.Channels,     () => entity.GetChannels    );
            AreEqual(test.SamplingRate, () => entity.GetSamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount(synthWishes));
        }

        private void Assert(Tape entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), Tolerance);
        }

        private void Assert(TapeConfig entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits        );
            AreEqual(test.Channels,     () => entity.Channels    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount(), Tolerance);
        }

        private void Assert(TapeActions entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), Tolerance);
        }

        private void Assert(TapeAction entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(), Tolerance);
        }

        private void Assert(Buff entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            int courtesyFrames = test.CourtesyFrames;
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFrames), Tolerance);
        }

        private void Assert(AudioFileOutput entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            int courtesyFrames = test.CourtesyFrames;
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFrames), Tolerance);
        }
        
        private void Assert(Sample entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            // Sample ignores FrameCount changes—either its own value or 0.
            //AreEqual(test.FrameCount, () => entity.FrameCount(), Tolerance);
        }

        private void Assert(AudioFileInfo entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits()        );
            AreEqual(test.Channels,     () => entity.Channels()    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate());
            AreEqual(test.FrameCount,   () => entity.FrameCount()  );
        }

        private void Assert(AudioInfoWish entity, Case test)
        {
            IsNotNull(() => test);
            IsNotNull(() => entity);
            AreEqual(test.Bits,         () => entity.Bits        );
            AreEqual(test.Channels,     () => entity.Channels    );
            AreEqual(test.SamplingRate, () => entity.SamplingRate);
            AreEqual(test.FrameCount,   () => entity.FrameCount  );
        }

        
        [TestMethod]
        public void WavHeader_EdgeCases()
        {
            var test = new Case { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100 };
            var x = CreateEntities(test);
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;

            // Weird Buff case
                        
            // Buff's too Buff to budge: always returns fixed FrameCount instead of using parameterization.
            AreEqual(100, () => x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount, Tolerance);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(frameCount    ).FrameCount, Tolerance);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(123           ).FrameCount, Tolerance);
            
            // Unbuff the Buff; loosens him up and he'll budge.
            x.BuffBound.Buff.Bytes = null;
            
                                    AreEqual(100, () => x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount, Tolerance);
            ThrowsException(() => { AreEqual(100, () => x.BuffBound.Buff.ToWish(frameCount    ).FrameCount, Tolerance); });
            ThrowsException(() => { AreEqual(100, () => x.BuffBound.Buff.ToWish(123           ).FrameCount, Tolerance); });
        }
        
        // Helpers
        
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression) => AreEqual<int>(expected, actualExpression);
        private static void AreEqual(int expected, Expression<Func<int>> actualExpression, int delta) => AssertWishes.AreEqual(expected, actualExpression, delta);
        private static void AreEqual(int expected, int actual) => AreEqual<int>(expected, actual);
    }
}
