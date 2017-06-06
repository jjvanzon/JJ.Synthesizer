using JJ.Framework.Exceptions;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
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
            get => WrappedOperator.Name;
            set => WrappedOperator.Name = value;
        }

        /// <summary> Base will determine the Inlet display name based on its Dimension. </summary>
        public virtual string GetInletDisplayName(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (!WrappedOperator.Inlets.Contains(inlet))
            {
                throw new NotContainsException(() => WrappedOperator.Inlets, ValidationHelper.GetUserFriendlyIdentifier(inlet));
            }

            return ResourceFormatter.GetDisplayName(inlet.Dimension);
        }

        /// <summary> Base will determine the Outlet display name based on its Dimension. </summary>
        public virtual string GetOutletDisplayName(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (!WrappedOperator.Outlets.Contains(outlet))
            {
                throw new NotContainsException(() => WrappedOperator.Outlets, ValidationHelper.GetUserFriendlyIdentifier(outlet));
            }

            return ResourceFormatter.GetDisplayName(outlet.Dimension);
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper) => wrapper?.WrappedOperator;

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}