using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;

namespace JJ.Business.Synthesizer.Wishes
{
    // Cache in SynthWishes

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(Func<FluentOutlet> func, string name = null, bool mustPad = false, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(func, inMemory: !MustCacheToDisk, mustPad, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(IList<FluentOutlet> channelInputs, string name = null, bool mustPad = false, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(channelInputs, inMemory: !MustCacheToDisk, mustPad, name, callerMemberName);
    }
}
