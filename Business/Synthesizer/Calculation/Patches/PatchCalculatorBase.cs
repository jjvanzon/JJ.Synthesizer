using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.Exceptions.TypeChecking;

// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Business.Synthesizer.Calculation.Patches
{
	public abstract class PatchCalculatorBase : IPatchCalculator
	{
		protected readonly double _frameDuration;
		protected readonly int _channelCount;
		protected readonly int _channelIndex;

		protected readonly Dictionary<DimensionEnum, double> _dimensionEnum_To_Value_Dictionary = new Dictionary<DimensionEnum, double>();
		protected readonly Dictionary<string, double> _name_To_Value_Dictionary = new Dictionary<string, double>();
		protected readonly Dictionary<(DimensionEnum, int), double> _dimensionEnumAndPosition_To_Value_Dictionary = new Dictionary<(DimensionEnum, int), double>();
		protected readonly Dictionary<(string, int), double> _nameAndPosition_To_Value_Dictionary = new Dictionary<(string, int), double>();
		protected readonly Dictionary<int, double> _position_To_Value_Dictionary = new Dictionary<int, double>();

		public PatchCalculatorBase(int samplingRate, int channelCount, int channelIndex)
		{
			if (channelCount <= 0) throw new LessThanOrEqualException(() => channelCount, 0);
			PatchCalculatorHelper.AssertChannelIndex(channelIndex, channelCount);

			_channelCount = channelCount;
			_channelIndex = channelIndex;

			_frameDuration = 1.0 / samplingRate;
		}

		// Calculate

		public abstract void Calculate(float[] buffer, int frameCount, double t0);

		// Values

		public double GetValue(int position)
		{
			_position_To_Value_Dictionary.TryGetValue(position, out double value);
			return value;
		}

		public virtual void SetValue(int position, double value)
		{
			_position_To_Value_Dictionary[position] = value;
		}

		public double GetValue(DimensionEnum dimensionEnum)
		{
			_dimensionEnum_To_Value_Dictionary.TryGetValue(dimensionEnum, out double value);
			return value;
		}

		public virtual void SetValue(DimensionEnum dimensionEnum, double value)
		{
			_dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;
		}

		public double GetValue(string name)
		{
			string canonicalName = NameHelper.ToCanonical(name);

			_name_To_Value_Dictionary.TryGetValue(canonicalName, out double value);
			return value;
		}

		public virtual void SetValue(string name, double value)
		{
			string canonicalName = NameHelper.ToCanonical(name);

			_name_To_Value_Dictionary[canonicalName] = value;
		}

		public double GetValue(DimensionEnum dimensionEnum, int position)
		{
			var key = (dimensionEnum, position);

			_dimensionEnumAndPosition_To_Value_Dictionary.TryGetValue(key, out double value);
			return value;
		}

		public virtual void SetValue(DimensionEnum dimensionEnum, int position, double value)
		{
			var key = (dimensionEnum, position);

			_dimensionEnumAndPosition_To_Value_Dictionary[key] = value;
		}

		public double GetValue(string name, int position)
		{
			string canonicalName = NameHelper.ToCanonical(name);

			var key = (canonicalName, position);

			_nameAndPosition_To_Value_Dictionary.TryGetValue(key, out double value);
			return value;
		}

		public virtual void SetValue(string name, int position, double value)
		{
			string canonicalName = NameHelper.ToCanonical(name);

			var key = (canonicalName, position);

			_nameAndPosition_To_Value_Dictionary[key] = value;
		}

		public virtual void CloneValues(IPatchCalculator sourcePatchCalculator)
		{
			if (sourcePatchCalculator == null) throw new NullException(() => sourcePatchCalculator);

			if (!(sourcePatchCalculator is PatchCalculatorBase castedSource))
			{
				throw new IsNotTypeException<PatchCalculatorBase>(() => sourcePatchCalculator);
			}

			foreach (var entry in castedSource._position_To_Value_Dictionary)
			{
				SetValue(entry.Key, entry.Value);
			}

			foreach (var entry in castedSource._dimensionEnum_To_Value_Dictionary)
			{
				SetValue(entry.Key, entry.Value);
			}

			foreach (var entry in castedSource._name_To_Value_Dictionary)
			{
				SetValue(entry.Key, entry.Value);
			}

			foreach (var entry in castedSource._dimensionEnumAndPosition_To_Value_Dictionary)
			{
				SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
			}

			foreach (var entry in castedSource._nameAndPosition_To_Value_Dictionary)
			{
				SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
			}
		}

		// Reset

		public abstract void Reset(double time);
		public virtual void Reset(double time, string name) => throw new NotSupportedException();
		public virtual void Reset(double time, int position) => throw new NotSupportedException();
	}
}
