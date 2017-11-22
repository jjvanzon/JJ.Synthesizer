using System.Collections.Generic;

namespace JJ.Business.SynthesizerPrototype.Dto
{
	public abstract class OperatorDtoBase_Vars_1Const : OperatorDtoBase
	{
		public IList<IOperatorDto> Vars { get; set; }
		public double ConstValue { get; set; }

		public override IList<IOperatorDto> InputOperatorDtos
		{
			get => Vars;
			set => Vars = value;
		}
	}
}
