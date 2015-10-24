using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Data;
using JJ.Data.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Testing;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Configuration;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Api;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class CurveTests
    {
        static CurveTests()
        {
            TestHelper.SetConfigurationSections();
        }

        [TestMethod]
        public void Test_Curve_Off()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Off, nodeTypeRepository));

                CurveCalculator calculator = CurveApi.CreateCalculator(curve);
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(0.5));
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(1.5));
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(2.0));
            }
        }

        [TestMethod]
        public void Test_Curve_Block()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Block, nodeTypeRepository));

                CurveCalculator calculator = CurveApi.CreateCalculator(curve);
                AssertHelper.AreEqual(1, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(1, () => calculator.CalculateValue(0.5));
                AssertHelper.AreEqual(2, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(2, () => calculator.CalculateValue(1.5));
                AssertHelper.AreEqual(0, () => calculator.CalculateValue(2.0));
            }
        }

        [TestMethod]
        public void Test_Curve_Line()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Line, nodeTypeRepository));

                CurveCalculator calculator = CurveApi.CreateCalculator(curve);
                AssertHelper.AreEqual(1.0, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(1.5, () => calculator.CalculateValue(0.5));
                AssertHelper.AreEqual(2.0, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(1.0, () => calculator.CalculateValue(1.5));
                AssertHelper.AreEqual(0.0, () => calculator.CalculateValue(2.0));
            }
        }

        [TestMethod]
        public void Test_Curve_MixedNodeTypes()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                var curveFactory = new CurveManager(new CurveRepositories(repositories));
                Curve curve = curveFactory.Create
                (
                    3,
                    new NodeInfo(0.5, NodeTypeEnum.Off),
                    new NodeInfo(2.0, NodeTypeEnum.Block),
                    new NodeInfo(1.0, NodeTypeEnum.Line),
                    new NodeInfo(0.5, NodeTypeEnum.Off)
                );

                CurveCalculator calculator = CurveApi.CreateCalculator(curve);

                // Off
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(0.5));
                // Block
                AssertHelper.AreEqual(2.00, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(2.00, () => calculator.CalculateValue(1.5));
                // Line
                AssertHelper.AreEqual(1.00, () => calculator.CalculateValue(2.0));
                // Behavior is different in JJ code base compared to the Circle code base. 
                // In the Circle code base, an Off node means value = 0.
                // In the JJ code base, a previous linear node will use the non-zero the value of the Off node.
                AssertHelper.AreEqual(0.75, () => calculator.CalculateValue(2.5));
                // Off
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(3.0));
            }
        }

        // Helpers

        public static Curve CreateTestCurve()
        {
            Curve curve = CurveApi.Create(2, 1, 2, 0);
            curve.Name = "Curve";
            return curve;
        }
    }
}
