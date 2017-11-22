using System;

namespace JJ.Business.Synthesizer.Roslyn
{
	internal class ActivationInfo
	{
		public ActivationInfo(Type type, object[] args)
		{
			Type = type ?? throw new ArgumentNullException(nameof(type));
			Args = args ?? throw new ArgumentNullException(nameof(args));
		}

		public Type Type { get; }
		public object[] Args { get; }
	}
}