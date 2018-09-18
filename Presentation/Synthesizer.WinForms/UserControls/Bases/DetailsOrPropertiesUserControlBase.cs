using System;
using System.ComponentModel;
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
		private readonly TitleBarUserControl _titleBarUserControl;

		public event EventHandler<EventArgs<int>> AddToInstrumentRequested;
		public event EventHandler<EventArgs<int>> CloneRequested;
		public event EventHandler<EventArgs<int>> CloseRequested;
	    public event EventHandler<EventArgs<int>> DeleteRequested;
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> NewRequested;
		public event EventHandler<EventArgs<int>> LoseFocusRequested;
		public event EventHandler<EventArgs<int>> SaveRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;

		public event EventHandler AddRequested
		{
			add => _titleBarUserControl.AddClicked += value;
			remove => _titleBarUserControl.AddClicked -= value;
		}

		// ReSharper disable once MemberCanBeProtected.Global
		public DetailsOrPropertiesUserControlBase()
		{
			Name = GetType().Name;

			Resize += Base_Resize;
			Leave += Base_Leave;

			_titleBarUserControl = CreateTitleBarUserControl();
			Controls.Add(_titleBarUserControl);
			_titleBarUserControl.AddToInstrumentClicked += _titleBarUserControl_AddToInstrumentClicked;
			_titleBarUserControl.CloneClicked += _titleBarUserControl_CloneClicked;
			_titleBarUserControl.CloseClicked += _titleBarUserControl_CloseClicked;
			_titleBarUserControl.ExpandClicked += _titleBarUserControl_ExpandClicked;
			_titleBarUserControl.NewClicked += _titleBarUserControl_NewClicked;
			_titleBarUserControl.SaveClicked += _titleBarUserControl_SaveClicked;
			_titleBarUserControl.PlayClicked += _titleBarUserControl_PlayClicked;
			_titleBarUserControl.DeleteClicked += _titleBarUserControl_DeleteClicked;

			_titleBarUserControl.DeleteButtonVisible = true;
		}

		~DetailsOrPropertiesUserControlBase()
		{
			if (_titleBarUserControl != null)
			{
				_titleBarUserControl.AddToInstrumentClicked -= _titleBarUserControl_AddToInstrumentClicked;
				_titleBarUserControl.CloseClicked -= _titleBarUserControl_CloseClicked;
				_titleBarUserControl.ExpandClicked -= _titleBarUserControl_ExpandClicked;
				_titleBarUserControl.NewClicked -= _titleBarUserControl_NewClicked;
				_titleBarUserControl.SaveClicked -= _titleBarUserControl_SaveClicked;
				_titleBarUserControl.PlayClicked -= _titleBarUserControl_PlayClicked;
				_titleBarUserControl.DeleteClicked -= _titleBarUserControl_DeleteClicked;
			}
		}

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

		public string TitleBarText
		{
			get => _titleBarUserControl.Text;
			set => _titleBarUserControl.Text = value;
		}

		protected bool AddButtonVisible
		{
			get => _titleBarUserControl.AddButtonVisible;
			set => _titleBarUserControl.AddButtonVisible = value;
		}

		protected bool AddToInstrumentButtonVisible
		{
			get => _titleBarUserControl.AddToInstrumentButtonVisible;
			set => _titleBarUserControl.AddToInstrumentButtonVisible = value;
		}

		[DefaultValue(false)]
		protected bool CloneButtonVisible
		{
			get => _titleBarUserControl.CloneButtonVisible;
			set => _titleBarUserControl.CloneButtonVisible = value;
		}

		protected bool CloseButtonVisible
		{
			get => _titleBarUserControl.CloseButtonVisible;
			set => _titleBarUserControl.CloseButtonVisible = value;
		}

		[DefaultValue(false)]
		protected bool ExpandButtonVisible
		{
			get => _titleBarUserControl.ExpandButtonVisible;
			set => _titleBarUserControl.ExpandButtonVisible = value;
		}

		[DefaultValue(false)]
		protected bool NewButtonVisible
		{
			get => _titleBarUserControl.NewButtonVisible;
			set => _titleBarUserControl.NewButtonVisible = value;
		}

		protected bool PlayButtonVisible
		{
			get => _titleBarUserControl.PlayButtonVisible;
			set => _titleBarUserControl.PlayButtonVisible = value;
		}

		protected bool DeleteButtonVisible
		{
			get => _titleBarUserControl.DeleteButtonVisible;
			set => _titleBarUserControl.DeleteButtonVisible = value;
		}

		protected bool RefreshButtonVisible
		{
			get => _titleBarUserControl.RefreshButtonVisible;
			set => _titleBarUserControl.RefreshButtonVisible = value;
		}

		protected bool SaveButtonVisible
		{
			get => _titleBarUserControl.SaveButtonVisible;
			set => _titleBarUserControl.SaveButtonVisible = value;
		}

		public Color TitleBarBackColor
		{
			get => _titleBarUserControl.BackColor;
			set => _titleBarUserControl.BackColor = value;
		}

		protected int TitleBarHeight => _titleBarUserControl.Height;

		protected virtual void PositionControls()
		{
            if (!string.IsNullOrEmpty(_titleBarUserControl.Text))
			{
				_titleBarUserControl.Width = Width;
			    _titleBarUserControl.Left = 0;
			}
            else
			{
				_titleBarUserControl.Width = _titleBarUserControl.ButtonBarWidth;
				_titleBarUserControl.Left = Width - _titleBarUserControl.ButtonBarWidth;
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

		private void _titleBarUserControl_AddToInstrumentClicked(object sender, EventArgs e) => AddToInstrumentRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void _titleBarUserControl_CloneClicked(object sender, EventArgs e) => CloneRequested?.Invoke(this, new EventArgs<int>(GetID()));
        private void _titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();
		private void _titleBarUserControl_ExpandClicked(object sender, EventArgs e) => ExpandRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void _titleBarUserControl_PlayClicked(object sender, EventArgs e) => Play();
		private void _titleBarUserControl_DeleteClicked(object sender, EventArgs e) => Delete();
		private void _titleBarUserControl_NewClicked(object sender, EventArgs e) => NewRequested?.Invoke(sender, new EventArgs<int>(GetID()));
		private void _titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested?.Invoke(sender, new EventArgs<int>(GetID()));

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
				Name = nameof(_titleBarUserControl),
				BackColor = SystemColors.Control,
				CloseButtonVisible = true,
				DeleteButtonVisible = false,
				AddButtonVisible = false,
				PlayButtonVisible = false,
				SaveButtonVisible = false,
				Margin = new Padding(0, 0, 0, 0),
				Height = StyleHelper.TitleBarHeight,
				Left = 0,
				Top = 0
			};

			return titleBarUserControl;
		}
	}
}