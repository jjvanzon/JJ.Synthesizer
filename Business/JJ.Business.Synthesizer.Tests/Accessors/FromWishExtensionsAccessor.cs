using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class FromWishExtensionsAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(FromWishExtensions));
        
        internal static void FromWish(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
            => _accessor.InvokeMethod(obj.Obj, infoWish, synthWishes);
        internal static void ApplyTo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
            => _accessor.InvokeMethod(infoWish, obj.Obj, synthWishes);
    }
}
