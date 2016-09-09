using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal abstract class OperatorVisitorBase
    {
        private readonly IDictionary<OperatorTypeEnum, Action<Operator>> _delegateDictionary;

        public OperatorVisitorBase()
        {
            _delegateDictionary = new Dictionary<OperatorTypeEnum, Action<Operator>>
            {
                { OperatorTypeEnum.Absolute, VisitAbsolute },
                { OperatorTypeEnum.Add, VisitAdd },
                { OperatorTypeEnum.AllPassFilter, VisitAllPassFilter },
                { OperatorTypeEnum.And, VisitAnd },
                { OperatorTypeEnum.AverageFollower, VisitAverageFollower },
                { OperatorTypeEnum.AverageOverDimension, VisitAverageOverDimension },
                { OperatorTypeEnum.AverageOverInlets, VisitAverageOverInlets },
                { OperatorTypeEnum.BandPassFilterConstantPeakGain, VisitBandPassFilterConstantPeakGain },
                { OperatorTypeEnum.BandPassFilterConstantTransitionGain, VisitBandPassFilterConstantTransitionGain },
                { OperatorTypeEnum.Bundle, VisitBundle },
                { OperatorTypeEnum.Cache, VisitCache },
                { OperatorTypeEnum.ChangeTrigger, VisitChangeTrigger },
                { OperatorTypeEnum.ClosestOverInlets, VisitClosestOverInlets },
                { OperatorTypeEnum.ClosestOverInletsExp, VisitClosestOverInletsExp },
                { OperatorTypeEnum.ClosestOverDimension, VisitClosestOverDimension },
                { OperatorTypeEnum.ClosestOverDimensionExp, VisitClosestOverDimensionExp },
                { OperatorTypeEnum.Curve, VisitCurveOperator },
                { OperatorTypeEnum.CustomOperator, VisitCustomOperator },
                { OperatorTypeEnum.Divide, VisitDivide },
                { OperatorTypeEnum.Equal, VisitEqual },
                { OperatorTypeEnum.Exponent, VisitExponent },
                { OperatorTypeEnum.GetDimension, VisitGetDimension },
                { OperatorTypeEnum.GreaterThan, VisitGreaterThan },
                { OperatorTypeEnum.GreaterThanOrEqual, VisitGreaterThanOrEqual },
                { OperatorTypeEnum.Hold, VisitHold },
                { OperatorTypeEnum.HighPassFilter, VisitHighPassFilter },
                { OperatorTypeEnum.HighShelfFilter, VisitHighShelfFilter },
                { OperatorTypeEnum.If, VisitIf },
                { OperatorTypeEnum.LessThan, VisitLessThan },
                { OperatorTypeEnum.LessThanOrEqual, VisitLessThanOrEqual },
                { OperatorTypeEnum.Loop, VisitLoop },
                { OperatorTypeEnum.LowPassFilter, VisitLowPassFilter },
                { OperatorTypeEnum.LowShelfFilter, VisitLowShelfFilter },
                { OperatorTypeEnum.MakeContinuous, VisitMakeContinuous },
                { OperatorTypeEnum.MakeDiscrete, VisitMakeDiscrete },
                { OperatorTypeEnum.MaxOverInlets, VisitMaxOverInlets },
                { OperatorTypeEnum.MaxFollower, VisitMaxFollower },
                { OperatorTypeEnum.MaxOverDimension, VisitMaxOverDimension },
                { OperatorTypeEnum.MinOverInlets, VisitMinOverInlets },
                { OperatorTypeEnum.MinFollower, VisitMinFollower },
                { OperatorTypeEnum.MinOverDimension, VisitMinOverDimension },
                { OperatorTypeEnum.Multiply, VisitMultiply },
                { OperatorTypeEnum.MultiplyWithOrigin, VisitMultiplyWithOrigin },
                { OperatorTypeEnum.Negative, VisitNegative },
                { OperatorTypeEnum.Noise, VisitNoise },
                { OperatorTypeEnum.Not, VisitNot },
                { OperatorTypeEnum.NotchFilter, VisitNotchFilter },
                { OperatorTypeEnum.NotEqual, VisitNotEqual },
                { OperatorTypeEnum.Number, VisitNumber },
                { OperatorTypeEnum.OneOverX, VisitOneOverX },
                { OperatorTypeEnum.Or, VisitOr },
                { OperatorTypeEnum.PatchInlet, VisitPatchInlet },
                { OperatorTypeEnum.PatchOutlet, VisitPatchOutlet },
                { OperatorTypeEnum.PeakingEQFilter, VisitPeakingEQFilter },
                { OperatorTypeEnum.Power, VisitPower },
                { OperatorTypeEnum.Pulse, VisitPulse },
                { OperatorTypeEnum.PulseTrigger, VisitPulseTrigger },
                { OperatorTypeEnum.Random, VisitRandom },
                { OperatorTypeEnum.Range, VisitRange },
                { OperatorTypeEnum.Resample, VisitResample },
                { OperatorTypeEnum.Reset, VisitReset },
                { OperatorTypeEnum.Reverse, VisitReverse },
                { OperatorTypeEnum.Round, VisitRound },
                { OperatorTypeEnum.Sample, VisitSampleOperator },
                { OperatorTypeEnum.SawDown, VisitSawDown },
                { OperatorTypeEnum.SawUp, VisitSawUp },
                { OperatorTypeEnum.Scaler, VisitScaler },
                { OperatorTypeEnum.Select, VisitSelect },
                { OperatorTypeEnum.SetDimension, VisitSetDimension },
                { OperatorTypeEnum.Shift, VisitShift },
                { OperatorTypeEnum.Sine, VisitSine },
                { OperatorTypeEnum.SortOverInlets, VisitSortOverInlets },
                { OperatorTypeEnum.SortOverDimension, VisitSortOverDimension },
                { OperatorTypeEnum.Spectrum, VisitSpectrum },
                { OperatorTypeEnum.Square, VisitSquare },
                { OperatorTypeEnum.Squash, VisitSquash },
                { OperatorTypeEnum.Stretch, VisitStretch },
                { OperatorTypeEnum.Subtract, VisitSubtract },
                { OperatorTypeEnum.SumOverDimension, VisitSumOverDimension },
                { OperatorTypeEnum.SumFollower, VisitSumFollower },
                { OperatorTypeEnum.TimePower, VisitTimePower },
                { OperatorTypeEnum.ToggleTrigger, VisitToggleTrigger },
                { OperatorTypeEnum.Triangle, VisitTriangle },
                { OperatorTypeEnum.Unbundle, VisitUnbundle },
            };
        }

        [DebuggerHidden]
        protected virtual void VisitOperatorPolymorphic(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            Action<Operator> action;
            if (!_delegateDictionary.TryGetValue(operatorTypeEnum, out action))
            {
                throw new Exception(String.Format("No delegate defined for OperatorTypeEnum '{0}' in {1}.", operatorTypeEnum, typeof(OperatorVisitorBase).Name));
            }

            action(op);
        }

        [DebuggerHidden]
        protected virtual void VisitOperatorBase(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // TODO: Low priority: Is the trick below not specific to the OptimizedPatchCalculatorVisitor?

            // Reverse the order of evaluating the inlet,
            // so that the first inlet will be the last one pushed
            // so it will be the first one popped.
            IList<Inlet> inlets = op.Inlets.OrderByDescending(x => x.ListIndex).ToArray();
            foreach (Inlet inlet in inlets)
            {
                VisitInlet(inlet);
            }
        }

        [DebuggerHidden]
        protected virtual void VisitInlet(Inlet inlet)
        {
            Outlet outlet = inlet.InputOutlet;

            if (outlet != null)
            {
                VisitOutlet(outlet);
            }
        }

        [DebuggerHidden]
        protected virtual void VisitOutlet(Outlet outlet)
        {
            Operator op = outlet.Operator;
            VisitOperatorPolymorphic(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAbsolute(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAdd(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAllPassFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAnd(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAverageFollower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAverageOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitAverageOverInlets(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitBandPassFilterConstantPeakGain(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitBandPassFilterConstantTransitionGain(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitBundle(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitChangeTrigger(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitClosestOverInlets(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitClosestOverInletsExp(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitClosestOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitClosestOverDimensionExp(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitCache(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitCurveOperator(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitCustomOperator(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitDivide(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitEqual(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitExponent(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitGetDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitGreaterThan(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitGreaterThanOrEqual(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitHold(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitHighPassFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitHighShelfFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitIf(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitLessThan(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitLessThanOrEqual(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitLoop(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitLowPassFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitLowShelfFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMakeContinuous(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMakeDiscrete(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMaxOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMaxOverInlets(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMaxFollower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMinOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMinOverInlets(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMinFollower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMultiply(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitMultiplyWithOrigin(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSquash(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNegative(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNoise(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNot(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNotchFilter(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNotEqual(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitNumber(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitOneOverX(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitOr(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitPatchInlet(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitPatchOutlet(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitPeakingEQFilter(Operator op)
        {
            VisitOperatorBase(op);
        }
        
        [DebuggerHidden]
        protected virtual void VisitPower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitPulse(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitPulseTrigger(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitRandom(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitRange(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitResample(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitReset(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitReverse(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitRound(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSampleOperator(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSawDown(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSawUp(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitScaler(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSelect(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSetDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitShift(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSine(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSortOverInlets(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSortOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSpectrum(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSquare(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitStretch(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSubtract(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSumOverDimension(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitSumFollower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitTimePower(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitTriangle(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitToggleTrigger(Operator op)
        {
            VisitOperatorBase(op);
        }

        [DebuggerHidden]
        protected virtual void VisitUnbundle(Operator op)
        {
            VisitOperatorBase(op);
        }
    }
}
