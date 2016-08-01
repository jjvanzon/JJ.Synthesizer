using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class BandPassFilterConstantPeakGain_OperatorValidator : OperatorValidator_Base
    {
        public BandPassFilterConstantPeakGain_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.BandPassFilterConstantPeakGain,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal },
                expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}
