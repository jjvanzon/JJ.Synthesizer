using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Loop_OperatorWarningValidator : VersatileValidator
    {
        private static readonly DimensionEnum[] _dimensionEnumsToCheck =
        {
            DimensionEnum.Signal,
            DimensionEnum.LoopStartMarker,
            DimensionEnum.LoopEndMarker,
            DimensionEnum.NoteDuration
        };

        public Loop_OperatorWarningValidator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            foreach (DimensionEnum dimensionEnum in _dimensionEnumsToCheck)
            {
                Inlet inlet = InletOutletSelector.TryGetInlet(op, dimensionEnum);
                if (inlet == null)
                {
                    continue;
                }

                string inletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);

                For(() => inlet.InputOutlet, inletIdentifier).NotNull();
            }
        }
    }
}
