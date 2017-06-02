using System;
using JetBrains.Annotations;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class ActivationInfo
    {
        public ActivationInfo([NotNull] Type type, [NotNull] object[] args)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Args = args ?? throw new ArgumentNullException(nameof(args));
        }

        [NotNull] public Type Type { get; }
        [NotNull] public object[] Args { get; }
    }
}