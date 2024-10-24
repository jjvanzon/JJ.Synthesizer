using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class CurveWishesTests : SynthWishes
    {
        [TestMethod]
        public void AsciiCurves_OneStringPerLine_WithRange()
        {
            var curve = CreateAsciiCurve_OneStringPerLine_WithRange();
            SaveAudioMono(() => curve, duration: 4);
        }
        
        [TestMethod]
        public void AsciiCurves_OneStringPerLine_WithoutRange()
        {
            var curve = CreateAsciiCurve_OneStringPerLine_WithoutRange();
            SaveAudioMono(() => curve, duration: 4);
        }

        [TestMethod]
        public void AsciiCurves_VerboseStrings()
        {
            var curve = CreateAsciiCurve_VerboseStrings();
            SaveAudioMono(() => curve, duration: 4);
        }

        Outlet CreateAsciiCurve_OneStringPerLine_WithoutRange() => Curve
        (
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o"
        );

        Outlet CreateAsciiCurve_OneStringPerLine_WithRange() => Curve
        (
            x: (start: 1, end: 3), y: (min: -1, max: 0.5),
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o"
        );

        Outlet CreateAsciiCurve_VerboseStrings() => Curve(
            x: (start: 1, end: 3), y: (min: -1, max: 0.5), @"

               o                 
             o   o                           
                                            
                       o         
            o                   o       

            ");

        /*
        CurveInWrapper AsciiCurve_WithArt => CurveFactory.CreateCurve(
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
