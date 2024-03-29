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
//    internal partial class OperatorPropertiesUserControl_ForSpectrum 
//        : OperatorPropertiesUserControl_ForSpectrum_NotDesignable
//    {
//        public event EventHandler CloseRequested;
//        public event EventHandler LoseFocusRequested;

//        public OperatorPropertiesUserControl_ForSpectrum()
//        {
//            InitializeComponent();

//            SetTitles();

//            this.AutomaticallyAssignTabIndexes();
//        }

//        private void OperatorPropertiesUserControl_ForSpectrum_Load(object sender, EventArgs e)
//        {
//            ApplyStyling();
//        }

//        // Gui

//        private void SetTitles()
//        {
//            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

//            labelName.Text = CommonTitles.Name;
//            labelOperatorTypeTitle.Text = Titles.Type + ":";
//            labelStartTime.Text = PropertyDisplayNames.StartTime;
//            labelEndTime.Text = PropertyDisplayNames.EndTime;
//            labelFrequencyCount.Text = PropertyDisplayNames.FrequencyCount;

//            labelOperatorTypeValue.Text = PropertyDisplayNames.Spectrum;
//        }

//        private void ApplyStyling()
//        {
//            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
//        }

//        protected override void ApplyViewModelToControls()
//        {
//            if (ViewModel == null) return;

//            textBoxName.Text = ViewModel.Name;
//            numericUpDownStartTime.Value = (decimal)ViewModel.StartTime;
//            numericUpDownEndTime.Value = (decimal)ViewModel.EndTime;
//            numericUpDownFrequencyCount.Value = ViewModel.FrequencyCount;
//        }

//        private void ApplyControlsToViewModel()
//        {
//            if (ViewModel == null) return;

//            ViewModel.Name = textBoxName.Text;
//            ViewModel.StartTime = (double)numericUpDownStartTime.Value;
//            ViewModel.EndTime = (double)numericUpDownEndTime.Value;
//            ViewModel.FrequencyCount = (int)numericUpDownFrequencyCount.Value;
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
//        private void OperatorPropertiesUserControl_ForSpectrum_Leave(object sender, EventArgs e)
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
//    internal class OperatorPropertiesUserControl_ForSpectrum_NotDesignable
//        : UserControlBase<OperatorPropertiesViewModel_ForSpectrum>
//    {
//        protected override void ApplyViewModelToControls()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
