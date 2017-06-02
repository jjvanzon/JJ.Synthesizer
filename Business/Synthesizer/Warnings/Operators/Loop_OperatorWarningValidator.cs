using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Loop_OperatorWarningValidator : OperatorWarningValidator_Base
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
        { }

        protected override void Execute()
        {
            foreach (DimensionEnum dimensionEnum in _dimensionEnumsToCheck)
            {
                Inlet inlet = OperatorHelper.TryGetInlet(Obj, dimensionEnum);
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
