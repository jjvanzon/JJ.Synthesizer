using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using System;
using JJ.Framework.Business;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using System.Collections.Generic;
using JJ.Framework.Validation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Calculation;

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

        public Curve Create()
        {
            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);

            ISideEffect sideEffect1 = new Curve_SideEffect_SetDefaults(
                curve, _repositories.NodeRepository, _repositories.NodeTypeRepository, _repositories.IDRepository);
            sideEffect1.Execute();

            return curve;
        }

        public Curve Create(Document document, bool mustGenerateName = false)
        {
            if (document == null) throw new NullException(() => document);

            Curve curve = Create();
            curve.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect2 = new Curve_SideEffect_GenerateName(curve);
                sideEffect2.Execute();
            }

            return curve;
        }

        public Curve Create(params NodeInfo[] nodeInfos)
        {
            return Create((IList<NodeInfo>)nodeInfos);
        }

        public Curve Create(IList<NodeInfo> nodeInfos)
        {
            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            var curve = new Curve();
            curve.ID = _repositories.IDRepository.GetID();
            _repositories.CurveRepository.Insert(curve);

            foreach (NodeInfo nodeInfo in nodeInfos)
            {
                Node node = CreateNode(curve);
                node.Time = nodeInfo.Time;
                node.Value = nodeInfo.Value;
                node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _repositories.NodeTypeRepository);
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
                    Node node = CreateNode(curve);
                    node.Time = time;
                    node.Value = nodeInfo.Value;
                    node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _repositories.NodeTypeRepository);
                }
            }

            return curve;
        }

        /// <param name="values">When a value is null, a node will not be created at that point in time.</param>
        public Curve Create(Document document, double timeSpan, params double?[] values)
        {
            if (document == null) throw new NullException(() => document);
            Curve curve = Create(timeSpan, values);
            curve.LinkTo(document);
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
                    Node node = CreateNode(curve);
                    node.Time = time;
                    node.Value = value.Value;
                    node.SetNodeTypeEnum(NodeTypeEnum.Line, _repositories.NodeTypeRepository);
                }
            }

            return curve;
        }

        public VoidResult Validate(Curve entity)
        {
            IValidator validator = new CurveValidator(entity);

            return new VoidResult
            {
                Successful = validator.IsValid,
                Messages = validator.ValidationMessages.ToCanonical()
            };
        }

        public VoidResult ValidateWithoutRelatedEntities(Curve entity)
        {
            IValidator validator = new CurveValidator_WithoutNodes(entity);

            return new VoidResult
            {
                Successful = validator.IsValid,
                Messages = validator.ValidationMessages.ToCanonical()
            };
        }

        public VoidResult ValidateNodeWithoutParent(Node entity)
        {
            // TODO: Low priority: I doubt it is a good idea to even offer this method. 
            IValidator validator1 = new NodeValidator_WithoutParent(entity);

            return new VoidResult
            {
                Successful = validator1.IsValid,
                Messages = validator1.ValidationMessages.ToCanonical()
            };
        }

        public VoidResult ValidateNode(Node entity)
        {
            IValidator validator1 = new NodeValidator_WithoutParent(entity);
            IValidator validator2 = new NodeValidator_Parent(entity);

            return new VoidResult
            {
                Successful = validator1.IsValid && validator2.IsValid,
                Messages = validator1.ValidationMessages.Union(validator2.ValidationMessages).ToCanonical()
            };
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

        public Node CreateNode(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            var node = new Node();
            node.ID = _repositories.IDRepository.GetID();
            node.LinkTo(curve);
            _repositories.NodeRepository.Insert(node);

            return node;
        }

        public Node CreateNode(Curve curve, Node afterNode)
        {
            if (curve == null) throw new NullException(() => curve);
            if (curve.Nodes.Count < 2) throw new LessThanException(() => curve.Nodes.Count, 2);
            if (afterNode == null) throw new NullException(() => afterNode);

            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.Time).ToArray(); // TODO: Low priority: You could optimize this if there would be an isntance Curve.
            int afterNodeIndex = sortedNodes.IndexOf(afterNode);

            bool isLastNode = afterNodeIndex == sortedNodes.Count - 1;
            if (isLastNode)
            {
                // Insert node at the right.
                Node previousNode = sortedNodes[afterNodeIndex - 1];

                Node node = CreateNode(curve);
                node.NodeType = afterNode.NodeType;
                node.Value = afterNode.Value;

                // Take the previous distance between nodes as the default for the next node.
                double dt = afterNode.Time - previousNode.Time;
                node.Time = afterNode.Time + dt;

                return node;
            }
            else
            {
                // Not last node? Insert node in between two nodes.
                Node beforeNode = sortedNodes[afterNodeIndex + 1];

                // Make sure you do this before creating the new node.
                double value = CalculateIntermediateValue(beforeNode, afterNode);

                Node node = CreateNode(curve);
                node.NodeType = afterNode.NodeType;
                node.Time = afterNode.Time + (beforeNode.Time - afterNode.Time) / 2.0;
                node.Value = value;

                return node;
            }
        }

        private double CalculateIntermediateValue(Node beforeNode, Node afterNode)
        {
            NodeTypeEnum nodeTypeEnum = afterNode.GetNodeTypeEnum();
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Block:
                case NodeTypeEnum.Off:
                    return afterNode.Value;

                case NodeTypeEnum.Line:
                    {
                        double value = (beforeNode.Value + afterNode.Value) / 2.0;
                        return value;
                    }

                case NodeTypeEnum.Curve:
                    {
                        CurveCalculator calculator = CreateCalculator(beforeNode.Curve);
                        double time = (beforeNode.Time + afterNode.Time) / 2;
                        double value =  calculator.CalculateValue(time);
                        return value;
                    }

                default:
                    throw new ValueNotSupportedException(nodeTypeEnum);
            }
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

        public CurveCalculator CreateCalculator(Curve curve)
        {
            return new CurveCalculator(curve);
        }
    }
}
