using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputChannelUserControl : UserControl
    {
        public AudioFileOutputChannelUserControl()
        {
            InitializeComponent();

            this.AutomaticallyAssignTabIndexes();
        }

        private AudioFileOutputChannelViewModel _viewModel;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AudioFileOutputChannelViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        private IList<IDAndName> _outletLookup;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<IDAndName> OutletLookup
        {
            get { return _outletLookup; }
            set
            {
                _outletLookup = value;
                ApplyOutletLookupToControls();
            }
        }

        // Gui

        private void ApplyOutletLookupToControls()
        {
            if (_outletLookup == null) return;

            if (comboBoxOutlet.DataSource == null)
            {
                comboBoxOutlet.ValueMember = PropertyNames.ID;
                comboBoxOutlet.DisplayMember = PropertyNames.Name;
                comboBoxOutlet.DataSource = _outletLookup;
            }
        }

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            labelName.Text = _viewModel.Name;
            //textBoxName.Text = _viewModel.Document.Name;
            //comboBoxOutlet.SelectedValue = _viewModel.Keys.AudioFileOutputListIndex;
            // Oops, The outlet lookup keys are not ID's either, but surrogate keys for uncomitted objects.
        }

        public void ApplyControlsToViewModel()
        {
            //if (_viewModel == null) return;
            //_viewModel.
            //_viewModel.Document.Name = textBoxName.Text;
            //throw new NotImplementedException();
        }
    }
}
