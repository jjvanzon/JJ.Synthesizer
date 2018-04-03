using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class MonitoringBarPresenter : PresenterBase<MonitoringBarViewModel>
	{
		public void Load(MonitoringBarViewModel viewModel)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.Midi.NoteNumber.Name = ResourceFormatter.NoteNumber;
					viewModel.Midi.Velocity.Name = ResourceFormatter.Velocity;
					viewModel.Midi.Channel.Name = ResourceFormatter.Channel;
				});
		}

		public void DimensionValuesChanged(MonitoringBarViewModel viewModel, IList<(DimensionEnum dimensionEnum, string name, double value)> values)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.Synth = values.Select(x => x.ToViewModel()).OrderBy(x => x.Name).ToList());
		}

		public void MidiNoteOnOccurred(MonitoringBarViewModel viewModel, (int midiNoteNumber, int midiVelocity, int midiChannel) values)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.Midi.NoteNumber.Value = values.midiNoteNumber;
					viewModel.Midi.Velocity.Value = values.midiVelocity;
					viewModel.Midi.Channel.Value = values.midiChannel;
				});
		}

		public void MidiControllerValueChanged(
			MonitoringBarViewModel viewModel,
			(int midiControllerCode, int midiControllerValue, int midiChannel) values)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					string formattedControllerName = $"{ResourceFormatter.Controller} {values.midiControllerCode}";
					viewModel.Midi.Controller = new NameAndValueViewModel { Name = formattedControllerName, Value = values.midiControllerValue };
					viewModel.Midi.Channel = new NameAndValueViewModel { Name = ResourceFormatter.Channel, Value = values.midiChannel };
				});
		}
	}
}