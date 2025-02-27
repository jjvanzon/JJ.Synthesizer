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
    internal static partial class WavExtensionWishesAccessor
    {
        private static AccessorEx _accessor = new AccessorEx(typeof(WavExtensionWishes));
        
        internal static ConfigResolverAccessor ApplyInfo(this ConfigResolverAccessor obj, AudioInfoWish infoWish, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj, infoWish, synthWishes));
        internal static AudioInfoWish ApplyInfo(this AudioInfoWish infoWish, ConfigResolverAccessor obj, SynthWishes synthWishes)
            => _accessor.InvokeMethod<AudioInfoWish>(infoWish, obj.Obj, synthWishes);
    }
}
