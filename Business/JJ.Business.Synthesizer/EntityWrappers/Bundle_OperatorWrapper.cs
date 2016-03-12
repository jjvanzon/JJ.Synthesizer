using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Bundle_OperatorWrapper : OperatorWrapperBase
    {
        private const int RESULT_INDEX = 0;

        public Bundle_OperatorWrapper(Operator op)
            : base(op)
        { }

        /// <summary> Executes a loop, so prevent calling it multiple times. summary>
        public IList<Outlet> Operands
        {
            get
            {
                IList<Outlet> operands = new Outlet[WrappedOperator.Inlets.Count];
                for (int i = 0; i < WrappedOperator.Inlets.Count; i++)
                {
                    operands[i] = WrappedOperator.Inlets[i].InputOutlet;
                }
                return operands;
            }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            if (listIndex > WrappedOperator.Inlets.Count) throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);

            string name = String.Format("{0} {1}", PropertyDisplayNames.Inlet, listIndex + 1);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Bundle_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;
            
            return wrapper.Result;
        }
    }
}