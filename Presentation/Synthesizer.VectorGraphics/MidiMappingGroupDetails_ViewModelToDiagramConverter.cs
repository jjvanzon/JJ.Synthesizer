using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingGroupDetails_ViewModelToDiagramConverter
	{
		private const float LABEL_Y = StyleHelper.DEFAULT_OBJECT_SIZE + StyleHelper.SPACING_SMALL;
		private const float DENT_POINT_Y = 8f;
		private const float HALF_DEFAULT_OBJECT_SIZE = StyleHelper.DEFAULT_OBJECT_SIZE / 2f;
		private const float DEFAULT_GRID_SNAP = 8f;
		private const float MAX_LABEL_WIDTH = 100f;

		private readonly Label _waterMarkTitleLabel;
		private readonly Dictionary<int, Ellipse> _circleDictionary;
		private readonly Dictionary<int, Label> _labelDictionary;
		private readonly Dictionary<int, Point> _dentPointDictionary;
		private readonly ITextMeasurer _textMeasurer;

		public MidiMappingGroupDetails_ViewModelToDiagramConverterResult Result { get; }

		public MidiMappingGroupDetails_ViewModelToDiagramConverter(
			ITextMeasurer textMeasurer,
			int doubleClickSpeedInMilliseconds,
			int doubleClickDeltaInPixels)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			Result = new MidiMappingGroupDetails_ViewModelToDiagramConverterResult(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			Result.GridSnapGesture.Snap = DEFAULT_GRID_SNAP;
			Result.Diagram.Gestures.Add(Result.DeleteMidiMappingGesture);
			Result.Diagram.Gestures.Add(Result.ExpandMidiMappingKeyboardGesture);

			_circleDictionary = new Dictionary<int, Ellipse>();
			_labelDictionary = new Dictionary<int, Label>();
			_dentPointDictionary = new Dictionary<int, Point>();

			_waterMarkTitleLabel = CreateWaterMarkTitleLabel(Result.Diagram);
		}

		public void Execute(MidiMappingGroupDetailsViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			UpdateWaterMarkTitleLabel(viewModel.MidiMappingGroup.Name);

			foreach (MidiMappingItemViewModel midiMappingItemViewModel in viewModel.MidiMappings.Values)
			{
				ConvertToVectorGraphics(midiMappingItemViewModel);
			}

			IEnumerable<int> idsToKeep = viewModel.MidiMappings.Keys;
			IEnumerable<int> idsToDelete = _circleDictionary.Keys.Except(idsToKeep);
			foreach (int idToDelete in idsToDelete.ToArray())
			{
				if (_circleDictionary.TryGetValue(idToDelete, out Ellipse circleToDelete))
				{
					circleToDelete.Dispose();

					_circleDictionary.Remove(idToDelete);
				}

				if (_labelDictionary.TryGetValue(idToDelete, out Label labelToDelete))
				{
					labelToDelete.Dispose();

					_labelDictionary.Remove(idToDelete);
				}

				if (_dentPointDictionary.TryGetValue(idToDelete, out Point dentPointToDelete))
				{
					dentPointToDelete.Dispose();

					_dentPointDictionary.Remove(idToDelete);
				}
			}
		}

		private void ConvertToVectorGraphics(MidiMappingItemViewModel viewModel)
		{
			Ellipse circle = ConvertToCircle(viewModel);
			ConvertToLabel(viewModel, circle);
			ConvertToDentPoint(viewModel, circle);
		}

		private Ellipse ConvertToCircle(MidiMappingItemViewModel viewModel)
		{
			if (!_circleDictionary.TryGetValue(viewModel.ID, out Ellipse circle))
			{
				circle = new Ellipse(Result.Diagram.Background)
				{
					Tag = viewModel.ID
				};

				circle.Position.Width = StyleHelper.DEFAULT_OBJECT_SIZE;
				circle.Position.Height = StyleHelper.DEFAULT_OBJECT_SIZE;
				circle.Style.BackStyle = StyleHelper.CircleBackStyle;
				circle.Style.LineStyle = StyleHelper.CircleLineStyle;

				circle.Gestures.Add(Result.MoveGesture);
				circle.Gestures.Add(Result.SelectMidiMappingGesture);
				circle.Gestures.Add(Result.ExpandMidiMappingMouseGesture);

				_circleDictionary[viewModel.ID] = circle;
			}

			circle.Position.X = viewModel.Position.CenterX - HALF_DEFAULT_OBJECT_SIZE;
			circle.Position.Y = viewModel.Position.CenterY - HALF_DEFAULT_OBJECT_SIZE;

			if (viewModel.IsSelected && viewModel.HasInactiveStyle)
			{
				circle.Style.BackStyle = StyleHelper.CircleBackStyleSelectedInactive;
				circle.Style.LineStyle = StyleHelper.CircleLineStyleSelectedInactive;
			}
			else if (viewModel.IsSelected)
			{
				circle.Style.BackStyle = StyleHelper.CircleBackStyleSelected;
				circle.Style.LineStyle = StyleHelper.CircleLineStyleSelected;
			}
			else if (viewModel.HasInactiveStyle)
			{
				circle.Style.BackStyle = StyleHelper.CircleBackStyleInactive;
				circle.Style.LineStyle = StyleHelper.CircleLineStyleInactive;
			}
			else
			{
				circle.Style.BackStyle = StyleHelper.CircleBackStyle;
				circle.Style.LineStyle = StyleHelper.CircleLineStyle;
			}

			return circle;
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private Label ConvertToLabel(MidiMappingItemViewModel viewModel, Element parent)
		{
			if (!_labelDictionary.TryGetValue(viewModel.ID, out Label label))
			{
				label = new Label(parent);

				label.Position.Y = LABEL_Y;

				_labelDictionary[viewModel.ID] = label;
			}

			if (viewModel.HasInactiveStyle)
			{
				label.TextStyle = StyleHelper.MidiMappingTextStyleInactive;
			}
			else
			{
				label.TextStyle = StyleHelper.MidiMappingTextStyle;
			}

			label.Text = viewModel.Caption;

			(float textWidth, float textHeight) = _textMeasurer.GetTextSize(label.Text, label.TextStyle.Font, MAX_LABEL_WIDTH);
			label.Position.Width = textWidth;
			label.Position.Height = textHeight;

			label.Position.X = StyleHelper.DEFAULT_OBJECT_SIZE / 2f - label.Position.Width / 2f;

			return label;
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private Point ConvertToDentPoint(MidiMappingItemViewModel viewModel, Element parent)
		{
			if (!_dentPointDictionary.TryGetValue(viewModel.ID, out Point point))
			{
				point = new Point(parent);

				point.Position.X = StyleHelper.DEFAULT_OBJECT_SIZE / 2f;
				point.Position.Y = DENT_POINT_Y;

				_dentPointDictionary[viewModel.ID] = point;
			}

			if (viewModel.IsSelected && viewModel.HasInactiveStyle)
			{
				point.PointStyle = StyleHelper.DentPointStyleSelectedInactive;
			}
			else if (viewModel.IsSelected)
			{
				point.PointStyle = StyleHelper.DentPointStyleSelected;
			}
			else if (viewModel.HasInactiveStyle)
			{
				point.PointStyle = StyleHelper.DentPointStyleInactive;
			}
			else
			{
				point.PointStyle = StyleHelper.DentPointStyle;
			}

			return point;
		}

		private Label CreateWaterMarkTitleLabel(Diagram diagram)
		{
			var label = new Label(diagram.Background)
			{
				ZIndex = -1,
				TextStyle = StyleHelper.TopWaterMarkTextStyle
			};

			label.Position.X = StyleHelper.SPACING_SMALL;
			label.Position.Y = StyleHelper.SPACING_SMALL;
#if DEBUG
			label.Tag = "Title Label";
#endif
			return label;
		}

		private void UpdateWaterMarkTitleLabel(string text)
		{
			_waterMarkTitleLabel.Text = text;
			_waterMarkTitleLabel.Position.Width = _waterMarkTitleLabel.Diagram.Position.ScaledWidth - _waterMarkTitleLabel.Position.X;
			_waterMarkTitleLabel.Position.Height = _waterMarkTitleLabel.Diagram.Position.ScaledHeight;
		}
	}
}