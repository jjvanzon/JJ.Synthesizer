using System;
using System.Diagnostics;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Dto
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	internal class InputDto
	{
		[Obsolete("Use InputDtoFactory instead.")]
		public InputDto(bool isVar = false, IOperatorDto var = null)
		{
			IsVar = isVar;
			Var = var;
			VarOrConst = var;
		}

		[Obsolete("Use InputDtoFactory instead.")]
		public InputDto(
			bool isConst = false,
			bool isConstZero = false,
			bool isConstOne = false,
			bool isConstNonZero = false,
			bool isConstSpecialValue = false,
			double @const = 0)
		{
			IsConst = isConst;
			IsConstZero = isConstZero;
			IsConstOne = isConstOne;
			IsConstNonZero = isConstNonZero;
			IsConstSpecialValue = isConstSpecialValue;
			Const = @const;
			VarOrConst = new Number_OperatorDto(@const);
		}

		public bool IsVar { get; }
		public bool IsConst { get; }
		public bool IsConstZero { get; }
		public bool IsConstOne { get; }
		public bool IsConstNonZero { get; }
		/// <summary> Meaning it is NaN, PositiveInfinity or NegativeInfinity. </summary>
		public bool IsConstSpecialValue { get; }
		public double Const { get; }
		public IOperatorDto Var { get; }
		public IOperatorDto VarOrConst { get; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);

		public static implicit operator InputDto(double @const) => InputDtoFactory.CreateInputDto(@const);

		/// <summary>
		/// Unfortunately IOperatorDto cannot be the source of the conversion.
		/// It has to be OperatorDtoBase, because implicit C# operators will not take interfaces as the source.
		/// </summary>
		public static implicit operator InputDto(OperatorDtoBase var) => InputDtoFactory.CreateInputDto(var);

		public static implicit operator double (InputDto dto) => dto.Const;
	}
}