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
        private OperatorCalculatorBase[] _channelOperatorCalculators;
        private VariableInput_OperatorCalculator[] _variableInput_OperatorCalculators;

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

            _channelOperatorCalculators = result.Output_OperatorCalculators.ToArray();
            _variableInput_OperatorCalculators = result.Input_OperatorCalculators.ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _channelOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }

        public void SetValue(int listIndex, double value)
        {
            // Be tollerant for non-existend list indexes, because you can switch instruments so dynamically.
            if (listIndex < 0) return;
            if (listIndex >= _variableInput_OperatorCalculators.Length) return;

            _variableInput_OperatorCalculators[listIndex]._value = value;
        }

        public void SetValue(string name, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            for (int i = 0; i < _variableInput_OperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _variableInput_OperatorCalculators[i];

                if (String.Equals(operatorCalculator.Name, name))
                {
                    operatorCalculator._value = value;
                }
            }
        }

        public void SetValue(string name, int listIndex, double value)
        {
            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

            int j = 0;

            for (int i = 0; i < _variableInput_OperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _variableInput_OperatorCalculators[i];

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
            for (int i = 0; i < _variableInput_OperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _variableInput_OperatorCalculators[i];

                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
                {
                    operatorCalculator._value = value;
                }
            }
        }

        public void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value)
        {
            int j = 0;

            for (int i = 0; i < _variableInput_OperatorCalculators.Length; i++)
            {
                VariableInput_OperatorCalculator operatorCalculator = _variableInput_OperatorCalculators[i];

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
    }
}
