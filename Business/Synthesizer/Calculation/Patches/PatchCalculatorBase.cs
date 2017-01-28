using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public abstract class PatchCalculatorBase : IPatchCalculator
    {
        protected readonly double _frameDuration;
        protected readonly int _channelCount;
        protected readonly int _channelIndex;

        protected readonly Dictionary<DimensionEnum, double> _dimensionEnum_To_Value_Dictionary = new Dictionary<DimensionEnum, double>();
        protected readonly Dictionary<string, double> _name_To_Value_Dictionary = new Dictionary<string, double>();
        protected readonly Dictionary<Tuple<DimensionEnum, int>, double> _dimensionEnumAndListIndex_To_Value_Dictionary = new Dictionary<Tuple<DimensionEnum, int>, double>();
        protected readonly Dictionary<Tuple<string, int>, double> _nameAndListIndex_To_Value_Dictionary = new Dictionary<Tuple<string, int>, double>();
        protected readonly Dictionary<int, double> _listIndex_To_Value_Dictionary = new Dictionary<int, double>();

        public PatchCalculatorBase(int samplingRate, int channelCount, int channelIndex)
        {
            if (channelCount <= 0) throw new LessThanOrEqualException(() => channelCount, 0);
            PatchCalculatorHelper.AssertChannelIndex(channelIndex, channelCount);

            _channelCount = channelCount;
            _channelIndex = channelIndex;

            _frameDuration = 1.0 / samplingRate;
        }

        // Calculate

        public virtual double Calculate(double time)
        {
            // Implementation for backward compatibility.
            // TODO: Lower priority: Refactor this method away completely or at least prefer not using it?

            var buffer = new float[_channelCount];

            Calculate(buffer, frameCount: 1, t0: time);

            return buffer[_channelIndex];
        }

        public abstract void Calculate(float[] buffer, int frameCount, double t0);

        // Values

        public double GetValue(int listIndex)
        {
            double value;
            _listIndex_To_Value_Dictionary.TryGetValue(listIndex, out value);
            return value;
        }

        public virtual void SetValue(int listIndex, double value)
        {
            _listIndex_To_Value_Dictionary[listIndex] = value;
        }

        public double GetValue(DimensionEnum dimensionEnum)
        {
            double value;
            _dimensionEnum_To_Value_Dictionary.TryGetValue(dimensionEnum, out value);
            return value;
        }

        public virtual void SetValue(DimensionEnum dimensionEnum, double value)
        {
            _dimensionEnum_To_Value_Dictionary[dimensionEnum] = value;
        }

        public double GetValue(string name)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            double value;
            _name_To_Value_Dictionary.TryGetValue(canonicalName, out value);
            return value;
        }

        public virtual void SetValue(string name, double value)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            _name_To_Value_Dictionary[canonicalName] = value;
        }

        public double GetValue(DimensionEnum dimensionEnum, int listIndex)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            double value;
            _dimensionEnumAndListIndex_To_Value_Dictionary.TryGetValue(key, out value);
            return value;
        }

        public virtual void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
        {
            var key = new Tuple<DimensionEnum, int>(dimensionEnum, listIndex);

            _dimensionEnumAndListIndex_To_Value_Dictionary[key] = value;
        }

        public double GetValue(string name, int listIndex)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            var key = new Tuple<string, int>(canonicalName, listIndex);

            double value;
            _nameAndListIndex_To_Value_Dictionary.TryGetValue(key, out value);
            return value;
        }

        public virtual void SetValue(string name, int listIndex, double value)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            var key = new Tuple<string, int>(canonicalName, listIndex);

            _nameAndListIndex_To_Value_Dictionary[key] = value;
        }

        public virtual void CloneValues(IPatchCalculator sourcePatchCalculator)
        {
            if (sourcePatchCalculator == null) throw new NullException(() => sourcePatchCalculator);

            var castedSource = sourcePatchCalculator as PatchCalculatorBase;
            if (castedSource == null)
            {
                throw new InvalidTypeException<PatchCalculatorBase>(() => sourcePatchCalculator);
            }

            foreach (var entry in castedSource._listIndex_To_Value_Dictionary)
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

            foreach (var entry in castedSource._dimensionEnumAndListIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }

            foreach (var entry in castedSource._nameAndListIndex_To_Value_Dictionary)
            {
                SetValue(entry.Key.Item1, entry.Key.Item2, entry.Value);
            }
        }

        // Reset

        public abstract void Reset(double time);

        public virtual void Reset(double time, string name)
        {
            throw new NotSupportedException();
        }

        public virtual void Reset(double time, int listIndex)
        {
            throw new NotSupportedException();
        }
    }
}
