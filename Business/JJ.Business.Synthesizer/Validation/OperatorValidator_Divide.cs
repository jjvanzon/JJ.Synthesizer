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

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Divide : OperatorValidator_Base_NonSpecialized
    {
        public OperatorValidator_Divide(Operator obj)
            : base(obj, 
                PropertyNames.Divide, 3, 
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
