using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
#pragma warning disable CS0612 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class StringifyWishesTests : SynthWishes
    {
        [TestMethod]
        public void Test_Stringify_ShortNotation1()
        {
            FluentOutlet fluentOutlet = Sine(A4).Curve(0, 1, 0);

            string stringified = fluentOutlet.Stringify(true, true);
            
            AreEqual("Curve * Sine(1,440)", stringified);
        }
        
        [TestMethod]
        public void Test_Stringify_ShortNotation2()
        {
            Outlet outlet = Sine(A4).Curve(0, 1, 0).Volume(2);

            string stringified = outlet.Stringify(true, true);
            
            AreEqual("Curve * (Sine(1,440) * 2)", stringified);
        }
        
        [TestMethod]
        public void Test_Stringify_LongNotation1()
        {
            Operator op = Sine(A4).Curve(0, 1, 0).UnderlyingOperator;

            string actual = op.Stringify();

            string expected = "Multiply(" + NewLine +
                              "  Curve * " + NewLine +
                              "  Sine(1,440))";
            
            AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void Test_Stringify_LongNotation2()
        {
            var wrapper = new EntityWrappers.Sine(
                Sine(A4).Curve(0, 1, 0).Volume(2).UnderlyingOperator);

            string actual = _[wrapper].Stringify();

            // Order changed:
            // It's a commutative multiplication
            // and curves tend to be shifted upwards,
            // for higher chance early 0 discovery.

            string expected = "Multiply(" + NewLine +
                              "  Curve * " + NewLine +
                              "  Multiply(" + NewLine +
                              "    Sine(1,440) * 2))";
            
            AreEqual(expected, actual);
        }
    }
}
