using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputPropertiesUserControl : UserControl
    {
        private AudioFileOutputPropertiesViewModel _viewModel;

        public AudioFileOutputPropertiesUserControl()
        {
            InitializeComponent();
            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AudioFileOutputPropertiesViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            // TODO: Program this.
        }

        private void ApplyViewModel()
        {
            // TODO: Program this.
        }
    }
}
