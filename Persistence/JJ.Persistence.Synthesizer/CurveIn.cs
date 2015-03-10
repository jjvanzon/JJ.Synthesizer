using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    [Obsolete("Deprecated. Properties will be integrated into Operator entity soon.")]
    public class CurveIn
    {
        public virtual int ID { get; set; }

        /// <summary> base </summary>
        public virtual Operator Operator { get; set; }

        public virtual Curve Curve { get; set; }
    }
}
