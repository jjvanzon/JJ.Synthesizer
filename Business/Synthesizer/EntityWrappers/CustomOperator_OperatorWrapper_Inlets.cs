using JJ.Data.Synthesizer;
using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Inlets : IEnumerable<Inlet>
    {
        private readonly Operator _operator;

        internal CustomOperator_OperatorWrapper_Inlets(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            _operator = op;
        }

        public Inlet this[string name]
        {
            get { return OperatorHelper.GetInlet(_operator, name); }
        }

        /// <summary> not fast </summary>
        public Inlet this[int index]
        {
            get { return OperatorHelper.GetInlet(_operator, index); }
        }

        public Inlet this[DimensionEnum dimensionEnum]
        {
            get { return OperatorHelper.GetInlet(_operator, dimensionEnum); }
        }

        public int Count => _operator.Inlets.Count;

        public IEnumerator<Inlet> GetEnumerator()
        {
            foreach (Inlet inlet in OperatorHelper.GetSortedInlets(_operator))
            {
                yield return inlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Inlet inlet in OperatorHelper.GetSortedInlets(_operator))
            {
                yield return inlet;
            }
        }
    }
}
