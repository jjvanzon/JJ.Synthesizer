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
        LibraryMidiMappingGroup,
        LibraryPatch,
        LibraryPatchGroup,
        LibraryScales,
        LibraryScale,
        Midi,
        MidiMappingGroup,
        Patch,

        /// <summary> Includes groupless patches, controlled through the main Patches node. </summary>
        PatchGroup,
        Scale,
        Scales
    }
}