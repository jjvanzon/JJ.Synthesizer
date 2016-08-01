using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class NotchFilter_OperatorValidator : OperatorValidator_Base
    {
        public NotchFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.NotchFilter,
                expectedDataKeys: new string[0],
                expectedInletCount: 3,
                expectedOutletCount: 1)
        { }


        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}
