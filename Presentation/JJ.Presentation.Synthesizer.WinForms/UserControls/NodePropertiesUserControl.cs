using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class NodePropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private NodePropertiesViewModel _viewModel;

        public NodePropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void NodePropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NodePropertiesViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Node);
            labelValue.Text = PropertyDisplayNames.Value;
            labelTime.Text = PropertyDisplayNames.Time;
            labelNodeType.Text = Titles.Type;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            numericUpDownValue.Value = (decimal)_viewModel.Entity.Value;
            numericUpDownTime.Value = (decimal)_viewModel.Entity.Time;

            if (comboBoxNodeType.DataSource == null)
            {
                comboBoxNodeType.ValueMember = PropertyNames.ID;
                comboBoxNodeType.DisplayMember = PropertyNames.Name;
                comboBoxNodeType.DataSource = _viewModel.NodeTypeLookup;
            }

            if (_viewModel.Entity.NodeType != null)
            {
                comboBoxNodeType.SelectedValue = _viewModel.Entity.NodeType.ID;
            }
            else
            {
                comboBoxNodeType.SelectedValue = 0;
            }
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Entity.Time = (double)numericUpDownTime.Value;
            _viewModel.Entity.Value = (double)numericUpDownValue.Value;
            _viewModel.Entity.NodeType = (IDAndName)comboBoxNodeType.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                ApplyControlsToViewModel();
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                ApplyControlsToViewModel();
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void NodePropertiesUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible) 
            {
                LoseFocus();
            }
        }
    }
}
