using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.StringResources;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

// ReSharper disable SuggestVarOrType_SimpleTypes

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class MonitoringBarPresenter : PresenterBase<MonitoringBarViewModel>
    {
        public void Load(MonitoringBarViewModel viewModel) => ExecuteNonPersistedAction(
            viewModel,
            () =>
            {
                var midi = viewModel.Midi;
                midi.NoteNumber.Name = ResourceFormatter.NoteNumber;
                midi.Velocity.Name = ResourceFormatter.Velocity;
                midi.Channel.Name = ResourceFormatter.Channel;
            });

        public void DimensionValuesChanged(
            MonitoringBarViewModel viewModel,
            IList<(DimensionEnum dimensionEnum, string name, int? position, double value)> values)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    var synth = viewModel.Synth;
                    synth.Items = values.Select(x => x.ToViewModel(visible: true)).OrderBy(x => x.Name).ToList();
                    synth.IsEmpty = false;
                });

        public void MidiNoteOnOccurred(MonitoringBarViewModel viewModel, int midiNoteNumber, int midiVelocity, int midiChannel) => ExecuteNonPersistedAction(
            viewModel,
            () =>
            {
                var midi = viewModel.Midi;
                midi.NoteNumber.Value = ToViewModelHelper.Format_MonitoringBar_MidiValue(midiNoteNumber);
                midi.NoteNumber.Visible = true;
                midi.Velocity.Value = ToViewModelHelper.Format_MonitoringBar_MidiValue(midiVelocity);
                midi.Velocity.Visible = true;
                midi.Channel.Value = ToViewModelHelper.Format_MonitoringBar_MidiValue(midiChannel);
                midi.Channel.Visible = true;
                midi.IsEmpty = false;
            });

        public void MidiControllerValueChanged(
            MonitoringBarViewModel viewModel,
            int midiControllerCode,
            int absoluteMidiControllerValue,
            int relativeMidiControllerValue,
            int midiChannel)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    MonitoringBarMidiViewModel midiViewModel = viewModel.Midi;
                    MonitoringItemViewModel midiControllerViewModel = midiViewModel.Controller;
                    MonitoringItemViewModel midiChannelViewModel = midiViewModel.Channel;

                    midiControllerViewModel.Name = ToViewModelHelper.Format_MonitoringBar_MidiControllerName(midiControllerCode);
                    midiControllerViewModel.Value = ToViewModelHelper.Format_MonitoringBar_MidiControllerValue(absoluteMidiControllerValue, relativeMidiControllerValue);
                    midiControllerViewModel.Visible = true;
                    midiChannelViewModel.Name = ResourceFormatter.Channel;
                    midiChannelViewModel.Value = ToViewModelHelper.Format_MonitoringBar_MidiValue(midiChannel);
                    midiChannelViewModel.Visible = true;
                    midiViewModel.IsEmpty = false;
                });
    }
}