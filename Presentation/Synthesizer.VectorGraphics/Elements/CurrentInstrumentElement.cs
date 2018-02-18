using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public class CurrentInstrumentElement : ElementWithViewModelBase
	{
		private const float MARGIN_TOP = 4f;

		public event EventHandler ExpandRequested;
		public event EventHandler<EventArgs<int>> ExpandItemRequested;
		public event EventHandler<EventArgs<int>> MoveBackwardRequested;
		public event EventHandler<EventArgs<int>> MoveForwardRequested;
		public event EventHandler PlayRequested;
		public event EventHandler<EventArgs<int>> PlayItemRequested;
		public event EventHandler<EventArgs<int>> DeleteRequested;

		private readonly ITextMeasurer _textMeasurer;
		private readonly Picture _picturePlay;
		private readonly Picture _pictureExpand;
		private readonly ToolTipElement _toolTipElement;
		private readonly IList<CurrentInstrumentPatchElement> _patchElements = new List<CurrentInstrumentPatchElement>();
		private readonly object _underlyingPictureDelete;
		private readonly object _underlyingPictureExpand;
		private readonly object _underlyingPictureMoveBackward;
		private readonly object _underlyingPictureMoveForward;
		private readonly object _underlyingPicturePlay;

		public CurrentInstrumentElement(
			Element parent,
			object underlyingPictureDelete,
			object underlyingPictureExpand,
			object underlyingPictureMoveBackward,
			object underlyingPictureMoveForward,
			object underlyingPicturePlay,
			ITextMeasurer textMeasurer)
			: base(parent)
		{
			_underlyingPictureDelete = underlyingPictureDelete ?? throw new ArgumentNullException(nameof(underlyingPictureDelete));
			_underlyingPictureExpand = underlyingPictureExpand ?? throw new ArgumentNullException(nameof(underlyingPictureExpand));
			_underlyingPictureMoveBackward = underlyingPictureMoveBackward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveBackward));
			_underlyingPictureMoveForward = underlyingPictureMoveForward ?? throw new ArgumentNullException(nameof(underlyingPictureMoveForward));
			_underlyingPicturePlay = underlyingPicturePlay ?? throw new ArgumentNullException(nameof(underlyingPicturePlay));
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_toolTipElement = CreateToolTipElement();
			_pictureExpand = CreatePicture(underlyingPictureExpand, _toolTipElement, CommonResourceFormatter.Open, _pictureExpand_MouseDown);
			_picturePlay = CreatePicture(underlyingPicturePlay, _toolTipElement, ResourceFormatter.Play, _picturePlay_MouseDown);
		}

		public new CurrentInstrumentViewModel ViewModel
		{
			get => (CurrentInstrumentViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToElements()
		{
			_picturePlay.Visible = ViewModel.CanPlay;
			_pictureExpand.Visible = ViewModel.CanExpand;

			// Update
			int minCount = Math.Min(_patchElements.Count, ViewModel.Patches.Count);
			for (int i = 0; i < minCount; i++)
			{
				CurrentInstrumentPatchViewModel patchViewModel = ViewModel.Patches[i];
				CurrentInstrumentPatchElement patchElement = _patchElements[i];
				patchElement.ViewModel = patchViewModel;
			}

			// Insert
			for (int i = _patchElements.Count; i < ViewModel.Patches.Count; i++)
			{
				CurrentInstrumentPatchViewModel patchViewModel = ViewModel.Patches[i];
				CurrentInstrumentPatchElement patchElement = CreatePatchElement(patchViewModel);

				_patchElements.Add(patchElement);
			}

			// Delete
			for (int i = _patchElements.Count - 1; i >= ViewModel.Patches.Count; i--)
			{
				CurrentInstrumentPatchElement patchElement = _patchElements[i];
				patchElement.ExpandRequested -= patchElement_ExpandRequested;
				patchElement.MoveBackwardRequested -= patchElement_MoveBackwardRequested;
				patchElement.MoveForwardRequested -= patchElement_MoveForwardRequested;
				patchElement.PlayRequested -= patchElement_PlayRequested;
				patchElement.DeleteRequested -= patchElement_DeleteRequested;
				patchElement.Dispose();

				_patchElements.RemoveAt(i);
			}

			PositionElements();
		}

		public void PositionElements()
		{
			float x = Position.Width;

			x -= StyleHelper.ICON_SIZE;

			_pictureExpand.Position.X = x;

			x -= StyleHelper.SMALL_SPACING;
			x -= StyleHelper.ICON_SIZE;

			_picturePlay.Position.X = x;

			foreach (CurrentInstrumentPatchElement itemElement in _patchElements.Reverse())
			{
				x -= StyleHelper.SMALL_SPACING;
				x -= StyleHelper.SMALL_SPACING;
				x -= itemElement.Position.Width;

				itemElement.Position.X = x;
			}

			Position.Height = StyleHelper.TITLE_BAR_HEIGHT;
		}

		private CurrentInstrumentPatchElement CreatePatchElement(CurrentInstrumentPatchViewModel itemViewModel)
		{
			var itemElement = new CurrentInstrumentPatchElement(
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

		private Picture CreatePicture(object underlyingPicture, ToolTipElement toolTipElement, string toolTipText, EventHandler<MouseEventArgs> mouseDownHandler)
		{
			var picture = new Picture(this)
			{
				UnderlyingPicture = underlyingPicture
			};
			picture.Position.Width = StyleHelper.ICON_SIZE;
			picture.Position.Height = StyleHelper.ICON_SIZE;
			picture.Position.Y = MARGIN_TOP;

			var mouseDownGesture = new MouseDownGesture();
			mouseDownGesture.MouseDown += mouseDownHandler;
			picture.Gestures.Add(mouseDownGesture);

			var toolTipGesture = new ToolTipGesture(toolTipElement, toolTipText);
			picture.Gestures.Add(toolTipGesture);

			return picture;
		}

		private ToolTipElement CreateToolTipElement()
		{
			return new ToolTipElement(
				Diagram.Background,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				_textMeasurer);
		}

		private void patchElement_ExpandRequested(object sender, EventArgs<int> e) => ExpandItemRequested(sender, e);
		private void patchElement_MoveBackwardRequested(object sender, EventArgs<int> e) => MoveBackwardRequested(sender, e);
		private void patchElement_MoveForwardRequested(object sender, EventArgs<int> e) => MoveForwardRequested(sender, e);
		private void patchElement_PlayRequested(object sender, EventArgs<int> e) => PlayItemRequested(sender, e);
		private void patchElement_DeleteRequested(object sender, EventArgs<int> e) => DeleteRequested(sender, e);
		private void _pictureExpand_MouseDown(object sender, EventArgs e) => ExpandRequested(sender, EventArgs.Empty);
		private void _picturePlay_MouseDown(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
	}
}