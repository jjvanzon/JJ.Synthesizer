using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FlowNodeAccessor
    {
        private readonly AccessorCore _accessor;
        
        public FlowNodeAccessor(FlowNode obj)
        {
            _accessor = new AccessorCore(obj);
        }

        public string DebuggerDisplay => (string)_accessor.Get(MemberName());
    }
}
