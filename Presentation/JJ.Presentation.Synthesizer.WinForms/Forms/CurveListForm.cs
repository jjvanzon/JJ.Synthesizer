using JJ.Business.Synthesizer.Resources;
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
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class CurveListForm : Form
    {
        public event EventHandler CloseRequested;

        public CurveListForm()
        {
            InitializeComponent();
        }

        public event EventHandler<PageEventArgs> ShowRequested
        {
            add { curveListUserControl1.ShowRequested += value; }
            remove { curveListUserControl1.ShowRequested -= value; }
        }

        public CurveListViewModel ViewModel
        {
            get { return curveListUserControl1.ViewModel; }
            set { curveListUserControl1.ViewModel = value; }
        }

        private void CurveListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }
    }
}
