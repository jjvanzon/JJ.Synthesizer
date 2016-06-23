

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
	    public static OperatorCalculatorBase CreateAddCalculatorOnlyVars(IList<OperatorCalculatorBase> operandCalculators)
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
                case 9:
                    return new Add_OperatorCalculator_9Vars
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
                    return new Add_OperatorCalculator_10Vars
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
                    return new Add_OperatorCalculator_11Vars
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
                    return new Add_OperatorCalculator_12Vars
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
                    return new Add_OperatorCalculator_13Vars
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
                    return new Add_OperatorCalculator_14Vars
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
                    return new Add_OperatorCalculator_15Vars
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
                    return new Add_OperatorCalculator_16Vars
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
                case 17:
                    return new Add_OperatorCalculator_17Vars
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
						operandCalculators[15],
						operandCalculators[16]
					);
                case 18:
                    return new Add_OperatorCalculator_18Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17]
					);
                case 19:
                    return new Add_OperatorCalculator_19Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18]
					);
                case 20:
                    return new Add_OperatorCalculator_20Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19]
					);
                case 21:
                    return new Add_OperatorCalculator_21Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20]
					);
                case 22:
                    return new Add_OperatorCalculator_22Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21]
					);
                case 23:
                    return new Add_OperatorCalculator_23Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22]
					);
                case 24:
                    return new Add_OperatorCalculator_24Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23]
					);
                case 25:
                    return new Add_OperatorCalculator_25Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24]
					);
                case 26:
                    return new Add_OperatorCalculator_26Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25]
					);
                case 27:
                    return new Add_OperatorCalculator_27Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26]
					);
                case 28:
                    return new Add_OperatorCalculator_28Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26],
						operandCalculators[27]
					);
                case 29:
                    return new Add_OperatorCalculator_29Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26],
						operandCalculators[27],
						operandCalculators[28]
					);
                case 30:
                    return new Add_OperatorCalculator_30Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26],
						operandCalculators[27],
						operandCalculators[28],
						operandCalculators[29]
					);
                case 31:
                    return new Add_OperatorCalculator_31Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26],
						operandCalculators[27],
						operandCalculators[28],
						operandCalculators[29],
						operandCalculators[30]
					);
                case 32:
                    return new Add_OperatorCalculator_32Vars
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
						operandCalculators[15],
						operandCalculators[16],
						operandCalculators[17],
						operandCalculators[18],
						operandCalculators[19],
						operandCalculators[20],
						operandCalculators[21],
						operandCalculators[22],
						operandCalculators[23],
						operandCalculators[24],
						operandCalculators[25],
						operandCalculators[26],
						operandCalculators[27],
						operandCalculators[28],
						operandCalculators[29],
						operandCalculators[30],
						operandCalculators[31]
					);
                default:
                    return new Add_OperatorCalculator_WithOperandArray(operandCalculators.ToArray());
            }
        }
	}
}
