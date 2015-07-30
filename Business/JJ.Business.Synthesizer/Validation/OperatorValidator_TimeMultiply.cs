using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_TimeMultiply : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_TimeMultiply(Operator obj)
            : base(obj,
                OperatorTypeEnum.TimeMultiply, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result)
        { }
    }
}
