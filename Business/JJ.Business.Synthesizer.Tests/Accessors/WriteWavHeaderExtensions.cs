using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static partial class WavExtensionWishesAccessor
    {
        internal static AudioInfoWish ToInfo(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<AudioInfoWish>(entity?.Obj, synthWishes);
        
        internal static AudioInfoWish ToInfo(this ConfigSectionAccessor entity)            
            => _accessor.InvokeMethod<AudioInfoWish>(entity?.Obj);
    }

    internal static partial class WavExtensionWishesAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(WavExtensionWishes));
        
        internal static ConfigResolverAccessor ApplyInfo(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj, infoWish, synthWishes));
        internal static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
            => _accessor.InvokeMethod<AudioInfoWish>(infoWish, obj.Obj, synthWishes);
    }

    internal static partial class WavExtensionWishesAccessor
    {
        internal static WavHeaderStruct ToWavHeader(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<WavHeaderStruct>(entity?.Obj, synthWishes);
        
        internal static WavHeaderStruct ToWavHeader(this ConfigSectionAccessor entity)            
            => _accessor.InvokeMethod<WavHeaderStruct>(entity?.Obj);

    }

    internal static partial class WavExtensionWishesAccessor
    {
        internal static ConfigResolverAccessor ApplyWavHeader(this ConfigResolverAccessor entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, wavHeader, synthWishes));
        
        internal static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<WavHeaderStruct>(wavHeader, entity?.Obj, synthWishes);

    }
    
    internal static partial class WavExtensionWishesAccessor
    {
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, filePath, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        internal static ConfigResolverAccessor ReadWavHeader(this ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, source, synthWishes));
        
        internal static string ReadWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.InvokeMethod(new[]{ filePath, entity?.Obj, synthWishes });
        internal static byte[] ReadWavHeader(this byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<byte[]>(source, entity?.Obj, synthWishes);
        internal static Stream ReadWavHeader(this Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<Stream>(source, entity?.Obj, synthWishes);
        internal static BinaryReader ReadWavHeader(this BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<BinaryReader>(source, entity?.Obj, synthWishes);
    }

    internal static partial class WavExtensionWishesAccessor
    {
        // With ConfigResolver
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, filePath, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static ConfigResolverAccessor WriteWavHeader(this ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, dest, synthWishes));
        
        internal static string WriteWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => (string)_accessor.InvokeMethod(new[]{ filePath, entity?.Obj, synthWishes });
        
        internal static byte[] WriteWavHeader(this byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<byte[]>(dest, entity?.Obj, synthWishes);
        
        internal static Stream WriteWavHeader(this Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<Stream>(dest, entity?.Obj, synthWishes);
        
        internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<BinaryWriter>(dest, entity?.Obj, synthWishes);

        // With ConfigSection
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, string filePath)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(new [] { entity?.Obj, filePath }));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, byte[] dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, BinaryWriter dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static ConfigSectionAccessor WriteWavHeader(this ConfigSectionAccessor entity, Stream dest)
            => new ConfigSectionAccessor(_accessor.InvokeMethod(entity?.Obj, dest));
        
        internal static string WriteWavHeader(this string filePath, ConfigSectionAccessor entity)
            => (string)_accessor.InvokeMethod(new[] { filePath, entity?.Obj });
        
        internal static byte[] WriteWavHeader(this byte[] dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<byte[]>(dest, entity?.Obj);
        
        internal static Stream WriteWavHeader(this Stream dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<Stream>(dest, entity?.Obj);
        
        internal static BinaryWriter WriteWavHeader(this BinaryWriter dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<BinaryWriter>(dest, entity?.Obj);

    }
}

