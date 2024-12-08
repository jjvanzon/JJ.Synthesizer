using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

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
        public Buff MaterializeCache(
            FlowNode signal, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, null, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff MaterializeCache(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, duration,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff MaterializeCache(
            FlowNode signal, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                new[] { signal }, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // With List of FlowNodes
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channelSignals, null,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Cache(
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // Instance Cache (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, [CallerMemberName] string name = null)
            => Cache(signal, null, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, FlowNode duration, [CallerMemberName] string name = null)
            => Cache(signal, duration, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Cache(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Cache(signal, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Cache(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode signal, FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(signal, duration, filePath, callerMemberName);
            tape.IsCache = true;
            tape.Callback = callback;
            return signal;
        }

        // Instance CacheChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel, [CallerMemberName] string name = null)
            => CacheChannel(channel, null, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, [CallerMemberName] string name = null)
            => CacheChannel(channel, duration, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, duration, filePath, callerMemberName);
            tape.IsCacheChannel = true;
            tape.ChannelCallback = callback;
            return channel;
        }

        // Statics (Buff to Buff) (End-Of-Chain)
        
        public static Buff Cache(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

        public static Buff Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                entity, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }

    // Statics Turned Instance (End-of-Chain)
    
    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesCacheStaticsTurnedInstanceExtensions
    {
        // On Buffs (End-of-Chain)
        
        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            
            SynthWishes.MakeBuff(
                buff,
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);
            
            return synthWishes;
        }

        public static SynthWishes Cache(
            this SynthWishes synthWishes, 
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            SynthWishes.MakeBuff(
                entity, 
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);
            
            return synthWishes;
        }
    }

    public partial class FlowNode
    {
        // FlowNode Cache (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            [CallerMemberName] string name = null)
            => _synthWishes.Cache(this, null, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode duration, [CallerMemberName] string name = null)
            => _synthWishes.Cache(this, duration, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Cache(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Cache(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Cache(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Cache(
            FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Cache(this, duration, filePath, callback, callerMemberName);

        // FlowNode CacheChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            [CallerMemberName] string name = null)
            => _synthWishes.CacheChannel(this, null, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode duration, [CallerMemberName] string name = null)
            => _synthWishes.CacheChannel(this, duration, null, null, name);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode CacheChannel(
            FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, filePath, callback, callerMemberName);
        
        // Cache on FlowNode (End-of-Chain)
            
        public FlowNode Cache(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            {
                SynthWishes.MakeBuff(
                    buff, 
                    inMemory: true, GetExtraBufferFrames, null, name, null, callerMemberName);

                return this; 
            }

        public FlowNode Cache(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            SynthWishes.MakeBuff(
                entity,
                inMemory: true, GetExtraBufferFrames, null, name, null, callerMemberName);

            return this;
        }
}
    
    // Buff to Buff Extensions (End-of-Chain)

    public static class CacheExtensionWishes
    {
        public static Buff Cache(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                buff, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
        
        public static Buff Cache(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                entity, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }
}
