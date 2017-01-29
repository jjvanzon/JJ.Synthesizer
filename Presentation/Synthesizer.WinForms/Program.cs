using System;
using System.Threading;
using System.Windows.Forms;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Api;

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

            // TODO: Make a framework class to handle this.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AudioOutput audioOutput = AudioOutputApi.Create();
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

            bool mustCreateEmptuPatchCalculatorContainer = !winFormsConfig.AudioOutputEnabled;
            if (mustCreateEmptuPatchCalculatorContainer)
            {
                PatchCalculatorContainer = new EmptyPatchCalculatorContainer();
            }
            else if (winFormsConfig.MultiThreaded)
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

            Thread thread = new Thread(() => audioOutputProcessor.Start());
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
            return string.Format("{0} - Exception", Titles.ApplicationName);
        }
    }
}
