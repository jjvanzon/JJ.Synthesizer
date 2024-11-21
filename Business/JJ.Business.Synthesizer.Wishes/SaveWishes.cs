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
        // Save on Instance
        
        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioData Save(
            Func<FlowNode> channelInputFunc, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputFunc, 
                inMemory: false, mustPad: true, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioData Save(
            FlowNode channelInput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInput,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public StreamAudioData Save(
            IList<FlowNode> channelInputs, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                channelInputs, 
                inMemory: false, mustPad: true, null, filePath, callerMemberName);

        // Save in Statics
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioData Save(
            StreamAudioData result,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioData Save(
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
    
    // Save on Statics Turned Instance

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
        // Make statics available on instances by using extension methods.

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            StreamAudioData data, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            filePath = synthWishes.FetchName(data?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                data, 
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
        public FlowNode SaveMono(
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            WithMono();
            WithCenter();

            _synthWishes.StreamAudio(
                this, 
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
            
            return this;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            StreamAudioData data, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            filePath = _synthWishes.FetchName(data?.FilePath, callerMemberName, explicitName: filePath);
            
            SynthWishes.StreamAudio(
                data, 
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
    }

    // Save on Entity / Results / Data

    public static class SaveExtensionWishes 
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioData Save(
            this StreamAudioData result,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.StreamAudio(
                result, 
                inMemory: false, null, filePath, callerMemberName);    

        /// <inheritdoc cref="docs._saveorplay" />
        public static StreamAudioData Save(
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
