using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;

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

        private readonly Dictionary<string, NoiseCalculator> _operationIdentity_To_NoiseCalculator_Dictionary = new Dictionary<string, NoiseCalculator>();
        private readonly object _operationIdentity_To_NoiseCalculator_Dictionary_Lock = new object();

        private readonly Dictionary<string, RandomCalculator_Block> _operationIdentity_To_RandomCalculator_Block_Dictionary = new Dictionary<string, RandomCalculator_Block>();
        private readonly object _operationIdentity_To_RandomCalculator_Block_Dictionary_Lock = new object();

        private readonly Dictionary<string, RandomCalculator_Stripe> _operationIdentity_To_RandomCalculator_Stripe_Dictionary = new Dictionary<string, RandomCalculator_Stripe>();
        private readonly object _operationIdentity_To_RandomCalculator_Stripe_Dictionary_Lock = new object();

        private readonly Dictionary<string, IList<ArrayDto>> _cacheOperationIdentity_To_ArrayDtos_Dictionary = new Dictionary<string, IList<ArrayDto>>();
        private readonly object _cacheOperationIdentity_To_ArrayDtos_Dictionary_Lock = new object();

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

        private ArrayDto GetCurveArrayDto(Curve curve)
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
            byte[] bytes = sampleRepository.GetBytes(sampleID);

            var sampleInfo = new SampleInfo { Sample = sample, Bytes = bytes };

            IList<ArrayDto> calculators = GetSampleArrayDtos(sampleInfo);

            return calculators;
        }

        /// <summary> Returns one calculator for each channel. </summary>
        private IList<ArrayDto> GetSampleArrayDtos(SampleInfo sampleInfo)
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

        internal NoiseCalculator GetNoiseCalculator(string operationIdentity)
        {
            if (string.IsNullOrEmpty(operationIdentity)) throw new NullOrEmptyException(nameof(operationIdentity));

            lock (_operationIdentity_To_NoiseCalculator_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operationIdentity_To_NoiseCalculator_Dictionary.TryGetValue(operationIdentity, out NoiseCalculator noiseCalculator))
                {
                    noiseCalculator = new NoiseCalculator();
                    _operationIdentity_To_NoiseCalculator_Dictionary.Add(operationIdentity, noiseCalculator);
                }

                return noiseCalculator;
            }
        }

        internal ArrayDto GetNoiseArrayDto() => _noiseArrayDto;

        internal RandomCalculator_Stripe GetRandomCalculator_Stripe(string operationIdentity)
        {
            if (string.IsNullOrEmpty(operationIdentity)) throw new NullOrEmptyException(nameof(operationIdentity));

            lock (_operationIdentity_To_RandomCalculator_Stripe_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operationIdentity_To_RandomCalculator_Stripe_Dictionary.TryGetValue(operationIdentity, out RandomCalculator_Stripe randomCalculator))
                {
                    randomCalculator = new RandomCalculator_Stripe();
                    _operationIdentity_To_RandomCalculator_Stripe_Dictionary.Add(operationIdentity, randomCalculator);
                }

                return randomCalculator;
            }
        }

        internal RandomCalculator_Block GetRandomCalculator_Block(string operationIdentity)
        {
            if (string.IsNullOrEmpty(operationIdentity)) throw new NullOrEmptyException(nameof(operationIdentity));

            lock (_operationIdentity_To_RandomCalculator_Block_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_operationIdentity_To_RandomCalculator_Block_Dictionary.TryGetValue(operationIdentity, out RandomCalculator_Block randomCalculator))
                {
                    randomCalculator = new RandomCalculator_Block();
                    _operationIdentity_To_RandomCalculator_Block_Dictionary.Add(operationIdentity, randomCalculator);
                }
                return randomCalculator;
            }
        }

        internal ArrayDto GetRandomArrayDto(InterpolationTypeEnum interpolationTypeEnum)
        {
            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    return GetRandomArrayDto_Block();

                default:
                    return GetRandomArrayDto_Stripe();
            }
        }

        private ArrayDto GetRandomArrayDto_Block() => _randomArrayDto_Block;

        private ArrayDto GetRandomArrayDto_Stripe() => _randomArrayDto_Stripe;

        internal IList<ArrayDto> GetCacheArrayDtos(
            string operationIdentity, 
            OperatorCalculatorBase signalCalculator, 
            double start, 
            double end, 
            double samplingRate,
            int channelCount,
            InterpolationTypeEnum interpolationTypeEnum,
            VariableInput_OperatorCalculator dimensionCalculator,
            VariableInput_OperatorCalculator channelDimensionCalculator)
        {
            if (string.IsNullOrEmpty(operationIdentity)) throw new NullOrEmptyException(nameof(operationIdentity));

            lock (_cacheOperationIdentity_To_ArrayDtos_Dictionary_Lock)
            {
                // ReSharper disable once InvertIf
                if (!_cacheOperationIdentity_To_ArrayDtos_Dictionary.TryGetValue(operationIdentity, out IList<ArrayDto> arrayCalculators))
                {
                    arrayCalculators = CreateCacheArrayDtos(
                        signalCalculator,
                        start,
                        end,
                        samplingRate,
                        channelCount,
                        interpolationTypeEnum,
                        dimensionCalculator,
                        channelDimensionCalculator);

                    _cacheOperationIdentity_To_ArrayDtos_Dictionary.Add(operationIdentity, arrayCalculators);
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
            VariableInput_OperatorCalculator dimensionCalculator,
            VariableInput_OperatorCalculator channelDimensionCalculator)
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
            if (dimensionCalculator == null) throw new NullException(() => dimensionCalculator);
            if (channelDimensionCalculator == null) throw new NullException(() => channelDimensionCalculator);

            double length = end - start;
            int tickCount = (int)(length * rate) + 1;
            double tickLength = 1.0 / rate;

            var arrayCalculators = new ArrayDto[channelCount];

            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
                channelDimensionCalculator._value = channelIndex;

                var samples = new double[tickCount];

                double position = start;

                dimensionCalculator._value = position;

                for (int i = 0; i < tickCount; i++)
                {
                    double sample = signalCalculator.Calculate();
                    samples[i] = sample;

                    position += tickLength;

                    dimensionCalculator._value = position;
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