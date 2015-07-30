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
    public class OperatorValidator_Multiply : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Multiply(Operator obj)
            : base(obj,
                OperatorTypeEnum.Multiply, 3, 
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result)
        { }
    }
}
