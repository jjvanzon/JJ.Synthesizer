using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class And_OperatorWrapper : OperatorWrapperBase_WithAAndB
    {
        public And_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}