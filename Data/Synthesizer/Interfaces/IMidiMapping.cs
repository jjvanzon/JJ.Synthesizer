namespace JJ.Data.Synthesizer.Interfaces
{
	public interface IMidiMapping
	{
		/// <summary> Tells us if MIDI controller value changes are interpreted as absolute values or relative changes. </summary>
		bool IsRelative { get; set; }

		int FromMidiValue { get; set; }
		int TillMidiValue { get; set; }
		int? MidiControllerCode { get; set; }

		/// <summary> optional </summary>
		string Name { get; set; }
		int? Position { get; set; }

		double FromDimensionValue { get; set; }
		double TillDimensionValue { get; set; }
		double? MinDimensionValue { get; set; }
		double? MaxDimensionValue { get; set; }
	}
}