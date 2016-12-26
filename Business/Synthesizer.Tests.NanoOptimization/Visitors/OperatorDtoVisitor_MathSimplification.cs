using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class OperatorDtoVisitor_MathSimplification : OperatorDtoVisitorBase
    {
        // General

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            // NaN / Infinity

            // Depth-first, so deeply pre-calculated NaN's can be picked up.
            OperatorDtoBase dto2 = base.Visit_OperatorDto_Polymorphic(dto);

            bool anyInputsHaveSpecialValue = dto2.InputOperatorDtos.Any(x => MathPropertiesHelper.GetMathPropertiesDto(x).IsConstSpecialValue);
            if (anyInputsHaveSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            return dto2;
        }

        // Add

        protected override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            base.Visit_Add_OperatorDto(dto);

            // Class Specialization
            VarsConsts_MathPropertiesDto mathPropertiesDto = MathPropertiesHelper.Get_VarsConsts_MathPropertiesDto(dto.InputOperatorDtos);

            if (mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return Visit_OperatorDto_Polymorphic(new Add_OperatorDto_Vars_Consts { Vars = mathPropertiesDto.Vars, Consts = mathPropertiesDto.Consts });
            }
            else if (mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return Visit_OperatorDto_Polymorphic(new Add_OperatorDto_Vars_NoConsts { Vars = mathPropertiesDto.Vars });
            }
            else if (!mathPropertiesDto.HasVars && mathPropertiesDto.HasConsts)
            {
                return Visit_OperatorDto_Polymorphic(new Add_OperatorDto_NoVars_Consts { Consts = mathPropertiesDto.Consts });
            }
            else if (!mathPropertiesDto.HasVars && !mathPropertiesDto.HasConsts)
            {
                return Visit_OperatorDto_Polymorphic(new Add_OperatorDto_NoVars_NoConsts());
            }
            else
            {
                throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
            }
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            return Process_NoVars_Consts(dto, Enumerable.Sum);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            return Process_NoVars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            return Process_Vars_NoConsts(dto);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            base.Visit_Add_OperatorDto_Vars_Consts(dto);

            // Pre-calculate
            double constValue = dto.Consts.Sum();

            var dto2 = new Add_OperatorDto_Vars_1Const { Vars = dto.Vars, ConstValue = constValue };

            return Visit_Add_OperatorDto_Vars_1Const(dto2);
        }

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            MathPropertiesDto constMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.ConstValue);

            // Identity
            if (constMathProperties.IsConstZero)
            {
                var dto2 = new Add_OperatorDto_Vars_NoConsts { Vars = dto.Vars };
                return Visit_Add_OperatorDto_Vars_NoConsts(dto2);
            }

            return dto;
        }

        // Multiply

        protected override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            base.Visit_Multiply_OperatorDto(dto);

            // Class Specialization
            OperatorDtoBase aOperatorDto = dto.AOperatorDto;
            OperatorDtoBase bOperatorDto = dto.BOperatorDto;

            MathPropertiesDto aMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(aOperatorDto);
            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(bOperatorDto);

            if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsConst)
            {
                return Visit_OperatorDto_Polymorphic(new Multiply_OperatorDto_ConstA_ConstB { A = aMathPropertiesDto.ConstValue, B = bMathPropertiesDto.ConstValue });
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsConst)
            {
                return Visit_OperatorDto_Polymorphic(new Multiply_OperatorDto_VarA_ConstB { AOperatorDto = aOperatorDto, B = bMathPropertiesDto.ConstValue });
            }
            else if (aMathPropertiesDto.IsConst && bMathPropertiesDto.IsVar)
            {
                return Visit_OperatorDto_Polymorphic(new Multiply_OperatorDto_ConstA_VarB { A = aMathPropertiesDto.ConstValue, BOperatorDto = bOperatorDto });
            }
            else if (aMathPropertiesDto.IsVar && bMathPropertiesDto.IsVar)
            {
                return Visit_OperatorDto_Polymorphic(new Multiply_OperatorDto_VarA_VarB { AOperatorDto = aOperatorDto, BOperatorDto = bOperatorDto });
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_ConstB(dto);

            // Pre-calculate
            return new Number_OperatorDto { Number = dto.A * dto.B };
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_ConstA_VarB(dto);

            // Commute
            var dto2 = new Multiply_OperatorDto_VarA_ConstB { AOperatorDto = dto.BOperatorDto, B = dto.A };

            return Visit_Multiply_OperatorDto_VarA_ConstB(dto2);
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            MathPropertiesDto bMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(dto.B);

            // 0
            if (bMathPropertiesDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            // Identity
            if (bMathPropertiesDto.IsConstOne)
            {
                return dto.AOperatorDto;
            }

            return dto;
        }

        // Shift

        protected override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            base.Visit_Shift_OperatorDto(dto);

            // Class Specialization
            OperatorDtoBase signalOperatorDto = dto.SignalOperatorDto;
            OperatorDtoBase distanceOperatorDto = dto.DistanceOperatorDto;

            MathPropertiesDto signalMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(signalOperatorDto);
            MathPropertiesDto distanceMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(distanceOperatorDto);

            if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsConst)
            {
                return Visit_OperatorDto_Polymorphic(new Shift_OperatorDto_ConstSignal_ConstDistance { Signal = signalMathPropertiesDto.ConstValue, Distance = distanceMathPropertiesDto.ConstValue });
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsConst)
            {
                return Visit_OperatorDto_Polymorphic(new Shift_OperatorDto_VarSignal_ConstDistance { SignalOperatorDto = signalOperatorDto, Distance = distanceMathPropertiesDto.ConstValue });
            }
            else if (signalMathPropertiesDto.IsConst && distanceMathPropertiesDto.IsVar)
            {
                return Visit_OperatorDto_Polymorphic(new Shift_OperatorDto_ConstSignal_VarDistance { Signal = signalMathPropertiesDto.ConstValue, DistanceOperatorDto = distanceOperatorDto });
            }
            else if (signalMathPropertiesDto.IsVar && distanceMathPropertiesDto.IsVar)
            {
                return Visit_OperatorDto_Polymorphic(new Shift_OperatorDto_VarSignal_VarDistance { SignalOperatorDto = signalOperatorDto, DistanceOperatorDto = distanceOperatorDto });
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return Process_ConstSignal_Identity(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return Process_ConstSignal_Identity(dto.Signal);
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            MathPropertiesDto distanceMathProperties = MathPropertiesHelper.GetMathPropertiesDto(dto.Distance);

            if (distanceMathProperties.IsConstZero)
            {
                // Identity
                return dto.SignalOperatorDto;
            }

            return dto;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return Process_Nothing(dto);
        }

        // Sine

        protected override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            base.Visit_Sine_OperatorDto(dto);

            // Class Specialization
            OperatorDtoBase frequencyOperatorDto = dto.FrequencyOperatorDto;
            MathPropertiesDto frequencyMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(frequencyOperatorDto);

            if (frequencyMathPropertiesDto.IsConstZero)
            {
                return Visit_OperatorDto_Polymorphic(new Sine_OperatorDto_ZeroFrequency());
            }
            else if (frequencyMathPropertiesDto.IsConst)
            {
                return Visit_OperatorDto_Polymorphic(new Sine_OperatorDto_ConstFrequency_WithOriginShifting { Frequency = frequencyMathPropertiesDto.ConstValue });
            }
            else if (frequencyMathPropertiesDto.IsVar)
            {
                return Visit_OperatorDto_Polymorphic(new Sine_OperatorDto_VarFrequency_WithPhaseTracking { FrequencyOperatorDto = frequencyOperatorDto });
            }

            throw new VisitationCannotBeHandledException(MethodBase.GetCurrentMethod());
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto)
        {
            base.Visit_Sine_OperatorDto_ZeroFrequency(dto);

            // 0
            return new Number_OperatorDto_Zero();
        }

        // Helpers

        /// <summary> 
        /// For overrides that do not add any processing. 
        /// They are overridden for maintainability purposes,
        /// so only new virtual methods show up when typing 'override'.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OperatorDtoBase Process_Nothing(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        private OperatorDtoBase Process_NoVars_Consts(
            OperatorDtoBase_Consts dto,
            Func<IEnumerable<double>, double> aggregationDelegate)
        {
            base.Visit_OperatorDto_Base(dto);

            // Pre-calculate
            double result = aggregationDelegate(dto.Consts);

            return new Number_OperatorDto { Number = result };
        }

        private OperatorDtoBase Process_NoVars_NoConsts(OperatorDtoBase dto)
        {
            base.Visit_OperatorDto_Base(dto);

            // 0
            return new Number_OperatorDto_Zero();
        }

        private OperatorDtoBase Process_Vars_NoConsts(OperatorDtoBase_Vars dto)
        {
            base.Visit_OperatorDto_Base(dto);

            switch (dto.Vars.Count)
            {
                case 0:
                    // 0
                    return new Number_OperatorDto_Zero();

                case 1:
                    return dto.Vars[0];

                default:
                    return dto;
            }
        }

        private OperatorDtoBase Process_ConstSignal_Identity(double signal)
        {
            // Identity
            return new Number_OperatorDto { Number = signal };
        }
    }
}