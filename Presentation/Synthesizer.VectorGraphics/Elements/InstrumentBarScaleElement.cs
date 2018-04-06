using System;
using JJ.Data.Canonical;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class InstrumentBarScaleElement : ElementBaseWithOpaqueBack
	{
		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _label;

		public InstrumentBarScaleElement(Element parent, ITextMeasurer textMeasurer) : base(parent)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_label = CreateLabel(_backRectangle);
			Position.Height = StyleHelper.ROW_HEIGHT;
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

		public override void PositionElements()
		{
			(float textWidth, _) = _textMeasurer.GetTextSize(_label.Text, _label.TextStyle.Font);
			_label.Position.Width = textWidth;
			Position.Width = textWidth;

			base.PositionElements();
		}

		private Label CreateLabel(Element parent)
		{
			var label = new Label(parent)
			{
				TextStyle = StyleHelper.TitleTextStyle
			};
			label.Position.Height = StyleHelper.ROW_HEIGHT;

			return label;
		}
	}
}