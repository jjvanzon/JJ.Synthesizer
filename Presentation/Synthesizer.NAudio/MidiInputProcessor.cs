using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using NAudio.Midi;

namespace JJ.Presentation.Synthesizer.NAudio
{
	/// <summary> thread-safe </summary>
	internal class MidiInputProcessor : IDisposable
	{
		private readonly IPatchCalculatorContainer _patchCalculatorContainer;
		private readonly TimeProvider _timeProvider;
		private readonly NoteRecycler _noteRecycler;
		private readonly MidiMappingCalculator _midiMappingCalculator;
		private readonly Dictionary<int, int> _midiControllerDictionary;
		private MidiIn _midiIn;

		/// <summary>
		/// Key is Scale ID. Value is frequency array.
		/// Caching prevents sorting the tones all the time.
		/// </summary>
		private readonly double[] _frequencies;

		private readonly object _lock = new object();

		/// <summary> Can be called more than once. </summary>
		public MidiInputProcessor(
			Scale scale,
			IList<MidiMapping> midiMappingElements,
			IPatchCalculatorContainer patchCalculatorContainer,
			TimeProvider timeProvider,
			NoteRecycler noteRecycler)
		{
			_patchCalculatorContainer = patchCalculatorContainer ?? throw new NullException(() => patchCalculatorContainer);
			_timeProvider = timeProvider ?? throw new NullException(() => timeProvider);
			_noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);

			_midiControllerDictionary = new Dictionary<int, int>();
			_midiMappingCalculator = new MidiMappingCalculator(midiMappingElements);

			_frequencies = new ScaleToDtoConverter().Convert(scale).Frequencies.ToArray();

			var thread = new Thread(TryStart);
			thread.Start();
		}

		/// <summary> 
		/// For now will only work with the first MIDI device it finds. 
		/// Does nothing when no MIDI devices.
		/// </summary>
		private void TryStart()
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
				Dispose();
			}
		}

		public void Dispose()
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

		private void _midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
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

		private void HandleNoteOn(MidiEvent midiEvent)
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
				noteInfo.MidiChannel = noteOnEvent.Channel;

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

		private void HandleNoteOff(MidiEvent midiEvent)
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

		private void HandleControlChange(MidiEvent midiEvent)
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

				if (!_midiControllerDictionary.TryGetValue(controllerCode, out int previousControllerValue))
				{
					// TODO: Initialize to the calculator's value converted back to a controller value.
					previousControllerValue = MidiMappingCalculator.MIDDLE_CONTROLLER_VALUE;
				}

				int absoluteControllerValue = _midiMappingCalculator.ToAbsoluteControllerValue(
					controllerCode,
					controlChangeEvent.ControllerValue,
					previousControllerValue);

				_midiControllerDictionary[controllerCode] = absoluteControllerValue;

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

		private void ApplyMappings(IPatchCalculator patchCalculator, NoteInfo noteInfo)
		{
			_midiMappingCalculator.Calculate(_midiControllerDictionary, noteInfo.NoteNumber, noteInfo.Velocity, noteInfo.MidiChannel);

			// Apply Dimension-Related MIDI Mappings
			{
				int count = _midiMappingCalculator.Results.Count;
				for (int i = 0; i < count; i++)
				{
					MidiMappingCalculatorResult mappingResult = _midiMappingCalculator.Results[i];

					if (mappingResult.DimensionEnum != default)
					{
						patchCalculator.SetValue(mappingResult.DimensionEnum, noteInfo.ListIndex, mappingResult.DimensionValue);

						Debug.WriteLine($"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.DimensionEnum, noteInfo.ListIndex, mappingResult.DimensionValue }}");
					}

					if (NameHelper.IsFilledIn(mappingResult.Name))
					{
						patchCalculator.SetValue(mappingResult.Name, noteInfo.ListIndex, mappingResult.DimensionValue);

						Debug.WriteLine($"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { mappingResult.Name, noteInfo.ListIndex, mappingResult.DimensionValue }}");
					}
				}
			}

			// Apply Scale-Related MIDI Mappings
			{
				int count = _midiMappingCalculator.Results.Count;
				for (int i = 0; i < count; i++)
				{
					MidiMappingCalculatorResult mappingResult = _midiMappingCalculator.Results[i];
					double? frequency = TryGetScaleFrequency(mappingResult);

					if (!frequency.HasValue)
					{
						continue;
					}

					patchCalculator.SetValue(DimensionEnum.Frequency, noteInfo.ListIndex, frequency.Value);

					Debug.WriteLine($"{nameof(patchCalculator)}.{nameof(patchCalculator.SetValue)}({new { DimensionEnum = DimensionEnum.Frequency, noteInfo.ListIndex, frequency }}");
				}
			}
		}

		private double? TryGetScaleFrequency(MidiMappingCalculatorResult mappingResult)
		{
			if (mappingResult.DimensionEnum != DimensionEnum.NoteNumber) return null;

			double value = mappingResult.DimensionValue;

			if (value <= 0) value = 0;
			if (value > _frequencies.Length - 1) value = _frequencies.Length - 1;

			int index = (int)value;

			double frequency = _frequencies[index];

			return frequency;
		}
	}
}