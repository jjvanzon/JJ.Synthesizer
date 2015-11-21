using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_CustomOperator_Outlets : IEnumerable<Outlet>
    {
        private Operator _operator;

        internal OperatorWrapper_CustomOperator_Outlets(Operator op)
        {
            _operator = op;
        }

        public Outlet this[string name]
        {
            get { return _operator.Outlets.Single(x => String.Equals(x.Name, name)); }
        }

        /// <summary> not fast </summary>
        public Outlet this[int index]
        {
            get { return _operator.Outlets.OrderBy(x => x.ListIndex).ElementAt(index); }
        }

        // TODO: Sort in these enumerators, because otherwise there will be inconsistency between these and the indexer.
        public IEnumerator<Outlet> GetEnumerator()
        {
            for (int i = 0; i < _operator.Outlets.Count; i++)
            {
                yield return _operator.Outlets[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _operator.Outlets.Count; i++)
            {
                yield return _operator.Outlets[i];
            }
        }
    }
}
