using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class ClassSpecialization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDto Execute(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }

        protected override OperatorDto Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            IList<OperatorDto> childOperatorDtos = dto.ChildOperatorDtos;

            childOperatorDtos = TruncateOperatorDtoList(childOperatorDtos, x => x.Sum());

            // Get rid of const zero.
            childOperatorDtos.TryRemoveFirst(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConstZero);

            switch (childOperatorDtos.Count)
            {
                case 0:
                    {
                        OperatorDto dto2 = new Number_OperatorDto_Zero();
                        return dto2;
                    }

                case 1:
                    {
                        OperatorDto dto2 = childOperatorDtos[0];
                        return dto2;
                    }

                default:
                    OperatorDto constChildOperatorDto = childOperatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst)
                                                                         .SingleOrDefault();
                    if (constChildOperatorDto == null)
                    {
                        OperatorDto dto2 = new Add_OperatorDto_Vars(childOperatorDtos);
                        return dto2;
                    }
                    else
                    {
                        IList<OperatorDto> varChildOperatorDtos = childOperatorDtos.Except(constChildOperatorDto).ToArray();
                        double constValue = MathPropertiesHelper.GetMathPropertiesDto(constChildOperatorDto).Value;
                        OperatorDto dto2 = new Add_OperatorDto_Vars_1Const(varChildOperatorDtos, constValue);
                        return dto2;
                    }
            }
        }

        protected override OperatorDto Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            OperatorDto aOperatorDto = dto.AOperatorDto;
            OperatorDto bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_ConstA_ConstB(aMathPropertiesDto.Value, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB(aOperatorDto, bMathPropertiesDto.Value);
            }

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_ConstA_VarB(aMathPropertiesDto.Value, bOperatorDto);
            }

            if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return new Multiply_OperatorDto_VarA_VarB(bOperatorDto, aOperatorDto);

            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            OperatorDto signalOperatorDto = dto.SignalOperatorDto;
            OperatorDto distanceOperatorDto = dto.DistanceOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(distanceOperatorDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_ConstSignal_ConstDistance(signalMathPropertiesDto.Value, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance(signalOperatorDto, distanceMathPropertiesDto.Value);
            }

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_ConstSignal_VarDistance(signalMathPropertiesDto.Value, distanceOperatorDto);
            }

            if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_VarDistance(signalOperatorDto, distanceOperatorDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDto Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            OperatorDto frequencyOperatorDto = dto.FrequencyOperatorDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(frequencyOperatorDto);

            if (frequencyMathPropertiesDto.IsConst)
            {
                return new Sine_OperatorDto_ConstFrequency_NoOriginShifting(frequencyMathPropertiesDto.Value);
            }

            if (frequencyMathPropertiesDto.IsVar)
            {
                return new Sine_OperatorDto_VarFrequency_WithPhaseTracking(frequencyOperatorDto);
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        // Helpers

        /// <summary> Collapses invariant operands to one and gets rid of nulls. </summary>
        /// <param name="constantsCombiningDelegate">The method that combines multiple invariant doubles to one.</param>
        private static IList<OperatorDto> TruncateOperatorDtoList(
            IList<OperatorDto> operatorDtos,
            Func<IEnumerable<double>, double> constantsCombiningDelegate)
        {
            IList<OperatorDto> constOperatorDtos = operatorDtos.Where(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConst).ToArray();
            IList<OperatorDto> varOperatorDtos = operatorDtos.Except(constOperatorDtos).ToArray();

            OperatorDto aggregatedConstOperatorDto = null;
            if (constOperatorDtos.Count != 0)
            {
                IEnumerable<double> consts = constOperatorDtos.Select(x => MathPropertiesHelper.GetMathPropertiesDto(x).Value);
                double aggregatedConsts = constantsCombiningDelegate(consts);
                aggregatedConstOperatorDto = new Number_OperatorDto(aggregatedConsts);
            }

            IList<OperatorDto> truncatedOperatorDtoList = varOperatorDtos.Union(aggregatedConstOperatorDto)
                                                                         .Where(x => x != null)
                                                                         .ToList();
            return truncatedOperatorDtoList;
        }
    }
}