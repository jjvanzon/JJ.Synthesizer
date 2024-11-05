using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;

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
    
        // Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(Result<SaveResultData> saveResult)
        {
            if (saveResult == null) throw new ArgumentNullException(nameof(saveResult));
            return Cache(saveResult.Data);
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(SaveResultData saveResultData)
        {
            if (saveResultData == null) throw new ArgumentNullException(nameof(saveResultData));
            return Cache(saveResultData.AudioFileOutput);
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(AudioFileOutput entity) 
            => WriteAudio(entity, inMemory: true);
    }

    // Save on SynthWishes Instances
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesCacheExtensions
    {
        // Make SynthWishes statics available on instances by using extension methods.
                
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(this SynthWishes synthWishes, Result<SaveResultData> saveResult) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(saveResult);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(this SynthWishes synthWishes, SaveResultData saveResultData) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(saveResultData);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(this SynthWishes synthWishes, AudioFileOutput entity) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(entity);
            return synthWishes;
        }
    }

    // Cache on FluentOutlet


    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet CacheMono(string name = null, bool mustPad = false, [CallerMemberName] string callerMemberName = null) 
        {
            _x.Channel = ChannelEnum.Single;
            _x.WithMono().Cache(() => this, name, mustPad, callerMemberName);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(Result<SaveResultData> result)
        {
            _x.Cache(result);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(SaveResultData result)
        {
            _x.Cache(result);
            return this;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(AudioFileOutput entity) 
        {
            _x.Cache(entity); 
            return this; 
        }
    }
    
    // Cache on Entity / Results / Data

    public static class CacheExtensions 
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(this Result<SaveResultData> saveResult) => SynthWishes.Cache(saveResult);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(this SaveResultData saveResultData) => SynthWishes.Cache(saveResultData);
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(this AudioFileOutput entity) => SynthWishes.Cache(entity);
    }
}
