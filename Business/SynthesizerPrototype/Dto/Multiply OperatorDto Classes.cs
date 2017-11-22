using JJ.Business.SynthesizerPrototype.Helpers;

namespace JJ.Business.SynthesizerPrototype.Dto
{
	/// <summary>
	/// Multiply could have variable inlet count. The only reason it is implemented as a 2-operand operator
	/// is to also have a normal 2-operand operator in our prototype.
	/// </summary>
	public class Multiply_OperatorDto : OperatorDtoBase_VarA_VarB
	{
		public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);
	}

	public class Multiply_OperatorDto_ConstA_ConstB : OperatorDtoBase_ConstA_ConstB
	{
		public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);
	}

	public class Multiply_OperatorDto_ConstA_VarB : OperatorDtoBase_ConstA_VarB
	{
		public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);
	}

	public class Multiply_OperatorDto_VarA_ConstB : OperatorDtoBase_VarA_ConstB
	{
		public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);
	}

	public class Multiply_OperatorDto_VarA_VarB : OperatorDtoBase_VarA_VarB
	{
		public override string OperatorTypeName => nameof(OperatorTypeEnum.Multiply);
	}
}
