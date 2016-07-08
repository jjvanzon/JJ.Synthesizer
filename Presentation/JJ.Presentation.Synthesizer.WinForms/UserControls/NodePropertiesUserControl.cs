using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class NodePropertiesUserControl : NodePropertiesUserControl_NotDesignable
    {
        public NodePropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelX, numericUpDownX);
            AddProperty(labelY, numericUpDownY);
            AddProperty(labelNodeType, comboBoxNodeType);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Node);
            labelX.Text = PropertyDisplayNames.X;
            labelY.Text = PropertyDisplayNames.Y;
            labelNodeType.Text = Titles.Type;
        }

        // Binding

        protected override int GetID()
        {
            return ViewModel.Entity.ID;
        }

        protected override void ApplyViewModelToControls()
        {
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

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.X = (double)numericUpDownX.Value;
            ViewModel.Entity.Y = (double)numericUpDownY.Value;
            ViewModel.Entity.NodeType = (IDAndName)comboBoxNodeType.SelectedItem;
        }
    }

    /// <summary> 
    /// The WinForms designer does not work when deriving directly from a generic class.
    /// And also not when you make this class abstract.
    /// </summary>
    internal class NodePropertiesUserControl_NotDesignable 
        : PropertiesUserControlBase<NodePropertiesViewModel>
    { }
}
