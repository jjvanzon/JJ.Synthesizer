

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithInheritance;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithInheritance
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateAddCalculator_Vars_1Const(IList<OperatorCalculatorBase> varOperandCalculators, double constValue)
        {
			if (varOperandCalculators.Count < 1) throw new LessThanException(() => varOperandCalculators.Count, 1);

            switch (varOperandCalculators.Count)
            {
                case 1:
                    return new Add_OperatorCalculator_1Vars_1Const
					(
						varOperandCalculators[0],
						constValue
					);
                case 2:
                    return new Add_OperatorCalculator_2Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						constValue
					);
                case 3:
                    return new Add_OperatorCalculator_3Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						constValue
					);
                case 4:
                    return new Add_OperatorCalculator_4Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						constValue
					);
                case 5:
                    return new Add_OperatorCalculator_5Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						constValue
					);
                case 6:
                    return new Add_OperatorCalculator_6Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						constValue
					);
                case 7:
                    return new Add_OperatorCalculator_7Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						constValue
					);
                default:
                    return new Add_OperatorCalculator_VarArray_1Const(varOperandCalculators, constValue);
            }
        }
	}
}
