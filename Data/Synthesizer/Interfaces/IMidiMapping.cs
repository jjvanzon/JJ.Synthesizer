namespace JJ.Data.Synthesizer.Interfaces
{
	public interface IMidiMapping
	{
		/// <summary> optional </summary>
		string CustomDimensionName { get; set; }
		double? FromDimensionValue { get; set; }
		int? FromMidiControllerValue { get; set; }
		int? FromMidiNoteNumber { get; set; }
		int? FromMidiVelocity { get; set; }
		int? FromPosition { get; set; }
		/// <summary> 1-based </summary>
		int? FromToneNumber { get; set; }
		/// <summary> Tells us if MIDI controller value changes are interpreted as absolute values or relative changes. </summary>
		bool IsRelative { get; set; }
		double? MaxDimensionValue { get; set; }
		int? MidiControllerCode { get; set; }
		double? MinDimensionValue { get; set; }
		double? TillDimensionValue { get; set; }
		int? TillMidiControllerValue { get; set; }
		int? TillMidiNoteNumber { get; set; }
		int? TillMidiVelocity { get; set; }
		int? TillPosition { get; set; }
		/// <summary> 1-based </summary>
		int? TillToneNumber { get; set; }
	}
}