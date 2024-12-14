using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // Record in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)
        
        // With Func
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                func, null,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                func, null,
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);

        // With FlowNode
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, null, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, duration,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                new[] { signal }, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // With List of FlowNodes
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channelSignals, null,
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: !GetCacheToDisk, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: !GetCacheToDisk, mustPad, null, name, null, callerMemberName);
        
        // Instance Intercept (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, 
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, null, x => { callback(x); return x; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, null, x => { callback(x); return x; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, filePath, x => { callback(x); return x; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, filePath, x => { callback(x); return x; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(signal, duration, callback, null, filePath, callerMemberName);
            tape.IsIntercept = true;
            return signal;
        }

        // Instance InterceptChannel (Mid-Chain)
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel,
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, null, (b, i) => { callback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, null, (b, i) => { callback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, string filePath, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, filePath, (b, i) => { callback(b, i); return b; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Action<Buff, int> channelCallback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, filePath, (b, i) => { channelCallback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Func<Buff, int, Buff> channelCallback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, duration, null, channelCallback, filePath, callerMemberName);
            tape.IsInterceptChannel = true;
            return channel;
        }

        // Statics (Buff to Buff) (End-Of-Chain)
        
        public static Buff Record(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

        public static Buff Record(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                entity, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }

    // Statics Turned Instance (End-of-Chain)
    
    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesRecordStaticsTurnedInstanceExtensions
    {
        // On Buffs (End-of-Chain)
        
        public static SynthWishes Record(
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

        public static SynthWishes Record(
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
        // FlowNode Intercept (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, filePath, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode InterceptChannel (Mid-Chain)
       
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            string filePath, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, filePath, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, string filePath,
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, filePath, callback, callerMemberName);
        
        // Record on FlowNode (End-of-Chain)
            
        public FlowNode Record(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            {
                SynthWishes.MakeBuff(
                    buff, 
                    inMemory: true, GetExtraBufferFrames, null, name, null, callerMemberName);

                return this; 
            }

        public FlowNode Record(
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

    public static class RecordExtensionWishes
    {
        public static Buff Record(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                buff, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
        
        public static Buff Record(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                entity, 
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }
}
