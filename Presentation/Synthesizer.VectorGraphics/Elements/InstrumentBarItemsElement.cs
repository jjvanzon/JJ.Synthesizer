using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Positioners;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class InstrumentBarItemsElement : ElementBase
	{
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		private readonly HorizontalAlignmentEnum _horizontalAlignmentEnum;
		private readonly ToolTipElement _toolTipElement;
		private readonly IList<InstrumentBarItemElement> _itemElements = new List<InstrumentBarItemElement>();
	    private readonly UnderlyingPictureWrapper _underlyingPictureWrapper;
		private readonly ITextMeasurer _textMeasurer;

		public InstrumentBarItemsElement(
			Element parent,
			HorizontalAlignmentEnum horizontalAlignmentEnum,
			ToolTipElement toolTipElement,
			UnderlyingPictureWrapper underlyingPictureWrapper,
			ITextMeasurer textMeasurer)
			: base(parent)
		{
			_horizontalAlignmentEnum = horizontalAlignmentEnum;
			_toolTipElement = toolTipElement ?? throw new ArgumentNullException(nameof(toolTipElement));
		    _underlyingPictureWrapper = underlyingPictureWrapper ?? throw new ArgumentNullException(nameof(underlyingPictureWrapper));
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			Position.Height = StyleHelper.ROW_HEIGHT;
		}

		private IList<InstrumentItemViewModel> _viewModels;

		public IList<InstrumentItemViewModel> ViewModels
		{
			// ReSharper disable once MemberCanBePrivate.Global
			get => _viewModels;
			set
			{
				_viewModels = value ?? throw new ArgumentNullException(nameof(ViewModels));
				ApplyViewModelToElements();
			}
		}

		private void ApplyViewModelToElements()
		{
			// Update
			int minCount = Math.Min(_itemElements.Count, ViewModels.Count);
			for (int i = 0; i < minCount; i++)
			{
				InstrumentItemViewModel patchViewModel = ViewModels[i];
				InstrumentBarItemElement patchElement = _itemElements[i];
				patchElement.ViewModel = patchViewModel;
			}

			// Insert
			for (int i = _itemElements.Count; i < ViewModels.Count; i++)
			{
				InstrumentItemViewModel patchViewModel = ViewModels[i];
				InstrumentBarItemElement patchElement = CreateItemElement(patchViewModel);

				_itemElements.Add(patchElement);
			}

			// Delete
			for (int i = _itemElements.Count - 1; i >= ViewModels.Count; i--)
			{
				InstrumentBarItemElement patchElement = _itemElements[i];
				patchElement.ExpandRequested -= patchElement_ExpandRequested;
				patchElement.MoveBackwardRequested -= patchElement_MoveBackwardRequested;
				patchElement.MoveForwardRequested -= patchElement_MoveForwardRequested;
				patchElement.PlayRequested -= patchElement_PlayRequested;
				patchElement.DeleteRequested -= patchElement_DeleteRequested;
				patchElement.Dispose();

				_itemElements.RemoveAt(i);
			}

			PositionElements();
		}

		/// <summary>
		/// The total width of all the items added together + the spacings between them,
		/// if they were all to be put on one line.
		/// </summary>
		public float GetTotalItemsWidth()
		{
			foreach (InstrumentBarItemElement element in _itemElements)
			{
				element.PositionElements();
			}

			float totalItemsWidth = _itemElements.Sum(x => x.Position.Width) + (_itemElements.Count - 1) * StyleHelper.SPACING;
			return totalItemsWidth;
		}

		/// <summary> Set the width beforehand. The height will be set automatically. </summary>
		public void PositionElements()
		{
			foreach (InstrumentBarItemElement element in _itemElements)
			{
				element.PositionElements();
			}

			IList<float> itemWidths = _itemElements.Select(x => x.Position.Width).ToArray();

			switch (_horizontalAlignmentEnum)
			{
				case HorizontalAlignmentEnum.Right:
				{
					IPositioner positioner = new FlowPositionerRightAligned(
						Position.Width,
						StyleHelper.ROW_HEIGHT,
						StyleHelper.SPACING,
						0,
						itemWidths);

					positioner.Calculate(_itemElements);
					break;
				}

				case HorizontalAlignmentEnum.Left:
				{
					IPositioner positioner = new FlowPositionerLeftAligned(
						Position.Width,
						StyleHelper.ROW_HEIGHT,
						StyleHelper.SPACING,
						0,
						itemWidths);

					positioner.Calculate(_itemElements);
					break;
				}

				default:
					throw new ValueNotSupportedException(_horizontalAlignmentEnum);
			}

			Position.Height = _itemElements.Select(x => x.Position.Y).MaxOrDefault() + StyleHelper.ROW_HEIGHT;
		}

		private InstrumentBarItemElement CreateItemElement(InstrumentItemViewModel itemViewModel)
		{
			var itemElement = new InstrumentBarItemElement(
				this,
				_toolTipElement,
				_underlyingPictureWrapper,
				_textMeasurer)
			{
				ViewModel = itemViewModel
			};
			itemElement.Position.Height = StyleHelper.ROW_HEIGHT;

			itemElement.ExpandRequested += patchElement_ExpandRequested;
			itemElement.MoveBackwardRequested += patchElement_MoveBackwardRequested;
			itemElement.MoveForwardRequested += patchElement_MoveForwardRequested;
			itemElement.PlayRequested += patchElement_PlayRequested;
			itemElement.DeleteRequested += patchElement_DeleteRequested;

			return itemElement;
		}

		private void patchElement_ExpandRequested(object sender, EventArgs<int> e) => ExpandRequested(sender, e);
		private void patchElement_MoveBackwardRequested(object sender, EventArgs<int> e) => MoveBackwardRequested(sender, e);
		private void patchElement_MoveForwardRequested(object sender, EventArgs<int> e) => MoveForwardRequested(sender, e);
		private void patchElement_PlayRequested(object sender, EventArgs<int> e) => PlayRequested(sender, e);

		private void patchElement_DeleteRequested(object sender, EventArgs<int> e)
		{
			DeleteRequested(sender, e);

			// Hide ToolTipElement. so that it does not jump to the delete button of another Patch or MidiMappingGroup.
			// (ItemElements get redistributed over the entities.)
			_toolTipElement.Visible = false;
		}
	}
}