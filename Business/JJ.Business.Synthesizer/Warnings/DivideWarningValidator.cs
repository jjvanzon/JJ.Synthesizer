using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class DivideWarningValidator : FirstXInletsNotFilledInWarningValidator
    {
        public DivideWarningValidator(Operator obj)
            : base(obj, inletCount: 2)
        { }
    }
}
