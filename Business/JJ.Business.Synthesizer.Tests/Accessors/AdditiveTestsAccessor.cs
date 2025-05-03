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
    internal class AdditiveTestsAccessor(AdditiveTests obj)
    {
        private readonly AccessorCore _accessor = new(obj);
        //private readonly SynthWishesAccessor _baseAccessor = new(obj);

        /// <inheritdoc cref="wishdocs._captureindexer" />
        //public SynthWishes _ => _baseAccessor._;
        public SynthWishes _ => _accessor.Get<SynthWishes>();
        
        /// <inheritdoc cref="_metallophone"/>
        public FlowNode MetallophoneJingle()
            => (FlowNode)_accessor.Call();
        
        /// <inheritdoc cref="_metallophone"/>
        public FlowNode MetallophoneChord
            => (FlowNode)_accessor.Get();

        /// <inheritdoc cref="_metallophone"/>
        public FlowNode Metallophone(FlowNode freq, FlowNode duration = null)
            => (FlowNode)_accessor.Call(freq, duration);
    }
}
