using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class RepeatInfoValidator : VersatileValidator
    {
        public RepeatInfoValidator(
            OperatorTypeEnum operatorTypeEnum,
            bool isRepeating,
            int? repetitionPosition)
        {
            bool repetitionPositionMustBeNull = !isRepeating ||
                                                operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
                                                operatorTypeEnum == OperatorTypeEnum.PatchOutlet;
            if (repetitionPositionMustBeNull)
            {
                For(repetitionPosition, ResourceFormatter.RepetitionPosition).IsNull();
            }

            if (isRepeating)
            {
                For(repetitionPosition, ResourceFormatter.RepetitionPosition).NotNull();
            }
        }
    }
}
