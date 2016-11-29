using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        private class VarsConsts_MathPropetiesDto
        {
            public IList<OperatorDtoBase> VarOperatorDtos { get; set; }
            public IList<double> Consts { get; set; }
            public bool HasVars { get; set; }
            public bool HasConsts { get; set; }
        }

        private readonly int _targetChannelCount;

        public ClassSpecialization_OperatorDtoVisitor(int targetChannelCount)
        {
            if (targetChannelCount <= 0) throw new LessThanOrEqualException(() => targetChannelCount, 0);

            _targetChannelCount = targetChannelCount;
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_Absolute_OperatorDto(Absolute_OperatorDto dto)
        {
            base.Visit_Absolute_OperatorDto(dto);

            OperatorDtoBase xOperatorDto = dto.XOperatorDto;

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(xOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Absolute_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else
            {
                return new Absolute_OperatorDto_VarX { XOperatorDto = xOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            VarsConsts_MathPropetiesDto mathPropertiesDto = Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.VarOperatorDtos, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.VarOperatorDtos };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Add_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
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
                return new AllPassFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
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
                return new And_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new And_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new And_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
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

        // AverageOverInlets not visited: It currently has no optimized calculation variations.

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
                return new BandPassFilterConstantPeakGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
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
                return new BandPassFilterConstantTransitionGain_OperatorDto_ConstCenterFrequency_ConstBandWidth { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
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

        // ClosestOverInlets and ClosestOverInletsExp not visited: It does not split up into vars/consts variations, only specific machine optimizations in the MachineOptimization_OperatorDtoVisitor.

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
                return new Divide_OperatorDto_VarNumerator_VarDenominator_ConstOrigin { NumeratorOperatorDto = numeratorOperatorDto, DenominatorOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_VarOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_ZeroOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_VarNumerator_ConstDenominator_ConstOrigin { NumeratorOperatorDto = numeratorOperatorDto, Denominator = denominatorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_VarOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, DenominatorOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_ZeroOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, DenominatorOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstNumerator_VarDenominator_ConstOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, DenominatorOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_VarOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, Denominator = denominatorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_ZeroOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, Denominator = denominatorMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new Divide_OperatorDto_ConstNumerator_ConstDenominator_ConstOrigin { Numerator = numeratorMathPropertiesDto.ConstValue, Denominator = denominatorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Equal_OperatorDto(Equal_OperatorDto dto)
        {
            base.Visit_Equal_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Equal_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Equal_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Equal_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Equal_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Exponent_OperatorDto(Exponent_OperatorDto dto)
        {
            base.Visit_Exponent_OperatorDto(dto);

            OperatorDtoBase lowOperatorDto = dto.LowOperatorDto;
            OperatorDtoBase highOperatorDto = dto.HighOperatorDto;
            OperatorDtoBase ratioOperatorDto = dto.RatioOperatorDto;

            MathPropertiesDto lowMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(lowOperatorDto);
            MathPropertiesDto highMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(highOperatorDto);
            MathPropertiesDto ratioMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(ratioOperatorDto);

            if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_VarLow_VarHigh_VarRatio { LowOperatorDto = lowOperatorDto, HighOperatorDto = highOperatorDto, RatioOperatorDto = ratioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_VarLow_VarHigh_ConstRatio { LowOperatorDto = lowOperatorDto, HighOperatorDto = highOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_VarLow_ConstHigh_VarRatio { LowOperatorDto = lowOperatorDto, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = ratioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsVar && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_VarLow_ConstHigh_ConstRatio { LowOperatorDto = lowOperatorDto, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_ConstLow_VarHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = highOperatorDto, RatioOperatorDto = ratioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsVar && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_ConstLow_VarHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, HighOperatorDto = highOperatorDto, Ratio = ratioMathPropertiesDto.ConstValue };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsVar)
            {
                return new Exponent_OperatorDto_ConstLow_ConstHigh_VarRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, RatioOperatorDto = ratioOperatorDto };
            }
            else if (lowMathPropertiesDto.IsConst && highMathPropertiesDto.IsConst && ratioMathPropertiesDto.IsConst)
            {
                return new Exponent_OperatorDto_ConstLow_ConstHigh_ConstRatio { Low = lowMathPropertiesDto.ConstValue, High = highMathPropertiesDto.ConstValue, Ratio = ratioMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_GreaterThan_OperatorDto(GreaterThan_OperatorDto dto)
        {
            base.Visit_GreaterThan_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new GreaterThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new GreaterThan_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new GreaterThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new GreaterThan_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_GreaterThanOrEqual_OperatorDto(GreaterThanOrEqual_OperatorDto dto)
        {
            base.Visit_GreaterThanOrEqual_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new GreaterThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new GreaterThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new GreaterThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new GreaterThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_HighPassFilter_OperatorDto(HighPassFilter_OperatorDto dto)
        {
            base.Visit_HighPassFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase minFrequencyOperatorDto = dto.MinFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto minFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(minFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (minFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                return new HighPassFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, MinFrequency = minFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                return new HighPassFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, MinFrequencyOperatorDto = minFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_HighShelfFilter_OperatorDto(HighShelfFilter_OperatorDto dto)
        {
            base.Visit_HighShelfFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase transitionFrequencyOperatorDto = dto.TransitionFrequencyOperatorDto;
            OperatorDtoBase transitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto;
            OperatorDtoBase dbGainOperatorDto = dto.DBGainOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(transitionFrequencyOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(transitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dbGainOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                return new HighShelfFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                return new HighShelfFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, TransitionFrequencyOperatorDto = transitionFrequencyOperatorDto, TransitionSlopeOperatorDto = transitionSlopeOperatorDto, DBGainOperatorDto = dbGainOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_If_OperatorDto(If_OperatorDto dto)
        {
            base.Visit_If_OperatorDto(dto);

            OperatorDtoBase conditionOperatorDto = dto.ConditionOperatorDto;
            OperatorDtoBase thenOperatorDto = dto.ThenOperatorDto;
            OperatorDtoBase elseOperatorDto = dto.ElseOperatorDto;

            MathPropertiesDto conditionMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(conditionOperatorDto);
            MathPropertiesDto thenMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(thenOperatorDto);
            MathPropertiesDto elseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(elseOperatorDto);

            if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_VarCondition_VarThen_VarElse { ConditionOperatorDto = conditionOperatorDto, ThenOperatorDto = thenOperatorDto, ElseOperatorDto = elseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_VarCondition_VarThen_ConstElse { ConditionOperatorDto = conditionOperatorDto, ThenOperatorDto = thenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_VarCondition_ConstThen_VarElse { ConditionOperatorDto = conditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = elseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsVar && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_VarCondition_ConstThen_ConstElse { ConditionOperatorDto = conditionOperatorDto, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_ConstCondition_VarThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = thenOperatorDto, ElseOperatorDto = elseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsVar && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_ConstCondition_VarThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, ThenOperatorDto = thenOperatorDto, Else = elseMathPropertiesDto.ConstValue };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsVar)
            {
                return new If_OperatorDto_ConstCondition_ConstThen_VarElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, ElseOperatorDto = elseOperatorDto };
            }
            else if (conditionMathPropertiesDto.IsConst && thenMathPropertiesDto.IsConst && elseMathPropertiesDto.IsConst)
            {
                return new If_OperatorDto_ConstCondition_ConstThen_ConstElse { Condition = conditionMathPropertiesDto.ConstValue, Then = thenMathPropertiesDto.ConstValue, Else = elseMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Interpolate_OperatorDto(Interpolate_OperatorDto dto)
        {
            base.Visit_Interpolate_OperatorDto(dto);

            MathPropertiesDto samplingRateMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SamplingRateOperatorDto);

            OperatorDtoBase dto2;

            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
            {
                dto2 = new Interpolate_OperatorDto_Block { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
            {
                dto2 = new Interpolate_OperatorDto_Stripe_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && samplingRateMathPropertiesDto.IsConst)
            {
                dto2 = new Interpolate_OperatorDto_Line_LagBehind_ConstSamplingRate { SamplingRate = samplingRateMathPropertiesDto.ConstValue };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line && samplingRateMathPropertiesDto.IsVar)
            {
                dto2 = new Interpolate_OperatorDto_Line_LagBehind_VarSamplingRate { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
            {
                dto2 = new Interpolate_OperatorDto_CubicEquidistant { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
            {
                dto2 = new Interpolate_OperatorDto_CubicAbruptSlope { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
            {
                dto2 = new Interpolate_OperatorDto_CubicSmoothSlope_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
            {
                dto2 = new Interpolate_OperatorDto_Hermite_LagBehind { SamplingRateOperatorDto = dto.SamplingRateOperatorDto };
            }
            else 
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            var asIInterpolate_OperatorDto = dto2 as IInterpolate_OperatorDto;
            if (asIInterpolate_OperatorDto != null)
            {
                Clone_InterpolateOperatorProperties(dto, asIInterpolate_OperatorDto);
            }

            return dto2;
        }

        protected override OperatorDtoBase Visit_LessThan_OperatorDto(LessThan_OperatorDto dto)
        {
            base.Visit_LessThan_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new LessThan_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new LessThan_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new LessThan_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new LessThan_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_LessThanOrEqual_OperatorDto(LessThanOrEqual_OperatorDto dto)
        {
            base.Visit_LessThanOrEqual_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new LessThanOrEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new LessThanOrEqual_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new LessThanOrEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new LessThanOrEqual_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Loop_OperatorDto(Loop_OperatorDto dto)
        {
            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_LowPassFilter_OperatorDto(LowPassFilter_OperatorDto dto)
        {
            base.Visit_LowPassFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase maxFrequencyOperatorDto = dto.MaxFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto maxFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(maxFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (maxFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst)
            {
                return new LowPassFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, MaxFrequency = maxFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                return new LowPassFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, MaxFrequencyOperatorDto = maxFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_LowShelfFilter_OperatorDto(LowShelfFilter_OperatorDto dto)
        {
            base.Visit_LowShelfFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase transitionFrequencyOperatorDto = dto.TransitionFrequencyOperatorDto;
            OperatorDtoBase transitionSlopeOperatorDto = dto.TransitionSlopeOperatorDto;
            OperatorDtoBase dbGainOperatorDto = dto.DBGainOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto transitionFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(transitionFrequencyOperatorDto);
            MathPropertiesDto transitionSlopeMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(transitionSlopeOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dbGainOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (transitionFrequencyMathPropertiesDto.IsConst && transitionSlopeMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                return new LowShelfFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, TransitionFrequency = transitionFrequencyMathPropertiesDto.ConstValue, TransitionSlope = transitionSlopeMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                return new LowShelfFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, TransitionFrequencyOperatorDto = transitionFrequencyOperatorDto, TransitionSlopeOperatorDto = transitionSlopeOperatorDto, DBGainOperatorDto = dbGainOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_MaxOverDimension_OperatorDto(MaxOverDimension_OperatorDto dto)
        {
            base.Visit_MaxOverDimension_OperatorDto(dto);

            MaxOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new MaxOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new MaxOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto(MaxOverInlets_OperatorDto dto)
        {
            base.Visit_MaxOverInlets_OperatorDto(dto);

            VarsConsts_MathPropetiesDto mathPropertiesDto = Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.VarOperatorDtos, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.VarOperatorDtos };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MaxOverInlets_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_MinOverDimension_OperatorDto(MinOverDimension_OperatorDto dto)
        {
            base.Visit_MinOverDimension_OperatorDto(dto);

            MinOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new MinOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new MinOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto(MinOverInlets_OperatorDto dto)
        {
            base.Visit_MinOverInlets_OperatorDto(dto);

            VarsConsts_MathPropetiesDto mathPropertiesDto = Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.VarOperatorDtos, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.VarOperatorDtos };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new MinOverInlets_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            VarsConsts_MathPropetiesDto mathPropertiesDto = Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.VarOperatorDtos, Consts = mathPropertiesDto.Consts };
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.VarOperatorDtos };
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts };
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return new Multiply_OperatorDto_NoVars_NoConsts();
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
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
                return new MultiplyWithOrigin_OperatorDto_VarA_VarB_ConstOrigin { AOperatorDto = numeratorOperatorDto, BOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_VarOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ZeroOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsVar && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_VarA_ConstB_ConstOrigin { AOperatorDto = numeratorOperatorDto, B = denominatorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_VarOrigin { A = numeratorMathPropertiesDto.ConstValue, BOperatorDto = denominatorOperatorDto, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ZeroOrigin { A = numeratorMathPropertiesDto.ConstValue, BOperatorDto = denominatorOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_VarB_ConstOrigin { A = numeratorMathPropertiesDto.ConstValue, BOperatorDto = denominatorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_VarOrigin { A = numeratorMathPropertiesDto.ConstValue, B = denominatorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ZeroOrigin { A = numeratorMathPropertiesDto.ConstValue, B = denominatorMathPropertiesDto.ConstValue };
            }
            else if (numeratorMathPropertiesDto.IsConst && denominatorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
            {
                return new MultiplyWithOrigin_OperatorDto_ConstA_ConstB_ConstOrigin { A = numeratorMathPropertiesDto.ConstValue, B = denominatorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Negative_OperatorDto(Negative_OperatorDto dto)
        {
            base.Visit_Negative_OperatorDto(dto);

            OperatorDtoBase xOperatorDto = dto.XOperatorDto;

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(xOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Negative_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else
            {
                return new Negative_OperatorDto_VarX { XOperatorDto = xOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_Not_OperatorDto(Not_OperatorDto dto)
        {
            base.Visit_Not_OperatorDto(dto);

            OperatorDtoBase xOperatorDto = dto.XOperatorDto;

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(xOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new Not_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else
            {
                return new Not_OperatorDto_VarX { XOperatorDto = xOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_NotchFilter_OperatorDto(NotchFilter_OperatorDto dto)
        {
            base.Visit_NotchFilter_OperatorDto(dto);

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
                return new NotchFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue };
            }
            else
            {
                return new NotchFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, CenterFrequencyOperatorDto = centerFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_NotEqual_OperatorDto(NotEqual_OperatorDto dto)
        {
            base.Visit_NotEqual_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new NotEqual_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new NotEqual_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new NotEqual_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new NotEqual_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_OneOverX_OperatorDto(OneOverX_OperatorDto dto)
        {
            base.Visit_OneOverX_OperatorDto(dto);

            OperatorDtoBase xOperatorDto = dto.XOperatorDto;

            MathPropertiesDto xMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(xOperatorDto);

            if (xMathPropertiesDto.IsConst)
            {
                return new OneOverX_OperatorDto_ConstX { X = xMathPropertiesDto.ConstValue };
            }
            else
            {
                return new OneOverX_OperatorDto_VarX { XOperatorDto = xOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_Or_OperatorDto(Or_OperatorDto dto)
        {
            base.Visit_Or_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Or_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Or_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Or_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Or_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_PeakingEQFilter_OperatorDto(PeakingEQFilter_OperatorDto dto)
        {
            base.Visit_PeakingEQFilter_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase centerFrequencyOperatorDto = dto.CenterFrequencyOperatorDto;
            OperatorDtoBase bandWidthOperatorDto = dto.BandWidthOperatorDto;
            OperatorDtoBase dbGainOperatorDto = dto.DBGainOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto centerFrequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(centerFrequencyOperatorDto);
            MathPropertiesDto bandWidthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bandWidthOperatorDto);
            MathPropertiesDto dbGainMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dbGainOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return signalOperatorDto;
            }
            else if (centerFrequencyMathPropertiesDto.IsConst && bandWidthMathPropertiesDto.IsConst & dbGainMathPropertiesDto.IsConst)
            {
                return new PeakingEQFilter_OperatorDto_ManyConsts { SignalOperatorDto = signalOperatorDto, CenterFrequency = centerFrequencyMathPropertiesDto.ConstValue, BandWidth = bandWidthMathPropertiesDto.ConstValue, DBGain = dbGainMathPropertiesDto.ConstValue };
            }
            else
            {
                return new PeakingEQFilter_OperatorDto_AllVars { SignalOperatorDto = signalOperatorDto, CenterFrequencyOperatorDto = centerFrequencyOperatorDto, BandWidthOperatorDto = bandWidthOperatorDto, DBGainOperatorDto = dbGainOperatorDto };
            }
        }

        protected override OperatorDtoBase Visit_Power_OperatorDto(Power_OperatorDto dto)
        {
            base.Visit_Power_OperatorDto(dto);

            MathPropertiesDto baseMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.BaseOperatorDto);
            MathPropertiesDto exponentMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.ExponentOperatorDto);

            if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsConst)
            {
                return new Power_OperatorDto_ConstBase_ConstExponent { Base = baseMathPropertiesDto.ConstValue, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsConst)
            {
                return new Power_OperatorDto_VarBase_ConstExponent { BaseOperatorDto = dto.BaseOperatorDto, Exponent = exponentMathPropertiesDto.ConstValue };
            }
            else if (baseMathPropertiesDto.IsConst && exponentMathPropertiesDto.IsVar)
            {
                return new Power_OperatorDto_ConstBase_VarExponent { Base = baseMathPropertiesDto.ConstValue, ExponentOperatorDto = dto.ExponentOperatorDto };
            }
            else if (baseMathPropertiesDto.IsVar && exponentMathPropertiesDto.IsVar)
            {
                return new Power_OperatorDto_VarBase_VarExponent { BaseOperatorDto = dto.BaseOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Pulse_OperatorDto(Pulse_OperatorDto dto)
        {
            base.Visit_Pulse_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);
            MathPropertiesDto widthMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.WidthOperatorDto);
            bool isHalfWidth = widthMathPropertiesDto.IsConst && widthMathPropertiesDto.ConstValue == 0.5;

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsConst && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && isHalfWidth && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }
            if (frequencyMathPropertiesDto.IsConst && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_HalfWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_ConstWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_ConstFrequency_VarWidth_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && isHalfWidth && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_HalfWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_ConstWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, Width = widthMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsVar && widthMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Pulse_OperatorDto_VarFrequency_VarWidth_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto, WidthOperatorDto = dto.WidthOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Random_OperatorDto(Random_OperatorDto dto)
        {
            base.Visit_Random_OperatorDto(dto);

            Random_OperatorDto dto2;

            if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Block)
            {
                dto2 = new Random_OperatorDto_Block();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Stripe)
            {
                dto2 = new Random_OperatorDto_Stripe();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Line)
            {
                dto2 = new Random_OperatorDto_Line();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicEquidistant)
            {
                dto2 = new Random_OperatorDto_CubicEquidistant();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicAbruptSlope)
            {
                dto2 = new Random_OperatorDto_CubicAbruptSlope();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.CubicSmoothSlope)
            {
                dto2 = new Random_OperatorDto_CubicSmoothSlope();
            }
            else if (dto.ResampleInterpolationTypeEnum == ResampleInterpolationTypeEnum.Hermite)
            {
                dto2 = new Random_OperatorDto_Hermite();
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_RandomOperatorProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_RangeOverDimension_OperatorDto(RangeOverDimension_OperatorDto dto)
        {
            base.Visit_RangeOverDimension_OperatorDto(dto);

            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto tillMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TillOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            if (fromMathPropertiesDto.IsConst &&
                tillMathPropertiesDto.IsConst &&
                stepMathPropertiesDto.IsConst)
            {
                return new RangeOverDimension_OperatorCalculator_OnlyConsts
                {
                    From = fromMathPropertiesDto.ConstValue,
                    Till = tillMathPropertiesDto.ConstValue,
                    Step = stepMathPropertiesDto.ConstValue,
                    StandardDimensionEnum = dto.StandardDimensionEnum,
                    CustomDimensionName = dto.CustomDimensionName
                };
            }
            else
            {
                return new RangeOverDimension_OperatorCalculator_OnlyVars
                {
                    FromOperatorDto = dto.FromOperatorDto,
                    TillOperatorDto = dto.TillOperatorDto,
                    StepOperatorDto = dto.StepOperatorDto,
                    StandardDimensionEnum = dto.StandardDimensionEnum,
                    CustomDimensionName = dto.CustomDimensionName
                };
            }
        }

        protected override OperatorDtoBase Visit_RangeOverOutlets_OperatorDto(RangeOverOutlets_OperatorDto dto)
        {
            base.Visit_RangeOverOutlets_OperatorDto(dto);

            MathPropertiesDto fromMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FromOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);

            if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsConst)
            {
                return new RangeOverOutlets_OperatorDto_ConstFrom_ConstStep { From = fromMathPropertiesDto.ConstValue, Step = stepMathPropertiesDto.ConstValue };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst)
            {
                return new RangeOverOutlets_OperatorDto_VarFrom_ConstStep { FromOperatorDto = dto.FromOperatorDto, Step = stepMathPropertiesDto.ConstValue };
            }
            else if (fromMathPropertiesDto.IsConst && stepMathPropertiesDto.IsVar)
            {
                return new RangeOverOutlets_OperatorDto_ConstFrom_VarStep { From = fromMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto };
            }
            else if (fromMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar)
            {
                return new RangeOverOutlets_OperatorDto_VarFrom_VarStep { FromOperatorDto = dto.FromOperatorDto, StepOperatorDto = dto.StepOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
        }

        protected override OperatorDtoBase Visit_Reverse_OperatorDto(Reverse_OperatorDto dto)
        {
            base.Visit_Reverse_OperatorDto(dto);

            MathPropertiesDto speedMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SpeedOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (speedMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDtoBase_VarSpeed_WithPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, SpeedOperatorDto = dto.SpeedOperatorDto };
            }
            else if (speedMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDtoBase_VarSpeed_NoPhaseTracking { SignalOperatorDto = dto.SignalOperatorDto, SpeedOperatorDto = dto.SpeedOperatorDto };
            }
            else if (speedMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDtoBase_ConstSpeed_WithOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Speed = speedMathPropertiesDto.ConstValue };
            }
            else if (speedMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Reverse_OperatorDtoBase_ConstSpeed_NoOriginShifting { SignalOperatorDto = dto.SignalOperatorDto, Speed = speedMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Round_OperatorDto(Round_OperatorDto dto)
        {
            Visit_Round_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto stepMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.StepOperatorDto);
            MathPropertiesDto offsetMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.OffsetOperatorDto);

            if (signalMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_ConstSignal { Signal = signalMathPropertiesDto.ConstValue, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConstOne && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_StepOne_OffsetZero { SignalOperatorDto = dto.SignalOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsVar)
            {
                return new Round_OperatorDto_VarSignal_VarStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_VarStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsVar && offsetMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_VarSignal_VarStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = dto.StepOperatorDto, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsVar)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_VarOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = stepMathPropertiesDto.ConstValue, OffsetOperatorDto = dto.OffsetOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConstZero)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_ZeroOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = stepMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && stepMathPropertiesDto.IsConst && offsetMathPropertiesDto.IsConst)
            {
                return new Round_OperatorDto_VarSignal_ConstStep_ConstOffset { SignalOperatorDto = dto.SignalOperatorDto, StepOperatorDto = stepMathPropertiesDto.ConstValue, Offset = offsetMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
        }

        protected override OperatorDtoBase Visit_Sample_OperatorDto(Sample_OperatorDto dto)
        {
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            int sampleChannelCount = dto.ChannelCount;
            bool hasTargetChannelCount = sampleChannelCount == _targetChannelCount;
            bool isFromMonoToStereo = sampleChannelCount == 1 && _targetChannelCount == 2;
            bool isFromStereoToMono = sampleChannelCount == 2 && _targetChannelCount == 1;

            OperatorDtoBase dto2;

            if (hasTargetChannelCount && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (hasTargetChannelCount && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_MonoToStereo_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromMonoToStereo && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_MonoToStereo_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_ConstFrequency_StereoToMono_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (isFromStereoToMono && frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sample_OperatorDto_VarFrequency_StereoToMono_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            var asISample_OperatorDto = dto2 as ISample_OperatorDto;
            if (asISample_OperatorDto != null)
            {
                Clone_SampleOperatorProperties(dto, asISample_OperatorDto);
            }

            var asIOperatorDto_WithDimension = dto2 as IOperatorDto_WithDimension;
            if (asIOperatorDto_WithDimension != null)
            {
                Clone_DimensionProperties(dto, asIOperatorDto_WithDimension);
            }

            return dto2;
        }

        protected override OperatorDtoBase Visit_SawDown_OperatorDto(SawDown_OperatorDto dto)
        {
            base.Visit_SawDown_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawDown_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_SawUp_OperatorDto(SawUp_OperatorDto dto)
        {
            base.Visit_SawUp_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new SawUp_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Scaler_OperatorDto(Scaler_OperatorDto dto)
        {
            base.Visit_Scaler_OperatorDto(dto);

            MathPropertiesDto sourceValueAMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SourceValueAOperatorDto);
            MathPropertiesDto sourceValueBMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SourceValueBOperatorDto);
            MathPropertiesDto targetValueAMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TargetValueAOperatorDto);
            MathPropertiesDto targetValueBMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.TargetValueBOperatorDto);

            if (sourceValueAMathPropertiesDto.IsConst &&
                sourceValueBMathPropertiesDto.IsConst &&
                targetValueAMathPropertiesDto.IsConst &&
                targetValueBMathPropertiesDto.IsConst)
            {
                return new Scaler_OperatorDto_ManyConsts
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SourceValueA = sourceValueAMathPropertiesDto.ConstValue,
                    SourceValueB = sourceValueBMathPropertiesDto.ConstValue,
                    TargetValueA = targetValueAMathPropertiesDto.ConstValue,
                    TargetValueB = targetValueBMathPropertiesDto.ConstValue
                };
            }
            else
            {
                return new Scaler_OperatorDto_AllVars
                {
                    SignalOperatorDto = dto.SignalOperatorDto,
                    SourceValueAOperatorDto = dto.SourceValueAOperatorDto,
                    SourceValueBOperatorDto = dto.SourceValueBOperatorDto,
                    TargetValueAOperatorDto = dto.TargetValueAOperatorDto,
                    TargetValueBOperatorDto = dto.TargetValueBOperatorDto
                };
            }
        }

        protected override OperatorDtoBase Visit_Select_OperatorDto(Select_OperatorDto dto)
        {
            base.Visit_Select_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto positionMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.PositionOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (signalMathPropertiesDto.IsConst && positionMathPropertiesDto.IsConst)
            {
                dto2 = new Select_OperatorDto_ConstSignal_ConstPosition { Signal = signalMathPropertiesDto.ConstValue, Position = positionMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && positionMathPropertiesDto.IsConst)
            {
                dto2 = new Select_OperatorDto_VarSignal_ConstPosition { SignalOperatorDto = dto.SignalOperatorDto, Position = positionMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst && positionMathPropertiesDto.IsVar)
            {
                dto2 = new Select_OperatorDto_ConstSignal_VarPosition { Signal = signalMathPropertiesDto.ConstValue, PositionOperatorDto = dto.PositionOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && positionMathPropertiesDto.IsVar)
            {
                dto2 = new Select_OperatorDto_VarSignal_VarPosition { SignalOperatorDto = dto.SignalOperatorDto, PositionOperatorDto = dto.PositionOperatorDto };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_SetDimension_OperatorDto(SetDimension_OperatorDto dto)
        {
            base.Visit_SetDimension_OperatorDto(dto);

            MathPropertiesDto valueMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.ValueOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (valueMathProperties.IsVar)
            {
                dto2 = new SetDimension_OperatorDto_VarValue { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, ValueOperatorDto = dto.ValueOperatorDto };
            }
            else if (valueMathProperties.IsConst)
            {
                dto2 = new SetDimension_OperatorDto_ConstValue { PassThroughInputOperatorDto = dto.PassThroughInputOperatorDto, Value = valueMathProperties.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.SignalOperatorDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.DistanceOperatorDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_ConstSignal_ConstDistance { Signal = signalMathPropertiesDto.ConstValue, Distance = distanceMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance { SignalOperatorDto = dto.SignalOperatorDto, Distance = distanceMathPropertiesDto.ConstValue };
            }
            else if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_ConstSignal_VarDistance { Signal = signalMathPropertiesDto.ConstValue, DistanceOperatorDto = dto.DistanceOperatorDto };
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_VarDistance { SignalOperatorDto = dto.SignalOperatorDto, DistanceOperatorDto = dto.DistanceOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Sine_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_SortOverDimension_OperatorDto(SortOverDimension_OperatorDto dto)
        {
            base.Visit_SortOverDimension_OperatorDto(dto);

            SortOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new SortOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new SortOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Square_OperatorDto(Square_OperatorDto dto)
        {
            base.Visit_Square_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Square_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Squash_OperatorDto(Squash_OperatorDto dto)
        {
            base.Visit_Squash_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase factorOperatorDto = dto.FactorOperatorDto;
            OperatorDtoBase originOperatorDto = dto.OriginOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto factorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(factorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(originOperatorDto);

            if (dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar)
                {
                    return new Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst)
                {
                    return new Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar)
                {
                    return new Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst)
                {
                    return new Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }
            else
            {
                OperatorDtoBase_WithDimension dto2;

                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_VarOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }

                Clone_DimensionProperties(dto, dto2);

                return dto2;
            }
        }

        protected override OperatorDtoBase Visit_Stretch_OperatorDto(Stretch_OperatorDto dto)
        {
            base.Visit_Stretch_OperatorDto(dto);

            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase factorOperatorDto = dto.FactorOperatorDto;
            OperatorDtoBase originOperatorDto = dto.OriginOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto factorMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(factorOperatorDto);
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(originOperatorDto);

            if (dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar)
                {
                    return new Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst)
                {
                    return new Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar)
                {
                    return new Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst)
                {
                    return new Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }
            }
            else
            {
                OperatorDtoBase_WithDimension dto2;

                if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin { SignalOperatorDto = signalOperatorDto, FactorOperatorDto = factorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsVar && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin { SignalOperatorDto = signalOperatorDto, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsVar && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, FactorOperatorDto = factorOperatorDto, Origin = originMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsVar)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, OriginOperatorDto = originOperatorDto };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConstZero)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue };
                }
                else if (signalMathPropertiesDto.IsConst && factorMathPropertiesDto.IsConst && originMathPropertiesDto.IsConst)
                {
                    dto2 = new Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin { Signal = signalMathPropertiesDto.ConstValue, Factor = factorMathPropertiesDto.ConstValue, Origin = originMathPropertiesDto.ConstValue };
                }
                else
                {
                    throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
                }

                Clone_DimensionProperties(dto, dto2);

                return dto2;
            }
        }

        protected override OperatorDtoBase Visit_Subtract_OperatorDto(Subtract_OperatorDto dto)
        {
            base.Visit_Subtract_OperatorDto(dto);

            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Subtract_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Subtract_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue };
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Subtract_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto };
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Subtract_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto };
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_SumOverDimension_OperatorDto(SumOverDimension_OperatorDto dto)
        {
            base.Visit_SumOverDimension_OperatorDto(dto);

            SumOverDimension_OperatorDto dto2;

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    dto2 = new SumOverDimension_OperatorDto_CollectionRecalculationContinuous();
                    break;

                case CollectionRecalculationEnum.UponReset:
                    dto2 = new SumOverDimension_OperatorDto_CollectionRecalculationUponReset();
                    break;

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }

            Clone_AggregateOverDimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_TimePower_OperatorDto(TimePower_OperatorDto dto)
        {
            base.Visit_TimePower_OperatorDto(dto);

            OperatorDtoBase originOperatorDto = dto.OriginOperatorDto;
            MathPropertiesDto originMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(originOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (originMathPropertiesDto.IsVar)
            {
                dto2 = new TimePower_OperatorDto_VarOrigin { SignalOperatorDto = dto.SignalOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto, OriginOperatorDto = dto.OriginOperatorDto };
            }
            else if (originMathPropertiesDto.IsConst)
            {
                dto2 = new TimePower_OperatorDto_ConstOrigin { SignalOperatorDto = dto.SignalOperatorDto, ExponentOperatorDto = dto.ExponentOperatorDto, Origin = originMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
        }

        protected override OperatorDtoBase Visit_Triangle_OperatorDto(Triangle_OperatorDto dto)
        {
            base.Visit_Triangle_OperatorDto(dto);

            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.FrequencyOperatorDto);

            OperatorDtoBase_WithDimension dto2;

            if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsVar && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_VarFrequency_NoPhaseTracking { FrequencyOperatorDto = dto.FrequencyOperatorDto };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum == DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else if (frequencyMathPropertiesDto.IsConst && dto.StandardDimensionEnum != DimensionEnum.Time)
            {
                dto2 = new Triangle_OperatorDto_ConstFrequency_NoOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue };
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }

            Clone_DimensionProperties(dto, dto2);

            return dto2;
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

        private void Clone_InterpolateOperatorProperties(IInterpolate_OperatorDto source, IInterpolate_OperatorDto dest)
        {
            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;
            dest.SignalOperatorDto = source.SignalOperatorDto;

            Clone_DimensionProperties(source, dest);
        }

        private void Clone_RandomOperatorProperties(Random_OperatorDto source, Random_OperatorDto dest)
        {
            dest.RateOperatorDto = source.RateOperatorDto;
            dest.ResampleInterpolationTypeEnum = source.ResampleInterpolationTypeEnum;
            
            Clone_DimensionProperties(source, dest);
        }

        private void Clone_SampleOperatorProperties(ISample_OperatorDto dto, ISample_OperatorDto dto2)
        {
            dto2.Sample = dto.Sample;
            dto2.ChannelCount = dto.ChannelCount;
            dto2.InterpolationTypeEnum = dto.InterpolationTypeEnum;
        }

        // Helpers

        private static VarsConsts_MathPropetiesDto Get_VarsConsts_MathPropertiesDto(IList<OperatorDtoBase> operatorDtos)
        {
            IList<OperatorDtoBase> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();

            IList<OperatorDtoBase> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();
            IList<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).ConstValue).ToArray();

            bool hasVars = varOperatorDtos.Any();
            bool hasConsts = constOperatorDtos.Any();

            var mathPropertiesDto = new VarsConsts_MathPropetiesDto
            {
                VarOperatorDtos = varOperatorDtos,
                Consts = consts,
                HasConsts = hasConsts,
                HasVars = hasVars
            };

            return mathPropertiesDto;
        }
    }
}
