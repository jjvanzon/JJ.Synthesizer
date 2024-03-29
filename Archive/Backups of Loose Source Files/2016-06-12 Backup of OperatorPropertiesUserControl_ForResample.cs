﻿//using System;
//using System.ComponentModel;
//using System.Windows.Forms;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Framework.Presentation.Resources;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Presentation.Synthesizer.WinForms.Helpers;
//using JJ.Framework.Presentation.WinForms.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Canonical;
//using JJ.Presentation.Synthesizer.Resources;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    internal partial class OperatorPropertiesUserControl_ForResample 
//        : OperatorPropertiesUserControl_ForResample_NotDesignable
//    {
//        public event EventHandler CloseRequested;
//        public event EventHandler LoseFocusRequested;

//        public OperatorPropertiesUserControl_ForResample()
//        {
//            InitializeComponent();

//            SetTitles();

//            this.AutomaticallyAssignTabIndexes();
//        }

//        private void OperatorPropertiesUserControl_ForResample_Load(object sender, EventArgs e)
//        {
//            ApplyStyling();
//        }

//        // Gui

//        private void SetTitles()
//        {
//            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

//            labelName.Text = CommonTitles.Name;
//            labelOperatorTypeTitle.Text = Titles.Type + ":";
//            labelOperatorTypeValue.Text = PropertyDisplayNames.Resample;
//            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
//            labelDimension.Text = PropertyDisplayNames.Dimension;
//        }

//        private void ApplyStyling()
//        {
//            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
//        }

//        // Binding

//        protected override void ApplyViewModelToControls()
//        {
//            if (ViewModel == null) return;

//            textBoxName.Text = ViewModel.Name;

//            // Interpolation
//            if (comboBoxInterpolation.DataSource == null)
//            {
//                comboBoxInterpolation.ValueMember = PropertyNames.ID;
//                comboBoxInterpolation.DisplayMember = PropertyNames.Name;
//                comboBoxInterpolation.DataSource = ViewModel.InterpolationLookup;
//            }
//            if (ViewModel.Interpolation != null)
//            {
//                comboBoxInterpolation.SelectedValue = ViewModel.Interpolation.ID;
//            }
//            else
//            {
//                comboBoxInterpolation.SelectedValue = 0;
//            }

//            // Dimension
//            if (comboBoxDimension.DataSource == null)
//            {
//                comboBoxDimension.ValueMember = PropertyNames.ID;
//                comboBoxDimension.DisplayMember = PropertyNames.Name;
//                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
//            }
//            if (ViewModel.Dimension != null)
//            {
//                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
//            }
//            else
//            {
//                comboBoxDimension.SelectedValue = 0;
//            }
//        }

//        private void ApplyControlsToViewModel()
//        {
//            if (ViewModel == null) return;

//            ViewModel.Name = textBoxName.Text;
//            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
//            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
//        }

//        // Actions

//        private void Close()
//        {
//            if (CloseRequested != null)
//            {
//                ApplyControlsToViewModel();
//                CloseRequested(this, EventArgs.Empty);
//            }
//        }

//        private void LoseFocus()
//        {
//            if (LoseFocusRequested != null)
//            {
//                ApplyControlsToViewModel();
//                LoseFocusRequested(this, EventArgs.Empty);
//            }
//        }

//        // Events

//        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
//        {
//            Close();
//        }

//        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
//        private void OperatorPropertiesUserControl_ForResample_Leave(object sender, EventArgs e)
//        {
//            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
//            // making it want to save again, even though view model is empty
//            // which makes it say that now clear fields are required.
//            if (Visible) 
//            {
//                LoseFocus();
//            }
//        }
//    }

//    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
//    internal class OperatorPropertiesUserControl_ForResample_NotDesignable
//        : UserControlBase<OperatorPropertiesViewModel_ForResample>
//    {
//        protected override void ApplyViewModelToControls()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
