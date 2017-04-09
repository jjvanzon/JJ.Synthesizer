using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public abstract class OperatorWrapperBase
    {
        public OperatorWrapperBase(Operator wrappedOperator)
        {
            WrappedOperator = wrappedOperator ?? throw new NullException(() => wrappedOperator);
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