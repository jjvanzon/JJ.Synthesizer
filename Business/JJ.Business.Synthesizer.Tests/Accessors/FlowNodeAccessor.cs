using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FlowNodeAccessor(FlowNode obj) : AccessorCore(obj)
    {
        public string DebuggerDisplay => Get<string>();
    }
}
