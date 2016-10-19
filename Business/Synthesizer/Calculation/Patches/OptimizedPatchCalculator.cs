using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class OptimizedPatchCalculator : IPatchCalculator
    {
#if !USE_INVAR_INDICES
        private const int TOP_LEVEL_DIMENSION_STACK_INDEX = 0;
#endif
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

        public OptimizedPatchCalculator(
            Outlet outlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache,
            double secondsBetweenApplyFilterVariables,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (outlet == null) throw new NullException(() => outlet);

            var visitor = new OptimizedPatchCalculatorVisitor(
                outlet,
                samplingRate,
                channelCount,
                secondsBetweenApplyFilterVariables,
                calculatorCache,
                curveRepository, 
                sampleRepository, 
                patchRepository, 
                speakerSetupRepository);

            OptimizedPatchCalculatorVisitorResult result = visitor.Execute();

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

        public double[] Calculate(double t0, double frameDuration, int count)
        {
            double t = t0;

            double[] values = new double[count];

            for (int i = 0; i < count; i++)
            {
#if !USE_INVAR_INDICES
                _timeDimensionStack.Set(t);
#else
                _timeDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, t);
#endif
                double value = Calculate(t);

                values[i] = value;

                t += frameDuration;
            }

            return values;
        }

        public double Calculate(double time, int channelIndex)
        {
            throw new NotSupportedException("Calculate with channelIndex is not supported. Use the overload without channelIndex.");
        }

        public double[] Calculate(double t0, double frameDuration, int count, int channelIndex)
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

            var source = sourcePatchCalculator as OptimizedPatchCalculator;
            if (source == null)
            {
                throw new InvalidTypeException<OptimizedPatchCalculator>(() => sourcePatchCalculator);
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
