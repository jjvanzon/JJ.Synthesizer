namespace JJ.Data.Synthesizer.Interfaces
{
	/// <summary> Exposes just the properties used by shared logic for MidiMapping and MidiMappingDto. </summary>
	public interface IMidiMapping
	{
		int FromMidiValue { get; }
		int TillMidiValue { get; }
		double FromDimensionValue { get; }
		double TillDimensionValue { get; }
	}
}