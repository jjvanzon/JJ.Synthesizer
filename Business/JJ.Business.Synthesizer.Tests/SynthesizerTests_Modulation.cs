using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests_Modulation : SynthesizerSugarBase
    {
        /// <summary> Constructor for test runner. </summary>
        [UsedImplicitly]
        public SynthesizerTests_Modulation()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public SynthesizerTests_Modulation(IContext context)
            : base(context, beat: 0.4, bar: 4 * 0.4)
        { }
        
        [TestMethod]
        public void Test_Synthesizer_Modulations_DrawnOutCurve()
        {
            Curve curve = DrawnOutCurve;
            Outlet outlet = CurveIn(curve);
            WriteToAudioFile(outlet, 1.0, 1.0);
        }
        
        private Curve DrawnOutCurve => CurveFactory.CreateCurve(
            "   x                 ",
            " x   x               ",
            "                     ",
            "           x         ",
            "x                   x");
    }
}