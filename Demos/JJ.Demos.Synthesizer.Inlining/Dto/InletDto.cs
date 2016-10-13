using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Dto
{
    internal class InletDto
    {
        public InletDto()
        { }

        public InletDto(OperatorDto inputOperatorDto)
        {
            InputOperatorDto = inputOperatorDto;
        }

        public bool IsConst { get; set; }
        public bool IsConstZero { get; set; }
        public bool IsConstOne { get; set; }
        public bool IsConstSpecialValue { get; set; }
        public double? Value { get; set; }

        public OperatorDto InputOperatorDto { get; set; }
    }
}
