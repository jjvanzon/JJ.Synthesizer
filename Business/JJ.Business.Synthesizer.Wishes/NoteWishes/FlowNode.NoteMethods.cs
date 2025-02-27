using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode delay = null, FlowNode volume = default, FlowNode duration = default,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => _synthWishes.Note(this, delay, volume, duration, name, callerMemberName);
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode sound, FlowNode delay, double volume, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Note(sound, delay, volume, duration, name, callerMemberName);
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode Note(
            FlowNode delay, double volume,
            string name = null, [CallerMemberName] string callerMemberName = null) 
            => _synthWishes.Note(this, delay, volume, name, callerMemberName);
    }
}
