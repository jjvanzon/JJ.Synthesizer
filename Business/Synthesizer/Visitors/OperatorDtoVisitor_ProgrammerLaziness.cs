using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_ProgrammerLaziness : OperatorDtoVisitorBase_AfterRewiring
    {
        public IOperatorDto Execute(IOperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);

            var dto2 = new SumFollower_OperatorDto_AllVars
            {
                SignalOperatorDto = new Number_OperatorDto { Number = dto.Signal },
                SampleCountOperatorDto = dto.SampleCountOperatorDto,
                SliceLengthOperatorDto = new Number_OperatorDto_One()
            };

            DtoCloner.Clone_OperatorBaseProperties(dto, dto2);

            // Dimension does not matter in case of ConstSignal.

            return dto2;
        }
    }
}
