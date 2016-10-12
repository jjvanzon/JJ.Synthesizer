using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class OperatorDtoVisitorBase
    {
        // TODO: Try to make dispatching faster, by introducing overloads that take OperatorDto?
        //private static readonly Dictionary<Type, Action<OperatorDto>> _operatorDtoTypeToDelegateDictionary =
        //                    new Dictionary<Type, Action<OperatorDto>>
        //{
        //    { typeof(Add_OperatorDto), Visit_Add_OperatorDto }
        //};

        public void Execute(OperatorDto operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            Visit_OperatorDto_Polymorphic(operatorDto);
        }

        protected virtual void Visit_OperatorDto_Polymorphic(OperatorDto operatorDto)
        {
            var add_OperatorDto = operatorDto as Add_OperatorDto;
            if (add_OperatorDto != null)
            {
                Visit_Add_OperatorDto_ConcreteOrPolymorphic(add_OperatorDto);
            }
        }

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

        protected virtual void Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto);
        }

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

        protected virtual void Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB add_OperatorDto_VarA_VarB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_VarB);
        }

        protected virtual void Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB add_OperatorDto_VarA_ConstB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_ConstB);
        }

        protected virtual void Visit_Add_OperatorDto_Base(Add_OperatorDto add_OperatorDto)
        {
            Visit_OperatorDto_Base(add_OperatorDto);
        }

        protected virtual void Visit_OperatorDto_Base(OperatorDto operatorDto)
        { }
    }
}
