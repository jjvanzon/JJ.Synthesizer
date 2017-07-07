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

        /// <summary> Base will determine the Inlet display name based on its Dimension. </summary>
        public virtual string GetInletDisplayName(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (!WrappedOperator.Inlets.Contains(inlet))
            {
                throw new NotContainsException(() => WrappedOperator.Inlets, ValidationHelper.GetUserFriendlyIdentifier(inlet));
            }

            // Use Name
            if (!string.IsNullOrWhiteSpace(inlet.Name))
            {
                return ResourceFormatter.GetDisplayName(inlet);
            }

            // Use Dimension
            if (inlet.Dimension != null)
            {
                return ResourceFormatter.GetDisplayName(inlet.Dimension);
            }

            int listPosition = WrappedOperator.Inlets.IndexOf(inlet);
            string displayName = $"{ResourceFormatter.Inlet} {listPosition + 1}";
            return displayName;
        }

        /// <summary> Base will determine the Outlet display name based on its Dimension. </summary>
        public virtual string GetOutletDisplayName(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (!WrappedOperator.Outlets.Contains(outlet))
            {
                throw new NotContainsException(() => WrappedOperator.Outlets, ValidationHelper.GetUserFriendlyIdentifier(outlet));
            }

            // Use Name
            if (!string.IsNullOrWhiteSpace(outlet.Name))
            {
                return ResourceFormatter.GetDisplayName(outlet);
            }

            // Use Dimension
            if (outlet.Dimension != null)
            {
                return ResourceFormatter.GetDisplayName(outlet.Dimension);
            }

            // Use List Index (not Position, becuase it does not have to be consecutive).
            int listPosition = WrappedOperator.Outlets.IndexOf(outlet);
            string displayName = $"{ResourceFormatter.Outlet} {listPosition + 1}";
            return displayName;
        }

        public static implicit operator Operator(OperatorWrapperBase wrapper) => wrapper?.WrappedOperator;

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}