using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using NAudio.Midi;

namespace JJ.Presentation.Synthesizer.NAudio
{
	/// <summary> thread-safe </summary>
	internal static class MidiInputProcessor
	{
		private static IPatchCalculatorContainer _patchCalculatorContainer;
		private static TimeProvider _timeProvider;
		private static NoteRecycler _noteRecycler;
		private static MidiIn _midiIn;
		private static MidiMappingCalculator _midiMappingCalculator;
		private static Dictionary<int, int> _controllerValueDictionary;

		/// <summary>
		/// Key is Scale ID. Value is frequency array.
		/// Caching prevents sorting the tones all the time.
		/// </summary>
		private static Dictionary<int, double[]> _scaleID_To_Frequencies_Dictionary;

		private static readonly object _lock = new object();

		/// <summary> Can be called more than once. </summary>
		public static void Initialize(
			IPatchCalculatorContainer patchCalculatorContainer,
			TimeProvider timeProvider,
			NoteRecycler noteRecycler,
			IDocumentRepository documentRepository)
		{
			lock (_lock)
			{
				// NOTE: Don't turn into throw expressions.
				// You want to check them all for null, before assigning any of them.
				if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);
				if (timeProvider == null) throw new NullException(() => timeProvider);
				if (noteRecycler == null) throw new NullException(() => noteRecycler);

				_patchCalculatorContainer = patchCalculatorContainer;
				_timeProvider = timeProvider;
				_noteRecycler = noteRecycler;

				_controllerValueDictionary = new Dictionary<int, int>();
				_scaleID_To_Frequencies_Dictionary = new Dictionary<int, double[]>();

				var systemFacade = new SystemFacade(documentRepository);
				_midiMappingCalculator = new MidiMappingCalculator(systemFacade.GetDefaultMidiMappingElements());
			}
		}

		public static Thread StartThread()
		{
			var thread = new Thread(TryStart);
			thread.Start();

			return thread;
		}

		/// <summary> 
		/// For now will only work with the first MIDI device it finds. 
		/// Does nothing when no MIDI devices.
		/// </summary>
		private static void TryStart()
		{
			if (MidiIn.NumberOfDevices == 0)
			{
				// TODO: Handle this better.
				return;
				//throw new Exception("No connected MIDI devices.");
			}

			const int deviceIndex = 0;

			try
			{
				lock (_lock)
				{
					_midiIn = new MidiIn(deviceIndex);
					_midiIn.MessageReceived += _midiIn_MessageReceived;
					_midiIn.Start();
				}
			}
			catch
			{
				// Do not crash your whole application, if midi device communication fails.
				Stop();
			}
		}

		public static void Stop()
		{
			lock (_lock)
			{
				if (_midiIn == null)
				{
					return;
				}

				try
				{
					_midiIn.MessageReceived -= _midiIn_MessageReceived;
					// Not sure if I need to call of these methods,
					// but when I omitted one I got an error upon application exit.
					_midiIn.Stop();
					_midiIn.Close();
					_midiIn.Dispose();
				}
				catch
				{
					// Do not crash your whole application, if midi device communication fails.
				}
			}
		}

		private static void _midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
		{
			switch (e.MidiEvent.CommandCode)
			{
				case MidiCommandCode.NoteOn:
					HandleNoteOn(e.MidiEvent);
					break;

				case MidiCommandCode.NoteOff:
					HandleNoteOff(e.MidiEvent);
					break;

				case MidiCommandCode.ControlChange:
					HandleControlChange(e.MidiEvent);
					break;
			}
		}

		private static void HandleNoteOn(MidiEvent midiEvent)
		{
			var noteOnEvent = (NoteOnEvent)midiEvent;

			ReaderWriterLockSlim calculatorLock = _patchCalculatorContainer.Lock;

			calculatorLock.EnterWriteLock();
			try
			{
				IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
				if (calculator == null)
				{
					return;
				}

				double time = _timeProvider.Time;

				NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToStart(noteOnEvent.NoteNumber, time);
				if (noteInfo == null)
				{
					return;
				}
				noteInfo.NoteNumber = noteOnEvent.NoteNumber;
				noteInfo.Velocity = noteOnEvent.Velocity;

				calculator.Reset(time, noteInfo.ListIndex);

				ApplyMappings(calculator, noteInfo);

				calculator.SetValue(DimensionEnum.NoteStart, noteInfo.ListIndex, time);
				calculator.SetValue(DimensionEnum.NoteDuration, noteInfo.ListIndex, CalculationHelper.VERY_HIGH_VALUE);
			}
			finally
			{
				calculatorLock.ExitWriteLock();
			}
		}

		private static void HandleNoteOff(MidiEvent midiEvent)
		{
			var noteEvent = (NoteEvent)midiEvent;

			ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

			lck.EnterWriteLock();
			try
			{
				IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
				if (calculator == null)
				{
					return;
				}

				double time = _timeProvider.Time;

				NoteInfo noteInfo = _noteRecycler.TryGetNoteInfoToRelease(noteEvent.NoteNumber, time);
				if (noteInfo == null)
				{
					return;
				}

				double noteStart = calculator.GetValue(DimensionEnum.NoteStart, noteInfo.ListIndex);
				double noteDuration = time - noteStart;
				calculator.SetValue(DimensionEnum.NoteDuration, noteInfo.ListIndex, noteDuration);

				double releaseDuration = calculator.GetValue(DimensionEnum.ReleaseDuration, noteInfo.ListIndex);
				double releaseTime = noteStart + noteDuration;
				double endTime = releaseTime + releaseDuration;

				_noteRecycler.ReleaseNote(noteInfo, releaseTime, endTime);
			}
			finally
			{
				lck.ExitWriteLock();
			}
		}

		private static void HandleControlChange(MidiEvent midiEvent)
		{
			var controlChangeEvent = (ControlChangeEvent)midiEvent;

			Debug.WriteLine($"ControlChange value received: {controlChangeEvent.Controller} = {controlChangeEvent.ControllerValue}");

			int controllerCode = (int)controlChangeEvent.Controller;

			ReaderWriterLockSlim lck = _patchCalculatorContainer.Lock;

			lck.EnterWriteLock();
			try
			{
				IPatchCalculator calculator = _patchCalculatorContainer.Calculator;
				if (calculator == null)
				{
					return;
				}

				double time = _timeProvider.Time;

				if (!_controllerValueDictionary.TryGetValue(controllerCode, out int previousControllerValue))
				{
					// TODO: Initialize to the calculator's value converted back to a controller value.
					previousControllerValue = MidiMappingCalculator.MIDDLE_CONTROLLER_VALUE;
				}

				int absoluteControllerValue = _midiMappingCalculator.ToAbsoluteControllerValue(
					controllerCode,
					controlChangeEvent.ControllerValue,
					previousControllerValue);

				_controllerValueDictionary[controllerCode] = absoluteControllerValue;

				IList<NoteInfo> noteInfos = _noteRecycler.GetPlayingNoteInfos(time);
				int noteInfoCount = noteInfos.Count;
				for (int i = 0; i < noteInfoCount; i++)
				{
					NoteInfo noteInfo = noteInfos[i];
					ApplyMappings(calculator, noteInfo);
				}
			}
			finally
			{
				lck.ExitWriteLock();
			}
		}

		private static void ApplyMappings(IPatchCalculator patchCalculator, NoteInfo noteInfo)
		{
			// TODO: Prevent garbage collection.
			IList<(int, int)> controllerCodesAndValues = _controllerValueDictionary.Select(x => (x.Key, x.Value)).ToArray();
			_midiMappingCalculator.Calculate(controllerCodesAndValues, noteInfo.NoteNumber, noteInfo.Velocity);

			// Apply Dimension-Related MIDI Mappings
			{
				int count = _midiMappingCalculator.Results.Count;
				for (int i = 0; i < count; i++)
				{
					MidiMappingCalculatorResult mappingResult = _midiMappingCalculator.Results[i];
					if (!mappingResult.DimensionValue.HasValue) continue;

					if (mappingResult.StandardDimensionEnum != default)
					{
						patchCalculator.SetValue(mappingResult.StandardDimensionEnum, noteInfo.ListIndex, mappingResult.DimensionValue.Value);

						Debug.WriteLine(
							$"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.StandardDimensionEnum, noteInfo.ListIndex, mappingResult.DimensionValue.Value }}");
					}

					if (NameHelper.IsFilledIn(mappingResult.CustomDimensionName))
					{
						patchCalculator.SetValue(mappingResult.CustomDimensionName, noteInfo.ListIndex, mappingResult.DimensionValue.Value);

						Debug.WriteLine(
							$"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.CustomDimensionName, noteInfo.ListIndex, mappingResult.DimensionValue.Value }}");
					}
				}
			}

			// Apply Scale-Related MIDI Mappings
			{
				int count = _midiMappingCalculator.Results.Count;
				for (int i = 0; i < count; i++)
				{
					MidiMappingCalculatorResult mappingResult = _midiMappingCalculator.Results[i];
					double? dimensionValue = TryGetScaleFrequency(mappingResult);

					if (!dimensionValue.HasValue)
					{
						continue;
					}

					if (mappingResult.StandardDimensionEnum != default)
					{
						patchCalculator.SetValue(mappingResult.StandardDimensionEnum, noteInfo.ListIndex, dimensionValue.Value);

						Debug.WriteLine(
							$"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.StandardDimensionEnum, noteInfo.ListIndex, frequency = dimensionValue }}");
					}

					if (NameHelper.IsFilledIn(mappingResult.CustomDimensionName))
					{
						patchCalculator.SetValue(mappingResult.CustomDimensionName, noteInfo.ListIndex, dimensionValue.Value);

						Debug.WriteLine(
							$"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.CustomDimensionName, noteInfo.ListIndex, frequency = dimensionValue }}");
					}
				}
			}
		}

		private static double? TryGetScaleFrequency(MidiMappingCalculatorResult mappingResult)
		{
			if (mappingResult.Scale == null) return null;
			if (!mappingResult.ToneNumber.HasValue) return null;

			if (!_scaleID_To_Frequencies_Dictionary.TryGetValue(mappingResult.Scale.ID, out double[] frequencies))
			{
				frequencies = mappingResult.Scale.Tones
				                           .Sort()
				                           .Select(x => x.GetFrequency())
				                           .ToArray();

				_scaleID_To_Frequencies_Dictionary[mappingResult.Scale.ID] = frequencies;
			}

			double frequency = frequencies[mappingResult.ToneNumber.Value - 1];

			return frequency;

		}
	}
}