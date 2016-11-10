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
using System.Threading;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculator : IPatchCalculator
    {
        private readonly int _channelCount;
        private readonly int _maxConcurrentNotes;
        private readonly NoteRecycler _noteRecycler;
        /// <summary> First index is NoteIndex, second index is channel. </summary>
        private readonly SingleChannelPatchCalculator[][] _patchCalculators;

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

            // Create PatchCalculators
            _patchCalculators = new SingleChannelPatchCalculator[_maxConcurrentNotes][];
            for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
            {
                _patchCalculators[noteIndex] = new SingleChannelPatchCalculator[_channelCount];

                for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
                {
                    // TODO: How assumptious it is SingleChannelPatchCalculator...
                    SingleChannelPatchCalculator patchCalculator = (SingleChannelPatchCalculator)patchManager.CreateCalculator(signalOutlet, samplingRate, _channelCount, channelIndex, calculatorCache);
                    _patchCalculators[noteIndex][channelIndex] = patchCalculator;
                }
            }
        }

        // Calculate

        /// <param name="frameCount">
        /// You cannot use buffer.Length as a basis for frameCount, 
        /// because if you write to the buffer beyond frameCount, then the audio driver might fail.
        /// A frameCount based on the entity model can differ from the frameCount you get from the driver,
        /// and you only know the frameCount at the time the driver calls us.
        /// </param>
        public void Calculate(float[] buffer, int frameCount, double t0)
        {
            int maxConcurrentNotes = _maxConcurrentNotes;
            int channelCount = _channelCount;
            Array.Clear(buffer, 0, buffer.Length);

            var tasks = new List<Task>(_maxConcurrentNotes * _channelCount);

            for (int noteIndex = 0; noteIndex < maxConcurrentNotes; noteIndex++)
            {
                bool noteIsReleased = _noteRecycler.NoteIsReleased(noteIndex, t0);
                if (noteIsReleased)
                {
                    continue;
                }

                for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
                {
                    // Capture variable in loop iteration,
                    // to prevent delegate from getting a value from a different iteration.
                    SingleChannelPatchCalculator patchCalculator = _patchCalculators[noteIndex][channelIndex];

                    Task task = Task.Factory.StartNew(() => patchCalculator.Calculate(buffer, frameCount, t0));
                    tasks.Add(task);
                }
            }

            // Note: We cannot use an array, because we do not know the array size in advance,
            // because we skip adding tasks if note is released.
            // Yet, Task.WaitAll wants an array.
            Task.WaitAll(tasks.ToArray());
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

            IPatchCalculator patchCalculator = _patchCalculators[noteIndex][0];

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

            IPatchCalculator patchCalculator = _patchCalculators[noteIndex][0];

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
    }
}
