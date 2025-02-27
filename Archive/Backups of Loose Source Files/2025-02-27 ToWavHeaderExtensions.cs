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
    internal static partial class WavExtensionWishesAccessor
    {
        internal static WavHeaderStruct ToWavHeader(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<WavHeaderStruct>(entity?.Obj, synthWishes);
        
        internal static WavHeaderStruct ToWavHeader(this ConfigSectionAccessor entity)            
            => _accessor.InvokeMethod<WavHeaderStruct>(entity?.Obj);

    }
}

