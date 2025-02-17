using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
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
    }
}

