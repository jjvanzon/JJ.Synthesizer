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
        public Result<StreamAudioData> Cache(
            Func<FluentOutlet> func, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Cache(
            Func<FluentOutlet> func, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Cache(
            FluentOutlet outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { outlet }, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Cache(
            FluentOutlet outlet, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                new[] { outlet }, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Cache(
            IList<FluentOutlet> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, 
                inMemory: !MustCacheToDisk, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Result<StreamAudioData> Cache(
            IList<FluentOutlet> channelInputs, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);
    
        // Cache in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<StreamAudioData> Cache(
            Result<StreamAudioData> result, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<StreamAudioData> Cache(
            StreamAudioData data, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                data, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<StreamAudioData> Cache(
            AudioFileOutput entity, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }

    // Cache on Statics Turned Instance
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesCacheStaticsTurnedInstanceExtensions
    {
        // Statics made available on instances, by using extension methods.
                
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            Result<StreamAudioData> result,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            name = synthWishes.FetchName(result?.Data?.AudioFileOutput?.FilePath, callerMemberName, explicitName: name);

            StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            StreamAudioData data,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            name = synthWishes.FetchName(data?.AudioFileOutput?.FilePath, callerMemberName, explicitName: name);

            StreamAudio(
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            name = synthWishes.FetchName(entity?.FilePath, callerMemberName, explicitName: name);

            StreamAudio(
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

            _synthWishes.StreamAudio(
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
            
            _synthWishes.StreamAudio(
                this, 
                inMemory: !MustCacheToDisk, mustPad, null, name, callerMemberName);

            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            Result<StreamAudioData> result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { 
                StreamAudio(
                    result, 
                    inMemory: true, null, name, callerMemberName);

                return this; 
            }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            StreamAudioData data,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            StreamAudio(
                data,
                inMemory: true, null, name, callerMemberName);
            
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FluentOutlet Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            StreamAudio(
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
            this Result<StreamAudioData> result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Result Cache(
            this StreamAudioData data,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                data, 
                inMemory: true, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Result<StreamAudioData> Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }
}
