using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class CurrentInstrumentPatchElement : ElementBase
	{
		private const float SPACING = 1f;

		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _label;
		private readonly Picture _pictureDelete;
		private readonly Picture _pictureMoveBackward;
		private readonly Picture _pictureMoveForward;
		private readonly Picture _picturePlay;
		private readonly Picture _pictureExpand;

		public event EventHandler<EventArgs<int>> DeleteRequested;
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;

		public CurrentInstrumentPatchElement(
			Diagram diagram,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			Diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));

			_label = CreateLabel();

			_pictureDelete = CreatePicture(underlyingPictureDelete, CommonResourceFormatter.Delete, _pictureDelete_MouseDown);
			_pictureExpand = CreatePicture(underlyingPictureExpand, CommonResourceFormatter.Open, _pictureExpand_MouseDown);
			_pictureMoveBackward = CreatePicture(underlyingPictureMoveBackward, CommonResourceFormatter.Move, _pictureMoveBackward_MouseDown);
			_pictureMoveForward = CreatePicture(underlyingPictureMoveForward, CommonResourceFormatter.Move, _pictureMoveForward_MouseDown);
			_picturePlay = CreatePicture(underlyingPicturePlay, ResourceFormatter.Play, _picturePlay_MouseDown);
		}

		private CurrentInstrumentPatchViewModel _viewModel;

		public CurrentInstrumentPatchViewModel ViewModel
		{
			get => _viewModel;
			set
			{
				_viewModel = value ?? throw new ArgumentNullException(nameof(ViewModel));
				ApplyViewModelToElements();
				PositionControls();
			}
		}

		private void ApplyViewModelToElements()
		{
			_label.Text = ViewModel.Name;
			_pictureMoveBackward.Visible = _viewModel.CanGoBackward;
			_pictureMoveForward.Visible = _viewModel.CanGoForward;
		}

		private void PositionControls()
		{
			float x = SPACING;

			if (_viewModel.CanGoBackward)
			{
				_pictureMoveForward.Position.X = x;
				x += StyleHelper.ICON_SIZE + SPACING;
			}

			_label.Position.X = x;
			_label.Position.Width = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font).width;
			x += _label.Position.Width + SPACING;

			if (_viewModel.CanGoForward)
			{
				_pictureMoveForward.Position.X = x;
				x += StyleHelper.ICON_SIZE + SPACING;
			}

			_picturePlay.Position.X = x;
			x += StyleHelper.ICON_SIZE + SPACING;

			_pictureExpand.Position.X = x;
			x += StyleHelper.ICON_SIZE + SPACING;

			_pictureDelete.Position.X = x;
			x += StyleHelper.ICON_SIZE + SPACING;

			x += SPACING;

			Position.Width = x;
		}

		private Label CreateLabel()
		{
			var label = new Label
			{
				Diagram = Diagram,
				Parent = this,
				// TODO: Probably needs a different style.
				TextStyle = StyleHelper.DefaultTextStyle
			};
			label.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

			return label;
		}

		private Picture CreatePicture(object underlyingPicture, string toolTipText, EventHandler<MouseEventArgs> mouseDownHandler)
		{
			var picture = new Picture
			{
				Diagram = Diagram,
				Parent = this,
				UnderlyingPicture = underlyingPicture
			};
			picture.Position.Width = StyleHelper.ICON_SIZE;
			picture.Position.Height = StyleHelper.ICON_SIZE;

			var mouseDownGesture = new MouseDownGesture();
			mouseDownGesture.MouseDown += mouseDownHandler;
			picture.Gestures.Add(mouseDownGesture);

			ToolTipGesture toolTipGesture = CreateToolTipGesture();
			picture.Gestures.Add(toolTipGesture);
			toolTipGesture.SetToolTipText(toolTipText);

			return picture;
		}

		private ToolTipGesture CreateToolTipGesture()
		{
			var toolTipGesture = new ToolTipGesture(
				Diagram,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				_textMeasurer,
				zIndex: 2);

			return toolTipGesture;
		}

		private void _pictureDelete_MouseDown(object sender, EventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void _pictureExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void _pictureMoveBackward_MouseDown(object sender, EventArgs e) => MoveBackwardRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void _pictureMoveForward_MouseDown(object sender, EventArgs e) => MoveForwardRequested(this, new EventArgs<int>(_viewModel.PatchID));
		private void _picturePlay_MouseDown(object sender, EventArgs e) => PlayRequested(this, new EventArgs<int>(_viewModel.PatchID));
	}
}