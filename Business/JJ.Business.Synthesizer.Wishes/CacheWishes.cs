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
        public StreamAudioResult Cache(
            Func<FlowNode> func, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioResult Cache(
            Func<FlowNode> func, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioResult Cache(
            FlowNode outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { outlet }, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioResult Cache(
            FlowNode outlet, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                new[] { outlet }, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioResult Cache(
            IList<FlowNode> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioResult Cache(
            IList<FlowNode> channelInputs, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
    
        // Cache in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioResult Cache(
            StreamAudioResult result, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioResult Cache(
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
            StreamAudioResult result,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            name = synthWishes.FetchName(result?.FilePath, callerMemberName, explicitName: name);

            StreamAudio(
                result, 
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

    // Cache on FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode CacheMono(
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();

            _synthWishes.StreamAudio(
                this, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
            
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode CacheMono(
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            WithMono();
            WithCenter();
            
            _synthWishes.StreamAudio(
                this, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);

            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Cache(
            StreamAudioResult result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { 
                StreamAudio(
                    result, 
                    inMemory: true, null, name, callerMemberName);

                return this; 
            }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Cache(
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

    public static class CacheExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioResult Cache(
            this StreamAudioResult result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioResult Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }
}
