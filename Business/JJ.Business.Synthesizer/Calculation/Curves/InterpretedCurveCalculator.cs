using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Linq;
using System;
using JJ.Framework.Mathematics;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class InterpretedCurveCalculator : ICurveCalculator
    {
        private readonly Curve _curve;

        /// <summary>
        /// Contains the nodes of the curve and also additional nodes before and after,
        /// to accomodate interpolation that requires 4 points to calculate a value.
        /// </summary>
        private readonly IList<Node> _sortedNodes;

        public InterpretedCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);
            _curve = curve;

            IValidator validator1 = new CurveValidator_WithoutNodes(_curve);
            validator1.Assert();

            IValidator validator2 = new CurveValidator_Nodes(_curve);
            validator2.Assert();

            _sortedNodes = CreateSortedNodes(_curve);
        }

        private IList<Node> CreateSortedNodes(Curve curve)
        {
            IList<Node> sortedNodes = _curve.Nodes.OrderBy(x => x.Time).ToArray();

            Node firstNode = sortedNodes[0];
            Node lastNode = sortedNodes.Last();

            var nodeMinus2 = new Node
            {
                Time = Double.MinValue,
                Value = firstNode.Value,
                NodeType = firstNode.NodeType
            };

            double timeBeforeFirstNode = firstNode.Time - Double.MinValue;
            var nodeMinus1 = new Node
            {
                Time = Double.MinValue + timeBeforeFirstNode / 2.0,
                Value = firstNode.Value,
                NodeType = firstNode.NodeType
            };

            double timeAfterLastNode = Double.MaxValue - lastNode.Time;
            var nodePlus1 = new Node
            {
                Time = Double.MaxValue - timeAfterLastNode / 2.0,
                Value = lastNode.Value,
                NodeType = lastNode.NodeType
            };

            var nodePlus2 = new Node
            {
                Time = Double.MaxValue,
                Value = lastNode.Value,
                NodeType = lastNode.NodeType
            };

            List<Node> sortedNodesIncludingFakeNodes = new List<Node>(curve.Nodes.Count + 4);
            sortedNodesIncludingFakeNodes.Add(nodeMinus2);
            sortedNodesIncludingFakeNodes.Add(nodeMinus1);
            sortedNodesIncludingFakeNodes.AddRange(sortedNodes);
            sortedNodesIncludingFakeNodes.Add(nodePlus1);
            sortedNodesIncludingFakeNodes.Add(nodePlus2);
            return sortedNodesIncludingFakeNodes;
        }

        public double CalculateValue(double time)
        {
            // Find the node right after the time.
            Node node1 = null;
            int node1Index = 0;
            for (int i = 0; i < _sortedNodes.Count; i++)
            {
                node1 = _sortedNodes[i];

                if (node1.Time > time)
                {
                    node1Index = i;
                    break;
                }
            }

            Node nodeMinus1 = _sortedNodes[node1Index - 2];
            Node node0 = _sortedNodes[node1Index - 1];
            Node node2 = _sortedNodes[node1Index + 1];

            // Calculate the Value
            NodeTypeEnum nodeTypeEnum = node0.GetNodeTypeEnum();
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Curve:
                    {
                        double value = CalculateValue_ForNodeTypeCurve(nodeMinus1, node0, node1, node2, time);
                        return value;
                    }

                case NodeTypeEnum.Line:
                    {
                        double value = CalculateValue_ForNodeTypeLine(node0, node1, time);
                        return value;
                    }

                case NodeTypeEnum.Off:
                    return 0;

                case NodeTypeEnum.Block:
                    return node0.Value;

                default:
                    throw new ValueNotSupportedException(nodeTypeEnum);
            }
        }

        private static double CalculateValue_ForNodeTypeCurve(Node nodeMinus1, Node node0, Node node1, Node node2, double time)
        {
            double value = Interpolator.Interpolate_Cubic_Ramses(
                nodeMinus1.Time, node0.Time, node1.Time, node2.Time,
                nodeMinus1.Value, node0.Value, node1.Value, node2.Value,
                time);

            return value;
        }

        private static double CalculateValue_ForNodeTypeLine(Node node0, Node node1, double time)
        {
            double dt = node1.Time - node0.Time;
            double dx = (node1.Value - node0.Value);

            double value = node0.Value + dx * (time - node0.Time) / dt;

            return value;
        }
    }
}
