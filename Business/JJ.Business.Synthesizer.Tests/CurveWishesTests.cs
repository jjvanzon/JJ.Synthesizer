using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    [TestCategory("Technical")]
    public class CurveWishesTests : SynthWishes
    {
        [TestMethod]
        public void CurveWishes_SynthesizerSugar_GetCurve() 
            => new CurveWishesTests().CurveWishes_SynthesizerSugar_GetCurve_RunTest();

        void CurveWishes_SynthesizerSugar_GetCurve_RunTest()
        {
            // Arrange
            CurveInWrapper curve1_cached = CurveIn("Curve1", (0, 1), (1, 0));
            CurveInWrapper curve2_cached = CurveIn("Curve2", (0, 0), (0.5, 1), (1, 0));

            // Act
            CurveInWrapper curve1_reused = GetCurve("Curve1");
            CurveInWrapper curve2_reused = GetCurve("Curve2");

            // Assert
            AssertHelper.AreEqual(curve1_cached, () => curve1_reused);
            AssertHelper.AreEqual(curve2_cached, () => curve2_reused);
            
            // Diagnostics
            SaveAudioMono(() => curve1_cached, fileName: Name() + "_Curve1.wav");
            SaveAudioMono(() => curve2_cached, fileName: Name() + "_Curve2.wav");
        }

        
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

        CurveInWrapper CreateAsciiCurve_OneStringPerLine_WithoutRange() => CurveIn
        (
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o"
        );

        CurveInWrapper CreateAsciiCurve_OneStringPerLine_WithRange() => CurveIn
        (
            x: (start: 1, end: 3), y: (min: -1, max: 0.5),
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o"
        );

        CurveInWrapper CreateAsciiCurve_VerboseStrings() => CurveIn(
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
