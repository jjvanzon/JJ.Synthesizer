using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class WavHeaderWishesTests
    {
        // TODO: CaseBase without MainProp to omit the <int> type argument?
        private class Case : CaseBase<int>
        {
            public CaseProp<int> SamplingRate   { get; set; }
            public CaseProp<int> Bits           { get; set; }
            public CaseProp<int> Channels       { get; set; }
            public CaseProp<int> CourtesyFrames { get; set; }
            public CaseProp<int> FrameCount     { get; set; }
        }
        
        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case>(new Case
        {
            SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100
        });

        [TestMethod]
        [DynamicData(nameof(Cases))]
        public void WavHeader_ToWish(string caseKey)
        { 
            Case test = Cases[caseKey];
            TestEntities x = CreateEntities(test);
            int frameCount = test.FrameCount;
            int courtesyFrames = test.CourtesyFrames;
            var synthWishes = x.SynthBound.SynthWishes;
            var intTuple    = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var typeTuple   = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var enumTuple   = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            var entityTuple = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);
            
            AreEqual(test.Bits,           () => x.SynthBound .SynthWishes    .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.SynthBound .SynthWishes    .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.SynthBound .SynthWishes    .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.SynthBound .SynthWishes    .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.SynthBound .FlowNode       .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.SynthBound .FlowNode       .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.SynthBound .FlowNode       .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.SynthBound .FlowNode       .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Bits                     );
            AreEqual(test.Channels,       () => x.SynthBound .ConfigResolver .ToWish(synthWishes).Channels                 );
            AreEqual(test.SamplingRate,   () => x.SynthBound .ConfigResolver .ToWish(synthWishes).SamplingRate             );
            AreEqual(test.FrameCount,     () => x.SynthBound .ConfigResolver .ToWish(synthWishes).FrameCount               );
            AreEqual(DefaultBits,         () => x.SynthBound .ConfigSection  .ToWish().Bits                                );
            AreEqual(DefaultChannels,     () => x.SynthBound .ConfigSection  .ToWish().Channels                            );
            AreEqual(DefaultSamplingRate, () => x.SynthBound .ConfigSection  .ToWish().SamplingRate                        );
            AreEqual(DefaultFrameCount,   () => x.SynthBound .ConfigSection  .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.TapeBound  .Tape           .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.TapeBound  .Tape           .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .Tape           .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.TapeBound  .Tape           .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.TapeBound  .TapeConfig     .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeConfig     .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeConfig     .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeConfig     .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.TapeBound  .TapeActions    .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeActions    .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeActions    .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeActions    .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.TapeBound  .TapeAction     .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.TapeBound  .TapeAction     .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.TapeBound  .TapeAction     .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.TapeBound  .TapeAction     .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish().SamplingRate                        );
            AreEqual(0,                   () => x.BuffBound  .Buff           .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound  .Buff           .ToWish(courtesyFrames).Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .Buff           .ToWish(courtesyFrames).SamplingRate          );
            AreEqual(test.FrameCount,     () => x.BuffBound  .Buff           .ToWish(courtesyFrames).FrameCount            );
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().SamplingRate                        );
            AreEqual(0,                   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).SamplingRate          );
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish(courtesyFrames).FrameCount            );
            AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Bits         );
            AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).Channels     );
            AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).SamplingRate );
            AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWish().FrameCount(frameCount).FrameCount   );
            AreEqual(test.Bits,           () => x.Independent.Sample         .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.Independent.Sample         .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.Independent.Sample         .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.Independent.Sample         .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.Independent.AudioFileInfo  .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.Independent.AudioFileInfo  .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo  .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo  .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => x.Immutable  .WavHeader      .ToWish().Bits                                );
            AreEqual(test.Channels,       () => x.Immutable  .WavHeader      .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => x.Immutable  .WavHeader      .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => x.Immutable  .WavHeader      .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => intTuple                     .ToWish().Bits                                );
            AreEqual(test.Channels,       () => intTuple                     .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => intTuple                     .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => intTuple                     .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => typeTuple                    .ToWish().Bits                                );
            AreEqual(test.Channels,       () => typeTuple                    .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => typeTuple                    .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => typeTuple                    .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => enumTuple                    .ToWish().Bits                                );
            AreEqual(test.Channels,       () => enumTuple                    .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => enumTuple                    .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => enumTuple                    .ToWish().FrameCount                          );
            AreEqual(test.Bits,           () => entityTuple                  .ToWish().Bits                                );
            AreEqual(test.Channels,       () => entityTuple                  .ToWish().Channels                            );
            AreEqual(test.SamplingRate,   () => entityTuple                  .ToWish().SamplingRate                        );
            AreEqual(test.FrameCount,     () => entityTuple                  .ToWish().FrameCount                          );
            
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
            AreEqual(100, () => x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(frameCount    ).FrameCount);
            AreEqual(100, () => x.BuffBound.Buff.ToWish(123           ).FrameCount);
            
            // Unbuff the Buff; loosens him up and he'll budge.
            x.BuffBound.Buff.Bytes = null;
            
                                    AreEqual(100, x.BuffBound.Buff.ToWish(courtesyFrames).FrameCount);
            ThrowsException(() => { AreEqual(100, x.BuffBound.Buff.ToWish(frameCount    ).FrameCount); });
            ThrowsException(() => { AreEqual(100, x.BuffBound.Buff.ToWish(123           ).FrameCount); });
        }

        public static void AreEqual(int expected, Expression<Func<int>> actualExpression) => AreEqual<int>(expected, actualExpression);
        public static void AreEqual(int expected, int actual) => AreEqual<int>(expected, actual);

        private TestEntities CreateEntities(Case testCase) 
            => new TestEntities(x => x.Bits(testCase.Bits)
                                      .Channels(testCase.Channels)
                                      .SamplingRate(testCase.SamplingRate)
                                      .CourtesyFrames(testCase.CourtesyFrames)
                                      .FrameCount(testCase.FrameCount));
    }
}
