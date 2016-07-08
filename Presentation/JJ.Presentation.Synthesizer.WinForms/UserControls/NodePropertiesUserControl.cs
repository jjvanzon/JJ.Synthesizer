using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class NodePropertiesUserControl : NodePropertiesUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

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

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Node);
            labelX.Text = PropertyDisplayNames.X;
            labelY.Text = PropertyDisplayNames.Y;
            labelNodeType.Text = Titles.Type;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            numericUpDownX.Value = (decimal)ViewModel.Entity.X;
            numericUpDownY.Value = (decimal)ViewModel.Entity.Y;

            if (comboBoxNodeType.DataSource == null)
            {
                comboBoxNodeType.ValueMember = PropertyNames.ID;
                comboBoxNodeType.DisplayMember = PropertyNames.Name;
                comboBoxNodeType.DataSource = ViewModel.NodeTypeLookup;
            }

            if (ViewModel.Entity.NodeType != null)
            {
                comboBoxNodeType.SelectedValue = ViewModel.Entity.NodeType.ID;
            }
            else
            {
                comboBoxNodeType.SelectedValue = 0;
            }
        }

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Entity.X = (double)numericUpDownX.Value;
            ViewModel.Entity.Y = (double)numericUpDownY.Value;
            ViewModel.Entity.NodeType = (IDAndName)comboBoxNodeType.SelectedItem;
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

    /// <summary> 
    /// The WinForms designer does not work when deriving directly from a generic class.
    /// And also not when you make this class abstract.
    /// </summary>
    internal class NodePropertiesUserControl_NotDesignable : UserControlBase<NodePropertiesViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
