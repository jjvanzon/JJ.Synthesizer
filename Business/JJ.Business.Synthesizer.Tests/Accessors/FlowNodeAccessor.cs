using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FlowNodeAccessor
    {
        private readonly AccessorEx _accessor;
        
        public FlowNodeAccessor(FlowNode obj)
        {
            _accessor = new AccessorEx(obj);
        }

        public string DebuggerDisplay => (string)_accessor.GetPropertyValue(MemberName());
    }
}
