using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class ClosestOverInlets_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public ClosestOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get => InputInlet.InputOutlet;
            set => InputInlet.LinkTo(value);
        }

        public Inlet InputInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Input);

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Outlet> Items
        {
            get
            {
                IList<Outlet> items = InletOutletSelector.EnumerateSortedInputOutlets(WrappedOperator)
                                                    .Skip(1)
                                                    .ToArray();
                return items;
            }
        }

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Inlet> ItemInlets
        {
            get
            {
                IList<Inlet> inlets = InletOutletSelector.EnumerateSortedInlets(WrappedOperator)
                                                    .Skip(1)
                                                    .ToArray();
                return inlets;
            }
        }

        public override string GetInletDisplayName(Inlet inlet)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (inlet.ListIndex > 0)
            {
                return $"{CommonResourceFormatter.Item} {inlet}";
            }

            return base.GetInletDisplayName(inlet);
        }
    }
}