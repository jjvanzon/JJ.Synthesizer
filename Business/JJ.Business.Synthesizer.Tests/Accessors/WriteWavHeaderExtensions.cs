using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class WriteWavHeaderExtensionsAccessor
    {
        private static readonly AccessorEx _accessor = new AccessorEx(typeof(WriteWavHeaderExtensions));
        
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

