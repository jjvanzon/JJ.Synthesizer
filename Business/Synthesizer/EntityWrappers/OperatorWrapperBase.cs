using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public abstract class OperatorWrapperBase
    {
        public OperatorWrapperBase(Operator wrappedOperator)
        {
            if (wrappedOperator == null) throw new NullException(() => wrappedOperator);

            WrappedOperator = wrappedOperator;
        }

        public Operator WrappedOperator { get; }

        public string Name
        {
            get { return WrappedOperator.Name; }
            set { WrappedOperator.Name = value; }
        }

        public abstract string GetInletDisplayName(int listIndex);

        public abstract string GetOutletDisplayName(int listIndex);

        public static implicit operator Operator(OperatorWrapperBase wrapper) => wrapper?.WrappedOperator;

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}