using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sine_OperatorWrapper : OperatorWrapperBase_WithFrequency
    {
        public Sine_OperatorWrapper(Operator op) 
            : base(op)
        { }
    }
}