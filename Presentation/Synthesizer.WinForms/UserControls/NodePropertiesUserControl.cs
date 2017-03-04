using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class NodePropertiesUserControl : PropertiesUserControlBase
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
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Node);
            labelX.Text = ResourceFormatter.X;
            labelY.Text = ResourceFormatter.Y;
            labelNodeType.Text = ResourceFormatter.Type;
        }

        // Binding

        public new NodePropertiesViewModel ViewModel
        {
            get { return (NodePropertiesViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

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
}
