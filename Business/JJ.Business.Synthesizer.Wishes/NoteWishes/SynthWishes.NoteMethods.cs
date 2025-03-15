using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Framework.Existence.Core.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Note Operator
        
        /// <inheritdoc cref="_note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay = default, FlowNode volume = default, FlowNode noteLength = default, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            // A little optimization, because so slow...
            bool delayFilledIn = delay != null && delay.AsConst != 0;
            bool volumeFilledIn = volume != null && volume.AsConst != 1;

            // Resolve Name
            string resolvedName = ResolveName(name, sound, callerMemberName);
            if (FilledIn(resolvedName))
            {
                resolvedName += " " + MemberName();
            }
            
            // Resolve NoteLength
            noteLength = GetNoteLengthSnapShot(noteLength);
            
            // Apply Volume
            if (volumeFilledIn)
            {
                sound *= volume.Stretch(noteLength / GetVolumeDuration(volume));
            }
            
            // Defer Taping
            sound = sound.Tape(noteLength).SetName(resolvedName);
            
            // Apply Delay
            if (delayFilledIn) sound = Delay(sound, delay);
            
            // Extend AudioLength
            EnsureAudioLength(delay + noteLength);
            
            return sound.SetName(resolvedName);
        }
        
        /// <inheritdoc cref="_note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume, FlowNode noteLength, 
            string name = null, [CallerMemberName] string callerMemberName = null)
            => Note(sound, delay, _[volume], noteLength, name, callerMemberName);
        
        /// <inheritdoc cref="_note" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => Note(sound, delay, _[volume], default, name, callerMemberName);
               
        private static double GetVolumeDuration(FlowNode volume)
        {
            if (volume.IsCurve)
            {
                return volume.UnderlyingCurve().Nodes.Max(x => x.Time);
            }
            
            if (volume.IsSample)
            {
                return volume.UnderlyingSample().GetDuration();
            }
            
            return 1;
        }
    }
}

