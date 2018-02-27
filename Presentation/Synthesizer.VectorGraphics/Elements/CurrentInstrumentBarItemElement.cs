using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	/// <summary> Can be used for both CurrentInstrument Patches as well as CurrentInstrument MidiMappings. </summary>
	internal class CurrentInstrumentBarItemElement : ElementBase
	{
		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _label;
		private readonly PictureButtonElement _pictureButtonDelete;
		private readonly PictureButtonElement _pictureButtonMoveBackward;
		private readonly PictureButtonElement _pictureButtonMoveForward;
		private readonly PictureButtonElement _pictureButtonPlay;
		private readonly PictureButtonElement _pictureButtonExpand;

		public event EventHandler<EventArgs<int>> DeleteRequested;
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;

		public CurrentInstrumentBarItemElement(
			Element parent,
			ToolTipElement toolTipElement,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer)
			: base(parent)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_label = CreateLabel();

			_pictureButtonDelete = new PictureButtonElement(this, underlyingPictureDelete, CommonResourceFormatter.Delete, toolTipElement);
			_pictureButtonDelete.MouseDown += _pictureButtonDelete_MouseDown;
			_pictureButtonDelete.PictureStyle = StyleHelper.IconPictureStyleLight;

			_pictureButtonExpand = new PictureButtonElement(this, underlyingPictureExpand, CommonResourceFormatter.Open, toolTipElement);
			_pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;
			_pictureButtonExpand.PictureStyle = StyleHelper.IconPictureStyleLight;

			_pictureButtonMoveBackward = new PictureButtonElement(this, underlyingPictureMoveBackward, CommonResourceFormatter.Move, toolTipElement);
			_pictureButtonMoveBackward.MouseDown += _pictureButtonMoveBackward_MouseDown;
			_pictureButtonMoveBackward.PictureStyle = StyleHelper.IconPictureStyleLight;

			_pictureButtonMoveForward = new PictureButtonElement(this, underlyingPictureMoveForward, CommonResourceFormatter.Move, toolTipElement);
			_pictureButtonMoveForward.MouseDown += _pictureButtonMoveForward_MouseDown;
			_pictureButtonMoveForward.PictureStyle = StyleHelper.IconPictureStyleLight;

			_pictureButtonPlay = new PictureButtonElement(this, underlyingPicturePlay, ResourceFormatter.Play, toolTipElement);
			_pictureButtonPlay.MouseDown += _pictureButtonPlay_MouseDown;
			_pictureButtonPlay.PictureStyle = StyleHelper.IconPictureStyleLight;
		}

		private CurrentInstrumentItemViewModel _viewModel;

		public CurrentInstrumentItemViewModel ViewModel
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
			_label.Text = ViewModel.Name;
			_pictureButtonMoveBackward.Visible = _viewModel.CanGoBackward;
			_pictureButtonMoveForward.Visible = _viewModel.CanGoForward;
			_pictureButtonPlay.Visible = _viewModel.CanPlay;
			_pictureButtonDelete.Visible = _viewModel.CanDelete;
			_pictureButtonExpand.Visible = _viewModel.CanExpand;
		}

		public void PositionElements()
		{
			float x = 0;

			if (_viewModel.CanGoBackward)
			{
				_pictureButtonMoveForward.Position.X = x;
				x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;
			}

			_label.Position.X = x;
			(_label.Position.Width, _) = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font);
			x += _label.Position.Width;

			if (_viewModel.CanGoForward)
			{
				_pictureButtonMoveForward.Position.X = x;
				x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.PICTURE_BUTTON_MARGIN;
			}

			if (_viewModel.CanPlay)
			{
				_pictureButtonPlay.Position.X = x;
				x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.PICTURE_BUTTON_MARGIN;
			}

			if (_viewModel.CanExpand)
			{
				_pictureButtonExpand.Position.X = x;
				x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.PICTURE_BUTTON_MARGIN;
			}

			if (_viewModel.CanDelete)
			{
				_pictureButtonDelete.Position.X = x;
				x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.PICTURE_BUTTON_MARGIN;
			}

			x -= StyleHelper.PICTURE_BUTTON_MARGIN;

			Position.Width = x;
			Position.Height = StyleHelper.PICTURE_BUTTON_SIZE;
		}

		private Label CreateLabel()
		{
			var label = new Label(this)
			{
				TextStyle = StyleHelper.TitleTextStyle
			};
			label.Position.Height = StyleHelper.ROW_HEIGHT;

			return label;
		}

		private void _pictureButtonDelete_MouseDown(object sender, EventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.EntityID));
		private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(this, new EventArgs<int>(_viewModel.EntityID));

		private void _pictureButtonMoveBackward_MouseDown(object sender, EventArgs e) =>
			MoveBackwardRequested(this, new EventArgs<int>(_viewModel.EntityID));

		private void _pictureButtonMoveForward_MouseDown(object sender, EventArgs e) =>
			MoveForwardRequested(this, new EventArgs<int>(_viewModel.EntityID));

		private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayRequested(this, new EventArgs<int>(_viewModel.EntityID));
	}
}