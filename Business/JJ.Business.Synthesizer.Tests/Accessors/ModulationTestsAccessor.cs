using System;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Wishes.Reflection;
using JJ.Business.Synthesizer.Tests.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ModulationTestsAccessor
    {
        private readonly ModulationTests     _obj;
        private readonly AccessorEx   _accessor;
        private readonly SynthWishesAccessor _baseAccessor;
            
        public ModulationTestsAccessor(ModulationTests obj)
        {
            _obj          = obj ?? throw new ArgumentNullException(nameof(obj));
            _accessor     = new AccessorEx(obj);
            _baseAccessor = new SynthWishesAccessor(obj);
        }

        /// <inheritdoc cref="_captureindexer" />
        public SynthWishes _ => _baseAccessor._; 

        public void WithLeft() => _obj.WithLeft();
        public void WithRight() => _obj.WithRight();
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica1(FlowNode freq, FlowNode duration = null, FlowNode detuneDepth = null, FlowNode chorusRate = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1], detuneDepth ?? _[0.8], chorusRate ?? _[0.03]);

        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica2(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica3(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica4(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="_detunica" />
        public FlowNode Detunica5(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="_detunica" />
        public FlowNode DetunicaBass(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="_detunica" />
        public FlowNode DetunicaJingle
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode Vibraphase(FlowNode freq = null, FlowNode duration = null, FlowNode depthAdjust1 = null, FlowNode depthAdjust2 = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1], depthAdjust1 ?? _[0.005], depthAdjust2 ?? _[0.250]);
        
        public FlowNode VibraphaseChord
            => (FlowNode)_accessor.GetPropertyValue(MemberName());
    }
}
