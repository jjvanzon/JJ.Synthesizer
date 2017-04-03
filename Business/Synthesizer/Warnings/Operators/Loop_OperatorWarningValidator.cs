using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Loop_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        private static readonly int[] _indexesToCheck =
        {
            OperatorConstants.LOOP_SIGNAL_INDEX,
            OperatorConstants.LOOP_LOOP_START_MARKER_INDEX,
            OperatorConstants.LOOP_LOOP_END_MARKER_INDEX,
            OperatorConstants.LOOP_NOTE_DURATION_INDEX
        };

        public Loop_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<Inlet> sortedInlets = Obj.Inlets.OrderBy(x => x.ListIndex).ToArray();

            foreach (int indexToCheck in _indexesToCheck)
            {
                bool isValidIndex = sortedInlets.Count > indexToCheck;
                // ReSharper disable once InvertIf
                if (isValidIndex)
                {
                    Inlet inlet = sortedInlets[indexToCheck];

                    string inletIdentifier = ValidationHelper.GetUserFriendlyIdentifier(inlet);

                    For(() => inlet.InputOutlet, inletIdentifier).NotNull();
                }
            }
        }
    }
}
