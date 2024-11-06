using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.CanonicalModel;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

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

    // Cache on Statics Turned Instance
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesCacheExtensions
    {
        // Statics made available on instances, by using extension methods.
                
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            Result<SaveResultData> result,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            WriteAudio(
                result, 
                inMemory: true, null, name, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            SaveResultData data,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            WriteAudio(
                data, 
                inMemory: true, null, name, callerMemberName);

            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            WriteAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
            
            return synthWishes;
        }
    }

    // Cache on FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet CacheMono(
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();

            _synthWishes.WriteAudio(
                this, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);
            
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet CacheMono(
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();
            
            _synthWishes.WriteAudio(
                this, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);

            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            Result<SaveResultData> result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { 
                WriteAudio(
                    result, 
                    inMemory: true, null, name, callerMemberName);

                return this; 
            }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            SaveResultData data,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WriteAudio(
                data,
                inMemory: true, null, name, callerMemberName);
            
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WriteAudio(
                entity,
                inMemory: true, null, name, callerMemberName);

            return this;
        }
    }
    
    // Cache on Entity / Results / Data

    public static class CacheExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(
            this Result<SaveResultData> result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(
            this SaveResultData data,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                data, 
                inMemory: true, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<SaveResultData> Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => WriteAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }
}
