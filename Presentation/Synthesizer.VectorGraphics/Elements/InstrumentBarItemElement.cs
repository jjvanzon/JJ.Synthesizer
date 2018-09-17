using System;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    /// <summary> Can be used for both InstrumentBar Patches as well as InstrumentBar MidiMappingGroups. </summary>
    internal class InstrumentBarItemElement : ElementBaseWithOpaqueBack
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

        public InstrumentBarItemElement(
            Element parent,
            ToolTipElement toolTipElement,
            UnderlyingPictureWrapper underlyingPictureWrapper,
            ITextMeasurer textMeasurer)
            : base(parent)
        {
            if (underlyingPictureWrapper == null) throw new ArgumentNullException(nameof(underlyingPictureWrapper));
            _textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

            _label = CreateLabel(_backRectangle);

            _pictureButtonDelete = new PictureButtonElement(
                _backRectangle,
                underlyingPictureWrapper.PictureDelete,
                CommonResourceFormatter.Delete,
                toolTipElement);

            _pictureButtonDelete.MouseDown += _pictureButtonDelete_MouseDown;
            _pictureButtonDelete.PictureStyle = StyleHelper.IconPictureStyleLight;

            _pictureButtonExpand = new PictureButtonElement(
                _backRectangle,
                underlyingPictureWrapper.PictureExpand,
                CommonResourceFormatter.Open,
                toolTipElement);

            _pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;
            _pictureButtonExpand.PictureStyle = StyleHelper.IconPictureStyleLight;

            _pictureButtonMoveBackward = new PictureButtonElement(
                _backRectangle,
                underlyingPictureWrapper.PictureMoveBackward,
                CommonResourceFormatter.Move,
                toolTipElement);

            _pictureButtonMoveBackward.MouseDown += _pictureButtonMoveBackward_MouseDown;
            _pictureButtonMoveBackward.PictureStyle = StyleHelper.IconPictureStyleLight;

            _pictureButtonMoveForward = new PictureButtonElement(
                _backRectangle,
                underlyingPictureWrapper.PictureMoveForward,
                CommonResourceFormatter.Move,
                toolTipElement);

            _pictureButtonMoveForward.MouseDown += _pictureButtonMoveForward_MouseDown;
            _pictureButtonMoveForward.PictureStyle = StyleHelper.IconPictureStyleLight;

            _pictureButtonPlay = new PictureButtonElement(
                _backRectangle,
                underlyingPictureWrapper.PicturePlay,
                ResourceFormatter.Play,
                toolTipElement);

            _pictureButtonPlay.MouseDown += _pictureButtonPlay_MouseDown;
            _pictureButtonPlay.PictureStyle = StyleHelper.IconPictureStyleLight;
#if DEBUG
            _pictureButtonDelete.Tag = nameof(_pictureButtonDelete);
            _pictureButtonExpand.Tag = nameof(_pictureButtonExpand);
            _pictureButtonMoveBackward.Tag = nameof(_pictureButtonMoveBackward);
            _pictureButtonMoveForward.Tag = nameof(_pictureButtonMoveForward);
            _pictureButtonPlay.Tag = nameof(_pictureButtonPlay);
#endif
        }

        private InstrumentItemViewModel _viewModel;

        public InstrumentItemViewModel ViewModel
        {
            // ReSharper disable once MemberCanBePrivate.Global
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

        public override void PositionElements()
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
                x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.SPACING_SMALL;
            }

            if (_viewModel.CanPlay)
            {
                _pictureButtonPlay.Position.X = x;
                x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.SPACING_SMALL;
            }

            if (_viewModel.CanExpand)
            {
                _pictureButtonExpand.Position.X = x;
                x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.SPACING_SMALL;
            }

            if (_viewModel.CanDelete)
            {
                _pictureButtonDelete.Position.X = x;
                x += StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.SPACING_SMALL;
            }

            x -= StyleHelper.SPACING_SMALL;

            Position.Width = x;
            Position.Height = StyleHelper.SPACING_SMALL + StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.SPACING_SMALL;

            _pictureButtonDelete.PositionElements();
            _pictureButtonMoveBackward.PositionElements();
            _pictureButtonMoveForward.PositionElements();
            _pictureButtonPlay.PositionElements();
            _pictureButtonExpand.PositionElements();

            base.PositionElements();
        }

        private Label CreateLabel(Element parent)
        {
            var label = new Label(parent)
            {
                TextStyle = StyleHelper.TitleTextStyle
            };

            label.Position.Height = StyleHelper.ROW_HEIGHT;

            return label;
        }

        private void _pictureButtonDelete_MouseDown(object sender, EventArgs e) => DeleteRequested(this, new EventArgs<int>(_viewModel.EntityID));
        private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(this, new EventArgs<int>(_viewModel.EntityID));

        private void _pictureButtonMoveBackward_MouseDown(object sender, EventArgs e)
            => MoveBackwardRequested(this, new EventArgs<int>(_viewModel.EntityID));

        private void _pictureButtonMoveForward_MouseDown(object sender, EventArgs e)
            => MoveForwardRequested(this, new EventArgs<int>(_viewModel.EntityID));

        private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayRequested(this, new EventArgs<int>(_viewModel.EntityID));
    }
}