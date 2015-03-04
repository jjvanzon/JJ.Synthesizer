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

namespace JJ.Business.Synthesizer.Validation
{
    public class MultiplyValidator : GenericOperatorValidator
    {
        public MultiplyValidator(Operator obj)
            : base(obj, 
            PropertyNames.Multiply, 3, 
            PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
            PropertyNames.Result)
        { }
    }
}
