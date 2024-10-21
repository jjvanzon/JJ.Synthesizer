using System;
using System.Reflection;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class CopiedFromFramework
    {
        public static bool IsProperty(this MethodBase method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            bool isProperty = method.Name.StartsWith("get_") ||
                              method.Name.StartsWith("set_");

            return isProperty;
        }
    }
}
