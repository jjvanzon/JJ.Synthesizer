using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Helpers
{
	internal static class DebugHelper
	{
		public static string GetDebuggerDisplay(object obj)
		{
			if (obj == null) throw new NullException(() => obj);

			return obj.GetType().Name;
		}
	}
}
