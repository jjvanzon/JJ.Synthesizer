using System;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Linq;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_AllInletsFilledIn : OperatorWarningValidator_Base_FirstXInletsFilledIn
    {
        public OperatorWarningValidator_Base_AllInletsFilledIn(Operator obj)
            : base(obj, inletCount: obj?.Inlets?.Count ?? 0)
        { }
    }
}
