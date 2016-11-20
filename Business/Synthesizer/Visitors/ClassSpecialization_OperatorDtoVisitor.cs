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

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    {
                        var dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationContinuous();
                        Clone_AggregateOverDimensionProperties(dto, dto2);
                        return dto2;
                    }
                case CollectionRecalculationEnum.UponReset:
                    {
                        var dto2 = new AverageOverDimension_OperatorDto_CollectionRecalculationUponReset();
                        Clone_AggregateOverDimensionProperties(dto, dto2);
                        return dto2;
                    }

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }
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

            switch (dto.ChannelCount)
            {
                case 1:
                    {
                        var dto2 = new Cache_OperatorDto_SingleChannel();
                        Clone_CacheOperatorProperties(dto, dto2);
                        return dto2;
                    }
                default:
                    {
                        var dto2 = new Cache_OperatorDto_MultiChannel();
                        Clone_CacheOperatorProperties(dto, dto2);
                        return dto2;
                    }
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_ClosestOverDimension_OperatorDto(ClosestOverDimension_OperatorDto dto)
        {
            base.Visit_ClosestOverDimension_OperatorDto(dto);

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    {
                        var dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationContinuous();
                        Clone_ClosestOverDimensionProperties(dto, dto2);
                        return dto2;
                    }
                case CollectionRecalculationEnum.UponReset:
                    {
                        var dto2 = new ClosestOverDimension_OperatorDto_CollectionRecalculationUponReset();
                        Clone_ClosestOverDimensionProperties(dto, dto2);
                        return dto2;
                    }

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }
        }

        protected override OperatorDtoBase Visit_ClosestOverDimensionExp_OperatorDto(ClosestOverDimensionExp_OperatorDto dto)
        {
            base.Visit_ClosestOverDimensionExp_OperatorDto(dto);

            switch (dto.CollectionRecalculationEnum)
            {
                case CollectionRecalculationEnum.Continuous:
                    {
                        var dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationContinuous();
                        Clone_ClosestOverDimensionProperties(dto, dto2);
                        return dto2;
                    }
                case CollectionRecalculationEnum.UponReset:
                    {
                        var dto2 = new ClosestOverDimensionExp_OperatorDto_CollectionRecalculationUponReset();
                        Clone_ClosestOverDimensionProperties(dto, dto2);
                        return dto2;
                    }

                default:
                    throw new ValueNotSupportedException(dto.CollectionRecalculationEnum);
            }
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            OperatorDtoBase inputOperatorDto = dto.InputOperatorDto;
            IList<OperatorDtoBase> itemOperatorDtos = dto.ItemOperatorDtos;

            MathPropertiesDto inputMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(inputOperatorDto);

            int count = itemOperatorDtos.Count;
            var itemMathPropertiesDtos = new MathPropertiesDto[count];
            for (int i = 0; i < count; i++)
            {
                OperatorDtoBase itemOperatorDto = itemOperatorDtos[i];
                MathPropertiesDto itemMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(itemOperatorDto);
                itemMathPropertiesDtos[i] = itemMathPropertiesDto;
            }

            bool allItemsAreConst = itemMathPropertiesDtos.All(x => x.IsConst);

            if (itemOperatorDtos.Count == 2 && allItemsAreConst)
            {
                double item1 = itemMathPropertiesDtos[0].Value;
                double item2 = itemMathPropertiesDtos[1].Value;
                return new ClosestOverInlets_OperatorDto_VarInput_2ConstItems { InputOperatorDto = inputOperatorDto, Item1 = item1, Item2 = item2 };
            }
            else if (allItemsAreConst)
            {
                IList<double> items = itemMathPropertiesDtos.Select(x => x.Value).ToArray();
                return new ClosestOverInlets_OperatorDto_VarInput_ConstItems { InputOperatorDto = inputOperatorDto, Items = items };
            }
            else
            {
                return new ClosestOverInlets_OperatorDto_AllVars { InputOperatorDto = inputOperatorDto, ItemOperatorDtos = itemOperatorDtos };
            }
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

        private void Clone_DimensionProperties(IOperatorDto_WithDimension source, IOperatorDto_WithDimension dest)
        {
            dest.CustomDimensionName = source.CustomDimensionName;
            dest.StandardDimensionEnum = source.StandardDimensionEnum;
        }
    }
}
