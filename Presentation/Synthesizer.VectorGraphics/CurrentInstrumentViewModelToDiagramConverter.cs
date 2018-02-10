using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Converters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class CurrentInstrumentViewModelToDiagramConverter
	{
		private const float SPACING = 1f;

		private readonly CurrentPatchRectangleConverter _rectangleConverter;
		private readonly CurrentPatchLabelConverter _labelConverter;
		private readonly CurrentPatchPictureButtonConverter _moveBackwardPictureButtonConverter;
		private readonly CurrentPatchPictureButtonConverter _moveForwardPictureButtonConverter;
		private readonly CurrentPatchPictureButtonConverter _playPictureButtonConverter;
		private readonly CurrentPatchPictureButtonConverter _expandPictureButtonConverter;
		private readonly CurrentPatchPictureButtonConverter _deletePictureButtonConverter;

		private readonly HashSet<int> _patchIDHashSet;

		public CurrentInstrumentViewModelToDiagramConverterResult Result { get; private set; }

		public CurrentInstrumentViewModelToDiagramConverter(ITextMeasurer textMeasurer)
		{
			// TODO: Pass underlying pictures.

			Result = new CurrentInstrumentViewModelToDiagramConverterResult();

			_rectangleConverter = new CurrentPatchRectangleConverter(Result.Diagram);
			_labelConverter = new CurrentPatchLabelConverter(textMeasurer);
			_moveBackwardPictureButtonConverter = new CurrentPatchPictureButtonConverter();
			_moveForwardPictureButtonConverter = new CurrentPatchPictureButtonConverter();
			_playPictureButtonConverter = new CurrentPatchPictureButtonConverter();
			_expandPictureButtonConverter = new CurrentPatchPictureButtonConverter();
			_deletePictureButtonConverter = new CurrentPatchPictureButtonConverter();
			_patchIDHashSet = new HashSet<int>();
		}

		public void Convert(CurrentInstrumentViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			foreach (CurrentInstrumentItemViewModel itemViewModel in viewModel.List)
			{
				ConvertPatch(itemViewModel);
			}

			IEnumerable<int> idsToKeep = viewModel.List.Select(x => x.PatchID);
			IEnumerable<int> idsToDelete = _patchIDHashSet.Except(idsToKeep);
			foreach (int idToDelete in idsToDelete.ToArray())
			{
				_rectangleConverter.TryRemove(idToDelete);
				_moveBackwardPictureButtonConverter.TryRemove(idToDelete);
				_moveForwardPictureButtonConverter.TryRemove(idToDelete);
				_playPictureButtonConverter.TryRemove(idToDelete);
				_expandPictureButtonConverter.TryRemove(idToDelete);
				_deletePictureButtonConverter.TryRemove(idToDelete);
				_labelConverter.TryRemove(idToDelete);
				_patchIDHashSet.Remove(idToDelete);
			}
		}

		//private Retangle ConvertPatchTo

		private CurrentInstrumentViewModelToDiagramConverterPatchResultItem ConvertPatch(CurrentInstrumentItemViewModel viewModel)
		{
			var resultItem = new CurrentInstrumentViewModelToDiagramConverterPatchResultItem();
			float x = SPACING;

			Rectangle invisibleRectangle = _rectangleConverter.Convert(viewModel);

			if (viewModel.CanGoBackward)
			{
				(Picture picture, MouseDownGesture mouseDownGesture) = _moveBackwardPictureButtonConverter.Convert(viewModel.PatchID, invisibleRectangle, null);
				resultItem.GoBackwardMouseDownGesture = mouseDownGesture;
				x += picture.Position.Width + SPACING;
			}

			Label label = _labelConverter.Convert(viewModel, invisibleRectangle);
			label.Position.X = x;
			x += label.Position.Width + SPACING;

			if (viewModel.CanGoForward)
			{
				(Picture picture, MouseDownGesture mouseDownGesture) = _moveForwardPictureButtonConverter.Convert(viewModel.PatchID, invisibleRectangle, null);
				resultItem.GoForwardMouseDownGesture = mouseDownGesture;
				x += picture.Position.Width + SPACING;
			}

			{
				(Picture picture, MouseDownGesture mouseDownGesture) = _playPictureButtonConverter.Convert(viewModel.PatchID, invisibleRectangle, null);
				resultItem.PlayMouseDownGesture = mouseDownGesture;
				x += picture.Position.Width + SPACING;
			}
			{
				(Picture picture, MouseDownGesture mouseDownGesture) = _expandPictureButtonConverter.Convert(viewModel.PatchID, invisibleRectangle, null);
				resultItem.ExpandMouseDownGesture = mouseDownGesture;
				x += picture.Position.Width + SPACING;
			}
			{
				(Picture picture, MouseDownGesture mouseDownGesture) = _deletePictureButtonConverter.Convert(viewModel.PatchID, invisibleRectangle, null);
				resultItem.DeleteMouseDownGesture = mouseDownGesture;
				x += picture.Position.Width + SPACING;
			}

			x += SPACING;

			invisibleRectangle.Position.Width = x;

			_patchIDHashSet.Add(viewModel.PatchID);

			return resultItem;
		}
	}
}