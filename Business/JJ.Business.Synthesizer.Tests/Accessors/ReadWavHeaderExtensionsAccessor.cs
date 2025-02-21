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
}

