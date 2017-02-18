using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Validation.Curves;

namespace JJ.Business.Synthesizer
{
    public class CurveManager
    {
        private readonly CurveRepositories _repositories;

        public CurveManager(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Create

        public Curve Create()
        {
            var curve = CreateWithoutNodes();

            new Curve_SideEffect_SetDefaults_Nodes(
                curve,
                _repositories.NodeRepository,
                _repositories.NodeTypeRepository,
                _repositories.IDRepository)
                .Execute();

            return curve;
        }

        private Curve CreateWithoutNodes()
        {
            var curve = new Curve { ID = _repositories.IDRepository.GetID() };
            _repositories.CurveRepository.Insert(curve);
            return curve;
        }

        public Curve Create(Document document, bool mustGenerateName = false)
        {
            if (document == null) throw new NullException(() => document);

            Curve curve = Create();
            curve.LinkTo(document);

            // ReSharper disable once InvertIf
            if (mustGenerateName)
            {
                new Curve_SideEffect_GenerateName(curve).Execute();
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

            Curve curve = CreateWithoutNodes();

            foreach (NodeInfo nodeInfo in nodeInfos)
            {
                Node node = CreateNode(curve);
                node.X = nodeInfo.X;
                node.Y = nodeInfo.Y;
                node.SetNodeTypeEnum(nodeInfo.NodeTypeEnum, _repositories.NodeTypeRepository);
            }

            return curve;
        }

        /// <param name="nodeInfos">When a NodeInfo is null, a node will not be created at that point in time.</param>
        public Curve Create(double timeSpan, params NodeInfo[] nodeInfos)
        {
            // TODO: I do not like this method signature looks in the method calls.

            if (nodeInfos == null) throw new NullException(() => nodeInfos);

            Curve curve = CreateWithoutNodes();

            double[] times = GetEquidistantPointsOverX(timeSpan, nodeInfos.Length);

            for (int i = 0; i < nodeInfos.Length; i++)
            {
                double time = times[i];
                NodeInfo nodeInfo = nodeInfos[i];

                // ReSharper disable once InvertIf
                if (nodeInfo != null)
                {
                    Node node = CreateNode(curve);
                    node.X = time;
                    node.Y = nodeInfo.Y;
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

        /// <param name="yValues">When a value is null, a node will not be created at that point in time.</param>
        public Curve Create(double xSpan, params double?[] yValues)
        {
            if (xSpan <= 0) throw new LessThanOrEqualException(() => xSpan, 0);
            if (yValues == null) throw new NullException(() => yValues);
            if (yValues.Length < 2) throw new LessThanException(() => yValues.Length, 2);

            Curve curve = CreateWithoutNodes();

            double[] times = GetEquidistantPointsOverX(xSpan, yValues.Length);

            for (int i = 0; i < yValues.Length; i++)
            {
                double? y = yValues[i];
                double x = times[i];

                // ReSharper disable once InvertIf
                if (y.HasValue)
                {
                    Node node = CreateNode(curve);
                    node.X = x;
                    node.Y = y.Value;
                    node.SetNodeTypeEnum(NodeTypeEnum.Line, _repositories.NodeTypeRepository);
                }
            }

            return curve;
        }

        public Node CreateNode(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            var node = new Node { ID = _repositories.IDRepository.GetID() };
            node.LinkTo(curve);
            _repositories.NodeRepository.Insert(node);

            return node;
        }

        public Node CreateNode(Curve curve, Node afterNode)
        {
            if (curve == null) throw new NullException(() => curve);
            if (curve.Nodes.Count < 2) throw new LessThanException(() => curve.Nodes.Count, 2);
            if (afterNode == null) throw new NullException(() => afterNode);

            IList<Node> sortedNodes = curve.Nodes.OrderBy(x => x.X).ToArray(); // TODO: Low priority: You could optimize this if there would be an isntance Curve.
            int afterNodeIndex = sortedNodes.IndexOf(afterNode);

            bool isLastNode = afterNodeIndex == sortedNodes.Count - 1;
            if (isLastNode)
            {
                // Insert node at the right.
                Node previousNode = sortedNodes[afterNodeIndex - 1];

                Node node = CreateNode(curve);
                node.NodeType = afterNode.NodeType;
                node.Y = afterNode.Y;

                // Take the previous distance between nodes as the default for the next node.
                double dx = afterNode.X - previousNode.X;
                node.X = afterNode.X + dx;

                return node;
            }
            else
            {
                // Not last node? Insert node in between two nodes.
                Node beforeNode = sortedNodes[afterNodeIndex + 1];

                // Make sure you do this before creating the new node.
                double y = CalculateIntermediateYValue(beforeNode, afterNode);

                Node node = CreateNode(curve);
                node.NodeType = afterNode.NodeType;
                node.X = afterNode.X + (beforeNode.X - afterNode.X) / 2.0;
                node.Y = y;

                return node;
            }
        }

        // Validate

        public VoidResult SaveCurveWithRelatedEntities(Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var validators = new List<IValidator>
            {
                new CurveValidator_WithoutNodes(entity),
                new CurveValidator_Nodes(entity)
            };

            if (entity.Document != null)
            {
                validators.Add(new CurveValidator_InDocument(entity));
            }

            var result = new VoidResult
            {
                Successful = validators.All(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        public VoidResult SaveNode(Node entity)
        {
            var validators = new IValidator[]
            {
                new NodeValidator_WithoutParent(entity),
                new NodeValidator_Parent(entity)
            };

            var result = new VoidResult
            {
                Successful = validators.All(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        // Delete

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
            // ReSharper disable once RedundantIfElseBlock
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

        public void DeleteNode(int nodeID)
        {
            Node node = _repositories.NodeRepository.Get(nodeID);
            DeleteNode(node);
        }

        public VoidResult DeleteNode(Node node)
        {
            if (node == null) throw new NullException(() => node);

            Curve curve = node.Curve;

            node.UnlinkRelatedEntities();
            _repositories.NodeRepository.Delete(node);

            return SaveCurveWithRelatedEntities(curve);
        }

        // Misc

        /// <summary> Faster initialization, slower calculation. </summary>
        public ICalculatorWithPosition CreateInterpretedCalculator(Curve curve)
        {
            return new InterpretedCurveCalculator(curve);
        }

        /// <summary> Slower initialization, faster calculation. </summary>
        public ICalculatorWithPosition CreateOptimizedCalculator(Curve curve)
        {
            return CurveArrayCalculatorFactory.CreateCurveArrayCalculator(curve);
        }

        public void RotateNodeType(Node node)
        {
            if (node == null) throw new NullException(() => node);

            NodeTypeEnum nodeTypeEnum = node.GetNodeTypeEnum();

            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Off:
                    nodeTypeEnum = NodeTypeEnum.Block;
                    break;

                case NodeTypeEnum.Block:
                    nodeTypeEnum = NodeTypeEnum.Line;
                    break;

                case NodeTypeEnum.Line:
                    nodeTypeEnum = NodeTypeEnum.Curve;
                    break;

                case NodeTypeEnum.Curve:
                    nodeTypeEnum = NodeTypeEnum.Off;
                    break;

                default:
                    throw new InvalidValueException(nodeTypeEnum);
            }

            node.SetNodeTypeEnum(nodeTypeEnum, _repositories.NodeTypeRepository);
        }

        // Helpers

        private double[] GetEquidistantPointsOverX(double xSpan, int pointCount)
        {
            if (xSpan <= 0) throw new LessThanOrEqualException(() => xSpan, 0);
            if (pointCount < 2) throw new LessThanException(() => pointCount, 2);

            var xValues = new double[pointCount];
            double x = 0;
            double dx = xSpan / (pointCount - 1);
            for (int i = 0; i < pointCount; i++)
            {
                xValues[i] = x;
                x += dx;
            }

            return xValues;
        }

        private double CalculateIntermediateYValue(Node beforeNode, Node afterNode)
        {
            NodeTypeEnum nodeTypeEnum = afterNode.GetNodeTypeEnum();
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Block:
                case NodeTypeEnum.Off:
                    return afterNode.Y;

                case NodeTypeEnum.Line:
                    {
                        double y = (beforeNode.Y + afterNode.Y) / 2.0;
                        return y;
                    }

                case NodeTypeEnum.Curve:
                    {
                        ICalculatorWithPosition calculator = CreateInterpretedCalculator(beforeNode.Curve);
                        double x = (beforeNode.X + afterNode.X) / 2;
                        double y = calculator.Calculate(x);
                        return y;
                    }

                default:
                    throw new ValueNotSupportedException(nodeTypeEnum);
            }
        }
    }
}
