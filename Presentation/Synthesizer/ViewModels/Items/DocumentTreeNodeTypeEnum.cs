namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public enum DocumentTreeNodeTypeEnum
	{
		Undefined,
		AudioFileOutputList,
		AudioOutput,
		Libraries,
		Library,
		LibraryMidi,
		LibraryMidiMapping,
		LibraryPatch,
		LibraryPatchGroup,
		LibraryScales,
		LibraryScale,
		Midi,
		MidiMapping,
		Patch,

		/// <summary> Includes groupless patches, controlled through the main Patches node. </summary>
		PatchGroup,
		Scales,
	}
}