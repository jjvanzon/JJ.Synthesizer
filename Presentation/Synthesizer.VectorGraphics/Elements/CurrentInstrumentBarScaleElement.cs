using System;
using JJ.Data.Canonical;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class CurrentInstrumentBarScaleElement : ElementBase
	{
		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _label;

		public CurrentInstrumentBarScaleElement(Element parent, ITextMeasurer textMeasurer) : base(parent)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_label = CreateLabel();
		}

		private IDAndName _viewModel;

		public IDAndName ViewModel
		{
			get => _viewModel;
			set
			{
				_viewModel = value ?? throw new ArgumentNullException(nameof(ViewModel));
				ApplyViewModelToElements();
			}
		}

		private void ApplyViewModelToElements() => _label.Text = ViewModel.Name;

		public void PositionElements()
		{
			(float textWidth, _) = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font);
			_label.Position.Width = textWidth;
			Position.Width = textWidth;
		}

		private Label CreateLabel()
		{
			var label = new Label(this)
			{
				TextStyle = StyleHelper.TitleTextStyle
			};
			label.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

			return label;
		}
	}
}