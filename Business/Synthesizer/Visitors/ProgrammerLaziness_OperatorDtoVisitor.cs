using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ProgrammerLaziness_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(SumFollower_OperatorDto_ConstSignal_VarSampleCount dto)
        {
            base.Visit_SumFollower_OperatorDto_ConstSignal_VarSampleCount(dto);

            var dto2 = new SumFollower_OperatorDto_AllVars
            {
                SignalOperatorDto = new Number_OperatorDto { Number = dto.Signal },
                SampleCountOperatorDto = dto.SampleCountOperatorDto,
                SliceLengthOperatorDto = new Number_OperatorDto_One()
            };
            
            // Dimension does not matter in case of ConstSignal.

            return dto2;
        }
    }
}
