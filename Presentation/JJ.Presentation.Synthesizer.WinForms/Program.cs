using JJ.Framework.Logging;
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

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var form = new PatchEditForm();

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
