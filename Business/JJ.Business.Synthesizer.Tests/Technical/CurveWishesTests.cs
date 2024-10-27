using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class CurveWishesTests : SynthWishes
    {
        [TestMethod]
        public void AsciiCurves_WithoutRange()
        {
            var curve = CreateAsciiCurve_WithoutRange();
            Mono().WithDuration(4).SaveAudio(() => curve);
        }

        [TestMethod]
        public void AsciiCurves_WithRange()
        {
            var curve = CreateAsciiCurve_WithRange();
            Mono().WithDuration(4).SaveAudio(() => curve);
        }

        Outlet CreateAsciiCurve_WithoutRange() => WithName().Curve(@"
               o                 
             o   o               
                                 
                       o         
            o                   o");

        Outlet CreateAsciiCurve_WithRange() => WithName().Curve(
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
