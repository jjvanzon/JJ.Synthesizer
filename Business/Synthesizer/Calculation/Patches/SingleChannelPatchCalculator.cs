using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Visitors;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public class SingleChannelPatchCalculator : IPatchCalculator
    {
#if !USE_INVAR_INDICES
        private const int TOP_LEVEL_DIMENSION_STACK_INDEX = 0;
#endif

        private static readonly CalculationEngineConfigurationEnum _calculationEngineConfigurationEnum = ConfigurationHelper.GetSection<ConfigurationSection>().CalculationEngine;

        private readonly DimensionStackCollection _dimensionStackCollection;
        private readonly DimensionStack _timeDimensionStack;
        private readonly DimensionStack _channelDimensionStack;

        private readonly OperatorCalculatorBase _outputOperatorCalculator;
        /// <summary> Array, instead of IList<T> for optimization in calculating values. </summary>
        private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

        private readonly Dictionary<int, IList<OperatorCalculatorBase>> _listIndex_To_ResettableOperatorCalculators_Dictionary;
        private readonly Dictionary<string, IList<OperatorCalculatorBase>> _name_To_ResettableOperatorCalculators_Dictionary;

        private readonly Dictionary<int, double> _listIndex_To_Value_Dictionary = new Dictionary<int, double>();
        private readonly Dictionary<DimensionEnum, double> _dimensionEnum_To_Value_Dictionary = new Dictionary<DimensionEnum, double>();
        private readonly Dictionary<string, double> _name_To_Value_Dictionary = new Dictionary<string, double>();
        private readonly Dictionary<Tuple<DimensionEnum, int>, double> _dimensionEnumAndListIndex_To_Value_Dictionary = new Dictionary<Tuple<DimensionEnum, int>, double>();
        private readonly Dictionary<Tuple<string, int>, double> _nameAndListIndex_To_Value_Dictionary = new Dictionary<Tuple<string, int>, double>();

        private readonly int _channelCount;
        private readonly int _channelIndex;
        private readonly double _frameDuration;

        public SingleChannelPatchCalculator(
            Outlet topLevelOutlet,
            int targetSamplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache,
            double secondsBetweenApplyFilterVariables,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (topLevelOutlet == null) throw new NullException(() => topLevelOutlet);
            if (channelCount <= 0) throw new LessThanOrEqualException(() => channelCount, 0);
            if (channelIndex < 0) throw new LessThanException(() => channelIndex, 0);

            _channelCount = channelCount;
            _channelIndex = channelIndex;
            _frameDuration = 1.0 / targetSamplingRate;

            ToCalculatorResult result;
            switch (_calculationEngineConfigurationEnum)
            {
                case CalculationEngineConfigurationEnum.EntityToCalculatorDirectly:
                    {
                        var visitor = new OperatorEntityToCalculatorDirectlyVisitor(
                            topLevelOutlet,
                            targetSamplingRate,
                            channelCount,
                            secondsBetweenApplyFilterVariables,
                            calculatorCache,
                            curveRepository,
                            sampleRepository,
                            patchRepository,
                            speakerSetupRepository);

                        result = visitor.Execute();
                        break;
                    }

                case CalculationEngineConfigurationEnum.EntityThruDtoToCalculator:
                    {
                        var visitor = new OperatorEntityThruDtoToCalculatorExecutor(
                            targetSamplingRate,
                            channelCount,
                            secondsBetweenApplyFilterVariables,
                            calculatorCache,
                            curveRepository,
                            patchRepository,
                            sampleRepository,
                            speakerSetupRepository);

                        result = visitor.Execute(topLevelOutlet);
                        break;
                    }

                default:
                    throw new ValueNotSupportedException(_calculationEngineConfigurationEnum);
            }

            // Yield over results to fields.
            _dimensionStackCollection = result.DimensionStackCollection;
            _outputOperatorCalculator = result.Output_OperatorCalculator;
            _inputOperatorCalculators = result.Input_OperatorCalculators.OrderBy(x => x.ListIndex).ToArray();
            _listIndex_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.Where(x => x.ListIndex.HasValue).ToNonUniqueDictionary(x => x.ListIndex.Value, x => x.OperatorCalculator);
            _name_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.ToNonUniqueDictionary(x => NameHelper.ToCanonical(x.Name), x => x.OperatorCalculator);

            foreach (VariableInput_OperatorCalculator inputOperatorCalculator in _inputOperatorCalculators)
            {
                double value = inputOperatorCalculator._value;
                int listIndex = inputOperatorCalculator.ListIndex;
                DimensionEnum dimensionEnum = inputOperatorCalculator.DimensionEnum;
                string name = NameHelper.ToCanonical(inputOperatorCalculator.CanonicalName);

                // Copy input calculator (default) values to dimensions.
                DimensionStack dimensionStackByEnum = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
                dimensionStackByEnum.Set(value);

                DimensionStack dimensionStackByName = _dimensionStackCollection.GetDimensionStack(name);
                dimensionStackByName.Set(value);

                // Copy input calculator values to dictionaries.
                _listIndex_To_Value_Dictionary[listIndex] = value;
                _dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;
                _name_To_Value_Dictionary[name] = value;

                var key2 = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);
                _dimensionEnumAndListIndex_To_Value_Dictionary[key2] = value;

                var key1 = new Tuple<string, int>(name, listIndex);
                _nameAndListIndex_To_Value_Dictionary[key1] = value;
            }

            // Get special dimensions' stacks.
            _timeDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Time);
            _channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            // Set special channel dimension value.
#if !USE_INVAR_INDICES
            _channelDimensionStack.Set(channelIndex);
#else
            _channelDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, channelIndex);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double time)
        {
#if !USE_INVAR_INDICES
            _timeDimensionStack.Set(time);
#else
            _timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
            double value = _outputOperatorCalculator.Calculate();

            return value;
        }

        /// <param name="frameCount">
        /// You cannot use buffer.Length as a basis for frameCount, 
        /// because if you write to the buffer beyond frameCount, then the audio driver might fail.
        /// A frameCount based on the entity model can differ from the frameCount you get from the driver,
        /// and you only know the frameCount at the time the driver calls us.
        /// </param>
        public void Calculate(float[] buffer, int frameCount, double t0)
        {
            int channelIndex = _channelIndex;
            int channelCount = _channelCount;
            double frameDuration = _frameDuration;
            int valueCount = frameCount * channelCount;
            DimensionStack timeDimensionStack = _timeDimensionStack;

            double t = t0;

            // Writes values in an interleaved way to the buffer.
            for (int i = channelIndex; i < valueCount; i += channelCount)
            {
#if !USE_INVAR_INDICES
                timeDimensionStack.Set(t);
#else
                timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, t);
#endif
                double value = Calculate(t);

                // winmm will trip over NaN.
                if (Double.IsNaN(value))
                {
                    value = 0;
                }

                // TODO: This seems unsafe. What happens if the cast is invalid?
                float floatValue = (float)value;

                InterlockedAdd(ref buffer[i], floatValue);

                t += frameDuration;
            }
        }

        // Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InterlockedAdd(ref float location1, float value)
        {
            float newCurrentValue = 0;
            while (true)
            {
                float currentValue = newCurrentValue;
                float newValue = currentValue + value;
                newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
                if (newCurrentValue == currentValue)
                    return newValue;
            }
        }

        public double Calculate(double time, int channelIndex)
        {
            throw new NotSupportedException("Calculate with channelIndex is not supported. Use the overload without channelIndex.");
        }

        public double GetValue(int listIndex)
        {
            if (listIndex < 0) return 0.0;
            if (listIndex >= _inputOperatorCalculators.Length) return 0.0;

            double value = _inputOperatorCalculators[listIndex]._value;
            return value;
        }

        public void SetValue(int listIndex, double value)
        {
            _listIndex_To_Value_Dictionary[listIndex] = value;

            if (listIndex < 0) return;
            if (listIndex >= _inputOperatorCalculators.Length) return;
            _inputOperatorCalculators[listIndex]._value = value;
        }

        public double GetValue(DimensionEnum dimensionEnum)
        {
            double value;
            _dimensionEnum_To_Value_Dictionary.TryGetValue(dimensionEnum, out value);
            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, double value)
        {
            _dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Set(value);

            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                if (inputCalculator.DimensionEnum == dimensionEnum)
                {
                    inputCalculator._value = value;
                }
            }
        }

        public double GetValue(string name)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            double value;
            _name_To_Value_Dictionary.TryGetValue(canonicalName, out value);
            return value;
        }

        public void SetValue(string name, double value)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            _name_To_Value_Dictionary[canonicalName] = value;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(canonicalName);
            dimensionStack.Set(value);

            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                if (String.Equals(inputCalculator.CanonicalName, canonicalName))
                {
                    inputCalculator._value = value;
                }
            }
        }

        public double GetValue(DimensionEnum dimensionEnum, int listIndex)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            double value;
            _dimensionEnumAndListIndex_To_Value_Dictionary.TryGetValue(key, out value);
            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            _dimensionEnumAndListIndex_To_Value_Dictionary[key] = value;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Set(value);

            int listIndex2 = 0;
            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                if (inputCalculator.DimensionEnum == dimensionEnum)
                {
                    if (listIndex2 == listIndex)
                    {
                        _inputOperatorCalculators[listIndex2]._value = value;
                        break;
                    }

                    listIndex2++;
                }
            }
        }

        public double GetValue(string name, int listIndex)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            var key = new Tuple<string, int>(canonicalName, listIndex);

            double value;
            _nameAndListIndex_To_Value_Dictionary.TryGetValue(key, out value);
            return value;
        }

        public void SetValue(string name, int listIndex, double value)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            var key = new Tuple<string, int>(canonicalName, listIndex);

            _nameAndListIndex_To_Value_Dictionary[key] = value;

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(canonicalName);
            dimensionStack.Set(value);

            int listIndex2 = 0;
            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                if (String.Equals(inputCalculator.CanonicalName, canonicalName))
                {
                    if (listIndex2 == listIndex)
                    {
                        _inputOperatorCalculators[listIndex2]._value = value;
                        break;
                    }

                    listIndex2++;
                }
            }
        }

        public void CloneValues(IPatchCalculator sourcePatchCalculator)
        {
            if (sourcePatchCalculator == null) throw new NullException(() => sourcePatchCalculator);

            var source = sourcePatchCalculator as SingleChannelPatchCalculator;
            if (source == null)
            {
                throw new InvalidTypeException<SingleChannelPatchCalculator>(() => sourcePatchCalculator);
            }

            foreach (var entry in source._listIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._dimensionEnum_To_Value_Dictionary)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._name_To_Value_Dictionary)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._dimensionEnumAndListIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }

            foreach (var entry in source._nameAndListIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }
        }

        public void Reset(double time)
        {
#if !USE_INVAR_INDICES
            _timeDimensionStack.Set(time);
#else
            _timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
            //// HACK: Reset does not work for other dimensions than time.
            //// (This means that MidiInputProcessor should reset only for the time dimension,
            //// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
            //foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
            //{
            //    if ((int)dimensionEnum == DimensionEnum.Time)
            //    {
            //        continue;
            //    }
            //    _dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
            //}

            _outputOperatorCalculator.Reset();

            // TODO: Remove outcommented code.
            //_listIndex_To_Value_Dictionary.Clear();
            //_name_To_Value_Dictionary.Clear();
            //_nameAndListIndex_To_Value_Dictionary.Clear();
            //_dimensionEnum_To_Value_Dictionary.Clear();
            //_dimensionEnumAndListIndex_To_Value_Dictionary.Clear();
        }

        public void Reset(double time, string name)
        {
            string canonicalName = NameHelper.ToCanonical(name);

#if !USE_INVAR_INDICES
            _timeDimensionStack.Set(time);
#else
            _timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
            //// HACK: Reset does not work for other dimensions than time.
            //// (This means that MidiInputProcessor should reset only for the time dimension,
            //// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
            //foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
            //{
            //    if ((int)dimensionEnum == DimensionEnum.Time)
            //    {
            //        continue;
            //    }
            //    _dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
            //}

            IList<OperatorCalculatorBase> calculators;
            if (_name_To_ResettableOperatorCalculators_Dictionary.TryGetValue(canonicalName, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.Reset();
                }
            }
        }

        public void Reset(double time, int listIndex)
        {
#if !USE_INVAR_INDICES
            _timeDimensionStack.Set(time);
#else
            _timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, time);
#endif
            //// HACK: Reset does not work for other dimensions than time.
            //// (This means that MidiInputProcessor should reset only for the time dimension,
            //// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
            //foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
            //{
            //    if ((int)dimensionEnum == DimensionEnum.Time)
            //    {
            //        continue;
            //    }
            //    _dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
            //}

            IList<OperatorCalculatorBase> calculators;
            if (_listIndex_To_ResettableOperatorCalculators_Dictionary.TryGetValue(listIndex, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.Reset();
                }
            }
        }
    }
}
