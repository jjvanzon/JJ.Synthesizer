using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;
// ReSharper disable PossibleMultipleEnumeration

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletOrOutletListValidator : VersatileValidator
    {
        public InletOrOutletListValidator(IEnumerable<IInletOrOutlet> inletOrOutletEnumerable)
        {
            if (inletOrOutletEnumerable == null) throw new NullException(() => inletOrOutletEnumerable);

            ValidateRepetitionPositionSequence(inletOrOutletEnumerable);

            foreach (IInletOrOutlet inletOrOutlet in inletOrOutletEnumerable)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(inletOrOutlet);
                ExecuteValidator(new InletOrOutletValidator(inletOrOutlet), messagePrefix);
            }
        }

        private void ValidateRepetitionPositionSequence(IEnumerable<IInletOrOutlet> inletOrOutletEnumerable)
        {
            IList<int> actualRepetitionPositions = inletOrOutletEnumerable.Where(
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

            string concatenatedActualRepetitionPositions = string.Join(", ", actualRepetitionPositions);
            string message = ResourceFormatter.RepetitionPositionsNotConsecutive(concatenatedActualRepetitionPositions);
            Messages.Add(message);
        }
    }
}