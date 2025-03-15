using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Business.Synthesizer.Tests.docs;
using JJ.Framework.Reflection.Core;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class AdditiveTestsAccessor
    {
        private readonly AccessorEx   _accessor;
        private readonly SynthWishesAccessor _baseAccessor;

        public AdditiveTestsAccessor(AdditiveTests obj)
        {
            _accessor     = new AccessorEx(obj);
            _baseAccessor = new SynthWishesAccessor(obj);
        }

        /// <inheritdoc cref="wishdocs._captureindexer" />
        public SynthWishes _ => _baseAccessor._;
        
        /// <inheritdoc cref="_metallophone"/>
        public FlowNode MetallophoneJingle()
            => (FlowNode)_accessor.InvokeMethod(MemberName());
        
        /// <inheritdoc cref="_metallophone"/>
        public FlowNode MetallophoneChord
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        /// <inheritdoc cref="_metallophone"/>
        public FlowNode Metallophone(FlowNode freq, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq, duration ?? _[1]);
    }
}
