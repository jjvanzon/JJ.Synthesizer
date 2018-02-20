//using System;
//using JJ.Data.Canonical;
//using JJ.Framework.Common;
//using JJ.Framework.Resources;
//using JJ.Framework.VectorGraphics.Helpers;
//using JJ.Framework.VectorGraphics.Models.Elements;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
//{
//	public class CurrentInstrumentMidiMappingElementElement : ElementBase
//	{
//		private readonly ITextMeasurer _textMeasurer;
//		private readonly Label _label;
//		private readonly PictureButtonElement _pictureButtonDelete;
//		private readonly PictureButtonElement _pictureButtonMoveBackward;
//		private readonly PictureButtonElement _pictureButtonMoveForward;
//		private readonly PictureButtonElement _pictureButtonExpand;

//		public event EventHandler<EventArgs<int>> DeleteRequested;
//		public event EventHandler<EventArgs<int>> ExpandRequested;
//		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
//		public event EventHandler<EventArgs<int>> MoveForwardRequested;

//		public CurrentInstrumentMidiMappingElementElement(
//			Element parent,
//			ToolTipElement toolTipElement,
//			object underlyingPictureDelete,
//			object underlyingPictureExpand,
//			object underlyingPictureMoveBackward,
//			object underlyingPictureMoveForward,
//			ITextMeasurer textMeasurer) : base(parent)
//		{
//			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

//			_label = CreateLabel();

//			_pictureButtonDelete = new PictureButtonElement(this, underlyingPictureDelete, CommonResourceFormatter.Delete, toolTipElement);
//			_pictureButtonDelete.MouseDown += _pictureButtonDelete_MouseDown;

//			_pictureButtonExpand = new PictureButtonElement(this, underlyingPictureExpand, CommonResourceFormatter.Open, toolTipElement);
//			_pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;

//			_pictureButtonMoveBackward = new PictureButtonElement(this, underlyingPictureMoveBackward, CommonResourceFormatter.Move, toolTipElement);
//			_pictureButtonMoveBackward.MouseDown += _pictureButtonMoveBackward_MouseDown;

//			_pictureButtonMoveForward = new PictureButtonElement(this, underlyingPictureMoveForward, CommonResourceFormatter.Move, toolTipElement);
//			_pictureButtonMoveForward.MouseDown += _pictureButtonMoveForward_MouseDown;
//		}

//		private IDAndName _viewModel;

//		public IDAndName ViewModel
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
//		}

//		private void PositionControls()
//		{
//			(float textWidth, _) = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font);
//			_label.Position.Width = textWidth;
//			Position.Width = textWidth;

//			// TODO: Also picture buttons.
//		}

//		private Label CreateLabel()
//		{
//			var label = new Label(this)
//			{
//				TextStyle = StyleHelper.TitleTextStyle
//			};
//			label.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

//			return label;
//		}

//		private void _pictureButtonDelete_MouseDown(object sender, EventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.PatchID));
//		private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(this, new EventArgs<int>(_viewModel.PatchID));

//		private void _pictureButtonMoveBackward_MouseDown(object sender, EventArgs e) =>
//			MoveBackwardRequested(this, new EventArgs<int>(_viewModel.PatchID));

//		private void _pictureButtonMoveForward_MouseDown(object sender, EventArgs e) =>
//			MoveForwardRequested(this, new EventArgs<int>(_viewModel.PatchID));

//		private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayRequested(this, new EventArgs<int>(_viewModel.PatchID));
//	}
//}