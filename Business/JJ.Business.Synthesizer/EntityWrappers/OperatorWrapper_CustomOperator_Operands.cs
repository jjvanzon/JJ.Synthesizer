using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_CustomOperator_Operands : IEnumerable<Outlet>
    {
        private Operator _operator;

        internal OperatorWrapper_CustomOperator_Operands(Operator op)
        {
            _operator = op;
        }

        public Outlet this[string name]
        {
            get { return _operator.Inlets.Single(x => String.Equals(x.Name, name)).InputOutlet; }
            set { _operator.Inlets.Single(x => String.Equals(x.Name, name)).InputOutlet = value; }
        }

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
