using System;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
	public class InfrastructureFacade
	{
		private static readonly bool _midiInputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().MidiInputEnabled;
		private static readonly bool _audioOutputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().AudioOutputEnabled;

		private readonly TimeProvider _timeProvider;
		private readonly NoteRecycler _noteRecycler;
		private readonly AudioOutputProcessor _audioOutputProcessor;
		private readonly IPatchCalculatorContainer _patchCalculatorContainer;

		private AudioOutput _audioOutput;

		// ReSharper disable once NotAccessedField.Local
		private Thread _audioOutputThread;

		// ReSharper disable once NotAccessedField.Local
		private Thread _midiInputThread;
		private readonly IDocumentRepository _documentRepository;

		public InfrastructureFacade(RepositoryWrapper repositories)
		{
			if (repositories == null) throw new ArgumentNullException(nameof(repositories));

			_documentRepository = repositories.DocumentRepository;

			var audioOutputFacade = new AudioOutputFacade(repositories.AudioOutputRepository, repositories.SpeakerSetupRepository, repositories.IDRepository);
			_audioOutput = audioOutputFacade.CreateWithDefaults();
			_timeProvider = new TimeProvider();
			_noteRecycler = new NoteRecycler(_audioOutput.MaxConcurrentNotes);

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

				_audioOutputThread = _audioOutputProcessor.StartThread();
			}

			// ReSharper disable once InvertIf
			if (_midiInputEnabled)
			{
				MidiInputProcessor.Stop();
				MidiInputProcessor.Initialize(_patchCalculatorContainer, _timeProvider, _noteRecycler, _documentRepository);
				_midiInputThread = MidiInputProcessor.StartThread();
			}
		}

		public void Dispose()
		{
			_audioOutputProcessor?.Stop();
			MidiInputProcessor.Stop();
		}

		public void UpdateInfrastructure(AudioOutput audioOutput, Patch patch)
		{
			_audioOutput = audioOutput ?? throw new NullException(() => audioOutput);

			int samplingRate = _audioOutput.SamplingRate;
			int channelCount = _audioOutput.GetChannelCount();
			int maxConcurrentNotes = _audioOutput.MaxConcurrentNotes;
			double desiredBufferDuration = audioOutput.DesiredBufferDuration;

			_audioOutputProcessor?.Stop();
			MidiInputProcessor.Stop();

			_noteRecycler.SetMaxConcurrentNotes(maxConcurrentNotes);
			_patchCalculatorContainer.RecreateCalculator(patch, samplingRate, channelCount, maxConcurrentNotes);

			if (_audioOutputProcessor != null)
			{
				_audioOutputProcessor.UpdateAudioProperties(samplingRate, channelCount, desiredBufferDuration);
				_audioOutputThread = _audioOutputProcessor.StartThread();
			}

			// ReSharper disable once InvertIf
			MidiInputProcessor.Initialize(_patchCalculatorContainer, _timeProvider, _noteRecycler, _documentRepository);
			_midiInputThread = MidiInputProcessor.StartThread();
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
