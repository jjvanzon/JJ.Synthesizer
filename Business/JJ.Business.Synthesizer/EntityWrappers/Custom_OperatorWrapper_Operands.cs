using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Custom_OperatorWrapper_Operands : IEnumerable<Outlet>
    {
        private Operator _operator;

        internal Custom_OperatorWrapper_Operands(Operator op)
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
