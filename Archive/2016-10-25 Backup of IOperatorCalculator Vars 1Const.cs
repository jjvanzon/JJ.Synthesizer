


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{

		internal interface IOperatorCalculator_1Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
		}

		internal interface IOperatorCalculator_2Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
		}

		internal interface IOperatorCalculator_3Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
				IOperatorCalculator Calculator3 { get; set; }
		}

		internal interface IOperatorCalculator_4Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
				IOperatorCalculator Calculator3 { get; set; }
				IOperatorCalculator Calculator4 { get; set; }
		}

		internal interface IOperatorCalculator_5Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
				IOperatorCalculator Calculator3 { get; set; }
				IOperatorCalculator Calculator4 { get; set; }
				IOperatorCalculator Calculator5 { get; set; }
		}

		internal interface IOperatorCalculator_6Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
				IOperatorCalculator Calculator3 { get; set; }
				IOperatorCalculator Calculator4 { get; set; }
				IOperatorCalculator Calculator5 { get; set; }
				IOperatorCalculator Calculator6 { get; set; }
		}

		internal interface IOperatorCalculator_7Vars_1Const : IOperatorCalculator
		{
			double ConstValue { get; set; }
				IOperatorCalculator Calculator1 { get; set; }
				IOperatorCalculator Calculator2 { get; set; }
				IOperatorCalculator Calculator3 { get; set; }
				IOperatorCalculator Calculator4 { get; set; }
				IOperatorCalculator Calculator5 { get; set; }
				IOperatorCalculator Calculator6 { get; set; }
				IOperatorCalculator Calculator7 { get; set; }
		}

}
