

/*
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateMultiplyCalculator_Vars_1Const(IList<OperatorCalculatorBase> varItemCalculators, double constValue)
        {
			if (varItemCalculators.Count < 1) throw new LessThanException(() => varItemCalculators.Count, 1);

            switch (varItemCalculators.Count)
            {
                case 1:
                    return new Multiply_OperatorCalculator_1Vars_1Const
					(
						varItemCalculators[0],
						constValue
					);
                case 2:
                    return new Multiply_OperatorCalculator_2Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						constValue
					);
                case 3:
                    return new Multiply_OperatorCalculator_3Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						constValue
					);
                case 4:
                    return new Multiply_OperatorCalculator_4Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						constValue
					);
                case 5:
                    return new Multiply_OperatorCalculator_5Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						constValue
					);
                case 6:
                    return new Multiply_OperatorCalculator_6Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						constValue
					);
                case 7:
                    return new Multiply_OperatorCalculator_7Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						constValue
					);
                case 8:
                    return new Multiply_OperatorCalculator_8Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						constValue
					);
                case 9:
                    return new Multiply_OperatorCalculator_9Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						constValue
					);
                case 10:
                    return new Multiply_OperatorCalculator_10Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						constValue
					);
                case 11:
                    return new Multiply_OperatorCalculator_11Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						varItemCalculators[10],
						constValue
					);
                case 12:
                    return new Multiply_OperatorCalculator_12Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						varItemCalculators[10],
						varItemCalculators[11],
						constValue
					);
                case 13:
                    return new Multiply_OperatorCalculator_13Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						varItemCalculators[10],
						varItemCalculators[11],
						varItemCalculators[12],
						constValue
					);
                case 14:
                    return new Multiply_OperatorCalculator_14Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						varItemCalculators[10],
						varItemCalculators[11],
						varItemCalculators[12],
						varItemCalculators[13],
						constValue
					);
                case 15:
                    return new Multiply_OperatorCalculator_15Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						varItemCalculators[5],
						varItemCalculators[6],
						varItemCalculators[7],
						varItemCalculators[8],
						varItemCalculators[9],
						varItemCalculators[10],
						varItemCalculators[11],
						varItemCalculators[12],
						varItemCalculators[13],
						varItemCalculators[14],
						constValue
					);
                default:
                    return new Multiply_OperatorCalculator_VarArray_1Const(varItemCalculators, constValue);
            }
        }
	}
}
*/