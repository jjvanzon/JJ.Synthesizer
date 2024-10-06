using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_WishesTests : SynthesizerSugarBase
    {
        [TestMethod]
        public void Test_Synthesizer_CurveWishes_AsciiCurves_OneStringPerLine()
        {
            Curve curve = CreateAsciiCurve_OneStringPerLine();
            Outlet outlet = CurveIn(curve);
            SaveWav(outlet, duration: 4, volume: 1);
        }
        
        [TestMethod]
        public void Test_Synthesizer_CurveWishes_AsciiCurves_VerboseStrings()
        {
            Curve curve = CreateAsciiCurve_VerboseStrings();
            Outlet outlet = CurveIn(curve);
            SaveWav(outlet, duration: 4, volume: 1);
        }

        private Curve CreateAsciiCurve_OneStringPerLine() => CurveFactory.CreateCurve(
            start: 1, end: 3, min: -1, max: 0.5,
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");

        private Curve CreateAsciiCurve_VerboseStrings() => CurveFactory.CreateCurve(
            start: 1, end: 3, min: -1, max: 0.5, @"

               o                 
             o   o                           
                                            
                       o         
            o                   o       

            ");

        /*
        private Curve AsciiCurve_WithArt => CurveFactory.CreateCurve(
            x:(1,3), y:(-1,0.5), @"

              ____________ DETUNICA VOLUME ____________
             |             o                           |
             |  o      o       o                       |
             |                                         |
             |      o                o                 |
             |o_______________________________________o|

        ");
        */
    }
}
