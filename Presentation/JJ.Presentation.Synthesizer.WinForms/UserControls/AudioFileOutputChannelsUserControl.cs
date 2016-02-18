using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputChannelsUserControl : UserControl
    {
        public AudioFileOutputChannelsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private IList<AudioFileOutputChannelViewModel> _viewModels;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<AudioFileOutputChannelViewModel> ViewModels
        {
            get { return _viewModels; }
            set
            {
                _viewModels = value;
                ApplyViewModelsToControls();
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
                _outletLookup = value;
                OutletLookupToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            groupBox.Text = PropertyDisplayNames.Channels;
        }

        private IList<AudioFileOutputChannelUserControl> _audioFileOutputChannelUserControls = new List<AudioFileOutputChannelUserControl>();

        private void ApplyViewModelsToControls()
        {
            if (_viewModels == null) return;

            // Not finished / not tested / not debugged.

            // TODO: I wonder how the original controls that are replaced is taken care of...

            _audioFileOutputChannelUserControls.Clear();

            tableLayoutPanel.RowCount = _viewModels.Count + 1;

            int i;
            for (i = 0; i < _viewModels.Count; i++)
            {
                AudioFileOutputChannelViewModel viewModel = _viewModels[i];

                tableLayoutPanel.RowStyles[i] = new RowStyle { Height = 24, SizeType = SizeType.Absolute };

                // TODO: Look at designer-generated code of AudioFileOutputPropertiesControl.Designer.cs
                // for an example
                var control = new AudioFileOutputChannelUserControl();
                control.Dock = DockStyle.Fill;
                control.ViewModel = viewModel;
                tableLayoutPanel.SetRow(control, i);

                _audioFileOutputChannelUserControls.Add(control);
            }

            tableLayoutPanel.RowStyles[i] = new RowStyle { SizeType = SizeType.AutoSize };
        }

        private void OutletLookupToControls()
        {
            if (_outletLookup == null) return;

            foreach (AudioFileOutputChannelUserControl audioFileOutputChannelUserControl in _audioFileOutputChannelUserControls)
            {
                audioFileOutputChannelUserControl.OutletLookup = _outletLookup;
            }
        }

        public void ApplyControlsToViewModels()
        {
            foreach (AudioFileOutputChannelUserControl audioFileOutputChannelUserControl in _audioFileOutputChannelUserControls)
            {
                audioFileOutputChannelUserControl.ApplyControlsToViewModel();
            }
        }
    }
}
