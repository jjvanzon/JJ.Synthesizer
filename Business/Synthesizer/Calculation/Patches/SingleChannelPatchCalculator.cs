using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SingleChannelPatchCalculator : PatchCalculatorBase
    {
#if !USE_INVAR_INDICES
        // ReSharper disable once UnusedMember.Local
        private const int TOP_LEVEL_DIMENSION_STACK_INDEX = 0;
#endif
        private static readonly CalculationEngineConfigurationEnum _calculationEngineConfigurationEnum = ConfigurationHelper.GetSection<ConfigurationSection>().CalculationEngine;

        private readonly DimensionStackCollection _dimensionStackCollection;
        private readonly DimensionStack _timeDimensionStack;

        private readonly OperatorCalculatorBase _outputOperatorCalculator;
        /// <summary> Array, instead of IList&lt;T&gt; for optimization in calculating values. </summary>
        private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

        private readonly Dictionary<int, IList<OperatorCalculatorBase>> _listIndex_To_ResettableOperatorCalculators_Dictionary;
        private readonly Dictionary<string, IList<OperatorCalculatorBase>> _name_To_ResettableOperatorCalculators_Dictionary;

        public SingleChannelPatchCalculator(
            Outlet topLevelOutlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache,
            double secondsBetweenApplyFilterVariables,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            ISpeakerSetupRepository speakerSetupRepository)
            : base(samplingRate, channelCount, channelIndex)
        {
            if (topLevelOutlet == null) throw new NullException(() => topLevelOutlet);

            ToCalculatorResult result;
            switch (_calculationEngineConfigurationEnum)
            {
                case CalculationEngineConfigurationEnum.EntityToCalculatorDirectly:
                    {
                        var visitor = new OperatorEntityToCalculatorDirectlyVisitor(
                            topLevelOutlet,
                            samplingRate,
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
                            samplingRate,
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
            _listIndex_To_ResettableOperatorCalculators_Dictionary = 
                result.ResettableOperatorTuples.Where(x => x.ListIndex.HasValue)
                                                // ReSharper disable once PossibleInvalidOperationException
                                               .ToNonUniqueDictionary(x => x.ListIndex.Value, x => x.OperatorCalculator);
            _name_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples
                                                                      .ToNonUniqueDictionary(x => NameHelper.ToCanonical(x.Name), x => x.OperatorCalculator);

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
            DimensionStack channelDimensionStack = _dimensionStackCollection.GetDimensionStack(DimensionEnum.Channel);

            // Set special channel dimension value.
#if !USE_INVAR_INDICES
            channelDimensionStack.Set(channelIndex);
#else
            _channelDimensionStack.Set(TOP_LEVEL_DIMENSION_STACK_INDEX, channelIndex);
#endif
        }

        // Calculate

        /// <param name="frameCount">
        /// You cannot use buffer.Length as a basis for frameCount, 
        /// because if you write to the buffer beyond frameCount, then the audio driver might fail.
        /// A frameCount based on the entity model can differ from the frameCount you get from the driver,
        /// and you only know the frameCount at the time the driver calls us.
        /// </param>
        public override void Calculate(float[] buffer, int frameCount, double t0)
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
                double value = _outputOperatorCalculator.Calculate();

                // winmm will trip over NaN.
                if (double.IsNaN(value))
                {
                    value = 0;
                }

                // TODO: This seems unsafe. What happens if the cast is invalid?
                float floatValue = (float)value;

                PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

                t += frameDuration;
            }
        }

        // Values

        public override void SetValue(int listIndex, double value)
        {
            base.SetValue(listIndex, value);

            if (listIndex < 0) return;
            if (listIndex >= _inputOperatorCalculators.Length) return;

            _inputOperatorCalculators[listIndex]._value = value;
        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

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

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            string canonicalName = NameHelper.ToCanonical(name);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(canonicalName);
            dimensionStack.Set(value);

            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                if (string.Equals(inputCalculator.CanonicalName, canonicalName))
                {
                    inputCalculator._value = value;
                }
            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            base.SetValue(dimensionEnum, listIndex, value);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(dimensionEnum);
            dimensionStack.Set(value);

            int listIndex2 = 0;
            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                // ReSharper disable once InvertIf
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

        public override void SetValue(string name, int listIndex, double value)
        {
            base.SetValue(name, listIndex, value);

            string canonicalName = NameHelper.ToCanonical(name);

            DimensionStack dimensionStack = _dimensionStackCollection.GetDimensionStack(canonicalName);
            dimensionStack.Set(value);

            int listIndex2 = 0;
            foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
            {
                // ReSharper disable once InvertIf
                if (string.Equals(inputCalculator.CanonicalName, canonicalName))
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

        // Reset

        public override void Reset(double time)
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

        public override void Reset(double time, string name)
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
            // ReSharper disable once InvertIf
            if (_name_To_ResettableOperatorCalculators_Dictionary.TryGetValue(canonicalName, out calculators))
            {
                foreach (OperatorCalculatorBase calculator in calculators)
                {
                    calculator.Reset();
                }
            }
        }

        public override void Reset(double time, int listIndex)
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
            // ReSharper disable once InvertIf
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
