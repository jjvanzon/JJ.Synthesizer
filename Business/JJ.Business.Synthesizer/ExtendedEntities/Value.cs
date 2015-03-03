using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.ExtendedEntities
{
    public class Value
    {
        private Operator _operator;
        public Operator Operator { get { return _operator; } }

        public Value(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            _operator = op;
        }

        public Outlet Result
        {
            get { return _operator.Outlets[0]; }
        }
    }
}