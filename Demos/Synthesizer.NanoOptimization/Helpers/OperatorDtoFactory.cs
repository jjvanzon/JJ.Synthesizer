using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers
{
    internal static class OperatorDtoFactory
    {
        public static OperatorDto CreateOperatorDto_8Partials()
        {
            int partialCount = 8;

            IList<OperatorDto> partialOperatorDtos = new List<OperatorDto>(partialCount);

            for (int i = 0; i < partialCount; i++)
            {
                OperatorDto partialOperatorDto = CreateOperatorDto_SinglePartial();
                partialOperatorDtos.Add(partialOperatorDto);
            }

            OperatorDto operatorDto = new Add_OperatorDto(partialOperatorDtos);
            return operatorDto;
        }

        //public static OperatorDto CreateOperatorDto_8Partials_WithMultiple2VarAdds_InsteadOfSingle8VarAdd()
        //{
        //    OperatorDto operatorDto = CreateOperatorDto_SinglePartial();

        //    for (int i = 0; i < 6; i++)
        //    {
        //        OperatorDto nextPartial_OperatorDto = CreateOperatorDto_SinglePartial();

        //        operatorDto = new Add_OperatorDto(new OperatorDto(operatorDto), new OperatorDto(nextPartial_OperatorDto));
        //    }

        //    return operatorDto;
        //}

        public static OperatorDto CreateOperatorDto_SinglePartial()
        {
            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var dto = new Multiply_OperatorDto
            (
                new Shift_OperatorDto
                (
                    new Sine_OperatorDto
                    (
                        new VariableInput_OperatorDto(frequency)
                    ),
                    new Number_OperatorDto(phaseShift)
                ),
                new Number_OperatorDto(volume)
            );

            return dto;
        }
    }
}