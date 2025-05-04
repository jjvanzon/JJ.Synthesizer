using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class CurveWishesTests : SynthWishes
    {
        [TestMethod]
        public void AsciiCurves_WithoutRange_Test() => Run(AsciiCurves_WithoutRange); 
        void AsciiCurves_WithoutRange()
        {
            var curve = CreateAsciiCurve_WithoutRange();
            WithMono().WithAudioLength(4).Save(curve);
        }

        [TestMethod]
        public void AsciiCurves_WithRange_Test() => Run(AsciiCurves_WithRange); 
        void AsciiCurves_WithRange()
        {
            var curve = CreateAsciiCurve_WithRange();
            WithMono().WithAudioLength(4).Save(curve);
        }

        FlowNode CreateAsciiCurve_WithoutRange() => Curve(@"
               o                 
             o   o               
                                 
                       o         
            o                   o").SetName();

        FlowNode CreateAsciiCurve_WithRange() => Curve(
            x: (start: 1, end: 3), y: (min: -1, max: 0.5), @"

               o                 
             o   o                           
                                            
                       o         
            o                   o

            ").SetName();

        /*
        FlowNode AsciiCurve_WithArt => CurveFactory.CreateCurve(
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
