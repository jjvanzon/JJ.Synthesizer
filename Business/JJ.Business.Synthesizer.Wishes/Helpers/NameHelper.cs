using System.Runtime.CompilerServices;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class NameHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Name([CallerMemberName] string calledMemberName = null) 
            => calledMemberName.CutLeft("get_").CutLeft("set_");

        public static string GetAssemblyName<TType>() => typeof(TType).Assembly.GetName().Name;
    }
}