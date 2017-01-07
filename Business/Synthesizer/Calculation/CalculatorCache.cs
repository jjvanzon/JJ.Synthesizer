using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary> Caches several calculators for shared use between PatchCalculators, to save memory. </summary>
    public class CalculatorCache
    {
        private const int TOP_LEVEL_DIMENSION_STACK_INDEX = 0;

        /// <summary>
        /// This dictionary is about reusing the same CurveCalculator in multiple OperatorCalculator_Curve's
        /// in case they uses the same Curve, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Curve, ICurveCalculator> _curve_CurveCalculator_Dictionary = new Dictionary<Curve, ICurveCalculator>();
        private readonly object _curveLock = new object();

        /// <summary>
        /// This dictionary is about reusing the same SampleCalculator in multiple OperatorCalculator_Sample's
        /// in case they uses the same Sample, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Sample, ISampleCalculator> _sample_SampleCalculator_Dictionary = new Dictionary<Sample, ISampleCalculator>();
        private readonly object _sampleLock = new object();

        private readonly Dictionary<int, NoiseCalculator> _operatorID_To_NoiseCalculator_Dictionary = new Dictionary<int, NoiseCalculator>();
        private readonly object _operatorID_To_NoiseCalculator_Dictionary_Lock = new object();

        private readonly Dictionary<int, RandomCalculator_BlockInterpolation> _operatorID_To_RandomCalculator_BlockInterpolation_Dictionary = new Dictionary<int, RandomCalculator_BlockInterpolation>();
        private readonly object _operatorID_To_RandomCalculator_BlockInterpolation_Dictionary_Lock = new object();

        private readonly Dictionary<int, RandomCalculator_StripeInterpolation> _operatorID_To_RandomCalculator_StripeInterpolation_Dictionary = new Dictionary<int, RandomCalculator_StripeInterpolation>();
        private readonly object _operatorID_To_RandomCalculator_StripeInterpolation_Dictionary_Lock = new object();

        private readonly Dictionary<int, IList<ArrayCalculatorBase>> _cacheOperatorID_To_ArrayCalculators_Dictionary = new Dictionary<int, IList<ArrayCalculatorBase>>();
        private readonly object _cacheOperatorID_To_ArrayCalculators_Dictionary_Lock = new object();

        internal ICurveCalculator GetCurveCalculator(int curveID, ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            Curve curve = curveRepository.Get(curveID);

            ICurveCalculator curveCalculator = GetCurveCalculator(curve);

            return curveCalculator;
        }

        internal ICurveCalculator GetCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            lock (_curveLock)
            {
                ICurveCalculator curveCalculator;
                if (!_curve_CurveCalculator_Dictionary.TryGetValue(curve, out curveCalculator))
                {
                    curveCalculator = CurveCalculatorFactory.CreateCurveCalculator(curve);
                    _curve_CurveCalculator_Dictionary.Add(curve, curveCalculator);
                }

                return curveCalculator;
            }
        }

        internal ISampleCalculator GetSampleCalculator(int sampleID, ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            Sample sample = sampleRepository.Get(sampleID);
            byte[] bytes = sampleRepository.TryGetBytes(sampleID);

            var sampleInfo = new SampleInfo { Sample = sample, Bytes = bytes };

            ISampleCalculator sampleCalculator = GetSampleCalculator(sampleInfo);

            return sampleCalculator;
        }

        internal ISampleCalculator GetSampleCalculator(SampleInfo sampleInfo)
        {
            if (sampleInfo == null) throw new NullException(() => sampleInfo);
            if (sampleInfo.Sample == null) throw new NullException(() => sampleInfo.Sample);

            lock (_sampleLock)
            {
                ISampleCalculator sampleCalculator;
                if (!_sample_SampleCalculator_Dictionary.TryGetValue(sampleInfo.Sample, out sampleCalculator))
                {
                    sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sampleInfo.Sample, sampleInfo.Bytes);
                    _sample_SampleCalculator_Dictionary.Add(sampleInfo.Sample, sampleCalculator);
                }

                return sampleCalculator;
            }
        }

        internal NoiseCalculator GetNoiseCalculator(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_NoiseCalculator_Dictionary_Lock)
            {
                NoiseCalculator noiseCalculator;
                if (!_operatorID_To_NoiseCalculator_Dictionary.TryGetValue(operatorID, out noiseCalculator))
                {
                    noiseCalculator = new NoiseCalculator();
                    _operatorID_To_NoiseCalculator_Dictionary.Add(operatorID, noiseCalculator);
                }

                return noiseCalculator;
            }
        }

        internal RandomCalculatorBase GetRandomCalculator(Operator op)
        {
            if (op == null) throw new NullException(() => op);
            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.Random)
            {
                throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.Random);
            }

            var wrapper = new Random_OperatorWrapper(op);
            ResampleInterpolationTypeEnum resampleInterpolationType = wrapper.InterpolationType;

            RandomCalculatorBase randomCalculator = GetRandomCalculator(op.ID, resampleInterpolationType);
            return randomCalculator;
        }

        internal RandomCalculatorBase GetRandomCalculator(int operatorID, ResampleInterpolationTypeEnum resampleInterpolationType)
        {
            switch (resampleInterpolationType)
            {
                case ResampleInterpolationTypeEnum.Block:
                    return Get_RandomCalculator_BlockInterpolation(operatorID);

                case ResampleInterpolationTypeEnum.Stripe:
                case ResampleInterpolationTypeEnum.Line:
                case ResampleInterpolationTypeEnum.CubicEquidistant:
                case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                case ResampleInterpolationTypeEnum.Hermite:
                    return Get_RandomCalculator_StripeInterpolation(operatorID);

                default:
                    throw new ValueNotSupportedException(resampleInterpolationType);
            }
        }

        private RandomCalculator_StripeInterpolation Get_RandomCalculator_StripeInterpolation(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_RandomCalculator_StripeInterpolation_Dictionary_Lock)
            {
                RandomCalculator_StripeInterpolation randomCalculator;
                if (!_operatorID_To_RandomCalculator_StripeInterpolation_Dictionary.TryGetValue(operatorID, out randomCalculator))
                {
                    randomCalculator = new RandomCalculator_StripeInterpolation();
                    _operatorID_To_RandomCalculator_StripeInterpolation_Dictionary.Add(operatorID, randomCalculator);
                }

                return randomCalculator;
            }
        }

        private RandomCalculator_BlockInterpolation Get_RandomCalculator_BlockInterpolation(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_RandomCalculator_BlockInterpolation_Dictionary_Lock)
            {
                RandomCalculator_BlockInterpolation randomCalculator;
                if (!_operatorID_To_RandomCalculator_BlockInterpolation_Dictionary.TryGetValue(operatorID, out randomCalculator))
                {
                    randomCalculator = new RandomCalculator_BlockInterpolation();
                    _operatorID_To_RandomCalculator_BlockInterpolation_Dictionary.Add(operatorID, randomCalculator);
                }
                return randomCalculator;
            }
        }

        /// <summary>
        /// Out comes an array calculator for each channel.
        /// The concrete type of the ArrayCalculators is the same, 
        /// so if you have to DO something with the concrete type,
        /// you only have to check one of them.
        /// </summary>
        /// <param name="samplingRate">greater than 0</param>
        internal IList<ArrayCalculatorBase> GetCacheArrayCalculators(
            Operator op,
            OperatorCalculatorBase signalCalculator,
            double start,
            double end,
            double samplingRate,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            var wrapper = new Cache_OperatorWrapper(op);
            SpeakerSetup speakerSetup = speakerSetupRepository.Get((int)wrapper.SpeakerSetup);
            int channelCount = speakerSetup.SpeakerSetupChannels.Count;
            InterpolationTypeEnum interpolationTypeEnum = wrapper.InterpolationType;

            IList<ArrayCalculatorBase> arrayCalculators = GetCacheArrayCalculators(
                op.ID, 
                signalCalculator, 
                start, 
                end, 
                samplingRate,
                channelCount,
                interpolationTypeEnum,
                dimensionStack, 
                channelDimensionStack);

            return arrayCalculators;
        }

        internal IList<ArrayCalculatorBase> GetCacheArrayCalculators(
            int operatorID, 
            OperatorCalculatorBase signalCalculator, 
            double start, 
            double end, 
            double samplingRate,
            int channelCount,
            InterpolationTypeEnum interpolationTypeEnum,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_cacheOperatorID_To_ArrayCalculators_Dictionary_Lock)
            {
                IList<ArrayCalculatorBase> arrayCalculators;
                if (!_cacheOperatorID_To_ArrayCalculators_Dictionary.TryGetValue(operatorID, out arrayCalculators))
                {
                    arrayCalculators = CreateCacheArrayCalculators(
                        signalCalculator,
                        start,
                        end,
                        samplingRate,
                        channelCount,
                        interpolationTypeEnum,
                        dimensionStack,
                        channelDimensionStack);

                    _cacheOperatorID_To_ArrayCalculators_Dictionary.Add(operatorID, arrayCalculators);
                }

                return arrayCalculators;
            }
        }

        private IList<ArrayCalculatorBase> CreateCacheArrayCalculators(
            OperatorCalculatorBase signalCalculator,
            double start, 
            double end, 
            double rate,
            int channelCount,
            InterpolationTypeEnum interpolationTypeEnum,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (channelCount < 1) throw new LessThanException(() => channelCount, 1);
            if (Double.IsNaN(end)) throw new NaNException(() => end);
            if (Double.IsInfinity(end)) throw new InfinityException(() => end);
            if (Double.IsNaN(start)) throw new NaNException(() => start);
            if (Double.IsInfinity(start)) throw new InfinityException(() => start);
            if (Double.IsNaN(rate)) throw new NaNException(() => rate);
            if (Double.IsInfinity(rate)) throw new InfinityException(() => rate);
            if (rate <= 0.0) throw new LessThanOrEqualException(() => rate, 0.0);
            if (end <= start) throw new LessThanOrEqualException(() => end, () => start);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (channelDimensionStack == null) throw new NullException(() => channelDimensionStack);

            double length = end - start;
            int tickCount = (int)(length * rate) + 1;
            double tickLength = 1.0 / rate;

            var arrayCalculators = new ArrayCalculatorBase[channelCount];

            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
#if !USE_INVAR_INDICES
                channelDimensionStack.Set(channelIndex);
#else
                channelDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, channelIndex);
#endif
                double[] samples = new double[tickCount];

                double position = start;

#if !USE_INVAR_INDICES
                dimensionStack.Set(position);
#else
                timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
                for (int i = 0; i < tickCount; i++)
                {
                    double sample = signalCalculator.Calculate();
                    samples[i] = sample;

                    position += tickLength;

#if !USE_INVAR_INDICES
                    dimensionStack.Set(position);
#else
                    timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
                }

                ArrayCalculatorBase arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(
                    samples, 
                    rate, 
                    start, 
                    interpolationTypeEnum);

                arrayCalculators[channelIndex] = arrayCalculator;
            }

            return arrayCalculators;
        }
    }
}