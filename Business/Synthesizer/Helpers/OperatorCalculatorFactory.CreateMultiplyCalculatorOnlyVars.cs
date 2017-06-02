

using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateMultiplyCalculator_Vars(IList<OperatorCalculatorBase> itemCalculators)
        {
			if (itemCalculators.Count < 2) throw new LessThanException(() => itemCalculators.Count, 2);

            switch (itemCalculators.Count)
            {
                case 2:
                    return new Multiply_OperatorCalculator_2Vars
					(
						itemCalculators[0],
						itemCalculators[1]
					);
                case 3:
                    return new Multiply_OperatorCalculator_3Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2]
					);
                case 4:
                    return new Multiply_OperatorCalculator_4Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3]
					);
                case 5:
                    return new Multiply_OperatorCalculator_5Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4]
					);
                case 6:
                    return new Multiply_OperatorCalculator_6Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5]
					);
                case 7:
                    return new Multiply_OperatorCalculator_7Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6]
					);
                case 8:
                    return new Multiply_OperatorCalculator_8Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7]
					);
                case 9:
                    return new Multiply_OperatorCalculator_9Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8]
					);
                case 10:
                    return new Multiply_OperatorCalculator_10Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9]
					);
                case 11:
                    return new Multiply_OperatorCalculator_11Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10]
					);
                case 12:
                    return new Multiply_OperatorCalculator_12Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10],
						itemCalculators[11]
					);
                case 13:
                    return new Multiply_OperatorCalculator_13Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10],
						itemCalculators[11],
						itemCalculators[12]
					);
                case 14:
                    return new Multiply_OperatorCalculator_14Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10],
						itemCalculators[11],
						itemCalculators[12],
						itemCalculators[13]
					);
                case 15:
                    return new Multiply_OperatorCalculator_15Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10],
						itemCalculators[11],
						itemCalculators[12],
						itemCalculators[13],
						itemCalculators[14]
					);
                case 16:
                    return new Multiply_OperatorCalculator_16Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5],
						itemCalculators[6],
						itemCalculators[7],
						itemCalculators[8],
						itemCalculators[9],
						itemCalculators[10],
						itemCalculators[11],
						itemCalculators[12],
						itemCalculators[13],
						itemCalculators[14],
						itemCalculators[15]
					);
                default:
                    return new Multiply_OperatorCalculator_VarArray(itemCalculators);
            }
        }
	}
}
