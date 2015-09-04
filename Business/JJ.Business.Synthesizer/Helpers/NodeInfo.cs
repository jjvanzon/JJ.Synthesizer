using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Helpers
{
    public class NodeInfo
    {
        public NodeInfo(double time, double value, NodeTypeEnum nodeTypeEnum = NodeTypeEnum.Line)
        {
            Time = time;
            Value = value;
            NodeTypeEnum = nodeTypeEnum;
        }

        public NodeInfo(double value, NodeTypeEnum nodeTypeEnum = NodeTypeEnum.Line)
        {
            Value = value;
            NodeTypeEnum = nodeTypeEnum;
        }

        public double Time { get; set; }
        public double Value { get; set; }
        public NodeTypeEnum NodeTypeEnum { get; set; }
    }
}
