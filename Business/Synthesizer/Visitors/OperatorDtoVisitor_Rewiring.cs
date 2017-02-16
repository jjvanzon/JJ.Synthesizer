using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_Rewiring : OperatorDtoVisitorBase_AfterMathSimplification
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

        protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line sourceInletsToDimensionOperatorDto)
        {
            var destInletsToDimensionOperatorDto = new InletsToDimension_OperatorDto_Stripe_LagBehind
            {
                ResampleInterpolationTypeEnum = sourceInletsToDimensionOperatorDto.ResampleInterpolationTypeEnum,
                Vars = sourceInletsToDimensionOperatorDto.Vars
            };
            DtoCloner.Clone_DimensionProperties(sourceInletsToDimensionOperatorDto, destInletsToDimensionOperatorDto);

            var destInterpolateOperatorDto = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate
            {
                SignalOperatorDto = destInletsToDimensionOperatorDto,
                SamplingRate = 1.0,
                ResampleInterpolationTypeEnum = sourceInletsToDimensionOperatorDto.ResampleInterpolationTypeEnum
            };
            DtoCloner.Clone_DimensionProperties(sourceInletsToDimensionOperatorDto, destInterpolateOperatorDto);

            return destInterpolateOperatorDto;
        }

        private static IOperatorDto Process_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto sourceInletsToDimensionOperatorDto, Interpolate_OperatorDto destInterpolateOperatorDto)
        {
            var destInletsToDimensionOperatorDto = new InletsToDimension_OperatorDto_Stripe_LagBehind
            {
                ResampleInterpolationTypeEnum = sourceInletsToDimensionOperatorDto.ResampleInterpolationTypeEnum,
                Vars = sourceInletsToDimensionOperatorDto.Vars
            };
            DtoCloner.Clone_DimensionProperties(sourceInletsToDimensionOperatorDto, destInletsToDimensionOperatorDto);

            destInterpolateOperatorDto.SignalOperatorDto = destInletsToDimensionOperatorDto;
            destInterpolateOperatorDto.SamplingRateOperatorDto = new Number_OperatorDto_One();
            destInterpolateOperatorDto.ResampleInterpolationTypeEnum = sourceInletsToDimensionOperatorDto.ResampleInterpolationTypeEnum;
            DtoCloner.Clone_DimensionProperties(sourceInletsToDimensionOperatorDto, destInterpolateOperatorDto);

            return destInterpolateOperatorDto;
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
        
        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_ConstRate(Random_OperatorDto_Line_LagBehind_ConstRate sourceRandomOperatorDto)
        {
            // HACK: Here a const sampling rate is replaced with a var again. Looks like we are nullifying an optimization here. What is the real solution?
            var destNumberOperatorDto = new Number_OperatorDto { Number = sourceRandomOperatorDto.Rate };

            var destRandomOperatorDto = new Random_OperatorDto_Stripe_LagBehind { RateOperatorDto = destNumberOperatorDto };
            DtoCloner.Clone_RandomOperatorProperties(sourceRandomOperatorDto, destRandomOperatorDto);

            var destInterpolateOperatorDto = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate
            {
                SignalOperatorDto = destRandomOperatorDto,
                SamplingRate = sourceRandomOperatorDto.Rate,
                ResampleInterpolationTypeEnum = sourceRandomOperatorDto.ResampleInterpolationTypeEnum
            };
            DtoCloner.Clone_DimensionProperties(sourceRandomOperatorDto, destInterpolateOperatorDto);

            return destInterpolateOperatorDto;
        }

        protected override IOperatorDto Visit_Random_OperatorDto_Line_LagBehind_VarRate(Random_OperatorDto_Line_LagBehind_VarRate dto)
        {
            return Process_Random_OperatorDto(dto, new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate());
        }

        private static IOperatorDto Process_Random_OperatorDto(Random_OperatorDto sourceRandomOperatorDto, Interpolate_OperatorDto destInterpolateOperatorDto)
        {
            var destRandomOperatorDto = new Random_OperatorDto_Stripe_LagBehind { RateOperatorDto = sourceRandomOperatorDto.RateOperatorDto };
            DtoCloner.Clone_RandomOperatorProperties(sourceRandomOperatorDto, destRandomOperatorDto);

            destInterpolateOperatorDto.SignalOperatorDto = destRandomOperatorDto;
            destInterpolateOperatorDto.SamplingRateOperatorDto = sourceRandomOperatorDto.RateOperatorDto;
            destInterpolateOperatorDto.ResampleInterpolationTypeEnum = sourceRandomOperatorDto.ResampleInterpolationTypeEnum;
            DtoCloner.Clone_DimensionProperties(sourceRandomOperatorDto, destInterpolateOperatorDto);

            return destInterpolateOperatorDto;
        }
    }
}
