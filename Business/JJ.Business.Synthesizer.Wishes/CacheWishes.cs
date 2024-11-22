using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Cache in SynthWishes

    public partial class SynthWishes
    {
        // Cache on Instance
        
        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            Func<FlowNode> func, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, null,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            Func<FlowNode> func, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, null, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            FlowNode outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { outlet }, null, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            FlowNode outlet, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                new[] { outlet }, null, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            IList<FlowNode> channelInputs, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, null, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public AudioStreamResult Cache(
            IList<FlowNode> channelInputs, 
            bool mustPad, string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, null, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        // ChannelCache on Instance
        
        public FlowNode ChannelCache(FlowNode signal, Action<AudioStreamResult> resultCallback)
            => ChannelCache(signal, (x, i) => resultCallback(x));
        
        public FlowNode ChannelCache(FlowNode signal, Action<AudioStreamResult, int> resultCallback)
        {
            Tape tape = AddTape(signal);
            tape.ResultCallback = resultCallback;
            return signal;
        }

        // Cache in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Cache(
            AudioStreamResult result, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Cache(
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
            AudioStreamResult result,
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
        public FlowNode Cache(
            AudioStreamResult result,
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
        
        // ChannelCache
        
        public FlowNode ChannelCache(Action<AudioStreamResult> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
        
        public FlowNode ChannelCache(Action<AudioStreamResult, int> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
    }
    
    // Cache on Entity / Results / Data

    public static class CacheExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Cache(
            this AudioStreamResult result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static AudioStreamResult Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }
}
