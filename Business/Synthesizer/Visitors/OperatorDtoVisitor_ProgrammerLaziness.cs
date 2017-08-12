using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_ProgrammerLaziness : OperatorDtoVisitorBase_AfterRewriting
    {
        public IOperatorDto Execute(IOperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);

            var dto2 = new SumFollower_OperatorDto_SoundVarOrConst_OtherInputsVar();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Signal = dto.Signal;
            dto2.SampleCount = dto.SampleCount;
            dto2.SliceLength = InputDtoFactory.CreateInputDto(1);

            return dto2;
        }
    }
}
