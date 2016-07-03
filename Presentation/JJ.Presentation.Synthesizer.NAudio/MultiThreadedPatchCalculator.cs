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
using System.Runtime.CompilerServices;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculator : IPatchCalculator
    {
        private const int TIME_DIMENSION_INDEX = (int)DimensionEnum.Time;
        private const int CHANNEL_DIMENSION_INDEX = (int)DimensionEnum.Channel;

        private readonly double _frameDuration;
        private readonly int _channelCount;
        private readonly int _maxConcurrentNotes;
        private readonly NoteRecycler _noteRecycler;
        /// <summary> First index is NoteIndex, second index is channel. </summary>
        private readonly IPatchCalculator[][] _patchCalculators;

        private int _frameCount;
        private double _t0;
        private double[] _emptyBuffer;
        /// <summary> First index is channel, second index is frame. </summary>
        private double[][] _buffers;
        /// <summary> First index is channel, second index is frame. </summary>
        private object[][] _bufferLocks;

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

            _noteRecycler = noteRecycler;

            _frameDuration = audioOutput.GetFrameDuration();
            _channelCount = audioOutput.GetChannelCount();
            _maxConcurrentNotes = audioOutput.MaxConcurrentNotes;

            // Prepare some patching variables
            int samplingRate = audioOutput.SamplingRate;

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
            _patchCalculators = new IPatchCalculator[_maxConcurrentNotes][];
            for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
            {
                _patchCalculators[noteIndex] = new IPatchCalculator[_channelCount];

                for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
                {
                    IPatchCalculator patchCalculator = patchManager.CreateCalculator(signalOutlet, samplingRate, _channelCount, channelIndex, calculatorCache);
                    _patchCalculators[noteIndex][channelIndex] = patchCalculator;
                }
            }
        }

        /// <summary>
        /// The buffers cannot be initialized up front, because the NAudio API adapts the buffer size
        /// depending on the infrastructural context, and only publically makes the buffer size known
        /// the first time it calls ISampleProvider.Read().
        /// The buffer size could have been guessed up front, but then it would not adapt to different 
        /// infrastructural contexts.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LazyInitializeBuffers(int frameCount)
        {
            // This makes it more than lazy initialization: if the frameCount would suddenly change,
            // it will also adapt itself to the buffer size change.
            if (_frameCount == frameCount) return;
            
            double[] emptyBuffer = new double[frameCount];

            double[][] buffers = new double[_channelCount][];
            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                buffers[channelIndex] = new double[frameCount];
            }

            object[][] bufferLocks = new object[_channelCount][];
            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                bufferLocks[channelIndex] = new object[frameCount];

                for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                {
                    bufferLocks[channelIndex][frameIndex] = new object();
                }
            }

            // Assign fields last
            _frameCount = frameCount;
            _emptyBuffer = emptyBuffer;
            _buffers = buffers;
            _bufferLocks = bufferLocks;
        }

        // Calculate

        /// <param name="frameDuration"> Not used. Alternative value is determined internally. </param>
        public double[] Calculate(double t0, double frameDuration, int frameCount, int channelIndex)
        {
            LazyInitializeBuffers(frameCount);

            _t0 = t0;

            double[] buffer = _buffers[channelIndex];

            Array.Clear(buffer, 0, buffer.Length);

            var tasks = new List<Task>(_maxConcurrentNotes);

            for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
            {
                bool noteIsReleased = _noteRecycler.NoteIsReleased(noteIndex, _t0);
                if (noteIsReleased)
                {
                    continue;
                }

                // Capture variable in loop iteration, 
                // to prevent delegate from getting a value from a different iteration.
                IPatchCalculator patchCalculator = _patchCalculators[noteIndex][channelIndex];

                Task task = Task.Factory.StartNew(() => CalculateSingleThread(patchCalculator, channelIndex));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            return buffer;
        }

        private void CalculateSingleThread(IPatchCalculator patchCalculator, int channelIndex)
        {
            double[] buffer = _buffers[channelIndex];
            object[] bufferLocks = _bufferLocks[channelIndex];

            double t = _t0;
            for (int frameIndex = 0; frameIndex < _frameCount; frameIndex++)
            {
                double value = patchCalculator.Calculate(t);

                // TODO: Low priority: Not sure how to do a quicker interlocked add for doubles.
                lock (bufferLocks[frameIndex])
                {
                    buffer[frameIndex] += value;
                }

                t += _frameDuration;
            }
        }

        public double[] Calculate(double t0, double frameDuration, int frameCount)
        {
            throw new NotSupportedException("Calculate without channelIndex is not supported. Use the overload with channelIndex.");
        }

        public double Calculate(double time, int channelIndex)
        {
            throw new NotSupportedException("Operation not supported. Can only calculate by chunk (use the other overload).");
        }

        public double Calculate(double time)
        {
            throw new NotSupportedException("Operation not supported. Can only calculate by chunk (use the other overload).");
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
            IPatchCalculator patchCalculator = _patchCalculators[0][0];

            if (patchCalculator != null)
            {
                return patchCalculator.GetValue(name);
            }

            return 0.0;
        }

        public void SetValue(string name, double value)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[i][j];
                    patchCalculator.SetValue(name, value);
                }
            }
        }

        public double GetValue(string name, int noteIndex)
        {
            AssertPatchCalculatorNoteIndex(noteIndex);

            IPatchCalculator patchCalculator = _patchCalculators[noteIndex].First();

            return patchCalculator.GetValue(name);
        }

        public void SetValue(string name, int noteIndex, double value)
        {
            AssertPatchCalculatorNoteIndex(noteIndex);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[noteIndex][channelIndex];
                patchCalculator.SetValue(name, value);
            }
        }

        public double GetValue(DimensionEnum dimensionEnum)
        {
            IPatchCalculator patchCalculator = _patchCalculators[0][0];

            if (patchCalculator != null)
            {
                return patchCalculator.GetValue(dimensionEnum);
            }

            return 0.0;
        }

        public void SetValue(DimensionEnum dimensionEnum, double value)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[i][j];
                    patchCalculator.SetValue(dimensionEnum, value);
                }
            }
        }

        public double GetValue(DimensionEnum dimensionEnum, int noteIndex)
        {
            AssertPatchCalculatorNoteIndex(noteIndex);

            IPatchCalculator patchCalculator = _patchCalculators[noteIndex].First();

            double value = patchCalculator.GetValue(dimensionEnum);

            return value;
        }

        public void SetValue(DimensionEnum dimensionEnum, int noteIndex, double value)
        {
            AssertPatchCalculatorNoteIndex(noteIndex);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[noteIndex][channelIndex];
                patchCalculator.SetValue(dimensionEnum, value);
            }
        }

        public void Reset(double time, int noteIndex)
        {
            AssertPatchCalculatorNoteIndex(noteIndex);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[noteIndex][channelIndex];
                patchCalculator.Reset(time);
            }
        }

        public void Reset(double time)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[i][j];
                    patchCalculator.Reset(time);
                }
            }
        }

        public void Reset(double time, string name)
        {
            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[i][j];
                    patchCalculator.Reset(time, name);
                }
            }
        }

        public void CloneValues(IPatchCalculator sourceCalculator)
        {
            var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
            if (castedSourceCalculator == null)
            {
                throw new InvalidTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
            }

            for (int i = 0; i < _maxConcurrentNotes; i++)
            {
                for (int j = 0; j < _channelCount; j++)
                {
                    IPatchCalculator source = castedSourceCalculator._patchCalculators[i][j];
                    IPatchCalculator dest = _patchCalculators[i][j];

                    dest.CloneValues(source);
                }
            }
        }

        // Helpers

        private void AssertPatchCalculatorNoteIndex(int patchCalcultorNoteIndex)
        {
            if (patchCalcultorNoteIndex < 0) throw new LessThanException(() => patchCalcultorNoteIndex, 0);
            if (patchCalcultorNoteIndex >= _patchCalculators.Length) throw new GreaterThanOrEqualException(() => patchCalcultorNoteIndex, () => _patchCalculators.Length);
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

            VoidResult result = audioOutputManager.Save(audioOutput);

            ResultHelper.Assert(result);
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
