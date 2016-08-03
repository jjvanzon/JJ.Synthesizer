using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        public static string GetOperatorIdentifier(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (!String.IsNullOrEmpty(op.Name))
            {
                return op.Name;
            }

            if (op.OperatorType != null)
            {
                string operatorTypeDisplayName = ResourceHelper.GetDisplayName(op.OperatorType);
                return operatorTypeDisplayName;
            }

            return op.ID.ToString();
        }

        public static string GetInletIdentifier(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // Use Name
            if (!String.IsNullOrEmpty(inlet.Name))
            {
                return inlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                // Downside: Dimension might not be unique among an operator's inlets.
                string dimensionDisplayName = ResourceHelper.GetDisplayName(dimensionEnum);
                return dimensionDisplayName;
            }

            // Use ListIndex
            string inletIdentifier = String.Format("{0} {1}", PropertyDisplayNames.Inlet, inlet.ListIndex);
            return inletIdentifier;
        }

        public static string GetOutletIdentifier(Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            // Use Name
            if (!String.IsNullOrEmpty(outlet.Name))
            {
                return outlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                // Downside: Dimension might not be unique among an operator's outlets.
                string dimensionDisplayName = ResourceHelper.GetDisplayName(dimensionEnum);
                return dimensionDisplayName;
            }

            // Use ListIndex
            string outletIdentifier = String.Format("{0} {1}", PropertyDisplayNames.Outlet, outlet.ListIndex);
            return outletIdentifier;
        }

        public static double? TryGetConstantNumberFromInlet(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            if (inlet.InputOutlet?.Operator?.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
            {
                return null;
            }

            if (DataPropertyParser.DataIsWellFormed(inlet.InputOutlet.Operator.Data))
            {
                double? number = DataPropertyParser.TryParseDouble(inlet.InputOutlet.Operator, PropertyNames.Number);
                return number;
            }

            return null;
        }
    }
}
