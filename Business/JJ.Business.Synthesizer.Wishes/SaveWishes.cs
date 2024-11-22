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
            Func<FlowNode> func, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                func, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                func, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channel, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channel, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channel, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channels,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channels, null,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channels, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channels, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        
        // Instance ChannelSave
        
        public FlowNode ChannelSave(FlowNode channel)
            => ChannelSave(channel, null, null, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration)
            => ChannelSave(channel, duration, null, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, string filePath)
            => ChannelSave(channel, null, filePath, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath)
            => ChannelSave(channel, duration, filePath, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, Action<Buff> callback)
            => ChannelSave(channel, null, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, Action<Buff> callback)
            => ChannelSave(channel, duration, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, Action<Buff, int> callback)
            => ChannelSave(channel, null, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, Action<Buff, int> callback)
            => ChannelSave(channel, duration, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, string filePath, Action<Buff, int> callback)
            => ChannelSave(channel, null, filePath, callback);
        
        public FlowNode ChannelSave(FlowNode channel, string filePath, Action<Buff> callback)
            => ChannelSave(channel, null, filePath, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath, Action<Buff> callback)
            => ChannelSave(channel, duration, filePath, (x, y) => callback(x));

        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath, Action<Buff, int> callback)
        {
            Tape tape = AddTape(channel);
            tape.MustSave = true;
            tape.FilePath = filePath;
            tape.Duration = duration;
            tape.Callback = callback;
            return channel;
        }

        // Save in Statics (Buff to Buff)

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                buff, 
                inMemory: false, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            AudioFileOutput a, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => StreamAudio(
                a, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => Save(sample.Bytes, filePath, callerMemberName);

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
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(buff?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                buff, 
                inMemory: false, null, filePath, callerMemberName);
            
            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(audioFileOutput?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                audioFileOutput, 
                inMemory: false, null, filePath, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(sample?.Location, callerMemberName, explicitName: filePath);
            
            SynthWishes.Save(sample, filePath, callerMemberName);

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
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            filePath = _synthWishes.FetchName(buff?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                buff, 
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
            this Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.StreamAudio(
                buff, 
                inMemory: false, null, filePath, callerMemberName);    

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            this AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.StreamAudio(
                audioFileOutput, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            this Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                sample,
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
