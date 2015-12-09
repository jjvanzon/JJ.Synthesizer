using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Operands : IEnumerable<Outlet>
    {
        private Operator _operator;

        internal CustomOperator_OperatorWrapper_Operands(Operator op)
        {
            _operator = op;
        }

        public Outlet this[string name]
        {
            get { return _operator.Inlets.Single(x => String.Equals(x.Name, name)).InputOutlet; }
            set { _operator.Inlets.Single(x => String.Equals(x.Name, name)).InputOutlet = value; }
        }

        // TODO: Add indexer by int, sort in it, and also make sure these enumerator return the inlets sorted.

        public IEnumerator<Outlet> GetEnumerator()
        {
            for (int i = 0; i < _operator.Inlets.Count; i++)
            {
                yield return _operator.Inlets[i].InputOutlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _operator.Inlets.Count; i++)
            {
                yield return _operator.Inlets[i].InputOutlet;
            }
        }
    }
}
