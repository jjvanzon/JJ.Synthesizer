using System.Collections.Generic;
using System.Linq;
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
			_midiLabel = new Label(this) { TextStyle = StyleHelper.TitleTextStyle };
			_midiLabel.Position.Height = StyleHelper.ROW_HEIGHT;

			_synthLabel = new Label(this) { TextStyle = StyleHelper.TitleTextStyle };
			_synthLabel.Position.Y = StyleHelper.ROW_HEIGHT;
			_synthLabel.Position.Height = StyleHelper.ROW_HEIGHT;

			Position.Height = StyleHelper.ROW_HEIGHT * 2;
		}

		public new MonitoringBarViewModel ViewModel
		{
			get => (MonitoringBarViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToElements()
		{
			_midiLabel.Text = FormatItemViewModels(
				ViewModel.Midi.Controller,
				ViewModel.Midi.NoteNumber,
				ViewModel.Midi.Velocity,
				ViewModel.Midi.Channel);

			_synthLabel.Text = FormatItemViewModels(ViewModel.Synth);
		}

		private string FormatItemViewModels(params NameAndValueViewModel[] viewModels) =>
			FormatItemViewModels((IList<NameAndValueViewModel>)viewModels);

		private string FormatItemViewModels(IList<NameAndValueViewModel> viewModels) => string.Join(" | ", viewModels.Select(FormatItemViewModel));

		private string FormatItemViewModel(NameAndValueViewModel viewModel) => $"{viewModel.Name} = {viewModel.Value}";

		public override void PositionElements()
		{
			_midiLabel.Position.Width = Position.Width;
			_synthLabel.Position.Width = Position.Width;
		}
	}
}