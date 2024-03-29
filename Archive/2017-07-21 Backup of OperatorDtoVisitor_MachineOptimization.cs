﻿//using System;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Common;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_MachineOptimization : OperatorDtoVisitorBase_AfterMathSimplification
//    {
//        public IOperatorDto Execute(IOperatorDto dto)
//        {
//            return Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(ClosestOverInlets_OperatorDto_VarInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInlets_OperatorDto_VarInput_ConstItems(dto);

//            if (dto.Items.Count == 2)
//            {
//                return new ClosestOverInlets_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = dto.Items[0], Item2 = dto.Items[1], OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(ClosestOverInletsExp_OperatorDto_VarInput_ConstItems dto)
//        {
//            base.Visit_ClosestOverInletsExp_OperatorDto_VarInput_ConstItems(dto);

//            if (dto.Items.Count == 2)
//            {
//                return new ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = dto.Items[0], Item2 = dto.Items[1], OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
//        {
//            base.Visit_MaxOverInlets_OperatorDto_Vars_1Const(dto);

//            if (dto.Vars.Count == 1)
//            {
//                return new MaxOverInlets_OperatorDto_1Var_1Const { AOperatorDto = dto.Vars[0], B = dto.ConstValue, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(MaxOverInlets_OperatorDto_Vars_NoConsts dto)
//        {
//            base.Visit_MaxOverInlets_OperatorDto_Vars_NoConsts(dto);

//            if (dto.Vars.Count == 2)
//            {
//                return new MaxOverInlets_OperatorDto_2Vars { AOperatorDto = dto.Vars[0], BOperatorDto = dto.Vars[1], OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
//        {
//            base.Visit_MinOverInlets_OperatorDto_Vars_1Const(dto);

//            if (dto.Vars.Count == 1)
//            {
//                return new MinOverInlets_OperatorDto_1Var_1Const { AOperatorDto = dto.Vars[0], B = dto.ConstValue, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_MinOverInlets_OperatorDto_Vars_NoConsts(MinOverInlets_OperatorDto_Vars_NoConsts dto)
//        {
//            base.Visit_MinOverInlets_OperatorDto_Vars_NoConsts(dto);

//            if (dto.Vars.Count == 2)
//            {
//                return new MinOverInlets_OperatorDto_2Vars { AOperatorDto = dto.Vars[0], BOperatorDto = dto.Vars[1], OperatorID = dto.OperatorID };
//            }
//            return dto;
//        }

//        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
//        {
//            base.Visit_Number_OperatorDto(dto);

//            double value = dto.Number;

//            if (DoubleHelper.IsSpecialValue(value))
//            {
//                return new Number_OperatorDto_NaN();
//            }

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (value == 1.0)
//            {
//                return new Number_OperatorDto_One();
//            }

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            // ReSharper disable once ConvertIfStatementToReturnStatement
//            if (value == 0.0)
//            {
//                return new Number_OperatorDto_Zero();
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_Power_OperatorDto_VarBase_ConstExponent(Power_OperatorDto_VarBase_ConstExponent dto)
//        {
//            base.Visit_Power_OperatorDto_VarBase_ConstExponent(dto);

//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (dto.Exponent == 2.0)
//            {
//                return new Power_OperatorDto_VarBase_Exponent2 { BaseOperatorDto = dto.BaseOperatorDto, OperatorID = dto.OperatorID };
//            }
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            else if (dto.Exponent == 3.0)
//            {
//                return new Power_OperatorDto_VarBase_Exponent3 { BaseOperatorDto = dto.BaseOperatorDto, OperatorID = dto.OperatorID };
//            }
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            else if (dto.Exponent == 4.0)
//            {
//                return new Power_OperatorDto_VarBase_Exponent4 { BaseOperatorDto = dto.BaseOperatorDto, OperatorID = dto.OperatorID };
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_RangeOverDimension_OperatorDto_OnlyConsts(RangeOverDimension_OperatorDto_OnlyConsts dto)
//        {
//            base.Visit_RangeOverDimension_OperatorDto_OnlyConsts(dto);

//            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.Step);

//            // ReSharper disable once InvertIf
//            if (stepMathPropertiesDto.IsConstOne)
//            {
//                var dto2 = new RangeOverDimension_OperatorDto_WithConsts_AndStepOne { From = dto.From, Till = dto.Till, OperatorID = dto.OperatorID };
//                DtoCloner.Clone_WithDimensionProperties(dto, dto2);

//                return dto2;
//            }

//            return dto;
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            if (InputAlwaysBoolean(dto))
//            {
//                // Input is bool.
//                foreach (IOperatorDto inputOperatorDto in dto.InputOperatorDtos)
//                {
//                    if (!OutputAlwaysBoolean(inputOperatorDto))
//                    {
//                        // Output is double.
//                        // TODO: Bool boundary! Insert a conversion node!
//                        throw new NotImplementedException();
//                    }
//                }
//            }
//            else
//            {
//                // Input can be double.

//                foreach (IOperatorDto inputOperatorDto in dto.InputOperatorDtos)
//                {
//                    if (OutputAlwaysBoolean(inputOperatorDto))
//                    {
//                        // Output is boolean.

//                        // TODO: Bool boundary! Insert a conversion node!
//                        throw new NotImplementedException();
//                    }
//                }
//            }

//            return base.Visit_OperatorDto_Polymorphic(dto);
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

//        private static bool OutputAlwaysBoolean(IOperatorDto dto)
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

//        private static bool InputAlwaysBoolean(IOperatorDto dto)
//        {
//            switch (dto.OperatorTypeEnum)
//            {
//                case OperatorTypeEnum.And:
//                case OperatorTypeEnum.Not:
//                case OperatorTypeEnum.Or:
//                    return true;

//                default:
//                    return false;
//            }
//        }

//        protected override IOperatorDto Visit_And_OperatorDto_VarA_VarB(And_OperatorDto_VarA_VarB dto)
//        {
//            //bool isBoolBoundary = dto.
//            return base.Visit_And_OperatorDto_VarA_VarB(dto);
//        }

//        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto(BooleanToDouble_OperatorDto dto) => base.Visit_BooleanToDouble_OperatorDto(dto)

//        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_ConstInput(BooleanToDouble_OperatorDto_ConstInput dto) => base.Visit_BooleanToDouble_OperatorDto_ConstInput(dto);

//        protected override IOperatorDto Visit_BooleanToDouble_OperatorDto_VarInput(BooleanToDouble_OperatorDto_VarInput dto) => base.Visit_BooleanToDouble_OperatorDto_VarInput(dto);

//        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto(DoubleToBoolean_OperatorDto dto) => base.Visit_DoubleToBoolean_OperatorDto(dto);

//        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_ConstNumber(DoubleToBoolean_OperatorDto_ConstNumber dto) => base.Visit_DoubleToBoolean_OperatorDto_ConstNumber(dto);

//        protected override IOperatorDto Visit_DoubleToBoolean_OperatorDto_VarNumber(DoubleToBoolean_OperatorDto_VarNumber dto) => base.Visit_DoubleToBoolean_OperatorDto_VarNumber(dto);
//    }
//}