using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation
{
    public class CurveCalculator
    {
        private Curve _curve;

        public CurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);
            _curve = curve;

            IValidator validator = new CurveValidator(_curve);
            validator.Verify();
        }

        public double CalculateValue(double time)
        {
            Node nodeA = null;
            Node nodeB = null;

            // TODO: Performance for this might be kind of crappy.
            // You might want to precalculate samples and do a simple linear interpolation here.
            // You may actually delegate to a sample calculator for that.

            // Find the nodes the time is in between
            foreach (var node in _curve.Nodes)
            {
                if (node.Time > time)
                {
                    nodeB = node;
                    break;
                }
                nodeA = node;
            }

            if (nodeA == null || nodeB == null) return 0;

            // Calculate the Value
            NodeTypeEnum nodeTypeEnum = nodeA.GetNodeTypeEnum();
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Line:
                    if (nodeB.Time <= nodeA.Time) return nodeA.Value; // If time accidently reversed...
                    double dt = nodeB.Time - nodeA.Time;
                    double dx = (nodeB.Value - nodeA.Value);
                    return nodeA.Value + dx * (time - nodeA.Time) / dt;

                case NodeTypeEnum.Off:
                    return 0;

                case NodeTypeEnum.Block:
                    return nodeA.Value;

                default:
                    throw new ValueNotSupportedException(nodeTypeEnum);
            }
        }
    }
}
