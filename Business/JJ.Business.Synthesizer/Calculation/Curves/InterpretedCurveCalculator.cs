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
using JJ.Framework.Common.Exceptions;
using JJ.Business.Synthesizer.Validation.Curves;

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
            IList<Node> sortedNodes = _curve.Nodes.OrderBy(n => n.X).ToArray();

            Node firstNode = sortedNodes[0];
            Node lastNode = sortedNodes.Last();

            var nodeMinus2 = new Node
            {
                X = CalculationHelper.VERY_LOW_VALUE,
                Y = firstNode.Y,
                NodeType = firstNode.NodeType
            };

            double xSpanBeforeFirstNode = firstNode.X - CalculationHelper.VERY_LOW_VALUE;
            var nodeMinus1 = new Node
            {
                X = CalculationHelper.VERY_LOW_VALUE + xSpanBeforeFirstNode / 2.0,
                Y = firstNode.Y,
                NodeType = firstNode.NodeType
            };

            double xSpanAfterLastNode = CalculationHelper.VERY_HIGH_VALUE - lastNode.X;
            var nodePlus1 = new Node
            {
                X = CalculationHelper.VERY_HIGH_VALUE - xSpanAfterLastNode / 2.0,
                Y = lastNode.Y,
                NodeType = lastNode.NodeType
            };

            var nodePlus2 = new Node
            {
                X = CalculationHelper.VERY_HIGH_VALUE,
                Y = lastNode.Y,
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

        public double CalculateY(double x)
        {
            // Find the node right after the time.
            Node node1 = null;
            int node1Index = 0;
            for (int i = 0; i < _sortedNodes.Count; i++)
            {
                node1 = _sortedNodes[i];

                if (node1.X > x)
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
                        double value = CalculateY_ForNodeTypeCurve(nodeMinus1, node0, node1, node2, x);
                        return value;
                    }

                case NodeTypeEnum.Line:
                    {
                        double value = CalculateY_ForNodeTypeLine(node0, node1, x);
                        return value;
                    }

                case NodeTypeEnum.Off:
                    return 0;

                case NodeTypeEnum.Block:
                    return node0.Y;

                default:
                    throw new ValueNotSupportedException(nodeTypeEnum);
            }
        }

        private static double CalculateY_ForNodeTypeCurve(Node nodeMinus1, Node node0, Node node1, Node node2, double time)
        {
            double y = Interpolator.Interpolate_Cubic_SmoothSlope(
                nodeMinus1.X, node0.X, node1.X, node2.X,
                nodeMinus1.Y, node0.Y, node1.Y, node2.Y,
                time);

            return y;
        }

        private static double CalculateY_ForNodeTypeLine(Node node0, Node node1, double x)
        {
            double dx = node1.X - node0.X;
            double dy = (node1.Y - node0.Y);

            double y = node0.Y + dy * (x - node0.X) / dx;

            return y;
        }
    }
}
