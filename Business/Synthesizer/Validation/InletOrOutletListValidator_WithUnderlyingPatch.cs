using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletOrOutletListValidator_WithUnderlyingPatch : VersatileValidator
    {
        public InletOrOutletListValidator_WithUnderlyingPatch(IEnumerable<IInletOrOutlet> inletOrOutletList)
        {
            if (inletOrOutletList == null) throw new NullException(() => inletOrOutletList);

            ValidateRepetitionPositionSequence(inletOrOutletList);

            foreach (IInletOrOutlet inletOrOutlet in inletOrOutletList)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(inletOrOutlet);
                ExecuteValidator(new InletOrOutletValidator_WithUnderlyingPatch(inletOrOutlet), messagePrefix);
            }
        }

        private void ValidateRepetitionPositionSequence(IEnumerable<IInletOrOutlet> inletOrOutletList)
        {
            IList<int> actualRepetitionPositions = inletOrOutletList.Where(
                                                                        x => x.IsRepeating &&
                                                                             x.RepetitionPosition.HasValue)
                                                                    .Select(x => x.RepetitionPosition.Value)
                                                                    .OrderBy(x => x)
                                                                    .ToArray();
            if (actualRepetitionPositions.Count <= 0)
            {
                return;
            }

            int firstRepetitionPosition = actualRepetitionPositions[0];
            if (firstRepetitionPosition != 0)
            {
                Messages.AddNotEqualMessage(ResourceFormatter.FirstRepetitionPosition, 0);
            }

            IList<int> expectedRepetitionPositions = Enumerable.Range(
                actualRepetitionPositions[0],
                actualRepetitionPositions.Count).ToArray();

            bool sequenceEqual = actualRepetitionPositions.SequenceEqual(expectedRepetitionPositions);
            if (sequenceEqual)
            {
                return;
            }

            string concatinatedActualRepetitionPositions = string.Join(", ", actualRepetitionPositions);
            string message = ResourceFormatter.RepetitionPositionsNotConsecutive(concatinatedActualRepetitionPositions);
            Messages.Add(message);
        }
    }
}