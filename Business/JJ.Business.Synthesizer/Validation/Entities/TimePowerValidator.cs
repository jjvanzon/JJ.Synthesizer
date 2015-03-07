using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class TimePowerValidator : OperatorValidatorBase
    {
        public TimePowerValidator(Operator obj)
            : base(obj, 
                PropertyNames.TimePower, 3,
                PropertyNames.Signal, PropertyNames.Exponent, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
