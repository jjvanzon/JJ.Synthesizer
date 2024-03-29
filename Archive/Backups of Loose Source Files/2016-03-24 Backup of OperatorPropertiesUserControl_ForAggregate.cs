﻿//using System;
//using System.ComponentModel;
//using System.Windows.Forms;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Framework.Presentation.Resources;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Presentation.Synthesizer.WinForms.Helpers;
//using JJ.Framework.Presentation.WinForms.Extensions;
//using JJ.Presentation.Synthesizer.Resources;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    internal partial class OperatorPropertiesUserControl_ForAggregate
//        : OperatorPropertiesUserControl_ForAggregate_NotDesignable
//    {
//        public event EventHandler CloseRequested;
//        public event EventHandler LoseFocusRequested;

//        public OperatorPropertiesUserControl_ForAggregate()
//        {
//            InitializeComponent();

//            SetTitles();

//            this.AutomaticallyAssignTabIndexes();
//        }

//        private void OperatorPropertiesUserControl_ForAggregate_Load(object sender, EventArgs e)
//        {
//            ApplyStyling();
//        }

//        // Gui

//        private void SetTitles()
//        {
//            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

//            labelName.Text = CommonTitles.Name;
//            labelOperatorTypeTitle.Text = Titles.Type + ":";
//            labelTimeSliceDuration.Text = PropertyDisplayNames.TimeSliceDuration;
//            labelSampleCount.Text = PropertyDisplayNames.SampleCount;
//        }

//        private void ApplyStyling()
//        {
//            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
//        }

//        protected override void ApplyViewModelToControls()
//        {
//            labelOperatorTypeValue.Text = ViewModel.OperatorType.DisplayName;
//            textBoxName.Text = ViewModel.Name;
//            numericUpDownTimeSliceDuration.Value = (decimal)ViewModel.TimeSliceDuration;
//            numericUpDownSampleCount.Value = ViewModel.SampleCount;
//        }

//        private void ApplyControlsToViewModel()
//        {
//            if (ViewModel == null) return;

//            ViewModel.Name = textBoxName.Text;
//            ViewModel.TimeSliceDuration = (double)numericUpDownTimeSliceDuration.Value;
//            ViewModel.SampleCount = (int)numericUpDownSampleCount.Value;
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
//        private void OperatorPropertiesUserControl_ForAggregate_Leave(object sender, EventArgs e)
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
//    internal class OperatorPropertiesUserControl_ForAggregate_NotDesignable
//        : UserControlBase<OperatorPropertiesViewModel_ForAggregate>
//    {
//        protected override void ApplyViewModelToControls()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
