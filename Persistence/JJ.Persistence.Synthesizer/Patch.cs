using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer
{
    public class Patch
    {
        public Patch()
        {
            Operators = new List<Operator>();
        }

        public IList<Operator> Operators { get; set; }
    }
}
