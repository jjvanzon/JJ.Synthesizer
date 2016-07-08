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
    internal partial class OperatorPropertiesUserControl_ForCurve
        : OperatorPropertiesUserControl_ForCurve_NotDesignable
    {
        public OperatorPropertiesUserControl_ForCurve()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelCurve.Text = PropertyDisplayNames.Curve;
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelOperatorTypeValue.Text = PropertyDisplayNames.Curve;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelCurve, comboBoxCurve);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;

            if (ViewModel.Curve != null)
            {
                comboBoxCurve.SelectedValue = ViewModel.Curve.ID;
            }
            else
            {
                comboBoxCurve.SelectedValue = 0;
            }

            // Dimension
            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }
            if (ViewModel.Dimension != null)
            {
                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
            }
            else
            {
                comboBoxDimension.SelectedValue = 0;
            }
        }

        public void SetCurveLookup(IList<IDAndName> curveLookup)
        {
            // Always refill the lookup, so changes to the curve collection are reflected.
            int? selectedID = TryGetSelectedCurveID();
            comboBoxCurve.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxCurve.ValueMember = PropertyNames.ID;
            comboBoxCurve.DisplayMember = PropertyNames.Name;
            comboBoxCurve.DataSource = curveLookup;
            if (selectedID != null)
            {
                comboBoxCurve.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedCurveID()
        {
            if (comboBoxCurve.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxCurve.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Curve = (IDAndName)comboBoxCurve.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
    
    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForCurve_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_ForCurve>
    { }
}
