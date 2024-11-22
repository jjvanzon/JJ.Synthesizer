using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Wishes
{
    // Save in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            Func<FlowNode> channelInputFunc, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputFunc, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            Func<FlowNode> channelInputFunc, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputFunc, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channelInput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInput, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channelInput, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInput, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channelInputs,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputs, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channelInputs, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputs, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        // Instance ChannelSave
        
        public FlowNode ChannelSave(FlowNode signal)
            => ChannelSave(signal, null, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode signal, string filePath)
            => ChannelSave(signal, filePath, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode signal, Action<Buff> callback)
            => ChannelSave(signal, null, callback);
        
        public FlowNode ChannelSave(FlowNode signal, Action<Buff, int> callback)
            => ChannelSave(signal, null, callback);
        
        public FlowNode ChannelSave(FlowNode signal, string filePath, Action<Buff> callback)
            => ChannelSave(signal, filePath, (x, y) => callback(x));
        
        public FlowNode ChannelSave(FlowNode signal, string filePath, Action<Buff, int> callback)
        {
            Tape tape = AddTape(signal);
            tape.MustSave = true;
            tape.FilePath = filePath;
            tape.Callback = callback;
            return signal;
        }

        // Save in Statics (Buff to Buff)

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            Buff result,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            AudioFileOutput entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                entity, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => Save(entity.Bytes, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            filePath = StaticFetchName(callerMemberName, explicitName: filePath);
            File.WriteAllBytes(filePath, bytes);
        }
    }
    
    // Statics Turned Instance (from Buff)

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Buff result, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(result?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);
            
            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            AudioFileOutput entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(entity?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                entity, 
                inMemory: false, null, filePath, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(entity?.Location, callerMemberName, explicitName: filePath);
            
            SynthWishes.Save(entity, filePath, callerMemberName);

            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes,
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(callerMemberName, explicitName: filePath);
            
            SynthWishes.Save(bytes, filePath);
            
            return synthWishes;
        }
    }

    // Save on FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            Buff result, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            filePath = _synthWishes.FetchName(result?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);

            return this;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            AudioFileOutput entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            filePath = _synthWishes.FetchName(entity?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                entity, 
                inMemory: false, null, filePath, callerMemberName);
            
            return this; 
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            filePath = _synthWishes.FetchName(entity?.Location, callerMemberName, explicitName: filePath);

            SynthWishes.Save(
                entity, 
                filePath, callerMemberName); 
            
            return this; 
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            filePath = _synthWishes.FetchName(callerMemberName, explicitName: filePath);

            SynthWishes.Save(
                bytes,
                filePath, callerMemberName); 

            return this; 
        }
        
        // ChannelSave
        
        public FlowNode ChannelSave()
            => _synthWishes.ChannelSave(this);
        
        public FlowNode ChannelSave(string filePath)
            => _synthWishes.ChannelSave(this, filePath);
        
        public FlowNode ChannelSave(Action<Buff> callback)
            => _synthWishes.ChannelSave(this, callback);
        
        public FlowNode ChannelSave(Action<Buff, int> callback)
            => _synthWishes.ChannelSave(this, callback);
        
        public FlowNode ChannelSave(string filePath, Action<Buff> callback)
            => _synthWishes.ChannelSave(this, filePath, callback);
        
        public FlowNode ChannelSave(string filePath, Action<Buff, int> callback)
            => _synthWishes.ChannelSave(this, filePath, callback);
    }

    // Buff Extensions

    public static class SaveExtensionWishes 
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            this Buff result,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);    

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            this AudioFileOutput entity,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.StreamAudio(
                entity, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            this Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                entity,
                filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            this byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                bytes, 
                filePath, callerMemberName);
    }
}
