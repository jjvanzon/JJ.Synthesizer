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

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class PatchOutletValidator : NonSpecializedOperatorValidatorBase
    {
        public PatchOutletValidator(Operator obj)
            : base(obj, PropertyNames.PatchOutlet, 1, PropertyNames.Input, PropertyNames.Result)
        { }
    }
}
