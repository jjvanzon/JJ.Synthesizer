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
    public class DivideValidator : NonSpecializedOperatorValidatorBase
    {
        public DivideValidator(Operator obj)
            : base(obj, 
                PropertyNames.Divide, 3, 
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
