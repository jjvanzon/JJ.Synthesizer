using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Visitors;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoVisitor_CodeGenerationSimplification : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        public IOperatorDto Execute(IOperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_NoOriginShifting(Reverse_OperatorDto_ConstFactor_NoOriginShifting dto)
        {
            double negativeFactor = -dto.Factor.Const;

            var dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Factor = InputDtoFactory.CreateInputDto(negativeFactor);

            return dto2;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            double negativeFactor = -dto.Factor.Const;

            var dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting();
            DtoCloner.CloneProperties(dto, dto2);

            dto2.Factor = InputDtoFactory.CreateInputDto(negativeFactor);


            return dto2;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking sourceDto)
        {
            var negativeDto = new Negative_OperatorDto_VarNumber
            {
                Number = sourceDto.Signal
            };

            var destDto = new Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin();
            DtoCloner.CloneProperties(sourceDto, destDto);

            destDto.Signal = InputDtoFactory.CreateInputDto(negativeDto);
            destDto.Factor = sourceDto.Factor;

            return destDto;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            var negativeDto = new Negative_OperatorDto_VarNumber
            {
                Number = dto.Signal
            };

            var dto3 = new Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking();
            DtoCloner.CloneProperties(dto, dto3);

            dto3.Signal = InputDtoFactory.CreateInputDto(negativeDto);
            dto3.Factor = dto.Factor;

            return dto3;
        }
    }
}
