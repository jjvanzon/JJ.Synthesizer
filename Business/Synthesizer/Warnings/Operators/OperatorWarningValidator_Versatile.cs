using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Versatile : ValidatorBase
    {
        public OperatorWarningValidator_Versatile(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            ExecuteValidator(new OperatorWarningValidator_Basic(op));

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                // Only ones with specialized validators are in here. Others are covered with the Basic validator already executed above.
                case OperatorTypeEnum.Cache: ExecuteValidator(new Cache_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Number: ExecuteValidator(new Number_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Spectrum: ExecuteValidator(new Spectrum_OperatorWarningValidator(op)); break;
            }
        }
    }
}
