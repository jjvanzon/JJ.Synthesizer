﻿using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sample_OperatorValidator : OperatorValidator_Basic
    {
        public Sample_OperatorValidator(Operator op)
            : base(op)
        {
            For(op.Sample, ResourceFormatter.Sample).NotNull();
            For(op.Curve, ResourceFormatter.Curve).IsNull();
        }
    }
}