using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using System;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class OptimizedPatchCalculator : IPatchCalculator
    {
        /// <summary> Array for optimization in calculating values. </summary>
        private readonly OperatorCalculatorBase[] _outputOperatorCalculators;
        private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

        private readonly Dictionary<string, IList<OperatorCalculatorBase>> _name_To_ResettableOperatorCalculators_Dictionary;
        private readonly Dictionary<int, IList<OperatorCalculatorBase>> _listIndex_To_ResettableOperatorCalculators_Dictionary;

        private Dictionary<int, double> _valuesByListIndex = new Dictionary<int, double>();
        private Dictionary<string, double> _valuesByName = new Dictionary<string, double>();
        private Dictionary<Tuple<string, int>, double> _valuesByNameAndListIndex = new Dictionary<Tuple<string, int>, double>();
        private Dictionary<InletTypeEnum, double> _valuesByInletTypeEnum = new Dictionary<InletTypeEnum, double>();
        private Dictionary<Tuple<InletTypeEnum, int>, double> _valuesByInletTypeEnumAndListIndex = new Dictionary<Tuple<InletTypeEnum, int>, double>();

        /// <summary> This overload has ChannelOutlets as params. </summary>
        /// <param name="channelOutlets">Can contain nulls.</param>
        public OptimizedPatchCalculator(
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            params Outlet[] channelOutlets)
            : this(channelOutlets, whiteNoiseCalculator, curveRepository, sampleRepository, patchRepository)
        { }

        /// <summary> This overload has ChannelOutlets as an IList<T>. </summary>
        /// <param name="channelOutlets">Can contain nulls.</param>
        public OptimizedPatchCalculator(
            IList<Outlet> channelOutlets,
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            var visitor = new OptimizedPatchCalculatorVisitor(curveRepository, sampleRepository, patchRepository);

            OptimizedPatchCalculatorVisitor.Result result = visitor.Execute(channelOutlets, whiteNoiseCalculator);

            _outputOperatorCalculators = result.Output_OperatorCalculators.ToArray(); // TODO: Low priority: One would think that the outlet operators should be sorted by a list index too. But it does not have a ListIndex property.
            _inputOperatorCalculators = result.Input_OperatorCalculators.OrderBy(x => x.ListIndex).ToArray();
            _name_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.ToNonUniqueDictionary(x => x.Name ?? "", x => x.OperatorCalculator);
            _listIndex_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples.Where(x => x.ListIndex.HasValue).ToNonUniqueDictionary(x => x.ListIndex.Value, x => x.OperatorCalculator);
        }

        public double Calculate(double time, int channelIndex)
        {
            double value = _outputOperatorCalculators[channelIndex].Calculate(time, channelIndex);
            return value;
        }

        public double[] Calculate(double t0, double sampleDuration, int count, int channelIndex)
        {
            double t = t0;

            double[] values = new double[count];

            for (int i = 0; i < count; i++)
            {
                double value = Calculate(t, channelIndex);

                values[i] = value;

                t += sampleDuration;
            }

            return values;
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

        public double GetValue(InletTypeEnum inletTypeEnum)
        {
            InletTypeEnum key = inletTypeEnum;

            double value;
            if (_valuesByInletTypeEnum.TryGetValue(key, out value))
            {
                return value;
            }

            IList<int> inputCalculatorIndexes = GetInputCalculatorIndexes(inletTypeEnum);
            if (inputCalculatorIndexes.Count != 0)
            {
                int inputCalculatorIndex = inputCalculatorIndexes[0];
                value = _inputOperatorCalculators[inputCalculatorIndex]._value;
                _valuesByInletTypeEnum[key] = value;
            }

            return value;
        }

        public void SetValue(InletTypeEnum inletTypeEnum, double value)
        {
            _valuesByInletTypeEnum[inletTypeEnum] = value;

            foreach (int inputCalculatorIndex in GetInputCalculatorIndexes(inletTypeEnum))
            {
                _inputOperatorCalculators[inputCalculatorIndex]._value = value;
            }
        }

        private IList<int> GetInputCalculatorIndexes(InletTypeEnum inletTypeEnum)
        {
            var list = new List<int>(_inputOperatorCalculators.Length);

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (inputCalculator.InletTypeEnum == inletTypeEnum)
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public double GetValue(InletTypeEnum inletTypeEnum, int listIndex)
        {
            var key = new Tuple<InletTypeEnum, int>(inletTypeEnum, listIndex);

            double value;
            if (_valuesByInletTypeEnumAndListIndex.TryGetValue(key, out value))
            {
                return value;
            }

            int? inputCalculatorIndex = TryGetInputCalculatorIndex(inletTypeEnum, listIndex);
            if (inputCalculatorIndex.HasValue)
            {
                value = _inputOperatorCalculators[inputCalculatorIndex.Value]._value;
                _valuesByInletTypeEnumAndListIndex[key] = value;
            }

            return value;
        }

        public void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value)
        {
            var key = new Tuple<InletTypeEnum, int>(inletTypeEnum, listIndex);

            _valuesByInletTypeEnumAndListIndex[key] = value;

            int? inputCalculatorListIndex = TryGetInputCalculatorIndex(inletTypeEnum, listIndex);
            if (inputCalculatorListIndex.HasValue)
            {
                _inputOperatorCalculators[inputCalculatorListIndex.Value]._value = value;
            }
        }

        private int? TryGetInputCalculatorIndex(InletTypeEnum inletTypeEnum, int listIndex)
        {
            int j = 0;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator inputCalculator = _inputOperatorCalculators[i];

                if (inputCalculator.InletTypeEnum == inletTypeEnum)
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

            foreach (var entry in source._valuesByInletTypeEnum)
            {
                SetValue(entry.Key, entry.Value);
            }

            foreach (var entry in source._valuesByInletTypeEnumAndListIndex)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }
        }

        public void ResetState()
        {
            for (int i = 0; i < _outputOperatorCalculators.Length; i++)
            {
                OperatorCalculatorBase outputOperatorCalculator = _outputOperatorCalculators[i];
                outputOperatorCalculator.ResetState();
            }

            _valuesByListIndex.Clear();
            _valuesByName.Clear();
            _valuesByNameAndListIndex.Clear();
            _valuesByInletTypeEnum.Clear();
            _valuesByInletTypeEnumAndListIndex.Clear();
        }

        public void ResetState(string name)
        {
            // Necessary for using null or empty string as the key of a dictionary.
            // The dictionary neither accepts null as a key,
            // and also null and empty must have the same behavior.
            name = name ?? "";

            IList<OperatorCalculatorBase> calculators;
            if (_name_To_ResettableOperatorCalculators_Dictionary.TryGetValue(name, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.ResetState();
                }
            }
        }

        public void ResetState(int listIndex)
        {
            IList<OperatorCalculatorBase> calculators;
            if (_listIndex_To_ResettableOperatorCalculators_Dictionary.TryGetValue(listIndex, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.ResetState();
                }
            }
        }
    }
}
