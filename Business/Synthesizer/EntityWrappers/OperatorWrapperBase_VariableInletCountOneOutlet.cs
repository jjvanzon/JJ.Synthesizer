using JJ.Business.Synthesizer.Helpers;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableInletCountOneOutlet : OperatorWrapperBase_WithOneOutlet
    {
        public OperatorWrapperBase_VariableInletCountOneOutlet(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Outlet> Items => InletOutletSelector.GetSortedInputOutlets(WrappedOperator);

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Inlet> Inlets => InletOutletSelector.GetSortedInlets(WrappedOperator);

        public override string GetInletDisplayName(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            string name = $"{ResourceFormatter.Inlet} {inlet.ListIndex + 1}";
            return name;
        }
    }
}