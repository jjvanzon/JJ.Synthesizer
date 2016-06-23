

using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateMultiplyCalculatorWithConst(double constValue, IList<OperatorCalculatorBase> varOperandCalculators)
        {
			if (varOperandCalculators.Count < 1) throw new LessThanException(() => varOperandCalculators.Count, 1);

            switch (varOperandCalculators.Count)
            {
                case 1:
                    return new Multiply_OperatorCalculator_1Const_1Var
					(
						constValue,
						varOperandCalculators[0]
					);
                case 2:
                    return new Multiply_OperatorCalculator_1Const_2Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1]
					);
                case 3:
                    return new Multiply_OperatorCalculator_1Const_3Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2]
					);
                case 4:
                    return new Multiply_OperatorCalculator_1Const_4Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3]
					);
                case 5:
                    return new Multiply_OperatorCalculator_1Const_5Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4]
					);
                case 6:
                    return new Multiply_OperatorCalculator_1Const_6Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5]
					);
                case 7:
                    return new Multiply_OperatorCalculator_1Const_7Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6]
					);
                case 8:
                    return new Multiply_OperatorCalculator_1Const_8Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7]
					);
                case 9:
                    return new Multiply_OperatorCalculator_1Const_9Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8]
					);
                case 10:
                    return new Multiply_OperatorCalculator_1Const_10Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9]
					);
                case 11:
                    return new Multiply_OperatorCalculator_1Const_11Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9],
						varOperandCalculators[10]
					);
                case 12:
                    return new Multiply_OperatorCalculator_1Const_12Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9],
						varOperandCalculators[10],
						varOperandCalculators[11]
					);
                case 13:
                    return new Multiply_OperatorCalculator_1Const_13Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9],
						varOperandCalculators[10],
						varOperandCalculators[11],
						varOperandCalculators[12]
					);
                case 14:
                    return new Multiply_OperatorCalculator_1Const_14Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9],
						varOperandCalculators[10],
						varOperandCalculators[11],
						varOperandCalculators[12],
						varOperandCalculators[13]
					);
                case 15:
                    return new Multiply_OperatorCalculator_1Const_15Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						varOperandCalculators[9],
						varOperandCalculators[10],
						varOperandCalculators[11],
						varOperandCalculators[12],
						varOperandCalculators[13],
						varOperandCalculators[14]
					);
                default:
                    return new Multiply_OperatorCalculator_WithOperandArray_WithConst(constValue, varOperandCalculators);
            }
        }
	}
}
