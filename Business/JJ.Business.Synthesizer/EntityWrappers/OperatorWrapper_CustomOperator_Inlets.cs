using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_CustomOperator_Inlets : IEnumerable<Inlet>
    {
        private Operator _operator;

        internal OperatorWrapper_CustomOperator_Inlets(Operator op)
        {
            _operator = op;
        }

        public Inlet this[string name]
        {
            get { return _operator.Inlets.Single(x => String.Equals(x.Name, name)); }
        }

        /// <summary> not fast </summary>
        public Inlet this[int index]
        {
            get { return _operator.Inlets.OrderBy(x => x.ListIndex).ElementAt(index); }
        }

        // TODO: Sort in these enumerators, because otherwise there will be inconsistency between these and the indexer.
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
