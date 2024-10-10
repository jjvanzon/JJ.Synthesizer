using System.Security.Cryptography.X509Certificates;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_WishesTests : SynthSugarBase
    {
        [TestMethod]
        public void Test_Synthesizer_CurveWishes_AsciiCurves_OneStringPerLine()
        {
            CurveInWrapper curve = CreateAsciiCurve_OneStringPerLine();
            SaveWav(curve, duration: 4, volume: 1);
        }
        
        [TestMethod]
        public void Test_Synthesizer_CurveWishes_AsciiCurves_VerboseStrings()
        {
            CurveInWrapper curve = CreateAsciiCurve_VerboseStrings();
            SaveWav(curve, duration: 4, volume: 1);
        }

        private CurveInWrapper CreateAsciiCurve_OneStringPerLine() => CurveIn
        (
            start: 1, end: 3, min: -1, max: 0.5,
            lines: new[]
            {
                "   o                 ",
                " o   o               ",
                "                     ",
                "           o         ",
                "o                   o"
            }
        );

        private CurveInWrapper CreateAsciiCurve_VerboseStrings() => CurveIn(
            "CreateAsciiCurve_VerboseStrings",
            start: 1, end: 3, min: -1, max: 0.5, @"

               o                 
             o   o                           
                                            
                       o         
            o                   o       

            ");

        /*
        private CurveInWrapper AsciiCurve_WithArt => CurveFactory.CreateCurve(
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
