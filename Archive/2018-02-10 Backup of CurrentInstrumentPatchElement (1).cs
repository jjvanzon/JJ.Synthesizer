//using System;
//using JJ.Framework.Common;
//using JJ.Framework.VectorGraphics.EventArg;
//using JJ.Framework.VectorGraphics.Gestures;
//using JJ.Framework.VectorGraphics.Helpers;
//using JJ.Framework.VectorGraphics.Models.Elements;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels.Items;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
//{
//	internal class CurrentInstrumentPatchElement : ElementBase
//	{
//		private readonly ITextMeasurer _textMeasurer;
//		private const float SPACING = 1f;

//		private Label _label;
//		private Picture _pictureButtonDelete;
//		private Picture _pictureButtonMoveBackward;
//		private Picture _pictureButtonMoveForward;
//		private Picture _pictureButtonPlay;
//		private Picture _pictureButtonExpand;
//		private MouseDownGesture _mouseDownGestureDelete;
//		private MouseDownGesture _mouseDownGestureMoveBackward;
//		private MouseDownGesture _mouseDownGestureMoveForward;
//		private MouseDownGesture _mouseDownGesturePlay;
//		private MouseDownGesture _mouseDownGestureExpand;

//		public event EventHandler<EventArgs<int>> DeleteRequested;
//		public event EventHandler<EventArgs<int>> ExpandRequested;
//		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
//		public event EventHandler<EventArgs<int>> MoveForwardRequested;
//		public event EventHandler<EventArgs<int>> PlayRequested;

//		public CurrentInstrumentPatchElement(
//			Diagram diagram,
//			object underlyingPictureDelete,
//			object underlyingPictureElement,
//			object underlyingPictureMoveBackward,
//			object underlyingPictureMoveForward,
//			object underlyingPicturePlay,
//			ITextMeasurer textMeasurer)
//		{
//			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

//			Diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));

//			CreateElements(
//				underlyingPictureDelete,
//				underlyingPictureElement,
//				underlyingPictureMoveBackward,
//				underlyingPictureMoveForward,
//				underlyingPicturePlay);
//		}

//		private void CreateElements(
//			object underlyingPictureDelete,
//			object underlyingPictureExpand,
//			object underlyingPictureMoveBackward,
//			object underlyingPictureMoveForward,
//			object underlyingPicturePlay)
//		{
//			_label = CreateLabel();

//			(_pictureButtonDelete, _mouseDownGestureDelete) = CreatePicture(underlyingPictureDelete, _mouseDownGestureDelete_MouseDown);
//			(_pictureButtonExpand, _mouseDownGestureExpand) = CreatePicture(underlyingPictureExpand);
//			(_pictureButtonMoveBackward, _mouseDownGestureMoveBackward) = CreatePicture(underlyingPictureMoveBackward);
//			(_pictureButtonMoveForward, _mouseDownGestureMoveForward) = CreatePicture(underlyingPictureMoveForward);
//			(_pictureButtonPlay, _mouseDownGesturePlay) = CreatePicture(underlyingPicturePlay);

//			// TODO: Create tooltips
//		}

//		private void _mouseDownGestureDelete_MouseDown(object sender, MouseEventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.PatchID));

//		private Label CreateLabel()
//		{
//			var label = new Label
//			{
//				Diagram = Diagram,
//				Parent = this,
//				// TODO: Probably needs a different style.
//				TextStyle = StyleHelper.DefaultTextStyle
//			};
//			label.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

//			return label;
//		}

//		private (Picture picture, MouseDownGesture mouseDownGesture) CreatePicture(
//			object underlyingPicture,
//			EventHandler<MouseEventArgs> mouseDownHandler)
//		{
//			var picture = new Picture
//			{
//				Diagram = Diagram,
//				Parent = this,
//				UnderlyingPicture = underlyingPicture
//			};
//			picture.Position.Width = StyleHelper.ICON_SIZE;
//			picture.Position.Height = StyleHelper.ICON_SIZE;

//			var mouseDownGesture = new MouseDownGesture();
//			mouseDownGesture.MouseDown += mouseDownHandler;

//			picture.Gestures.Add(mouseDownGesture);

//			return (picture, mouseDownGesture);
//		}

//		private CurrentInstrumentItemViewModel _viewModel;

//		public CurrentInstrumentItemViewModel ViewModel
//		{
//			get => _viewModel;
//			set
//			{
//				_viewModel = value ?? throw new ArgumentNullException(nameof(ViewModel));
//				ApplyViewModelToElements();
//				PositionControls();
//			}
//		}

//		private void ApplyViewModelToElements()
//		{
//			_label.Text = ViewModel.Name;
//			_pictureButtonMoveBackward.Visible = _viewModel.CanGoBackward;
//			_pictureButtonMoveForward.Visible = _viewModel.CanGoForward;
//		}

//		private void PositionControls()
//		{
//			float x = SPACING;

//			if (_viewModel.CanGoBackward)
//			{
//				_pictureButtonMoveForward.Position.X = x;
//				x += StyleHelper.ICON_SIZE + SPACING;
//			}

//			_label.Position.X = x;
//			_label.Position.Width = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font).width;
//			x += _label.Position.Width + SPACING;

//			if (_viewModel.CanGoForward)
//			{
//				_pictureButtonMoveForward.Position.X = x;
//				x += StyleHelper.ICON_SIZE + SPACING;
//			}

//			_pictureButtonPlay.Position.X = x;
//			x += StyleHelper.ICON_SIZE + SPACING;

//			_pictureButtonExpand.Position.X = x;
//			x += StyleHelper.ICON_SIZE + SPACING;

//			_pictureButtonDelete.Position.X = x;
//			x += StyleHelper.ICON_SIZE + SPACING;

//			x += SPACING;

//			Position.Width = x;
//		}
//	}
//}