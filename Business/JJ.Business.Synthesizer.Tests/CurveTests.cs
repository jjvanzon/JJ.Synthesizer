using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Testing;
using JJ.Business.Synthesizer.Calculation;
using JJ.Framework.Validation;

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
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Off, nodeTypeRepository));

                var calculator = new CurveCalculator(curve);
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
            using (IContext context = PersistenceHelper.CreateContext())
            {
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Block, nodeTypeRepository));

                var calculator = new CurveCalculator(curve);
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
            using (IContext context = PersistenceHelper.CreateContext())
            {
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = CreateTestCurve();
                curve.Nodes.ForEach(x => x.SetNodeTypeEnum(NodeTypeEnum.Line, nodeTypeRepository));

                var calculator = new CurveCalculator(curve);
                AssertHelper.AreEqual(1.0, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(1.5, () => calculator.CalculateValue(0.5));
                AssertHelper.AreEqual(2.0, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(1.0, () => calculator.CalculateValue(1.5));
                AssertHelper.AreEqual(0.0, () => calculator.CalculateValue(2.0));
            }
        }

        // TODO: Port this code.

        [TestMethod]
        public void Test_Curve_MixedNodeTypes()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
                INodeRepository nodeRepository = PersistenceHelper.CreateRepository<INodeRepository>(context);
                INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);

                Curve curve = curveRepository.Create();
                curve.Name = "Curve";

                Node node1 = nodeRepository.Create();
                node1.Time = 0;
                node1.Value = 0.5;
                node1.SetNodeTypeEnum(NodeTypeEnum.Off, nodeTypeRepository);
                node1.LinkTo(curve);

                Node node2 = nodeRepository.Create();
                node2.Time = 1;
                node2.Value = 2;
                node2.SetNodeTypeEnum(NodeTypeEnum.Block, nodeTypeRepository);
                node2.LinkTo(curve);

                Node node3 = nodeRepository.Create();
                node3.Time = 2;
                node3.Value = 1;
                node3.SetNodeTypeEnum(NodeTypeEnum.Line, nodeTypeRepository);
                node3.LinkTo(curve);

                Node node4 = nodeRepository.Create();
                node4.Time = 3;
                // Behavior is different in JJ code base compared to the Circle code base. 
                // In the Circle code base, an Off node means value = 0.
                // In the JJ code base, a previous linear node will use the non-zero the value of the Off node.
                node4.Value = 0.5;
                node4.SetNodeTypeEnum(NodeTypeEnum.Off, nodeTypeRepository);
                node4.LinkTo(curve);

                var calculator = new CurveCalculator(curve);

                // Off
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(0.0));
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(0.5));
                // Block
                AssertHelper.AreEqual(2.00, () => calculator.CalculateValue(1.0));
                AssertHelper.AreEqual(2.00, () => calculator.CalculateValue(1.5));
                // Line
                AssertHelper.AreEqual(1.00, () => calculator.CalculateValue(2.0));
                // Behavior is different in JJ code base compared to the Circle code base. 
                // See earilier comment.
                AssertHelper.AreEqual(0.75, () => calculator.CalculateValue(2.5));
                // Off
                AssertHelper.AreEqual(0.00, () => calculator.CalculateValue(3.0));
            }
        }

        // Helpers

        public static Curve CreateTestCurve()
        {
            var curve = new Curve() { Name = "Curve" };

            curve.Nodes.Add(
                new Node() { Time = 0, Value = 1 },
                new Node() { Time = 1, Value = 2 },
                new Node() { Time = 2, Value = 0 });

            return curve;
        }
    }
}
