using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;

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
            get { return OperatorHelper.GetInputOutlet(_operator, name); }
            set { OperatorHelper.GetInlet(_operator, name).LinkTo(value); }
        }

        public Outlet this[int index]
        {
            get { return OperatorHelper.GetInputOutlet(_operator, index); }
            set { OperatorHelper.GetInlet(_operator, index).LinkTo(value); }
        }

        public IEnumerator<Outlet> GetEnumerator()
        {
            foreach (Outlet outlet in OperatorHelper.GetSortedInputOutlets(_operator))
            {
                yield return outlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Outlet outlet in OperatorHelper.GetSortedInputOutlets(_operator))
            {
                yield return outlet;
            }
        }
    }
}
