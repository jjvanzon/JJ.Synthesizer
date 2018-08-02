using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Data;
using JJ.Framework.Testing;
using JJ.Framework.Testing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class CurveTests
    {
        [TestMethod]
        public void Test_Curve_Interpolation_Undefined()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        CurveRepositories repositories = PersistenceHelper.CreateCurveRepositories(context);
                        var curveFacade = new CurveFacade(repositories);

                        Curve curve = CreateTestCurve(curveFacade);
                        curve.Nodes.ForEach(x => x.SetInterpolationTypeEnum(InterpolationTypeEnum.Undefined, repositories.InterpolationTypeRepository));

                        ICalculatorWithPosition calculator = curveFacade.CreateInterpretedCalculator(curve);
                        AssertHelper.AreEqual(0, () => calculator.Calculate(0.0));
                        AssertHelper.AreEqual(0, () => calculator.Calculate(0.5));
                        AssertHelper.AreEqual(0, () => calculator.Calculate(1.0));
                        AssertHelper.AreEqual(0, () => calculator.Calculate(1.5));
                        AssertHelper.AreEqual(0, () => calculator.Calculate(2.0));
                    }
                });

        [TestMethod]
        public void Test_Curve_Block()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        CurveRepositories repositories = PersistenceHelper.CreateCurveRepositories(context);
                        var curveFacade = new CurveFacade(repositories);

                        Curve curve = CreateTestCurve(curveFacade);
                        curve.Nodes.ForEach(x => x.SetInterpolationTypeEnum(InterpolationTypeEnum.Block, repositories.InterpolationTypeRepository));

                        ICalculatorWithPosition calculator = curveFacade.CreateInterpretedCalculator(curve);
                        AssertHelper.AreEqual(1, () => calculator.Calculate(0.0));
                        AssertHelper.AreEqual(1, () => calculator.Calculate(0.5));
                        AssertHelper.AreEqual(2, () => calculator.Calculate(1.0));
                        AssertHelper.AreEqual(2, () => calculator.Calculate(1.5));
                        AssertHelper.AreEqual(0, () => calculator.Calculate(2.0));
                    }
                });

        [TestMethod]
        public void Test_Curve_Line()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        CurveRepositories repositories = PersistenceHelper.CreateCurveRepositories(context);
                        var curveFacade = new CurveFacade(repositories);

                        Curve curve = CreateTestCurve(curveFacade);
                        curve.Nodes.ForEach(x => x.SetInterpolationTypeEnum(InterpolationTypeEnum.Line, repositories.InterpolationTypeRepository));

                        ICalculatorWithPosition calculator = curveFacade.CreateInterpretedCalculator(curve);
                        AssertHelper.AreEqual(1.0, () => calculator.Calculate(0.0));
                        AssertHelper.AreEqual(1.5, () => calculator.Calculate(0.5));
                        AssertHelper.AreEqual(2.0, () => calculator.Calculate(1.0));
                        AssertHelper.AreEqual(1.0, () => calculator.Calculate(1.5));
                        AssertHelper.AreEqual(0.0, () => calculator.Calculate(2.0));
                    }
                });

        [TestMethod]
        public void Test_Curve_MixedNodeTypes()
            => AssertInconclusiveHelper.WithConnectionInconclusiveAssertion(
                () =>
                {
                    using (IContext context = PersistenceHelper.CreateContext())
                    {
                        CurveRepositories repositories = PersistenceHelper.CreateCurveRepositories(context);
                        var curveFacade = new CurveFacade(repositories);

                        Curve curve = curveFacade.Create(
                            3,
                            (0.5, InterpolationTypeEnum.Undefined),
                            (2.0, InterpolationTypeEnum.Block),
                            (1.0, InterpolationTypeEnum.Line),
                            (0.5, InterpolationTypeEnum.Undefined)
                        );

                        ICalculatorWithPosition calculator = curveFacade.CreateInterpretedCalculator(curve);

                        // Undefined
                        AssertHelper.AreEqual(0.00, () => calculator.Calculate(0.0));
                        AssertHelper.AreEqual(0.00, () => calculator.Calculate(0.5));
                        // Block
                        AssertHelper.AreEqual(2.00, () => calculator.Calculate(1.0));
                        AssertHelper.AreEqual(2.00, () => calculator.Calculate(1.5));
                        // Line
                        AssertHelper.AreEqual(1.00, () => calculator.Calculate(2.0));
                        // Behavior is different in JJ code base compared to the Circle code base. 
                        // In the Circle code base, an Off node means value = 0.
                        // In the JJ code base, a previous linear node will use the non-zero the value of the Undefined node.
                        AssertHelper.AreEqual(0.75, () => calculator.Calculate(2.5));
                        // Undefined
                        AssertHelper.AreEqual(0.00, () => calculator.Calculate(3.0));
                    }
                });

        // Helpers

        private static Curve CreateTestCurve(CurveFacade curveFacade)
        {
            Curve curve = curveFacade.Create(2, 1, 2, 0);
            curve.Name = "Curve";
            return curve;
        }
    }
}