using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.Validation;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Calculation
{
    internal class InterpretedCurveCalculator : ICalculatorWithPosition
    {
        private readonly Curve _curve;

        /// <summary>
        /// Contains the nodes of the curve and also additional nodes before and after,
        /// to accomodate interpolation that requires 4 points to calculate a value.
        /// </summary>
        private readonly IList<Node> _sortedNodes;

        public InterpretedCurveCalculator(Curve curve)
        {
            _curve = curve ?? throw new NullException(() => curve);

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
                InterpolationType = firstNode.InterpolationType
            };

            double xSpanBeforeFirstNode = firstNode.X - CalculationHelper.VERY_LOW_VALUE;

            var nodeMinus1 = new Node
            {
                X = CalculationHelper.VERY_LOW_VALUE + xSpanBeforeFirstNode / 2.0,
                Y = firstNode.Y,
                InterpolationType = firstNode.InterpolationType
            };

            double xSpanAfterLastNode = CalculationHelper.VERY_HIGH_VALUE - lastNode.X;

            var nodePlus1 = new Node
            {
                X = CalculationHelper.VERY_HIGH_VALUE - xSpanAfterLastNode / 2.0,
                Y = lastNode.Y,
                InterpolationType = lastNode.InterpolationType
            };

            var nodePlus2 = new Node
            {
                X = CalculationHelper.VERY_HIGH_VALUE,
                Y = lastNode.Y,
                InterpolationType = lastNode.InterpolationType
            };

            // ReSharper disable once UseObjectOrCollectionInitializer
            var sortedNodesIncludingFakeNodes = new List<Node>(curve.Nodes.Count + 4);
            sortedNodesIncludingFakeNodes.Add(nodeMinus2);
            sortedNodesIncludingFakeNodes.Add(nodeMinus1);
            sortedNodesIncludingFakeNodes.AddRange(sortedNodes);
            sortedNodesIncludingFakeNodes.Add(nodePlus1);
            sortedNodesIncludingFakeNodes.Add(nodePlus2);
            return sortedNodesIncludingFakeNodes;
        }

        public double Calculate(double x)
        {
            // Find the node right after the time.
            Node node1 = null;
            var node1Index = 0;
            int count = _sortedNodes.Count;

            for (var i = 0; i < count; i++)
            {
                node1 = _sortedNodes[i];

                // ReSharper disable once InvertIf
                if (node1.X > x)
                {
                    node1Index = i;
                    break;
                }
            }

            Node nodeMinus1 = _sortedNodes[node1Index - 2];
            Node node0 = _sortedNodes[node1Index - 1];
            Node node2 = _sortedNodes[node1Index + 1];

            // Stripe Interpolation has effect 'earlier' than the others.
            if (node1.GetInterpolationTypeEnum() == InterpolationTypeEnum.Stripe)
            {
                // ReSharper disable once PossibleNullReferenceException
                double delta = node1.X - node0.X;
                double halfWay = node0.X + delta / 2.0;

                if (x > halfWay)
                {
                    return node1.Y;
                }
            }

            // Calculate the Value
            InterpolationTypeEnum interpolationTypeEnum = node0.GetInterpolationTypeEnum();

            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Cubic:
                {
                    double value = CalculateY_ForInterpolationTypeCubic(nodeMinus1, node0, node1, node2, x);
                    return value;
                }

                case InterpolationTypeEnum.Hermite:
                {
                    double value = CalculateY_ForInterpolationTypeHermite(nodeMinus1, node0, node1, node2, x);
                    return value;
                }

                case InterpolationTypeEnum.Line:
                {
                    double value = CalculateY_ForInterpolationTypeLine(node0, node1, x);
                    return value;
                }

                case InterpolationTypeEnum.Block:
                case InterpolationTypeEnum.Stripe:
                    return node0.Y;

                case InterpolationTypeEnum.Undefined:
                    return 0;

                default:
                    throw new ValueNotSupportedException(interpolationTypeEnum);
            }
        }

        private static double CalculateY_ForInterpolationTypeCubic(Node nodeMinus1, Node node0, Node node1, Node node2, double x)
        {
            double y = Interpolator.Cubic_SmoothSlope(
                nodeMinus1.X,
                node0.X,
                node1.X,
                node2.X,
                nodeMinus1.Y,
                node0.Y,
                node1.Y,
                node2.Y,
                x);

            return y;
        }

        private double CalculateY_ForInterpolationTypeHermite(Node nodeMinus1, Node node0, Node node1, Node node2, double x)
        {
            double dx = node1.X - node0.X;
            double t = (x - node0.X) / dx;

            double y = Interpolator.Hermite_4pt3oX(nodeMinus1.Y, node0.Y, node1.Y, node2.Y, t);
            return y;
        }

        private static double CalculateY_ForInterpolationTypeLine(Node node0, Node node1, double x)
        {
            double dx = node1.X - node0.X;
            double dy = node1.Y - node0.Y;

            double y = node0.Y + dy * (x - node0.X) / dx;

            return y;
        }
    }
}