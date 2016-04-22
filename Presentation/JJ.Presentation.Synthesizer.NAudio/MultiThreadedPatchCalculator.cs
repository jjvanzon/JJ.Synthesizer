using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;
using System.Threading.Tasks;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Canonical;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculator : IPatchCalculator
    {
        // TODO: Not even sure I need this Info class anymore.
        private class PatchCalculatorInfo
        {
            public PatchCalculatorInfo(int noteIndex, int channelIndex, IPatchCalculator patchCalculator)
            {
                NoteIndex = noteIndex;
                ChannelIndex = channelIndex;
                PatchCalculator = patchCalculator;
            }

            public int NoteIndex { get; private set; }
            public int ChannelIndex { get; private set; }
            public IPatchCalculator PatchCalculator { get; private set; }

            public bool IsActive { get; set; }
        }

        private const int TIME_DIMENSION_INDEX = (int)DimensionEnum.Time;
        private const int CHANNEL_DIMENSION_INDEX = (int)DimensionEnum.Channel;

        private readonly NoteRecycler _noteRecycler;

        private readonly double[] _emptyBuffer;

        /// <summary> First index is channel, second index is frame. </summary>
        private readonly double[][] _buffers;
        /// <summary> First index is channel, second index is frame. </summary>
        private readonly object[][] _bufferLocks;
        /// <summary> First index is NoteIndex, second index is channel. </summary>
        private readonly PatchCalculatorInfo[][] _patchCalculatorInfos;

        private readonly int _frameCount;
        private readonly double _frameDuration;
        private readonly int _channelCount;
        private readonly int _maxConcurrentNotes;
        private double _t0;

        public MultiThreadedPatchCalculator(
            Patch patch,
            AudioOutput audioOutput,
            NoteRecycler noteRecycler,
            RepositoryWrapper repositories)
        {
            if (patch == null) throw new NullException(() => patch);
            if (audioOutput == null) throw new NullException(() => audioOutput);
            if (noteRecycler == null) throw new NullException(() => noteRecycler);
            if (repositories == null) throw new NullException(() => repositories);

            AssertAudioOutput(audioOutput, repositories);

            // Get Audio Properties
            double frameDuration = audioOutput.GetSampleDuration();
            int channelCount = audioOutput.GetChannelCount();
            int maxConcurrentNotes = audioOutput.MaxConcurrentNotes;

            // TODO: Solve whatever problem makes us have to divide by 2
            // to not get jittery (mono) sound!
            int frameCount = audioOutput.GetBufferFrameCount() / 2;

            // Create Buffers
            double[] emptyBuffer = new double[frameCount];

            double[][] buffers = new double[channelCount][];
            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
                buffers[channelIndex] = new double[frameCount];
            }

            object[][] bufferLocks = new object[channelCount][];
            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
            {
                bufferLocks[channelIndex] = new object[frameCount];

                for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                {
                    bufferLocks[channelIndex][frameIndex] = new object();
                }
            }

            // Prepare some patching variables
            var patchManager = new PatchManager(new PatchRepositories(repositories));
            patchManager.Patch = patch;

            var calculatorCache = new CalculatorCache();

            Outlet signalOutlet = patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                       .Where(x => x.Result.GetDimensionEnum() == DimensionEnum.Signal)
                                       .SingleOrDefault();
            if (signalOutlet == null)
            {
                signalOutlet = patchManager.Number(0.0);
                signalOutlet.Operator.Name = "Dummy operator, because Auto-Patch has no signal outlets.";
            }

            // Create PatchCalculator(Infos)
            PatchCalculatorInfo[][] patchCalculatorInfos = new PatchCalculatorInfo[maxConcurrentNotes][];
            for (int noteIndex = 0; noteIndex < maxConcurrentNotes; noteIndex++)
            {
                patchCalculatorInfos[noteIndex] = new PatchCalculatorInfo[channelCount];

                for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
                {
                    IPatchCalculator patchCalculator = patchManager.CreateCalculator(signalOutlet, calculatorCache);
                    patchCalculatorInfos[noteIndex][channelIndex] = new PatchCalculatorInfo(noteIndex, channelIndex, patchCalculator);
                }
            }

            // Assign Fields
            _noteRecycler = noteRecycler;
            _frameDuration = frameDuration;
            _channelCount = channelCount;
            _maxConcurrentNotes = maxConcurrentNotes;
            _emptyBuffer = emptyBuffer;
            _buffers = buffers;
            _bufferLocks = bufferLocks;
            _frameCount = frameCount;
            _patchCalculatorInfos = patchCalculatorInfos;
        }

        // Calculate

        /// <param name="sampleDuration">
        /// Not used. Alternative value is determined internally.
        /// This parameter is currently not used, but I want this abstraction to stay similar
        /// to PatchCalculator, or I would be refactoring my brains out.
        /// </param>
        /// <param name="count">
        /// Not used. Alternative value is determined internally.
        /// This parameter is currently not used, but I want this abstraction to stay similar
        /// to PatchCalculator, or I would be refactoring my brains out.
        /// </param>
        /// <param name="dimensionStack">
        /// Not used. Alternative value is determined internally.
        /// This parameter is currently not used, but I want this abstraction to stay similar
        /// to PatchCalculator, or I would be refactoring my brains out.
        /// </param>
        public double[] Calculate(double t0, double sampleDuration, int count, DimensionStack dimensionStack)
        {
            _t0 = t0;

            double channelIndexDouble = dimensionStack.Get(DimensionEnum.Channel);
            if (!CanCastToInt32(channelIndexDouble))
            {
                return _emptyBuffer;
            }
            int channelIndex = (int)channelIndexDouble;

            double[] buffer = _buffers[channelIndex];

            Array.Clear(buffer, 0, buffer.Length);

            var tasks = new List<Task>(_maxConcurrentNotes);

            for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
            {
                bool isActive = !_noteRecycler.IsNoteReleased(noteIndex, _t0);
                if (!isActive)
                {
                    continue;
                }

                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[noteIndex][channelIndex];
                Task task = Task.Factory.StartNew(() => CalculateSingleThread(patchCalculatorInfo));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            return buffer;
        }

        public double Calculate(DimensionStack dimensionStack)
        {
            throw new NotSupportedException("Operation not supported. Can only calculate by chunk (use the other overload).");
        }

        private void CalculateSingleThread(PatchCalculatorInfo patchCalculatorInfo)
        {
            int channelIndex = patchCalculatorInfo.ChannelIndex;
            double[] buffer = _buffers[channelIndex];
            object[] bufferLocks = _bufferLocks[channelIndex];
            IPatchCalculator patchCalculator = patchCalculatorInfo.PatchCalculator;

            var dimensionStack = new DimensionStack();
            dimensionStack.Set(CHANNEL_DIMENSION_INDEX, channelIndex);

            double t = _t0;
            for (int frameIndex = 0; frameIndex < _frameCount; frameIndex++)
            {
                dimensionStack.Set(TIME_DIMENSION_INDEX, t);

                double value = patchCalculator.Calculate(dimensionStack);

                // TODO: Low priority: Not sure how to do a quicker interlocked add for doubles.
                lock (bufferLocks[frameIndex])
                {
                    buffer[frameIndex] += value;
                }

                t += _frameDuration;
            }
        }

        // Values

        public double GetValue(int noteIndex)
        {
            throw new NotSupportedException();
        }

        public void SetValue(int noteIndex, double value)
        {
            throw new NotSupportedException();
        }

        public double GetValue(string name)
        {
            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[0][0];

            if (patchCalculatorInfo != null)
            {
                return patchCalculatorInfo.PatchCalculator.GetValue(name);
            }

            return 0.0;
        }

        public void SetValue(string name, double value)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i][j];
                    patchCalculatorInfo.PatchCalculator.SetValue(name, value);
                }
            }
        }

        public double GetValue(string name, int noteIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteIndex);
            return patchCalculatorInfo.PatchCalculator.GetValue(name);
        }

        public void SetValue(string name, int noteIndex, double value)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteIndex);
            patchCalculatorInfo.PatchCalculator.SetValue(name, value);
        }

        public double GetValue(DimensionEnum dimensionEnum)
        {
            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[0][0];

            if (patchCalculatorInfo != null)
            {
                return patchCalculatorInfo.PatchCalculator.GetValue(dimensionEnum);
            }

            return 0.0;
        }

        public void SetValue(DimensionEnum dimensionEnum, double value)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i][j];
                    patchCalculatorInfo.PatchCalculator.SetValue(dimensionEnum, value);
                }
            }
        }

        public double GetValue(DimensionEnum dimensionEnum, int noteIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteIndex);

            double value = patchCalculatorInfo.PatchCalculator.GetValue(dimensionEnum);
            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, int noteIndex, double value)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteIndex);

            patchCalculatorInfo.PatchCalculator.SetValue(dimensionEnum, value);
        }

        public void Reset(DimensionStack dimensionStack, int noteIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteIndex);
            patchCalculatorInfo.PatchCalculator.Reset(dimensionStack);
        }

        public void Reset(DimensionStack dimensionStack)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i][j];
                    patchCalculatorInfo.PatchCalculator.Reset(dimensionStack);
                }
            }
        }

        public void Reset(DimensionStack dimensionStack, string name)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i][j];
                    patchCalculatorInfo.PatchCalculator.Reset(dimensionStack, name);
                }
            }
        }

        public void CloneValues(IPatchCalculator sourceCalculator)
        {
            var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
            if (castedSourceCalculator == null)
            {
                throw new IsNotTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
            }

            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    PatchCalculatorInfo source = castedSourceCalculator._patchCalculatorInfos[i][j];
                    PatchCalculatorInfo dest = _patchCalculatorInfos[i][j];

                    dest.PatchCalculator.CloneValues(source.PatchCalculator);
                }
            }
        }

        // Helpers

        private PatchCalculatorInfo GetPatchCalculatorInfo(int noteIndex)
        {
            AssertPatchCalculatorInfosListIndex(noteIndex);

            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[noteIndex].First();
            return patchCalculatorInfo;
        }

        private void AssertPatchCalculatorInfosListIndex(int patchCalcultorInfosListIndex)
        {
            if (patchCalcultorInfosListIndex < 0) throw new LessThanException(() => patchCalcultorInfosListIndex, 0);
            if (patchCalcultorInfosListIndex >= _patchCalculatorInfos.Length) throw new GreaterThanOrEqualException(() => patchCalcultorInfosListIndex, () => _patchCalculatorInfos.Length);
        }

        private void AssertAudioOutput(AudioOutput audioOutput, RepositoryWrapper repositories)
        {
            // Assert validity of AudioOutput values, by delegating to AudioOutputManager.
            // It may not be clear from the interface that this will ensure things are valid for this class,
            // but it is a shame to reprogram the rules here, already programmed out in the business layer.

            var audioOutputManager = new AudioOutputManager(
                repositories.AudioOutputRepository,
                repositories.SpeakerSetupRepository,
                repositories.IDRepository);

            VoidResult voidResult = audioOutputManager.Save(audioOutput);

            ResultHelper.Assert(voidResult);
        }

        private bool CanCastToInt32(double value)
        {
            return value >= Int32.MinValue &&
                   value <= Int32.MaxValue &&
                   !Double.IsNaN(value) &&
                   !Double.IsInfinity(value);
        }

        // Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
        //private static double InterlockedAddDouble(ref double location1, double value)
        //{
        //    double newCurrentValue = 0;
        //    while (true)
        //    {
        //        double currentValue = newCurrentValue;
        //        double newValue = currentValue + value;
        //        newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
        //        if (newCurrentValue == currentValue)
        //            return newValue;
        //    }
        //}
    }
}
