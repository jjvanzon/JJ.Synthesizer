﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorValidator : OperatorValidator_Base
    {
        public BandPassFilterConstantTransitionGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantTransitionGain,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal },
                expectedDataKeys: new string[0])
        { }
    }
}