using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Presentation.Synthesizer.ViewModels;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
	internal partial class LibrarySelectionPopupForm : Form
	{
		public event EventHandler CancelRequested;
		public event EventHandler CloseRequested;

		public event EventHandler<EventArgs<int?>> OKRequested
		{
			add => librarySelectionPopupUserControl.OKRequested += value;
			remove => librarySelectionPopupUserControl.OKRequested -= value;
		}

		public event EventHandler<EventArgs<int>> PlayRequested
		{
			add => librarySelectionPopupUserControl.PlayRequested += value;
			remove => librarySelectionPopupUserControl.PlayRequested -= value;
		}

		public event EventHandler<EventArgs<int>> OpenItemExternallyRequested
		{
			add => librarySelectionPopupUserControl.OpenItemExternallyRequested += value;
			remove => librarySelectionPopupUserControl.OpenItemExternallyRequested -= value;
		}

		public LibrarySelectionPopupForm()
		{
			InitializeComponent();
			SetTitles();
		}

		public LibrarySelectionPopupViewModel ViewModel
		{
			get => librarySelectionPopupUserControl.ViewModel;
			set => librarySelectionPopupUserControl.ViewModel = value;
		}

		private void SetTitles() => Text = ResourceFormatter.ApplicationName;

		private void LibrarySelectionPopupForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;

			CloseRequested(this, EventArgs.Empty);
		}

		private void librarySelectionPopupUserControl_CancelRequested(object sender, EventArgs e)
		{
			CancelRequested.Invoke(this, EventArgs.Empty);
		}
	}
}
