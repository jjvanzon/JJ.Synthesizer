using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Cascading;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
	/// <summary> Public for instance to validate in the CurveDetails view. </summary>
	public class CurveFacade
	{
		private readonly CurveRepositories _repositories;

		public CurveFacade(CurveRepositories repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
		}

		// Create

		// About the overloads:
		// You would think you want to generate a Curve name any time you can.
		// But this creates an optional dependency on Document.
		// Combined with params, this would create an explosion of overloads.
		// Instead the App will generally use the overload that takes Document, which generates a name.
		// The fancy overloads with tuples will generally only be used as an API, 
		// in case of which generating a name is not as important, saving us the dependency on Document.

		public Curve Create(Document document = null)
		{
			Curve curve = CreateWithoutNodes();

			new Curve_SideEffect_SetDefaults_Nodes(
				curve,
				_repositories.NodeRepository,
				_repositories.NodeTypeRepository,
				_repositories.IDRepository)
				.Execute();

			// Only execute this side-effect for the overload that takes Document.
			new Curve_SideEffect_GenerateName(curve, document).Execute();

			return curve;
		}

		private Curve CreateWithoutNodes()
		{
			var curve = new Curve { ID = _repositories.IDRepository.GetID() };
			_repositories.CurveRepository.Insert(curve);

			return curve;
		}

		public Curve Create(params (double x, double y, NodeTypeEnum nodeTypeEnum)[] nodeTuples)
				  => Create((IList<(double x, double y, NodeTypeEnum nodeTypeEnum)>)nodeTuples);

		public Curve Create(IList<(double x, double y, NodeTypeEnum nodeTypeEnum)> nodeTuples)
		{
			if (nodeTuples == null) throw new NullException(() => nodeTuples);

			Curve curve = CreateWithoutNodes();

			foreach (var (x, y, nodeTypeEnum) in nodeTuples)
			{
				Node node = CreateNode(curve);
				node.X = x;
				node.Y = y;
				node.SetNodeTypeEnum(nodeTypeEnum, _repositories.NodeTypeRepository);
			}

			return curve;
		}

		/// <param name="nodeTuples">When an item is null, a node will not be created at that point in time.</param>
		public Curve Create(double xSpan, params (double y, NodeTypeEnum nodeTypeEnum)?[] nodeTuples) 
			      => Create(xSpan, (IList<(double y, NodeTypeEnum nodeTypeEnum)?>)nodeTuples);

		/// <param name="nodeTuples">When an item is null, a node will not be created at that point in time.</param>
		public Curve Create(double xSpan, IList<(double y, NodeTypeEnum nodeTypeEnum)?> nodeTuples)
		{
			if (nodeTuples == null) throw new NullException(() => nodeTuples);

			int count = nodeTuples.Count;

			double[] xList = MathHelper.SpreadDoubles(xSpan, count);

			Curve curve = CreateWithoutNodes();

			for (int i = 0; i < count; i++)
			{
				(double y, NodeTypeEnum nodeTypeEnum)? tuple = nodeTuples[i];
				double x = xList[i];

				if (tuple == null)
				{
					continue;
				}

				Node node = CreateNode(curve);
				node.X = x;
				node.Y = tuple.Value.y;
				node.SetNodeTypeEnum(tuple.Value.nodeTypeEnum, _repositories.NodeTypeRepository);
			}

			return curve;
		}

		/// <param name="yValues">When a value is null, a node will not be created at that point in time.</param>
		public Curve Create(double xSpan, params double?[] yValues)
				  => Create(xSpan, (IList<double?>)yValues);

		/// <param name="yValues">When a value is null, a node will not be created at that point in time.</param>
		public Curve Create(double xSpan, IList<double?> yValues)
		{
			if (yValues == null) throw new NullException(() => yValues);

			Curve curve = CreateWithoutNodes();

			int count = yValues.Count;

			double[] xList = MathHelper.SpreadDoubles(xSpan, count);

			for (int i = 0; i < count; i++)
			{
				double? y = yValues[i];

				if (!y.HasValue)
				{
					continue;
				}

				double x = xList[i];

				Node node = CreateNode(curve);
				node.X = x;
				node.Y = y.Value;
				node.SetNodeTypeEnum(NodeTypeEnum.Line, _repositories.NodeTypeRepository);
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

		// Save

		public VoidResult SaveCurveWithRelatedEntities(Curve entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var validators = new List<IValidator>
			{
				new CurveValidator_WithoutNodes(entity),
				new CurveValidator_Nodes(entity)
			};

			return validators.ToResult();
		}

		public VoidResult SaveNode(Node entity)
		{
			var validators = new IValidator[]
			{
				new NodeValidator_WithoutParent(entity),
				new NodeValidator_Parent(entity)
			};

			return validators.ToResult();
		}

		// Delete

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
			ArrayDto arrayDto = CurveArrayDtoFactory.ConvertToArrayDto(curve);
			return ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);
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
