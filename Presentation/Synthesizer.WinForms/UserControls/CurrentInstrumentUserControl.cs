using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class CurrentInstrumentUserControl : UserControlBase
	{
		private readonly IList<CurrentInstrumentItemUserControl> _itemControls = new List<CurrentInstrumentItemUserControl>();

		public event EventHandler ExpandRequested;
		public event EventHandler<EventArgs<int>> ExpandItemRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler PlayRequested;
		public event EventHandler<EventArgs<int>> PlayItemRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		public CurrentInstrumentUserControl() => InitializeComponent();

		private void CurrentInstrumentUserControl_Load(object sender, EventArgs e) => SetTitles();

		public new CurrentInstrumentViewModel ViewModel
		{
			get => (CurrentInstrumentViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		private void SetTitles()
		{
			toolTip.SetToolTip(buttonExpand, CommonResourceFormatter.Open);
			toolTip.SetToolTip(buttonPlay, ResourceFormatter.Play);
		}

		protected override void ApplyViewModelToControls()
		{
			buttonPlay.Visible = ViewModel.CanPlay;
			buttonExpand.Visible = ViewModel.CanExpand;

			// Update
			int minCount = Math.Min(_itemControls.Count, ViewModel.Patches.Count);
			for (int i = 0; i < minCount; i++)
			{
				CurrentInstrumentPatchViewModel itemViewModel = ViewModel.Patches[i];
				CurrentInstrumentItemUserControl itemUserControl = _itemControls[i];
				itemUserControl.ViewModel = itemViewModel;
			}

			// Insert
			for (int i = _itemControls.Count; i < ViewModel.Patches.Count; i++)
			{
				CurrentInstrumentPatchViewModel itemViewModel = ViewModel.Patches[i];
				var itemControl = new CurrentInstrumentItemUserControl
				{
					Margin = new Padding(0),
					ViewModel = itemViewModel
				};
				itemControl.ExpandRequested += ItemControl_ExpandRequested;
				itemControl.MoveBackwardRequested += ItemControl_MoveBackwardRequested;
				itemControl.MoveForwardRequested += ItemControl_MoveForwardRequested;
				itemControl.PlayRequested += ItemControl_PlayRequested;
				itemControl.DeleteRequested += ItemControl_DeleteRequested;

				_itemControls.Add(itemControl);
				Controls.Add(itemControl);
			}

			// Delete
			for (int i = _itemControls.Count - 1; i >= ViewModel.Patches.Count; i--)
			{
				CurrentInstrumentItemUserControl itemControl = _itemControls[i];
				itemControl.ExpandRequested -= ItemControl_ExpandRequested;
				itemControl.MoveBackwardRequested -= ItemControl_MoveBackwardRequested;
				itemControl.MoveForwardRequested -= ItemControl_MoveForwardRequested;
				itemControl.PlayRequested -= ItemControl_PlayRequested;
				itemControl.DeleteRequested -= ItemControl_DeleteRequested;

				_itemControls.RemoveAt(i);
				Controls.Remove(itemControl);
			}

			PositionControls();
		}

		private void PositionControls()
		{
			int x = Width;

			x -= StyleHelper.IconButtonSize;

			buttonExpand.Top = 0;
			buttonExpand.Left = x;
			buttonExpand.Width = StyleHelper.IconButtonSize;
			buttonExpand.Height = StyleHelper.IconButtonSize;

			x -= StyleHelper.DefaultSpacing;
			x -= StyleHelper.IconButtonSize;

			buttonPlay.Top = 0;
			buttonPlay.Left = x;
			buttonPlay.Width = StyleHelper.IconButtonSize;
			buttonPlay.Height = StyleHelper.IconButtonSize;

			foreach (CurrentInstrumentItemUserControl itemControl in _itemControls.Reverse())
			{
				x -= StyleHelper.DefaultSpacing;
				x -= StyleHelper.DefaultSpacing;
				x -= itemControl.Width;

				itemControl.Top = 0;
				itemControl.Left = x;
				itemControl.Height = Height;
			}
		}

		private void Base_SizeChanged(object sender, EventArgs e) => PositionControls();
		private void ItemControl_ExpandRequested(object sender, EventArgs<int> e) => ExpandItemRequested(sender, e);
		private void ItemControl_MoveBackwardRequested(object sender, EventArgs<int> e) => MoveBackwardRequested(sender, e);
		private void ItemControl_MoveForwardRequested(object sender, EventArgs<int> e) => MoveForwardRequested(sender, e);
		private void ItemControl_PlayRequested(object sender, EventArgs<int> e) => PlayItemRequested(sender, e);
		private void ItemControl_DeleteRequested(object sender, EventArgs<int> e) => DeleteRequested(sender, e);
		private void buttonExpand_Click(object sender, EventArgs e) => ExpandRequested(sender, EventArgs.Empty);
		private void buttonPlay_Click(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
	}
}