using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public CaptureIndexer _ 
            => (CaptureIndexer)_accessor.GetFieldValue(nameof(_));

        public IList<FlowNode> FlattenTerms(FlowNode sumOrAdd) 
            => (IList<FlowNode>)_accessor.InvokeMethod(MemberName(), sumOrAdd);

        public IList<FlowNode> FlattenFactors(IList<FlowNode> operands) 
            => (IList<FlowNode>)_accessor.InvokeMethod(MemberName(), operands);

        public IList<FlowNode> FlattenFactors(FlowNode multiply) 
            => (IList<FlowNode>)_accessor.InvokeMethod(MemberName(), multiply);

        public TapeRunnerAccessor _tapeRunner
        {
            get
            {
                object tapeRunner = _accessor.GetFieldValue(MemberName());
                return new TapeRunnerAccessor(tapeRunner);
            }
        }
        
        // EchoAdditive
        
        public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay ?? _[0.25], callerMemberName);

        public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);

        public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null) 
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay, callerMemberName);

        // EchoFeedBack
        
        public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay ?? _[0.25], callerMemberName);

        public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);
        
        public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), sound, count, magnitude, delay, callerMemberName);
    }
}
