using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Api
{
    public class PatchApi
    {
        private PatchManager _patchManager;

        public Patch Patch { get; private set; }

        public PatchApi()
        {
            _patchManager = new PatchManager(RepositoryHelper.PatchRepositories);
            _patchManager.CreatePatch();
        }

        public OperatorWrapper_Add Add(Outlet operandA = null, Outlet operandB = null)
        {
            return _patchManager.Add(operandA, operandB);
        }

        public OperatorWrapper_Adder Adder(params Outlet[] operands)
        {
            return _patchManager.Adder(operands);
        }

        public OperatorWrapper_Adder Adder(IList<Outlet> operands)
        {
            return _patchManager.Adder(operands);
        }

        public OperatorWrapper_Curve Curve(Curve curve = null)
        {
            return _patchManager.Curve(curve);
        }

        public OperatorWrapper_CustomOperator CustomOperator()
        {
            return _patchManager.CustomOperator();
        }

        public OperatorWrapper_CustomOperator CustomOperator(Patch underlyingPatch)
        {
            return _patchManager.CustomOperator(underlyingPatch);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_CustomOperator CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_CustomOperator CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        public OperatorWrapper_Delay Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            return _patchManager.Delay(signal, timeDifference);
        }

        public OperatorWrapper_Divide Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            return _patchManager.Divide(numerator, denominator, origin);
        }

        public OperatorWrapper_Exponent Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            return _patchManager.Exponent(low, high, ratio);
        }

        public OperatorWrapper_Loop Loop(
            Outlet signal = null,
            Outlet attack = null,
            Outlet start = null,
            Outlet sustain = null,
            Outlet end = null,
            Outlet release = null)
        {
            return _patchManager.Loop(signal, attack, start, sustain, end, release);
        }

        public OperatorWrapper_Multiply Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            return _patchManager.Multiply(operandA, operandB, origin);
        }

        public OperatorWrapper_Number Number(double number = 0)
        {
            return _patchManager.Number(number);
        }

        public OperatorWrapper_PatchInlet PatchInlet()
        {
            return _patchManager.PatchInlet();
        }

        public OperatorWrapper_PatchOutlet PatchOutlet(Outlet input = null)
        {
            return _patchManager.PatchOutlet(input);
        }

        public OperatorWrapper_Power Power(Outlet @base = null, Outlet exponent = null)
        {
            return _patchManager.Power(@base, exponent);
        }

        public OperatorWrapper_Resample Resample(Outlet signal = null, Outlet samplingRate = null)
        {
            return _patchManager.Resample(signal, samplingRate);
        }

        public OperatorWrapper_SawTooth SawTooth(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SawTooth(frequency, phaseShift);
        }

        public OperatorWrapper_Sample Sample(Sample sample = null)
        {
            return _patchManager.Sample(sample);
        }

        public OperatorWrapper_Select Select(Outlet signal = null, Outlet time = null)
        {
            return _patchManager.Select(signal, time);
        }

        public OperatorWrapper_Sine Sine(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.Sine(frequency, phaseShift);
        }

        public OperatorWrapper_Subtract Subtract(Outlet operandA = null, Outlet operandB = null)
        {
            return _patchManager.Subtract(operandA, operandB);
        }

        public OperatorWrapper_SpeedUp SpeedUp(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            return _patchManager.SpeedUp(signal, timeDivider, origin);
        }

        public OperatorWrapper_SlowDown SlowDown(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            return _patchManager.SlowDown(signal, timeMultiplier, origin);
        }

        public OperatorWrapper_SquareWave SquareWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.SquareWave(frequency, phaseShift);
        }

        public OperatorWrapper_TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            return _patchManager.TimePower(signal, exponent, origin);
        }

        public OperatorWrapper_Earlier Earlier(Outlet signal = null, Outlet timeDifference = null)
        {
            return _patchManager.Earlier(signal, timeDifference);
        }

        public OperatorWrapper_TriangleWave TriangleWave(Outlet frequency = null, Outlet phaseShift = null)
        {
            return _patchManager.TriangleWave(frequency, phaseShift);
        }

        public OperatorWrapper_WhiteNoise WhiteNoise()
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
