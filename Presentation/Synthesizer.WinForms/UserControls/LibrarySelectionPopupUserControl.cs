using System;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class LibrarySelectionPopupUserControl : UserControl
	{
		public event EventHandler<EventArgs<int?>> OKRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;

		public event EventHandler CancelRequested
		{
			add => buttonCancel.Click += value;
			remove => buttonCancel.Click -= value;
		}

		public event EventHandler<EventArgs<int>> OpenItemExternallyRequested
		{
			add => librarySelectionGridUserControl.OpenItemExternallyRequested += value;
			remove => librarySelectionGridUserControl.OpenItemExternallyRequested -= value;
		}

		public LibrarySelectionPopupUserControl()
		{
			InitializeComponent();
			SetTitles();

			librarySelectionGridUserControl.ShowItemRequested += librarySelectionGridUserControl_ShowItemRequested;
			librarySelectionGridUserControl.PlayRequested += librarySelectionGridUserControl_PlayRequested;
		}

		private void SetTitles()
		{
			buttonOK.Text = CommonResourceFormatter.OK;
			buttonCancel.Text = CommonResourceFormatter.Cancel;
		}

		private void PositionControls()
		{
			int buttonWidth = Width / 2;
			if (buttonWidth == 0) buttonWidth = 1;

			int gridHeight = Height - StyleHelper.ButtonHeight;
			if (gridHeight == 0) gridHeight = 1;

			librarySelectionGridUserControl.Top = 0;
			librarySelectionGridUserControl.Left = 0;
			librarySelectionGridUserControl.Width = Width;
			librarySelectionGridUserControl.Height = gridHeight;

			buttonOK.Top = gridHeight;
			buttonOK.Left = 0;
			buttonOK.Width = buttonWidth;
			buttonOK.Height = StyleHelper.ButtonHeight;

			buttonCancel.Top = gridHeight;
			buttonCancel.Left = buttonWidth;
			buttonCancel.Width = buttonWidth;
			buttonCancel.Height = StyleHelper.ButtonHeight;
		}

		public LibrarySelectionPopupViewModel ViewModel
		{
			get => librarySelectionGridUserControl.ViewModel;
			set => librarySelectionGridUserControl.ViewModel = value;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			int? id = librarySelectionGridUserControl.TryGetSelectedID();
			OKRequested?.Invoke(sender, new EventArgs<int?>(id));
		}

		private void librarySelectionGridUserControl_ShowItemRequested(object sender, EventArgs<int> e) => OKRequested?.Invoke(sender, new EventArgs<int?>(e.Value));

	    private void librarySelectionGridUserControl_PlayRequested(object sender, EventArgs<int> e) => PlayRequested?.Invoke(sender, new EventArgs<int>(e.Value));

	    private void Base_Load(object sender, EventArgs e) => PositionControls();
		private void Base_Resize(object sender, EventArgs e) => PositionControls();
	}
}
