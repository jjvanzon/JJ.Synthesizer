using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class WavHeaderWishesAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(WavHeaderWishes));
        
        // With ConfigResolver
        
        internal static AudioInfoWish ToWish(ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj, synthWishes);

        internal static void ApplyTo(AudioInfoWish infoWish, ConfigResolverAccessor entity, SynthWishes synthWishes) 
            => _accessor.InvokeMethod(infoWish, entity.Obj, synthWishes);

        internal static void FromWish(ConfigResolverAccessor entity, AudioInfoWish infoWish, SynthWishes synthWishes)
            => _accessor.InvokeMethod(entity.Obj, infoWish, synthWishes);
        
        // With ConfigSection
        
        internal static AudioInfoWish ToWish(ConfigSectionAccessor entity)
            => _accessor.InvokeMethod<AudioInfoWish>(entity.Obj);

    }
}
