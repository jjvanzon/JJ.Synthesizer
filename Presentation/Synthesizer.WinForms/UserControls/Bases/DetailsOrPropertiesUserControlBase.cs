using System;
using System.Drawing;
using System.Windows.Forms;
using JJ.Framework.Common;
using JJ.Framework.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
	internal class DetailsOrPropertiesUserControlBase : UserControlBase
	{
		public TitleBarUserControl TitleBarUserControl { get; }

	    public event EventHandler AddRequested;
		public event EventHandler<EventArgs<int>> AddToInstrumentRequested;
		public event EventHandler<EventArgs<int>> CloneRequested;
		public event EventHandler<EventArgs<int>> CloseRequested;
	    public event EventHandler<EventArgs<int>> DeleteRequested;
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> NewRequested;
		public event EventHandler<EventArgs<int>> LoseFocusRequested;
		public event EventHandler<EventArgs<int>> SaveRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;

		// ReSharper disable once MemberCanBeProtected.Global
		public DetailsOrPropertiesUserControlBase()
		{
			Name = GetType().Name;

			Resize += Base_Resize;
			Leave += Base_Leave;

			TitleBarUserControl = CreateTitleBarUserControl();
			Controls.Add(TitleBarUserControl);
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonAddToInstrument.MouseDown += PictureButtonAddToInstrument_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonClone.MouseDown += PictureButtonClone_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonClose.MouseDown += PictureButtonClose_MouseDown;
		    TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonDelete.Visible = true;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonExpand.MouseDown += PictureButtonExpand_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonNew.MouseDown += PictureButtonNew_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonSave.MouseDown += PictureButtonSave_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonPlay.MouseDown += PictureButtonPlay_MouseDown;
			TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonDelete.MouseDown += PictureButtonDelete_MouseDown;
            TitleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonAdd.MouseDown += PictureButtonAdd_MouseDown;
		}

	    private void PictureButtonAdd_MouseDown(object sender, Framework.VectorGraphics.EventArg.MouseEventArgs e)
	        => AddRequested?.Invoke(this, EventArgs.Empty);

        /// <summary> Executes SetTitles, ApplyStyling, PositionControls and AutomaticallyAssignTabIndexes. </summary>
        protected override void OnLoad(EventArgs e)
		{
			SetTitles();
			ApplyStyling();
			PositionControls();

			this.AutomaticallyAssignTabIndexes();

			base.OnLoad(e);
		}

		// Gui

		protected virtual void ApplyStyling() => BackColor = SystemColors.ButtonFace;

	    /// <summary> does nothing </summary>
		protected virtual void SetTitles() { }

		protected int TitleBarHeight => TitleBarUserControl.Height;

		protected virtual void PositionControls()
		{
            if (!string.IsNullOrEmpty(TitleBarUserControl.Text))
			{
				TitleBarUserControl.Width = Width;
			    TitleBarUserControl.Left = 0;
			}
            else
			{
				TitleBarUserControl.Width = TitleBarUserControl.ButtonBarWidth;
				TitleBarUserControl.Left = Width - TitleBarUserControl.ButtonBarWidth;
			}
		}

		private void Base_Resize(object sender, EventArgs e) => PositionControls();

	    // Binding

		/// <summary> does nothing </summary>
		protected virtual int GetID() => default;

	    /// <summary> does nothing </summary>
		protected virtual void ApplyControlsToViewModel() { }

		// Actions

		private void Close()
		{
			if (ViewModel == null) return;

			ApplyControlsToViewModel();

			CloseRequested?.Invoke(this, new EventArgs<int>(GetID()));
		}

		private void LoseFocus()
		{
			if (ViewModel == null) return;

			ApplyControlsToViewModel();

			LoseFocusRequested?.Invoke(this, new EventArgs<int>(GetID()));
		}

		private void Play()
		{
			if (ViewModel == null) return;

			ApplyControlsToViewModel();

			PlayRequested?.Invoke(this, new EventArgs<int>(GetID()));
		}

		protected void Delete() => DeleteRequested?.Invoke(this, new EventArgs<int>(GetID()));

	    // Events

		private void PictureButtonAddToInstrument_MouseDown(object sender, EventArgs e) => AddToInstrumentRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void PictureButtonClone_MouseDown(object sender, EventArgs e) => CloneRequested?.Invoke(this, new EventArgs<int>(GetID()));
        private void PictureButtonClose_MouseDown(object sender, EventArgs e) => Close();
		private void PictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void PictureButtonPlay_MouseDown(object sender, EventArgs e) => Play();
		private void PictureButtonDelete_MouseDown(object sender, EventArgs e) => Delete();
		private void PictureButtonNew_MouseDown(object sender, EventArgs e) => NewRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void PictureButtonSave_MouseDown(object sender, EventArgs e) => SaveRequested?.Invoke(sender, new EventArgs<int>(GetID()));

		// This event does not go off, if not clicked on a control that according to WinForms can get focus.
		private void Base_Leave(object sender, EventArgs e)
		{
			// This Visible check is there because the leave event (lose focus) goes off after I closed, 
			// making it want to save again, even though view model is empty
			// which makes it say that now clear fields are required.
			if (Visible)
			{
				LoseFocus();
			}
		}

		// Create Controls

		private TitleBarUserControl CreateTitleBarUserControl()
		{
			var titleBarUserControl = new TitleBarUserControl
			{
				Name = nameof(TitleBarUserControl),
				BackColor = SystemColors.Control,
				Margin = new Padding(0, 0, 0, 0),
				Height = StyleHelper.TitleBarHeight,
				Left = 0,
				Top = 0
			};

		    titleBarUserControl.TitleBarElement.ButtonBarElement.PictureButtonClose.Visible = true;

			return titleBarUserControl;
		}
	}
}