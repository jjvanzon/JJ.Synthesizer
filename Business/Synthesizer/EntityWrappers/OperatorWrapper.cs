using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

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
        public DimensionEnum StandardDimension => WrappedOperator.GetStandardDimensionEnum();

        public string CustomDimension
        {
            get => WrappedOperator.CustomDimensionName;
            set => WrappedOperator.CustomDimensionName = value;
        }

        public string Name
        {
            get => WrappedOperator.Name;
            set => WrappedOperator.Name = value;
        }

        /// <summary> Determines the Inlet display name based on its name, dimension or position. </summary>
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

            // Use List Index (not Position, becuase it does not have to be consecutive).
            int listIndex = WrappedOperator.Inlets.Sort().IndexOf(inlet);
            string displayName = $"{ResourceFormatter.Inlet} {listIndex + 1}";
            return displayName;
        }

        /// <summary> Determines the Outlet display name based on its name, dimension or position. </summary>
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
