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
            double negativeFactor = -dto.Factor;

            var dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin
            {
                SignalOperatorDto = dto.SignalOperatorDto,
                Factor = negativeFactor,
                DimensionStackLevel = dto.DimensionStackLevel
            };

            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            double negativeFactor = -dto.Factor;

            var dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting
            {
                SignalOperatorDto = dto.SignalOperatorDto,
                Factor = negativeFactor,
                DimensionStackLevel = dto.DimensionStackLevel
            };

            DtoCloner.Clone_WithDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking dto)
        {
            var negativeDto = new Negative_OperatorDto_VarNumber
            {
                NumberOperatorDto = dto.SignalOperatorDto
            };

            var dto3 = new Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin
            {
                SignalOperatorDto = negativeDto,
                FactorOperatorDto = dto.FactorOperatorDto,
                DimensionStackLevel = dto.DimensionStackLevel
            };

            DtoCloner.Clone_WithDimensionProperties(dto, dto3);

            return dto3;
        }

        protected override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            var negativeDto = new Negative_OperatorDto_VarNumber
            {
                NumberOperatorDto = dto.SignalOperatorDto
            };

            var dto3 = new Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking
            {
                SignalOperatorDto = negativeDto,
                FactorOperatorDto = dto.FactorOperatorDto,
                DimensionStackLevel = dto.DimensionStackLevel
            };

            DtoCloner.Clone_WithDimensionProperties(dto, dto3);

            return dto3;
        }
    }
}
