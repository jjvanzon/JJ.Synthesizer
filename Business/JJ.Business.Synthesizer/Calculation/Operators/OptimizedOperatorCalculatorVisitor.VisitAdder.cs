

using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Calculation.Operators.Entities;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal partial class OptimizedOperatorCalculatorVisitor
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
                    calculator = new Zero_Calculator();
                    break;

                case 1:
                    calculator = operandCalculators[0];
                    break;

                case 2:
                    calculator = new Add_Calculator(operandCalculators[0], operandCalculators[1]);
                    break;

                case 3:
                    calculator = new AdderCalculator3
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2]
					);
                    break;

                case 4:
                    calculator = new AdderCalculator4
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3]
					);
                    break;

                case 5:
                    calculator = new AdderCalculator5
					(
						operandCalculators[0],
						operandCalculators[1],
						operandCalculators[2],
						operandCalculators[3],
						operandCalculators[4]
					);
                    break;

                case 6:
                    calculator = new AdderCalculator6
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
                    calculator = new AdderCalculator7
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
                    calculator = new AdderCalculator8
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
                    calculator = new AdderCalculator9
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
                    calculator = new AdderCalculator10
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
                    calculator = new AdderCalculator11
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
                    calculator = new AdderCalculator12
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
                    calculator = new AdderCalculator13
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
                    calculator = new AdderCalculator14
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
                    calculator = new AdderCalculator15
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
                    calculator = new AdderCalculator16
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
                    calculator = new AdderCalculator17
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
                    calculator = new AdderCalculator18
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
                    calculator = new AdderCalculator19
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
                    calculator = new AdderCalculator20
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
                    calculator = new AdderCalculator21
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
                    calculator = new AdderCalculator22
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
                    calculator = new AdderCalculator23
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
                    calculator = new AdderCalculator24
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
                    calculator = new AdderCalculator25
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
                    calculator = new AdderCalculator26
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
                    calculator = new AdderCalculator27
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
                    calculator = new AdderCalculator28
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
                    calculator = new AdderCalculator29
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
                    calculator = new AdderCalculator30
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
                    calculator = new AdderCalculator31
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
                    calculator = new AdderCalculator32
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
                    calculator = new Adder_Calculator(operandCalculators.ToArray());
                    break;
            }

            _stack.Push(calculator);
        }
	}
}
