using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class PreProcessingDtoVisitor
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new SpecializationDtoVisitor().Execute(dto);
            dto = new SimplificationDtoVisitor().Execute(dto);

            return dto;
        }
    }
}
