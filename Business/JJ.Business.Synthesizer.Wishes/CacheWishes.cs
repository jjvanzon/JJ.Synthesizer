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
        
        // Instance CacheChannel (Mid-Chain)
        
        public FlowNode CacheChannel(
            FlowNode channel, [CallerMemberName] string name = null)
            => CacheChannel(channel, null, null, default(Func<Buff, int, Buff>), name);
        
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, [CallerMemberName] string name = null)
            => CacheChannel(channel, duration, null, default(Func<Buff, int, Buff>), name);
        
        public FlowNode CacheChannel(
            FlowNode channel, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, null, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, duration, null, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, null, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, duration, null, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, filePath, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, null, filePath, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => CacheChannel(channel, duration, filePath, (x, y) => callback(x), callerMemberName);

        public FlowNode CacheChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = AddTape(channel, callerMemberName);
            tape.WithCacheChannel = true;
            tape.FilePath = filePath;
            tape.Duration = duration;
            tape.ChannelCallback = callback;
            return channel;
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
        // On Buffs (End-of-Chain)
        
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
        // CacheChannel on FlowNode (Mid-Chain)
        
        public FlowNode CacheChannel(
            [CallerMemberName] string name = null)
            => _synthWishes.CacheChannel(this, name);
        
        public FlowNode CacheChannel(
            FlowNode duration, [CallerMemberName] string name = null)
            => _synthWishes.CacheChannel(this, duration, name);

        public FlowNode CacheChannel(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, callback, callerMemberName);

        public FlowNode CacheChannel(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, callback, callerMemberName);

        public FlowNode CacheChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, callback, callerMemberName);

        public FlowNode CacheChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, callback, callerMemberName);

        public FlowNode CacheChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, filePath, callback, callerMemberName);
        
        public FlowNode CacheChannel(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, filePath, callback, callerMemberName);

        public FlowNode CacheChannel(
            FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, filePath, callback, callerMemberName);

        public FlowNode CacheChannel(
            FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.CacheChannel(this, duration, filePath, callback, callerMemberName);

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
