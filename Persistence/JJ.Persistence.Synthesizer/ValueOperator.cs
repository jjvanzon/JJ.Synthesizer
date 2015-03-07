using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer
{
    public class ValueOperator
    {
        public virtual int ID { get; set; }

        /// <summary>
        /// base
        /// </summary>
        public Operator Operator { get; set; }

        public virtual double Value { get; set; }
    }
}
