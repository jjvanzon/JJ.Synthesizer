using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Helpers
{
	public class NodeInfo
	{
		public NodeInfo(double x, double y) : this(x, y, NodeTypeEnum.Line)
		{ }

		public NodeInfo(double x, double y, NodeTypeEnum nodeTypeEnum)
		{
			X = x;
			Y = y;
			NodeTypeEnum = nodeTypeEnum;
		}

		public NodeInfo(double value) : this(value, NodeTypeEnum.Line)
		{ }

		public NodeInfo(double value, NodeTypeEnum nodeTypeEnum)
		{
			Y = value;
			NodeTypeEnum = nodeTypeEnum;
		}

		public double X { get; set; }
		public double Y { get; set; }
		public NodeTypeEnum NodeTypeEnum { get; set; }
	}
}