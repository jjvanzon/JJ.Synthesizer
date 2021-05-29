using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Interfaces;
// ReSharper disable once InconsistentNaming

namespace JJ.Business.Synthesizer.Extensions
{
    public static class IInletOrOutletExtensions
    {
        public static void ReassignRepetitionPositions(this IEnumerable<IInletOrOutlet> inletsOrOutlets)
        {
            IList<IInletOrOutlet> sortedInletsOrOutlets = inletsOrOutlets.Sort().ToArray();

            int i = 0;

            foreach (IInletOrOutlet inletOrOutlet in sortedInletsOrOutlets)
            {
                OperatorTypeEnum operatorTypeEnum = inletOrOutlet.Operator.GetOperatorTypeEnum();

                bool mustSetRepetitionPosition = inletOrOutlet.IsRepeating &&
                                                 operatorTypeEnum != OperatorTypeEnum.PatchInlet &&
                                                 operatorTypeEnum != OperatorTypeEnum.PatchOutlet;
                if (mustSetRepetitionPosition)
                {
                    inletOrOutlet.RepetitionPosition = i++;
                }
                else
                {
                    inletOrOutlet.RepetitionPosition = null;
                }
            }
        }
    }
}
