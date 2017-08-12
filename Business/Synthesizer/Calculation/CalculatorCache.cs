using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary> Caches several calculators for shared use between PatchCalculators, to save memory. </summary>
    public class CalculatorCache
    {
        private const double DEFAULT_VALUE_BEFORE = 0.0;
        private const double DEFAULT_VALUE_AFTER = 0.0;
        
        /// <summary>
        /// This dictionary is about reusing the same CurveCalculator in multiple OperatorCalculator_Curve's
        /// in case they uses the same Curve, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Curve, ArrayDto> _curve_To_Calculator_Dictionary = new Dictionary<Curve, ArrayDto>();
        private readonly object _curveLock = new object();

        /// <summary>
        /// This dictionary is about reusing the same SampleCalculator in multiple OperatorCalculator_Sample's
        /// in case they uses the same Sample, more than optimizing things by using a dictionary.
        /// </summary>
        private readonly Dictionary<Sample, IList<ArrayDto>> _sample_To_ArrayDtos_Dictionary = new Dictionary<Sample, IList<ArrayDto>>();
        private readonly object _sampleLock = new object();

        private readonly Dictionary<int, NoiseCalculator> _operatorID_To_NoiseCalculator_Dictionary = new Dictionary<int, NoiseCalculator>();
        private readonly object _operatorID_To_NoiseCalculator_Dictionary_Lock = new object();

        private readonly Dictionary<int, RandomCalculator_Block> _operatorID_To_RandomCalculator_Block_Dictionary = new Dictionary<int, RandomCalculator_Block>();
        private readonly object _operatorID_To_RandomCalculator_Block_Dictionary_Lock = new object();

        private readonly Dictionary<int, RandomCalculator_Stripe> _operatorID_To_RandomCalculator_Stripe_Dictionary = new Dictionary<int, RandomCalculator_Stripe>();
        private readonly object _operatorID_To_RandomCalculator_Stripe_Dictionary_Lock = new object();

        private readonly Dictionary<int, IList<ArrayDto>> _cacheOperatorID_To_ArrayDtos_Dictionary = new Dictionary<int, IList<ArrayDto>>();
        private readonly object _cacheOperatorID_To_ArrayDtos_Dictionary_Lock = new object();

        private static readonly ArrayDto _randomArrayDto_Block = new ArrayDto
        {
            Array = RandomCalculatorHelper.Samples,
            InterpolationTypeEnum = InterpolationTypeEnum.Block,
            Rate = 1,
            IsRotating = true
        };

        private static readonly ArrayDto _randomArrayDto_Stripe = new ArrayDto
        {
            Array = RandomCalculatorHelper.Samples,
            InterpolationTypeEnum = InterpolationTypeEnum.Stripe,
            Rate = 1,
            IsRotating = true
        };

        private static readonly ArrayDto _noiseArrayDto = new ArrayDto
        {
            Array = NoiseCalculatorHelper.Samples,
            InterpolationTypeEnum = InterpolationTypeEnum.Block,
            Rate = NoiseCalculatorHelper.SamplingRate,
            IsRotating = true
        };

        internal ArrayDto GetCurveArrayDto(int curveID, ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            Curve curve = curveRepository.Get(curveID);

            ArrayDto curveCalculator = GetCurveArrayDto(curve);

            return curveCalculator;
        }

        internal ArrayDto GetCurveArrayDto(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            lock (_curveLock)
            {
                // ReSharper disable once InvertIf
                if (!_curve_To_Calculator_Dictionary.TryGetValue(curve, out ArrayDto arrayDto))
                {
                    arrayDto = CurveArrayDtoFactory.ConvertToArrayDto(curve);
                    _curve_To_Calculator_Dictionary.Add(curve, arrayDto);
                }

                return arrayDto;
            }
        }

        /// <summary> Returns one calculator for each channel. </summary>
        internal IList<ArrayDto> GetSampleArrayDtos(int sampleID, ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            Sample sample = sampleRepository.Get(sampleID);
            byte[] bytes = sampleRepository.TryGetBytes(sampleID);

            var sampleInfo = new SampleInfo { Sample = sample, Bytes = bytes };

            IList<ArrayDto> calculators = GetSampleArrayDtos(sampleInfo);

            return calculators;
        }

        /// <summary> Returns one calculator for each channel. </summary>
        internal IList<ArrayDto> GetSampleArrayDtos(SampleInfo sampleInfo)
        {
            if (sampleInfo == null) throw new NullException(() => sampleInfo);
            if (sampleInfo.Sample == null) throw new NullException(() => sampleInfo.Sample);

            lock (_sampleLock)
            {
                // ReSharper disable once InvertIf
                if (!_sample_To_ArrayDtos_Dictionary.TryGetValue(sampleInfo.Sample, out IList<ArrayDto> calculators))
                {
                    calculators = SampleArrayDtoFactory.CreateArrayDtos(sampleInfo.Sample, sampleInfo.Bytes);
                    _sample_To_ArrayDtos_Dictionary.Add(sampleInfo.Sample, calculators);
                }

                return calculators;
            }
        }

        internal NoiseCalculator GetNoiseCalculator(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_NoiseCalculator_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operatorID_To_NoiseCalculator_Dictionary.TryGetValue(operatorID, out NoiseCalculator noiseCalculator))
                {
                    noiseCalculator = new NoiseCalculator();
                    _operatorID_To_NoiseCalculator_Dictionary.Add(operatorID, noiseCalculator);
                }

                return noiseCalculator;
            }
        }

        internal ArrayDto GetNoiseArrayDto() => _noiseArrayDto;

        internal RandomCalculatorBase GetRandomCalculator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

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
                    return GetRandomCalculator_Block(operatorID);

                case ResampleInterpolationTypeEnum.Stripe:
                case ResampleInterpolationTypeEnum.Line:
                case ResampleInterpolationTypeEnum.CubicEquidistant:
                case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                case ResampleInterpolationTypeEnum.Hermite:
                    return GetRandomCalculator_Stripe(operatorID);

                default:
                    throw new ValueNotSupportedException(resampleInterpolationType);
            }
        }

        internal RandomCalculator_Stripe GetRandomCalculator_Stripe(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_RandomCalculator_Stripe_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operatorID_To_RandomCalculator_Stripe_Dictionary.TryGetValue(operatorID, out RandomCalculator_Stripe randomCalculator))
                {
                    randomCalculator = new RandomCalculator_Stripe();
                    _operatorID_To_RandomCalculator_Stripe_Dictionary.Add(operatorID, randomCalculator);
                }

                return randomCalculator;
            }
        }

        internal RandomCalculator_Block GetRandomCalculator_Block(int operatorID)
        {
            if (operatorID == 0) throw new ZeroException(() => operatorID);

            lock (_operatorID_To_RandomCalculator_Block_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operatorID_To_RandomCalculator_Block_Dictionary.TryGetValue(operatorID, out RandomCalculator_Block randomCalculator))
                {
                    randomCalculator = new RandomCalculator_Block();
                    _operatorID_To_RandomCalculator_Block_Dictionary.Add(operatorID, randomCalculator);
                }
                return randomCalculator;
            }
        }

        internal ArrayDto GetRandomArrayDto(ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
        {
            switch (resampleInterpolationTypeEnum)
            {
                case ResampleInterpolationTypeEnum.Block:
                    return GetRandomArrayDto_Block();

                case ResampleInterpolationTypeEnum.Stripe:
                    return GetRandomArrayDto_Stripe();

                default:
                    throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
            }
        }
        internal ArrayDto GetRandomArrayDto_Block() => _randomArrayDto_Block;

        internal ArrayDto GetRandomArrayDto_Stripe() => _randomArrayDto_Stripe;

        /// <summary>
        /// Out comes an array calculator for each channel.
        /// The concrete type of the ArrayCalculators is the same, 
        /// so if you have to DO something with the concrete type,
        /// you only have to check one of them.
        /// </summary>
        /// <param name="samplingRate">greater than 0</param>
        internal IList<ArrayDto> GetCacheArrayDtos(
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

            IList<ArrayDto> arrayCalculators = GetCacheArrayDtos(
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

        internal IList<ArrayDto> GetCacheArrayDtos(
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

            lock (_cacheOperatorID_To_ArrayDtos_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_cacheOperatorID_To_ArrayDtos_Dictionary.TryGetValue(operatorID, out IList<ArrayDto> arrayCalculators))
                {
                    arrayCalculators = CreateCacheArrayDtos(
                        signalCalculator,
                        start,
                        end,
                        samplingRate,
                        channelCount,
                        interpolationTypeEnum,
                        dimensionStack,
                        channelDimensionStack);

                    _cacheOperatorID_To_ArrayDtos_Dictionary.Add(operatorID, arrayCalculators);
                }

                return arrayCalculators;
            }
        }

        private IList<ArrayDto> CreateCacheArrayDtos(
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
            if (double.IsNaN(end)) throw new NaNException(() => end);
            if (double.IsInfinity(end)) throw new InfinityException(() => end);
            if (double.IsNaN(start)) throw new NaNException(() => start);
            if (double.IsInfinity(start)) throw new InfinityException(() => start);
            if (double.IsNaN(rate)) throw new NaNException(() => rate);
            if (double.IsInfinity(rate)) throw new InfinityException(() => rate);
            if (rate <= 0.0) throw new LessThanOrEqualException(() => rate, 0.0);
            if (end <= start) throw new LessThanOrEqualException(() => end, () => start);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (channelDimensionStack == null) throw new NullException(() => channelDimensionStack);

            double length = end - start;
            int tickCount = (int)(length * rate) + 1;
            double tickLength = 1.0 / rate;

            var arrayCalculators = new ArrayDto[channelCount];

            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
#if !USE_INVAR_INDICES
                channelDimensionStack.Set(channelIndex);
#else
                channelDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, channelIndex);
#endif
                var samples = new double[tickCount];

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

                var arrayDto = new ArrayDto
                {
                    Array = samples,
                    Rate = rate,
                    MinPosition = start,
                    ValueBefore = DEFAULT_VALUE_BEFORE,
                    ValueAfter = DEFAULT_VALUE_AFTER,
                    InterpolationTypeEnum = interpolationTypeEnum
                };

                arrayCalculators[channelIndex] = arrayDto;
            }

            return arrayCalculators;
        }
    }
}