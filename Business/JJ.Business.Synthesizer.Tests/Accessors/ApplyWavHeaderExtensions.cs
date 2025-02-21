using System;
using System.Collections.Generic;
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
    internal static class FromWavHeaderExtensionsAccessor
    {
        private static readonly AccessorEx _accessor = new AccessorEx(typeof(ApplyWavHeaderExtensions));
        
        internal static ConfigResolverAccessor ApplyWavHeader(this ConfigResolverAccessor entity, WavHeaderStruct wavHeader, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(entity?.Obj, wavHeader, synthWishes));
        
        internal static WavHeaderStruct ApplyWavHeader(this WavHeaderStruct wavHeader, ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<WavHeaderStruct>(wavHeader, entity?.Obj, synthWishes);

    }
}

