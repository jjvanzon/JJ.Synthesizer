using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Api
{
    public class PatchApi
    {
        private readonly PatchManager _patchManager;

        public Patch Patch
        {
            get { return _patchManager.Patch; }
        }

        public PatchApi()
        {
            _patchManager = new PatchManager(RepositoryHelper.PatchRepositories);
            _patchManager.CreatePatch();
        }

        public Absolute_OperatorWrapper Absolute(Outlet x = null)
        {
            return _patchManager.Absolute(x);
        }

        public Add_OperatorWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            return _patchManager.Add(operandA, operandB);
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            return _patchManager.Adder(operands);
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
        {
            return _patchManager.Adder(operands);
        }

        public And_OperatorWrapper And(Outlet a = null, Outlet b = null)
        {
            return _patchManager.And(a, b);
        }

        public Average_OperatorWrapper Average(Outlet signal = null, double timeSliceDuration = 0.02, int sampleCount = 100)
        {
            return _patchManager.Average(signal, timeSliceDuration, sampleCount);
        }

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            double startTime = 0.0,
            double endTime = 1.0,
            int samplingRate = 44100,
            InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line)
        {
            return _patchManager.Cache(signal, startTime, endTime, samplingRate, interpolationTypeEnum);
        }

        public Curve_OperatorWrapper Curve(Curve curve = null)
        {
            return _patchManager.Curve(curve);
        }

        public CustomOperator_OperatorWrapper CustomOperator()
        {
            return _patchManager.CustomOperator();
        }

        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch)
        {
            return _patchManager.CustomOperator(underlyingPatch);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        public Delay_OperatorWrapper Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            return _patchManager.Delay(signal, timeDifference);
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            return _patchManager.Divide(numerator, denominator, origin);
        }

        public Earlier_OperatorWrapper Earlier(Outlet signal = null, Outlet timeDifference = null)
        {
            return _patchManager.Earlier(signal, timeDifference);
        }

        public Equal_OperatorWrapper Equal(Outlet a = null, Outlet b = null)
        {
            return _patchManager.Equal(a, b);
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            return _patchManager.Exponent(low, high, ratio);
        }

        public GreaterThan_OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
        {
            return _patchManager.GreaterThan(a, b);
        }

        public GreaterThanOrEqual_OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.GreaterThanOrEqual(a, b);
        }

        public HighPassFilter_OperatorWrapper HighPassFilter(Outlet signal = null, Outlet minFrequency = null)
        {
            return _patchManager.HighPassFilter(signal, minFrequency);
        }

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            return _patchManager.If(condition, then, @else);
        }

        public LessThan_OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
        {
            return _patchManager.LessThan(a, b);
        }

        public LessThanOrEqual_OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.LessThanOrEqual(a, b);
        }

        public Loop_OperatorWrapper Loop(
            Outlet signal = null,
            Outlet attack = null,
            Outlet start = null,
            Outlet sustain = null,
            Outlet end = null,
            Outlet release = null)
        {
            return _patchManager.Loop(signal, attack, start, sustain, end, release);
        }

        public LowPassFilter_OperatorWrapper LowPassFilter(Outlet signal = null, Outlet maxFrequency = null)
        {
            return _patchManager.LowPassFilter(signal, maxFrequency);
        }

        public Maximum_OperatorWrapper Maximum(Outlet signal = null, double timeSliceDuration = 0.02, int sampleCount = 100)
        {
            return _patchManager.Maximum(signal, timeSliceDuration, sampleCount);
        }

        public Minimum_OperatorWrapper Minimum(Outlet signal = null, double timeSliceDuration = 0.02, int sampleCount = 100)
        {
            return _patchManager.Minimum(signal, timeSliceDuration, sampleCount);
        }

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            return _patchManager.Multiply(operandA, operandB, origin);
        }

        public Narrower_OperatorWrapper Narrower(Outlet signal = null, Outlet factor = null, Outlet origin = null)
        {
            return _patchManager.Narrower(signal, factor, origin);
        }

        public Negative_OperatorWrapper Negative(Outlet x = null)
        {
            return _patchManager.Negative(x);
        }

        public Noise_OperatorWrapper Noise()
        {
            return _patchManager.Noise();
        }

        public Not_OperatorWrapper Not(Outlet x = null)
        {
            return _patchManager.Not(x);
        }

        public NotEqual_OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.NotEqual(a, b);
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            return _patchManager.Number(number);
        }

        public OneOverX_OperatorWrapper OneOverX(Outlet x = null)
        {
            return _patchManager.OneOverX(x);
        }

        public Or_OperatorWrapper Or(Outlet a = null, Outlet b = null)
        {
            return _patchManager.Or(a, b);
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            return _patchManager.PatchInlet();
        }

        public PatchInlet_OperatorWrapper PatchInlet(InletTypeEnum inletTypeEnum)
        {
            return _patchManager.PatchInlet(inletTypeEnum);
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            return _patchManager.PatchInlet(name);
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            return _patchManager.PatchInlet(name, defaultValue);
        }

        public PatchInlet_OperatorWrapper PatchInlet(InletTypeEnum inletTypeEnum, double defaultValue)
        {
            return _patchManager.PatchInlet(inletTypeEnum, defaultValue);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            return _patchManager.PatchOutlet(input);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(OutletTypeEnum outletTypeEnum, Outlet input = null)
        {
            return _patchManager.PatchOutlet(outletTypeEnum, input);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            return _patchManager.PatchOutlet(name, input);
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            return _patchManager.Power(@base, exponent);
        }

        public Pulse_OperatorWrapper Pulse(Outlet frequency = null, Outlet width = null, Outlet phaseShift = null)
        {
            return _patchManager.Pulse(frequency, width, phaseShift);
        }

        public Random_OperatorWrapper Random(Outlet rate = null, Outlet phaseShift = null)
        {
            return _patchManager.Random(rate, phaseShift);
        }

        public Resample_OperatorWrapper Resample(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothInclination)
        {
            return _patchManager.Resample(signal, samplingRate, interpolationType);
        }

        public Reset_OperatorWrapper Reset(Outlet operand = null)
        {
            return _patchManager.Reset(operand);
        }

        public Reverse_OperatorWrapper Reverse(Outlet signal = null, Outlet speed = null)
        {
            return _patchManager.Reverse(signal, speed);
        }

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            return _patchManager.Round(signal, step, offset);
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            return _patchManager.Sample(sample);
        }

        public SawDown_OperatorWrapper SawDown(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SawDown(frequency, phaseShift);
        }

        public SawUp_OperatorWrapper SawUp(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SawUp(frequency, phaseShift);
        }

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
        {
            return _patchManager.Scaler(signal, sourceValueA, sourceValueB, targetValueA, targetValueB);
        }

        public Select_OperatorWrapper Select(Outlet signal = null, Outlet time = null)
        {
            return _patchManager.Select(signal, time);
        }

        public Shift_OperatorWrapper Shift(Outlet signal = null, Outlet difference = null)
        {
            return _patchManager.Shift(signal, difference);
        }

        public Sine_OperatorWrapper Sine(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.Sine(frequency, phaseShift);
        }

        public SlowDown_OperatorWrapper SlowDown(Outlet signal = null, Outlet factor = null)
        {
            return _patchManager.SlowDown(signal, factor);
        }

        public Spectrum_OperatorWrapper Spectrum(
            Outlet signal = null,
            double startTime = 0.0,
            double endTime = 1.0,
            int samplingRate = 44100)
        {
            return _patchManager.Spectrum(signal, startTime, endTime, samplingRate);
        }

        public SpeedUp_OperatorWrapper SpeedUp(Outlet signal = null, Outlet factor = null)
        {
            return _patchManager.SpeedUp(signal, factor);
        }

        public Square_OperatorWrapper Square(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.Square(frequency, phaseShift);
        }

        public Stretch_OperatorWrapper Stretch(Outlet signal = null, Outlet factor = null, Outlet origin = null)
        {
            return _patchManager.Stretch(signal, factor, origin);
        }

        public Subtract_OperatorWrapper Subtract(Outlet operandA = null, Outlet operandB = null)
        {
            return _patchManager.Subtract(operandA, operandB);
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            return _patchManager.TimePower(signal, exponent, origin);
        }

        public Triangle_OperatorWrapper Triangle(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.Triangle(frequency, phaseShift);
        }

        public IPatchCalculator CreateCalculator(CalculatorCache calculatorCache, params Outlet[] channelOutlets)
        {
            return _patchManager.CreateCalculator(calculatorCache, channelOutlets);
        }
    }
}
