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
        
        internal static void WriteWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, filePath, synthWishes);
        
        internal static void WriteWavHeader(this ConfigResolverAccessor entity, byte[] dest, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, dest, synthWishes);
        
        internal static void WriteWavHeader(this ConfigResolverAccessor entity, BinaryWriter dest, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, dest, synthWishes);
        
        internal static void WriteWavHeader(this ConfigResolverAccessor entity, Stream dest, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, dest, synthWishes);
        
        internal static void WriteWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(filePath, entity?.Obj, synthWishes);
        
        internal static void WriteWavHeader(this byte[] dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(dest, entity?.Obj, synthWishes);
        
        internal static void WriteWavHeader(this Stream dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(dest, entity?.Obj, synthWishes);
        
        internal static void WriteWavHeader(this BinaryWriter dest, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(dest, entity?.Obj, synthWishes);

        // With ConfigSection
        
        internal static void WriteWavHeader(this ConfigSectionAccessor entity, string filePath)
            => _accessor.InvokeMethod(entity?.Obj, filePath);
        
        internal static void WriteWavHeader(this ConfigSectionAccessor entity, byte[] dest)
            => _accessor.InvokeMethod(entity?.Obj, dest);
        
        internal static void WriteWavHeader(this ConfigSectionAccessor entity, BinaryWriter dest)
            => _accessor.InvokeMethod(entity?.Obj, dest);
        
        internal static void WriteWavHeader(this ConfigSectionAccessor entity, Stream dest)
            => _accessor.InvokeMethod(entity?.Obj, dest);
        
        internal static void WriteWavHeader(this string filePath, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod(filePath, entity?.Obj);
        
        internal static void WriteWavHeader(this byte[] dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod(dest, entity?.Obj);
        
        internal static void WriteWavHeader(this Stream dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod(dest, entity?.Obj);
        
        internal static void WriteWavHeader(this BinaryWriter dest, ConfigSectionAccessor entity)
            => _accessor.InvokeMethod(dest, entity?.Obj);

    }
}

