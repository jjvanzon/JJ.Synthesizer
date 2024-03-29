﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Common;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.VectorGraphics.Drawing;
using JJ.Framework.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.Configuration;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal static class Program
    {
        private class ParsedCommandLineArguments
        {
            public string DocumentName { get; set; }
            public string PatchName { get; set; }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UnhandledExceptionMessageBoxShower.Initialize(ResourceFormatter.ApplicationName);
            ParsedCommandLineArguments parsedCommandLineArguments = ParseCommandLineArguments(args);
#if DEBUG
            DrawerBase.MustDrawCoordinateIndicatorsForPrimitives = CustomConfigurationManager.GetSection<ConfigurationSection>()
                                                                                             .DrawCoordinateIndicatorsForPrimitives;
            DrawerBase.MustDrawCoordinateIndicatorsForComposites = CustomConfigurationManager.GetSection<ConfigurationSection>()
                                                                                             .DrawCoordinateIndicatorsForComposites;
#endif
            string cultureNameFromConfig = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultCulture;
            if (!string.IsNullOrWhiteSpace(cultureNameFromConfig))
            {
                CultureHelper.SetCurrentCultureName(cultureNameFromConfig);
            }

            MainForm form = ShowMainWindow(parsedCommandLineArguments.DocumentName, parsedCommandLineArguments.PatchName);
            Application.Run(form);
        }

        private static readonly Dictionary<string, MainForm> _documentNameToMainWindowDictionary = new Dictionary<string, MainForm>();
        private static readonly object _documentNameToMainWindowDictionaryLock = new object();

        public static MainForm ShowMainWindow(string documentName, string patchName)
        {
            if (string.IsNullOrEmpty(documentName))
            {
                var mainForm = new MainForm();
                mainForm.Show(null, null);
                return mainForm;
            }

            lock (_documentNameToMainWindowDictionaryLock)
            {
                if (!_documentNameToMainWindowDictionary.TryGetValue(documentName, out MainForm mainForm))
                {
                    mainForm = new MainForm();
                    mainForm.Show(documentName, patchName);
                    _documentNameToMainWindowDictionary.Add(documentName, mainForm);
                }
                else
                {
                    if (mainForm.WindowState == FormWindowState.Minimized)
                    {
                        mainForm.WindowState = FormWindowState.Normal;
                    }
                    mainForm.BringToFront();

                    if (!string.IsNullOrEmpty(patchName))
                    {
                        mainForm.PatchShow(patchName);
                    }
                }

                return mainForm;
            }
        }

        public static void RemoveMainWindow(Form form)
        {
            lock (_documentNameToMainWindowDictionaryLock)
            {
                string key = _documentNameToMainWindowDictionary.Where(x => x.Value == form).Select(x => x.Key).SingleOrDefault();
                if (!string.IsNullOrEmpty(key))
                {
                    _documentNameToMainWindowDictionary.Remove(key);
                }
            }
        }

        private static ParsedCommandLineArguments ParseCommandLineArguments(IList<string> args)
        {
            var parsedCommandLineArguments = new ParsedCommandLineArguments();

            if (args == null)
            {
                return parsedCommandLineArguments;
            }

            switch (args.Count)
            {
                case 0:
                    return parsedCommandLineArguments;

                case 1:
                    parsedCommandLineArguments.DocumentName = args[0];
                    return parsedCommandLineArguments;

                case 2:
                    parsedCommandLineArguments.DocumentName = args[0];
                    parsedCommandLineArguments.PatchName = args[1];
                    return parsedCommandLineArguments;


                default:
                    throw new GreaterThanException(() => args.Count, 2);
            }
        }
    }
}