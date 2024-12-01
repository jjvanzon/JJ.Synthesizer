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
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                func, null,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            Func<FlowNode> func, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                func, null,
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            Func<FlowNode> func, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);

        // With FlowNode
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            FlowNode outlet, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { outlet }, null, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            FlowNode outlet, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { outlet }, duration,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            FlowNode outlet, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                new[] { outlet }, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // With List of FlowNodes
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channels,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channels, null,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channels, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channels, duration, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channels, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channels, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // Instance ChannelCache (Mid-Chain)
        
        public FlowNode ChannelCache(FlowNode signal, [CallerMemberName] string callerMemberName = null)
            => ChannelCache(signal, null, (Action<Buff, int>)null, callerMemberName);
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelCache(signal, null, callback, callerMemberName);
        
        public FlowNode ChannelCache(FlowNode signal, Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelCache(signal, null, callback, callerMemberName);
        
        public FlowNode ChannelCache(FlowNode signal, FlowNode duration, Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelCache(signal, duration, (x, i) => callback(x), callerMemberName);
        
        public FlowNode ChannelCache(FlowNode signal, FlowNode duration, Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = AddTape(signal, callerMemberName);
            tape.Callback = callback;
            tape.Duration = duration;
            tape.IsCache = true;
            return signal;
        }

        // Statics (Buff to Buff) (End-Of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Cache(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff,
                inMemory: true, ConfigResolver.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                entity, 
                inMemory: true, ConfigResolver.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }

    // Statics Turned Instance (End-of-Chain)
    
    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesCacheStaticsTurnedInstanceExtensions
    {
        // On Buffs
        
        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            MakeBuff(
                buff,
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            MakeBuff(
                entity, 
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);
            
            return synthWishes;
        }
    }

    public partial class FlowNode
    {
        // Cache on FlowNode (End-of-Chain)
            
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            { 
                MakeBuff(
                    buff, 
                    inMemory: true, GetExtraBufferFrames, null, name, null, callerMemberName);

                return this; 
            }

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            MakeBuff(
                entity,
                inMemory: true, GetExtraBufferFrames, null, name, null, callerMemberName);

            return this;
        }
        
        // ChannelCache on FlowNode (Mid-Chain)
        
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
    
    // Buff to Buff Extensions (End-of-Chain)

    public static class CacheExtensionWishes
    {
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Cache(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff, 
                inMemory: true, ConfigResolver.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                entity, 
                inMemory: true, ConfigResolver.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }
}
