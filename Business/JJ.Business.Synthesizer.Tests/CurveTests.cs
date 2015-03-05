using JJ.Business.Synthesizer.Extensions;
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

                Curve curve = CreateCurve();
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

                Curve curve = CreateCurve();
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

                Curve curve = CreateCurve();
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

        //[TestMethod]
        //public void Test_Curve_MixedNodeTypes()
        //{
        //    var curve = new Curve() { Name = "Curve" };

        //    curve.Nodes.Add
        //    (
        //        new Node()
        //        {
        //            Time = 0,
        //            Value = 0.5,
        //            Type = NodeType.Off
        //        },
        //        new Node()
        //        {
        //            Time = 1,
        //            Value = 2,
        //            Type = NodeType.Block
        //        },
        //        new Node()
        //        {
        //            Time = 2,
        //            Value = 1,
        //            Type = NodeType.Line
        //        },
        //        new Node()
        //        {
        //            Time = 3,
        //            Value = 1,
        //            Type = NodeType.Off
        //        }
        //        // TODO: Test other node types too.
        //    );

        //    // Off
        //    Assert.AreEqual(0.0, () => curve.Value(0.0));
        //    Assert.AreEqual(0.0, () => curve.Value(0.5));
        //    // Block
        //    Assert.AreEqual(2.0, () => curve.Value(1.0));
        //    Assert.AreEqual(2.0, () => curve.Value(1.5));
        //    // Line
        //    Assert.AreEqual(1.0, () => curve.Value(2.0));
        //    Assert.AreEqual(0.5, () => curve.Value(2.5));
        //    // Off
        //    Assert.AreEqual(0.0, () => curve.Value(3.0));
        //}

        //[TestMethod]
        //public void Test_Curve_NullNode()
        //{
        //    Curve curve = CreateCurve();
        //    curve.Nodes.ForEach(x => x.Type = NodeType.Line);
        //    Assert.ThrowsException(() =>
        //        curve.Nodes[1] = null,
        //        "Node cannot be null.");
        //}

        // Helpers

        public static Curve CreateCurve()
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
