using System;
using JJ.Framework.Common;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public class InstrumentBarElement : ElementWithScreenViewModelBase
	{
		private readonly InstrumentBarScaleElement _scaleElement;
		private readonly InstrumentBarItemsElement _midiMappingsElement;
		private readonly InstrumentBarItemsElement _patchesElement;
		private readonly InstrumentBarButtonsElement _buttonsElement;

		public InstrumentBarElement(
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

			_scaleElement = new InstrumentBarScaleElement(this, textMeasurer);

			_midiMappingsElement = new InstrumentBarItemsElement(
				this,
				HorizontalAlignmentEnum.Left,
				toolTipElement,
				underlyingPictureDelete,
				underlyingPictureExpand,
				underlyingPictureMoveBackward,
				underlyingPictureMoveForward,
				underlyingPicturePlay,
				textMeasurer);

			_patchesElement = new InstrumentBarItemsElement(
				this,
				HorizontalAlignmentEnum.Right,
				toolTipElement,
				underlyingPictureDelete,
				underlyingPictureExpand,
				underlyingPictureMoveBackward,
				underlyingPictureMoveForward,
				underlyingPicturePlay,
				textMeasurer);

			_buttonsElement = new InstrumentBarButtonsElement(this, toolTipElement, underlyingPictureExpand, underlyingPicturePlay);
		}

		public new InstrumentBarViewModel ViewModel
		{
			get => (InstrumentBarViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		public override void PositionElements()
		{
			_scaleElement.PositionElements();

			_buttonsElement.Position.X = Position.Width - _buttonsElement.Position.Width - StyleHelper.SPACING_SMALL;

			float remainingWidth = _buttonsElement.Position.X - _scaleElement.Position.Width - StyleHelper.SPACING * 3;

			float midiMappingsTotalWidth = _midiMappingsElement.GetTotalItemsWidth();
			float patchesTotalWidth = _patchesElement.GetTotalItemsWidth();
			float patchesAndMidiMappingsTotalWidth = midiMappingsTotalWidth + patchesTotalWidth;
			float midiMappingsFraction = midiMappingsTotalWidth / patchesAndMidiMappingsTotalWidth;
			float patchesFraction = patchesTotalWidth / patchesAndMidiMappingsTotalWidth;

			float midiMappingsFractionOfRemainingWidth = midiMappingsTotalWidth / remainingWidth;
			float patchesFractionOfRemainingWidth = patchesTotalWidth / remainingWidth;
			bool midiMappingGroupsAreWithinHalfTheRemainingWidth = midiMappingsFractionOfRemainingWidth <= 0.5;
			bool patchesAreWithinHalfTheRemainingWidth = patchesFractionOfRemainingWidth <= 0.5;

			float midiMappingWidth;
			float patchesWidth;
			if (!patchesAreWithinHalfTheRemainingWidth && !midiMappingGroupsAreWithinHalfTheRemainingWidth)
			{
				// Divide space fairly over MidiMappingGroups and Patches.
				midiMappingWidth = remainingWidth * midiMappingsFraction;
				patchesWidth = remainingWidth * patchesFraction;
			}
			else if (patchesAreWithinHalfTheRemainingWidth && !midiMappingGroupsAreWithinHalfTheRemainingWidth)
			{
				// Let Patches use all the width they needs.
				patchesWidth = patchesTotalWidth;
				midiMappingWidth = remainingWidth - patchesWidth;
			}
			else if (!patchesAreWithinHalfTheRemainingWidth && midiMappingGroupsAreWithinHalfTheRemainingWidth)
			{
				// Let MidiMappingGroups use all the width they needs.
				midiMappingWidth = midiMappingsTotalWidth;
				patchesWidth = remainingWidth - midiMappingsTotalWidth;
			}
			else if (patchesAreWithinHalfTheRemainingWidth && midiMappingGroupsAreWithinHalfTheRemainingWidth)
			{
				// Divide space fairly over MidiMappingGroups and Patches.
				midiMappingWidth = remainingWidth * midiMappingsFraction;
				patchesWidth = remainingWidth * patchesFraction;
			}
			else
			{
				throw new Exception(
					$"Error evaluating {new { patchesAreWithinHalfTheWidth = patchesAreWithinHalfTheRemainingWidth, midiMappingGroupsAreWithinHalfTheWidth = midiMappingGroupsAreWithinHalfTheRemainingWidth }}. All cases should have been covered, but somehow they were not.");
			}
			// ReSharper restore ConditionIsAlwaysTrueOrFalse

			_midiMappingsElement.Position.X = _scaleElement.Position.Right + StyleHelper.SPACING;
			_midiMappingsElement.Position.Width = midiMappingWidth;

			_patchesElement.Position.X = _midiMappingsElement.Position.Right + StyleHelper.SPACING;
			_patchesElement.Position.Width = patchesWidth;

			_midiMappingsElement.PositionElements();
			_patchesElement.PositionElements();

			Position.Height = Math.Max(_midiMappingsElement.Position.Height, _patchesElement.Position.Height);
		}

		protected override void ApplyViewModelToElements()
		{
			_scaleElement.ViewModel = ViewModel.Scale;
			_patchesElement.ViewModels = ViewModel.Patches;
			_midiMappingsElement.ViewModels = ViewModel.MidiMappingGroups;
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

		public event EventHandler<EventArgs<int>> ExpandMidiMappingGroupRequested
		{
			add => _midiMappingsElement.ExpandRequested += value;
			remove => _midiMappingsElement.ExpandRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MoveMidiMappingGroupBackwardRequested
		{
			add => _midiMappingsElement.MoveBackwardRequested += value;
			remove => _midiMappingsElement.MoveBackwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> MoveMidiMappingGroupForwardRequested
		{
			add => _midiMappingsElement.MoveForwardRequested += value;
			remove => _midiMappingsElement.MoveForwardRequested -= value;
		}

		public event EventHandler<EventArgs<int>> DeleteMidiMappingGroupRequested
		{
			add => _midiMappingsElement.DeleteRequested += value;
			remove => _midiMappingsElement.DeleteRequested -= value;
		}
	}
}