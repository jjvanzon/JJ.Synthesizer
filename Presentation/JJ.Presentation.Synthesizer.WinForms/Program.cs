using System;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal static class Program
    {
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

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new MainForm();

            Application.Run(form);
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
