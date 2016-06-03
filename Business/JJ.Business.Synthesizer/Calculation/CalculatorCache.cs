using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

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

        private readonly Dictionary<int, IList<ArrayCalculatorBase>> _cacheOperatorID_To_ArrayCalculators_Dictionary = new Dictionary<int, IList<ArrayCalculatorBase>>();
        private readonly object _cacheOperatorID_To_ArrayCalculators_Dictionary_Lock = new object();

        /// <summary> Caches several calculators for shared use between PatchCalculators, to save memory. </summary>
        public CalculatorCache()
        {
            int assumedSamplingRate = 44100;
            NoiseCalculator = new NoiseCalculator(assumedSamplingRate);
        }

        internal NoiseCalculator NoiseCalculator { get; private set; }

        internal ICurveCalculator GetCurveCalculator(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            lock (_curveLock)
            {
                ICurveCalculator curveCalculator;
                if (!_curve_CurveCalculator_Dictionary.TryGetValue(curve, out curveCalculator))
                {
                    curveCalculator =  CurveCalculatorFactory.CreateCurveCalculator(curve);
                    _curve_CurveCalculator_Dictionary.Add(curve, curveCalculator);
                }

                return curveCalculator;
            }
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

        /// <summary>
        /// Out comes an array calculator for each channel.
        /// The concrete type of the ArrayCalculators is the same, 
        /// so if you have to something with the concrete type,
        /// you only have to check one of them.
        /// </summary>
        /// <param name="samplingRate">greater than 0</param>
        internal IList<ArrayCalculatorBase> GetCacheArrayCalculators(
            Operator op, 
            OperatorCalculatorBase signalCalculator,
            double startTime,
            double endTime,
            int samplingRate,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            lock (_cacheOperatorID_To_ArrayCalculators_Dictionary_Lock)
            {
                IList<ArrayCalculatorBase> arrayCalculators;
                if (!_cacheOperatorID_To_ArrayCalculators_Dictionary.TryGetValue(op.ID, out arrayCalculators))
                {
                    var wrapper = new Cache_OperatorWrapper(op);

                    SpeakerSetup speakerSetup = speakerSetupRepository.Get((int)wrapper.SpeakerSetup);
                    int channelCount = speakerSetup.SpeakerSetupChannels.Count;

                    arrayCalculators = CreateCacheArrayCalculators(
                        signalCalculator,
                        channelCount,
                        startTime,
                        endTime,
                        samplingRate,
                        wrapper.InterpolationType);

                    _cacheOperatorID_To_ArrayCalculators_Dictionary.Add(op.ID, arrayCalculators);
                }

                return arrayCalculators;
            }
        }

        // TODO: Low priorty: why is rate an int in GetCacheArrayCalculators, but a double in CreateCacheArrayCalculators?

        private IList<ArrayCalculatorBase> CreateCacheArrayCalculators(
            OperatorCalculatorBase signalCalculator,
            int channelCount, double startTime, double endTime, double rate,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (channelCount < 1) throw new LessThanException(() => channelCount, 1);
            if (Double.IsNaN(endTime)) throw new NaNException(() => endTime);
            if (Double.IsInfinity(endTime)) throw new InfinityException(() => endTime);
            if (Double.IsNaN(startTime)) throw new NaNException(() => startTime);
            if (Double.IsInfinity(startTime)) throw new InfinityException(() => startTime);
            if (Double.IsNaN(rate)) throw new NaNException(() => rate);
            if (Double.IsInfinity(rate)) throw new InfinityException(() => rate);
            if (rate <= 0.0) throw new LessThanOrEqualException(() => rate, 0.0);
            if (endTime <= startTime) throw new LessThanOrEqualException(() => endTime, () => startTime);

            double duration = endTime - startTime;
            int tickCount = (int)(duration * rate) + 1;
            double tickDuration = 1.0 / rate;

            var arrayCalculators = new ArrayCalculatorBase[channelCount];

            var dimensionStackCollection = new DimensionStackCollection();
            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
                // TODO: This could be done faster, by tapping into a specific DimensionStack.
#if !USE_INVAR_INDICES
                dimensionStackCollection.Set(DimensionEnum.Channel, channelIndex);
#else
                dimensionStackCollection.Set(DimensionEnum.Channel, TOP_LEVEL_DIMENSION_STACK_INDEX, channelIndex);
#endif
                double[] samples = new double[tickCount];

                double time = startTime;

                // TODO: This could be done faster, by tapping into a specific DimensionStack.
#if !USE_INVAR_INDICES
                dimensionStackCollection.Set(DimensionEnum.Time, time);
#else
                dimensionStackCollection.Set(DimensionEnum.Time, TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif

                for (int i = 0; i < tickCount; i++)
                {
                    double sample = signalCalculator.Calculate();
                    samples[i] = sample;

                    time += tickDuration;

                    // TODO: This could be done faster, by tapping into a specific DimensionStack.
#if !USE_INVAR_INDICES
                    dimensionStackCollection.Set(DimensionEnum.Time, time);
#else
                    dimensionStackCollection.Set(DimensionEnum.Time, TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
                }

                ArrayCalculatorBase arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(
                    samples, rate, startTime, interpolationTypeEnum);
                arrayCalculators[channelIndex] = arrayCalculator;
            }

            return arrayCalculators;
        }
    }
}