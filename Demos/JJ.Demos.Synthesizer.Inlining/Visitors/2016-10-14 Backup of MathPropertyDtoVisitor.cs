//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Demos.Synthesizer.Inlining.Dto;
//using JJ.Demos.Synthesizer.Inlining.Helpers;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Demos.Synthesizer.Inlining.Visitors
//{
//    internal class MathPropertyDtoVisitor : OperatorDtoVisitorBase
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Execute(OperatorDto operatorDto)
//        {
//            if (operatorDto == null) throw new NullException(() => operatorDto);

//            Visit_OperatorDto_Polymorphic(operatorDto);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override InletDto VisitInletDto(InletDto inletDto)
//        {
//            var number_OperatorDto = inletDto.InputOperatorDto as Number_OperatorDto;

//            MathPropertiesDto mathematicalPropertiesDto = MathPropertiesHelper.GetMathematicaPropertiesDto(inletDto);

//            return base.VisitInletDto(inletDto);
//        }
//    }
//}
