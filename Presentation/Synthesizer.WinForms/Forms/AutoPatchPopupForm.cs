using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

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

		private void SetTitles()
		{
			// ReSharper disable once LocalizableElement
			Text = ResourceFormatter.AutoPatch + " - " + ResourceFormatter.ApplicationName;
		}

		private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => Close();

		private void PatchDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;

			CloseRequested?.Invoke(this, EventArgs.Empty);
		}
	}
}
