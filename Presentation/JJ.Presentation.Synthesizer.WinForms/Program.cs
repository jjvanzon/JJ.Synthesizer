using System;
using System.Threading;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal static class Program
    {
        private static Thread _audioOutputThread;
        private static Thread _midiInputThread;

        [STAThread]
        static void Main()
        {
            var config1 = CustomConfigurationManager.GetSection<JJ.Business.Synthesizer.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(config1);

            var config2 = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.Helpers.ConfigurationSection>();
            ConfigurationHelper.SetSection(config2);

            var config3 = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.VectorGraphics.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(config3);

            var config4 = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection>();
            CultureHelper.SetThreadCultureName(config4.DefaultCulture);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartAudioOutputThread();
            StartMidiInputThread(config4.MaxConcurrentNotes);

            var form = new MainForm();
            Application.Run(form);

            MidiInputProcessor.Stop();
            AudioOutputProcessor.Stop();
        }

        private static void StartMidiInputThread(int maxCurrentNotes)
        {
            MidiInputProcessor.Stop();

            MidiInputProcessor.MaxConcurrentNotes = maxCurrentNotes;
            _midiInputThread = new Thread(() => MidiInputProcessor.TryStart());
            _midiInputThread.Start();
        }

        public static void StartAudioOutputThread()
        {
            AudioOutputProcessor.Stop();
            
            // Temporarily call another method for debugging (2016-01-09).
            _audioOutputThread = new Thread(() => AudioOutputProcessor.Start());
            //_audioOutputThread = new Thread(() => AudioOutputProcessor.StartAndPause());
            _audioOutputThread.Start();

            // Starting AudioOutputProcessor on another thread seems to start and keep alive a new Windows message loop,
            // but that does not mean that the thread keeps running.
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
