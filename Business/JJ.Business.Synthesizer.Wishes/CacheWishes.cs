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
        // Cache on Instance
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            Func<FluentOutlet> func, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(
                func, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            Func<FluentOutlet> func, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(
                func, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            FluentOutlet outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                new[] { outlet }, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            FluentOutlet outlet, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(
                new[] { outlet }, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            IList<FluentOutlet> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(
                channelInputs, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<SaveResultData> Cache(
            IList<FluentOutlet> channelInputs, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => WriteAudio(
                channelInputs, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);
    
        // Cache in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(
            Result<SaveResultData> result, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(
            SaveResultData data, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                data, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(
            AudioFileOutput entity, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }

    // Save on SynthWishes Instances
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesCacheExtensions
    {
        // Overload SynthWishes statics, to make available on instances, by using extension methods.
                
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, Result<SaveResultData> saveResult,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(saveResult, name, callerMemberName);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, SaveResultData saveResultData,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(saveResultData, name, callerMemberName);
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            SynthWishes.Cache(entity, name, callerMemberName);
            return synthWishes;
        }
    }

    // Cache on FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet CacheMono(
            string name = null, [CallerMemberName] string callerMemberName = null)
            => CacheMono(mustPad: false, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet CacheMono(
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            _synthWishes.Channel = ChannelEnum.Single;
            _synthWishes.WithMono().Cache(() => this, mustPad, name, callerMemberName);
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            Result<SaveResultData> result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { _synthWishes.Cache(result, name, callerMemberName); return this; }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            SaveResultData result,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            { _synthWishes.Cache(result, name, callerMemberName); return this; }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            { _synthWishes.Cache(entity, name, callerMemberName); return this; }
    }
    
    // Cache on Entity / Results / Data

    public static class CacheExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(
            this Result<SaveResultData> saveResult,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.Cache(saveResult, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(
            this SaveResultData saveResultData,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.Cache(saveResultData, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.Cache(entity, name, callerMemberName);
    }
}
