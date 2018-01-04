using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class MidiMapping_SideEffect_AutoCreate_MidiMapingElement : ISideEffect
	{
		private readonly MidiMapping _midiMapping;
		private readonly MidiMappingManager _midiMappingManager;

		public MidiMapping_SideEffect_AutoCreate_MidiMapingElement(MidiMapping midiMapping, MidiMappingManager midiMappingManager)
		{
			_midiMapping = midiMapping ?? throw new ArgumentNullException(nameof(midiMapping));
			_midiMappingManager = midiMappingManager ?? throw new ArgumentNullException(nameof(midiMappingManager));
		}

		public void Execute()
		{
			bool mustExecute = _midiMapping.MidiMappingElements.Count == 0;
			if (mustExecute)
			{
				_midiMappingManager.CreateMidiMappingElement(_midiMapping);
			}
		}
	}
}