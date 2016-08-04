using JJ.Data.Synthesizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Outlets : IEnumerable<Outlet>
    {
        private readonly Operator _operator;

        internal CustomOperator_OperatorWrapper_Outlets(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            _operator = op;
        }

        public Outlet this[string name]
        {
            get { return OperatorHelper.GetOutlet(_operator, name); }
        }

        /// <summary> not fast </summary>
        public Outlet this[int index]
        {
            get { return OperatorHelper.GetOutlet(_operator, index); }
        }

        public Outlet this[DimensionEnum dimensionEnum]
        {
            get { return OperatorHelper.GetOutlet(_operator, dimensionEnum); }
        }

        public int Count => _operator.Outlets.Count;

        public IEnumerator<Outlet> GetEnumerator()
        {
            foreach (Outlet outlet in OperatorHelper.GetSortedOutlets(_operator))
            {
                yield return outlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Outlet outlet in OperatorHelper.GetSortedOutlets(_operator))
            {
                yield return outlet;
            }
        }
    }
}
