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
    internal static class WavHeaderWishesAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(WavHeaderWishes));
        
        internal static AudioInfoWish ToWish(ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj, synthWishes);

        internal static AudioInfoWish ToWish(ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj);
    }
}
