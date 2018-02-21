using System;
using JJ.Framework.Common;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public class CurrentInstrumentElement : ElementWithScreenViewModelBase
	{
		private readonly CurrentInstrumentScaleElement _scaleElement;
		private readonly CurrentInstrumentItemsElement _midiMappingElementsElement;
		private readonly CurrentInstrumentItemsElement _patchesElement;
		private readonly CurrentInstrumentButtonsElement _buttonsElement;

		public CurrentInstrumentElement(
			Element parent,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer) : base(parent)
		{
			var toolTipElement = new ToolTipElement(
				Diagram.Background,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				textMeasurer);

			_scaleElement = new CurrentInstrumentScaleElement(this, textMeasurer);

			_midiMappingElementsElement = new CurrentInstrumentItemsElement(
				this,
				HorizontalAlignmentEnum.Left,
				toolTipElement,
				underlyingPictureDelete,
				underlyingPictureExpand,
				underlyingPictureMoveBackward,
				underlyingPictureMoveForward,
				underlyingPicturePlay,
				textMeasurer);

			_patchesElement = new CurrentInstrumentItemsElement(
				this,
				HorizontalAlignmentEnum.Right,
				toolTipElement,
				underlyingPictureDelete,
				underlyingPictureExpand,
				underlyingPictureMoveBackward,
				underlyingPictureMoveForward,
				underlyingPicturePlay,
				textMeasurer);

			_buttonsElement = new CurrentInstrumentButtonsElement(this, toolTipElement, underlyingPictureExpand, underlyingPicturePlay);
		}

		public new CurrentInstrumentViewModel ViewModel
		{
			get => (CurrentInstrumentViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		public override void PositionElements()
		{
			_scaleElement.PositionElements();

			_buttonsElement.Position.X = Position.Width - _buttonsElement.Position.Width - StyleHelper.SMALL_SPACING;
			
			float remainingWidth = _buttonsElement.Position.X - _scaleElement.Position.Width - StyleHelper.SPACING * 3;
			float halfRemainingWidth = remainingWidth / 2f;

			_midiMappingElementsElement.Position.X = _scaleElement.Position.Right + StyleHelper.SPACING;
			_midiMappingElementsElement.Position.Width = halfRemainingWidth;

			_patchesElement.Position.X = _midiMappingElementsElement.Position.Right + StyleHelper.SPACING;
			_patchesElement.Position.Width = halfRemainingWidth;

			_patchesElement.PositionElements();
			_midiMappingElementsElement.PositionElements();
		}

		protected override void ApplyViewModelToElements()
		{
			_scaleElement.ViewModel = ViewModel.Scale;
			_patchesElement.ViewModels = ViewModel.Patches;
			_midiMappingElementsElement.ViewModels = ViewModel.MidiMappingElements;
			_buttonsElement.ViewModel = ViewModel;
		}

		public event EventHandler ExpandRequested
		{
			add => _buttonsElement.ExpandRequested += value;
			remove => _buttonsElement.ExpandRequested -= value;
		}

		public event EventHandler PlayRequested
		{
			add => _buttonsElement.PlayRequested += value;
			remove => _buttonsElement.PlayRequested -= value;
		}

		public event EventHandler<EventArgs<int>> ExpandPatchRequested
		{
			add => _patchesElement.ExpandRequested += value;
			remove => _patchesElement.ExpandRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MovePatchBackwardRequested
		{
			add => _patchesElement.MoveBackwardRequested += value;
			remove => _patchesElement.MoveBackwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MovePatchForwardRequested
		{
			add => _patchesElement.MoveForwardRequested += value;
			remove => _patchesElement.MoveForwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> PlayPatchRequested
		{
			add => _patchesElement.PlayRequested += value;
			remove => _patchesElement.PlayRequested -= value;
		}

		public event EventHandler<EventArgs<int>> DeletePatchRequested
		{
			add => _patchesElement.DeleteRequested += value;
			remove => _patchesElement.DeleteRequested -= value;
		}

		public event EventHandler<EventArgs<int>> ExpandMidiMappingElementRequested
		{
			add => _midiMappingElementsElement.ExpandRequested += value;
			remove => _midiMappingElementsElement.ExpandRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MoveMidiMappingElementBackwardRequested
		{
			add => _midiMappingElementsElement.MoveBackwardRequested += value;
			remove => _midiMappingElementsElement.MoveBackwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MoveMidiMappingElementForwardRequested
		{
			add => _midiMappingElementsElement.MoveForwardRequested += value;
			remove => _midiMappingElementsElement.MoveForwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> PlayMidiMappingElementRequested
		{
			add => _midiMappingElementsElement.PlayRequested += value;
			remove => _midiMappingElementsElement.PlayRequested -= value;
		}

		public event EventHandler<EventArgs<int>> DeleteMidiMappingElementRequested
		{
			add => _midiMappingElementsElement.DeleteRequested += value;
			remove => _midiMappingElementsElement.DeleteRequested -= value;
		}
	}
}