using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using System.Threading;
using System;
using JJ.Business.Synthesizer.Api;
using JJ.Framework.Configuration;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public static class InfrastructureFacade
    {
        private static readonly bool _midiInputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().MidiInputEnabled;
        private static readonly bool _audioOutputEnabled = CustomConfigurationManager.GetSection<ConfigurationSection>().AudioOutputEnabled;

        private static NoteRecycler _noteRecycler;
        private static AudioOutputProcessor _audioOutputProcessor;
        private static MidiInputProcessor _midiInputProcessor;

        public static IPatchCalculatorContainer PatchCalculatorContainer { get; private set; }
        public static AudioOutput AudioOutput { get; private set; }

        // ReSharper disable once NotAccessedField.Local
        private static Thread _audioOutputThread;

        // ReSharper disable once NotAccessedField.Local
        private static Thread _midiInputThread;

        public static void Initialize()
        {
            AudioOutput audioOutput = AudioOutputApi.Create();
            SetAudioOutput(audioOutput);
        }

        public static void Dispose()
        {
            if (_audioOutputProcessor != null) _audioOutputProcessor.Stop();
            if (_midiInputProcessor != null) _midiInputProcessor.Stop();
        }

        private static void SetAudioOutput(AudioOutput audioOutput)
        {
            AudioOutput = audioOutput ?? throw new NullException(() => audioOutput);

            _noteRecycler = new NoteRecycler(audioOutput.MaxConcurrentNotes);

            bool mustCreateEmptyPatchCalculatorContainer = !_audioOutputEnabled;
            if (mustCreateEmptyPatchCalculatorContainer)
            {
                PatchCalculatorContainer = new EmptyPatchCalculatorContainer();
            }
            else
            {
                PatchCalculatorContainer = new MultiThreadedPatchCalculatorContainer();
            }

            if (_audioOutputEnabled) _audioOutputProcessor = new AudioOutputProcessor(PatchCalculatorContainer, audioOutput.SamplingRate, audioOutput.GetChannelCount(), audioOutput.DesiredBufferDuration);
            if (_midiInputEnabled) _midiInputProcessor = new MidiInputProcessor(PatchCalculatorContainer, _noteRecycler);

            if (_audioOutputEnabled) _audioOutputThread = StartAudioOutputThread(_audioOutputProcessor);
            if (_midiInputEnabled) _midiInputThread = StartMidiInputThread(_midiInputProcessor);
        }

        public static void UpdateInfrastructure(AudioOutput audioOutput, Patch patch, PatchRepositories patchRepositories)
        {
            if (_audioOutputProcessor != null) _audioOutputProcessor.Stop();
            if (_midiInputProcessor != null) _midiInputProcessor.Stop();

            AudioOutput = audioOutput ?? throw new NullException(() => audioOutput);

            _noteRecycler.SetMaxConcurrentNotes(audioOutput.MaxConcurrentNotes);

            PatchCalculatorContainer.RecreateCalculator(
                patch,
                audioOutput.SamplingRate,
                audioOutput.GetChannelCount(),
                audioOutput.MaxConcurrentNotes,
                _noteRecycler,
                patchRepositories);

            if (_audioOutputEnabled) _audioOutputProcessor = new AudioOutputProcessor(PatchCalculatorContainer, audioOutput.SamplingRate, audioOutput.GetChannelCount(), audioOutput.DesiredBufferDuration);
            if (_midiInputEnabled) _midiInputProcessor = new MidiInputProcessor(PatchCalculatorContainer, _noteRecycler);

            if (_audioOutputEnabled) _audioOutputThread = StartAudioOutputThread(_audioOutputProcessor);
            if (_midiInputEnabled) _midiInputThread = StartMidiInputThread(_midiInputProcessor);
        }

        public static void RecreatePatchCalculator(Patch patch, PatchRepositories patchRepositories)
        {
            if (patch == null) throw new NullException(() => patch);
            if (patchRepositories == null) throw new NullException(() => patchRepositories);

            PatchCalculatorContainer.RecreateCalculator(
                patch,
                AudioOutput.SamplingRate,
                AudioOutput.GetChannelCount(),
                AudioOutput.MaxConcurrentNotes,
                _noteRecycler,
                patchRepositories);
        }

        private static Thread StartMidiInputThread(MidiInputProcessor midiInputProcessor)
        {
            var thread = new Thread(() => midiInputProcessor.TryStart());
            thread.Start();

            return thread;
        }

        private static Thread StartAudioOutputThread(AudioOutputProcessor audioOutputProcessor)
        {
            var thread = new Thread(() => audioOutputProcessor.Start());
            thread.Start();

            // Starting AudioOutputProcessor on another thread seems to 
            // start and keep alive a new Windows message loop,
            // but that does not mean that the thread keeps running.

            return thread;
        }
    }
}
