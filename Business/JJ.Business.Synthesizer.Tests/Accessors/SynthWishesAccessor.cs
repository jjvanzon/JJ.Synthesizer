using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class SynthWishesAccessor
    {
        private readonly AccessorCore _accessor;

        public SynthWishesAccessor(SynthWishes obj)
        {
            //_accessor = new AccessorCore(obj, typeof(SynthWishes));
            _accessor = new AccessorCore(obj);
        }
        
        /// <inheritdoc cref="wishdocs._captureindexer" />
        public SynthWishes _ 
            => (SynthWishes)_accessor.Get(nameof(_));

        public void Run(Action action)
            => _accessor.Call(MemberName(), action);

        public IList<FlowNode> FlattenTerms(FlowNode sumOrAdd) 
            => (IList<FlowNode>)_accessor.Call(MemberName(), sumOrAdd);

        public IList<FlowNode> FlattenFactors(IList<FlowNode> operands) 
            => (IList<FlowNode>)_accessor.Call(MemberName(), operands);

        public IList<FlowNode> FlattenFactors(FlowNode multiply) 
            => (IList<FlowNode>)_accessor.Call(MemberName(), multiply);

        public TapeRunnerAccessor     _tapeRunner => new TapeRunnerAccessor(_accessor.Get(MemberName()));
        public TapeCollectionAccessor _tapes      => new TapeCollectionAccessor(_accessor.Get(MemberName()));
        public ConfigResolverAccessor _config     => new ConfigResolverAccessor(_accessor.Get(MemberName()));

        // EchoAdditive

        public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => _accessor.Call(() => EchoAdditive(sound, count, magnitude, delay, callerMemberName));

        public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FlowNode EchoAdditive(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);

        public FlowNode EchoAdditive(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null) 
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude, delay, callerMemberName);

        // EchoFeedBack
        
        public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude = null, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call([ sound, count, magnitude, delay, callerMemberName ]);

        public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, FlowNode delay = null, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude, delay ?? _[0.25], callerMemberName);

        public FlowNode EchoFeedBack(FlowNode sound, int count, FlowNode magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude ?? _[0.66], delay, callerMemberName);
        
        public FlowNode EchoFeedBack(FlowNode sound, int count, double magnitude, double delay, [CallerMemberName] string callerMemberName = null)
            => (FlowNode)_accessor.Call(MemberName(), sound, count, magnitude, delay, callerMemberName);
    }
}
