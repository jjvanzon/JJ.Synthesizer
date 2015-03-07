using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Factories
{
    public class CurveFactory
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;
        private INodeTypeRepository _nodeTypeRepository;

        public CurveFactory(
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
            _nodeTypeRepository = nodeTypeRepository;
        }

        public Curve CreateCurve(params NodeInfo[] nodeInfos)
        {
            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            Curve curve = _curveRepository.Create();

            foreach (NodeInfo nodeInfo in nodeInfos)
            {
                Node node = _nodeRepository.Create();
                node.Time = nodeInfo.Time;
                node.Value = nodeInfo.Value;
                node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _nodeTypeRepository);
                node.LinkTo(curve);
            }

            return curve;
        }

        /// <param name="nodeInfos">When a NodeInfo is null, a node will not be created at that point in time.</param>
        public Curve CreateCurve(double timeSpan, params NodeInfo[] nodeInfos)
        {
            // TODO: I do not like this method signature looks in the method calls.

            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            Curve curve = _curveRepository.Create();

            double[] times = GetEquidistantPointsInTime(timeSpan, nodeInfos.Length);

            for (int i = 0; i < nodeInfos.Length; i++)
            {
                double time = times[i];
                NodeInfo nodeInfo = nodeInfos[i];

                if (nodeInfo != null)
                {
                    Node node = _nodeRepository.Create();
                    node.Time = time;
                    node.Value = nodeInfo.Value;
                    node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _nodeTypeRepository);
                    node.LinkTo(curve);
                }
            }

            return curve;
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public Curve CreateCurve(double timeSpan, params double?[] values)
        {
            if (timeSpan <= 0) throw new Exception("timespan must be greater than 0.");
            if (values.Length < 2) throw new Exception("values must contain at least 2 items.");

            Curve curve = _curveRepository.Create();

            double[] times = GetEquidistantPointsInTime(timeSpan, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                double? value = values[i];
                double time = times[i];

                if (value.HasValue)
                {
                    Node node = _nodeRepository.Create();
                    node.Time = time;
                    node.Value = value.Value;
                    node.SetNodeTypeEnum(NodeTypeEnum.Line, _nodeTypeRepository);
                    node.LinkTo(curve);
                }
            }

            return curve;
        }

        private double[] GetEquidistantPointsInTime(double timeSpan, int pointCount)
        {
            if (timeSpan <= 0) throw new Exception("timespan must be greater than 0.");
            if (pointCount < 2) throw new Exception("pointCount must be at least 2.");

            double[] times = new double[pointCount];
            double t = 0;
            double dt = timeSpan / (pointCount - 1);
            for (int i = 0; i < pointCount; i++)
            {
                times[i] = t;
                t += dt;
            }

            return times;
        }
    }
}
