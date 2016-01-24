using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer;

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

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            return _patchManager.Exponent(low, high, ratio);
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

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            return _patchManager.Multiply(operandA, operandB, origin);
        }

        public Narrower_OperatorWrapper Narrower(Outlet signal = null, Outlet factor = null, Outlet origin = null)
        {
            return _patchManager.Narrower(signal, factor, origin);
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            return _patchManager.Number(number);
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

        public Resample_OperatorWrapper Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            return _patchManager.Resample(signal, samplingRate);
        }

        public Reset_OperatorWrapper Reset(Outlet operand = null)
        {
            return _patchManager.Reset(operand);
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            return _patchManager.Sample(sample);
        }

        public SawTooth_OperatorWrapper SawTooth(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SawTooth(frequency, phaseShift);
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

        public SpeedUp_OperatorWrapper SpeedUp(Outlet signal = null, Outlet factor = null)
        {
            return _patchManager.SpeedUp(signal, factor);
        }

        public SquareWave_OperatorWrapper SquareWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SquareWave(frequency, phaseShift);
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

        public TriangleWave_OperatorWrapper TriangleWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.TriangleWave(frequency, phaseShift);
        }

        public WhiteNoise_OperatorWrapper WhiteNoise()
        {
            return _patchManager.WhiteNoise();
        }

        public IPatchCalculator CreateOptimizedCalculator(params Outlet[] channelOutlets)
        {
            return _patchManager.CreateOptimizedCalculator(channelOutlets);
        }

        public IPatchCalculator CreateInterpretedCalculator(params Outlet[] channelOutlets)
        {
            return _patchManager.CreateInterpretedCalculator(channelOutlets);
        }
    }
}
