﻿

/*

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
	    private OperatorCalculatorBase CreateAddCalculator_WithThreeOrMoreOperands(IList<OperatorCalculatorBase> operandCalculators)
        {
			if (operandCalculators.Count < 3) throw new LessThanException(() => operandCalculators.Count, 3);

            switch (operandCalculators.Count)
            {
                case 3:
                    return new Add_OperatorCalculator3
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2]					);
                case 4:
                    return new Add_OperatorCalculator4
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3]					);
                case 5:
                    return new Add_OperatorCalculator5
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4]					);
                case 6:
                    return new Add_OperatorCalculator6
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5]					);
                case 7:
                    return new Add_OperatorCalculator7
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6]					);
                case 8:
                    return new Add_OperatorCalculator8
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7]					);
                case 9:
                    return new Add_OperatorCalculator9
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8]					);
                case 10:
                    return new Add_OperatorCalculator10
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9]					);
                case 11:
                    return new Add_OperatorCalculator11
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10]					);
                case 12:
                    return new Add_OperatorCalculator12
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11]					);
                case 13:
                    return new Add_OperatorCalculator13
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12]					);
                case 14:
                    return new Add_OperatorCalculator14
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13]					);
                case 15:
                    return new Add_OperatorCalculator15
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14]					);
                case 16:
                    return new Add_OperatorCalculator16
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15]					);
                case 17:
                    return new Add_OperatorCalculator17
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16]					);
                case 18:
                    return new Add_OperatorCalculator18
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17]					);
                case 19:
                    return new Add_OperatorCalculator19
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18]					);
                case 20:
                    return new Add_OperatorCalculator20
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19]					);
                case 21:
                    return new Add_OperatorCalculator21
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20]					);
                case 22:
                    return new Add_OperatorCalculator22
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21]					);
                case 23:
                    return new Add_OperatorCalculator23
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22]					);
                case 24:
                    return new Add_OperatorCalculator24
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23]					);
                case 25:
                    return new Add_OperatorCalculator25
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24]					);
                case 26:
                    return new Add_OperatorCalculator26
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25]					);
                case 27:
                    return new Add_OperatorCalculator27
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26]					);
                case 28:
                    return new Add_OperatorCalculator28
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26],						operandCalculators[27]					);
                case 29:
                    return new Add_OperatorCalculator29
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26],						operandCalculators[27],						operandCalculators[28]					);
                case 30:
                    return new Add_OperatorCalculator30
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26],						operandCalculators[27],						operandCalculators[28],						operandCalculators[29]					);
                case 31:
                    return new Add_OperatorCalculator31
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26],						operandCalculators[27],						operandCalculators[28],						operandCalculators[29],						operandCalculators[30]					);
                case 32:
                    return new Add_OperatorCalculator32
					(
						operandCalculators[0],						operandCalculators[1],						operandCalculators[2],						operandCalculators[3],						operandCalculators[4],						operandCalculators[5],						operandCalculators[6],						operandCalculators[7],						operandCalculators[8],						operandCalculators[9],						operandCalculators[10],						operandCalculators[11],						operandCalculators[12],						operandCalculators[13],						operandCalculators[14],						operandCalculators[15],						operandCalculators[16],						operandCalculators[17],						operandCalculators[18],						operandCalculators[19],						operandCalculators[20],						operandCalculators[21],						operandCalculators[22],						operandCalculators[23],						operandCalculators[24],						operandCalculators[25],						operandCalculators[26],						operandCalculators[27],						operandCalculators[28],						operandCalculators[29],						operandCalculators[30],						operandCalculators[31]					);
                default:
                    return new Add_OperatorCalculator_WithOperandArray(operandCalculators.ToArray());
            }
        }
	}
}

*/