using System;
using System.Threading;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal static class Program
    {
        private static AudioOutputProcessor _audioOutputProcessor;
        private static MidiInputProcessor _midiInputProcessor;
        public static IPatchCalculatorContainer PatchCalculatorContainer { get; private set; }

        private static Thread _audioOutputThread;
        private static Thread _midiInputThread;

        [STAThread]
        static void Main()
        {
            var businessConfig = CustomConfigurationManager.GetSection<JJ.Business.Synthesizer.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(businessConfig);

            var presenterConfig = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.Helpers.ConfigurationSection>();
            ConfigurationHelper.SetSection(presenterConfig);

            var vectorGraphicsConfig = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.VectorGraphics.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(vectorGraphicsConfig);

            var winFormsConfig = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection>();
            CultureHelper.SetThreadCultureName(winFormsConfig.DefaultCulture);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var noteRecycler = new NoteRecycler(winFormsConfig.MaxConcurrentNotes);

            if (winFormsConfig.MultiThreaded)
            {
                PatchCalculatorContainer = new MultiThreadedPatchCalculatorContainer(noteRecycler, winFormsConfig.MaxThreads);
            }
            else
            {
                PatchCalculatorContainer = new SingleThreadedPatchCalculatorContainer();
            }

            _audioOutputProcessor = new AudioOutputProcessor(PatchCalculatorContainer);
            _midiInputProcessor = new MidiInputProcessor(PatchCalculatorContainer, _audioOutputProcessor, noteRecycler);

            _audioOutputThread = StartAudioOutputThread(_audioOutputProcessor);
            _midiInputThread = StartMidiInputThread(_midiInputProcessor);

            var form = new MainForm();
            Application.Run(form);

            _audioOutputProcessor.Stop();
            _midiInputProcessor.Stop();

            // TODO: Low priority: nothing really tells you that a could be disposable.
            IDisposable disposablePatchCalculator = PatchCalculatorContainer.Calculator as IDisposable;
            if (disposablePatchCalculator != null)
            {
                disposablePatchCalculator.Dispose();
            }

            // TODO: Clean-up threads all over the place.
            //_patchCalculatorContainer.Dispose();
        }

        private static Thread StartMidiInputThread(MidiInputProcessor midiInputProcessor)
        {
            if (midiInputProcessor == null) throw new NullException(() => midiInputProcessor);

            Thread thread = new Thread(() => midiInputProcessor.TryStart());
            thread.Start();

            return thread;
        }

        public static Thread StartAudioOutputThread(AudioOutputProcessor audioOutputProcessor)
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

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message;
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                message = ExceptionHelper.FormatException(ex, false);
            }
            else
            {
                message = Convert.ToString(e.ExceptionObject);
            }
            
            MessageBox.Show(message, GetMessageBoxCaption());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
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
