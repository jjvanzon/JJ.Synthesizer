using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly AccessorEx _accessor;

        public SynthWishesAccessor(SynthWishes obj)
        {
            _accessor = new AccessorEx(obj, typeof(SynthWishes));
        }
        
        /// <inheritdoc cref="wishdocs._captureindexer" />
        public SynthWishes _ 
            => (SynthWishes)_accessor.GetPropertyValue(nameof(_));

        public void Run(Action action)
            => _accessor.InvokeMethod(MemberName(), action);

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
                object obj = _accessor.GetFieldValue(MemberName());
                return new TapeRunnerAccessor(obj);
            }
        }

        public TapeCollectionAccessor _tapes
        {
            get
            {
                object obj = _accessor.GetFieldValue(MemberName());
                return new TapeCollectionAccessor(obj);
            }
        }

        public ConfigResolverAccessor Config 
            => new ConfigResolverAccessor(_accessor.GetPropertyValue(MemberName()));

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
