using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.Exceptions.TypeChecking;

namespace JJ.Presentation.Synthesizer.NAudio
{
	internal class MultiThreadedPatchCalculator : PatchCalculatorBase
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
			RepositoryWrapper repositories)
			: base(samplingRate, channelCount, channelIndex: default)
		{
			if (patch == null) throw new NullException(() => patch);
			if (maxConcurrentNotes <= 0) throw new LessThanOrEqualException(() => maxConcurrentNotes, 0);
			if (repositories == null) throw new NullException(() => repositories);

			_noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);

			_maxConcurrentNotes = maxConcurrentNotes;

			// Prepare some patching variables

			var calculatorCache = new CalculatorCache();

			Outlet soundOutlet = patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
									  .SelectMany(x => x.Outlets)
									  .Where(x => x.GetDimensionEnumWithFallback() == DimensionEnum.Sound)
									  .SingleOrDefault();
			if (soundOutlet == null)
			{
				var operatorFactory = new OperatorFactory(patch, repositories);
				soundOutlet = operatorFactory.Number(0);
				soundOutlet.Operator.Name = "Dummy operator, because Auto-Patch has no signal outlets.";
			}

			// Create PatchCalculators
			var patchFacade = new PatchFacade(repositories);
			_patchCalculators = new IPatchCalculator[_channelCount][];
			for (int channelIndex = 0; channelIndex < _channelCount; channelIndex++)
			{
				_patchCalculators[channelIndex] = patchFacade.CreateCalculators(
					_maxConcurrentNotes,
					soundOutlet,
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
				bool noteHasStopped = _noteRecycler.NoteHasStopped(noteIndex, t0);
				if (noteHasStopped)
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

			// ReSharper disable once UsePatternMatching
			var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
			if (castedSourceCalculator == null)
			{
				throw new IsNotTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
			}

			// base.CloneValues may have yielded over all values to all patch calculators,
			// but could not be specific about which note.
			// By cloning the values per note here, we do accomplish setting the right values for the right notes.

			int maxChannelCount = Math.Min(_channelCount, castedSourceCalculator._channelCount);
			for (int channelIndex = 0; channelIndex < maxChannelCount; channelIndex++)
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
