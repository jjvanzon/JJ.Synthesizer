using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Inlets : IEnumerable<Inlet>
    {
        private readonly Operator _operator;

        internal CustomOperator_OperatorWrapper_Inlets(Operator op)
        {
            _operator = op ?? throw new NullException(() => op);
        }

        public Inlet this[string name] => OperatorHelper.GetInlet(_operator, name);
        public Inlet TryGet(string name) => OperatorHelper.TryGetInlet(_operator, name);
        public IList<Inlet> GetMany(string name) => OperatorHelper.GetInlets(_operator, name);

        /// <summary> not fast </summary>
        public Inlet this[int index] => OperatorHelper.GetInlet(_operator, index);
        public Inlet TryGet(int index) => OperatorHelper.TryGetInlet(_operator, index);
        public IList<Inlet> GetMany(int index) => OperatorHelper.GetInlets(_operator, index);

        public Inlet this[DimensionEnum dimensionEnum] => OperatorHelper.GetInlet(_operator, dimensionEnum);
        public Inlet TryGet(DimensionEnum dimensionEnum) => OperatorHelper.TryGetInlet(_operator, dimensionEnum);
        public IList<Inlet> GetMany(DimensionEnum dimensionEnum) => OperatorHelper.GetInlets(_operator, dimensionEnum);

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
