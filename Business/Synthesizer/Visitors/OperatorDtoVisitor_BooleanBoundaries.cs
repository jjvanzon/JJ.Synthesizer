using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_BooleanBoundaries : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public IOperatorDto Execute(IOperatorDto dto)
        {
            IOperatorDto dto2 = Visit_OperatorDto_Polymorphic(dto);

            if (OutputIsAlwaysBoolean(dto2))
            {
                return InsertBooleanToDouble(dto2);
            }

            return dto2;
        }

        // Visit

        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
            => ProcessAndOrNot(dto);

        protected override IOperatorDto Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
            => ProcessAndOrNot(dto);

        protected override IOperatorDto Visit_Not_OperatorDto_VarNumber(Not_OperatorDto_VarNumber dto) 
            => ProcessAndOrNot(dto);

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
            => ProcessIf(dto);

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto) 
            => ProcessIf(dto);

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto) 
            => ProcessIf(dto);

        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto) 
            => ProcessIf(dto);

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            dto = base.Visit_OperatorDto_Polymorphic(dto);

            if (dto.OperatorTypeEnum != OperatorTypeEnum.And &&
                dto.OperatorTypeEnum != OperatorTypeEnum.Or && 
                dto.OperatorTypeEnum != OperatorTypeEnum.If && 
                dto.OperatorTypeEnum != OperatorTypeEnum.Not &&
                dto.OperatorTypeEnum != OperatorTypeEnum.BooleanToDouble &&
                dto.OperatorTypeEnum != OperatorTypeEnum.DoubleToBoolean)
            {
                var list = new List<IOperatorDto>();

                foreach (IOperatorDto inputOperatorDto in dto.InputOperatorDtos)
                {
                    IOperatorDto inputOperatorDto2 = TryInsertBooleanToDouble(inputOperatorDto);
                    list.Add(inputOperatorDto2);
                }

                dto.InputOperatorDtos = list;
            }

            return dto;
        }

        // Process

        private IOperatorDto ProcessIf(If_OperatorDtoBase_VarCondition dto)
        {
            base.Visit_OperatorDto_Base(dto);

            dto.ConditionOperatorDto = TryInsertDoubleToBoolean(dto.ConditionOperatorDto);

            return dto;
        }

        private IOperatorDto ProcessAndOrNot(IOperatorDto dto)
        {
            dto = base.Visit_OperatorDto_Base(dto);

            var list = new List<IOperatorDto>();

            for (int i = 0; i < dto.InputOperatorDtos.Count; i++)
            {
                IOperatorDto dto2 = TryInsertDoubleToBoolean(dto.InputOperatorDtos[i]);
                list.Add(dto2);
            }

            dto.InputOperatorDtos = list;

            return dto;
        }

        private IOperatorDto TryInsertDoubleToBoolean(IOperatorDto inputDto)
        {
            bool mustConvert = !OutputIsAlwaysBoolean(inputDto);
            if (mustConvert)
            {
                return InsertDoubleToBoolean(inputDto);
            }

            return inputDto;
        }

        private static IOperatorDto InsertDoubleToBoolean(IOperatorDto inputDto)
        {
            return new DoubleToBoolean_OperatorDto { NumberOperatorDto = inputDto };
        }

        private IOperatorDto TryInsertBooleanToDouble(IOperatorDto inputDto)
        {
            bool mustConvert = OutputIsAlwaysBoolean(inputDto);
            if (mustConvert)
            {
                return InsertBooleanToDouble(inputDto);
            }

            return inputDto;
        }

        private static IOperatorDto InsertBooleanToDouble(IOperatorDto inputDto)
        {
            return new BooleanToDouble_OperatorDto { InputOperatorDto = inputDto };
        }

        private static bool OutputIsAlwaysBoolean(IOperatorDto dto)
        {
            switch (dto.OperatorTypeEnum)
            {
                case OperatorTypeEnum.And:
                case OperatorTypeEnum.Equal:
                case OperatorTypeEnum.GreaterThan:
                case OperatorTypeEnum.GreaterThanOrEqual:
                case OperatorTypeEnum.NotEqual:
                case OperatorTypeEnum.LessThan:
                case OperatorTypeEnum.LessThanOrEqual:
                case OperatorTypeEnum.Not:
                case OperatorTypeEnum.Or:
                case OperatorTypeEnum.DoubleToBoolean:
                    return true;

                default:
                    return false;
            }
        }
    }
}
