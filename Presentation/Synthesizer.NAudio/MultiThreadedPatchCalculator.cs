using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculator : PatchCalculatorBase
    {
        private readonly int _maxConcurrentNotes;
        private readonly NoteRecycler _noteRecycler;
        /// <summary> 1st index is channel, 2nd index is note index. </summary>
        private readonly IPatchCalculator[][] _patchCalculators;

        /// <param name="patch">Must have at most one PatchOutlet of type Signal.</param>
        public MultiThreadedPatchCalculator(
            Patch patch,
            int samplingRate,
            int channelCount,
            int maxConcurrentNotes,
            NoteRecycler noteRecycler,
            PatchRepositories repositories)
            : base(samplingRate, channelCount, channelIndex: default(int))
        {
            if (patch == null) throw new NullException(() => patch);
            if (maxConcurrentNotes <= 0) throw new LessThanOrEqualException(() => maxConcurrentNotes, 0);
            if (repositories == null) throw new NullException(() => repositories);

            _noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);

            _maxConcurrentNotes = maxConcurrentNotes;

            // Prepare some patching variables

            var patchManager = new PatchManager(patch, repositories);
            var calculatorCache = new CalculatorCache();

            Outlet signalOutlet = patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                       .Where(x => x.DimensionEnum == DimensionEnum.Signal)
                                       .SingleOrDefault();
            if (signalOutlet == null)
            {
                signalOutlet = patchManager.Number();
                signalOutlet.Operator.Name = "Dummy operator, because Auto-Patch has no signal outlets.";
            }

            // Create PatchCalculators
            _patchCalculators = new IPatchCalculator[_channelCount][];
            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                _patchCalculators[channelIndex] = patchManager.CreateCalculators(
                    _maxConcurrentNotes,
                    signalOutlet,
                    samplingRate,
                    _channelCount,
                    channelIndex,
                    calculatorCache).ToArray();
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
                    IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];

                    Task task = Task.Factory.StartNew(() => patchCalculator.Calculate(buffer, frameCount, t0));
                    tasks.Add(task);
                }
            }

            // Note: We cannot use an array, because we do not know the array size in advance,
            // because we skip adding tasks if note is released.
            // Yet, Task.WaitAll wants an array.
            Task.WaitAll(tasks.ToArray());
        }

        // Values

        public override void SetValue(int noteIndex, double value)
        {
            base.SetValue(noteIndex, value);

            // TODO: Figure out why nothing is done here. If you figured it out, document the reason here.
        }

        public override void SetValue(string name, double value)
        {
            base.SetValue(name, value);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                    patchCalculator.SetValue(name, value);
                }
            }
        }

        public override void SetValue(string name, int noteIndex, double value)
        {
            base.SetValue(name, noteIndex, value);

            if (!IsValidNoteIndex(noteIndex))
            {
                return;
            }

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                patchCalculator.SetValue(name, value);
            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, double value)
        {
            base.SetValue(dimensionEnum, value);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                    patchCalculator.SetValue(dimensionEnum, value);
                }
            }
        }

        public override void SetValue(DimensionEnum dimensionEnum, int noteIndex, double value)
        {
            base.SetValue(dimensionEnum, noteIndex, value);

            if (!IsValidNoteIndex(noteIndex))
            {
                return;
            }

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                patchCalculator.SetValue(dimensionEnum, value);
            }
        }

        public override void CloneValues(IPatchCalculator sourceCalculator)
        {
            base.CloneValues(sourceCalculator);

            var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
            if (castedSourceCalculator == null)
            {
                throw new InvalidTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
            }

            // base.CloneValues may have yielded over all values to all patch calculators,
            // but could not be specific about which note.
            // By cloning the values per note here, we do accomplish setting the right values for te right notes.

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                int maxConcurrentNotes = Math.Min(_maxConcurrentNotes, castedSourceCalculator._maxConcurrentNotes);

                for (int noteIndex = 0; noteIndex < maxConcurrentNotes; noteIndex++)
                {
                    IPatchCalculator source = castedSourceCalculator._patchCalculators[channelIndex][noteIndex];
                    IPatchCalculator dest = _patchCalculators[channelIndex][noteIndex];

                    dest.CloneValues(source);
                }
            }
        }

        // Reset

        public override void Reset(double time, int noteIndex)
        {
            AssertNoteIndex(noteIndex);

            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                patchCalculator.Reset(time);
            }
        }

        public override void Reset(double time)
        {
            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                    patchCalculator.Reset(time);
                }
            }
        }

        public override void Reset(double time, string name)
        {
            for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
            {
                for (int noteIndex = 0; noteIndex < _maxConcurrentNotes; noteIndex++)
                {
                    IPatchCalculator patchCalculator = _patchCalculators[channelIndex][noteIndex];
                    patchCalculator.Reset(time, name);
                }
            }
        }

        // Helpers

        private void AssertNoteIndex(int noteIndex)
        {
            if (!IsValidNoteIndex(noteIndex))
            {
                throw new Exception($"{noteIndex} should be between 0 and {_maxConcurrentNotes - 1}.");
            }
        }
        
        private bool IsValidNoteIndex(int noteIndex)
        {
            if (noteIndex < 0)
            {
                return false;
            }
            int maxNoteIndex = _maxConcurrentNotes - 1;
            return noteIndex <= maxNoteIndex;
        }
    }
}
