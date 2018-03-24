using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Visitors;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
	public class SingleChannelPatchCalculator : PatchCalculatorBase
	{
#if !USE_INVAR_INDICES
		// ReSharper disable once UnusedMember.Local
		private const int TOP_LEVEL_DIMENSION_STACK_INDEX = 0;
#endif
		private static readonly CalculationMethodEnum _calculationMethodEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().CalculationMethod;

		private readonly VariableInput_OperatorCalculator _timeInputCalculator;

		private readonly OperatorCalculatorBase _outputOperatorCalculator;
		/// <summary> Array, instead of IList&lt;T&gt; for optimization in calculating values. </summary>
		private readonly VariableInput_OperatorCalculator[] _inputOperatorCalculators;

		private readonly Dictionary<int, IList<OperatorCalculatorBase>> _position_To_ResettableOperatorCalculators_Dictionary;
		private readonly Dictionary<string, IList<OperatorCalculatorBase>> _name_To_ResettableOperatorCalculators_Dictionary;

		public SingleChannelPatchCalculator(
			Outlet topLevelOutlet,
			int samplingRate,
			int channelCount,
			int channelIndex,
			CalculatorCache calculatorCache,
			ICurveRepository curveRepository,
			ISampleRepository sampleRepository,
			ISpeakerSetupRepository speakerSetupRepository)
			: base(samplingRate, channelCount, channelIndex)
		{
			if (topLevelOutlet == null) throw new NullException(() => topLevelOutlet);

			ToCalculatorResult result;
			switch (_calculationMethodEnum)
			{
				case CalculationMethodEnum.CalculatorClasses:
					{
						var visitor = new OperatorEntityToCalculatorExecutor(
							samplingRate,
							channelCount,
							calculatorCache,
							curveRepository,
							sampleRepository,
							speakerSetupRepository);

						result = visitor.Execute(topLevelOutlet);
						break;
					}

				default:
					throw new ValueNotSupportedException(_calculationMethodEnum);
			}

			// Yield over results to fields.
			_outputOperatorCalculator = result.Output_OperatorCalculator;
			_inputOperatorCalculators = result.Input_OperatorCalculators.Sort().ToArray();
			_position_To_ResettableOperatorCalculators_Dictionary = 
				result.ResettableOperatorTuples.Where(x => x.Position.HasValue)
												// ReSharper disable once PossibleInvalidOperationException
											   .ToNonUniqueDictionary(x => x.Position.Value, x => x.OperatorCalculator);
			_name_To_ResettableOperatorCalculators_Dictionary = result.ResettableOperatorTuples
																	  .ToNonUniqueDictionary(x => NameHelper.ToCanonical(x.Name), x => x.OperatorCalculator);

			foreach (VariableInput_OperatorCalculator inputOperatorCalculator in _inputOperatorCalculators)
			{
				double value = inputOperatorCalculator._value;
				int position = inputOperatorCalculator.Position;
				DimensionEnum dimensionEnum = inputOperatorCalculator.DimensionEnum;
				string name = NameHelper.ToCanonical(inputOperatorCalculator.CanonicalName);

				// Copy input calculator values to dictionaries.
				_position_To_Value_Dictionary[position] = value;
				_dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;
				_name_To_Value_Dictionary[name] = value;
				_dimensionEnumAndPosition_To_Value_Dictionary[(dimensionEnum, position)] = value;
				_nameAndPosition_To_Value_Dictionary[(name, position)] = value;
			}

			// Get special dimensions' inputs.

			// Instead of just getting a Single one with DimensionEnum.Time / DimensionEnum.Channel,
			// make the filters a little more specific and the selection more multiplificy tollerant,
			// because these dimensions can just as well be used by the user,
			// even though it is supposed to be used primarily by the system.
			// Note that e.g. _timeInputCalculator can even be null, if the calculation does not even use time.

			_timeInputCalculator = result.Input_OperatorCalculators
										 .Where(x => x.DimensionEnum == DimensionEnum.Time && x.Position == 0 && x.CanonicalName == "")
										 .DefaultIfEmpty(new VariableInput_OperatorCalculator(DimensionEnum.Time, "", 0, 0))
										 .First();

			VariableInput_OperatorCalculator channelInputCalculator = result.Input_OperatorCalculators
																			.Where(x => x.DimensionEnum == DimensionEnum.Channel && x.Position == 0 && x.CanonicalName == "")
																			.DefaultIfEmpty(new VariableInput_OperatorCalculator(DimensionEnum.Channel, "", 0, 0))
																			.FirstOrDefault();
			// Set special channel dimension value.
			if (channelInputCalculator != null)
			{
				channelInputCalculator._value = channelIndex;
			}
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
			VariableInput_OperatorCalculator timeInputCalculator = _timeInputCalculator;

			double t = t0;

			// Writes values in an interleaved way to the buffer.
			for (int i = channelIndex; i < valueCount; i += channelCount)
			{
				timeInputCalculator._value = t;

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

		public override void SetValue(int position, double value)
		{
			base.SetValue(position, value);

			if (position < 0) return;
			if (position >= _inputOperatorCalculators.Length) return;

			_inputOperatorCalculators[position]._value = value;
		}

		public override void SetValue(DimensionEnum dimensionEnum, double value)
		{
			base.SetValue(dimensionEnum, value);

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

			foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
			{
				if (string.Equals(inputCalculator.CanonicalName, canonicalName))
				{
					inputCalculator._value = value;
				}
			}
		}

		public override void SetValue(DimensionEnum dimensionEnum, int position, double value)
		{
			base.SetValue(dimensionEnum, position, value);

			int position2 = 0;
			foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
			{
				// ReSharper disable once InvertIf
				if (inputCalculator.DimensionEnum == dimensionEnum)
				{
					if (position2 == position)
					{
						_inputOperatorCalculators[position2]._value = value;
						break;
					}

					position2++;
				}
			}
		}

		public override void SetValue(string name, int position, double value)
		{
			base.SetValue(name, position, value);

			string canonicalName = NameHelper.ToCanonical(name);

			int position2 = 0;
			foreach (VariableInput_OperatorCalculator inputCalculator in _inputOperatorCalculators)
			{
				// ReSharper disable once InvertIf
				if (string.Equals(inputCalculator.CanonicalName, canonicalName))
				{
					if (position2 == position)
					{
						_inputOperatorCalculators[position2]._value = value;
						break;
					}

					position2++;
				}
			}
		}

		// Reset

		public override void Reset(double time)
		{
			_timeInputCalculator._value = time;
	 
			//// HACK: Reset does not work for other dimensions than time.
			//// (This means that MidiInputProcessor should reset only for the time dimension,
			//// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
			//foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
			//{
			//	if ((int)dimensionEnum == DimensionEnum.Time)
			//	{
			//		continue;
			//	}
			//	_dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
			//}

			_outputOperatorCalculator.Reset();
		}

		public override void Reset(double time, string name)
		{
			string canonicalName = NameHelper.ToCanonical(name);

			_timeInputCalculator._value = time;
	 
			//// HACK: Reset does not work for other dimensions than time.
			//// (This means that MidiInputProcessor should reset only for the time dimension,
			//// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
			//foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
			//{
			//	if ((int)dimensionEnum == DimensionEnum.Time)
			//	{
			//		continue;
			//	}
			//	_dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
			//}

			// ReSharper disable once InvertIf
			if (_name_To_ResettableOperatorCalculators_Dictionary.TryGetValue(canonicalName, out IList<OperatorCalculatorBase> calculators))
			{
				foreach (OperatorCalculatorBase calculator in calculators)
				{
					calculator.Reset();
				}
			}
		}

		public override void Reset(double time, int position)
		{
			_timeInputCalculator._value = time;
	 
			//// HACK: Reset does not work for other dimensions than time.
			//// (This means that MidiInputProcessor should reset only for the time dimension,
			//// but through the Reset method of IPatchCalculator you cannot be specific about what dimension it is.)
			//foreach (DimensionEnum dimensionEnum in EnumHelper.GetValues<DimensionEnum>())
			//{
			//	if ((int)dimensionEnum == DimensionEnum.Time)
			//	{
			//		continue;
			//	}
			//	_dimensionStackCollection.Set(dimensionEnum, TOP_LEVEL_DIMENSION_STACK_INDEX, 0.0);
			//}

			// ReSharper disable once InvertIf
			if (_position_To_ResettableOperatorCalculators_Dictionary.TryGetValue(position, out IList<OperatorCalculatorBase> calculators))
			{
				foreach (OperatorCalculatorBase calculator in calculators)
				{
					calculator.Reset();
				}
			}
		}
	}
}
