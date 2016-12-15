

using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithInheritance;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers.WithInheritance
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
                case 8:
                    return new Add_OperatorCalculator_8Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						
						constValue
					);
                case 9:
                    return new Add_OperatorCalculator_9Vars_1Const
					(
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4],
						varOperandCalculators[5],
						varOperandCalculators[6],
						varOperandCalculators[7],
						varOperandCalculators[8],
						
						constValue
					);
                case 10:
                    return new Add_OperatorCalculator_10Vars_1Const
					(
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
						
						constValue
					);
                case 11:
                    return new Add_OperatorCalculator_11Vars_1Const
					(
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
						
						constValue
					);
                case 12:
                    return new Add_OperatorCalculator_12Vars_1Const
					(
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
						
						constValue
					);
                case 13:
                    return new Add_OperatorCalculator_13Vars_1Const
					(
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
						
						constValue
					);
                case 14:
                    return new Add_OperatorCalculator_14Vars_1Const
					(
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
						
						constValue
					);
                case 15:
                    return new Add_OperatorCalculator_15Vars_1Const
					(
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
						varOperandCalculators[14],
						
						constValue
					);
                case 16:
                    return new Add_OperatorCalculator_16Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						
						constValue
					);
                case 17:
                    return new Add_OperatorCalculator_17Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						
						constValue
					);
                case 18:
                    return new Add_OperatorCalculator_18Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						
						constValue
					);
                case 19:
                    return new Add_OperatorCalculator_19Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						
						constValue
					);
                case 20:
                    return new Add_OperatorCalculator_20Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						
						constValue
					);
                case 21:
                    return new Add_OperatorCalculator_21Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						
						constValue
					);
                case 22:
                    return new Add_OperatorCalculator_22Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						
						constValue
					);
                case 23:
                    return new Add_OperatorCalculator_23Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						
						constValue
					);
                case 24:
                    return new Add_OperatorCalculator_24Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						
						constValue
					);
                case 25:
                    return new Add_OperatorCalculator_25Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						
						constValue
					);
                case 26:
                    return new Add_OperatorCalculator_26Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						
						constValue
					);
                case 27:
                    return new Add_OperatorCalculator_27Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						varOperandCalculators[26],
						
						constValue
					);
                case 28:
                    return new Add_OperatorCalculator_28Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						varOperandCalculators[26],
						varOperandCalculators[27],
						
						constValue
					);
                case 29:
                    return new Add_OperatorCalculator_29Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						varOperandCalculators[26],
						varOperandCalculators[27],
						varOperandCalculators[28],
						
						constValue
					);
                case 30:
                    return new Add_OperatorCalculator_30Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						varOperandCalculators[26],
						varOperandCalculators[27],
						varOperandCalculators[28],
						varOperandCalculators[29],
						
						constValue
					);
                case 31:
                    return new Add_OperatorCalculator_31Vars_1Const
					(
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23],
						varOperandCalculators[24],
						varOperandCalculators[25],
						varOperandCalculators[26],
						varOperandCalculators[27],
						varOperandCalculators[28],
						varOperandCalculators[29],
						varOperandCalculators[30],
						
						constValue
					);
                default:
                    return new Add_OperatorCalculator_VarArray_1Const(varOperandCalculators, constValue);
            }
        }
	}
}
