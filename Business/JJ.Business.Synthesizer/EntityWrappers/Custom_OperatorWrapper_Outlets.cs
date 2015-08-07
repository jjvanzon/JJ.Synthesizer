using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Custom_OperatorWrapper_Outlets : IEnumerable<Outlet>
    {
        private Operator _operator;

        internal Custom_OperatorWrapper_Outlets(Operator op)
        {
            _operator = op;
        }

        public Outlet this[string name]
        {
            get { return _operator.Outlets.Single(x => String.Equals(x.Name, name)); }
        }

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
