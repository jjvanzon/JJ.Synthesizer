namespace JJ.Data.Synthesizer
{
    public class Node
    {
        public virtual int ID { get; set; }
        public virtual double Time { get; set; }
        public virtual double Value { get; set; }
        public virtual double Direction { get; set; }
        public virtual NodeType NodeType { get; set; }

        /// <summary> parent </summary>
        public virtual Curve Curve { get; set; }
    }
}
