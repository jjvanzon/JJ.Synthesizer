using System;
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
            IList<FlowNode> channels,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channels, null,
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            IList<FlowNode> channels, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channels, duration, 
                inMemory: !GetDiskCacheOn, mustPad: false, null, name, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Cache(
            IList<FlowNode> channels, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                channels, duration, 
                inMemory: !GetDiskCacheOn, mustPad, null, name, callerMemberName);
        
        // Instance ChannelCache
        
        public FlowNode ChannelCache(FlowNode signal)
            => ChannelCache(signal, null, (Action<Buff, int>)null);
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff> callback)
            => ChannelCache(signal, null, callback);
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff, int> callback)
            => ChannelCache(signal, null, callback);
        
        public FlowNode ChannelCache(FlowNode signal, FlowNode duration, Action<Buff> callback)
            => ChannelCache(signal, duration, (x, i) => callback(x));
        
        public FlowNode ChannelCache(FlowNode signal, FlowNode duration, Action<Buff, int> callback)
        {
            Tape tape = AddTape(signal);
            tape.Callback = callback;
            tape.Duration = duration;
            tape.IsCache = true;
            return signal;
        }

        // Statics (Buff to Buff)
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            Buff buff, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                buff, 
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
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            name = synthWishes.FetchName(buff?.FilePath, callerMemberName, explicitName: name);

            StreamAudio(
                buff, 
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
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { 
                StreamAudio(
                    buff, 
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
        
        public FlowNode ChannelCache()
            => _synthWishes.ChannelCache(this);
        
        public FlowNode ChannelCache(Action<Buff> callback)
            => _synthWishes.ChannelCache(this, callback);
        
        public FlowNode ChannelCache(Action<Buff, int> callback)
            => _synthWishes.ChannelCache(this, callback);
        
        public FlowNode ChannelCache(FlowNode duration, Action<Buff> callback)
            => _synthWishes.ChannelCache(this, duration, callback);
        
        public FlowNode ChannelCache(FlowNode duration, Action<Buff, int> callback)
            => _synthWishes.ChannelCache(this, duration, callback);
    }
    
    // Buff to Buff Extensions

    public static class CacheExtensionWishes
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Cache(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                buff, 
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
