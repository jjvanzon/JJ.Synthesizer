using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public sealed class MonitoringBarElement : ElementWithScreenViewModelBase
	{
		public readonly Label _midiLabel;
		public readonly Label _synthLabel;

		public MonitoringBarElement(Element parent) : base(parent)
		{
			float x = StyleHelper.SPACING_SMALL;
			float y = StyleHelper.SPACING_SMALL;

			_midiLabel = new Label(this) { TextStyle = StyleHelper.TitleTextStyle };
			_midiLabel.Position.X = x;
			_midiLabel.Position.Y = y;
			_midiLabel.Position.Height = StyleHelper.ROW_HEIGHT_SMALL;

			y += StyleHelper.ROW_HEIGHT_SMALL;
			y += StyleHelper.SPACING_SMALL;

			_synthLabel = new Label(this) { TextStyle = StyleHelper.TitleTextStyle };
			_synthLabel.Position.X = x;
			_synthLabel.Position.Y = y;
			_synthLabel.Position.Height = StyleHelper.ROW_HEIGHT_SMALL;

			y += StyleHelper.ROW_HEIGHT_SMALL;
			y += StyleHelper.SPACING_SMALL;

			Position.Height = y;
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

		private string FormatItemViewModels(string title, params NameAndValueViewModel[] viewModels) =>
			FormatItemViewModels(title, (IList<NameAndValueViewModel>)viewModels);

		private string FormatItemViewModels(string title, IList<NameAndValueViewModel> viewModels)
		{
			return title + ": " + string.Join(" | ", viewModels.Select(FormatItemViewModel));
		}

		private string FormatItemViewModel(NameAndValueViewModel viewModel) => $"{viewModel.Name} = {viewModel.Value}";

		public override void PositionElements()
		{
			_midiLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;
			_synthLabel.Position.Width = Position.Width - StyleHelper.SPACING_SMALL_TIMES_2;
		}
	}
}