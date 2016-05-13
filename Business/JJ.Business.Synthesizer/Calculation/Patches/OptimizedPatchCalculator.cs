using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class OptimizedPatchCalculator : IPatchCalculator
    {
        private const int TIME_DIMENSION_INDEX = (int)DimensionEnum.Time;
        private const int CHANNEL_DIMENSION_INDEX = (int)DimensionEnum.Channel;

        private DimensionStacks _dimensionStack;
        private readonly OperatorCalculatorBase _outputOperatorCalculator;
        /// <summary> Array, instead of IList<T> for optimization in calculating values. </summary>
        private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

        private readonly Dictionary<string, IList<OperatorCalculatorBase>> _name_To_ResettableOperatorCalculators_Dictionary;
        private readonly Dictionary<int, IList<OperatorCalculatorBase>> _listIndex_To_ResettableOperatorCalculators_Dictionary;

        private Dictionary<int, double> _valuesByListIndex = new Dictionary<int, double>();
        private Dictionary<string, double> _valuesByName = new Dictionary<string, double>();
        private Dictionary<Tuple<string, int>, double> _valuesByNameAndListIndex = new Dictionary<Tuple<string, int>, double>();
        private Dictionary<DimensionEnum, double> _dimensionEnum_To_Value_Dictionary = new Dictionary<DimensionEnum, double>();
        private Dictionary<Tuple<DimensionEnum, int>, double> _dimensionEnumAndListIndex_To_Value_Dictionary = new Dictionary<Tuple<DimensionEnum, int>, double>();

        public OptimizedPatchCalculator(
            Outlet outlet,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (outlet == null) throw new NullException(() => outlet);

            var visitor = new OptimizedPatchCalculatorVisitor(curveRepository, sampleRepository, patchRepository, speakerSetupRepository, calculatorCache);

            OptimizedPatchCalculatorVisitor.Result result = visitor.Execute(outlet, channelCount);

            _dimensionStack = result.DimensionStack;
            _outputOperatorCalculator = result.Output_OperatorCalculator;
            _inputOperatorCalculators = result.Input_OperatorCalculators.OrderBy(x => x.ListIndex).ToArray();
            _name_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.ToNonUniqueDictionary(x => x.Name ?? "", x => x.OperatorCalculator);
            _listIndex_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.Where(x => x.ListIndex.HasValue).ToNonUniqueDictionary(x => x.ListIndex.Value, x => x.OperatorCalculator);

            _dimensionStack.Set(DimensionEnum.Channel, channelIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate(double time)
        {
            _dimensionStack.Set(TIME_DIMENSION_INDEX, time);

            double value = _outputOperatorCalculator.Calculate();

            return value;
        }

        public double[] Calculate(double t0, double frameDuration, int count)
        {
            double t = t0;

            double[] values = new double[count];

            for (int i = 0; i < count; i++)
            {
                _dimensionStack.Set(TIME_DIMENSION_INDEX, t);

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
            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
            if (listIndex < 0) return 0.0;
            if (listIndex >= _inputOperatorCalculators.Length) return 0.0;

            double value = _inputOperatorCalculators[listIndex]._value;
            return value;
        }

        public void SetValue(int listIndex, double value)
        {
            _valuesByListIndex[listIndex] = value;

            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
            if (listIndex < 0) return;
            if (listIndex >= _inputOperatorCalculators.Length) return;

            _inputOperatorCalculators[listIndex]._value = value;
        }

        public double GetValue(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            string key = name;

            double value;
            if (_valuesByName.TryGetValue(key, out value))
            {
                return value;
            }

            IList<int> inputCalculatorIndexes = GetInputCalculatorIndexes(name);
            if (inputCalculatorIndexes.Count != 0)
            {
                int inputCalculatorIndex = inputCalculatorIndexes[0];
                value = _inputOperatorCalculators[inputCalculatorIndex]._value;
                _valuesByName[key] = value;
            }

            return value;
        }

        public void SetValue(string name, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            _valuesByName[name] = value;

            foreach (int inputCalculatorIndex in GetInputCalculatorIndexes(name))
            {
                _inputOperatorCalculators[inputCalculatorIndex]._value = value;
            }
        }

        private IList<int> GetInputCalculatorIndexes(string name)
        {
            var list = new List<int>(_inputOperatorCalculators.Length);

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (String.Equals(inputCalculator.Name, name))
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public double GetValue(string name, int listIndex)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            var key = new Tuple<string, int>(name, listIndex);

            double value;
            if (_valuesByNameAndListIndex.TryGetValue(key, out value))
            {
                return value;
            }

            int? inputCalculatorIndex = TryGetInputCalculatorIndex(name, listIndex);
            if (inputCalculatorIndex.HasValue)
            {
                value = _inputOperatorCalculators[inputCalculatorIndex.Value]._value;
                _valuesByNameAndListIndex[key] = value;
            }

            return value;
        }

        public void SetValue(string name, int listIndex, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            var key = new Tuple<string, int>(name, listIndex);

            _valuesByNameAndListIndex[key] = value;

            int? inputCalculatorIndex = TryGetInputCalculatorIndex(name, listIndex);
            if (inputCalculatorIndex.HasValue)
            {
                _inputOperatorCalculators[inputCalculatorIndex.Value]._value = value;
            }
        }

        private int? TryGetInputCalculatorIndex(string name, int listIndex)
        {
            int j = 0;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (String.Equals(inputCalculator.Name, name))
                {
                    if (j == listIndex)
                    {
                        return i;
                    }

                    j++;
                }
            }

            return null;
        }

        public double GetValue(DimensionEnum dimensionEnum)
        {
            DimensionEnum key = dimensionEnum;

            double value;
            if (_dimensionEnum_To_Value_Dictionary.TryGetValue(key, out value))
            {
                return value;
            }

            IList<int> inputCalculatorIndexes = GetInputCalculatorIndexes(dimensionEnum);
            if (inputCalculatorIndexes.Count != 0)
            {
                int inputCalculatorIndex = inputCalculatorIndexes[0];
                value = _inputOperatorCalculators[inputCalculatorIndex]._value;
                _dimensionEnum_To_Value_Dictionary[key] = value;
            }

            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, double value)
        {
            _dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;

            foreach (int inputCalculatorIndex in GetInputCalculatorIndexes(dimensionEnum))
            {
                _inputOperatorCalculators[inputCalculatorIndex]._value = value;
            }
        }

        private IList<int> GetInputCalculatorIndexes(DimensionEnum dimensionEnum)
        {
            var list = new List<int>(_inputOperatorCalculators.Length);

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (inputCalculator.DimensionEnum == dimensionEnum)
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public double GetValue(DimensionEnum dimensionEnum, int listIndex)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            double value;
            if (_dimensionEnumAndListIndex_To_Value_Dictionary.TryGetValue(key, out value))
            {
                return value;
            }

            int? inputCalculatorIndex = TryGetInputCalculatorIndex(dimensionEnum, listIndex);
            if (inputCalculatorIndex.HasValue)
            {
                value = _inputOperatorCalculators[inputCalculatorIndex.Value]._value;
                _dimensionEnumAndListIndex_To_Value_Dictionary[key] = value;
            }

            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            _dimensionEnumAndListIndex_To_Value_Dictionary[key] = value;

            int? inputCalculatorListIndex = TryGetInputCalculatorIndex(dimensionEnum, listIndex);
            if (inputCalculatorListIndex.HasValue)
            {
                _inputOperatorCalculators[inputCalculatorListIndex.Value]._value = value;
            }
        }

        private int? TryGetInputCalculatorIndex(DimensionEnum dimensionEnum, int listIndex)
        {
            int j = 0;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (inputCalculator.DimensionEnum == dimensionEnum)
                {
                    if (j == listIndex)
                    {
                        return i;
                    }

                    j++;
                }
            }

            return null;
        }

        public void CloneValues(IPatchCalculator sourcePatchCalculator)
        {
            if (sourcePatchCalculator == null) throw new NullException(() => sourcePatchCalculator);

            var source = sourcePatchCalculator as OptimizedPatchCalculator;
            if (source == null)
            {
                throw new IsNotTypeException<OptimizedPatchCalculator>(() => sourcePatchCalculator);
            }

            foreach (var entry in source._valuesByListIndex)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._valuesByName)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._valuesByNameAndListIndex)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }

            foreach (var entry in source._dimensionEnum_To_Value_Dictionary)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._dimensionEnumAndListIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }
        }

        public void Reset(double time)
        {
            _dimensionStack.Set(TIME_DIMENSION_INDEX, time);

            _outputOperatorCalculator.Reset();

            _valuesByListIndex.Clear();
            _valuesByName.Clear();
            _valuesByNameAndListIndex.Clear();
            _dimensionEnum_To_Value_Dictionary.Clear();
            _dimensionEnumAndListIndex_To_Value_Dictionary.Clear();
        }

        public void Reset(double time, string name)
        {
            // Necessary for using null or empty string as the key of a dictionary.
            // The dictionary neither accepts null as a key,
            // and also null and empty must have the same behavior.
            name = name ?? "";

            _dimensionStack.Set(TIME_DIMENSION_INDEX, time);

            IList<OperatorCalculatorBase> calculators;
            if (_name_To_ResettableOperatorCalculators_Dictionary.TryGetValue(name, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.Reset();
                }
            }
        }

        public void Reset(double time, int listIndex)
        {
            _dimensionStack.Set(TIME_DIMENSION_INDEX, time);

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
