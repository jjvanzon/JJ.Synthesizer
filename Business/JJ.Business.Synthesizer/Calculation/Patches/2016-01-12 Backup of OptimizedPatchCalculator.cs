//using JJ.Framework.Reflection.Exceptions;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Business.Synthesizer.Enums;
//using System;
//using JJ.Framework.Reflection;

//namespace JJ.Business.Synthesizer.Calculation.Patches
//{
//    internal class OptimizedPatchCalculator : IPatchCalculator
//    {
//        /// <summary> Array for optimization in calculating values. </summary>
//        private readonly OperatorCalculatorBase[] _outputOperatorCalculators;
//        private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

//        /// <summary> This overload has ChannelOutlets as params. </summary>
//        /// <param name="channelOutlets">Can contain nulls.</param>
//        public OptimizedPatchCalculator(
//            WhiteNoiseCalculator whiteNoiseCalculator,
//            ICurveRepository curveRepository, 
//            ISampleRepository sampleRepository,
//            IPatchRepository patchRepository,
//            params Outlet[] channelOutlets)
//            : this(channelOutlets, whiteNoiseCalculator, curveRepository, sampleRepository, patchRepository)
//        { }

//        /// <summary> This overload has ChannelOutlets as an IList<T>. </summary>
//        /// <param name="channelOutlets">Can contain nulls.</param>
//        public OptimizedPatchCalculator(
//            IList<Outlet> channelOutlets,
//            WhiteNoiseCalculator whiteNoiseCalculator,
//            ICurveRepository curveRepository, 
//            ISampleRepository sampleRepository,
//            IPatchRepository patchRepository)
//        {
//            if (channelOutlets == null) throw new NullException(() => channelOutlets);

//            var visitor = new OptimizedPatchCalculatorVisitor(curveRepository, sampleRepository, patchRepository);

//            OptimizedPatchCalculatorVisitor.Result result = visitor.Execute(channelOutlets, whiteNoiseCalculator);

//            // TODO: One would think that the outlet operators should be sorted by a list index too.
//            _outputOperatorCalculators = result.Output_OperatorCalculators.ToArray();

//            _inputOperatorCalculators = result.Input_OperatorCalculators.OrderBy(x => x.ListIndex).ToArray();
//        }

//        public double Calculate(double time, int channelIndex)
//        {
//            return _outputOperatorCalculators[channelIndex].Calculate(time, channelIndex);
//        }

//        public void SetValue(int listIndex, double value)
//        {
//            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
//            if (listIndex < 0) return;
//            if (listIndex >= _inputOperatorCalculators.Length) return;

//            _inputOperatorCalculators[listIndex]._value = value;
//        }

//        public void SetValue(string name, double value)
//        {
//            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (String.Equals(operatorCalculator.Name, name))
//                {
//                    operatorCalculator._value = value;
//                }
//            }
//        }

//        public void SetValue(string name, int listIndex, double value)
//        {
//            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

//            int j = 0;

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (String.Equals(operatorCalculator.Name, name))
//                {
//                    if (j == listIndex)
//                    {
//                        operatorCalculator._value = value;
//                        return;
//                    }

//                    j++;
//                }
//            }
//        }

//        public void SetValue(InletTypeEnum inletTypeEnum, double value)
//        {
//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
//                {
//                    operatorCalculator._value = value;
//                }
//            }
//        }

//        public void SetValue(InletTypeEnum inletTypeEnum, int listIndex, double value)
//        {
//            int j = 0;

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
//                {
//                    if (j == listIndex)
//                    {
//                        operatorCalculator._value = value;
//                        return;
//                    }

//                    j++;
//                }
//            }
//        }

//        public double GetValue(int listIndex)
//        {
//            // Be tollerant for non-existent list indexes, because you can switch instruments so dynamically.
//            if (listIndex < 0) return 0.0;
//            if (listIndex >= _inputOperatorCalculators.Length) return 0.0;

//            double value = _inputOperatorCalculators[listIndex]._value;
//            return value;
//        }

//        public double GetValue(string name)
//        {
//            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (String.Equals(operatorCalculator.Name, name))
//                {
//                    return operatorCalculator._value;
//                }
//            }

//            return 0.0;
//        }

//        public double GetValue(string name, int listIndex)
//        {
//            if (String.IsNullOrEmpty(name)) throw new NullOrEmptyException(() => name);

//            int j = 0;

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (String.Equals(operatorCalculator.Name, name))
//                {
//                    if (j == listIndex)
//                    {
//                        return operatorCalculator._value;
//                    }

//                    j++;
//                }
//            }

//            return 0.0;
//        }

//        public double GetValue(InletTypeEnum inletTypeEnum)
//        {
//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
//                {
//                    return operatorCalculator._value;
//                }
//            }

//            return 0.0;
//        }

//        public double GetValue(InletTypeEnum inletTypeEnum, int listIndex)
//        {
//            int j = 0;

//            for (int i = 0; i < _inputOperatorCalculators.Length; i++)
//            {
//                VariableInput_OperatorCalculator operatorCalculator = _inputOperatorCalculators[i];

//                if (operatorCalculator.InletTypeEnum == inletTypeEnum)
//                {
//                    if (j == listIndex)
//                    {
//                        return operatorCalculator._value;
//                    }

//                    j++;
//                }
//            }

//            return 0.0;
//        }

//        public void ResetPhases(double time)
//        {
//            for (int i = 0; i < _outputOperatorCalculators.Length; i++)
//            {
//                OperatorCalculatorBase outputOperatorCalculator = _outputOperatorCalculators[i];

//                outputOperatorCalculator.ResetPhase(time);
//            }
//        }

//        // TODO: HACK. Temporary (2016-01-11) to test something for the NoteStart delays.
//        public void ResetPhase(int monophonicListIndex, double time)
//        {
//            var adderCalculator = (Adder_OperatorCalculator4)_outputOperatorCalculators[0];

//            var accessor = new Accessor(adderCalculator);
//            OperatorCalculatorBase operandCalculator = (OperatorCalculatorBase)accessor.GetFieldValue("_operandCalculator" + (monophonicListIndex + 1).ToString());

//            var accessor2 = new Accessor(operandCalculator);
//            OperatorCalculatorBase monophonicOutlet = (OperatorCalculatorBase)accessor2.GetFieldValue("_signalCalculator");

//            monophonicOutlet.ResetPhase(0);
//        }
//    }
//}
