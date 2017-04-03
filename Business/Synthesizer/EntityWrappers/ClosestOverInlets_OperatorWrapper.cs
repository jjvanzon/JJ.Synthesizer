using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class ClosestOverInlets_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int INPUT_INDEX = 0;

        public ClosestOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get { return InputInlet.InputOutlet; }
            set { InputInlet.LinkTo(value); }
        }

        public Inlet InputInlet => OperatorHelper.GetInlet(WrappedOperator, INPUT_INDEX);

        /// <summary> Executes a loop, so prevent calling it multiple times. </summary>
        public IList<Outlet> Items
        {
            get
            {
                IList<Outlet> items = OperatorHelper.EnumerateSortedInputOutlets(WrappedOperator)
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
                IList<Inlet> inlets = OperatorHelper.EnumerateSortedInlets(WrappedOperator)
                                                    .Skip(1)
                                                    .ToArray();
                return inlets;
            }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            if (listIndex > WrappedOperator.Inlets.Count + 1) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count + 1);

            if (listIndex == INPUT_INDEX)
            {
                string name = ResourceFormatter.GetDisplayName(() => Input);
                return name;
            }
            else
            {
                string name = $"{CommonResourceFormatter.Item} {listIndex}";
                return name;
            }
        }
    }
}