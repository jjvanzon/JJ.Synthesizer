//using JJ.Business.Synthesizer.Dto.Operators;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Visitors
//{
//	internal class OperatorDtoVisitor_ProgrammerLaziness : OperatorDtoVisitorBase_AfterRewriting
//	{
//		public IOperatorDto Execute(IOperatorDto dto)
//		{
//			return Visit_OperatorDto_Polymorphic(dto);
//		}

//		protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//		{
//			return WithAlreadyProcessedCheck(dto, () => base.Visit_OperatorDto_Polymorphic(dto));
//		}

//		protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
//		{
//			// TODO: This could be replaced by a math simplification where you replace this noce by a multiplication
//			// with a const and a var.

//			// So
//			// var newNode = Mutliply(signal, sampleCount slicelength)

//			base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);

//			var dto2 = new SumFollower_OperatorDto_AllVars();
//			DtoCloner.CloneProperties(dto, dto2);

//			dto2.Signal = dto.Signal;
//			dto2.SampleCount = dto.SampleCount;
//			dto2.SliceLength = dto.SliceLength;

//			return dto2;
//		}
//	}
//}
