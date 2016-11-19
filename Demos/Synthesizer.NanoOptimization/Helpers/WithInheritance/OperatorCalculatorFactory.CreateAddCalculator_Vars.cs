

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers.WithInheritance
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateAddCalculator_Vars(IList<OperatorCalculatorBase> operandCalculators)
        {
			if (operandCalculators.Count < 2) throw new LessThanException(() => operandCalculators.Count, 2);

            switch (operandCalculators.Count)
            {
                case 2:
                    return new Add_OperatorCalculator_2Vars
					(
						operandCalculators[0],
						operandCalculators[1]
					);
                case 3:
                    return new Add_OperatorCalculator_3Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2]
					);
                case 4:
                    return new Add_OperatorCalculator_4Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3]
					);
                case 5:
                    return new Add_OperatorCalculator_5Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4]
					);
                case 6:
                    return new Add_OperatorCalculator_6Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5]
					);
                case 7:
                    return new Add_OperatorCalculator_7Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6]
					);
                case 8:
                    return new Add_OperatorCalculator_8Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7]
					);
                default:
                    return new Add_OperatorCalculator_VarArray(operandCalculators);
            }
        }
	}
}
