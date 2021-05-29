using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class OperatorWrapper
    {
        public OperatorWrapper(Operator wrapperOperator)
        {
            WrappedOperator = wrapperOperator ?? throw new NullException(() => wrapperOperator);

            Inputs = new OperatorWrapper_Inputs(wrapperOperator);
            Inlets = new OperatorWrapper_Inlets(wrapperOperator);
            Outlets = new OperatorWrapper_Outlets(wrapperOperator);
        }

        public Operator WrappedOperator { get; }
        public OperatorWrapper_Inputs Inputs { get; }
        public OperatorWrapper_Inlets Inlets { get; }
        public OperatorWrapper_Outlets Outlets { get; }

        /// <summary> Determines the Inlet display name based on its name, dimension or position. </summary>
        public virtual string GetInletDisplayName(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (!WrappedOperator.Inlets.Contains(inlet))
            {
                throw new NotContainsException(() => WrappedOperator.Inlets, ValidationHelper.GetUserFriendlyIdentifier(inlet));
            }

            // Use Name
            string nameWithFallback = inlet.GetNameWithFallback();
            if (!string.IsNullOrWhiteSpace(nameWithFallback))
            {
                return ResourceFormatter.GetDisplayName(nameWithFallback);
            }

            // Use Dimension
            Dimension dimensionWithFallback = inlet.GetDimensionWithFallback();
            if (dimensionWithFallback != null)
            {
                return ResourceFormatter.GetDisplayName(dimensionWithFallback);
            }

            // Use List Index (not Position, because it does not have to be consecutive).
            int listIndex = WrappedOperator.Inlets.Sort().IndexOf(inlet);
            string displayName = $"{ResourceFormatter.Inlet} {listIndex + 1}";
            return displayName;
        }

        /// <summary> Determines the Outlet display name based on its name, dimension or position. </summary>
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual string GetOutletDisplayName(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            if (!WrappedOperator.Outlets.Contains(outlet))
            {
                throw new NotContainsException(() => WrappedOperator.Outlets, ValidationHelper.GetUserFriendlyIdentifier(outlet));
            }

            // Use Name
            string nameWithFallback = outlet.GetNameWithFallback();
            if (!string.IsNullOrWhiteSpace(nameWithFallback))
            {
                return ResourceFormatter.GetDisplayName(nameWithFallback);
            }

            // Use Dimension
            Dimension dimensionWithFallback = outlet.GetDimensionWithFallback();
            if (dimensionWithFallback != null)
            {
                return ResourceFormatter.GetDisplayName(dimensionWithFallback);
            }

            // Use List Index (not Position, because it does not have to be consecutive).
            int listPosition = WrappedOperator.Outlets.Sort().IndexOf(outlet);
            string displayName = $"{ResourceFormatter.Outlet} {listPosition + 1}";
            return displayName;
        }

        public static implicit operator Operator(OperatorWrapper wrapper) => wrapper?.WrappedOperator;

        /// <summary> Implicit for syntactic sugar. </summary>
        public static implicit operator Outlet(OperatorWrapper wrapper) => wrapper?.WrappedOperator.Outlets.Single();

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
