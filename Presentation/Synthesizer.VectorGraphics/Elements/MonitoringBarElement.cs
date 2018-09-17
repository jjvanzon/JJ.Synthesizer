using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable once CompareOfFloatsByEqualityOperator

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public sealed class MonitoringBarElement : ElementBaseWithScreenViewModel
	{
		public event EventHandler HeightChanged;

		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _midiLabel;
		private readonly Label _synthLabel;

		private static readonly TextStyle _textStyle = CreateTextStyle();

		public MonitoringBarElement(Element parent, ITextMeasurer textMeasurer) : base(parent)
		{
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_midiLabel = new Label(this)
			{
				TextStyle = _textStyle
			};
			_midiLabel.Position.X = StyleHelper.SPACING_SMALL;
			_midiLabel.Position.Y = StyleHelper.SPACING_SMALL;

			_synthLabel = new Label(this)
			{
				TextStyle = _textStyle
			};

			_synthLabel.Position.X = StyleHelper.SPACING_SMALL;

			PositionElements();
		}

		private static TextStyle CreateTextStyle()
		{
			TextStyle textStyle = StyleHelper.DefaultTextStyle.Clone();
			textStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;
			textStyle.Wrap = true;
			return textStyle;
		}

		public override void PositionElements()
		{
			_midiLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;
			(_, _midiLabel.Position.HeightInPixels) = _textMeasurer.GetTextSize(
				_midiLabel.Text,
				_midiLabel.TextStyle.Font,
				_midiLabel.Position.WidthInPixels);

			_synthLabel.Position.Y = _midiLabel.Position.Bottom + StyleHelper.SPACING_SMALL;

			_synthLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;
			(_, _synthLabel.Position.HeightInPixels) = _textMeasurer.GetTextSize(
				_synthLabel.Text,
				_synthLabel.TextStyle.Font,
				_synthLabel.Position.WidthInPixels);

			float newHeight = _synthLabel.Position.Bottom + StyleHelper.SPACING_SMALL;
			if (Position.Height != newHeight)
			{
				Position.Height = newHeight;
				HeightChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public new MonitoringBarViewModel ViewModel
		{
			get => (MonitoringBarViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToElements()
		{
			if (ViewModel.Midi.IsEmpty)
			{
				_midiLabel.Text = $"{ResourceFormatter.Midi}: ...";
			}
			else
			{
				_midiLabel.Text = FormatItemViewModels(
					ResourceFormatter.Midi,
					ViewModel.Midi.NoteNumber,
					ViewModel.Midi.Velocity,
					ViewModel.Midi.Controller,
					ViewModel.Midi.Channel);
			}

			if (ViewModel.Synth.IsEmpty)
			{
				_synthLabel.Text = $"{ResourceFormatter.Synth}: ...";
			}
			else
			{
				_synthLabel.Text = FormatItemViewModels(ResourceFormatter.Synth, ViewModel.Synth.Items);
			}
		}

		private string FormatItemViewModels(string title, params MonitoringItemViewModel[] viewModels) => FormatItemViewModels(title, (IList<MonitoringItemViewModel>)viewModels);

	    private string FormatItemViewModels(string title, IList<MonitoringItemViewModel> viewModels) => title + ": " + string.Join(" | ", viewModels.Where(x => x.Visible).Select(x => $"{x.Name} = {x.Value}"));
	}
}