using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;
using static JJ.Framework.Testing.Core.AssertHelperCore;


#pragma warning disable CS0612

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class StringifyWishesTests : SynthWishes
    {
        [TestMethod]
        public void Test_Stringify_ShortNotation1()
        {
            WithMathBoost(false);

            FlowNode flowNode = Sine(A4).Curve(0, 1, 0);

            string stringified = flowNode.Stringify(true, true);
            
            AreEqual("Sine(1,440) * Curve In", stringified);
        }
        
        [TestMethod]
        public void Test_Stringify_ShortNotation2()
        {
            WithMathBoost();

            Outlet outlet = Sine(A4).Curve(0, 1, 0).Volume(2);

            string stringified = outlet.Stringify(true, true);
            
            AreEqual("Curve In * (Sine(1,440) * 2)", stringified);
        }
        
        [TestMethod]
        public void Test_Stringify_LongNotation1()
        {
            WithMathBoost(true);
            
            Operator op = Sine(A4).Curve(0, 1, 0).UnderlyingOperator;

            string actual = op.Stringify();
            
            // Order changed:
            // It's a commutative multiplication
            // and curves tend to be shifted upwards,
            // for higher chance early 0 discovery.

            string expected = "Multiply(" + NewLine +
                              "  Curve In * " + NewLine +
                              "  Sine(1,440))";
            
            AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void Test_Stringify_LongNotation2()
        {
            WithMathBoost(false);

            var wrapper = new EntityWrappers.Sine(
                Sine(A4).Curve(0, 1, 0).Volume(2).UnderlyingOperator);

            string actual = _[wrapper].Stringify();

            string expected = "Multiply(" + NewLine +
                              "  Multiply(" + NewLine + 
                              "    Sine(1,440) * " + NewLine +
                              "    Curve In) * 2)";
            
            AreEqual(expected, actual);
        }
    }
}
