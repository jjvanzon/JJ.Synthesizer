using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FlowNodeAccessor
    {
        private readonly Accessor _accessor;
        
        public FlowNodeAccessor(FlowNode obj)
        {
            _accessor = new Accessor(obj);
        }

        public string DebuggerDisplay => (string)_accessor.GetPropertyValue(MemberName());
    }
}
