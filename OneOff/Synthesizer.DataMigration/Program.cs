using System;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Configuration;

namespace JJ.OneOff.Synthesizer.DataMigration
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

            var config = CustomConfigurationManager.GetSection<JJ.Business.Synthesizer.Configuration.ConfigurationSection>();
            ConfigurationHelper.SetSection(config);

            Application.Run(new MainForm());
        }
    }
}
