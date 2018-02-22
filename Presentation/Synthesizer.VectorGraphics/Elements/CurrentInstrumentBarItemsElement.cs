using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable VirtualMemberCallInConstructor

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class CurrentInstrumentBarItemsElement : ElementBase
	{
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		private readonly HorizontalAlignmentEnum _horizontalAlignmentEnum;
		private readonly ToolTipElement _toolTipElement;
		private readonly IList<CurrentInstrumentBarItemElement> _itemElements = new List<CurrentInstrumentBarItemElement>();
		private readonly object _underlyingPictureDelete;
		private readonly object _underlyingPictureExpand;
		private readonly object _underlyingPictureMoveBackward;
		private readonly object _underlyingPictureMoveForward;
		private readonly object _underlyingPicturePlay;
		private readonly ITextMeasurer _textMeasurer;

		public CurrentInstrumentBarItemsElement(
			Element parent,
			HorizontalAlignmentEnum horizontalAlignmentEnum,
			ToolTipElement toolTipElement,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer)
			: base(parent)
		{
			_horizontalAlignmentEnum = horizontalAlignmentEnum;
			_toolTipElement = toolTipElement ?? throw new ArgumentNullException(nameof(toolTipElement));
			_underlyingPictureDelete = underlyingPictureDelete ?? throw new ArgumentNullException(nameof(underlyingPictureDelete));
			_underlyingPictureExpand = underlyingPictureExpand ?? throw new ArgumentNullException(nameof(underlyingPictureExpand));
			_underlyingPictureMoveBackward = underlyingPictureMoveBackward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveBackward));
			_underlyingPictureMoveForward = underlyingPictureMoveForward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveForward));
			_underlyingPicturePlay = underlyingPicturePlay ?? throw new ArgumentNullException(nameof(underlyingPicturePlay));
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			Position.Height = StyleHelper.TITLE_BAR_HEIGHT;
		}

		private IList<CurrentInstrumentItemViewModel> _viewModels;

		public IList<CurrentInstrumentItemViewModel> ViewModels
		{
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
				CurrentInstrumentItemViewModel patchViewModel = ViewModels[i];
				CurrentInstrumentBarItemElement patchElement = _itemElements[i];
				patchElement.ViewModel = patchViewModel;
			}

			// Insert
			for (int i = _itemElements.Count; i < ViewModels.Count; i++)
			{
				CurrentInstrumentItemViewModel patchViewModel = ViewModels[i];
				CurrentInstrumentBarItemElement patchElement = CreateItemElement(patchViewModel);

				_itemElements.Add(patchElement);
			}

			// Delete
			for (int i = _itemElements.Count - 1; i >= ViewModels.Count; i--)
			{
				CurrentInstrumentBarItemElement patchElement = _itemElements[i];
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

		public void PositionElements()
		{
			switch (_horizontalAlignmentEnum)
			{
				case HorizontalAlignmentEnum.Right:
					PositionElementsRightAligned();
					break;

				case HorizontalAlignmentEnum.Left:
					PositionElementsLeftAligned();
					break;

				default:
					throw new ValueNotSupportedException(_horizontalAlignmentEnum);

			}
		}

		private void PositionElementsRightAligned()
		{
			float x = Position.Width;

			foreach (CurrentInstrumentBarItemElement itemElement in _itemElements.Reverse())
			{
				itemElement.PositionElements();

				x -= itemElement.Position.Width;

				itemElement.Position.X = x;

				x -= StyleHelper.SPACING;
			}
		}

		private void PositionElementsLeftAligned()
		{
			float x = 0;

			foreach (CurrentInstrumentBarItemElement itemElement in _itemElements)
			{
				itemElement.PositionElements();

				itemElement.Position.X = x;

				x += itemElement.Position.Width + StyleHelper.SPACING;
			}
		}

		private CurrentInstrumentBarItemElement CreateItemElement(CurrentInstrumentItemViewModel itemViewModel)
		{
			var itemElement = new CurrentInstrumentBarItemElement(
				this,
				_toolTipElement,
				_underlyingPictureDelete,
				_underlyingPictureExpand,
				_underlyingPictureMoveBackward,
				_underlyingPictureMoveForward,
				_underlyingPicturePlay,
				_textMeasurer)
			{
				ViewModel = itemViewModel
			};
			itemElement.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

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

			// Hide ToolTipElement. so that it does not jump to the delete button of another Patch or MidiMappingElement.
			// (ItemElements get redistributed over the entities.)
			_toolTipElement.Visible = false;
		}
	}
}