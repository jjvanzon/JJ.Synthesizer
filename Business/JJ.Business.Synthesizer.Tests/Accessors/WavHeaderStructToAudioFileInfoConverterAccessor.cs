using JJ.Business.Synthesizer.Infos;
// ReSharper disable UnusedMethodReturnValue.Global

namespace JJ.Business.Synthesizer.Tests.Accessors;

internal static class WavHeaderStructToAudioFileInfoConverterAccessor
{
    private static readonly AccessorCore _accessor = new("WavHeaderStructToAudioFileInfoConverter");

    public static AudioFileInfo Convert(WavHeaderStruct wavHeader)
        => (AudioFileInfo)_accessor.Call(wavHeader);
}
