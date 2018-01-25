using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	internal partial class CurrentInstrumentItemUserControl : UserControl
	{
		private const int SPACING = 1;

		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		public CurrentInstrumentItemUserControl() => InitializeComponent();

		private void CurrentInstrumentItemUserControl_Load(object sender, EventArgs e)
		{
			SetTitles();
			PositionControls();
		}

		private CurrentInstrumentItemViewModel _viewModel;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CurrentInstrumentItemViewModel ViewModel
		{
			get => _viewModel;
			set
			{
				_viewModel = value;
				ApplyViewModelToControls();
				PositionControls();
			}
		}

		private void SetTitles()
		{
			toolTip.SetToolTip(buttonExpand, CommonResourceFormatter.Open);
			toolTip.SetToolTip(buttonMoveBackward, CommonResourceFormatter.Move);
			toolTip.SetToolTip(buttonMoveForward, CommonResourceFormatter.Move);
			toolTip.SetToolTip(buttonPlay, ResourceFormatter.Play);
			toolTip.SetToolTip(buttonDelete, CommonResourceFormatter.Remove);
		}

		private void ApplyViewModelToControls()
		{
			labelName.Text = _viewModel.Name;
			buttonMoveBackward.Visible = _viewModel.CanGoBackward;
			buttonMoveForward.Visible = _viewModel.CanGoForward;
		}

		private void PositionControls()
		{
			if (_viewModel == null)
			{
				return;
			}

			int buttonWidth = buttonMoveBackward.Width;
			const int labelY = 3;

			int x = SPACING;

			if (_viewModel.CanGoBackward)
			{
				buttonMoveBackward.Location = new Point(x, SPACING);
				x += buttonWidth + SPACING;
			}

			labelName.Location = new Point(x, labelY);
			x += labelName.Width;

			if (_viewModel.CanGoForward)
			{
				buttonMoveForward.Location = new Point(x, SPACING);
				x += buttonWidth + SPACING;
			}

			buttonPlay.Location = new Point(x, SPACING);
			x += buttonWidth + SPACING;

			buttonExpand.Location = new Point(x, SPACING);
			x += buttonWidth + SPACING;

			buttonDelete.Location = new Point(x, SPACING);
			x += buttonWidth + SPACING;

			x += SPACING;

			Width = x;
		}

		private void buttonExpand_Click(object sender, EventArgs e) => ExpandRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void buttonMoveBackward_Click(object sender, EventArgs e) => MoveBackwardRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void buttonMoveForward_Click(object sender, EventArgs e) => MoveForwardRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void buttonPlay_Click(object sender, EventArgs e) => PlayRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void buttonDelete_Click(object sender, EventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.PatchID));
	}
}
