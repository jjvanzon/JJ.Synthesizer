using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    [Obsolete("Deprecated. SampleOperator's properties will be integrated into Operator entity soon.")]
    public class SampleOperator
    {
        public virtual int ID { get; set; }

        /// <summary> base </summary>
        public virtual Operator Operator { get; set; }

        public Sample Sample { get; set; }
    }
}
