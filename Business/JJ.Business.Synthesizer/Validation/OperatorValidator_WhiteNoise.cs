using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_WhiteNoise : OperatorValidator_Base_NonSpecialized
    {
        public OperatorValidator_WhiteNoise(Operator obj)
            : base(obj, OperatorTypeEnum.WhiteNoise, 0, PropertyNames.Result)
        { }
    }
}
