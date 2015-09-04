using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Custom_OperatorWrapper_Inlets : IEnumerable<Inlet>
    {
        private Operator _operator;

        internal Custom_OperatorWrapper_Inlets(Operator op)
        {
            _operator = op;
        }

        public Inlet this[string name]
        {
            get { return _operator.Inlets.Single(x => String.Equals(x.Name, name)); }
        }

        public IEnumerator<Inlet> GetEnumerator()
        {
            for (int i = 0; i < _operator.Inlets.Count; i++)
            {
                yield return _operator.Inlets[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _operator.Inlets.Count; i++)
            {
                yield return _operator.Inlets[i];
            }
        }
    }
}
