﻿using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    /// <summary>
    /// This class exists to somewhat enforce that all operator warning validators 
    /// take Operator as the constructor argument.
    /// </summary>
    public abstract class OperatorWarningValidatorBase : FluentValidator<Operator>
    {
        public OperatorWarningValidatorBase(Operator op, bool postponeExecute = false)
            : base(op, postponeExecute)
        { }
    }
}
