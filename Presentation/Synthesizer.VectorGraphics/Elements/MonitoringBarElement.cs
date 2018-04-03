using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public sealed class MonitoringBarElement : ElementWithScreenViewModelBase
	{
		private const string DUMMY_TEXT = "Midi";

		private static readonly TextStyle _textStyle = CreateTextStyle();

		private static TextStyle CreateTextStyle()
		{
			TextStyle labelTextStyle = StyleHelper.DefaultTextStyle.Clone();
			labelTextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;
			labelTextStyle.Wrap = true;
			return labelTextStyle;
		}

		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _midiLabel;
		private readonly Label _synthLabel;

		public MonitoringBarElement(Element parent, ITextMeasurer textMeasurer) : base(parent)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			float x = StyleHelper.SPACING_SMALL;
			//float x = 0;
			float y = StyleHelper.SPACING_SMALL;
			//float y = 0;

			_midiLabel = new Label(this) { TextStyle = _textStyle };
			_midiLabel.Position.X = x;
			_midiLabel.Position.Y = y;

			(_, _midiLabel.Position.HeightInPixels) = _textMeasurer.GetTextSize(
				DUMMY_TEXT,
				_midiLabel.TextStyle.Font,
				_midiLabel.Position.WidthInPixels);

			y += _midiLabel.Position.Height;
			//y += StyleHelper.SPACING_SMALL;
			y += 0;

			_synthLabel = new Label(this) { TextStyle = _textStyle };
			_synthLabel.Position.X = x;
			_synthLabel.Position.Y = y;

			PositionElements();
		}

		public override void PositionElements()
		{
			_midiLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;

			_synthLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;
			(_, _synthLabel.Position.HeightInPixels) = _textMeasurer.GetTextSize(
				_synthLabel.Text,
				_synthLabel.TextStyle.Font,
				_synthLabel.Position.WidthInPixels);

			Position.Height = _synthLabel.Position.Bottom + StyleHelper.SPACING_SMALL;
			//Position.Height = _synthLabel.Position.Bottom;
		}

		public new MonitoringBarViewModel ViewModel
		{
			get => (MonitoringBarViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToElements()
		{
			_midiLabel.Text = FormatItemViewModels(
				ResourceFormatter.Midi,
				ViewModel.Midi.NoteNumber,
				ViewModel.Midi.Velocity,
				ViewModel.Midi.Controller,
				ViewModel.Midi.Channel);

			_synthLabel.Text = FormatItemViewModels(ResourceFormatter.Synth, ViewModel.Synth);
		}

		private string FormatItemViewModels(string title, params MonitoringItemViewModel[] viewModels) =>
			FormatItemViewModels(title, (IList<MonitoringItemViewModel>)viewModels);

		private string FormatItemViewModels(string title, IList<MonitoringItemViewModel> viewModels)
		{
			return title + ": " + string.Join(" | ", viewModels.Where(x => x.Visible).Select(x => $"{x.Name} = {x.Value}"));
		}
	}
}