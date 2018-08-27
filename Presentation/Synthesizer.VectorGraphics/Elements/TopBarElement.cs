using System;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public class TopBarElement : ElementBase
    {
        private readonly TopButtonBarElement _topButtonBarElement;
        private readonly InstrumentBarElement _instrumentBarElement;
        private readonly PictureButtonElement _pictureButtonDocumentClose;

        public TopBarElement(Element parent, UnderlyingPictureWrapper underlyingPictureWrapper, ITextMeasurer textMeasurer)
            : base(parent)
        {
            var toolTipElement = new ToolTipElement(
                parent?.Diagram?.Background,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                textMeasurer);

            _topButtonBarElement = new TopButtonBarElement(this, toolTipElement, underlyingPictureWrapper);
            _instrumentBarElement = new InstrumentBarElement(this, underlyingPictureWrapper, textMeasurer);
            _pictureButtonDocumentClose = new PictureButtonElement(this, underlyingPictureWrapper.PictureClose, CommonResourceFormatter.Close, toolTipElement);
        }

        public void PositionElements()
        {
            _topButtonBarElement.PositionElements();

            _pictureButtonDocumentClose.Position.Right = Position.Width - StyleHelper.SPACING_SMALL;

            _instrumentBarElement.Position.Width = _pictureButtonDocumentClose.Position.X -
                                                   _topButtonBarElement.MaxWidth -
                                                   StyleHelper.SPACING_SMALL_TIMES_2;

            _instrumentBarElement.Position.X = _topButtonBarElement.MaxWidth + StyleHelper.SPACING_SMALL;
            _instrumentBarElement.PositionElements();

            Position.Height = new[] { _instrumentBarElement.Position.Height, _pictureButtonDocumentClose.Position.Height, _topButtonBarElement.Position.Height }.Max();
        }

        public TopButtonBarViewModel TopButtonBarViewModel
        {
            get => _topButtonBarElement.ViewModel;
            set => _topButtonBarElement.ViewModel = value;
        }

        public InstrumentBarViewModel InstrumentBarViewModel
        {
            get => _instrumentBarElement.ViewModel;
            set => _instrumentBarElement.ViewModel = value;
        }

        // Events

        public event EventHandler HeightChanged
        {
            add => _instrumentBarElement.HeightChanged += value;
            remove => _instrumentBarElement.HeightChanged -= value;
        }

        public event EventHandler InstrumentBar_ExpandRequested
        {
            add => _instrumentBarElement.ExpandRequested += value;
            remove => _instrumentBarElement.ExpandRequested -= value;
        }

        public event EventHandler InstrumentBar_PlayRequested
        {
            add => _instrumentBarElement.PlayRequested += value;
            remove => _instrumentBarElement.PlayRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_ExpandPatchRequested
        {
            add => _instrumentBarElement.ExpandPatchRequested += value;
            remove => _instrumentBarElement.ExpandPatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MovePatchBackwardRequested
        {
            add => _instrumentBarElement.MovePatchBackwardRequested += value;
            remove => _instrumentBarElement.MovePatchBackwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MovePatchForwardRequested
        {
            add => _instrumentBarElement.MovePatchForwardRequested += value;
            remove => _instrumentBarElement.MovePatchForwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_PlayPatchRequested
        {
            add => _instrumentBarElement.PlayPatchRequested += value;
            remove => _instrumentBarElement.PlayPatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_DeletePatchRequested
        {
            add => _instrumentBarElement.DeletePatchRequested += value;
            remove => _instrumentBarElement.DeletePatchRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_ExpandMidiMappingGroupRequested
        {
            add => _instrumentBarElement.ExpandMidiMappingGroupRequested += value;
            remove => _instrumentBarElement.ExpandMidiMappingGroupRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MoveMidiMappingGroupBackwardRequested
        {
            add => _instrumentBarElement.MoveMidiMappingGroupBackwardRequested += value;
            remove => _instrumentBarElement.MoveMidiMappingGroupBackwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_MoveMidiMappingGroupForwardRequested
        {
            add => _instrumentBarElement.MoveMidiMappingGroupForwardRequested += value;
            remove => _instrumentBarElement.MoveMidiMappingGroupForwardRequested -= value;
        }

        public event EventHandler<EventArgs<int>> InstrumentBar_DeleteMidiMappingGroupRequested
        {
            add => _instrumentBarElement.DeleteMidiMappingGroupRequested += value;
            remove => _instrumentBarElement.DeleteMidiMappingGroupRequested -= value;
        }

        public event EventHandler TopButtonBar_AddToInstrumentRequested
        {
            add => _topButtonBarElement.AddToInstrumentRequested += value;
            remove => _topButtonBarElement.AddToInstrumentRequested -= value;
        }

        public event EventHandler TopButtonBar_CloseRequested
        {
            add => _topButtonBarElement.CloseRequested += value;
            remove => _topButtonBarElement.CloseRequested -= value;
        }

        public event EventHandler TopButtonBar_DeleteRequested
        {
            add => _topButtonBarElement.DeleteRequested += value;
            remove => _topButtonBarElement.DeleteRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentGridShowRequested
        {
            add => _topButtonBarElement.DocumentGridShowRequested += value;
            remove => _topButtonBarElement.DocumentGridShowRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentPropertiesShowRequested
        {
            add => _topButtonBarElement.DocumentPropertiesShowRequested += value;
            remove => _topButtonBarElement.DocumentPropertiesShowRequested -= value;
        }

        public event EventHandler TopButtonBar_DocumentTreeShowOrCloseRequested
        {
            add => _topButtonBarElement.DocumentTreeShowOrCloseRequested += value;
            remove => _topButtonBarElement.DocumentTreeShowOrCloseRequested -= value;
        }

        public event EventHandler TopButtonBar_NewRequested
        {
            add => _topButtonBarElement.NewRequested += value;
            remove => _topButtonBarElement.NewRequested -= value;
        }

        public event EventHandler TopButtonBar_OpenItemExternallyRequested
        {
            add => _topButtonBarElement.OpenItemExternallyRequested += value;
            remove => _topButtonBarElement.OpenItemExternallyRequested -= value;
        }

        public event EventHandler TopButtonBar_PlayRequested
        {
            add => _topButtonBarElement.PlayRequested += value;
            remove => _topButtonBarElement.PlayRequested -= value;
        }

        public event EventHandler TopButtonBar_RefreshRequested
        {
            add => _topButtonBarElement.RefreshRequested += value;
            remove => _topButtonBarElement.RefreshRequested -= value;
        }

        public event EventHandler TopButtonBar_RedoRequested
        {
            add => _topButtonBarElement.RedoRequested += value;
            remove => _topButtonBarElement.RedoRequested -= value;
        }

        public event EventHandler TopButtonBar_SaveRequested
        {
            add => _topButtonBarElement.SaveRequested += value;
            remove => _topButtonBarElement.SaveRequested -= value;
        }

        public event EventHandler TopButtonBar_UndoRequested
        {
            add => _topButtonBarElement.UndoRequested += value;
            remove => _topButtonBarElement.UndoRequested -= value;
        }
    }
}