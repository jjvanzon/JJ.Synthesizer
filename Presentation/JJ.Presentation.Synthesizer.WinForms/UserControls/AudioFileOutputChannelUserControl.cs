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
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputChannelUserControl : UserControl
    {
        public AudioFileOutputChannelUserControl()
        {
            InitializeComponent();

            this.AutomaticallyAssignTabIndexes();
        }

        /// <summary> virtually not nullable </summary>
        private AudioFileOutputChannelViewModel _viewModel;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AudioFileOutputChannelViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        /// <summary> virtually not nullable </summary>
        private IList<IDAndName> _outletLookup;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<IDAndName> OutletLookup
        {
            get { return _outletLookup; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _outletLookup = value;
                ApplyOutletLookupToControls();
            }
        }

        // Gui

        private void ApplyOutletLookupToControls()
        {
            if (comboBoxOutlet.DataSource == null)
            {
                comboBoxOutlet.DataSource = _outletLookup;
                comboBoxOutlet.ValueMember = PropertyNames.ID;
                comboBoxOutlet.DisplayMember = PropertyNames.Name;
            }
        }

        private void ApplyViewModelToControls()
        {
            labelName.Text = _viewModel.Name;
            //textBoxName.Text = _viewModel.Document.Name;
            //comboBoxOutlet.SelectedValue = _viewModel.Keys.AudioFileOutputListIndex;
            // Oops, The outlet lookup keys are not ID's either, but surrogate keys for uncomitted objects.
        }

        public void ApplyControlsToViewModel()
        {
            //_viewModel.
            //_viewModel.Document.Name = textBoxName.Text;
            //throw new NotImplementedException();
        }
    }
}
