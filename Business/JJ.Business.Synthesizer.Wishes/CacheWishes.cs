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
        // Instance (Start-Of-Chain)
        
        // With Func
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                func, null,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, duration,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            Func<FlowNode> func, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                func, null,
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            Func<FlowNode> func, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                func, duration, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            FlowNode outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { outlet }, null, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            FlowNode outlet, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                new[] { outlet }, duration,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        // With FlowNode
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            FlowNode outlet, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                new[] { outlet }, duration, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            IList<FlowNode> channelInputs,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputs, null,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            IList<FlowNode> channelInputs, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, duration, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            IList<FlowNode> channelInputs, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channelInputs, duration, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        // Instance ChannelCache
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff> resultCallback)
            => ChannelCache(signal, (x, i) => resultCallback(x));
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff, int> resultCallback)
        {
            Tape tape = AddTape(signal);
            tape.ResultCallback = resultCallback;
            return signal;
        }

        // Statics (Buff to Buff)
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            Buff result, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            AudioFileOutput entity, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }

    // Statics Turned Instance
    
    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesCacheStaticsTurnedInstanceExtensions
    {
        // On Buffs
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            Buff result,
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
            Buff result,
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
        
        // ChannelCache on FlowNode
        
        public FlowNode ChannelCache(Action<Buff> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
        
        public FlowNode ChannelCache(Action<Buff, int> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
    }
    
    // Buff to Buff Extensions

    public static class CacheExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            this Buff result,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: true, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                entity, 
                inMemory: true, null, name, callerMemberName);
    }
}
