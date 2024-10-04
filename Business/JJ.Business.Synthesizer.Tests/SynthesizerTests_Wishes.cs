using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests_Wishes : SynthesizerSugarBase
    {
        [TestMethod]
        public void Test_Synthesizer_CurveWishes_AsciiCurves()
        {
            Curve curve = AsciiCurve;
            Outlet outlet = CurveIn(curve);
            WriteToAudioFile(outlet, duration: 4, volume: 1);
        }
        
        private Curve AsciiCurve => CurveFactory.CreateCurve(
            start: 1, end: 3, min: -1, max: 0.5,
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");
    }
}
