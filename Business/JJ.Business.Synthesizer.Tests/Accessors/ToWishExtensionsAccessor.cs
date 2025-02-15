using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class ToWishExtensionsAccessor
    {
        private static readonly AccessorEx _accessor = new AccessorEx(typeof(ToWishExtensions));
        
        internal static AudioInfoWish ToWish(this ConfigResolverAccessor entity, SynthWishes synthWishes)
            => _accessor.InvokeMethod<AudioInfoWish>(entity?.Obj, synthWishes);
        
        internal static AudioInfoWish ToWish(this ConfigSectionAccessor entity)            
            => _accessor.InvokeMethod<AudioInfoWish>(entity?.Obj);
    }
}

