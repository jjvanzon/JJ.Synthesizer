using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Outlets : IEnumerable<Outlet>
    {
        private readonly Operator _operator;

        internal CustomOperator_OperatorWrapper_Outlets(Operator op)
        {
            _operator = op ?? throw new NullException(() => op);
        }

        public Outlet this[string name] => OperatorHelper.GetOutlet(_operator, name);
        public Outlet TryGet(string name) => OperatorHelper.TryGetOutlet(_operator, name);
        public IList<Outlet> GetMany(string name) => OperatorHelper.GetOutlets(_operator, name);

        /// <summary> not fast </summary>
        public Outlet this[int index] => OperatorHelper.GetOutlet(_operator, index);
        public Outlet TryGet(int index) => OperatorHelper.TryGetOutlet(_operator, index);
        public IList<Outlet> GetMany(int index) => OperatorHelper.GetOutlets(_operator, index);

        public Outlet this[DimensionEnum dimensionEnum] => OperatorHelper.GetOutlet(_operator, dimensionEnum);
        public Outlet TryGet(DimensionEnum dimensionEnum) => OperatorHelper.TryGetOutlet(_operator, dimensionEnum);
        public IList<Outlet> GetMany(DimensionEnum dimensionEnum) => OperatorHelper.GetOutlets(_operator, dimensionEnum);

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
