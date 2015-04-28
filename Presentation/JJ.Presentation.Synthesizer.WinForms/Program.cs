using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Logging;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = CustomConfigurationManager.GetSection<JJ.Presentation.Synthesizer.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(config);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //var form = new PatchDetailsForm();
            //var form = new PatchListForm();
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

            MessageBox.Show(message);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(ExceptionHelper.FormatException(e.Exception, false));
        }
    }
}
