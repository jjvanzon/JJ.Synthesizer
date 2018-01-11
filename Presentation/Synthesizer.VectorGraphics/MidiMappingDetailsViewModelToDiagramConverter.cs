using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingDetailsViewModelToDiagramConverter
	{
		private const float CIRCLE_SIZE = StyleHelper.OPERATOR_HEIGHT; // TODO: Rename this constant.
		private const float HALF_CIRCLE_SIZE = CIRCLE_SIZE / 2f; // TODO: Rename this constant.
		private const float LABEL_Y = CIRCLE_SIZE + StyleHelper.DEFAULT_SPACING;

		private static readonly TextStyle _textStyle = CreateTextStyle();
		private static readonly BackStyle _circleBackStyle = GetCircleBackStyle();
		private static readonly LineStyle _circleLineStyle = CreateCircleLineStyle();

		private readonly Label _waterMarkTitleLabel;
		private readonly Dictionary<int, Ellipse> _circleDictionary;
		private readonly Dictionary<int, Label> _labelDictionary;

		public MidiMappingDetailsViewModelToDiagramConverterResult Result { get; }

		public MidiMappingDetailsViewModelToDiagramConverter(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Result = new MidiMappingDetailsViewModelToDiagramConverterResult(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);

			_circleDictionary = new Dictionary<int, Ellipse>();
			_labelDictionary = new Dictionary<int, Label>();

			_waterMarkTitleLabel = CreateWaterMarkTitleLabel(Result.Diagram);
		}

		public void Execute(MidiMappingDetailsViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			UpdateWaterMarkTitleLabel(viewModel.MidiMapping.Name);

			foreach (MidiMappingElementItemViewModel midiMappingElementViewModel in viewModel.Elements.Values)
			{
				ConvertMidiMappingElement(midiMappingElementViewModel);
			}

			IEnumerable<int> idsToKeep = viewModel.Elements.Keys;
			IEnumerable<int> idsToDelete = _circleDictionary.Keys.Except(idsToKeep);
			foreach (int idToDelete in idsToDelete.ToArray())
			{
				if (_circleDictionary.TryGetValue(idToDelete, out Ellipse circleToDelete))
				{
					circleToDelete.Parent = null;
					circleToDelete.Children.Clear();
					Result.Diagram.Elements.Remove(circleToDelete);

					_circleDictionary.Remove(idToDelete);
				}

				if (_labelDictionary.TryGetValue(idToDelete, out Label labelToDelete))
				{
					labelToDelete.Parent = null;
					Result.Diagram.Elements.Remove(labelToDelete);

					_labelDictionary.Remove(idToDelete);
				}
			}
		}

		private void ConvertMidiMappingElement(MidiMappingElementItemViewModel viewModel)
		{
			Ellipse circle = ConvertToCircle(viewModel);
			ConvertToLabel(viewModel, circle);
		}

		private Ellipse ConvertToCircle(MidiMappingElementItemViewModel viewModel)
		{
			if (!_circleDictionary.TryGetValue(viewModel.ID, out Ellipse circle))
			{
				circle = new Ellipse
				{
					Diagram = Result.Diagram,
					Parent = Result.Diagram.Background,
				};
				circle.Position.Width = CIRCLE_SIZE;
				circle.Position.Height = CIRCLE_SIZE;
				circle.Style.BackStyle = _circleBackStyle;
				circle.Style.LineStyle = _circleLineStyle;

				_circleDictionary[viewModel.ID] = circle;
			}

			circle.Position.X = viewModel.Position.CenterX;
			circle.Position.Y = viewModel.Position.CenterY;

			return circle;
		}

		private Label ConvertToLabel(MidiMappingElementItemViewModel viewModel, Element parent)
		{
			if (!_labelDictionary.TryGetValue(viewModel.ID, out Label label))
			{
				label = new Label
				{
					Diagram = Result.Diagram,
					Parent = parent,
					TextStyle = _textStyle
				};

				label.Position.X = HALF_CIRCLE_SIZE;
				label.Position.Y = LABEL_Y;
				
				_labelDictionary[viewModel.ID] = label;
			}

			label.Text = viewModel.Caption;

			return label;
		}

		private Label CreateWaterMarkTitleLabel(Diagram diagram)
		{
			var label = new Label
			{
				Diagram = diagram,
				Parent = diagram.Background,
				ZIndex = -1,
				TextStyle = StyleHelper.WaterMarkTextStyle.Clone()
			};

			label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
#if DEBUG
			label.Tag = "Title Label";
#endif
			return label;
		}

		private void UpdateWaterMarkTitleLabel(string text)
		{
			_waterMarkTitleLabel.Text = text;
			_waterMarkTitleLabel.Position.X = _waterMarkTitleLabel.Diagram.Position.PixelsToX(StyleHelper.DEFAULT_SPACING);
			_waterMarkTitleLabel.Position.Y = _waterMarkTitleLabel.Diagram.Position.PixelsToY(StyleHelper.DEFAULT_SPACING);
			_waterMarkTitleLabel.Position.Width = _waterMarkTitleLabel.Diagram.Position.ScaledWidth - _waterMarkTitleLabel.Position.X;
			_waterMarkTitleLabel.Position.Height = _waterMarkTitleLabel.Diagram.Position.ScaledHeight;
		}

		private static TextStyle CreateTextStyle()
		{
			TextStyle textStyle = StyleHelper.DefaultTextStyle.Clone();
			textStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
			textStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
			return textStyle;
		}

		private static BackStyle GetCircleBackStyle() => StyleHelper.GetNeutralBackStyle();

		private static LineStyle CreateCircleLineStyle()
		{
			LineStyle lineStyle = StyleHelper.BorderStyle.Clone();
			lineStyle.Width *= 2;
			return lineStyle;
		}
	}
}
