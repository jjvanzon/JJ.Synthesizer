

using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateMultiplyCalculator_Vars(IList<OperatorCalculatorBase> operandCalculators)
        {
			if (operandCalculators.Count < 2) throw new LessThanException(() => operandCalculators.Count, 2);

            switch (operandCalculators.Count)
            {
                case 2:
                    return new Multiply_OperatorCalculator_2Vars
					(
						operandCalculators[0],
						operandCalculators[1]
					);
                case 3:
                    return new Multiply_OperatorCalculator_3Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2]
					);
                case 4:
                    return new Multiply_OperatorCalculator_4Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3]
					);
                case 5:
                    return new Multiply_OperatorCalculator_5Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4]
					);
                case 6:
                    return new Multiply_OperatorCalculator_6Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5]
					);
                case 7:
                    return new Multiply_OperatorCalculator_7Vars
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
                    return new Multiply_OperatorCalculator_8Vars
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
                case 9:
                    return new Multiply_OperatorCalculator_9Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8]
					);
                case 10:
                    return new Multiply_OperatorCalculator_10Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9]
					);
                case 11:
                    return new Multiply_OperatorCalculator_11Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10]
					);
                case 12:
                    return new Multiply_OperatorCalculator_12Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10],
						operandCalculators[11]
					);
                case 13:
                    return new Multiply_OperatorCalculator_13Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10],
						operandCalculators[11],
						operandCalculators[12]
					);
                case 14:
                    return new Multiply_OperatorCalculator_14Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10],
						operandCalculators[11],
						operandCalculators[12],
						operandCalculators[13]
					);
                case 15:
                    return new Multiply_OperatorCalculator_15Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10],
						operandCalculators[11],
						operandCalculators[12],
						operandCalculators[13],
						operandCalculators[14]
					);
                case 16:
                    return new Multiply_OperatorCalculator_16Vars
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6],
						operandCalculators[7],
						operandCalculators[8],
						operandCalculators[9],
						operandCalculators[10],
						operandCalculators[11],
						operandCalculators[12],
						operandCalculators[13],
						operandCalculators[14],
						operandCalculators[15]
					);
                default:
                    return new Multiply_OperatorCalculator_VarArray(operandCalculators);
            }
        }
	}
}
