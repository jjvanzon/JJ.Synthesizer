using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal abstract class OperatorDtoVisitorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_OperatorDto_Polymorphic(OperatorDto operatorDto)
        {
            var add_OperatorDto = operatorDto as Add_OperatorDto;
            if (add_OperatorDto != null)
            {
                Visit_Add_OperatorDto_ConcreteOrPolymorphic(add_OperatorDto);
            }

            var multiply_OperatorDto = operatorDto as Multiply_OperatorDto;
            if (multiply_OperatorDto != null)
            {
                Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(multiply_OperatorDto);
            }

            var number_OperatorDto = operatorDto as Number_OperatorDto;
            if (number_OperatorDto != null)
            {
                Visit_Number_OperatorDto(number_OperatorDto);
            }
        }

        // Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_ConcreteOrPolymorphic(Add_OperatorDto add_OperatorDto)
        {
            bool isConcrete = add_OperatorDto.GetType() == typeof(Add_OperatorDto);

            if (isConcrete)
            {
                Visit_Add_OperatorDto_Concrete(add_OperatorDto);
            }
            else
            {
                Visit_Add_OperatorDto_Polymorphic(add_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Polymorphic(Add_OperatorDto add_OperatorDto)
        {
            var add_OperatorDto_VarA_VarB = add_OperatorDto as Add_OperatorDto_VarA_VarB;
            if (add_OperatorDto_VarA_VarB != null)
            {
                Visit_Add_OperatorDto_VarA_VarB(add_OperatorDto_VarA_VarB);
            }

            var add_OperatorDto_VarA_ConstB = add_OperatorDto as Add_OperatorDto_VarA_ConstB;
            if (add_OperatorDto_VarA_ConstB != null)
            {
                Visit_Add_OperatorDto_VarA_ConstB(add_OperatorDto_VarA_ConstB);
            }

            throw new UnexpectedTypeException(() => add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB add_OperatorDto_VarA_VarB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB add_OperatorDto_VarA_ConstB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Base(Add_OperatorDto add_OperatorDto)
        {
            Visit_OperatorDto_Base(add_OperatorDto);
        }

        // Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            bool isConcrete = multiply_OperatorDto.GetType() == typeof(Multiply_OperatorDto);

            if (isConcrete)
            {
                Visit_Multiply_OperatorDto_Concrete(multiply_OperatorDto);
            }
            else
            {
                Visit_Multiply_OperatorDto_Polymorphic(multiply_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto multiply_OperatorDto)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Polymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            var multiply_OperatorDto_VarA_VarB = multiply_OperatorDto as Multiply_OperatorDto_VarA_VarB;
            if (multiply_OperatorDto_VarA_VarB != null)
            {
                Visit_Multiply_OperatorDto_VarA_VarB(multiply_OperatorDto_VarA_VarB);
            }

            var multiply_OperatorDto_VarA_ConstB = multiply_OperatorDto as Multiply_OperatorDto_VarA_ConstB;
            if (multiply_OperatorDto_VarA_ConstB != null)
            {
                Visit_Multiply_OperatorDto_VarA_ConstB(multiply_OperatorDto_VarA_ConstB);
            }

            throw new UnexpectedTypeException(() => multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB multiply_OperatorDto_VarA_VarB)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB multiply_OperatorDto_VarA_ConstB)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Base(Multiply_OperatorDto multiply_OperatorDto)
        {
            Visit_OperatorDto_Base(multiply_OperatorDto);
        }

        // Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Visit_Number_OperatorDto(Number_OperatorDto number_OperatorDto)
        {
            Visit_OperatorDto_Base(number_OperatorDto);
        }

        // Base

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_OperatorDto_Base(OperatorDto operatorDto)
        {
            VisitInletDtos(operatorDto.InletDtos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInletDtos(IList<InletDto> inletDtos)
        {
            foreach (InletDto inletDto in inletDtos)
            {
                VisitInletDto(inletDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInletDto(InletDto inletDto)
        {
            VisitInputOperatorDto(inletDto.InputOperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInputOperatorDto(OperatorDto operatorDto)
        {
            Visit_OperatorDto_Polymorphic(operatorDto);
        }
    }
}
