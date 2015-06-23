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
    public class OperatorValidator_Sine : OperatorValidator_Base_NonSpecialized
    {
        public OperatorValidator_Sine(Operator obj)
            : base(obj,
                OperatorTypeEnum.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Level, PropertyNames.PhaseStart, 
                PropertyNames.Result)
        { }
    }
}
