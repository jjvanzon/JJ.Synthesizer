

using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal partial class OptimizedPatchCalculatorVisitor
    {
	    private OperatorCalculatorBase CreateMultiplyCalculator(IList<OperatorCalculatorBase> operandCalculators)
        {
			if (operandCalculators.Count < 2) throw new LessThanException(() => operandCalculators.Count, 2);

            switch (operandCalculators.Count)
            {
                case 2:
                    return new Multiply_OperatorCalculator2
					(
						operandCalculators[0],
						operandCalculators[1]
					);
                case 3:
                    return new Multiply_OperatorCalculator3
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2]
					);
                case 4:
                    return new Multiply_OperatorCalculator4
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3]
					);
                case 5:
                    return new Multiply_OperatorCalculator5
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4]
					);
                case 6:
                    return new Multiply_OperatorCalculator6
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5]
					);
                case 7:
                    return new Multiply_OperatorCalculator7
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
                    return new Multiply_OperatorCalculator8
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
                    return new Multiply_OperatorCalculator9
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
                    return new Multiply_OperatorCalculator10
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
                    return new Multiply_OperatorCalculator11
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
                    return new Multiply_OperatorCalculator12
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
                    return new Multiply_OperatorCalculator13
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
                    return new Multiply_OperatorCalculator14
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
                    return new Multiply_OperatorCalculator15
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
                    return new Multiply_OperatorCalculator16
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
                    return new Multiply_OperatorCalculator_WithOperandArray(operandCalculators.ToArray());
            }
        }
	}
}
