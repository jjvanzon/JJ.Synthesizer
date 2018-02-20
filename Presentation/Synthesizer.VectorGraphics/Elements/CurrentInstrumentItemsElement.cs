using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class CurrentInstrumentItemsElement : ElementBase
	{
		public event EventHandler<EventArgs<int>> ExpandRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler<EventArgs<int>> PlayRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		private readonly ToolTipElement _toolTipElement;
		private readonly IList<CurrentInstrumentItemElement> _itemElements = new List<CurrentInstrumentItemElement>();
		private readonly object _underlyingPictureDelete;
		private readonly object _underlyingPictureExpand;
		private readonly object _underlyingPictureMoveBackward;
		private readonly object _underlyingPictureMoveForward;
		private readonly object _underlyingPicturePlay;
		private readonly ITextMeasurer _textMeasurer;

		public CurrentInstrumentItemsElement(
			Element parent,
			ToolTipElement toolTipElement,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer)
			: base(parent)
		{
			_toolTipElement = toolTipElement ?? throw new ArgumentNullException(nameof(toolTipElement));
			_underlyingPictureDelete = underlyingPictureDelete ?? throw new ArgumentNullException(nameof(underlyingPictureDelete));
			_underlyingPictureExpand = underlyingPictureExpand ?? throw new ArgumentNullException(nameof(underlyingPictureExpand));
			_underlyingPictureMoveBackward = underlyingPictureMoveBackward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveBackward));
			_underlyingPictureMoveForward = underlyingPictureMoveForward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveForward));
			_underlyingPicturePlay = underlyingPicturePlay ?? throw new ArgumentNullException(nameof(underlyingPicturePlay));
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));
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
				CurrentInstrumentItemElement patchElement = _itemElements[i];
				patchElement.ViewModel = patchViewModel;
			}

			// Insert
			for (int i = _itemElements.Count; i < ViewModels.Count; i++)
			{
				CurrentInstrumentItemViewModel patchViewModel = ViewModels[i];
				CurrentInstrumentItemElement patchElement = CreateItemElement(patchViewModel);

				_itemElements.Add(patchElement);
			}

			// Delete
			for (int i = _itemElements.Count - 1; i >= ViewModels.Count; i--)
			{
				CurrentInstrumentItemElement patchElement = _itemElements[i];
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
			float x = Position.Width;

			foreach (CurrentInstrumentItemElement itemElement in _itemElements.Reverse())
			{
				itemElement.PositionElements();

				x -= StyleHelper.SMALL_SPACING;
				x -= StyleHelper.SMALL_SPACING;
				x -= itemElement.Position.Width;

				itemElement.Position.X = x;
			}

			Position.Height = StyleHelper.TITLE_BAR_HEIGHT;
		}

		private CurrentInstrumentItemElement CreateItemElement(CurrentInstrumentItemViewModel itemViewModel)
		{
			var itemElement = new CurrentInstrumentItemElement(
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
		private void patchElement_DeleteRequested(object sender, EventArgs<int> e) => DeleteRequested(sender, e);
	}
}