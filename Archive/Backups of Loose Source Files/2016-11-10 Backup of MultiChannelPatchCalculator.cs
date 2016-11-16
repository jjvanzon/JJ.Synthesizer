//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Patches
//{
//    internal class MultiChannelPatchCalculator : IPatchCalculator
//    {
//        // TODO: Also make a faster MultiChannelPatchCalculator for 2 channels. And perhaps others too.

//        private readonly int _channelCount;
//        private readonly SingleChannelPatchCalculator[] _singleChannelPatchCalculators;
//        private readonly double _frameDuration;

//        public MultiChannelPatchCalculator(
//            IList<Outlet> channelOutlets,
//            int samplingRate,
//            CalculatorCache calculatorCache,
//            double secondsBetweenApplyFilterVariables,
//            ICurveRepository curveRepository,
//            ISampleRepository sampleRepository,
//            IPatchRepository patchRepository,
//            ISpeakerSetupRepository speakerSetupRepository)
//        {
//            if (channelOutlets == null) throw new NullException(() => channelOutlets);
//            if (channelOutlets.Count == 0) throw new CollectionEmptyException(() => channelOutlets);

//            int channelCount = channelOutlets.Count;

//            _singleChannelPatchCalculators = new SingleChannelPatchCalculator[channelCount];

//            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//            {
//                Outlet channelOutlet = channelOutlets[channelIndex];

//                _singleChannelPatchCalculators[channelIndex] = new SingleChannelPatchCalculator(
//                    channelOutlet,
//                    samplingRate,
//                    channelCount,
//                    channelIndex,
//                    calculatorCache,
//                    secondsBetweenApplyFilterVariables,
//                    curveRepository,
//                    sampleRepository,
//                    patchRepository,
//                    speakerSetupRepository);
//            }

//            _channelCount = channelCount;
//            _frameDuration = 1.0 / samplingRate;
//        }

//        public void Calculate(float[] buffer, double t0)
//        {
//            double frameDuration = _frameDuration;
//            int channelCount = _channelCount;
//            int valueCount = buffer.Length;
//            int frameCount = valueCount / channelCount;

//            double time = t0;

//            int valueIndex = 0;

//            for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
//            {
//                for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//                {
//                    SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                    double value  = singleChannelPatchCalculator.Calculate(time);

//                    // winmm will trip over NaN.
//                    if (Double.IsNaN(value))
//                    {
//                        value = 0;
//                    }

//                    // TODO: This seems unsafe. What happens if the cast is invalid?
//                    buffer[valueIndex] = (float)value;

//                    valueIndex++;
//                }

//                time += frameDuration;
//            }
//        }

//        public double Calculate(double time)
//        {
//            throw new NotSupportedException("Operation not supported. Can only calculate by chunk (use the other overload).");
//        }

//        public double Calculate(double time, int channelIndex)
//        {
//            throw new NotSupportedException("Operation not supported. Can only calculate by chunk (use the other overload).");
//        }

//        public void CloneValues(IPatchCalculator sourceCalculator)
//        {
//            var castedSourceCalculator = sourceCalculator as MultiChannelPatchCalculator;
//            if (castedSourceCalculator == null)
//            {
//                throw new InvalidTypeException<MultiChannelPatchCalculator>(() => castedSourceCalculator);
//            }

//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator source = castedSourceCalculator._singleChannelPatchCalculators[channelIndex];
//                SingleChannelPatchCalculator dest = _singleChannelPatchCalculators[channelIndex];

//                dest.CloneValues(source);
//            }
//        }

//        public double GetValue(DimensionEnum dimensionEnum)
//        {
//            SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[0];
//            return singleChannelPatchCalculator.GetValue(dimensionEnum);
//        }

//        public double GetValue(string name)
//        {
//            SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[0];
//            return singleChannelPatchCalculator.GetValue(name);
//        }

//        public double GetValue(int listIndex)
//        {
//            SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[0];
//            return singleChannelPatchCalculator.GetValue(listIndex);
//        }

//        public double GetValue(DimensionEnum dimensionEnum, int listIndex)
//        {
//            SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[0];
//            return singleChannelPatchCalculator.GetValue(dimensionEnum, listIndex);
//        }

//        public double GetValue(string name, int listIndex)
//        {
//            SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[0];
//            return singleChannelPatchCalculator.GetValue(name, listIndex);
//        }

//        public void Reset(double time)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.Reset(time);
//            }
//        }

//        public void Reset(double time, int listIndex)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.Reset(time, listIndex);
//            }
//        }

//        public void Reset(double time, string name)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.Reset(time, name);
//            }
//        }

//        public void SetValue(string name, double value)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.SetValue(name, value);
//            }
//        }

//        public void SetValue(DimensionEnum dimensionEnum, double value)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.SetValue(dimensionEnum, value);
//            }
//        }

//        public void SetValue(int listIndex, double value)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.SetValue(listIndex, value);
//            }
//        }

//        public void SetValue(DimensionEnum dimensionEnum, int listIndex, double value)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.SetValue(dimensionEnum, listIndex, value);
//            }
//        }

//        public void SetValue(string name, int listIndex, double value)
//        {
//            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
//            {
//                SingleChannelPatchCalculator singleChannelPatchCalculator = _singleChannelPatchCalculators[channelIndex];
//                singleChannelPatchCalculator.SetValue(name, listIndex, value);
//            }
//        }
//    }
//}
