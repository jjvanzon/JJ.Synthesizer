

using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal partial class OptimizedPatchCalculatorVisitor
    {
	    protected override void VisitAdder(Operator op)
        {
            OperatorCalculatorBase calculator;

            List<OperatorCalculatorBase> operandCalculators = new List<OperatorCalculatorBase>();

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                OperatorCalculatorBase operandCalculator =  _stack.Pop();

                if (operandCalculator != null)
                {
                    operandCalculators.Add(operandCalculator);
                }
            }

            switch (operandCalculators.Count)
            {
                case 0:
                    calculator = new Zero_OperatorCalculator();
                    break;

                case 1:
                    calculator = operandCalculators[0];
                    break;

                case 2:
                    calculator = new Add_OperatorCalculator(operandCalculators[0], operandCalculators[1]);
                    break;

                case 3:
                    calculator = new Adder_OperatorCalculator3
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2]
					);
                    break;

                case 4:
                    calculator = new Adder_OperatorCalculator4
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3]
					);
                    break;

                case 5:
                    calculator = new Adder_OperatorCalculator5
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4]
					);
                    break;

                case 6:
                    calculator = new Adder_OperatorCalculator6
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5]
					);
                    break;

                case 7:
                    calculator = new Adder_OperatorCalculator7
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4],
						operandCalculators[5],
						operandCalculators[6]
					);
                    break;

                case 8:
                    calculator = new Adder_OperatorCalculator8
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
                    break;

                case 9:
                    calculator = new Adder_OperatorCalculator9
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
                    break;

                case 10:
                    calculator = new Adder_OperatorCalculator10
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
                    break;

                case 11:
                    calculator = new Adder_OperatorCalculator11
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
                    break;

                case 12:
                    calculator = new Adder_OperatorCalculator12
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
                    break;

                case 13:
                    calculator = new Adder_OperatorCalculator13
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
                    break;

                case 14:
                    calculator = new Adder_OperatorCalculator14
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
                    break;

                case 15:
                    calculator = new Adder_OperatorCalculator15
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
                    break;

                case 16:
                    calculator = new Adder_OperatorCalculator16
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
                    break;

                case 17:
                    calculator = new Adder_OperatorCalculator17
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
                    break;

                case 18:
                    calculator = new Adder_OperatorCalculator18
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
                    break;

                case 19:
                    calculator = new Adder_OperatorCalculator19
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
                    break;

                case 20:
                    calculator = new Adder_OperatorCalculator20
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
                    break;

                case 21:
                    calculator = new Adder_OperatorCalculator21
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
                    break;

                case 22:
                    calculator = new Adder_OperatorCalculator22
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
                    break;

                case 23:
                    calculator = new Adder_OperatorCalculator23
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
                    break;

                case 24:
                    calculator = new Adder_OperatorCalculator24
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
                    break;

                case 25:
                    calculator = new Adder_OperatorCalculator25
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
                    break;

                case 26:
                    calculator = new Adder_OperatorCalculator26
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
                    break;

                case 27:
                    calculator = new Adder_OperatorCalculator27
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
                    break;

                case 28:
                    calculator = new Adder_OperatorCalculator28
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
                    break;

                case 29:
                    calculator = new Adder_OperatorCalculator29
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
                    break;

                case 30:
                    calculator = new Adder_OperatorCalculator30
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
                    break;

                case 31:
                    calculator = new Adder_OperatorCalculator31
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
                    break;

                case 32:
                    calculator = new Adder_OperatorCalculator32
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
                    break;


                default:
                    calculator = new Adder_OperatorCalculator(operandCalculators.ToArray());
                    break;
            }

            _stack.Push(calculator);
        }
	}
}
