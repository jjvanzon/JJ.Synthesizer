using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        private class ClosestOverInlets_MathPropertiesDto
        {
            public MathPropertiesDto InputMathPropertiesDto { get; set; }
            public IList<MathPropertiesDto> ItemMathPropertiesDtos { get; set; }
            public bool AllItemsAreConst { get; set; }
            public IList<double> ItemsValues { get; set; }
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            IList<OperatorDtoBase> operatorDtos = dto.InputOperatorDtos;

            IList<OperatorDtoBase> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();
            IList<OperatorDtoBase> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();
            IList<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).Value).ToArray();

            bool hasVars = varOperatorDtos.Any();
            bool hasConsts = constOperatorDtos.Any();

            if (hasVars && hasConsts)
            {
                return new Add_OperatorDto_Vars_Consts { Vars = varOperatorDtos, Consts = consts };
            }
            else if (hasVars && !hasConsts)
            {
                return new Add_OperatorDto_Vars_NoConsts { Vars = varOperatorDtos };
            }
            else if (!hasVars && hasConsts)
            {
                return new Add_OperatorDto_NoVars_Consts { Consts = consts };
            }
            else if (!hasVars && !hasConsts)
            {
                return new Add_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_AllPassFilter_OperatorDto(AllPassFilter_OperatorDto dto)
        {
            base.Visit_AllPassFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase centerFrequencyOperatorDto = dto.CenterFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(centerFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                return new AllPassFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.Value, BandWidth = bandWidthMathPropertiesDto.Value };
            }
            else
            {
                return new AllPassFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, CenterFrequencyOperatorDto = centerFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_And_OperatorDto(And_OperatorDto dto)
        {
            base.Visit_And_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new And_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.Value, B = bMathPropertiesDto.Value };
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new And_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.Value };
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new And_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.Value, BOperatorDto = bOperatorDto };
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new And_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_AverageOverDimension_OperatorDto(AverageOverDimension_OperatorDto dto)
        {
            base.Visit_AverageOverDimension_OperatorDto(dto);

            AverageOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantPeakGain_OperatorDto(BandPassFilterConstantPeakGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantPeakGain_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase centerFrequencyOperatorDto = dto.CenterFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(centerFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                return new BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.Value, BandWidth = bandWidthMathPropertiesDto.Value };
            }
            else
            {
                return new BandPassFilterConstantPeakGain_OperatorDto_VarCenterFrequency_VarBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequencyOperatorDto = centerFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_BandPassFilterConstantTransitionGain_OperatorDto(BandPassFilterConstantTransitionGain_OperatorDto dto)
        {
            base.Visit_BandPassFilterConstantTransitionGain_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase centerFrequencyOperatorDto = dto.CenterFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(centerFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                return new BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.Value, BandWidth = bandWidthMathPropertiesDto.Value };
            }
            else
            {
                return new BandPassFilterConstantTransitionGain_OperatorDto_VarCenterFrequency_VarBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequencyOperatorDto = centerFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_Cache_OperatorDto(Cache_OperatorDto dto)
        {
            base.Visit_Cache_OperatorDto(dto);

            Cache_OperatorDto dto2;

            switch (dto.ChannelCount)
            {
                case 1:
                    dto2 = new Cache_OperatorDto_SingleChannel();
                    break;

                default:
                    dto2 = new Cache_OperatorDto_MultiChannel();
                    break;
            }

            Clone_CacheOperatorProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto)
        {
            base.Visit_ClosestOverDimension_OperatorDto(dto);

            ClosestOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_ClosestOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto)
        {
            base.Visit_ClosestOverDimensionExp_OperatorDto(dto);

            ClosestOverDimensionExp_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_ClosestOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            var mathPropertiesDto = Get_ClosestOverInlets_MathPropertiesDto(dto);

            if (mathPropertiesDto.InputMathPropertiesDto.IsConst)
            {
                return dto.InputOperatorDto;
            }
            if (dto.ItemOperatorDtos.Count == 2 && mathPropertiesDto.AllItemsAreConst)
            {
                double item1 = mathPropertiesDto.ItemsValues[0];
                double item2 = mathPropertiesDto.ItemsValues[1];

                return new ClosestOverInlets_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = item1, Item2 = item2 };
            }
            else if (mathPropertiesDto.AllItemsAreConst)
            {
                return new ClosestOverInlets_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = mathPropertiesDto.ItemsValues };
            }
            else
            {
                return new ClosestOverInlets_OperatorDto_AllVars { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            var mathPropertiesDto = Get_ClosestOverInlets_MathPropertiesDto(dto);

            if (mathPropertiesDto.InputMathPropertiesDto.IsConst)
            {
                return dto.InputOperatorDto;
            }
            if (dto.ItemOperatorDtos.Count == 2 && mathPropertiesDto.AllItemsAreConst)
            {
                double item1 = mathPropertiesDto.ItemsValues[0];
                double item2 = mathPropertiesDto.ItemsValues[1];

                return new ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = item1, Item2 = item2 };
            }
            else if (mathPropertiesDto.AllItemsAreConst)
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = mathPropertiesDto.ItemsValues };
            }
            else
            {
                return new ClosestOverInletsExp_OperatorDto_AllVars { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override OperatorDtoBase Visit_Curve_OperatorDto(Curve_OperatorDto dto)
        {
            base.Visit_Curve_OperatorDto(dto);

            if (dto.Curve == null)
            {
                return new Number_OperatorDto_Zero();
            }

            Curve_OperatorDto dto2;

            if (dto.MinX == 0.0)
            {
                if (dto.StandardDimensionEnum == DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinXZero_WithOriginShifting();

                }
                else
                {
                    dto2 = new Curve_OperatorDto_MinXZero_NoOriginShifting();
                }
            }
            else
            {
                if (dto.StandardDimensionEnum == DimensionEnum.Time)
                {
                    dto2 = new Curve_OperatorDto_MinX_WithOriginShifting();
                }
                else
                {
                    dto2 = new Curve_OperatorDto_MinX_NoOriginShifting();
                }
            }

            Clone_CurveProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Divide_OperatorDto(Divide_OperatorDto dto)
        {
            base.Visit_Divide_OperatorDto(dto);

            OperatorDtoBase numeratorOperatorDto = dto.NumeratorOperatorDto;
            OperatorDtoBase denominatorOperatorDto = dto.DenominatorOperatorDto;
            OperatorDtoBase originOperatorDto = dto.OriginOperatorDto;

            MathPropertiesDto numeratorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(numeratorOperatorDto);
            MathPropertiesDto denominatorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(denominatorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(originOperatorDto);

            if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_VarNumerator_VarDenominator_VarOrigin { NumeratorOperatorDto = numeratorOperatorDto, DenominatorOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_VarNumerator_VarDenominator_ZeroOrigin { NumeratorOperatorDto = numeratorOperatorDto, DenominatorOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin { NumeratorOperatorDto = numeratorOperatorDto, DenominatorOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.Value, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.Value, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin { Numerator = numeratorMathPropertiesDto.Value, DenominatorOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin { Numerator = numeratorMathPropertiesDto.Value, DenominatorOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin { Numerator = numeratorMathPropertiesDto.Value, DenominatorOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin { Numerator = numeratorMathPropertiesDto.Value, Denominator = denominatorMathPropertiesDto.Value, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin { Numerator = numeratorMathPropertiesDto.Value, Denominator = denominatorMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin { Numerator = numeratorMathPropertiesDto.Value, Denominator = denominatorMathPropertiesDto.Value, Origin = originMathPropertiesDto.Value };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto(Exponent_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_If_OperatorDto(If_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto(Loop_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MultiplyWithOrigin_OperatorDto(MultiplyWithOrigin_OperatorDto dto)
        {
            base.Visit_MultiplyWithOrigin_OperatorDto(dto);

            OperatorDtoBase numeratorOperatorDto = dto.AOperatorDto;
            OperatorDtoBase denominatorOperatorDto = dto.BOperatorDto;
            OperatorDtoBase originOperatorDto = dto.OriginOperatorDto;

            MathPropertiesDto numeratorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(numeratorOperatorDto);
            MathPropertiesDto denominatorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(denominatorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(originOperatorDto);

            if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_VarOrigin { AOperatorDto = numeratorOperatorDto, BOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_ZeroOrigin { AOperatorDto = numeratorOperatorDto, BOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = numeratorOperatorDto, BOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.Value, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.Value, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin { A = numeratorMathPropertiesDto.Value, BOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin { A = numeratorMathPropertiesDto.Value, BOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin { A = numeratorMathPropertiesDto.Value, BOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin { A = numeratorMathPropertiesDto.Value, B = denominatorMathPropertiesDto.Value, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin { A = numeratorMathPropertiesDto.Value, B = denominatorMathPropertiesDto.Value };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin { A = numeratorMathPropertiesDto.Value, B = denominatorMathPropertiesDto.Value, Origin = originMathPropertiesDto.Value };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto(OneOverX_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto(Pulse_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto(Random_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorCalculator_OnlyConsts(RangeOverDimension_OperatorCalculator_OnlyConsts dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto(RangeOverOutlets_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto(Reverse_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto(Round_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto(Sample_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto(SawDown_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto(SawUp_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto(Scaler_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto(Select_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto(Square_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto(Squash_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto(Stretch_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto(TimePower_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto(Triangle_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        // Clone

        private void Clone_AggregateOverDimensionProperties(OperatorDtoBase_AggregateOverDimension source, OperatorDtoBase_AggregateOverDimension dest)
        {
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.FromOperatorDto = source.FromOperatorDto;
            dest.TillOperatorDto = source.TillOperatorDto;
            dest.StepOperatorDto = source.StepOperatorDto;
            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

            Clone_DimensionProperties(source, dest);
        }

        private void Clone_CacheOperatorProperties(Cache_OperatorDto source, Cache_OperatorDto dest)
        {
            dest.SignalOperatorDto = source.SignalOperatorDto;
            dest.StartOperatorDto = source.StartOperatorDto;
            dest.EndOperatorDto = source.EndOperatorDto;
            dest.SamplingRateOperatorDto = source.SamplingRateOperatorDto;
            dest.InterpolationTypeEnum = source.InterpolationTypeEnum;
            dest.SpeakerSetupEnum = source.SpeakerSetupEnum;
            dest.ChannelCount = source.ChannelCount;
        }

        private void Clone_ClosestOverDimensionProperties(ClosestOverDimension_OperatorDto source, ClosestOverDimension_OperatorDto dest)
        {
            dest.InputOperatorDto = source.InputOperatorDto;
            dest.CollectionOperatorDto = source.CollectionOperatorDto;
            dest.FromOperatorDto = source.FromOperatorDto;
            dest.TillOperatorDto = source.TillOperatorDto;
            dest.StepOperatorDto = source.StepOperatorDto;
            dest.CollectionRecalculationEnum = source.CollectionRecalculationEnum;

            Clone_DimensionProperties(source, dest);
        }

        private void Clone_CurveProperties(Curve_OperatorDto source, Curve_OperatorDto dest)
        {
            dest.Curve = source.Curve;
            dest.MinX = source.MinX;
            Clone_DimensionProperties(source, dest);
        }

        private void Clone_DimensionProperties(IOperatorDto_WithDimension source, IOperatorDto_WithDimension dest)
        {
            dest.CustomDimensionName = source.CustomDimensionName;
            dest.StandardDimensionEnum = source.StandardDimensionEnum;
        }

        // Helpers

        private ClosestOverInlets_MathPropertiesDto Get_ClosestOverInlets_MathPropertiesDto(ClosestOverInlets_OperatorDto dto)
        {
            OperatorDtoBase inputOperatorDto = dto.InputOperatorDto;
            IList<OperatorDtoBase> itemOperatorDtos = dto.ItemOperatorDtos;

            MathPropertiesDto inputMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(inputOperatorDto);

            int count = itemOperatorDtos.Count;
            IList<MathPropertiesDto> itemMathPropertiesDtos = new MathPropertiesDto[count];
            for (int i = 0; i < count; i++)
            {
                OperatorDtoBase itemOperatorDto = itemOperatorDtos[i];
                MathPropertiesDto itemMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(itemOperatorDto);
                itemMathPropertiesDtos[i] = itemMathPropertiesDto;
            }

            bool allItemsAreConst = itemMathPropertiesDtos.All(x => x.IsConst);
            IList<double> itemsValues = itemMathPropertiesDtos.Select(x => x.Value).ToArray();

            var mathPropertiesDto = new ClosestOverInlets_MathPropertiesDto
            {
                InputMathPropertiesDto = inputMathPropertiesDto,
                ItemMathPropertiesDtos = itemMathPropertiesDtos,
                AllItemsAreConst = allItemsAreConst,
                ItemsValues = itemsValues
            };

            return mathPropertiesDto;
        }
    }
}
