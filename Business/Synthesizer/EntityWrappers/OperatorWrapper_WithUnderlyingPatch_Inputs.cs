using System.Collections;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_WithUnderlyingPatch_Inputs : IEnumerable<Outlet>
    {
        private readonly Operator _operator;

        internal OperatorWrapper_WithUnderlyingPatch_Inputs(Operator op)
        {
            _operator = op ?? throw new NullException(() => op);
        }

        public Outlet this[string name]
        {
            get => InletOutletSelector.GetInputOutlet(_operator, name);
            set => InletOutletSelector.GetInlet(_operator, name).LinkTo(value);
        }

        public Outlet this[int index]
        {
            get => InletOutletSelector.GetInputOutlet(_operator, index);
            set => InletOutletSelector.GetInlet(_operator, index).LinkTo(value);
        }

        public Outlet this[DimensionEnum dimensionEnum]
        {
            get => InletOutletSelector.GetInputOutlet(_operator, dimensionEnum);
            set => InletOutletSelector.GetInlet(_operator, dimensionEnum).LinkTo(value);
        }

        public IEnumerator<Outlet> GetEnumerator()
        {
            foreach (Outlet outlet in InletOutletSelector.GetSortedInputOutlets(_operator))
            {
                yield return outlet;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Outlet outlet in InletOutletSelector.GetSortedInputOutlets(_operator))
            {
                yield return outlet;
            }
        }
    }
}
