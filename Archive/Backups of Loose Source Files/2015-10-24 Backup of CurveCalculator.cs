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

namespace JJ.Business.Synthesizer.Calculation
{
    public class CurveCalculator
    {
        private Curve _curve;

        internal CurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);
            _curve = curve;

            IValidator validator = new CurveValidator(_curve);
            validator.Verify();
        }

        public double CalculateValue(double time)
        {
            // TODO: This performs badly. Precalculate samples and do a simple linear interpolation here.
            // You may actually delegate to a sample calculator for that.

            IList<Node> sortedNodes = _curve.Nodes.OrderBy(x => x.Time).ToArray();

            // Find the node right after the time.
            int node1Index;
            Node node1 = null;
            Node previousNode = null;

            int i;
            for (i = 0; i < sortedNodes.Count; i++)
            {
                Node node = sortedNodes[i];

                if (node.Time > time)
                {
                    node1 = previousNode;
                    node1Index = i - 1;
                    break;
                }

                previousNode = node;
            }

            if (node1 == null) return 0.0;

            // TODO: Eventually you might create a fake set of nodes that include some dummy nodes at the beginning and end,
            // to have enough of them so that the indexers do not go wrong.

            // TODO: The fake time should be way more out there, for maximum 'stretch'.

            // TODO: The value should not go to 0. It should be set to the value to the right or to the left of it.

            int nodeMinus1Index = i - 2;
            Node nodeMinus1 = null;
            if (nodeMinus1Index >= 0)
            {
                nodeMinus1 = sortedNodes[nodeMinus1Index];
            }
            else
            {
                nodeMinus1 = new Node
                {
                    Time = node1.Time - 2
                };
            }

            int node0Index = i - 1;
            Node node0 = null;
            if (node0Index >= 0 && node0Index <= sortedNodes.Count - 1)
            {
                node0 = sortedNodes[node0Index];
            }
            else
            {
                node0 = new Node
                {
                    Time = node1.Time - 1
                };
            }

            int node2Index = i + 1;
            Node node2 = null;
            if (node2Index >= 0 && node2Index <= sortedNodes.Count - 1)
            {
                node2 = sortedNodes[node2Index];
            }
            else
            {
                node2 = new Node
                {
                    Time = node1.Time + 1
                };
            }

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
