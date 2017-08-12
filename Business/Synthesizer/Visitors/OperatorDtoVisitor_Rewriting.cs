using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_Rewriting : OperatorDtoVisitorBase_AfterMathSimplification
    {
        // Execute

        public IOperatorDto Execute(IOperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        // InletsToDimension

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicAbruptSlope(InletsToDimension_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_CubicAbruptSlope());
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicEquidistant(InletsToDimension_OperatorDto_CubicEquidistant dto)
        {
            return Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_CubicEquidistant());
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind(InletsToDimension_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind());
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto)
        {
            return Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_Hermite_LagBehind());
        }

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line sourceDto)
        {
            var destDto1 = new InletsToDimension_OperatorDto_Stripe_LagBehind();
            DtoCloner.CloneProperties(sourceDto, destDto1);
            destDto1.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;

            var destDto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate();
            DtoCloner.CloneProperties(sourceDto, destDto2);
            destDto2.Signal = destDto1;
            destDto2.SamplingRate = 1.0;
            destDto2.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;

            return destDto2;
        }

        private static IOperatorDto Process_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto sourceDto, Interpolate_OperatorDto destDto)
        {
            var intermediateDto = new InletsToDimension_OperatorDto_Stripe_LagBehind();
            DtoCloner.CloneProperties(sourceDto, intermediateDto);
            intermediateDto.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;
            intermediateDto.Vars = sourceDto.Vars;

            DtoCloner.CloneProperties(sourceDto, destDto);
            destDto.Signal = intermediateDto;
            destDto.SamplingRate = 1.0;
            destDto.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;

            return destDto;
        }

        // Random

        protected override IOperatorDto Visit_Random_OperatorDto_CubicAbruptSlope(Random_OperatorDto_CubicAbruptSlope dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_CubicAbruptSlope());
        }

        protected override IOperatorDto Visit_Random_OperatorDto_CubicEquidistant(Random_OperatorDto_CubicEquidistant dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_CubicEquidistant());
        }

        protected override IOperatorDto Visit_Random_OperatorDto_CubicSmoothSlope_LagBehind(Random_OperatorDto_CubicSmoothSlope_LagBehind dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind());
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Hermite_LagBehind(Random_OperatorDto_Hermite_LagBehind dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_Hermite_LagBehind());
        }
        
        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_ConstRate(Random_OperatorDto_Line_LagBehind_ConstRate sourceDto)
        {
            var destDto1 = new Random_OperatorDto_Stripe_LagBehind();
            DtoCloner.CloneProperties(sourceDto, destDto1);

            var destDto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate();
            DtoCloner.CloneProperties(sourceDto, destDto2);
            destDto2.Signal = destDto1;
            destDto2.SamplingRate = sourceDto.Rate;
            destDto2.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;

            return destDto2;
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_VarRate(Random_OperatorDto_Line_LagBehind_VarRate dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate());
        }

        private static IOperatorDto Process_Random_OperatorDto(Random_OperatorDto sourceDto, Interpolate_OperatorDto destDto)
        {
            var intermediateDto = new Random_OperatorDto_Stripe_LagBehind();
            DtoCloner.CloneProperties(sourceDto, intermediateDto);

            DtoCloner.CloneProperties(sourceDto, destDto);
            destDto.Signal = intermediateDto;
            destDto.SamplingRate = sourceDto.Rate;
            destDto.ResampleInterpolationTypeEnum = sourceDto.ResampleInterpolationTypeEnum;

            return destDto;
        }
    }
}
