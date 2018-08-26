using System;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public class TopBarElement : ElementBase
    {
        private readonly ButtonBarElement _buttonBarElement;
        private readonly InstrumentBarElement _instrumentBarElement;
        private readonly PictureButtonElement _pictureButtonDocumentClose;

        public TopBarElement(
            Element parent,
            ITextMeasurer textMeasurer,
            object underlyingPictureAdd,
            object underlyingPictureAddToInstrument,
            object underlyingPictureBrowse,
            object underlyingPictureClose,
            object underlyingPictureDelete,
            object underlyingPictureExpand,
            object underlyingPictureMoveBackward,
            object underlyingPictureMoveForward,
            object underlyingPictureNew,
            object underlyingPicturePlay,
            object underlyingPictureRedo,
            object underlyingPictureRefresh,
            object underlyingPictureRename,
            object underlyingPictureSave,
            object underlyingPictureTreeStructure,
            object underlyingPictureUndo)
            : base(parent)
        {
            var toolTipElement = new ToolTipElement(
                parent?.Diagram?.Background,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                textMeasurer);

            _buttonBarElement = new ButtonBarElement(
                this,
                toolTipElement,
                underlyingPictureAdd,
                underlyingPictureAddToInstrument,
                underlyingPictureBrowse,
                underlyingPictureClose,
                underlyingPictureDelete,
                underlyingPictureExpand,
                underlyingPictureNew,
                underlyingPicturePlay,
                underlyingPictureRedo,
                underlyingPictureRefresh,
                underlyingPictureRename,
                underlyingPictureSave,
                underlyingPictureTreeStructure,
                underlyingPictureUndo);

            _instrumentBarElement = new InstrumentBarElement(
                this,
                underlyingPictureDelete,
                underlyingPictureExpand,
                underlyingPictureMoveBackward,
                underlyingPictureMoveForward,
                underlyingPicturePlay,
                textMeasurer);

            _pictureButtonDocumentClose = new PictureButtonElement(this, underlyingPictureClose, CommonResourceFormatter.Close, toolTipElement);
        }

        public void PositionElements()
        {
            _pictureButtonDocumentClose.Position.Right = Position.Width;

            _instrumentBarElement.Position.Width = _pictureButtonDocumentClose.Position.X -
                                                   _buttonBarElement.MaxWidth -
                                                   StyleHelper.SPACING_SMALL_TIMES_2;
            _instrumentBarElement.Position.X = _buttonBarElement.MaxWidth + StyleHelper.SPACING_SMALL;
            _instrumentBarElement.PositionElements();
        }
    }
}