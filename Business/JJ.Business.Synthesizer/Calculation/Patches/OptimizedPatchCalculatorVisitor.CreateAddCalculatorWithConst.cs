

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
	    private OperatorCalculatorBase CreateAddCalculatorWithConst(double constValue, IList<OperatorCalculatorBase> varOperandCalculators)
        {
			if (varOperandCalculators.Count < 1) throw new LessThanException(() => varOperandCalculators.Count, 1);

            switch (varOperandCalculators.Count)
            {
                case 1:
                    return new Add_OperatorCalculator_1Const_1Var
					(
						constValue,
						varOperandCalculators[0]
					);
                case 2:
                    return new Add_OperatorCalculator_1Const_2Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1]
					);
                case 3:
                    return new Add_OperatorCalculator_1Const_3Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2]
					);
                case 4:
                    return new Add_OperatorCalculator_1Const_4Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3]
					);
                case 5:
                    return new Add_OperatorCalculator_1Const_5Var
					(
						constValue,
						varOperandCalculators[0],
						varOperandCalculators[1],
						varOperandCalculators[2],
						varOperandCalculators[3],
						varOperandCalculators[4]
					);
                case 6:
                    return new Add_OperatorCalculator_1Const_6Var
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
                    return new Add_OperatorCalculator_1Const_7Var
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
                    return new Add_OperatorCalculator_1Const_8Var
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
                    return new Add_OperatorCalculator_1Const_9Var
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
                    return new Add_OperatorCalculator_1Const_10Var
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
                    return new Add_OperatorCalculator_1Const_11Var
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
                    return new Add_OperatorCalculator_1Const_12Var
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
                    return new Add_OperatorCalculator_1Const_13Var
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
                    return new Add_OperatorCalculator_1Const_14Var
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
                    return new Add_OperatorCalculator_1Const_15Var
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
                case 16:
                    return new Add_OperatorCalculator_1Const_16Var
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
						varOperandCalculators[14],
						varOperandCalculators[15]
					);
                case 17:
                    return new Add_OperatorCalculator_1Const_17Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16]
					);
                case 18:
                    return new Add_OperatorCalculator_1Const_18Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17]
					);
                case 19:
                    return new Add_OperatorCalculator_1Const_19Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18]
					);
                case 20:
                    return new Add_OperatorCalculator_1Const_20Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19]
					);
                case 21:
                    return new Add_OperatorCalculator_1Const_21Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20]
					);
                case 22:
                    return new Add_OperatorCalculator_1Const_22Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21]
					);
                case 23:
                    return new Add_OperatorCalculator_1Const_23Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22]
					);
                case 24:
                    return new Add_OperatorCalculator_1Const_24Var
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
						varOperandCalculators[14],
						varOperandCalculators[15],
						varOperandCalculators[16],
						varOperandCalculators[17],
						varOperandCalculators[18],
						varOperandCalculators[19],
						varOperandCalculators[20],
						varOperandCalculators[21],
						varOperandCalculators[22],
						varOperandCalculators[23]
					);
                case 25:
                    return new Add_OperatorCalculator_1Const_25Var
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
						varOperandCalculators[24]
					);
                case 26:
                    return new Add_OperatorCalculator_1Const_26Var
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
						varOperandCalculators[25]
					);
                case 27:
                    return new Add_OperatorCalculator_1Const_27Var
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
						varOperandCalculators[26]
					);
                case 28:
                    return new Add_OperatorCalculator_1Const_28Var
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
						varOperandCalculators[27]
					);
                case 29:
                    return new Add_OperatorCalculator_1Const_29Var
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
						varOperandCalculators[28]
					);
                case 30:
                    return new Add_OperatorCalculator_1Const_30Var
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
						varOperandCalculators[29]
					);
                case 31:
                    return new Add_OperatorCalculator_1Const_31Var
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
						varOperandCalculators[30]
					);
                default:
                    return new Add_OperatorCalculator_WithConst_WithOperandArray(constValue, varOperandCalculators);
            }
        }
	}
}
