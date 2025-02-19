using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class ReadWavHeaderExtensionsAccessor
    {
        private static readonly AccessorEx _accessor = new AccessorEx(typeof(ReadWavHeaderExtensions));
        
        internal static void ReadWavHeader(this ConfigResolverAccessor entity, string filePath, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, filePath, synthWishes);
        internal static void ReadWavHeader(this ConfigResolverAccessor entity, byte[] source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
        internal static void ReadWavHeader(this ConfigResolverAccessor entity, Stream source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
        internal static void ReadWavHeader(this ConfigResolverAccessor entity, BinaryReader source, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
        
        internal static void ReadWavHeader(this string filePath, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, filePath, synthWishes);
        internal static void ReadWavHeader(this byte[] source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
        internal static void ReadWavHeader(this Stream source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
        internal static void ReadWavHeader(this BinaryReader source, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity?.Obj, source, synthWishes);
    }
}

