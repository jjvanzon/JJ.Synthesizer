using System;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class MidiMappingDetailsViewModelToDiagramConverter
	{
		private readonly Label _waterMarkTitleLabel;

		public MidiMappingDetailsViewModelToDiagramConverterResult Result { get; }

		public MidiMappingDetailsViewModelToDiagramConverter(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			Result = new MidiMappingDetailsViewModelToDiagramConverterResult(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);

			_waterMarkTitleLabel = CreateWaterMarkTitleLabel(Result.Diagram);
		}

		public void Execute(MidiMappingDetailsViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			UpdateWaterMarkTitleLabel(viewModel.MidiMapping.Name);
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
	}
}
