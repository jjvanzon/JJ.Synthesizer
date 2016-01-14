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

            // TODO: One would think that the outlet operators should be sorted by a list index too.
            _outputOperatorCalculators = result.Output_OperatorCalculators.ToArray();

            _inputOperatorCalculators = result.Input_OperatorCalculators.OrderBy(x => x.ListIndex).ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _outputOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }

        public void SetValue(int listIndex, double value)
        {
            _valuesByListIndex[listIndex] = value;

            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
            if (listIndex < 0) return;
            if (listIndex >= _inputOperatorCalculators.Length) return;

            _inputOperatorCalculators[listIndex]._value = value;
        }

        public void SetValue(string name, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            _valuesByName[name] = value;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

                if (String.Equals(operatorCalculator.Name, name))
                {
                    operatorCalculator._value = value;
                }
            }
        }

        public void SetValue(string name, int listIndex, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            _valuesByNameAndListIndex[new Tuple<string, int>(name, listIndex)] = value;

            int j = 0;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

                if (String.Equals(operatorCalculator.Name, name))
                {
                    if (j == listIndex)
                    {
                        operatorCalculator._value = value;
                        return;
                    }

                    j++;
                }
            }
        }

        public void SetValue(InletTypeEnum inletTypeEnum, double value)
        {
            _valuesByInletTypeEnum[inletTypeEnum] = value;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
                {
                    operatorCalculator._value = value;
                }
            }
        }

        public void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value)
        {
            _valuesByInletTypeEnumAndListIndex[new Tuple<InletTypeEnum, int>(inletTypeEnum, listIndex)] = value;

            int j = 0;

            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
                {
                    if (j == listIndex)
                    {
                        operatorCalculator._value = value;
                        return;
                    }

                    j++;
                }
            }
        }

        public double GetValue(int listIndex)
        {
            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
            if (listIndex < 0) return 0.0;
            if (listIndex >= _inputOperatorCalculators.Length) return 0.0;

            double value = _inputOperatorCalculators[listIndex]._value;
            return value;
        }

        public double GetValue(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            double value;
            _valuesByName.TryGetValue(name, out value);
            return value;
        }

        public double GetValue(string name, int listIndex)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            double value;
            _valuesByNameAndListIndex.TryGetValue(new Tuple<string, int>(name, listIndex), out value);
            return value;
        }

        public double GetValue(InletTypeEnum inletTypeEnum)
        {
            double value;
            _valuesByInletTypeEnum.TryGetValue(inletTypeEnum, out value);
            return value;
        }

        public double GetValue(InletTypeEnum inletTypeEnum, int listIndex)
        {
            double value;
            _valuesByInletTypeEnumAndListIndex.TryGetValue(new Tuple<InletTypeEnum, int>(inletTypeEnum, listIndex), out value);
            return value;
        }

        public void ResetState()
        {
            for (int i = 0; i < _outputOperatorCalculators.Length; i++)
            {
                OperatorCalculatorBase outputOperatorCalculator = _outputOperatorCalculators[i];

                outputOperatorCalculator.ResetState();
            }

            // Temporarily disable this, until you can reset the state of a specific tone. (2016-01-14)
            //_valuesByListIndex.Clear();
            //_valuesByName.Clear();
            //_valuesByNameAndListIndex.Clear();
            //_valuesByInletTypeEnum.Clear();
            //_valuesByInletTypeEnumAndListIndex.Clear();
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
    }
}
