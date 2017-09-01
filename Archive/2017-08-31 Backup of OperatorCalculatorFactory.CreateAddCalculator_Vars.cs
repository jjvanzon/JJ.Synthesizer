

/*
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static partial class OperatorCalculatorFactory
    {
	    public static OperatorCalculatorBase CreateAddCalculator_Vars(IList<OperatorCalculatorBase> itemCalculators)
        {
			if (itemCalculators.Count < 2) throw new LessThanException(() => itemCalculators.Count, 2);

            switch (itemCalculators.Count)
            {
                case 2:
                    return new Add_OperatorCalculator_2Vars
					(
						itemCalculators[0],
						itemCalculators[1]
					);
                case 3:
                    return new Add_OperatorCalculator_3Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2]
					);
                case 4:
                    return new Add_OperatorCalculator_4Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3]
					);
                case 5:
                    return new Add_OperatorCalculator_5Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4]
					);
                case 6:
                    return new Add_OperatorCalculator_6Vars
					(
						itemCalculators[0],
						itemCalculators[1],
						itemCalculators[2],
						itemCalculators[3],
						itemCalculators[4],
						itemCalculators[5]
					);
                case 7:
                    return new Add_OperatorCalculator_7Vars
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
                    return new Add_OperatorCalculator_8Vars
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
                    return new Add_OperatorCalculator_9Vars
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
                    return new Add_OperatorCalculator_10Vars
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
                    return new Add_OperatorCalculator_11Vars
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
                    return new Add_OperatorCalculator_12Vars
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
                    return new Add_OperatorCalculator_13Vars
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
                    return new Add_OperatorCalculator_14Vars
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
                    return new Add_OperatorCalculator_15Vars
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
                    return new Add_OperatorCalculator_16Vars
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
                case 17:
                    return new Add_OperatorCalculator_17Vars
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
						itemCalculators[15],
						itemCalculators[16]
					);
                case 18:
                    return new Add_OperatorCalculator_18Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17]
					);
                case 19:
                    return new Add_OperatorCalculator_19Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18]
					);
                case 20:
                    return new Add_OperatorCalculator_20Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19]
					);
                case 21:
                    return new Add_OperatorCalculator_21Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20]
					);
                case 22:
                    return new Add_OperatorCalculator_22Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21]
					);
                case 23:
                    return new Add_OperatorCalculator_23Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22]
					);
                case 24:
                    return new Add_OperatorCalculator_24Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23]
					);
                case 25:
                    return new Add_OperatorCalculator_25Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24]
					);
                case 26:
                    return new Add_OperatorCalculator_26Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25]
					);
                case 27:
                    return new Add_OperatorCalculator_27Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26]
					);
                case 28:
                    return new Add_OperatorCalculator_28Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26],
						itemCalculators[27]
					);
                case 29:
                    return new Add_OperatorCalculator_29Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26],
						itemCalculators[27],
						itemCalculators[28]
					);
                case 30:
                    return new Add_OperatorCalculator_30Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26],
						itemCalculators[27],
						itemCalculators[28],
						itemCalculators[29]
					);
                case 31:
                    return new Add_OperatorCalculator_31Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26],
						itemCalculators[27],
						itemCalculators[28],
						itemCalculators[29],
						itemCalculators[30]
					);
                case 32:
                    return new Add_OperatorCalculator_32Vars
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
						itemCalculators[15],
						itemCalculators[16],
						itemCalculators[17],
						itemCalculators[18],
						itemCalculators[19],
						itemCalculators[20],
						itemCalculators[21],
						itemCalculators[22],
						itemCalculators[23],
						itemCalculators[24],
						itemCalculators[25],
						itemCalculators[26],
						itemCalculators[27],
						itemCalculators[28],
						itemCalculators[29],
						itemCalculators[30],
						itemCalculators[31]
					);
                default:
                    return new Add_OperatorCalculator_VarArray(itemCalculators);
            }
        }
	}
}
*/