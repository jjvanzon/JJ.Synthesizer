using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Presentation.Synthesizer.NAudio
{
	public class InfrastructureFacade
	{
		public event EventHandler<EventArgs<(int midiNoteNumber, int midiVelocity, int midiChannel)>> MidiNoteOnOccurred
		{
			add => _midiInputProcessor.MidiNoteOnOccurred += value;
			remove => _midiInputProcessor.MidiNoteOnOccurred -= value;
		}

		public event EventHandler<EventArgs<(int midiControllerCode, int midiControllerValue, int midiChannel)>> MidiControllerValueChanged
		{
			add => _midiInputProcessor.MidiControllerValueChanged += value;
			remove => _midiInputProcessor.MidiControllerValueChanged -= value;
		}

		/// <summary> Position is left out, because there is still ambiguity between NoteIndex and ListIndex in the system. </summary>
		public event EventHandler<EventArgs<IList<(DimensionEnum dimensionEnum, string name, double value)>>> MidiDimensionValuesChanged
		{
			add => _midiInputProcessor.DimensionValuesChanged += value;
			remove => _midiInputProcessor.DimensionValuesChanged -= value;
		}

		public event EventHandler<EventArgs<Exception>> ExceptionOnMidiThreadOcurred
		{
			add => _midiInputProcessor.ExceptionOnMidiThreadOcurred += value;
			remove => _midiInputProcessor.ExceptionOnMidiThreadOcurred -= value;
		}

		private static readonly bool _midiInputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().MidiInputEnabled;
		private static readonly bool _audioOutputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().AudioOutputEnabled;

		private readonly TimeProvider _timeProvider;
		private readonly NoteRecycler _noteRecycler;
		private readonly AudioOutputProcessor _audioOutputProcessor;
		private readonly MidiInputProcessor _midiInputProcessor;
		private readonly IPatchCalculatorContainer _patchCalculatorContainer;

		private AudioOutput _audioOutput;

		public InfrastructureFacade(AudioOutputFacade audioOutputFacade, SystemFacade systemFacade, RepositoryWrapper repositories)
		{
			if (audioOutputFacade == null) throw new ArgumentNullException(nameof(audioOutputFacade));
			if (systemFacade == null) throw new ArgumentNullException(nameof(systemFacade));
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			_timeProvider = new TimeProvider();

			if (_audioOutputEnabled)
			{
				_audioOutput = audioOutputFacade.CreateWithDefaults();
				_noteRecycler = new NoteRecycler(_audioOutput.MaxConcurrentNotes);
			}

			bool mustCreateEmptyPatchCalculatorContainer = !_audioOutputEnabled;
			if (mustCreateEmptyPatchCalculatorContainer)
			{
				_patchCalculatorContainer = new EmptyPatchCalculatorContainer();
			}
			else
			{
				_patchCalculatorContainer = new MultiThreadedPatchCalculatorContainer(_noteRecycler, repositories);
			}

			if (_audioOutputEnabled)
			{
				_audioOutputProcessor = new AudioOutputProcessor(
					_patchCalculatorContainer,
					_timeProvider,
					_audioOutput.SamplingRate,
					_audioOutput.GetChannelCount(),
					_audioOutput.DesiredBufferDuration);

				_audioOutputProcessor.StartThread();
			}

			// ReSharper disable once InvertIf
			if (_midiInputEnabled)
			{
				_midiInputProcessor = new MidiInputProcessor(
					systemFacade.GetDefaultScale(),
					systemFacade.GetDefaultMidiMappings(),
					_patchCalculatorContainer,
					_timeProvider,
					_noteRecycler);

				_midiInputProcessor.TryStartThread();
			}
		}

		public void Dispose()
		{
			_audioOutputProcessor?.Stop();
			_midiInputProcessor?.Dispose();
		}

		public void UpdateInfrastructure(AudioOutput audioOutput, Patch patch, Scale scale, IList<MidiMapping> midiMappings)
		{
			_audioOutput = audioOutput ?? throw new NullException(() => audioOutput);

			int samplingRate = _audioOutput.SamplingRate;
			int channelCount = _audioOutput.GetChannelCount();
			int maxConcurrentNotes = _audioOutput.MaxConcurrentNotes;
			double desiredBufferDuration = audioOutput.DesiredBufferDuration;

			_audioOutputProcessor?.Stop();
			_midiInputProcessor?.Stop();

			_noteRecycler.SetMaxConcurrentNotes(maxConcurrentNotes);
			_patchCalculatorContainer.RecreateCalculator(patch, samplingRate, channelCount, maxConcurrentNotes);

			if (_audioOutputProcessor != null)
			{
				_audioOutputProcessor.UpdateAudioProperties(samplingRate, channelCount, desiredBufferDuration);
				_audioOutputProcessor.StartThread();
			}

			// ReSharper disable once InvertIf
			if (_midiInputProcessor != null)
			{
				_midiInputProcessor.UpdateScaleAndMidiMappings(scale, midiMappings);
				_midiInputProcessor.TryStartThread();
			}
		}

		public void RecreatePatchCalculator(Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			_patchCalculatorContainer.RecreateCalculator(
				patch,
				_audioOutput.SamplingRate,
				_audioOutput.GetChannelCount(),
				_audioOutput.MaxConcurrentNotes);
		}
	}
}