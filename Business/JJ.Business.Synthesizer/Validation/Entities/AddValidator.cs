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
    public class AddValidator : GenericOperatorValidator
    {
        public AddValidator(Operator obj)
            : base(obj, PropertyNames.Add, 2, PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Result)
        { }
    }
}
