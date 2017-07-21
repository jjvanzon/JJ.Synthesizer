//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_BooleanBoundaries : OperatorDtoVisitorBase_AfterMathSimplification
//    {
//        public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

//        // Visit

//        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
//            => ProcessBoolBoundaries_AllInputsBoolean(dto);

//        protected override IOperatorDto Visit_Or_OperatorDto_VarA_VarB(Or_OperatorDto_VarA_VarB dto)
//            => ProcessBoolBoundaries_AllInputsBoolean(dto);

//        protected override IOperatorDto Visit_Not_OperatorDto_VarNumber(Not_OperatorDto_VarNumber dto) 
//            => ProcessBoolBoundaries_AllInputsBoolean(dto);

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_ConstElse(If_OperatorDto_VarCondition_ConstThen_ConstElse dto)
//            => ProcessIf(dto);

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_ConstThen_VarElse(If_OperatorDto_VarCondition_ConstThen_VarElse dto) 
//            => ProcessIf(dto);

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_ConstElse(If_OperatorDto_VarCondition_VarThen_ConstElse dto) 
//            => ProcessIf(dto);

//        protected override IOperatorDto Visit_If_OperatorDto_VarCondition_VarThen_VarElse(If_OperatorDto_VarCondition_VarThen_VarElse dto) 
//            => ProcessIf(dto);

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            dto = base.Visit_OperatorDto_Polymorphic(dto);

//            if (!IsLogical(dto))
//            {
//                var list = new List<IOperatorDto>();

//                for (int i = 0; i < dto.InputOperatorDtos.Count; i++)
//                {
//                    IOperatorDto dto2 = TryProcessBooleanToDoubleBoundary(dto.InputOperatorDtos[i]);
//                    list.Add(dto2);
//                }

//                dto.InputOperatorDtos = list;
//            }

//            return dto;
//        }

//        // Process

//        private IOperatorDto ProcessIf(If_OperatorDtoBase_VarCondition dto)
//        {
//            base.Visit_OperatorDto_Base(dto);

//            dto.ConditionOperatorDto = TryProcessDoubleToBooleanBoundary(dto.ConditionOperatorDto);

//            return dto;
//        }

//        private IOperatorDto ProcessBoolBoundaries_AllInputsBoolean(IOperatorDto dto)
//        {
//            dto = base.Visit_OperatorDto_Base(dto);

//            var list = new List<IOperatorDto>();

//            for (int i = 0; i < dto.InputOperatorDtos.Count; i++)
//            {
//                IOperatorDto dto2 = TryProcessDoubleToBooleanBoundary(dto.InputOperatorDtos[i]);
//                list.Add(dto2);
//            }

//            dto.InputOperatorDtos = list;

//            return dto;
//        }

//        private IOperatorDto TryProcessDoubleToBooleanBoundary(IOperatorDto inputDto)
//        {
//            bool mustConvert = !OutputIsAlwaysBoolean(inputDto);
//            if (mustConvert)
//            {
//                // Sandwich conversion between the two nodes.
//                IOperatorDto insertedDto = new DoubleToBoolean_OperatorDto { NumberOperatorDto = inputDto };

//                // TODO: Consider if and how to reprocess it.
//                // Reprocess
//                //insertedDto = Visit_OperatorDto_Polymorphic(insertedDto);

//                return insertedDto;
//            }

//            return inputDto;
//        }

//        private IOperatorDto TryProcessBooleanToDoubleBoundary(IOperatorDto inputDto)
//        {
//            bool mustConvert = OutputIsAlwaysBoolean(inputDto);
//            if (mustConvert)
//            {
//                // Sandwich conversion between the two nodes.
//                IOperatorDto insertedDto = new BooleanToDouble_OperatorDto { InputOperatorDto = inputDto };

//                // TODO: Consider if and how to reprocess it.
//                // Reprocess
//                //insertedDto = Visit_OperatorDto_Polymorphic(insertedDto);

//                return insertedDto;
//            }

//            return inputDto;
//        }

//        private static bool OutputIsAlwaysBoolean(IOperatorDto dto)
//        {
//            switch (dto.OperatorTypeEnum)
//            {
//                case OperatorTypeEnum.And:
//                case OperatorTypeEnum.Equal:
//                case OperatorTypeEnum.GreaterThan:
//                case OperatorTypeEnum.GreaterThanOrEqual:
//                case OperatorTypeEnum.NotEqual:
//                case OperatorTypeEnum.LessThan:
//                case OperatorTypeEnum.LessThanOrEqual:
//                case OperatorTypeEnum.Not:
//                case OperatorTypeEnum.Or:
//                    return true;

//                default:
//                    return false;
//            }
//        }

//        private static bool IsLogical(IOperatorDto dto)
//        {
//            switch (dto.OperatorTypeEnum)
//            {
//                case OperatorTypeEnum.And:
//                case OperatorTypeEnum.Or:
//                case OperatorTypeEnum.If:
//                case OperatorTypeEnum.Not:
//                    return true;

//                default:
//                    return false;
//            }
//        }


//        //protected override IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto)
//        //{
//        //    MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.InputOperatorDto);

//        //    IOperatorDto dto2;
//        //    if (mathPropertiesDto.IsConst)
//        //    {
//        //        dto2 = new BooleanToDouble_OperatorDto_ConstInput { Input = mathPropertiesDto.ConstValue, OperatorID = dto.OperatorID };
//        //    }
//        //    else if (mathPropertiesDto.IsVar)
//        //    {
//        //        dto2 = new BooleanToDouble_OperatorDto_VarInput { InputOperatorDto = dto.InputOperatorDto, OperatorID = dto.OperatorID };
//        //    }
//        //    else
//        //    {
//        //        throw new VisitationCannotBeHandledException();
//        //    }

//        //    // TODO: Consider if and how to reprocess it.
//        //    // Reprocess
//        //    //dto2 = Visit_OperatorDto_Polymorphic(dto2);

//        //    return dto2;
//        //}

//        //protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_ConstInput(BooleanToDouble_OperatorDto_ConstInput dto)
//        //{
//        //    return base.Visit_BooleanToDouble_OperatorDto_ConstInput(dto);
//        //}

//        //protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_VarInput(BooleanToDouble_OperatorDto_VarInput dto)
//        //{
//        //    return base.Visit_BooleanToDouble_OperatorDto_VarInput(dto);
//        //}

//        //protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto)
//        //{
//        //    MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.NumberOperatorDto);

//        //    IOperatorDto dto2;
//        //    if (mathPropertiesDto.IsConst)
//        //    {
//        //        dto2 = new DoubleToBoolean_OperatorDto_ConstNumber { Number = mathPropertiesDto.ConstValue, OperatorID = dto.OperatorID };
//        //    }
//        //    else if (mathPropertiesDto.IsVar)
//        //    {
//        //        dto2 = new DoubleToBoolean_OperatorDto_VarNumber { NumberOperatorDto = dto.NumberOperatorDto, OperatorID = dto.OperatorID };
//        //    }
//        //    else
//        //    {
//        //        throw new VisitationCannotBeHandledException();
//        //    }

//        //    // TODO: Consider if and how to reprocess it.
//        //    // Reprocess
//        //    //dto2 = Visit_OperatorDto_Polymorphic(dto2);

//        //    return dto2;
//        //}

//        //protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_ConstNumber(DoubleToBoolean_OperatorDto_ConstNumber dto)
//        //{
//        //    return base.Visit_DoubleToBoolean_OperatorDto_ConstNumber(dto);
//        //}

//        //protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_VarNumber(DoubleToBoolean_OperatorDto_VarNumber dto)
//        //{
//        //    return base.Visit_DoubleToBoolean_OperatorDto_VarNumber(dto);
//        //}
//    }
//}
