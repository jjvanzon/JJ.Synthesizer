using System.Collections.Generic;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Api
{
    public class CurveApi
    {
        private static readonly CurveManager _curveManager = CreateCurveManager();

        private static CurveManager CreateCurveManager()
        {
            return new CurveManager(RepositoryHelper.CurveRepositories);
        }

        public static Curve Create()
        {
            return _curveManager.Create();
        }

        public static Curve Create(Document document, bool mustGenerateName = false)
        {
            return _curveManager.Create(document, mustGenerateName);
        }

        public static Curve Create(params NodeInfo[] nodeInfos)
        {
            return _curveManager.Create(nodeInfos);
        }

        public static Curve Create(IList<NodeInfo> nodeInfos)
        {
            return _curveManager.Create(nodeInfos);
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public static Curve Create(double timeSpan, params double?[] values)
        {
            return _curveManager.Create(timeSpan, values);
        }

        /// <param name="nodeInfos">When a NodeInfo is null, a node will not be created at that point in time.</param>
        public static Curve Create(double timeSpan, params NodeInfo[] nodeInfos)
        {
            return _curveManager.Create(timeSpan, nodeInfos);
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public static Curve Create(Document document, double timeSpan, params double?[] values)
        {
            return _curveManager.Create(document, timeSpan, values);
        }

        public static Node CreateNode(Curve curve)
        {
            return _curveManager.CreateNode(curve);
        }

        public static void SetNodeType(Node node, NodeTypeEnum nodeTypeEnum)
        {
            if (node == null) throw new NullException(() => nodeTypeEnum);
            node.SetNodeTypeEnum(nodeTypeEnum, RepositoryHelper.CurveRepositories.NodeTypeRepository);
        }

        /// <summary> Faster initialization, slower calculation. </summary>
        public static ICalculatorWithPosition CreateInterpretedCalculator(Curve curve)
        {
            return _curveManager.CreateInterpretedCalculator(curve);
        }

        /// <summary> Slower initialization, faster calculation. </summary>
        public static ICalculatorWithPosition CreateOptimizedCalculator(Curve curve)
        {
            return _curveManager.CreateOptimizedCalculator(curve);
        }
    }
}