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
					var midi = viewModel.Midi;
					midi.NoteNumber.Name = ResourceFormatter.NoteNumber;
					midi.Velocity.Name = ResourceFormatter.Velocity;
					midi.Channel.Name = ResourceFormatter.Channel;
				});
		}

		public void DimensionValuesChanged(MonitoringBarViewModel viewModel, IList<(DimensionEnum dimensionEnum, string name, double value)> values)
		{
			ExecuteNonPersistedAction(viewModel, () =>
			{
				var synth = viewModel.Synth;
				synth.Items = values.Select(x => x.ToViewModel(visible: true)).OrderBy(x => x.Name).ToList();
				synth.IsEmpty = false;
			});
		}

		public void MidiNoteOnOccurred(MonitoringBarViewModel viewModel, int midiNoteNumber, int midiVelocity, int midiChannel)
		{
			base.ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					var midi = viewModel.Midi;
					midi.NoteNumber.Value = midiNoteNumber;
					midi.NoteNumber.Visible = true;
					midi.Velocity.Value = midiVelocity;
					midi.Velocity.Visible = true;
					midi.Channel.Value = midiChannel;
					midi.Channel.Visible = true;
					midi.IsEmpty = false;
				});
		}

		public void MidiControllerValueChanged(MonitoringBarViewModel viewModel, int midiControllerCode, int midiControllerValue, int midiChannel)
		{
			base.ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					string formattedControllerName = $"{ResourceFormatter.Controller} {midiControllerCode}";
					var midi = viewModel.Midi;
					midi.Controller = new MonitoringItemViewModel { Name = formattedControllerName, Value = midiControllerValue };
					midi.Controller.Visible = true;
					midi.Channel = new MonitoringItemViewModel { Name = ResourceFormatter.Channel, Value = midiChannel };
					midi.Channel.Visible = true;
				});
		}
	}
}