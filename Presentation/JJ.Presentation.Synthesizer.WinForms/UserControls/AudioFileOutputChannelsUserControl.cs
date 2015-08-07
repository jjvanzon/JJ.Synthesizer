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
    internal partial class AudioFileOutputChannelsUserControl : UserControl
    {
        public AudioFileOutputChannelsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        /// <summary> virtually not nullable </summary>
        private IList<AudioFileOutputChannelViewModel> _viewModels;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<AudioFileOutputChannelViewModel> ViewModels
        {
            get { return _viewModels; }
            set
            {
                if (value == null) throw new NullException(() => value);
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
                if (value == null) throw new NullException(() => value);
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
