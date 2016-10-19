using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class OperatorDtoFactory
    {
        public static OperatorDto CreateOperatorDto_SinglePartial()
        {
            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var dto = new Multiply_OperatorDto
            (
                new InletDto
                (
                    new Shift_OperatorDto
                    (
                        new InletDto
                        (
                            new Sine_OperatorDto
                            (
                                new InletDto
                                (
                                    new VariableInput_OperatorDto(frequency)
                                )
                            )
                        ),
                        new InletDto
                        (
                            new Number_OperatorDto(phaseShift)
                        )
                    )
                ),
                new InletDto
                {
                    InputOperatorDto = new Number_OperatorDto(volume)
                }
            );

            return dto;
        }
    }
}