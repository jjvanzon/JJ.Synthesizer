using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly Accessor _accessor;
        
        public SynthWishesAccessor(SynthWishes obj) 
            => _accessor = new Accessor(obj, typeof(SynthWishes));

        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes.CaptureIndexer _ 
            => (SynthWishes.CaptureIndexer)_accessor.GetFieldValue(nameof(_));

        public IList<FluentOutlet> FlattenTerms(FluentOutlet sumOrAdd) 
            => (IList<FluentOutlet>)_accessor.InvokeMethod(MemberName(), sumOrAdd);

        public IList<FluentOutlet> FlattenFactors(IList<FluentOutlet> operands) 
            => (IList<FluentOutlet>)_accessor.InvokeMethod(MemberName(), operands);

        public IList<FluentOutlet> FlattenFactors(FluentOutlet multiply) 
            => (IList<FluentOutlet>)_accessor.InvokeMethod(MemberName(), multiply);

        public void RunParallelsRecursive(FluentOutlet op) 
            => _accessor.InvokeMethod(MemberName(), op);

        // EchoAdditive
        
        public FluentOutlet EchoAdditive(FluentOutlet sound, int count, FluentOutlet magnitude = null, FluentOutlet delay = null, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay ?? _[0.25], callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet sound, int count, double magnitude, FluentOutlet delay = null, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet sound, int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);

        public FluentOutlet EchoAdditive(FluentOutlet sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null) 
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay, callerMemberName);

        // EchoFeedBack
        
        public FluentOutlet EchoFeedBack(FluentOutlet sound, int count, FluentOutlet magnitude = null, FluentOutlet delay = null, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay ?? _[0.25], callerMemberName);

        public FluentOutlet EchoFeedBack(FluentOutlet sound, int count, double magnitude, FluentOutlet delay = null, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FluentOutlet EchoFeedBack(FluentOutlet sound, int count, FluentOutlet magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);
        
        public FluentOutlet EchoFeedBack(FluentOutlet sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay, callerMemberName);
    }
}
