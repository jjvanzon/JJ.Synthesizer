

/*
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateAddCalculator_Vars_1Const(IList<OperatorCalculatorBase> varItemCalculators, double constValue)
        {
			if (varItemCalculators.Count < 1) throw new LessThanException(() => varItemCalculators.Count, 1);

            switch (varItemCalculators.Count)
            {
                case 1:
                    return new Add_OperatorCalculator_1Vars_1Const
					(
						varItemCalculators[0],
						constValue
					);
                case 2:
                    return new Add_OperatorCalculator_2Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						constValue
					);
                case 3:
                    return new Add_OperatorCalculator_3Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						constValue
					);
                case 4:
                    return new Add_OperatorCalculator_4Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						constValue
					);
                case 5:
                    return new Add_OperatorCalculator_5Vars_1Const
					(
						varItemCalculators[0],
						varItemCalculators[1],
						varItemCalculators[2],
						varItemCalculators[3],
						varItemCalculators[4],
						constValue
					);
                case 6:
                    return new Add_OperatorCalculator_6Vars_1Const
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
                    return new Add_OperatorCalculator_7Vars_1Const
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
                    return new Add_OperatorCalculator_8Vars_1Const
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
                    return new Add_OperatorCalculator_9Vars_1Const
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
                    return new Add_OperatorCalculator_10Vars_1Const
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
                    return new Add_OperatorCalculator_11Vars_1Const
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
                    return new Add_OperatorCalculator_12Vars_1Const
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
                    return new Add_OperatorCalculator_13Vars_1Const
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
                    return new Add_OperatorCalculator_14Vars_1Const
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
                    return new Add_OperatorCalculator_15Vars_1Const
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
                case 16:
                    return new Add_OperatorCalculator_16Vars_1Const
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
						varItemCalculators[15],
						constValue
					);
                case 17:
                    return new Add_OperatorCalculator_17Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						constValue
					);
                case 18:
                    return new Add_OperatorCalculator_18Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						constValue
					);
                case 19:
                    return new Add_OperatorCalculator_19Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						constValue
					);
                case 20:
                    return new Add_OperatorCalculator_20Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						constValue
					);
                case 21:
                    return new Add_OperatorCalculator_21Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						constValue
					);
                case 22:
                    return new Add_OperatorCalculator_22Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						constValue
					);
                case 23:
                    return new Add_OperatorCalculator_23Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						constValue
					);
                case 24:
                    return new Add_OperatorCalculator_24Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						constValue
					);
                case 25:
                    return new Add_OperatorCalculator_25Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						constValue
					);
                case 26:
                    return new Add_OperatorCalculator_26Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						constValue
					);
                case 27:
                    return new Add_OperatorCalculator_27Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						varItemCalculators[26],
						constValue
					);
                case 28:
                    return new Add_OperatorCalculator_28Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						varItemCalculators[26],
						varItemCalculators[27],
						constValue
					);
                case 29:
                    return new Add_OperatorCalculator_29Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						varItemCalculators[26],
						varItemCalculators[27],
						varItemCalculators[28],
						constValue
					);
                case 30:
                    return new Add_OperatorCalculator_30Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						varItemCalculators[26],
						varItemCalculators[27],
						varItemCalculators[28],
						varItemCalculators[29],
						constValue
					);
                case 31:
                    return new Add_OperatorCalculator_31Vars_1Const
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
						varItemCalculators[15],
						varItemCalculators[16],
						varItemCalculators[17],
						varItemCalculators[18],
						varItemCalculators[19],
						varItemCalculators[20],
						varItemCalculators[21],
						varItemCalculators[22],
						varItemCalculators[23],
						varItemCalculators[24],
						varItemCalculators[25],
						varItemCalculators[26],
						varItemCalculators[27],
						varItemCalculators[28],
						varItemCalculators[29],
						varItemCalculators[30],
						constValue
					);
                default:
                    return new Add_OperatorCalculator_VarArray_1Const(varItemCalculators, constValue);
            }
        }
	}
}
*/