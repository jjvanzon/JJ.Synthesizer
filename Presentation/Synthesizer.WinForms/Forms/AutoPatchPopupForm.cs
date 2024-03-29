﻿using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable LocalizableElement

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class AutoPatchPopupForm : Form
    {
        public event EventHandler CloseRequested;

        public event EventHandler<EventArgs<int>> SaveRequested
        {
            add => patchDetailsUserControl.SaveRequested += value;
            remove => patchDetailsUserControl.SaveRequested += value;
        }

        private AutoPatchPopupViewModel _viewModel;
        public AutoPatchPopupViewModel ViewModel
        {
            // ReSharper disable once UnusedMember.Global
            get => _viewModel;
            set
            {
                _viewModel = value;
                patchDetailsUserControl.ViewModel = _viewModel.PatchDetails;
            }
        }

        public AutoPatchPopupForm()
        {
            InitializeComponent();

            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
        }

        private void PatchDetailsForm_Load(object sender, EventArgs e) => SetTitles();

        private void SetTitles() => Text = ResourceFormatter.AutoPatch + " - " + ResourceFormatter.ApplicationName;

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => Close();

        private void PatchDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
