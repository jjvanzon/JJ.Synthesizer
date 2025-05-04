namespace JJ.Business.Synthesizer.Tests.Accessors;

internal static class WavWishesAccessor
{
    private static AccessorCore _accessor = new(typeof(WavWishes));
    
    // With ConfigResolver
    
    public static AudioInfoWish ToInfo(ConfigResolverAccessor entity, SynthWishes synthWishes) 
        => (AudioInfoWish)_accessor.Call(entity.Obj, synthWishes);

    public static AudioInfoWish ApplyInfo(AudioInfoWish infoWish, ConfigResolverAccessor entity, SynthWishes synthWishes) 
        => (AudioInfoWish)_accessor.Call(infoWish, entity.Obj, synthWishes);

    public static ConfigResolverAccessor ApplyInfo(ConfigResolverAccessor entity, AudioInfoWish infoWish, SynthWishes synthWishes)
        => new (_accessor.Call(entity.Obj, infoWish, synthWishes));
    
    // With ConfigSection

    // ToInfo
    
    public static AudioInfoWish ToInfo(ConfigSectionAccessor entity)
        => (AudioInfoWish)_accessor.Call(entity.Obj);
    
    // ToWavHeader
    
    public static WavHeaderStruct ToWavHeader(ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (WavHeaderStruct)_accessor.Call(entity.Obj, synthWishes);
    
    public static WavHeaderStruct ToWavHeader(ConfigSectionAccessor entity)
        => (WavHeaderStruct)_accessor.Call(entity.Obj);

    // ApplyWavHeader
    
    public static ConfigResolverAccessor ApplyWavHeader(ConfigResolverAccessor entity, WavHeaderStruct wav, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, wav, synthWishes));
    
    public static WavHeaderStruct ApplyWavHeader(WavHeaderStruct wav, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (WavHeaderStruct)_accessor.Call(wav, entity.Obj, synthWishes);
    
    // ReadWavHeader
    
    public static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, filePath, synthWishes));
    
    public static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, source, synthWishes));
    
    public static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, source, synthWishes));
    
    public static ConfigResolverAccessor ReadWavHeader(ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, source, synthWishes));
    
    public static string ReadWavHeader(string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (string)_accessor.Call(new[]{ filePath, entity.Obj, synthWishes });
    
    public static byte[] ReadWavHeader(byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (byte[])_accessor.Call(source, entity.Obj, synthWishes);
    
    public static Stream ReadWavHeader(Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (Stream)_accessor.Call(source, entity.Obj, synthWishes);
    
    public static BinaryReader ReadWavHeader(BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (BinaryReader)_accessor.Call(source, entity.Obj, synthWishes);
    
    // WriteWavHeader
    
    public static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, filePath, synthWishes));
    
    public static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, dest, synthWishes));
    
    public static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, dest, synthWishes));
    
    public static ConfigResolverAccessor WriteWavHeader(ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity.Obj, dest, synthWishes));
    
    public static string WriteWavHeader(string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (string)_accessor.Call(new[] { filePath, entity.Obj, synthWishes });
    
    public static byte[] WriteWavHeader(byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (byte[])_accessor.Call(dest, entity.Obj, synthWishes);
    
    public static Stream WriteWavHeader(Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (Stream)_accessor.Call(dest, entity.Obj, synthWishes);
    
    public static BinaryWriter WriteWavHeader(BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (BinaryWriter)_accessor.Call(dest, entity.Obj, synthWishes);
    
    public static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, string filePath)
        => new(_accessor.Call([ entity.Obj, filePath ]));
    
    public static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, byte[] dest)
        => new(_accessor.Call(entity.Obj, dest));
    
    public static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, BinaryWriter dest)
        => new(_accessor.Call(entity.Obj, dest));
    
    public static ConfigSectionAccessor WriteWavHeader(ConfigSectionAccessor entity, Stream dest)
        => new(_accessor.Call(entity.Obj, dest));
    
    public static string WriteWavHeader(string filePath, ConfigSectionAccessor entity)
        => (string)_accessor.Call([ filePath, entity.Obj ]);
    
    public static byte[] WriteWavHeader(byte[] dest, ConfigSectionAccessor entity)
        => (byte[])_accessor.Call(dest, entity.Obj);
    
    public static Stream WriteWavHeader(Stream dest, ConfigSectionAccessor entity)
        => (Stream)_accessor.Call(dest, entity.Obj);
    
    public static BinaryWriter WriteWavHeader(BinaryWriter dest, ConfigSectionAccessor entity)
        => (BinaryWriter)_accessor.Call(dest, entity.Obj);
}
