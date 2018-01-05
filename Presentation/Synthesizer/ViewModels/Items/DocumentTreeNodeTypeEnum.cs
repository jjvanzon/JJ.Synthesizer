namespace JJ.Presentation.Synthesizer.ViewModels.Items
{
	public enum DocumentTreeNodeTypeEnum
	{
		Undefined,
		AudioFileOutputList,
		AudioOutput,
		Libraries,
		Library,
		LibraryPatch,
		LibraryPatchGroup,
		Midi,
		Patch,

		/// <summary> Includes groupless patches, controlled through the main Patches node. </summary>
		PatchGroup,
		Scales,
	}
}