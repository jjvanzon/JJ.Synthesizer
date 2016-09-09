using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SortOverInlets_OperatorWrapper : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public SortOverInlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(SortOverInlets_OperatorWrapper wrapper) => wrapper?.Result;
    }
}