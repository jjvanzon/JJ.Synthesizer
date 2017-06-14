using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sine_OperatorWrapper : OperatorWrapperBase_WithFrequency
    {
        public Sine_OperatorWrapper(Operator op) 
            : base(op)
        { }
    }
}