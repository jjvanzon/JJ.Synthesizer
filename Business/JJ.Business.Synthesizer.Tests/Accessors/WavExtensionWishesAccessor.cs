namespace JJ.Business.Synthesizer.Tests.Accessors;

internal static class WavExtensionWishesAccessor
{
    private static AccessorCore _accessor = new (typeof(WavExtensionWishes));

    // ToInfo
    
    internal static AudioInfoWish ToInfo(this ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (AudioInfoWish)_accessor.Call(entity?.Obj, synthWishes);
    
    internal static AudioInfoWish ToInfo(this ConfigSectionAccessor entity)            
        => (AudioInfoWish)_accessor.Call(entity?.Obj);

    // ApplyInfo
    
    internal static ConfigResolverAccessor ApplyInfo(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
        => new(_accessor.Call(obj.Obj, infoWish, synthWishes));
    
    internal static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
        => (AudioInfoWish)_accessor.Call(infoWish, obj.Obj, synthWishes);

    // ToWavHeader
    
    internal static WavHeaderStruct ToWavHeader(this ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (WavHeaderStruct)_accessor.Call(entity?.Obj, synthWishes);
    
    internal static WavHeaderStruct ToWavHeader(this ConfigSectionAccessor entity)            
        => (WavHeaderStruct)_accessor.Call(entity?.Obj);

    // ApplyWavHeader
    
    internal static ConfigResolverAccessor ApplyWavHeader(this ConfigResolverAccessor entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, wavHeader, synthWishes));
    
    internal static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (WavHeaderStruct)_accessor.Call(wavHeader, entity?.Obj, synthWishes);

    // ReadWavHeader

    internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, filePath, synthWishes));
    internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, source, synthWishes));
    internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, source, synthWishes));
    internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, source, synthWishes));
    
    internal static string ReadWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (string)_accessor.Call([ filePath, entity?.Obj, synthWishes ]);
    internal static byte[] ReadWavHeader(this byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (byte[])_accessor.Call(source, entity?.Obj, synthWishes);
    internal static Stream ReadWavHeader(this Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (Stream)_accessor.Call(source, entity?.Obj, synthWishes);
    internal static BinaryReader ReadWavHeader(this BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (BinaryReader)_accessor.Call(source, entity?.Obj, synthWishes);

    // WriteWavHeader
    
    // With ConfigResolver

    internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, filePath, synthWishes));
    
    internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, dest, synthWishes));
    
    internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, dest, synthWishes));
    
    internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
        => new(_accessor.Call(entity?.Obj, dest, synthWishes));
    
    internal static string WriteWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (string)_accessor.Call([ filePath, entity?.Obj, synthWishes ]);
    
    internal static byte[] WriteWavHeader(this byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (byte[])_accessor.Call(dest, entity?.Obj, synthWishes);
    
    internal static Stream WriteWavHeader(this Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (Stream)_accessor.Call(dest, entity?.Obj, synthWishes);
    
    internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
        => (BinaryWriter)_accessor.Call(dest, entity?.Obj, synthWishes);

    // With ConfigSection
    
    internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, string filePath)
        => new ConfigSectionAccessor(_accessor.Call([ entity?.Obj, filePath ]));
    
    internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, byte[] dest)
        => new ConfigSectionAccessor(_accessor.Call(entity?.Obj, dest));
    
    internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, BinaryWriter dest)
        => new ConfigSectionAccessor(_accessor.Call(entity?.Obj, dest));
    
    internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, Stream dest)
        => new ConfigSectionAccessor(_accessor.Call(entity?.Obj, dest));
    
    internal static string WriteWavHeader(this string filePath, ConfigSectionAccessor entity)
        => (string)_accessor.Call([ filePath, entity?.Obj ]);
    
    internal static byte[] WriteWavHeader(this byte[] dest, ConfigSectionAccessor entity)
        => (byte[])_accessor.Call(dest, entity?.Obj);
    
    internal static Stream WriteWavHeader(this Stream dest, ConfigSectionAccessor entity)
        => (Stream)_accessor.Call(dest, entity?.Obj);
    
    internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigSectionAccessor entity)
        => (BinaryWriter)_accessor.Call(dest, entity?.Obj);

}

