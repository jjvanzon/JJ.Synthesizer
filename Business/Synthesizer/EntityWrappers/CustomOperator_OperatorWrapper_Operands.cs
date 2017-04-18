using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper_Operands : IEnumerable<Outlet>
    {
        private readonly Operator _operator;

        internal CustomOperator_OperatorWrapper_Operands(Operator op)
        {
            _operator = op ?? throw new NullException(() => op);
        }

        public Outlet this[string name]
        {
            get => OperatorHelper.GetInputOutlet(_operator, name);
            set => OperatorHelper.GetInlet(_operator, name).LinkTo(value);
        }

        public Outlet this[int index]
        {
            get => OperatorHelper.GetInputOutlet(_operator, index);
            set => OperatorHelper.GetInlet(_operator, index).LinkTo(value);
        }

        public Outlet this[DimensionEnum dimensionEnum]
        {
            get => OperatorHelper.GetInputOutlet(_operator, dimensionEnum);
            set => OperatorHelper.GetInlet(_operator, dimensionEnum).LinkTo(value);
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
