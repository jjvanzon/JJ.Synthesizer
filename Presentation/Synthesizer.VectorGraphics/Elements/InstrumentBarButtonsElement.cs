using System;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.ResourceStrings;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    internal class InstrumentBarButtonsElement : ElementBaseWithOpaqueBack
    {
        public event EventHandler ExpandRequested;
        public event EventHandler PlayRequested;

        private readonly PictureButtonElement _pictureButtonPlay;
        private readonly PictureButtonElement _pictureButtonExpand;

        public InstrumentBarButtonsElement(
            Element parent,
            ToolTipElement toolTipElement,
            object underlyingPictureExpand,
            object underlyingPicturePlay)
            : base(parent)
        {
            _pictureButtonExpand = new PictureButtonElement(this, underlyingPictureExpand, CommonResourceFormatter.Open, toolTipElement);
            _pictureButtonExpand.MouseDown += _pictureButtonExpand_MouseDown;
            _pictureButtonExpand.Position.X = StyleHelper.SPACING_SMALL + StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;

            _pictureButtonPlay = new PictureButtonElement(this, underlyingPicturePlay, ResourceFormatter.Play, toolTipElement);
            _pictureButtonPlay.MouseDown += _pictureButtonPlay_MouseDown;

            Position.Width = StyleHelper.PICTURE_BUTTON_PICTURE_SIZE +
                             StyleHelper.SPACING_SMALL +
                             StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;

            Position.Height = StyleHelper.ROW_HEIGHT;

            PositionElements();
        }

        private InstrumentBarViewModel _viewModel;

        public InstrumentBarViewModel ViewModel
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
            _pictureButtonPlay.Visible = ViewModel.CanPlay;
            _pictureButtonExpand.Visible = ViewModel.CanExpand;
        }

        public override void PositionElements()
        {
            base.PositionElements();

            _pictureButtonExpand.PositionElements();
            _pictureButtonPlay.PositionElements();
        }

        private void _pictureButtonExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(sender, EventArgs.Empty);
        private void _pictureButtonPlay_MouseDown(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
    }
}