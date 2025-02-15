using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

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
            Case testCase = Cases[caseKey];
            var x = CreateEntities(testCase);
            
            //WavHeaderWishes.
            
            AudioInfoWish wish = x.SynthBound.SynthWishes.ToWish();
            Assert(testCase, wish);
        }

        private void Assert(Case testCase, AudioInfoWish wish)
        {
            IsNotNull(() => testCase);
            IsNotNull(() => wish);
            
            int samplingRate = testCase.SamplingRate;
            int frameCount   = testCase.FrameCount;
            int bits         = testCase.Bits;
            int channels     = testCase.Channels;
            
            AreEqual(samplingRate, () => wish.SamplingRate);
            AreEqual(frameCount,   () => wish.FrameCount);
            AreEqual(bits,         () => wish.Bits);
            AreEqual(channels,     () => wish.Channels);
        }

        private TestEntities CreateEntities(Case testCase) 
            => new TestEntities(x => x.Bits(testCase.Bits)
                                      .Channels(testCase.Channels)
                                      .SamplingRate(testCase.SamplingRate)
                                      .CourtesyFrames(testCase.CourtesyFrames)
                                      .FrameCount(testCase.FrameCount));
    }
}
