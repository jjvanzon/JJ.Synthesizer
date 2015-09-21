using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using System;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Managers
{
    public class CurveManager
    {
        private CurveRepositories _repositories;

        public CurveManager(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public VoidResult DeleteWithRelatedEntities(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);
            return DeleteWithRelatedEntities(curve);
        }

        public VoidResult DeleteWithRelatedEntities(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            IValidator validator = new CurveValidator_Delete(curve, _repositories.CurveRepository);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }
            else
            {
                curve.UnlinkRelatedEntities();
                curve.DeleteRelatedEntities(_repositories.NodeRepository);
                _repositories.CurveRepository.Delete(curve);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        // TODO: These Create overloads seem inconsistent.

        public Curve Create(Document document, bool mustGenerateName)
        {
            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);
            curve.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Curve_SideEffect_GenerateName(curve);
                sideEffect.Execute();
            }

            return curve;
        }

        public Curve Create(params NodeInfo[] nodeInfos)
        {
            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);

            foreach (NodeInfo nodeInfo in nodeInfos)
            {
                var node = new Node();
                node.ID = _repositories.IDRepository.GetID();
                node.Time = nodeInfo.Time;
                node.Value = nodeInfo.Value;
                node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _repositories.NodeTypeRepository);
                node.LinkTo(curve);
                _repositories.NodeRepository.Insert(node);
            }

            return curve;
        }

        /// <param name="nodeInfos">When a NodeInfo is null, a node will not be created at that point in time.</param>
        public Curve Create(double timeSpan, params NodeInfo[] nodeInfos)
        {
            // TODO: I do not like this method signature looks in the method calls.

            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);

            double[] times = GetEquidistantPointsInTime(timeSpan, nodeInfos.Length);

            for (int i = 0; i < nodeInfos.Length; i++)
            {
                double time = times[i];
                NodeInfo nodeInfo = nodeInfos[i];

                if (nodeInfo != null)
                {
                    var node = new Node();
                    node.ID = _repositories.IDRepository.GetID();
                    node.Time = time;
                    node.Value = nodeInfo.Value;
                    node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _repositories.NodeTypeRepository);
                    node.LinkTo(curve);
                    _repositories.NodeRepository.Insert(node);
                }
            }

            return curve;
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public Curve Create(double timeSpan, params double?[] values)
        {
            if (timeSpan <= 0) throw new LessThanOrEqualException(() => timeSpan, 0);
            if (values == null) throw new NullException(() => values);
            if (values.Length < 2) throw new LessThanException(() => values.Length, 2);

            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);

            double[] times = GetEquidistantPointsInTime(timeSpan, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                double? value = values[i];
                double time = times[i];

                if (value.HasValue)
                {
                    var node = new Node();
                    node.ID = _repositories.IDRepository.GetID();
                    node.Time = time;
                    node.Value = value.Value;
                    node.SetNodeTypeEnum(NodeTypeEnum.Line, _repositories.NodeTypeRepository);
                    node.LinkTo(curve);
                    _repositories.NodeRepository.Insert(node);
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

        public void DeleteNode(int nodeID)
        {
            Node node = _repositories.NodeRepository.Get(nodeID);
            DeleteNode(node);
        }

        public void DeleteNode(Node node)
        {
            if (node == null) throw new NullException(() => node);
            node.UnlinkRelatedEntities();
            _repositories.NodeRepository.Delete(node);
        }
    }
}
