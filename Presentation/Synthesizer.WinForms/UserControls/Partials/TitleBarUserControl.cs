using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Global

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	internal partial class TitleBarUserControl : UserControl
	{
		public TitleBarUserControl()
		{
			InitializeComponent();

			_titleLabelVisible = labelTitle.Visible;
		}

		public event EventHandler AddClicked
		{
			add => buttonBarUserControl.AddClicked += value;
			remove => buttonBarUserControl.AddClicked -= value;
		}

		public event EventHandler AddToInstrumentClicked
		{
			add => buttonBarUserControl.AddToInstrumentClicked += value;
			remove => buttonBarUserControl.AddToInstrumentClicked -= value;
		}

		public event EventHandler CloseClicked
		{
			add => buttonBarUserControl.CloseClicked += value;
			remove => buttonBarUserControl.CloseClicked -= value;
		}

		public event EventHandler NewClicked
		{
			add => buttonBarUserControl.NewClicked += value;
			remove => buttonBarUserControl.NewClicked -= value;
		}

		public event EventHandler ExpandClicked
		{
			add => buttonBarUserControl.ExpandClicked += value;
			remove => buttonBarUserControl.ExpandClicked -= value;
		}

		public event EventHandler PlayClicked
		{
			add => buttonBarUserControl.PlayClicked += value;
			remove => buttonBarUserControl.PlayClicked -= value;
		}

		public event EventHandler RedoClicked
		{
			add => buttonBarUserControl.RedoClicked += value;
			remove => buttonBarUserControl.RedoClicked -= value;
		}

		public event EventHandler RefreshClicked
		{
			add => buttonBarUserControl.RefreshClicked += value;
			remove => buttonBarUserControl.RefreshClicked -= value;
		}

		public event EventHandler DeleteClicked
		{
			add => buttonBarUserControl.DeleteClicked += value;
			remove => buttonBarUserControl.DeleteClicked -= value;
		}

		public event EventHandler SaveClicked
		{
			add => buttonBarUserControl.SaveClicked += value;
			remove => buttonBarUserControl.SaveClicked -= value;
		}

		public event EventHandler UndoClicked
		{
			add => buttonBarUserControl.UndoClicked += value;
			remove => buttonBarUserControl.UndoClicked -= value;
		}

		public override string Text
		{
			get => labelTitle.Text;
			set => labelTitle.Text = value;
		}

		public override Color BackColor
		{
			get => base.BackColor;
			set
			{
				base.BackColor = value;
				labelTitle.BackColor = value;
				buttonBarUserControl.BackColor = value;
			}
		}

		public bool AddButtonVisible
		{
			get => buttonBarUserControl.AddButtonVisible;
			set
			{
				buttonBarUserControl.AddButtonVisible = value;
				PositionControls();
			}
		}

		public bool AddToInstrumentButtonVisible
		{
			get => buttonBarUserControl.AddToInstrumentButtonVisible;
			set
			{
				buttonBarUserControl.AddToInstrumentButtonVisible = value;
				PositionControls();
			}
		}

		public bool CloseButtonVisible
		{
			get => buttonBarUserControl.CloseButtonVisible;
			set
			{
				buttonBarUserControl.CloseButtonVisible = value;
				PositionControls();
			}
		}

		public bool ExpandButtonVisible
		{
			get => buttonBarUserControl.ExpandButtonVisible;
			set
			{
				buttonBarUserControl.ExpandButtonVisible = value;
				PositionControls();
			}
		}

		[DefaultValue(false)]
		public bool NewButtonVisible
		{
			get => buttonBarUserControl.NewButtonVisible;
			set
			{
				buttonBarUserControl.NewButtonVisible = value;
				PositionControls();
			}
		}

		public bool PlayButtonVisible
		{
			get => buttonBarUserControl.PlayButtonVisible;
			set
			{
				buttonBarUserControl.PlayButtonVisible = value;
				PositionControls();
			}
		}

		public bool RedoButtonVisible
		{
			get => buttonBarUserControl.RedoButtonVisible;
			set
			{
				buttonBarUserControl.RedoButtonVisible = value;
				PositionControls();
			}
		}

		public bool RefreshButtonVisible
		{
			get => buttonBarUserControl.RefreshButtonVisible;
			set
			{
				buttonBarUserControl.RefreshButtonVisible = value;
				PositionControls();
			}
		}

		public bool DeleteButtonVisible
		{
			get => buttonBarUserControl.DeleteButtonVisible;
			set
			{
				buttonBarUserControl.DeleteButtonVisible = value;
				PositionControls();
			}
		}

		public bool SaveButtonVisible
		{
			get => buttonBarUserControl.SaveButtonVisible;
			set
			{
				buttonBarUserControl.SaveButtonVisible = value;
				PositionControls();
			}
		}

		public bool UndoButtonVisible
		{
			// ReSharper disable once UnusedMember.Global
			get => buttonBarUserControl.UndoButtonVisible;
			set
			{
				buttonBarUserControl.UndoButtonVisible = value;
				PositionControls();
			}
		}

		private bool _titleLabelVisible;
		public bool TitleLabelVisible
		{
			get => _titleLabelVisible;
			set
			{
				_titleLabelVisible = value;
				labelTitle.Visible = value;
				PositionControls();
			}
		}

		public int ButtonBarWidth => buttonBarUserControl.Width;

		private void PositionControls()
		{
			Height = buttonBarUserControl.Height;

            if (_titleLabelVisible)
			{
				labelTitle.Top = 0;
				labelTitle.Left = 0;
				labelTitle.Height = Height;
				labelTitle.Width = Width - buttonBarUserControl.Width;
			}

			buttonBarUserControl.Top = 0;
			buttonBarUserControl.Left = Width - buttonBarUserControl.Width;
		}

		private void TitleBarUserControl_Load(object sender, EventArgs e) => PositionControls();
		private void TitleBarUserControl_SizeChanged(object sender, EventArgs e) => PositionControls();
	}
}