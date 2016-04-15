using System.Collections.Generic;

namespace JJ.Data.Synthesizer
{
    public class Curve
    {
        public Curve()
        {
            Nodes = new List<Node>();
        }

        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Node> Nodes { get; set; }

        /// <summary> nullable </summary>
        public virtual Document Document { get; set; }
    }
}
