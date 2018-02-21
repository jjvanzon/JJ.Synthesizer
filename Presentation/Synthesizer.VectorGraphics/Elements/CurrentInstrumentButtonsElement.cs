using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable once VirtualMemberCallInConstructor

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class CurrentInstrumentButtonsElement : ElementBase
	{
		public event EventHandler ExpandRequested;
		public event EventHandler PlayRequested;

		private readonly PictureButtonElement _pictureButtonPlay;
		private readonly PictureButtonElement _pictureButtonExpand;

		public CurrentInstrumentButtonsElement(
			Element parent,
			ToolTipElement toolTipElement,
			object underlyingPictureExpand,
			object underlyingPicturePlay)
			: base(parent)
		{
			_pictureButtonExpand = new PictureButtonElement(this, underlyingPictureExpand, CommonResourceFormatter.Open, toolTipElement);
			_pictureButtonExpand.MouseDown += _pictureExpand_MouseDown;
			_pictureButtonExpand.Position.X = StyleHelper.ICON_BUTTON_MARGIN + StyleHelper.ICON_BUTTON_PICTURE_SIZE;

			_pictureButtonPlay = new PictureButtonElement(this, underlyingPicturePlay, ResourceFormatter.Play, toolTipElement);
			_pictureButtonPlay.MouseDown += _picturePlay_MouseDown;

			Position.Width = StyleHelper.ICON_BUTTON_PICTURE_SIZE +
			                 StyleHelper.ICON_BUTTON_MARGIN +
			                 StyleHelper.ICON_BUTTON_PICTURE_SIZE;
		}

		private CurrentInstrumentViewModel _viewModel;

		public CurrentInstrumentViewModel ViewModel
		{
			get => _viewModel;
			set
			{
				_viewModel = value ?? throw new ArgumentNullException(nameof(ViewModel));
				ApplyViewModelToElements();
			}
		}

		private void ApplyViewModelToElements()
		{
			_pictureButtonPlay.Visible = ViewModel.CanPlay;
			_pictureButtonExpand.Visible = ViewModel.CanExpand;
		}

		private void _pictureExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(sender, EventArgs.Empty);
		private void _picturePlay_MouseDown(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
	}
}