using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using System.Threading;
using JJ.Business.Synthesizer.Api;
using JJ.Framework.Configuration;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class InfrastructureFacade
    {
        private static readonly bool _midiInputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().MidiInputEnabled;
        private static readonly bool _audioOutputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().AudioOutputEnabled;

        private readonly TimeProvider _timeProvider;
        private readonly NoteRecycler _noteRecycler;
        private readonly AudioOutputProcessor _audioOutputProcessor;
        private readonly MidiInputProcessor _midiInputProcessor;
        private readonly IPatchCalculatorContainer _patchCalculatorContainer;

        private AudioOutput _audioOutput;

        // ReSharper disable once NotAccessedField.Local
        private Thread _audioOutputThread;

        // ReSharper disable once NotAccessedField.Local
        private Thread _midiInputThread;

        public InfrastructureFacade(PatchRepositories patchRepositories)
        {
            _audioOutput = AudioOutputApi.Create();
            _timeProvider = new TimeProvider();
            _noteRecycler = new NoteRecycler(_audioOutput.MaxConcurrentNotes);

            bool mustCreateEmptyPatchCalculatorContainer = !_audioOutputEnabled;
            if (mustCreateEmptyPatchCalculatorContainer)
            {
                _patchCalculatorContainer = new EmptyPatchCalculatorContainer();
            }
            else
            {
                _patchCalculatorContainer = new MultiThreadedPatchCalculatorContainer(_noteRecycler, patchRepositories);
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
                _midiInputProcessor = new MidiInputProcessor(_patchCalculatorContainer, _timeProvider, _noteRecycler);
                _midiInputThread = _midiInputProcessor.StartThread();
            }
        }

        public void Dispose()
        {
            _audioOutputProcessor?.Stop();
            _midiInputProcessor?.Stop();
        }

        public void UpdateInfrastructure(AudioOutput audioOutput, Patch patch)
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
                _audioOutputThread = _audioOutputProcessor.StartThread();
            }

            // ReSharper disable once InvertIf
            if (_midiInputProcessor != null)
            {
                _midiInputThread = _midiInputProcessor.StartThread();
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
