using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Testing;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Calculation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

namespace JJ.Business.Synthesizer.Tests
{
	[TestClass]
	public class CurveTests
	{
		[TestMethod]
		public void Test_Curve_Off()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateCurveRepositories(context);
				var curveManager = new CurveManager(repositories);

				Curve curve = CreateTestCurve(curveManager);
				curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Off, repositories.NodeTypeRepository));

				ICalculatorWithPosition calculator = curveManager.CreateInterpretedCalculator(curve);
				AssertHelper.AreEqual(0, () => calculator.Calculate(0.0));
				AssertHelper.AreEqual(0, () => calculator.Calculate(0.5));
				AssertHelper.AreEqual(0, () => calculator.Calculate(1.0));
				AssertHelper.AreEqual(0, () => calculator.Calculate(1.5));
				AssertHelper.AreEqual(0, () => calculator.Calculate(2.0));
			}
		}

		[TestMethod]
		public void Test_Curve_Block()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateCurveRepositories(context);
				var curveManager = new CurveManager(repositories);

				Curve curve = CreateTestCurve(curveManager);
				curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Block, repositories.NodeTypeRepository));

				ICalculatorWithPosition calculator = curveManager.CreateInterpretedCalculator(curve);
				AssertHelper.AreEqual(1, () => calculator.Calculate(0.0));
				AssertHelper.AreEqual(1, () => calculator.Calculate(0.5));
				AssertHelper.AreEqual(2, () => calculator.Calculate(1.0));
				AssertHelper.AreEqual(2, () => calculator.Calculate(1.5));
				AssertHelper.AreEqual(0, () => calculator.Calculate(2.0));
			}
		}

		[TestMethod]
		public void Test_Curve_Line()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateCurveRepositories(context);
				var curveManager = new CurveManager(repositories);

				Curve curve = CreateTestCurve(curveManager);
				curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Line, repositories.NodeTypeRepository));

				ICalculatorWithPosition calculator = curveManager.CreateInterpretedCalculator(curve);
				AssertHelper.AreEqual(1.0, () => calculator.Calculate(0.0));
				AssertHelper.AreEqual(1.5, () => calculator.Calculate(0.5));
				AssertHelper.AreEqual(2.0, () => calculator.Calculate(1.0));
				AssertHelper.AreEqual(1.0, () => calculator.Calculate(1.5));
				AssertHelper.AreEqual(0.0, () => calculator.Calculate(2.0));
			}
		}

		[TestMethod]
		public void Test_Curve_MixedNodeTypes()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var repositories = PersistenceHelper.CreateCurveRepositories(context);
				var curveManager = new CurveManager(repositories);
				Curve curve = curveManager.Create
				(
					3,
					new NodeInfo(0.5, NodeTypeEnum.Off),
					new NodeInfo(2.0, NodeTypeEnum.Block),
					new NodeInfo(1.0, NodeTypeEnum.Line),
					new NodeInfo(0.5, NodeTypeEnum.Off)
				);

				ICalculatorWithPosition calculator = curveManager.CreateInterpretedCalculator(curve);

				// Off
				AssertHelper.AreEqual(0.00, () => calculator.Calculate(0.0));
				AssertHelper.AreEqual(0.00, () => calculator.Calculate(0.5));
				// Block
				AssertHelper.AreEqual(2.00, () => calculator.Calculate(1.0));
				AssertHelper.AreEqual(2.00, () => calculator.Calculate(1.5));
				// Line
				AssertHelper.AreEqual(1.00, () => calculator.Calculate(2.0));
				// Behavior is different in JJ code base compared to the Circle code base. 
				// In the Circle code base, an Off node means value = 0.
				// In the JJ code base, a previous linear node will use the non-zero the value of the Off node.
				AssertHelper.AreEqual(0.75, () => calculator.Calculate(2.5));
				// Off
				AssertHelper.AreEqual(0.00, () => calculator.Calculate(3.0));
			}
		}

		// Helpers

		public static Curve CreateTestCurve(CurveManager curveManager)
		{
			Curve curve = curveManager.Create(2, 1, 2, 0);
			curve.Name = "Curve";
			return curve;
		}
	}
}
