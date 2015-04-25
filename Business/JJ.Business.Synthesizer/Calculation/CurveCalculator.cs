using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
