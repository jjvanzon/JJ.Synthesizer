﻿using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Svg.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class AudioFileOutputPropertiesForm : Form
    {
        public event EventHandler CloseRequested;

        public AudioFileOutputPropertiesForm()
        {
            InitializeComponent();
        }

        public AudioFileOutputPropertiesViewModel ViewModel 
        {
            get { return audioFileOutputPropertiesUserControl1.ViewModel; }
            set { audioFileOutputPropertiesUserControl1.ViewModel = value; }
        }

        private void AudioFileOutputPropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }
    }
}
