using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Loop_OperatorWarningValidator : VersatileValidator<Operator>
    {
        private static readonly DimensionEnum[] _dimensionEnumsToCheck =
        {
            DimensionEnum.Signal,
            DimensionEnum.LoopStartMarker,
            DimensionEnum.LoopEndMarker,
            DimensionEnum.NoteDuration
        };

        public Loop_OperatorWarningValidator(Operator obj)
            : base(obj)
        { 
            foreach (DimensionEnum dimensionEnum in _dimensionEnumsToCheck)
            {
                Inlet inlet = InletOutletSelector.TryGetInlet(obj, dimensionEnum);
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
