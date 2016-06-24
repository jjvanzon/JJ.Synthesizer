using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Closest_OperatorWrapper : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int RESULT_INDEX = 0;

        public Closest_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
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

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            if (listIndex > WrappedOperator.Inlets.Count + 1) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count + 1);

            if (listIndex == SIGNAL_INDEX)
            {
                string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                return name;
            }
            else
            {
                string name = String.Format("{0} {1}", CommonTitles.Item, listIndex);
                return name;
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }
    }
}