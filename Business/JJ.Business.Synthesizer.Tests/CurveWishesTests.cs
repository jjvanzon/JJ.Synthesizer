﻿using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class CurveWishesTests : SynthSugarBase
    {
        [TestMethod]
        public void AsciiCurves_OneStringPerLine_WithRange()
        {
            CurveInWrapper curve = CreateAsciiCurve_OneStringPerLine_WithRange();
            SaveWav(curve, duration: 4, volume: 1);
        }
        [TestMethod]
        public void AsciiCurves_OneStringPerLine_WithoutRange()
        {
            CurveInWrapper curve = CreateAsciiCurve_OneStringPerLine_WithoutRange();
            SaveWav(curve, duration: 4, volume: 1);
        }

        [TestMethod]
        public void AsciiCurves_VerboseStrings()
        {
            CurveInWrapper curve = CreateAsciiCurve_VerboseStrings();
            SaveWav(curve, duration: 4, volume: 1);
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