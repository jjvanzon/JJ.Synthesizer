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
    internal static class ApplyInfoExtensionsAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(ApplyInfoExtensions));
        
        internal static void ApplyInfo(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
            => _accessor.InvokeMethod(obj.Obj, infoWish, synthWishes);
        internal static void ApplyInfo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
            => _accessor.InvokeMethod(infoWish, obj.Obj, synthWishes);
    }
}
