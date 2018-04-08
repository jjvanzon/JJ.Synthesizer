using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public sealed class MonitoringBarElement : ElementWithScreenViewModelBase
	{
		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _midiLabel;
		private readonly Label _synthLabel;
		private readonly ToolTipElement _toolTipElement;
		private readonly ToolTipGesture _midiToolTipGesture;

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

			_toolTipElement = new ToolTipElement(
				_midiLabel,
				StyleHelper.ToolTipBackStyle,
				StyleHelper.ToolTipLineStyle,
				StyleHelper.ToolTipTextStyle,
				textMeasurer);

			_midiToolTipGesture = new ToolTipGesture(_toolTipElement);
			_midiLabel.Gestures.Add(_midiToolTipGesture);

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

			Position.Height = _synthLabel.Position.Bottom + StyleHelper.SPACING_SMALL;
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

			_toolTipElement.SetText(ViewModel.Midi.ToolTip);
		}

		private string FormatItemViewModels(string title, params MonitoringItemViewModel[] viewModels)
		{
			return FormatItemViewModels(title, (IList<MonitoringItemViewModel>)viewModels);
		}

		private string FormatItemViewModels(string title, IList<MonitoringItemViewModel> viewModels)
		{
			return title + ": " + string.Join(" | ", viewModels.Where(x => x.Visible).Select(x => $"{x.Name} = {x.Value}"));
		}
	}
}