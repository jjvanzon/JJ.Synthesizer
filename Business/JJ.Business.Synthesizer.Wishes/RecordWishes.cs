using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
                inMemory: true, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration,
                inMemory: true, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                func, null,
                inMemory: true, mustPad, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            Func<FlowNode> func, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration, 
                inMemory: true, mustPad, null, name, null, callerMemberName);

        // With FlowNode
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, null, 
                inMemory: true, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                new[] { signal }, duration,
                inMemory: true, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            FlowNode signal, FlowNode duration, bool mustPad, 
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                new[] { signal }, duration, 
                inMemory: true, mustPad, null, name, null, callerMemberName);
        
        // With List of FlowNodes
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channelSignals, null,
                inMemory: true, default, null, name, null, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration = null,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: true, default, null, name, null, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public Buff Record(
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration, 
                inMemory: true, mustPad, null, name, null, callerMemberName);
        
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
