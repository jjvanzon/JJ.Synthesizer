using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal static class Program
    {
        private static AudioOutputProcessor _audioOutputProcessor;
        private static MidiInputProcessor _midiInputProcessor;

        public static IPatchCalculatorContainer PatchCalculatorContainer { get; private set; }
        public static AudioOutput AudioOutput { get; private set; }

        private static Thread _audioOutputThread;
        private static Thread _midiInputThread;

        [STAThread]
        static void Main()
        {
            var businessConfig = CustomConfigurationManager.GetSection<JJ.Business.Synthesizer.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(businessConfig);

            var vectorGraphicsConfig = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.VectorGraphics.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(vectorGraphicsConfig);

            var winFormsConfig = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection>();
            CultureHelper.SetThreadCultureName(winFormsConfig.DefaultCulture);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AudioOutput audioOutput = CreateMockAudioOutput_Stereo();

            SetAudioOutput(audioOutput);

            var form = new MainForm();
            Application.Run(form);

            DisposeAudioOutput();
        }
        
        public static void SetAudioOutput(AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            if (AudioOutput != null)
            {
                DisposeAudioOutput();
            }

            AudioOutput = audioOutput;

            var noteRecycler = new NoteRecycler(audioOutput.MaxConcurrentNotes);

            var winFormsConfig = CustomConfigurationManager.GetSection<Configuration.ConfigurationSection>();

            if (winFormsConfig.MultiThreaded)
            {
                PatchCalculatorContainer = new MultiThreadedPatchCalculatorContainer(noteRecycler);
            }
            else
            {
                PatchCalculatorContainer = new SingleThreadedPatchCalculatorContainer();
            }

            if (winFormsConfig.AudioOutputEnabled) _audioOutputProcessor = new AudioOutputProcessor(PatchCalculatorContainer, audioOutput);
            if (winFormsConfig.MidiInputEnabled) _midiInputProcessor = new MidiInputProcessor(PatchCalculatorContainer, _audioOutputProcessor, noteRecycler);

            if (winFormsConfig.AudioOutputEnabled) _audioOutputThread = StartAudioOutputThread(_audioOutputProcessor);
            if (winFormsConfig.MidiInputEnabled) _midiInputThread = StartMidiInputThread(_midiInputProcessor);
        }

        private static void DisposeAudioOutput()
        {
            var winFormsConfig = CustomConfigurationManager.GetSection<Configuration.ConfigurationSection>();

            if (winFormsConfig.AudioOutputEnabled) _audioOutputProcessor.Stop();
            if (winFormsConfig.MidiInputEnabled) _midiInputProcessor.Stop();

            IDisposable disposablePatchCalculator = PatchCalculatorContainer.Calculator as IDisposable;
            if (disposablePatchCalculator != null)
            {
                disposablePatchCalculator.Dispose();
            }
        }

        /// <summary>
        /// This mock AudioOutput entity is needed to keep the code runnable,
        /// until I create the audio output infrastructure objects elsewhere in the code.
        /// </summary>
        private static AudioOutput CreateMockAudioOutput_Mono()
        {
            var speakerSetup = new SpeakerSetup
            {
                ID = (int)SpeakerSetupEnum.Mono,
                SpeakerSetupChannels = new List<SpeakerSetupChannel>()
            };

            var speakerSetupChannel = new SpeakerSetupChannel
            {
                IndexNumber = 0,
                SpeakerSetup = speakerSetup
            };
            speakerSetup.SpeakerSetupChannels.Add(speakerSetupChannel);

            var audioOutput = new AudioOutput
            {
                SamplingRate = 44100,
                SpeakerSetup = speakerSetup,
                MaxConcurrentNotes = 16,
                DesiredBufferDuration = 0.1
            };

            return audioOutput;
        }

        /// <summary>
        /// This mock AudioOutput entity is needed to keep the code runnable,
        /// until I create the audio output infrastructure objects elsewhere in the code.
        /// </summary>
        private static AudioOutput CreateMockAudioOutput_Stereo()
        {
            var speakerSetup = new SpeakerSetup
            {
                ID = (int)SpeakerSetupEnum.Stereo,
                SpeakerSetupChannels = new List<SpeakerSetupChannel>()
            };

            var speakerSetupChannel1 = new SpeakerSetupChannel
            {
                IndexNumber = 0,
                SpeakerSetup = speakerSetup
            };
            speakerSetup.SpeakerSetupChannels.Add(speakerSetupChannel1);

            var speakerSetupChannel2 = new SpeakerSetupChannel
            {
                IndexNumber = 1,
                SpeakerSetup = speakerSetup
            };
            speakerSetup.SpeakerSetupChannels.Add(speakerSetupChannel2);

            var audioOutput = new AudioOutput
            {
                SamplingRate = 44100,
                SpeakerSetup = speakerSetup,
                MaxConcurrentNotes = 16,
                DesiredBufferDuration = 0.1
            };

            return audioOutput;
        }

        private static Thread StartMidiInputThread(MidiInputProcessor midiInputProcessor)
        {
            if (midiInputProcessor == null) throw new NullException(() => midiInputProcessor);

            Thread thread = new Thread(() => midiInputProcessor.TryStart());
            thread.Start();

            return thread;
        }

        private static Thread StartAudioOutputThread(AudioOutputProcessor audioOutputProcessor)
        {
            if (audioOutputProcessor == null) throw new NullException(() => audioOutputProcessor);

            // Temporarily call another method for debugging (2016-01-09).
            Thread thread = new Thread(() => audioOutputProcessor.Start());
            //_audioOutputThread = new Thread(() => AudioOutputProcessor.StartAndPause());
            thread.Start();

            // Starting AudioOutputProcessor on another thread seems to start and keep alive a new Windows message loop,
            // but that does not mean that the thread keeps running.

            return thread;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message;
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                message = ExceptionHelper.FormatException(ExceptionHelper.GetInnermostException(ex), false);
            }
            else
            {
                message = Convert.ToString(e.ExceptionObject);
            }
            
            MessageBox.Show(message, GetMessageBoxCaption());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = ExceptionHelper.GetInnermostException(e.Exception);
            MessageBox.Show(ExceptionHelper.FormatException(ex, false), GetMessageBoxCaption());
        }

        private static string GetMessageBoxCaption()
        {
            return String.Format("{0} - Exception", Titles.ApplicationName);
        }
    }
}
